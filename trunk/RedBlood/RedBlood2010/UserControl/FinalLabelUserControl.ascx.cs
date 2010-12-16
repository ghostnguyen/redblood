using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;

namespace RedBlood
{
    public partial class FinalLabelUserControl : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Fill_Letter(Pack e)
        {
            if (e == null) return;

            imgDINBarcode.ImageUrl = BarcodeBLL.Url4DIN(e.DIN, "00", false);
            imgDINBarcode.Style.Apply(PrintSettingBLL.FinalLabel.DINBarcode);

            lblDIN.Text = e.DIN;
            lblDIN.Style.Apply(PrintSettingBLL.FinalLabel.DIN);

            lblCheckChar.Text = BarcodeBLL.CalculateISO7064Mod37_2(e.DIN);
            lblCheckChar.Style.Apply(PrintSettingBLL.FinalLabel.CheckChar);

            imgABOBarcode.ImageUrl = BarcodeBLL.Url4BloodGroup(e.Donation.BloodGroup, false);
            imgABOBarcode.Style.Apply(PrintSettingBLL.FinalLabel.ABOBarcode);

            lblABOCode.Text = e.Donation.BloodGroup;
            lblABOCode.Style.Apply(PrintSettingBLL.FinalLabel.ABOCode);

            lblABOLetter.Text = BloodGroupBLL.GetLetter(e.Donation.BloodGroup);
            lblABOLetter.Style.Apply(PrintSettingBLL.FinalLabel.ABOLetter);

            lblABORh.Text = BloodGroupBLL.GetRh(e.Donation.BloodGroup);
            lblABORh.Style.Apply(PrintSettingBLL.FinalLabel.ABORh);

            lblGeo.Text = e.Donation.Campaign != null
                ? string.Format("Nguồn: {0} - {1} - {2}",
                e.Donation.Campaign.Source == null ? "" : e.Donation.Campaign.Source.Name,
                e.Donation.Campaign.ID.ToString(),
                e.Donation.Campaign.CoopOrg == null ? "" :
                    (e.Donation.Campaign.CoopOrg.Geo1 == null ? "" : e.Donation.Campaign.CoopOrg.Geo1.Name)) : "";
            lblGeo.Style.Apply(PrintSettingBLL.FinalLabel.Geo);

            lblCollectedDateLabel.Text = "Ngày lấy máu: ";
            lblCollectedDateLabel.Style.Apply(PrintSettingBLL.FinalLabel.CollectedDateLabel);

            lblCollectedDate.Text = e.Date.ToStringVN();
            lblCollectedDate.Style.Apply(PrintSettingBLL.FinalLabel.CollectedDate);

            imgProductBarcode.ImageUrl = BarcodeBLL.Url4Product(e.ProductCode, false);
            imgProductBarcode.Style.Apply(PrintSettingBLL.FinalLabel.ProductBarcode);

            lblProductCode.Text = e.ProductCode;
            lblProductCode.Style.Apply(PrintSettingBLL.FinalLabel.ProductCode);

            lblProductDesc.Text = e.Product.LabelDesc;
            lblProductDesc.Style.Apply(PrintSettingBLL.FinalLabel.ProductDesc);

            lblVolumeLabel.Text = "Thể tích      ml";
            lblVolumeLabel.Style.Apply(PrintSettingBLL.FinalLabel.VolumeLabel);

            lblVolume.Text = e.Volume.ToString();
            lblVolume.Style.Apply(PrintSettingBLL.FinalLabel.Volume);

            lblExpiryDateLabel.Text = "Hạn sử dụng";
            lblExpiryDateLabel.Style.Apply(PrintSettingBLL.FinalLabel.ExpiryDateLabel);

            lblExpiryDate.Text = e.ExpirationDate.ToStringVN_Hour();
            lblExpiryDate.Style.Apply(PrintSettingBLL.FinalLabel.ExpiryDate);

            lblOrgLine1.Text = Resources.Resource.OrgLine1;
            lblOrgLine1.Style.Apply(PrintSettingBLL.FinalLabel.OrgLine1);

            lblOrgLine2.Text = Resources.Resource.OrgLine2;
            lblOrgLine2.Style.Apply(PrintSettingBLL.FinalLabel.OrgLine2);

            lblOrgLine3.Text = Resources.Resource.OrgLine3;
            lblOrgLine3.Style.Apply(PrintSettingBLL.FinalLabel.OrgLine3);

            lblOrgLine4.Text = Resources.Resource.OrgLine4;
            lblOrgLine4.Style.Apply(PrintSettingBLL.FinalLabel.OrgLine4);

            DivUC.Style.Apply(PrintSettingBLL.FinalLabel.FinalLabelSize);
        }
    }
}