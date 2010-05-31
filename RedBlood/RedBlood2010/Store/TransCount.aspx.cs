using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Store_TransCount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucDateRange.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ucDateRange.ToDate = DateTime.Now.Date;
        }

    }
    protected void LinqDataSourceStart_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        ucDateRange.Validated();

        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.vw_PackRemainDailies.Where(r => r.Date == ucDateRange.FromDate.Value.AddDays(-1))
            .ToList()
            .GroupBy(r => new { r.ProductCode, r.ProductDesc }, (r, sub) => new
            {
                r.ProductCode,
                r.ProductDesc,
                Total = sub.Sum(r1 => r1.Count),
                BloodGroupSumary = sub.GroupBy(r1 => r1.BloodGroup, (r1, BGSub) => new
                {
                    BloodGroupDesc = BloodGroupBLL.GetDescription(r1),
                    Total = BGSub.Sum(r3 => r3.Count)
                }),
                VolumeSumary = sub.GroupBy(r1 => r1.Volume, (r1, VolSub) => new
                {
                    Volume = r1,
                    Total = VolSub.Sum(r3 => r3.Count)
                })
            })
            .OrderBy(r => r.ProductDesc);
    }
    protected void LinqDataSourceIn_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        ucDateRange.Validated();

        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.vw_PackTrans.Where(r => PackTransactionBLL.InTypeList.Contains(r.Type)
                                                && ucDateRange.FromDate <= r.Date
                                                && r.Date <= ucDateRange.ToDate)
            .ToList()
            .GroupBy(r => new { r.ProductCode, r.ProductDesc }, (r, sub) => new
            {
                r.ProductCode,
                r.ProductDesc,
                Total = sub.Sum(r1 => r1.Count),
                TotalInCollect = sub.Where(r1 => r1.Type == PackTransaction.TypeX.In_Collect)
                                    .Sum(r1 => r1.Count),
                TotalInProduct = sub.Where(r1 => r1.Type == PackTransaction.TypeX.In_Product)
                                    .Sum(r1 => r1.Count),
                TotalInReturn = sub.Where(r1 => r1.Type == PackTransaction.TypeX.In_Return)
                                    .Sum(r1 => r1.Count),
                BloodGroupSumary = sub.GroupBy(r1 => r1.BloodGroup, (r1, BGSub) => new
                {
                    BloodGroupDesc = BloodGroupBLL.GetDescription(r1),
                    Total = BGSub.Sum(r3 => r3.Count)
                }),
                VolumeSumary = sub.GroupBy(r1 => r1.Volume, (r1, VolSub) => new
                {
                    Volume = r1,
                    Total = VolSub.Sum(r3 => r3.Count)
                })
            })
            .OrderBy(r => r.ProductDesc);
    }

    protected void LinqDataSourceOut_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        ucDateRange.Validated();

        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.vw_PackTrans.Where(r => PackTransactionBLL.OutTypeList.Contains(r.Type)
                                                && ucDateRange.FromDate <= r.Date
                                                && r.Date <= ucDateRange.ToDate)
            .ToList()
            .GroupBy(r => new { r.ProductCode, r.ProductDesc }, (r, sub) => new
            {
                r.ProductCode,
                r.ProductDesc,
                Total = sub.Sum(r1 => r1.Count),
                TotalOutOrder = sub.Where(r1 => PackTransactionBLL.OutOrderTypeList.Contains(r1.Type))
                                    .Sum(r1 => r1.Count),
                TotalOutProduct = sub.Where(r1 => r1.Type == PackTransaction.TypeX.Out_Product)
                                    .Sum(r1 => r1.Count),
                TotalOutDelete = sub.Where(r1 => r1.Type == PackTransaction.TypeX.Out_Delete)
                                    .Sum(r1 => r1.Count),
                BloodGroupSumary = sub.GroupBy(r1 => r1.BloodGroup, (r1, BGSub) => new
                {
                    BloodGroupDesc = BloodGroupBLL.GetDescription(r1),
                    Total = BGSub.Sum(r3 => r3.Count)
                }),
                VolumeSumary = sub.GroupBy(r1 => r1.Volume, (r1, VolSub) => new
                {
                    Volume = r1,
                    Total = VolSub.Sum(r3 => r3.Count)
                })
            })
            .OrderBy(r => r.ProductDesc);
    }


    protected void LinqDataSourceEnd_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        ucDateRange.Validated();

        RedBloodDataContext db = new RedBloodDataContext();

        e.Result = db.vw_PackRemainDailies.Where(r => r.Date == ucDateRange.ToDate)
            .ToList()
            .GroupBy(r => new { r.ProductCode, r.ProductDesc }, (r, sub) => new
            {
                r.ProductCode,
                r.ProductDesc,
                Total = sub.Sum(r1 => r1.Count),
                BloodGroupSumary = sub.GroupBy(r1 => r1.BloodGroup, (r1, BGSub) => new
                {
                    BloodGroupDesc = BloodGroupBLL.GetDescription(r1),
                    Total = BGSub.Sum(r3 => r3.Count)
                }),
                VolumeSumary = sub.GroupBy(r1 => r1.Volume, (r1, VolSub) => new
                {
                    Volume = r1,
                    Total = VolSub.Sum(r3 => r3.Count)
                })
            })
            .OrderBy(r => r.ProductDesc);
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        GridViewStart.DataBind();
        GridViewIn.DataBind();
        GridViewOut.DataBind();
        GridViewEnd.DataBind();

    }
    protected void chkStart_CheckedChanged(object sender, EventArgs e)
    {
        PanelStart.Visible = chkStart.Checked;
    }
    protected void chkIn_CheckedChanged(object sender, EventArgs e)
    {
        PanelIn.Visible = chkIn.Checked;
    }
    protected void chkOut_CheckedChanged(object sender, EventArgs e)
    {
        PanelOut.Visible = chkOut.Checked;
    }
    protected void chkEnd_CheckedChanged(object sender, EventArgs e)
    {
        PanelEnd.Visible = chkEnd.Checked;
    }
}
