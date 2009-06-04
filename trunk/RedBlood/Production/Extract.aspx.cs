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
            ViewState["AutoNum"] = value;
        }
    }

    public int CheckPackAutonum
    {
        get
        {
            if (ViewState["CheckPackAutonum"] == null)
            {
                return 0;
            }
            return (int)ViewState["CheckPackAutonum"];
        }
        set
        {
            ViewState["CheckPackAutonum"] = value;
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
            LoadPack(CodabarBLL.ParsePackAutoNum(code));
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

    void LoadPack(int autonum)
    {
        if (PackBLL.Get4Production_Extract(autonum) != null)
        {
            Autonum = autonum;
            DetailsViewPack.DataBind();
            GridViewExtract.DataBind();
        }
        else if (PackBLL.IsExtracted(autonum) != null)
        {
            CheckPackAutonum = autonum;

            if (Autonum == 0)
            {
                //btnLoad_Click(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadConfirm", "doLoadPackCombined();", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông báo", "alert ('Dữ liệu không hợp lệ.');", true);
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
