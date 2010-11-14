using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;

public partial class Collect_CollectDetailRpt2 : System.Web.UI.Page
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

                LabelTitle1.Text = "Danh sách hiến máu";

                GridView1.DataBind();
            }
        }
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        List<Donation> list = DonationBLL.Get(CampaignDetail1.CampaignID).ToList();
        e.Result = list.Select(r => new
        {
            DIN = r.DIN,
            ProductCode = r.Pack == null ? "" : r.Pack.ProductCode,
            CMND = r.People.CMND,
            Name = r.People.Name,
            DOB = r.People.DOBToString,
            BloodGroupDesc = r.BloodGroupDesc,
            OrgVolume = r.OrgVolume,
            ResidentAddress = r.People.ResidentAddress,
            ResidentGeo3Name = r.People.ResidentGeo3 == null ? "" : r.People.ResidentGeo3.Name,
            ResidentGeo2Name = r.People.ResidentGeo2 == null ? "" : r.People.ResidentGeo2.Name,
            ResidentGeo1Name = r.People.ResidentGeo1 == null ? "" : r.People.ResidentGeo1.Name,
            Collector = r.Collector,
            Note = r.Note
        }).ToList();

        Summary(list.ToList());
    }

    private void Summary(List<Donation> list)
    {
        var v = list.GroupBy(r => r.OrgVolume)
            .Select(g => new { Vol = g.Key, Count = g.Count(), DINList = g.Select(r1 => r1.DIN) })
            .OrderBy(r => r.Count);

        int sum1 = 0;
        int sum2 = 0;
        string deniedDINStr = "";
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
                deniedDINStr = string.Join(" - ", item.DINList.ToArray());
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
            Literal1.Text += "<br /> " + deniedDINStr;
        }

        var BloodGroupList = list.GroupBy(r => r.BloodGroupDesc)
           .Select(g => new { BloodGroupDesc = g.Key, Count = g.Count() })
           .Where(r => !string.IsNullOrEmpty(r.BloodGroupDesc))
           .OrderBy(r => r.BloodGroupDesc);

        Literal1.Text += "<br />-------------";
        foreach (var item in BloodGroupList)
        {
            Literal1.Text += "<br />" + item.BloodGroupDesc + " : " + item.Count.ToString();
        }
    }

    protected void LinqDataSource1_Selected(object sender, LinqDataSourceStatusEventArgs e)
    {
        //if (e.Result != null && e.Result is List<Donation>)
        //    LableCount.Text = "" + ((List<Donation>)e.Result).Count.ToString();
    }
}
