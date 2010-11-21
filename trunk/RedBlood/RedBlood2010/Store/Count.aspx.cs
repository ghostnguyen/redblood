using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;

namespace RedBlood.Store
{
    public partial class Count : System.Web.UI.Page
    {
        public int ExpiredInDays { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExpiredInDays = 3;
                txtDays.Text = ExpiredInDays.ToString();
                ucInDays.Date = DateTime.Now.Date.AddDays(ExpiredInDays);
            }
        }

        protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            e.Result = GetData(true);
        }

        protected void LinqDataSource2_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            e.Result = GetData(false);
        }

        protected void LinqDataSource3_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            e.Result = GetData(null);
        }

        public object GetData(bool? IsNeg)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            var v = db.vw_ProductCounts.Where(r => r.Status == Pack.StatusX.Product)
                .ToList()
                .Where(r => !IsNeg.HasValue || (IsNeg.Value ? r.TestResultStatus == Donation.TestResultStatusX.Negative 
                    : r.TestResultStatus != Donation.TestResultStatusX.Negative))
                .GroupBy(r => new { r.ProductCode, r.ProductDesc, r.Status }, (r, sub) => new
                {
                    r.ProductCode,
                    r.ProductDesc,
                    r.Status,
                    Total = sub.Sum(r1 => r1.Count),
                    TotalExpired = sub.Where(r1 => r1.ExpirationDate.Value.Expired())
                                        .Sum(r1 => r1.Count).ToStringRemoveZero(),
                    TotalExpiredInDays = sub.Where(r1 => r1.ExpirationDate.Value.ExpiredInDays(ExpiredInDays))
                                        .Sum(r1 => r1.Count).ToStringRemoveZero(),
                    TotalTRNA = sub.Where(r1 => r1.TestResultStatus == Donation.TestResultStatusX.Non)
                                    .Sum(r1 => r1.Count).ToStringRemoveZero(),
                    TotalTRNeg = sub.Where(r1 => r1.TestResultStatus == Donation.TestResultStatusX.Negative)
                                    .Sum(r1 => r1.Count).ToStringRemoveZero(),
                    TotalTRPos = sub.Where(r1 => r1.TestResultStatus == Donation.TestResultStatusX.Positive)
                                    .Sum(r1 => r1.Count).ToStringRemoveZero(),
                    BloodGroupSumary = sub.GroupBy(r1 => BloodGroupBLL.GetLetter(r1.BloodGroup), (r1, BGSub) => new
                    {
                        //BloodGroupDesc = BloodGroupBLL.GetDescription(r1),
                        BloodGroupDesc = r1,
                        Total = BGSub.Sum(r3 => r3.Count),
                        VolumeSumary = BGSub.GroupBy(r2 => r2.Volume, (r2, VolSub) => new
                        {
                            Volume = r2.HasValue ? r2.Value.ToString() : "_",
                            Total = VolSub.Sum(r3 => r3.Count)
                        }).OrderBy(r2 => r2.Volume)
                    }).OrderBy(r1 => r1.BloodGroupDesc),
                    VolumeSumary = sub.GroupBy(r1 => r1.Volume, (r1, VolSub) => new
                    {
                        Volume = r1.HasValue ? r1.Value.ToString() : "_",
                        Total = VolSub.Sum(r3 => r3.Count)
                    }).OrderBy(r1 => r1.Volume)
                })
                .OrderBy(r => r.ProductDesc);

            return v;
        }

        protected void btnOk1_Click(object sender, EventArgs e)
        {
            ExpiredInDays = txtDays.Text.ToInt();
            ucInDays.Date = DateTime.Now.Date.AddDays(ExpiredInDays);
            GridView1.DataBind();
        }

        protected void btnOk2_Click(object sender, EventArgs e)
        {
            ExpiredInDays = (ucInDays.Date.Value - DateTime.Now.Date).Days;
            txtDays.Text = ExpiredInDays.ToString();
            GridView1.DataBind();
        }
    }


}