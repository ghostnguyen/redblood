using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RedBlood.BLL;

namespace RedBlood.Collect
{
    public partial class Rpt2OrgMenu : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int campaignID = Request.Params["key"].ToInt();

                if (campaignID != 0)
                {
                    CampaignEnter(campaignID);
                }
            }
            else
            {

                string code = Master.TextBoxCode.Text.Trim();
                Master.TextBoxCode.Text = "";

                if (code.Length == 0) return;

                if (BarcodeBLL.IsValidCampaignCode(code))
                {
                    CampaignEnter(BarcodeBLL.ParseCampaignID(code));
                }
            }
        }

        private void CampaignEnter(int campaignID)
        {
            CampaignDetail1.CampaignID = campaignID;

            GridView1.DataBind();

            if (CampaignDetail1.CampaignID != 0)
            {
                HyperLinkNeg.NavigateUrl = "~/Collect/Rpt2OrgTemplate.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
                HyperLinkPos.NavigateUrl = "~/Collect/Rpt2OrgTemplate.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
                HyperLinkHIV.NavigateUrl = "~/Collect/Rpt2OrgTemplate.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

                HyperLinkNegThankLetter.NavigateUrl = "~/Collect/ThankLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
                HyperLinkPosThankLetter.NavigateUrl = "~/Collect/ThankLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
                HyperLinkHIVInvitationLetter.NavigateUrl = "~/Collect/InvitationLetter.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();

                HyperLinkNegEnvolope.NavigateUrl = "~/Collect/EnvelopePrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.NegInCam).ToString();
                HyperLinkPosEnvelope.NavigateUrl = "~/Collect/EnvelopePrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.FourPosInCam).ToString();
                HyperLinkHIVEnvelope.NavigateUrl = "~/Collect/EnvelopePrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.HIVInCam).ToString();


                HyperLinkAllCard.NavigateUrl = "~/Collect/DonationCardPrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.All).ToString();
                HyperLinkAllDINCert.NavigateUrl = "~/Collect/DINCertPrint.aspx?CampaignID=" + CampaignDetail1.CampaignID.ToString() + "&rptType=" + ((int)ReportType.All).ToString();
            }


        }
        protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            e.Result = db.Donations.Where(r => r.CampaignID == CampaignDetail1.CampaignID).OrderBy(r => r.DIN).ToList()
                .Select(r => new
                {
                    r.DIN,
                    Name = r.People.Name,
                    CollectedDate = r.CollectedDate.ToStringVN_Hour(),
                    ProductDescription = r.Pack != null ? ProductBLL.GetDesc(r.Pack.ProductCode) : "",
                    r.BloodGroupDesc,
                    Markers_HIV = r.Markers.HIV,
                    Markers_HCV_Ab = r.Markers.HCV_Ab,
                    Markers_HBs_Ag = r.Markers.HBs_Ag,
                    Markers_Malaria = r.Markers.Malaria,
                    Markers_Syphilis = r.Markers.Syphilis,
                })
                ;
        }

        protected void btnSelectedCard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Collect/DonationCardPrint.aspx?DINList=" + GetSelectedDIN());
        }

        protected void btnSelectedDINCert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Collect/DINCertPrint.aspx?DINList=" + GetSelectedDIN());
        }

        string GetSelectedDIN()
        {
            string selectedDIN = "";
            foreach (GridViewRow item in GridView1.Rows)
            {
                CheckBox chk = item.Cells[10].Controls[1] as CheckBox;

                if (chk != null && chk.Checked)
                {
                    selectedDIN += item.Cells[1].Text + ",";
                }
            }
            return selectedDIN;
        }
    }
}