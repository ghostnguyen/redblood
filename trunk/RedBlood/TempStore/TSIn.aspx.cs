using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TempStore_TSIn : System.Web.UI.Page
{
    CampaignBLL bll = new CampaignBLL();
    PackBLL packBLL = new PackBLL();

    public int CampaignID
    {
        get
        {
            if (ViewState["CampaignID"] == null)
                return 0;
            return (int)ViewState["CampaignID"];
        }
        set
        {
            //Clear();

            ViewState["CampaignID"] = value;
            if (value == 0)
            { }
            else
            {
                //LoadCampaign();
                GridView1.DataBind();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbSource_Click(object sender, BulletedListEventArgs e)
    {
        //foreach (ListItem item in lbSource.Items)
        //{
        //    item.Selected = false;
        //}

        //lbSource.Items[e.Index].Selected = true;

        //LinqDataSourceCampaign.DataBind();
        //ListView1.DataBind();
        //ucCampaign1.CampaignID = 0;
    }

    protected void ListViewLongRunCampaign_SelectedIndexChanged(object sender, EventArgs e)
    {
        CampaignID = (int)ListViewLongRunCampaign.SelectedDataKey.Value;
    }

    protected void LinqDataSourceLongRunCampaign_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = bll.GetTSIn(true);
    }

    protected void ListViewShortRunCampaign_SelectedIndexChanged(object sender, EventArgs e)
    {
        CampaignID = (int)ListViewShortRunCampaign.SelectedDataKey.Value;
    }

    protected void LinqDataSourceShortRunCampaign_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = bll.GetTSIn(false);
    }

    protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        Campaign c = CampaignBLL.GetByID(CampaignID);
        if (c == null)
        {
            e.Cancel = true;
        }
        else
        {
            e.Result = CampaignBLL.GetByID(CampaignID).Packs;
        }
    }


}
