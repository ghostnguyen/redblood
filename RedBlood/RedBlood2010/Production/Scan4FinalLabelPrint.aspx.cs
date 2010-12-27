using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using RedBlood;
using RedBlood.BLL;
public partial class Production_Scan4FinalLabelPrint : System.Web.UI.Page
{
    public string CurrentProductCode
    {
        get
        {
            if (ViewState["CurrentProductCode"] == null)
                return "";
            return (string)ViewState["CurrentProductCode"];
        }
        set
        {
            ViewState["CurrentProductCode"] = value;
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

    public List<Guid> PackList
    {
        get
        {
            if (ViewState["PackList"] == null)
            {
                ViewState["PackList"] = new List<Guid>();
            }
            return (List<Guid>)ViewState["PackList"];
        }
        set
        {
            ViewState["PackList"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
        else
        {
            string code = Master.TextBoxCode.Text.Trim();
            Master.TextBoxCode.Text = "";

            if (BarcodeBLL.IsValidDINCode(code))
            {
                AddPack(BarcodeBLL.ParseDIN(code));
            }
            else if (BarcodeBLL.IsValidProductCode(code))
            {
                LoadCurrentProduct(BarcodeBLL.ParseProductCode(code));
            }
        }
    }

    void AddPack(string DIN)
    {
        Pack p = PackBLL.Get(DIN, CurrentProductCode);

        if (PackList.Contains(p.ID))
        {
            throw new Exception("Đã nhập túi máu này.");
        }

        if (p.Donation.TestResultStatus != Donation.TestResultStatusX.Âm_tính)
        {
            throw new Exception(p.Donation.TestResultStatus.ToString());
        }

        PackList.Add(p.ID);
        ShowInfo();
    }

    void ShowInfo()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        var v = db.Packs.Where(r => PackList.Contains(r.ID))
            .Select(r => new
            {
                r.ID,
                DIN = r.Donation.DIN,
                r.ProductCode,
            }).ToList();

        DataListPack.DataSource = v;
        DataListPack.DataBind();

        GridViewSum.DataSource = v.GroupBy(r => r.ProductCode)
            .Select(r => new
            {
                ProductCode = r.Key,
                Sum = r.Count()
            });
        GridViewSum.DataBind();

        CurrentDIN = "";
        imgCurrentDIN.ImageUrl = "none";
    }

    protected void btnPackRemove_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;

        if (btn != null)
        {
            PackList.Remove(btn.CommandArgument.ToGuid());
            ShowInfo();
        }
    }

    void LoadCurrentProduct(string productCode)
    {
        var v = ProductBLL.Get(productCode);
        if (v != null)
        {
            CurrentProductCode = productCode;
            imgCurrentProductCode.ImageUrl = BarcodeBLL.Url4Product(productCode);
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Production/FinalLabelPrint.aspx?PackList=" + string.Join(",", PackList.Select(r => r.ToString()).ToArray()));
    }
}
