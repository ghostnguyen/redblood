using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Find_CampaignDetail : System.Web.UI.Page
{
    public int CampaignID
    {
        get
        {
            if (ViewState["CampaignID"] == null) return 0;
            return (int)ViewState["CampaignID"];
        }
        set
        {
            ViewState["CampaignID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CampaignID = Request.Params["key"].ToInt();

        DetailView1.DataBind();
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        Campaign p = CampaignBLL.GetByID(CampaignID);
        if (p == null)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
            e.Result = p;
    }
}
