using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Order : System.Web.UI.UserControl
{
    public event EventHandler OrderChanged;

    public Order.TypeX OrderType
    {
        get
        {
            if (ViewState["OrderType"] == null)
                return 0;
            return (Order.TypeX)ViewState["OrderType"];
        }
        set
        {
            ViewState["OrderType"] = value;
        }
    }
    public int OrderID
    {
        get
        {
            if (ViewState["OrderID"] == null)
                return 0;
            return (int)ViewState["OrderID"];
        }
        set
        {
            Clear();

            ViewState["OrderID"] = value;
            if (value == 0)
            { }
            else
            {
                LoadOrder();
            }
            if (OrderChanged != null)
                OrderChanged(value, null);
        }
    }

    public string Code
    {
        set
        {
            //string code = value.Trim();
            //if (BarcodeBLL.IsValidPackCode(code))
            //{
            //    AddPack(BarcodeBLL.ParsePackAutoNum(code));
            //}
            //else if (BarcodeBLL.IsValidOrderCode(code))
            //{
            //    OrderID = BarcodeBLL.ParseOrderID(code);
            //}
            //else if (BarcodeBLL.IsValidPeopleCode(code))
            //{
            //    People1.Code = code;
            //}
            //else if (code.Length >= 9)
            //{
            //    People1.Code = code;
            //}
            //else
            //{ }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rowOrg.Attributes.Add("style", "visibility:collapse;");

            OrderType = Order.TypeX.ToPeople;
        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (OrderID == 0)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            Order p = new Order();
            p.Status = Order.StatusX.Init;

            if (LoadFromGUI(p))
            {
                db.Orders.InsertOnSubmit(p);
                db.SubmitChanges();
                OrderID = p.ID;
            }
            else
                return;
        }
        else
        {
            RedBloodDataContext db = new RedBloodDataContext();

            Order p = OrderBLL.Get(OrderID, db);

            if (p == null) return;


            if (LoadFromGUI(p))
            {
                db.SubmitChanges();
                OrderID = p.ID;
            }
            else return;
        }

        ScriptManager.RegisterStartupScript(btnUpdate, btnUpdate.GetType(), "SaveDone", "alert ('Lưu thành công.');", true);
    }


    private bool LoadFromGUI(Order p)
    {
        bool isDone = true;

        try
        {
            p.Name = txtName.Text.Trim();
            divErrName.Attributes["class"] = "hidden";
        }
        catch (Exception ex)
        {
            divErrName.InnerText = ex.Message;
            divErrName.Attributes["class"] = "err";
            isDone = false;
        }

        if (p.Date == null) p.Date = DateTime.Now;

        p.Type = OrderType;
        p.Note = txtNote.Text.Trim();

        if (OrderType == Order.TypeX.ToOrg)
        {
            try
            {
                p.SetOrgID(txtOrgName.Text.Trim());
                divErrOrgName.Attributes["class"] = "hidden";
            }
            catch (Exception ex)
            {
                divErrOrgName.Attributes["class"] = "err";
                divErrOrgName.InnerText = ex.Message;
                isDone = false;
            }
        }

        if (OrderType == Order.TypeX.ToPeople)
        {
            p.PeopleID = People1.PeopleID;
            
            p.SetDepartment(txtDept.Text.Trim());
            
            p.Room = txtRoom.Text.Trim();
            p.Bed = txtBed.Text.Trim();
            p.Diagnosis = txtDiagnosis.Text.Trim();
            p.PatientCode = txtPatientCode.Text.Trim();
            p.TransfusionNote = txtTransfusionNote.Text.Trim();
        }

        return isDone;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //if (CampaignID == 0)
        //{
        //    return;
        //}
        //else
        //{
        //    try
        //    {
        //        string m = bll.Delete(CampaignID);
        //        Clear();

        //        ScriptManager.RegisterStartupScript(btnDelete, btnDelete.GetType(), "", "alert ('" + m + "');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(btnDelete, btnDelete.GetType(), "", "alert ('" + ex.Message + "');", true);
        //    }
        //}
    }
    public void New(Order.TypeX type)
    {
        Clear();

        OrderType = type;
        txtName.Focus();
        btnUpdate.Enabled = true;
        SwitchGUI();
    }
    public void Clear()
    {
        ViewState["OrderID"] = 0;

        //imgCodabar.Attributes.Add("src", "none");
        imgCodabar.ImageUrl = "none";

        txtName.Text = "";
        txtDate.Text = "";
        txtNote.Text = "";
        txtOrgName.Text = "";
        txtDept.Text = "";
        txtRoom.Text = "";
        txtBed.Text = "";
        txtDiagnosis.Text = "";
        txtPatientCode.Text = "";
        txtTransfusionNote.Text = "";
        People1.PeopleID = Guid.Empty;

        divErrName.Attributes["class"] = "hidden";

        rowOrg.Attributes.Remove("style");
        rowPeople.Visible = true;

        GridViewPack.DataBind();
    }

    public void LoadOrder()
    {
        Order e = OrderBLL.Get(OrderID);

        if (e == null)
        {
        }
        else
        {
            OrderType = e.Type;

            imgCodabar.ImageUrl = BarcodeBLL.Url4Order(e.ID);

            txtName.Text = e.Name;
            txtNote.Text = e.Note;

            if (e.Date != null)
                txtDate.Text = e.Date.ToStringVN_Hour();

            if (OrderType == Order.TypeX.ToOrg && e.Org != null)
            {
                txtOrgName.Text = e.Org.Name;
            }

            if (OrderType == Order.TypeX.ToPeople && e.People != null)
            {
                People1.PeopleID = e.PeopleID.GetValueOrDefault();
                txtDept.Text = e.FullDepartment;
                txtRoom.Text = e.Room;
                txtBed.Text = e.Bed;
                txtDiagnosis.Text = e.Diagnosis;
                txtPatientCode.Text = e.PatientCode;
                txtTransfusionNote.Text = e.TransfusionNote;
            }

            GridViewPack.DataBind();
            SwitchGUI();

            btnUpdate.Enabled = e.Status == Order.StatusX.Init;
        }
    }

    void SwitchGUI()
    {
        if (OrderType == Order.TypeX.ToOrg)
            rowPeople.Visible = false;

        if (OrderType == Order.TypeX.ToPeople)
            rowOrg.Attributes.Add("style", "visibility:collapse;");
    }

    void AddPack(int autonum)
    {
        PackErr err = OrderBLL.Add(OrderID, autonum);

        if (err == null || err == PackErrEnum.Non)
        {
            GridViewPack.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông tin", "alert ('" + err.Message + "');", true);
        }
    }
    protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.PackOrders.Where(r => r.OrderID.Value == OrderID
            && r.Status != PackOrder.StatusX.Return);
    }


    protected void GridViewPack_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OrderBLL.Remove(e.Keys[0].ToInt(), txtRemoveNoteGlobal.Text.Trim());
    }
}
