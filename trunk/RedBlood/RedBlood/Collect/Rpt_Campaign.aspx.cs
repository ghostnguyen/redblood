using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_Rpt_Campaign : System.Web.UI.Page
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
            e.Result = DonationBLL.Get(CampaignDetail1.CampaignID);
            if (e.Result == null)
                e.Cancel = true;
        }

    }
    protected void LinqDataSource1_Selected(object sender, LinqDataSourceStatusEventArgs e)
    {
        LableCount.Text = "Tổng cộng: " + e.TotalRowCount.ToString();
    }
}
