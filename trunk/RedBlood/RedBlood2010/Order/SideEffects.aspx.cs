using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Order_SideEffects : System.Web.UI.Page
{
    public string DIN
    {
        get
        {
            if (ViewState["DIN"] == null)
                return "";
            return (string)ViewState["DIN"];
        }
        set
        {
            ViewState["DIN"] = value;
        }
    }

    public string ProductCode
    {
        get
        {
            if (ViewState["ProductCode"] == null)
                return "";
            return (string)ViewState["ProductCode"];
        }
        set
        {
            ViewState["ProductCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (BarcodeBLL.IsValidDINCode(code))
        {
            LoadDIN(BarcodeBLL.ParseDIN(code));
        }
        else if (BarcodeBLL.IsValidProductCode(code))
        {
            LoadPack(BarcodeBLL.ParseProductCode(code));
        }
    }

    void LoadDIN(string code)
    {
        Donation e = DonationBLL.Get(code);
        if (e == null)
        { }
        else
        {
            DIN = e.DIN;
            ImageCurrentDIN.ImageUrl = BarcodeBLL.Url4DIN(e.DIN);

            ProductCode = "";
            ImageProduct.ImageUrl = "none";
        }
    }

    void LoadPack(string productCode)
    {
        Pack p = PackBLL.Get4ReportSideEffects(DIN, productCode);

        ProductCode = productCode;
        ImageProduct.ImageUrl = BarcodeBLL.Url4Product(p.ProductCode);
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        PackSideEffectBLL.Add(DIN, ProductCode, txtSideEffect.Text.Trim(), txtNote.Text.Trim());

        GridViewSideEffect.DataBind();

        txtSideEffect.Text = txtNote.Text = "";

        Page.Alert("Lưu thành công.");
    }

    protected void LinqDataSourceSideEffect_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (!string.IsNullOrEmpty(DIN) && !string.IsNullOrEmpty(ProductCode))
            e.Result = PackSideEffectBLL.Get(DIN, ProductCode);
    }
}
