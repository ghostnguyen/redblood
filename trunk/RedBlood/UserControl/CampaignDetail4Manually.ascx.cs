using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_CampaignDetail4Manually : System.Web.UI.UserControl
{
    GeoBLL geoBLL = new GeoBLL();
    CodabarBLL codabarBLL = new CodabarBLL();
    CampaignBLL bll = new CampaignBLL();

    public int CampaignID
    {
        get
        {
            if (ViewState["CampaignID"] == null)
                return 0;
            return (int)ViewState["CampaignID"];
        }
        set
        {
            Clear();

            ViewState["CampaignID"] = value;
            if (value == 0)
            { }
            else
            {
                LoadCampaign();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadCampaign()
    {
        Campaign e = CampaignBLL.GetByID(CampaignID);

        if (e == null)
        {
        }
        else
        {
            ImageCodabar.ImageUrl = "../Codabar/Image.aspx?hasText=true&code="
                + codabarBLL.GenStringCode(Resources.Codabar.campaignSSC, e.ID.ToString());
            lblName.Text = e.Name;

            if (e.Date != null)
            {
                lblDate.Text = e.Date.ToStringVN_Hour();
            }

            if (e.Source != null)
            {
                lblSrc.Text = "Nguồn: " + e.Source.Name;
            }

            if (e.Est != null)
            {
                lblEst.Text = "SL: " + e.Est.Value.ToString();
            }

            if (e.CoopOrgID != null)
            {
                lblCoopOrg.Text = "Tổ chức: " + e.CoopOrg.Name;
            }

            if (e.HostOrgID != null)
            {
                lblHostOrg.Text = "Địa điểm: " + e.HostOrg.Name;
            }

            lblNote.Text = e.Note;            
        }
    }

    public void Clear()
    {
        ViewState["CampaignID"] = 0;
        ImageCodabar.ImageUrl = "none";
        lblName.Text = "";
        lblEst.Text = "";
        lblDate.Text = "";
        lblSrc.Text = "";

        lblCoopOrg.Text = "";
        lblHostOrg.Text = "";

        lblNote.Text = "";
    }
}
