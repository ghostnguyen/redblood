using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Report_ThankLetter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int campID = Request["CampaignID"].ToInt();
        string rptType = Request["RptType"];

        if (campID == 0
            || string.IsNullOrEmpty(rptType)) return;

        ReportType type = (ReportType)rptType.ToInt();

        List<Pack> p = PackBLL.Get4Rpt(campID, type);

        foreach (Pack item in p)
        {
            UserControl_ThanksLetter uc = new UserControl_ThanksLetter();
            uc = (UserControl_ThanksLetter)LoadControl("~/UserControl/ThanksLetter.ascx");
            uc.Fill_Letter(item.TestResult2);

            divCon.Controls.Add(uc);

            HtmlGenericControl gen = new HtmlGenericControl();
            gen.TagName = "div";
            gen.Attributes.Add("style", "page-break-after:always;");
            divCon.Controls.Add(gen);
        }
    }
}
