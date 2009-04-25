using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_CampaignRptSelect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TextBoxCode.Text = Master.TextBoxCode.Text.Trim();

        if (Master.TextBoxCode.Text.Length == 0) return;

        if (CodabarBLL.IsValidPackCode(Master.TextBoxCode.Text))
        {
            //PackCodeEnter(Master.TextBoxCode.Text);
        }
        else if (CodabarBLL.IsValidCampaignCode(Master.TextBoxCode.Text))
        {
            CampaignEnter(Master.TextBoxCode.Text);
        }
        else
        {
            //ucPeople.Code = Master.TextBoxCode.Text;
        }

        Master.TextBoxCode.Text = "";
    }

    private void CampaignEnter(string code)
    {
        CampaignDetail1.CampaignID = CodabarBLL.ParseCampaignID(code);

        if (CampaignDetail1.CampaignID != 0)
        {
            HyperLinkNeg.NavigateUrl = "../Report/CampaignRpt.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPos.NavigateUrl = "../Report/CampaignRpt.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();            
            HyperLinkHIV.NavigateUrl = "../Report/CampaignRpt.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkNegThankLetter.NavigateUrl = "../Report/ThankLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPosThankLetter.NavigateUrl = "../Report/ThankLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();

            HyperLinkNegEnvolope.NavigateUrl = "../Report/Envelope.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPosEnvelope.NavigateUrl = "../Report/Envelope.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            HyperLinkHIVEnvelope.NavigateUrl = "../Report/Envelope.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkHIVInvitationLetter.NavigateUrl = "../Report/InvitationLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();
        }
    }
}
