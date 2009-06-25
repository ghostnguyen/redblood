using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindAndReport_CampaignRpt : System.Web.UI.Page
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
                Camp = CampaignBLL.GetByID(strCamID.ToInt());

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
                            //if (item.HeaderText == "HIV")
                            //{ item.Visible = false; }
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
        if (CampaignDetail1.CampaignID == 0)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
        {
            e.Result = PackBLL.Get4Rpt(CampaignDetail1.CampaignID, RptType);
            if (e.Result == null)
                e.Cancel = true;
        }

    }
    protected void LinqDataSource1_Selected(object sender, LinqDataSourceStatusEventArgs e)
    {
        LableCount.Text = "Tổng cộng: " + e.TotalRowCount.ToString();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Pack p = null;
        try
        {
            p = e.Row.DataItem as Pack;
        }
        catch (Exception)
        {

        }

        string style = "font-weight:bolder";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                DataControlFieldCell item = cell as DataControlFieldCell;

                if (item.ContainingField.ToString() == "HIV")
                {
                    foreach (Control ctr in cell.Controls)
                    {
                        if (ctr is Label)
                        {
                            if (p.TestResult2.HIVID == TestDef.HIV.Pos)
                            {
                                cell.Attributes.Add("style", style);
                                if (IsSpecialProvince()) (ctr as Label).Text = "XN lần 2";
                            }
                        }
                    }
                }

                if (item.ContainingField.ToString() == "HCV")
                {
                    foreach (Control ctr in cell.Controls)
                    {
                        if (ctr is Label)
                        {
                            if (p.TestResult2.HCVID == TestDef.HCV.Pos)
                                cell.Attributes.Add("style", style);
                        }
                    }

                }

                if (item.ContainingField.ToString() == "HBsAg")
                {
                    foreach (Control ctr in cell.Controls)
                    {
                        if (ctr is Label)
                        {
                            if (p.TestResult2.HBsAgID == TestDef.HBsAg.Pos)
                                cell.Attributes.Add("style", style);
                        }
                    }

                }

                if (item.ContainingField.ToString() == "Syphilis")
                {
                    foreach (Control ctr in cell.Controls)
                    {
                        if (ctr is Label)
                        {
                            if (p.TestResult2.SyphilisID == TestDef.Syphilis.Pos)
                                cell.Attributes.Add("style", style);
                        }
                    }
                }

                if (item.ContainingField.ToString() == "Malaria")
                {
                    foreach (Control ctr in cell.Controls)
                    {
                        if (ctr is Label)
                        {
                            if (p.TestResult2.MalariaID == TestDef.Malaria.Pos)
                                cell.Attributes.Add("style", style);
                        }
                    }
                }
            }
        }
    }

}
