﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindAndReport_CampaignDetail : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int campaignID = Request.Params["key"].ToInt();

            if (campaignID != 0)
            {
                CampaignEnter(campaignID);
            }
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

            HyperLinkNegEnvolope.NavigateUrl = "../FindAndReport/Envelope.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
            HyperLinkPosEnvelope.NavigateUrl = "../FindAndReport/Envelope.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
            HyperLinkHIVEnvelope.NavigateUrl = "../FindAndReport/Envelope.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

            HyperLinkHIVInvitationLetter.NavigateUrl = "../FindAndReport/InvitationLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();
        }
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = db.Packs.Where(r => r.CampaignID == CampaignDetail1.CampaignID).OrderBy(r => r.Autonum).ToList();
    }
}