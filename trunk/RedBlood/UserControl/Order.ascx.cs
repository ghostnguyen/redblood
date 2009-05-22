using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Order : System.Web.UI.UserControl
{
    public event EventHandler OrderChanged;

    public Order.Typex OrderType
    {
        get
        {
            if (ViewState["OrderType"] == null)
                return 0;
            return (Order.Typex)ViewState["OrderType"];
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
            People1.Code = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //rowPeople.Attributes.Add("style", "visibility:collapse;");
            rowPeople.Visible = false;

            rowOrg.Attributes.Add("style", "visibility:collapse;");
        }

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
        p.Note = txtNote.Text.Trim();

        if (OrderType == Order.Typex.ToOrg)
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

        if (OrderType == Order.Typex.ToPeople)
        {
            p.PeopleID = People1.PeopleID;
            p.Dept = txtDept.Text.Trim();
            p.Room = txtRoom.Text.Trim();
            p.Bed = txtBed.Text.Trim();
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
    public void New(Order.Typex type)
    {
        Clear();

        OrderType = type;
        txtName.Focus();

        SwitchGUI();
    }
    public void Clear()
    {
        ViewState["OrderID"] = 0;
        imgCodabar.Attributes.Add("src", "none");
        txtName.Text = "";
        txtDate.Text = "";
        txtNote.Text = "";
        txtOrgName.Text = "";
        //PeopleOrder1.PeopleID = Guid.Empty;
        People1.PeopleID = Guid.Empty;

        divErrName.Attributes["class"] = "hidden";
        rowOrg.Attributes.Remove("style");
        //rowPeople.Attributes.Remove("style");
        rowPeople.Visible = true;
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

            if (OrderType == Order.Typex.ToOrg && e.Org != null)
            {
                txtOrgName.Text = e.Org.Name;
            }

            if (OrderType == Order.Typex.ToPeople && e.People != null)
            {
                People1.PeopleID = e.PeopleID.GetValueOrDefault();
                txtDept.Text = e.Dept;
                txtRoom.Text = e.Room;
                txtBed.Text = e.Bed;
            }
            SwitchGUI();
        }
    }

    void SwitchGUI()
    {
        if (OrderType == Order.Typex.ToOrg)
            //rowPeople.Attributes.Add("style", "visibility:collapse;");
            rowPeople.Visible = false;

        if (OrderType == Order.Typex.ToPeople)
            rowOrg.Attributes.Add("style", "visibility:collapse;");
    }
}
