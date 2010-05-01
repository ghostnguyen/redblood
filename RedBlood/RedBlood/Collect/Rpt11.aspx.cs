using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_Rpt11 : System.Web.UI.Page
{
    public Guid ProvinceID
    {
        get
        {
            if (ViewState["ProvinceID"] == null)
            {
                ViewState["ProvinceID"] = Guid.Empty;
            }
            return (Guid)ViewState["ProvinceID"];
        }
        set
        {
            ViewState["ProvinceID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["ProvinceID"]))
                ProvinceID = Request["ProvinceID"].ToGuid();

            if (!string.IsNullOrEmpty(Request["From"]))
                ucDateRange.FromDate = Request["From"].ToString().ToShortDate();

            if (!string.IsNullOrEmpty(Request["To"]))
                ucDateRange.ToDate = Request["To"].ToString().ToShortDate();

            Geo g = GeoBLL.Get(ProvinceID, 1);

            lblProvince.Text = g.Fullname;

            GridView1.DataBind();
        }
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        ucDateRange.Validated();

        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.Campaigns.Where(r => r.Type == Campaign.TypeX.Short_run && ucDateRange.FromDate <= r.Date
                                    && r.Date <= ucDateRange.ToDate
                                    && r.CoopOrg.GeoID1 == ProvinceID)
                                .ToList()
                                .Select(r => new
                                                {
                                                    CoopOrg = r.CoopOrg.Name,
                                                    HostOrg = r.HostOrg.Name,
                                                    r.Date,
                                                    Total = r.Donations.Where(r2 => r2.Pack != null).Count(),
                                                    Total450 = r.Donations.Where(r2 => r2.Pack != null && r2.Pack.Volume == 450).Count().ToStringRemoveZero(),
                                                    Total350 = r.Donations.Where(r2 => r2.Pack != null && r2.Pack.Volume == 350).Count().ToStringRemoveZero(),
                                                    Total250 = r.Donations.Where(r2 => r2.Pack != null && r2.Pack.Volume == 250).Count().ToStringRemoveZero(),
                                                    TotalXXX = r.Donations.Where(r2 => r2.Pack != null && r2.Pack.Volume != 250 && r2.Pack.Volume != 350 && r2.Pack.Volume != 450).Count().ToStringRemoveZero(),
                                                    TotalPos = r.Donations.Where(r2 => r2.Pack != null && r2.TestResultStatus == Donation.TestResultStatusX.Positive).Count().ToStringRemoveZero(),
                                                    TotalNeg = r.Donations.Where(r2 => r2.Pack != null && r2.TestResultStatus == Donation.TestResultStatusX.Negative).Count().ToStringRemoveZero(),
                                                    TotalNon = r.Donations.Where(r2 => r2.Pack != null && r2.TestResultStatus == Donation.TestResultStatusX.Non).Count().ToStringRemoveZero(),
                                                    TotalMiss = r.Donations.Where(r2 => r2.Pack == null).Count().ToStringRemoveZero()
                                                })
            .OrderBy(r => r.Date);
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
}


