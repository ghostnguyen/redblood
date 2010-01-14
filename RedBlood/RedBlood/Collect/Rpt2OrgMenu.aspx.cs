using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_Rpt2OrgMenu : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //int campaignID = Request.Params["key"].ToInt();

            //if (campaignID != 0)
            //{
            //    CampaignEnter(campaignID);
            //}

            //Master.TextBoxCode.Text;


        }

        string code = Master.TextBoxCode.Text.Trim();
        Master.TextBoxCode.Text = "";

        if (code.Length == 0) return;

        if (BarcodeBLL.IsValidCampaignCode(code))
        {
            CampaignEnter(BarcodeBLL.ParseCampaignID(code));
        }
    }

    private void CampaignEnter(int campaignID)
    {
        CampaignDetail1.CampaignID = campaignID;

        GridView1.DataBind();

        if (CampaignDetail1.CampaignID != 0)
        {
            HyperLinkNeg.NavigateUrl = "~/Collect/Rpt2OrgTemplate.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPos.NavigateUrl = "~/Collect/Rpt2OrgTemplate.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            HyperLinkHIV.NavigateUrl = "~/Collect/Rpt2OrgTemplate.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkNegThankLetter.NavigateUrl = "~/Collect/ThankLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPosThankLetter.NavigateUrl = "~/Collect/ThankLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            HyperLinkHIVInvitationLetter.NavigateUrl = "~/Collect/InvitationLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkNegEnvolope.NavigateUrl = "~/Collect/EnvelopePrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPosEnvelope.NavigateUrl = "~/Collect/EnvelopePrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            HyperLinkHIVEnvelope.NavigateUrl = "~/Collect/EnvelopePrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkAllCard.NavigateUrl = "~/Collect/DonationCardPrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.All).ToString();
            //HyperLinkPosCard.NavigateUrl = "~/Collect/DonationCardPrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            //HyperLinkHIVCard.NavigateUrl = "~/Collect/DonationCardPrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkAllDINCert.NavigateUrl = "~/Collect/DINCertPrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.All).ToString();
            //HyperLinkPosDINCert.NavigateUrl = "~/Collect/DINCertPrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            //HyperLinkHIV_DINCert.NavigateUrl = "~/Collect/DINCertPrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();


        }
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = db.Donations.Where(r => r.CampaignID == CampaignDetail1.CampaignID).OrderBy(r => r.DIN).ToList();
    }
}
