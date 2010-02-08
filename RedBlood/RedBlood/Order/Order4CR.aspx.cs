using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class Order_Order4CR : System.Web.UI.Page
{
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
            ViewState["OrderID"] = value;
            LoadOrder();
        }
    }

    public string CurrentDIN
    {
        get
        {
            if (ViewState["CurrentDIN"] == null)
                return "";
            return (string)ViewState["CurrentDIN"];
        }
        set
        {
            ViewState["CurrentDIN"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (BarcodeBLL.IsValidDINCode(code))
        {
            LoadCurrentDIN(BarcodeBLL.ParseDIN(code));
        }
        else if (BarcodeBLL.IsValidOrderCode(code))
        {
            OrderID = BarcodeBLL.ParseOrderID(code);
        }
        else if (BarcodeBLL.IsValidProductCode(code))
        {
            AddPack(BarcodeBLL.ParseProductCode(code));
        }
        else if (BarcodeBLL.IsValidPeopleCode(code))
        {
            People1.Code = code;
        }
        else if (code.Length >= 9)
        {
            People1.Code = code;
        }
    }

    void AddPack(string productCode)
    {
        OrderBLL.Add(OrderID, CurrentDIN, productCode);

        GridViewPack.DataBind();

        CurrentDIN = "";
        ImageCurrentDIN.ImageUrl = "none";
    }

    void LoadCurrentDIN(string DIN)
    {
        Donation e = OrderBLL.GetDIN4Order(DIN);

        CurrentDIN = e.DIN;
        ImageCurrentDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN);
    }

    protected void btnNew4CR_Click(object sender, EventArgs e)
    {
        Clear();
        btnUpdate.Enabled = true;
    }

    public void Clear()
    {
        OrderID = 0;

        imgCodabar.ImageUrl = "none";

        txtDate.Text = "";
        txtNote.Text = "";
        txtDept.Text = "";
        txtRoom.Text = "";
        txtBed.Text = "";
        txtDiagnosis.Text = "";
        txtPatientCode.Text = "";
        txtTransfusionNote.Text = "";
        People1.PeopleID = Guid.Empty;

        GridViewPack.DataBind();
    }

    public void LoadOrder()
    {
        Order e = OrderBLL.Get(OrderID);

        imgCodabar.ImageUrl = BarcodeBLL.Url4Order(e.ID);

        txtNote.Text = e.Note;

        if (e.Date != null)
            txtDate.Text = e.Date.ToStringVN_Hour();

        if (e.People != null)
        {
            People1.PeopleID = e.PeopleID.GetValueOrDefault();
        }

        txtDept.Text = e.FullDepartment;
        txtRoom.Text = e.Room;
        txtBed.Text = e.Bed;
        txtDiagnosis.Text = e.Diagnosis;
        txtPatientCode.Text = e.PatientCode;
        txtTransfusionNote.Text = e.TransfusionNote;

        GridViewPack.DataBind();

        btnUpdate.Enabled = e.Status == Order.StatusX.Init;
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
        this.Alert("Lưu thành công.");
        
    }


    private bool LoadFromGUI(Order p)
    {
        bool isDone = true;

        if (p.Date == null) p.Date = DateTime.Now;

        p.Type = Order.TypeX.ForPeople;
        p.Note = txtNote.Text.Trim();

        p.PeopleID = People1.PeopleID;

        p.SetDepartment(txtDept.Text.Trim());

        p.Room = txtRoom.Text.Trim();
        p.Bed = txtBed.Text.Trim();
        p.Diagnosis = txtDiagnosis.Text.Trim();
        p.PatientCode = txtPatientCode.Text.Trim();
        p.TransfusionNote = txtTransfusionNote.Text.Trim();

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
}
