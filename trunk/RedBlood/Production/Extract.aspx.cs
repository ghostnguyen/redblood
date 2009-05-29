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
            LoadPack();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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
        Pack p = PackBLL.Get(Autonum,PackBLL.StatusList4Extract());

        if (p.ComponentID != (int)TestDef.Component.Full) return;
        
        if (p == null) e.Cancel = true;
        e.Result = p;
    }

    void LoadPack()
    {
        DetailsViewPack.DataBind();

        Pack p = PackBLL.Get(Autonum);
        if (p == null) return;

        //if (p.PeopleID == null && p.CampaignID == null)
    }

    protected void btnProduct_Click(object sender, EventArgs e)
    {

    }
}
