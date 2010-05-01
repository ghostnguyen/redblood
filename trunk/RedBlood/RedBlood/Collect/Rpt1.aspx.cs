using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_Rpt1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucDateRange.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ucDateRange.ToDate = DateTime.Now.Date;
        }

    }
    protected void LinqDataSourceStart_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        ucDateRange.Validated();

        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.Campaigns.Where(r => r.Type == Campaign.TypeX.Short_run && ucDateRange.FromDate <= r.Date
                                                && r.Date <= ucDateRange.ToDate)
            .ToList()
                                                .GroupBy(r => new { r.CoopOrg.Geo1 }, (r, sub) => new
            {
                Province = r.Geo1.Fullname,
                Url = RedBloodSystem.Url4CollectRpt11 
                    + "ProvinceID=" + r.Geo1.ID.ToString() 
                    + "&from=" + ucDateRange.FromDate.Value.Date.ToShortDateString()
                    + "&to=" + ucDateRange.ToDate.Value.Date.ToShortDateString(),
                Total = sub.Sum(r1 => r1.Donations.Where(r2 => r2.Pack != null).Count()),
                Total450 = sub.Sum(r1 => r1.Donations.Where(r2 => r2.Pack != null && r2.Pack.Volume == 450).Count()).ToStringRemoveZero(),
                Total350 = sub.Sum(r1 => r1.Donations.Where(r2 => r2.Pack != null && r2.Pack.Volume == 350).Count()).ToStringRemoveZero(),
                Total250 = sub.Sum(r1 => r1.Donations.Where(r2 => r2.Pack != null && r2.Pack.Volume == 250).Count()).ToStringRemoveZero(),
                TotalXXX = sub.Sum(r1 => r1.Donations.Where(r2 => r2.Pack != null && r2.Pack.Volume != 250 && r2.Pack.Volume != 350 && r2.Pack.Volume != 450).Count()).ToStringRemoveZero(),
                TotalPos = sub.Sum(r1 => r1.Donations.Where(r2 => r2.Pack != null && r2.TestResultStatus == Donation.TestResultStatusX.Positive).Count()).ToStringRemoveZero(),
                TotalNeg = sub.Sum(r1 => r1.Donations.Where(r2 => r2.Pack != null && r2.TestResultStatus == Donation.TestResultStatusX.Negative).Count()).ToStringRemoveZero(),
                TotalNon = sub.Sum(r1 => r1.Donations.Where(r2 => r2.Pack != null && r2.TestResultStatus == Donation.TestResultStatusX.Non).Count()).ToStringRemoveZero(),
                TotalMiss = sub.Sum(r1 => r1.Donations.Where(r2 => r2.Pack == null).Count()).ToStringRemoveZero()
            })
            .OrderBy(r => r.Province);
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        GridViewStart.DataBind();


    }


}
