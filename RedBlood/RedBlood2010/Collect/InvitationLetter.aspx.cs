﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Collect_InvitationLetter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int campID = Request["CampaignID"].ToInt();
        string rptType = Request["RptType"];

        if (campID == 0
            || string.IsNullOrEmpty(rptType)) return;

        ReportType type = (ReportType)rptType.ToInt();

        List<Donation> p = DonationBLL.Get(campID, type);

        foreach (Donation item in p) 
        {
            InvitationLetterUserControl uc = new InvitationLetterUserControl();
            uc = (InvitationLetterUserControl)LoadControl("~/Collect/InvitationLetterUserControl.ascx");
            uc.Fill_Letter(item);

            divCon.Controls.Add(uc);

            HtmlGenericControl gen = new HtmlGenericControl();
            gen.TagName = "div";
            gen.Attributes.Add("style", "page-break-after:always;");
            divCon.Controls.Add(gen);
        }
    }
}
