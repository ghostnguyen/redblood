using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Collect_DINCertPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PrintSettingBLL.Reload();

        int campID = Request["CampaignID"].ToInt();
        string rptType = Request["RptType"];

        if (campID == 0
            || string.IsNullOrEmpty(rptType)) return;

        ReportType type = (ReportType)rptType.ToInt();

        List<Donation> pl = DonationBLL.Get(campID, type);

        foreach (Donation item in pl)
        {
            Panel p = new Panel();
            p.Style.Add("position", "relative");
            p.Style.Add("page-break-after", "always");
            p.Style.Apply(PrintSettingBLL.DINCert.PaperSize);
            p.Style.Add("border", "1px solid white");
            divCon.Controls.Add(p);

            AddControl(item, p);
        }
    }

    void AddControl(Donation item, Panel panel)
    {
        DINCertUserControl uc = new DINCertUserControl();
        uc = (DINCertUserControl)LoadControl("~/Collect/DINCertUserControl.ascx");
        uc.Fill_Letter(item);

        panel.Controls.Add(uc);
    }
}
