using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood.BLL;
namespace RedBlood.TestResult
{
    public partial class TestResult_Rpt920 : System.Web.UI.Page
    {
        public Campaign Camp { get; set; }
        public Guid CoopOrgGeo1ID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strCamID = Request["CampaignID"];

                if (!string.IsNullOrEmpty(strCamID))
                {
                    Camp = CampaignBLL.Get(strCamID.ToInt());

                    try
                    {
                        CoopOrgGeo1ID = Camp.CoopOrg.Geo1.ID;
                    }
                    catch (Exception)
                    {
                    }

                    CampaignDetail1.CampaignID = Camp.ID;

                    LabelTitle1.Text = "KẾT QUẢ XÉT NGHIỆM SÀNG LỌC";

                    GridView1.DataBind();
                }
            }
        }

        protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            List<Donation> list = CampaignBLL.Get(CampaignDetail1.CampaignID).CollectedDonations.ToList();

            e.Result = RedBloodSystem.CheckingInfection.Select(r1 => new
            {
                r1.Name,
                PosList = list.Where(r2 => r1.Decode(r2.InfectiousMarkers) == TR.pos.Name),
                NAList = list.Where(r2 => r1.Decode(r2.InfectiousMarkers) == TR.na.Name),
            })
            .Where(r => r.PosList.Count() > 0 || r.NAList.Count() > 0);
        }

        private void Summary(List<Donation> list)
        {
            var v = list.GroupBy(r => r.OrgVolume)
                .Select(g => new { Vol = g.Key, Count = g.Count() })
                .OrderBy(r => r.Count);

            int sum1 = 0;
            int sum2 = 0;

            if (v.Count() > 0)
                Literal1.Text += "<br /> Tổng cộng";

            foreach (var item in v)
            {
                if (!string.IsNullOrEmpty(item.Vol))
                {
                    Literal1.Text += "<br />" + item.Vol + "ml : " + item.Count.ToString();
                    sum1 += item.Count;
                }
                else
                {
                    sum2 += item.Count;
                }
            }

            if (sum1 != 0)
            {
                Literal1.Text += "<br />-------------";
                Literal1.Text += "<br /> TC: " + sum1.ToString();
            }

            if (sum2 != 0)
            {
                Literal1.Text += "<br />-------------";
                Literal1.Text += "<br /> Không thu: " + sum2.ToString();
            }
        }

        protected void LinqDataSource1_Selected(object sender, LinqDataSourceStatusEventArgs e)
        {
            //if (e.Result != null && e.Result is List<Donation>)
            //    LableCount.Text = "" + ((List<Donation>)e.Result).Count.ToString();
        }
    }

}