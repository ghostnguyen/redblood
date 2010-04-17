using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class Order_Return : System.Web.UI.Page
{
    public int ReturnID
    {
        get
        {
            if (ViewState["ReturnID"] == null)
                return 0;
            return (int)ViewState["ReturnID"];
        }
        set
        {
            ViewState["ReturnID"] = value;
            LoadReturn();
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

    public List<int> PackOrderList
    {
        get
        {
            if (ViewState["PackOrderList"] == null)
            {
                ViewState["PackOrderList"] = new List<int>();
            }
            return (List<int>)ViewState["PackOrderList"];
        }
        set
        {
            ViewState["PackOrderList"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int returnID = Request.Params["key"].ToInt();

            if (returnID != 0)
            {
                ReturnID = returnID;
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
            else if (BarcodeBLL.IsValidReturnCode(code))
            {
                ReturnID = BarcodeBLL.ParseReturnID(code);
            }
            else if (BarcodeBLL.IsValidProductCode(code))
            {
                AddPackOrder(BarcodeBLL.ParseProductCode(code));
            }
        }
    }

    void AddPackOrder(string productCode)
    {
        PackOrder po = PackOrderBLL.Get4Return(CurrentDIN, productCode);

        if (PackOrderList.Contains(po.ID))
        {
            throw new Exception("Đã nhập túi máu này.");
        }

        PackOrderList.Add(po.ID);

        GridViewPack.DataBind();

        CurrentDIN = "";
        imgCurrentDIN.ImageUrl = "none";
    }

    void LoadCurrentDIN(string DIN)
    {
        Donation e = DonationBLL.Get(DIN);

        CurrentDIN = e.DIN;
        imgCurrentDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN);
    }

    protected void btnNewReturn_Click(object sender, EventArgs e)
    {
        ReturnID = 0;
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (ReturnID != 0) return;

        LinkButton btn = sender as LinkButton;
        if (btn != null)
        {
            if (PackOrderList.Remove(btn.CommandArgument.ToInt()))
                GridViewPack.DataBind();
        }
    }

    public void LoadReturn()
    {
        Return e = new Return();
        if (ReturnID == 0)
        {
            imgOrder.ImageUrl = "none";
            btnOk.Enabled = true;
        }
        else
        {
            e = ReturnBLL.Get(ReturnID);
            imgOrder.ImageUrl = BarcodeBLL.Url4Return(e.ID);
            btnOk.Enabled = false;
        }

        txtNote.Text = e.Note;

        txtDate.Text = e.Date.ToStringVN_Hour();

        PackOrderList = e.ReturnPackOrders.Select(r => r.PackOrderID.Value).ToList();

        GridViewPack.DataBind();

        CurrentDIN = "";
        imgCurrentDIN.ImageUrl = "none";
    }

    protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.PackOrders.Where(r => PackOrderList.Contains(r.ID))
            .Select(r => new
            {
                r.ID,
                r.OrderID,
                OrderType = r.Order.Type,
                DIN = r.Pack.Donation.DIN,
                r.Pack.ProductCode,
                Expired = r.Pack.ExpirationDate.Value.Expired()
            });
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        ReturnID = ReturnBLL.Add(PackOrderList, txtNote.Text.Trim());

        this.Alert("Lưu thành công.");
    }
}
