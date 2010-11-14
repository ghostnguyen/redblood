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
    public partial class Rpt_OrderByDay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDateFrom.Text = DateTime.Now.Date.ToStringVN();
                txtHourFrom.Text = "00:01";

                txtDateTo.Text = DateTime.Now.Date.ToStringVN();
                txtHourTo.Text = "23:59";
                LoadData();
            }
        }

        protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            RedBloodDataContext db = new RedBloodDataContext();


        }

        protected void btnOk2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            DateTime? dtFrom = txtDateFrom.Text.ToDatetimeFromVNFormat();
            DateTime? dtTo = txtDateTo.Text.ToDatetimeFromVNFormat();

            if (dtFrom.HasValue)
            {
                DateTime hourFrom;
                if (DateTime.TryParse(txtHourFrom.Text, out hourFrom))
                {
                    dtFrom = dtFrom.Value.AddHours(hourFrom.Hour).AddMinutes(hourFrom.Minute);
                }
            }

            if (dtTo.HasValue)
            {
                DateTime hourTo;
                if (DateTime.TryParse(txtHourTo.Text, out hourTo))
                {
                    dtTo = dtTo.Value.AddHours(hourTo.Hour).AddMinutes(hourTo.Minute);
                }
            }

            RedBloodDataContext db = new RedBloodDataContext();

            var v = db.Orders.Where(r => r.Date.Value >= dtFrom && r.Date.Value <= dtTo)
                //.OrderBy(r => r.Date)
                //.ToList()
                //.Select(r => new
                //{
                //    r.ID,
                //    r.Name,
                //    Date = r.Date.ToStringVN_Hour(),
                //    r.Actor,
                //    r.Type,
                //    For = r.Type == Order.TypeX.ForCR ? r.Org.Name : r.People.Name,
                //})
                .SelectMany(r => r.PackOrders.Select(r1 => r1.Pack))
                .ToList()
                .GroupBy(r => new { r.Product })
                .Select(r => new
                {
                    ProductCode = r.Key.Product.Code,
                    ProductDesc = r.Key.Product.Description,
                    Total = r.Count(),
                    BloodGroupSumary = r.GroupBy(r1 => r1.Donation.BloodGroup).Select(r1 => new
                    {
                        BloodGroupDesc = BloodGroupBLL.GetDescription(r1.Key),
                        Total = r1.Count(),
                        VolumeSumary = r1.GroupBy(r2 => r2.Volume).Select(r2 => new
                        {
                            Volume = r2.Key.HasValue ? r2.Key.Value.ToString() : "_",
                            Total = r2.Count()
                        }).OrderBy(r2 => r2.Volume)
                    }).OrderBy(r1 => r1.BloodGroupDesc),
                    //VolumeSumary = sub.GroupBy(r1 => r1.Volume, (r1, VolSub) => new
                    //{
                    //    Volume = r1.HasValue ? r1.Value.ToString() : "_",
                    //    Total = VolSub.Sum(r3 => r3.Count)
                    //}).OrderBy(r1 => r1.Volume)
                })
                .OrderBy(r => r.ProductDesc);

            GridView1.DataSource = v;
            GridView1.DataBind();
        }
    }


}