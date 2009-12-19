using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_Extract : System.Web.UI.Page
{
    public List<string> ProductCodeList
    {
        get
        {
            if (ViewState["ProductCodeList"] == null)
            {
                ViewState["ProductCodeList"] = new List<string>();
            }
            return (List<string>)ViewState["ProductCodeList"];
        }
        set
        {
            ViewState["ProductCodeList"] = value;
        }
    }

    public List<Guid> PackInList
    {
        get
        {
            if (ViewState["PackInList"] == null)
            {
                ViewState["PackInList"] = new List<Guid>();
            }
            return (List<Guid>)ViewState["PackInList"];
        }
        set
        {
            ViewState["PackInList"] = value;
        }
    }

    public string DIN
    {
        get
        {
            if (ViewState["DIN"] == null)
            {
                ViewState["DIN"] = "";
            }
            return (string)ViewState["DIN"];
        }
        set
        {
            ViewState["DIN"] = value;
        }
    }

    public string Code
    {
        get
        {
            if (ViewState["Code"] == null)
            {
                ViewState["Code"] = "";
            }
            return (string)ViewState["Code"];
        }
        set
        {
            ViewState["Code"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (BarcodeBLL.IsValidDINCode(code))
        {
            if (RadioButtonList1.SelectedValue.ToInt() == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "",
                    "alert ('Chuyển sang: Nhập mã chính.');", true);
            }
            if (RadioButtonList1.SelectedValue.ToInt() == 2)
            {
                Donation d = DonationBLL.Get(BarcodeBLL.ParseDIN(code));
                if (d == null) return;

                if (d.TestResultStatus == Donation.TestResultStatusX.Positive
                    || d.TestResultStatus == Donation.TestResultStatusX.PositiveLocked)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi",
                    "alert ('Túi máu KQXN: Positive');", true);
                    return;
                }

                RedBloodDataContext db = new RedBloodDataContext();

                if (db.Packs.Where(r => r.DIN == d.DIN && ProductCodeList.Contains(r.ProductCode)).Count() > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông báo",
                    "alert ('Đã có sản phẩm này');", true);
                    return;
                }

                if (db.Packs.Where(r => r.DIN == d.DIN && PackInList.Contains(r.ID)).Count() > 0)
                {
                    //TODO: Thông báo
                    return;
                }

                DIN = d.DIN;

                ImageCurrentDIN.ImageUrl = BarcodeBLL.Url4DIN(DIN, "00");
            }
        }
        else if (BarcodeBLL.IsValidProductCode(code))
        {
            if (RadioButtonList1.SelectedValue.ToInt() == 1)
            {
                EnterProductCode(BarcodeBLL.ParseProductCode(code));
            }
            if (RadioButtonList1.SelectedValue.ToInt() == 2)
            {
                EnterProductCode2(BarcodeBLL.ParseProductCode(code));
            }
        }
    }

    private void Clear()
    {

    }

    void EnterProductCode(string productCode)
    {
        if (!ProductCodeList.Contains(productCode))
        {
            Product e = ProductBLL.Get(productCode);
            if (e == null) return;

            ProductCodeList.Add(productCode);
            DataListProduct.DataBind();
        }
    }

    void EnterProductCode2(string code)
    {
        if (string.IsNullOrEmpty(DIN)) return;

        if (ProductCodeList.Contains(code)) return;

        Pack p = PackBLL.Get(DIN, code);

        if (p == null) return;

        if (PackInList.Contains(p.ID)) return;

        PackInList.Add(p.ID);

        DIN = "";
        ImageCurrentDIN.ImageUrl = "none";

        DataListPack.DataBind();
    }


    protected void LinqDataSourceProduct_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = db.Products.Where(r => ProductCodeList.Contains(r.Code));
    }

    protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = db.Packs.Where(r => PackInList.Contains(r.ID));
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        DIN = "";
        ImageCurrentDIN.ImageUrl = "none";

        RadioButtonList1.SelectedValue = "1";

        ProductCodeList.Clear();
        DataListProduct.DataBind();

        PackInList.Clear();
        DataListPack.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PackErr err = PackBLL.Extract(PackInList, ProductCodeList);

        if (err != PackErrEnum.Non)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi",
                    "alert ('" + err.Message + "');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "",
                    "alert ('Lưu thành công.');", true);
        }

    }
}
