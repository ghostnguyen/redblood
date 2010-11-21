using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using RedBlood;
using RedBlood.BLL;
public partial class Store_Order4Org : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            int orgID = Request.Params["key"].ToInt();

            if (orgID != 0)
            {
                OrderID = orgID;
            }
        }
        else
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
                //People1.Code = code;
            }
            else if (code.Length >= 9)
            {
                //People1.Code = code;
            }
        }
    }

    void AddPack(string productCode)
    {
        PackOrderBLL.Add(OrderID, CurrentDIN, productCode);

        GridViewPack.DataBind();

        CurrentDIN = "";
        imgCurrentDIN.ImageUrl = "none";
        GridViewSum.DataBind();
    }

    void LoadCurrentDIN(string DIN)
    {
        Donation e = DonationBLL.Get4Order(DIN);

        CurrentDIN = e.DIN;
        imgCurrentDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN);
    }

    protected void btnNew4CR_Click(object sender, EventArgs e)
    {
        OrderID = 0;

        Clear();
        btnUpdate.Enabled = true;
    }

    public void Clear()
    {
        OrderID = 0;

        imgOrder.ImageUrl = "none";

        txtDate.Text = "";
        txtNote.Text = "";

        GridViewPack.DataBind();

        CurrentDIN = "";
        imgCurrentDIN.ImageUrl = "none";

        GridViewSum.DataBind();
    }

    public void LoadOrder()
    {
        Order e = new Order();
        if (OrderID == 0)
        {
            imgOrder.ImageUrl = "none";
            txtOrg.Text = "";
            //People1.PeopleID = Guid.Empty;
        }
        else
        {
            e = OrderBLL.Get(OrderID);

            if (e.Type == Order.TypeX.ForOrg)
            {
                imgOrder.ImageUrl = BarcodeBLL.Url4Order(e.ID);
                txtOrg.Text = e.Org != null ? e.Org.Name : "";
            }
            else if (e.Type == Order.TypeX.ForCR)
            {
                Response.Redirect(RedBloodSystem.Url4Order4CR + "key=" + e.ID.ToString());
            }
        }

        txtNote.Text = e.Note;

        if (e.Date != null)
            txtDate.Text = e.Date.ToStringVN_Hour();

        txtTransfusionNote.Text = e.TransfusionNote;

        GridViewPack.DataBind();

        btnUpdate.Enabled = e.Status == Order.StatusX.Init;

        CurrentDIN = "";
        imgCurrentDIN.ImageUrl = "none";

        GridViewSum.DataBind();
    }

    protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var v = db.PackOrders.Where(r => r.OrderID.Value == OrderID
            && !r.ReturnID.HasValue).ToList();

        v.Reverse();

        e.Result = v;
    }

    protected void LinqDataSourceSum_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var v = db.PackOrders.Where(r => r.OrderID.Value == OrderID
            && !r.ReturnID.HasValue).ToList()
            .GroupBy(r => r.Pack.ProductCode)
            .Select(r => new
            {
                ProductCode = r.Key,
                Sum = r.Count()
            });

        e.Result = v;
    }


    protected void GridViewPack_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ReturnBLL.Add(new List<int>() { e.Keys[0].ToInt() }, txtRemoveNoteGlobal.Text.Trim());
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

        p.Type = Order.TypeX.ForOrg;
        p.Note = txtNote.Text.Trim();

        p.SetOrgID(txtOrg.Text.Trim());

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
