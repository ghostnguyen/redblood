﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collect_AssignDIN : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ucPeople.Code = Request.Params["key"].ToString();
        }
        else
        {
            //ucEnterPack.PlateletApheresisConfirmed += new EventHandler(ucEnterPack_PlateletApheresisConfirmed);
            //ucPeople.PeopleChanged += new EventHandler(ucPeople_PeopleChanged);

            string code = Master.TextBoxCode.Text.Trim();
            Master.TextBoxCode.Text = "";

            if (code.Length == 0) return;

            if (BarcodeBLL.IsValidDINCode(code))
            {
                DINEnter(code);
            }
            else if (BarcodeBLL.IsValidCampaignCode(code))
            {
                CampaignEnter(code);
            }
            else
            {
                ucPeople.Code = code;
            }
        }
    }

    //void ucEnterPack_PlateletApheresisConfirmed(object sender, EventArgs e)
    //{
    //    PeopleHistory1.LoadPeople();
    //}

    void ucPeople_PeopleChanged(object sender, EventArgs e)
    {
        //ucEnterPack.PeopleID = (Guid)sender;
        //PeopleHistory1.PeopleID = (Guid)sender;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ucPeople.New("");
        //ucEnterPack.PeopleID = Guid.Empty;
    }

    private void DINEnter(string code)
    {
        ucEnterPack.Assign(p.Autonum, CamDetailLeft.CampaignID);

        //Pack p = PackBLL.GetByCode(code);
        //if (p == null) return;

        //Donation e = DonationBLL

        //if (p.PeopleID == null && p.CampaignID == null)
        //{
        //    if (ucPeople.PeopleID == Guid.Empty)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi", "alert ('Chưa nhập thông tin người cho máu.');", true);
        //        return;
        //    }
        //    if (CamDetailLeft.CampaignID == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi", "alert ('Chưa nhập thông tin đợt thu máu.');", true);
        //        return;
        //    }

            
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi",
        //            "alert ('Túi máu: " + p.Status.ToString() + "');", true);
        //    return;
        //}
    }

    private void CampaignEnter(string code)
    {
        CamDetailLeft.CampaignID = BarcodeBLL.ParseCampaignID(code);
    }

}
