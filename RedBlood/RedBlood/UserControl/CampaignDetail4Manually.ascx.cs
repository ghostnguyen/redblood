﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_CampaignDetail4Manually : System.Web.UI.UserControl
{
    GeoBLL geoBLL = new GeoBLL();
    BarcodeBLL codabarBLL = new BarcodeBLL();
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
        Campaign e = CampaignBLL.Get(CampaignID);

        if (e == null)
        {
        }
        else
        {
            ImageCodabar.ImageUrl = BarcodeBLL.Url4Campaign(e.ID);

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

            if (!string.IsNullOrEmpty(e.ContactName))
            {
                lblContactName.Text = "Người LH: " + e.ContactName;
                lblTitle.Text = "Chức vụ: " + e.ContactTitle;
                lblPhone.Text = "ĐT: " + e.ContactPhone;
            }

            lblNote.Text = "Ghi chú: " + e.Note;            
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

        lblContactName.Text = "";
        lblTitle.Text = "";
        lblPhone.Text = "";

        lblNote.Text = "";
    }
}
