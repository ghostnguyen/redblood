using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestResult_BloodGroup : System.Web.UI.Page
{
    CampaignBLL campaignBLL = new CampaignBLL();
    PackBLL packBLL = new PackBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

        if (Master.TextBoxCode.Text.Length == 0) return;

        if (BarcodeBLL.IsValidCampaignCode(Master.TextBoxCode.Text))
        {
            CampaignEnter(Master.TextBoxCode.Text);
        }

        Master.TextBoxCode.Text = "";
    }

    void DeletePack1_PackDeleted(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }

    private void CampaignEnter(string code)
    {
        CampaignDetail1.CampaignID = BarcodeBLL.ParseCampaignID(code);
        GridView1.DataBind();
        GridViewLock.DataBind();
        GridViewUnCollect.DataBind();
    }

    protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignDetail1.CampaignID == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {
            e.Result = DonationBLL.Get(CampaignDetail1.CampaignID)
                .ToList()
                .Where(r => r.OrgPackID != null
                && !r.IsTRLocked
                );
        }
    }

    protected void LinqDataSourcePackLock_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignDetail1.CampaignID == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {
            e.Result = DonationBLL.Get(CampaignDetail1.CampaignID)
                .ToList()
                .Where(r => r.OrgPackID != null && r.IsTRLocked);
        }
    }

    protected void LinqDataSourceUnCollect_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignDetail1.CampaignID == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {
            e.Result = DonationBLL.Get(CampaignDetail1.CampaignID).Where(r => r.OrgPackID == null);
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string DIN = (string)e.Keys[0];

        // It will be null if the groupbloodis NOT enter when collect blood.
        if (e.NewValues["BloodGroup"] != null)
        {
            DonationBLL.Update(DIN, e.NewValues["BloodGroup"].ToString(), "");
        }

        e.Cancel = true;
        GridView1.EditIndex = -1;
    }
}
