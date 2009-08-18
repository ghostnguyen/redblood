using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Order4Org : System.Web.UI.UserControl
{
    public event EventHandler OrderChanged;

    public Order.TypeX OrderType = Order.TypeX.ForOrg;

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

    public string Code
    {
        set
        {
            string code = value.Trim();
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
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

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

        ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveDone", "alert ('Lưu thành công.');", true);
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

        if (OrderType == Order.TypeX.ForOrg)
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

        return isDone;
    }

    void LoadCurrentDIN(string code)
    {
        Donation e = DonationBLL.Get(code);
        if (e == null) return;

        if (e.TestResultStatus == Donation.TestResultStatusX.Negative
            || e.TestResultStatus == Donation.TestResultStatusX.NegativeLocked)
        { }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert ('Túi máu " + e.TestResultStatus + "');", true);
        }

        CurrentDIN = e.DIN;
        ImageCurrentDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN);
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
        //btnUpdate.Enabled = true;
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

        divErrName.Attributes["class"] = "hidden";

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

            if (OrderType == Order.TypeX.ForOrg && e.Org != null)
            {
                txtOrgName.Text = e.Org.Name;
            }

            GridViewPack.DataBind();

            //btnUpdate.Enabled = e.Status == Order.StatusX.Init;
        }
    }

    void AddPack(string productCode)
    {
        PackErr err = OrderBLL.Add(OrderID, CurrentDIN, productCode);

        if (err == null || err == PackErrEnum.Non)
        {
            GridViewPack.DataBind();

            CurrentDIN = "";
            ImageCurrentDIN.ImageUrl = "none";
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
        //OrderBLL.Remove(e.Keys[0].ToInt(), txtRemoveNoteGlobal.Text.Trim());
    }
}
