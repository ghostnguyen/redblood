using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_Rpt2Org : System.Web.UI.Page
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
            HyperLinkNeg.NavigateUrl = "../FindAndReport/CampaignRpt.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPos.NavigateUrl = "../FindAndReport/CampaignRpt.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            HyperLinkHIV.NavigateUrl = "../FindAndReport/CampaignRpt.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkNegThankLetter.NavigateUrl = "../FindAndReport/ThankLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPosThankLetter.NavigateUrl = "../FindAndReport/ThankLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            HyperLinkHIVInvitationLetter.NavigateUrl = "../FindAndReport/InvitationLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkNegEnvolope.NavigateUrl = "../FindAndReport/PrintEnvelope.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPosEnvelope.NavigateUrl = "../FindAndReport/PrintEnvelope.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            HyperLinkHIVEnvelope.NavigateUrl = "../FindAndReport/PrintEnvelope.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkNegCard.NavigateUrl = "../FindAndReport/PrintCard.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPosCard.NavigateUrl = "../FindAndReport/PrintCard.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            HyperLinkHIVCard.NavigateUrl = "../FindAndReport/PrintCard.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkDINCert.NavigateUrl = "../Collect/PrintDINCert.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPosDINCert.NavigateUrl = "../Collect/PrintDINCert.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            HyperLinkHIV_DINCert.NavigateUrl = "../Collect/PrintDINCert.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();


        }
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = db.Donations.Where(r => r.CampaignID == CampaignDetail1.CampaignID).OrderBy(r => r.DIN).ToList();
    }
}
