using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CampaignPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (BarcodeBLL.IsValidPackCode(code))
        {
            
        }
        else if (BarcodeBLL.IsValidTestResultCode(code))
        {
            
        }
        else if (BarcodeBLL.IsValidCampaignCode(code))
        {
            ucCampaign1.CampaignID = BarcodeBLL.ParseCampaignID(code);
        }
        else
        {
            
        }

    }
    protected void lbSource_Click(object sender, BulletedListEventArgs e)
    {
        foreach (ListItem item in lbSource.Items)
        {
            item.Selected = false;            
        }

        lbSource.Items[e.Index].Selected = true;

        LinqDataSourceCampaign.DataBind();
        ListView1.DataBind();
        ucCampaign1.CampaignID = 0;
    }
    
    protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucCampaign1.CampaignID = (int)ListView1.SelectedDataKey.Value;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        ucCampaign1.New();
    }
}
