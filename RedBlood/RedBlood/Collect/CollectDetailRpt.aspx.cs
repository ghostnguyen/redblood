using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_CollectDetailRpt : System.Web.UI.Page
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
                Camp = CampaignBLL.GetByID(strCamID.ToInt());

                try
                {
                    CoopOrgGeo1ID = Camp.CoopOrg.Geo1.ID;
                }
                catch (Exception)
                {
                }

                CampaignDetail1.CampaignID = Camp.ID;

                LabelTitle1.Text = "Danh sách thu máu";

                GridView1.DataBind();
            }
        }
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        if (CampaignDetail1.CampaignID == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {
            IQueryable<Donation> list = DonationBLL.Get(CampaignDetail1.CampaignID);
            e.Result = list;

            if (e.Result == null)
            {
                e.Cancel = true;
                return;
            }

            Summary(list.ToList());
        }

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
