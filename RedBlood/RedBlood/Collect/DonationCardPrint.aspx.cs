using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Collect_DonationCardPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PrintSettingBLL.Reload();

        int campID = Request["CampaignID"].ToInt();
        string rptType = Request["RptType"];

        if (campID == 0
            || string.IsNullOrEmpty(rptType)) return;

        ReportType type = (ReportType)rptType.ToInt();

        List<Donation> pL = DonationBLL.Get(campID, type);

        foreach (Donation item in pL)
        {
            Panel p = new Panel();
            p.Style.Add("position", "relative");
            p.Style.Add("page-break-after", "always");
            p.Style.Apply(PrintSettingBLL.Card.PaperSize);
            p.Style.Add("border", "1px solid white");
            divCon.Controls.Add(p);

            AddDINLabelControl(item, p);
        }
    }

    void AddDINLabelControl(Donation item, Panel panel)
    {
        DonationCardUserControl uc = new DonationCardUserControl();
        uc = (DonationCardUserControl)LoadControl("~/Collect/DonationCardUserControl.ascx");
        uc.Fill_Letter(item);

        panel.Controls.Add(uc);
    }
}
