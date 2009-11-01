using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_SelectCampaignToRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.Date.ToStringVN();
            ListCampaign();
        }
    }

    private void ListCampaign()
    {
        RedBloodDataContext db = new RedBloodDataContext();
        DateTime? dt = txtDate.Text.ToDatetimeFromVNFormat();
        string url = RedBloodSystem.Url4Collect4Rpt_Campaign;

        var v = from r in db.Donations
                where r.CollectedDate.Value.Date == dt.Value.Date
                select new { Name = r.Campaign.Name, Link = url + "CampaignID=" + r.CampaignID.ToString() }
                ;

        ListView1.DataSource = v.Distinct();
        ListView1.DataBind();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        ListCampaign();
    }
}
