using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_CollectDetailRptSelect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucDateRange.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ucDateRange.ToDate = DateTime.Now.Date;
            ListCampaign();
        }
    }

    private void ListCampaign()
    {
        RedBloodDataContext db = new RedBloodDataContext();
        string url = RedBloodSystem.Url4CollectDetailRpt;

        var v = from r in db.Donations
                where ucDateRange.FromDate <= r.CollectedDate.Value.Date
                && r.CollectedDate.Value.Date <= ucDateRange.ToDate
                select new { Name = r.Campaign.Name, Link = url + "CampaignID=" + r.CampaignID.ToString() }
                ;

        ListView1.DataSource = v.Distinct();
        ListView1.DataBind();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        ListCampaign();
    }
}
