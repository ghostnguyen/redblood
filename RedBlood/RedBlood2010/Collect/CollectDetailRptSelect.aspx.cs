using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
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
        string url2 = RedBloodSystem.Url4CollectDetailRpt2;

        var v = from r in db.Donations
                where ucDateRange.FromDate <= r.CollectedDate.Value.Date
                && r.CollectedDate.Value.Date <= ucDateRange.ToDate
                select new
                {
                    ID = r.Campaign.ID,
                    Name = r.Campaign.Name,
                    Link = url + "CampaignID=" + r.CampaignID.ToString(),
                    Name2 = "Báo cáo có tên KTV",
                    Link2 = url2 + "CampaignID=" + r.CampaignID.ToString()

                }
                ;

        ListView1.DataSource = v.Distinct();
        ListView1.DataBind();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        ListCampaign();
    }
}
