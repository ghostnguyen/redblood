using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class UserControl_CampaignListByProvince : System.Web.UI.UserControl
{
    public Guid ProvinceID
    {
        get
        {
            if (ViewState["ProvinceID"] == null)
                return new Guid();
            else
                return (Guid)ViewState["ProvinceID"];
        }
        set
        {
            ViewState["ProvinceID"] = value;
        }
    }

    public DateTime? From
    {
        get
        {
            return (DateTime?)ViewState["From"];
        }
        set
        {
            ViewState["From"] = value;
        }
    }

    public DateTime? To
    {
        get
        {
            return (DateTime?)ViewState["To"];
        }
        set
        {
            ViewState["To"] = value;
        }
    }

    //public List<int> SourceList
    //{
    //    get
    //    {
    //        if (Session["SourceList"] == null)
    //        {
    //            Session["SourceList"] = new List<int>();
    //        }
    //        return (List<int>)Session["SourceList"];
    //    }
    //    set
    //    {
    //        Session["SourceList"] = value;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {

        var v = CampaignBLL.Get(new List<Guid>() { ProvinceID }, From, To, Campaign.TypeX.Short_run);
        
        e.Result = v.ToList().Select(r => new
        {
            r.ID,
            r.Name,
            Date = r.Date.HasValue ? r.Date.ToStringVN() : "",
            SourceName = r.Source != null ? r.Source.Name : "",
            CoopOrg = r.CoopOrg != null ? r.CoopOrg.Name : "",
            HostOrg = r.HostOrg != null ? r.HostOrg.Name : "",
            PacksCount = r.Donations.Count.ToString(),
            r.Est,
            CountPack350 = r.Donations.Where(r1 => r1.OrgVolume == "350").Count(),
            CountPack450 = r.Donations.Where(r1 => r1.OrgVolume == "450").Count(),
            CountPack250 = r.Donations.Where(r1 => r1.OrgVolume == "250").Count(),
            r.Note,
        });

    }
}
