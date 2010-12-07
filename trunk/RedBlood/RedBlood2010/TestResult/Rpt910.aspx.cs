using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood.BLL;
namespace RedBlood.TestResult
{
    public partial class TestResult_Rpt910 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ucDateRange.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                ucDateRange.ToDate = DateTime.Now.Date;
            }

        }
        protected void LinqDataSourceRpt_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            ucDateRange.Validated();

            RedBloodDataContext db = new RedBloodDataContext();

            e.Result = db.Campaigns.Where(r => ucDateRange.FromDate <= r.Date && r.Date <= ucDateRange.ToDate)
                .ToList()
                .Select(r => new
                {
                    r.ID,
                    Url = RedBloodSystem.Url4CollectRpt920
                       + "CampaignID=" + r.ID.ToString(),
                    r.Name,
                    r.Date,
                    Total = r.CollectedDonations.Count(),
                    HostName = r.HostOrg.Name,
                    CoopName = r.CoopOrg.Name,
                    TestResultPos = RedBloodSystem.CheckingInfection.Select(r1 => new
                    {
                        r1.Name,
                        Total = r.Donations.Where(r2 => r2.OrgPackID.HasValue && r1.Decode(r2.InfectiousMarkers) == TR.pos.Name).Count()
                    }).Where(r1 => r1.Total > 0),
                    TestResultNA = RedBloodSystem.CheckingInfection.Select(r1 => new
                    {
                        r1.Name,
                        Total = r.Donations.Where(r2 => r2.OrgPackID.HasValue && r1.Decode(r2.InfectiousMarkers) == TR.na.Name).Count()
                    }).Where(r1 => r1.Total > 0),
                    BloodGroupSumary = r.Donations.Where(r2 => r2.OrgPackID.HasValue).GroupBy(r1 => r1.BloodGroup, (r2, BGSub) => new
                    {
                        BloodGroupDesc = BloodGroupBLL.GetDescription(r2),
                        Total = BGSub.Count()
                    }).OrderBy(r1 => r1.BloodGroupDesc)
                });
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            GridViewRpt.DataBind();
        }

    }
}