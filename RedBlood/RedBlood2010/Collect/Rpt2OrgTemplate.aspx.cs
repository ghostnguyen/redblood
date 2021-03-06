﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Collect_Rpt2OrgTemplate : System.Web.UI.Page
{
    public ReportType RptType { get; set; }
    public Campaign Camp { get; set; }
    public Guid CoopOrgGeo1ID { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strCamID = Request["CampaignID"];
            string rptType = Request["RptType"];

            if (!string.IsNullOrEmpty(strCamID)
                && !string.IsNullOrEmpty(rptType))
            {
                RptType = (ReportType)rptType.ToInt();
                Camp = CampaignBLL.Get(strCamID.ToInt());

                try
                {
                    CoopOrgGeo1ID = Camp.CoopOrg.Geo1.ID;
                }
                catch (Exception)
                {
                }

                CampaignDetail1.CampaignID = Camp.ID;

                switch (RptType)
                {
                    case ReportType.FourPosInCam:
                        LabelTitle1.Text = "Danh sách kết quả xét nghiệm";
                        //LabelTitle2.Text = "Dương tính (Không bao gồm HIV)";
                        foreach (DataControlField item in GridView1.Columns)
                        {
                            if (item.HeaderText == "HIV")
                            { item.Visible = false; }
                        }

                        break;
                    case ReportType.NegInCam:
                        LabelTitle1.Text = "Danh sách kết quả xét nghiệm";
                        //LabelTitle2.Text = "Âm tính";

                        break;
                    case ReportType.HIVInCam:
                        LabelTitle1.Text = "Danh sách kết quả xét nghiệm";
                        //LabelTitle2.Text = "Dương tính (Bao gồm HIV)";

                        break;
                    default:
                        break;
                }
                GridView1.DataBind();

                divNote.Visible = (RptType == ReportType.HIVInCam) && IsSpecialProvince();
            }
        }
    }

    private bool IsSpecialProvince()
    {
        if (CoopOrgGeo1ID != Guid.Empty)
        {
            if (CoopOrgGeo1ID == Geo.BRVT || CoopOrgGeo1ID == Geo.TayNinh)
            {
                return true;
            }
            else return false;
        }
        return false;
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = DonationBLL.Get(CampaignDetail1.CampaignID, RptType)
            .Select(r => new
            {
                r.DIN,
                CMND = r.People != null ? r.People.CMND : "",
                Name = r.People != null ? r.People.Name : "",
                DOB = r.People != null ? r.People.DOBToString : "",
                r.OrgVolume,
                BloodGroupDesc = r.BloodGroupDesc,
                HIV = r.Markers != null ? (r.Markers.HIV == TR.pos.Name && IsSpecialProvince() ? "XN lần 2" : r.Markers.HIV) : "",
                HCV_Ab = r.Markers != null ? r.Markers.HCV_Ab : "",
                HBs_Ag = r.Markers != null ? r.Markers.HBs_Ag : "",
                Syphilis = r.Markers != null ? r.Markers.Syphilis : "",
                Malaria = r.Markers != null ? r.Markers.Malaria : "",
                ResidentAddress = r.People != null ? r.People.ResidentAddress : "",
                Geo3Name = (r.People != null && r.People.ResidentGeo3 != null) ? r.People.ResidentGeo3.Name : "",
                Geo2Name = (r.People != null && r.People.ResidentGeo2 != null) ? r.People.ResidentGeo2.Name : "",
            });
    }
    protected void LinqDataSource1_Selected(object sender, LinqDataSourceStatusEventArgs e)
    {
        if (e.Result != null && e.Result is List<Donation>)
            LableCount.Text = "Tổng cộng: " + ((List<Donation>)e.Result).Count.ToString();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dynamic p = e.Row.DataItem;
    }

}
