using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Order : System.Web.UI.UserControl
{
    public event EventHandler OrderChanged;


    public Order.Typex OrderType { get; set; }
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (OrderID == 0)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            Order p = new Order();

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
        p.Note = txtNote.Text;

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
    public void New(Order.Typex type)
    {
        Clear();

        OrderType = type;
        txtName.Focus();

        if (OrderType == Order.Typex.ToOrg)
            rowPeople.Attributes.Add("style", "visibility:collapse;");

        if (OrderType == Order.Typex.ToPeople)
            rowOrg.Attributes.Add("style", "visibility:collapse;");
    }
    public void Clear()
    {
        ViewState["OrderID"] = 0;
        imgCodabar.Attributes.Add("src", "none");
        txtName.Text = "";
        txtDate.Text = "";
        txtNote.Text = "";

        divErrName.Attributes["class"] = "hidden";
        rowOrg.Attributes.Remove("style");
        rowPeople.Attributes.Remove("style");
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

            imgCodabar.Attributes.Add("src", "../Codabar/Image.aspx?hasText=true&code="
                + CodabarBLL.GenStringCode(Resources.Codabar.orderSSC, e.ID.ToString()));

            txtName.Text = e.Name;
            txtNote.Text = e.Note;

            if (e.Date != null)
                txtDate.Text = e.Date.ToStringVN_Hour();
        }
    }
}
