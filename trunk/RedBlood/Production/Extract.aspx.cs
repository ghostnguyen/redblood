using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_Extract : System.Web.UI.Page
{
    public int Autonum
    {
        get
        {
            if (ViewState["AutoNum"] == null) return 0;
            return (int)ViewState["AutoNum"];
        }
        set
        {
            Pack p = PackBLL.Get4Production_Extract(value);
            if (p == null)
            {
                ViewState["AutoNum"] = 0;
            }
            else
            {
                ViewState["AutoNum"] = value;
                LoadPack();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnProduct.Enabled = false;
        }

        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(code))
        {
            Autonum = CodabarBLL.ParsePackAutoNum(code);
        }
        else if (CodabarBLL.IsValidTestResultCode(code))
        {

        }
        else if (CodabarBLL.IsValidCampaignCode(code))
        {

        }
        else
        {

        }
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (Autonum == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {

            Pack p = PackBLL.Get4Production_Extract(Autonum);
            e.Result = p;
        }
    }

    protected void LinqDataSourceExtract_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (Autonum == 0)
        {
            e.Result = null;
            e.Cancel = true;
            return;
        }

        Pack p = PackBLL.Get4Production_Extract(Autonum);

        if (p != null && p.PackExtractsBySource.Count > 0)
        {
            e.Result = p.PackExtractsBySource;
            btnProduct.Enabled = false;
        }
        else
        {
            e.Cancel = true;
            btnProduct.Enabled = true;
        }

    }



    void LoadPack()
    {
        Pack p = PackBLL.Get4Production_Extract(Autonum);

        if (p != null)
        {
            DetailsViewPack.DataBind();
            GridViewExtract.DataBind();
        }
    }

    protected void btnProduct_Click(object sender, EventArgs e)
    {
        PackErr err = PackBLL.Extract(Autonum, Page.User.Identity.Name);

        if (err == null || err == PackErrList.Non)
        {
            //GridViewPack.DataBind();
            LoadPack();
        }
        else
        {
            ScriptManager.RegisterStartupScript(btnProduct, btnProduct.GetType(), "Thông tin", "alert ('" + err.Message + "');", true);
        }

    }
}
