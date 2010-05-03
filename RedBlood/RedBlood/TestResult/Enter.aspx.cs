using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestResult_Enter : System.Web.UI.Page
{
    CampaignBLL campaignBLL = new CampaignBLL();
    PackBLL packBLL = new PackBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

            Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

            if (Master.TextBoxCode.Text.Length == 0) return;

            if (BarcodeBLL.IsValidCampaignCode(Master.TextBoxCode.Text))
            {
                CampaignEnter(Master.TextBoxCode.Text);
            }

            Master.TextBoxCode.Text = "";
        }
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

        //DeletePack1.CampaignID = BarcodeBLL.ParseCampaignID(code);
    }

    protected void LinqDataSourcePack_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignDetail1.CampaignID > 0)
        {
            var v = DonationBLL.GetUnLock(CampaignDetail1.CampaignID);
            e.Result = v;

            PanelAllNeg.Visible = v.Count > 0;
        }
        else { e.Cancel = true; }
    }

    protected void LinqDataSourcePackLock_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignDetail1.CampaignID > 0)
        {
            e.Result = CampaignBLL.Get(CampaignDetail1.CampaignID).CollectedDonations
                .ToList()
                .Where(r => r.IsTRLocked);
        }
        else { e.Cancel = true; }
    }

    protected void LinqDataSourceUnCollect_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignDetail1.CampaignID > 0)
        {
            e.Result = CampaignBLL.Get(CampaignDetail1.CampaignID).Donations.Where(r => r.OrgPackID == null);
        }
        else { e.Cancel = true; }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string DIN = (string)e.Keys[0];

        DonationBLL.Update(DIN, e.NewValues["Markers.HIV"].ToString(),
           e.NewValues["Markers.HCV_Ab"].ToString(),
           e.NewValues["Markers.HBs_Ag"].ToString(),
            e.NewValues["Markers.Syphilis"].ToString(),
           e.NewValues["Markers.Malaria"].ToString(),
            "");

        // It will be null if the groupbloodis NOT enter when collect blood.
        if (e.NewValues["BloodGroup"] != null)
        {
            DonationBLL.Update(DIN, e.NewValues["BloodGroup"].ToString(), "");
        }

        e.Cancel = true;
        GridView1.EditIndex = -1;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SetNegative")
        {
            DonationBLL.UpdateNegative(e.CommandArgument.ToString());
            GridView1.DataBind();
        }
    }
    protected void btnAllNegative_Click(object sender, EventArgs e)
    {
        foreach (var item in DonationBLL.GetUnLock(CampaignDetail1.CampaignID))
        {
            DonationBLL.UpdateNegative(item.DIN);
        }
        GridView1.DataBind();
    }
}
