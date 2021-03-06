﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood.BLL;

namespace RedBlood.UserControl
{
    public partial class UCCampaign : System.Web.UI.UserControl
    {
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CampaignID == 0)
            {
                Campaign p = new Campaign();

                if (LoadFromGUI(p))
                {
                    CampaignBLL.New(p);
                    CampaignID = p.ID;
                }
                else
                    return;
            }
            else
            {
                RedBloodDataContext db = new RedBloodDataContext();

                Campaign p = CampaignBLL.Get(CampaignID, db);

                if (p == null) return;

                if (LoadFromGUI(p))
                {
                    db.SubmitChanges();
                    CampaignID = p.ID;
                }
                else return;
            }
            Page.Alert("Lưu thành công.");
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (CampaignID == 0)
            {
                return;
            }
            else
            {
                CampaignBLL.Delete(CampaignID);
                Clear();
                Page.Alert("Xóa thành công");
            }
        }


        public void LoadCampaign()
        {
            Campaign e = CampaignBLL.Get(CampaignID);

            if (e == null)
            {
            }
            else
            {
                imgCodabar.ImageUrl = BarcodeBLL.Url4Campaign(e.ID);

                txtName.Text = e.Name;

                if (e.Est != null)
                {
                    txtEst.Text = e.Est.Value.ToString();
                }


                if (e.Date != null)
                {
                    txtDate.Text = e.Date.ToStringVN_Hour();
                }

                ddlSource.SelectedValue = e.SourceID.ToString();

                if (e.Type == Campaign.TypeX.Long_run) //Infinity campaign
                {
                    chkInfiCam.Checked = true;
                }

                if (e.CoopOrgID != null)
                {
                    txtCoopOrgName.Text = e.CoopOrg.Name;
                }

                if (e.HostOrgID != null)
                {
                    txtHostOrgName.Text = e.HostOrg.Name;
                }

                txtContactName.Text = e.ContactName;
                txtContactPhone.Text = e.ContactPhone;
                txtContactTitle.Text = e.ContactTitle;
                txtNote.Text = e.Note;
            }
        }

        public void Clear()
        {
            ViewState["CampaignID"] = 0;
            imgCodabar.ImageUrl = "none";
            txtName.Text = "";
            txtEst.Text = "";
            txtDate.Text = "";
            ddlSource.SelectedIndex = 0;
            chkInfiCam.Checked = false;

            txtCoopOrgName.Text = "";
            txtHostOrgName.Text = "";

            txtContactName.Text = "";
            txtContactPhone.Text = "";
            txtContactTitle.Text = "";
            txtNote.Text = "";

            divErrName.Attributes["class"] = "hidden";
            divErrDate.Attributes["class"] = "hidden";
            divErrCoopOrgName.Attributes["class"] = "hidden";
            divErrHostOrgName.Attributes["class"] = "hidden";
        }

        public void New()
        {
            Clear();
            txtName.Focus();
        }

        private bool LoadFromGUI(Campaign p)
        {
            bool isDone = true;

            try
            {
                p.Name = txtName.Text.Trim();
                divErrName.Attributes["class"] = "hidden";
            }
            catch (Exception ex)
            {
                divErrName.InnerText = ex.Message;
                divErrName.Attributes["class"] = "err";
                isDone = false;
            }

            p.Est = txtEst.Text.ToIntNullable();


            try
            {
                p.Date = txtDate.Text.ToDatetimeFromVNFormat();
                divErrDate.Attributes["class"] = "hidden";
            }
            catch (Exception ex)
            {
                divErrDate.Attributes["class"] = "err";
                divErrDate.InnerText = ex.Message;
                isDone = false;
            }

            //if (ddlSource.SelectedValue.ToInt() == 0)
            //    p.SourceID = null;
            //else
            //    p.SourceID = ddlSource.SelectedValue.ToInt();

            try
            {
                p.SourceID = ddlSource.SelectedValue.ToInt();
                divErrSrc.Attributes["class"] = "hidden";
            }
            catch (Exception ex)
            {
                divErrSrc.Attributes["class"] = "err";
                divErrSrc.InnerText = ex.Message;
                isDone = false;
            }

            if (chkInfiCam.Checked)
                p.Type = Campaign.TypeX.Long_run;
            else
                p.Type = Campaign.TypeX.Short_run;

            try
            {
                //p.CoopOrg = OrgBLL.GetByName(txtCoopOrgName.Text.Trim());
                p.CoopOrgID = OrgBLL.GetByName(txtCoopOrgName.Text.Trim()).ID;
                divErrCoopOrgName.Attributes["class"] = "hidden";
            }
            catch (Exception ex)
            {
                divErrCoopOrgName.Attributes["class"] = "err";
                divErrCoopOrgName.InnerText = ex.Message;
                isDone = false;
            }

            try
            {
                //p.HostOrg = OrgBLL.GetByName(txtHostOrgName.Text.Trim());
                p.HostOrgID = OrgBLL.GetByName(txtHostOrgName.Text.Trim()).ID;
                divErrHostOrgName.Attributes["class"] = "hidden";
            }
            catch (Exception ex)
            {
                divErrHostOrgName.Attributes["class"] = "err";
                divErrHostOrgName.InnerText = ex.Message;
                isDone = false;
            }

            p.ContactName = txtContactName.Text;
            p.ContactPhone = txtContactPhone.Text;
            p.ContactTitle = txtContactTitle.Text;

            p.Note = txtNote.Text;

            return isDone;
        }
    }
}