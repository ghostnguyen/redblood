using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Store_PrintReturn : System.Web.UI.Page
{
    public Return returnObj { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParamID = Request["ReturnID"];

            if (!string.IsNullOrEmpty(strParamID))
            {
                returnObj = ReturnBLL.Get(strParamID.ToInt());

                if (returnObj != null)
                {
                    LabelTitle1.Text = "BIÊN BẢN THU HỒI";

                    LoadReturn();

                    RedBloodDataContext db = new RedBloodDataContext();

                    var v1 = db.PackOrders.Where(r => r.ReturnID.Value == returnObj.ID)
                        .Select(r => r.Pack)
                        .ToList();

                    var v = v1.GroupBy(r => r.ProductCode)
                        .Select(r => new
                        {
                            ProductCode = r.Key,
                            Sum = r.Count(),
                            BloodGroupSumary = r.GroupBy(r1 => r1.Donation.BloodGroup).Select(r1 => new
                            {
                                BloodGroupDesc = BloodGroupBLL.GetDescription(r1.Key),
                                Total = r1.Count(),
                                VolumeSumary = r1.GroupBy(r2 => r2.Volume).Select(r2 => new
                                {
                                    Volume = r2.Key.HasValue ? r2.Key.Value.ToString() : "_",
                                    Total = r2.Count(),
                                    DINList = r2.Select(r3 => new { DIN = r3.DIN }).OrderBy(r4 => r4.DIN),
                                }).OrderBy(r2 => r2.Volume)
                            }).OrderBy(r1 => r1.BloodGroupDesc),
                        });

                    GridViewSum.DataSource = v;
                    GridViewSum.DataBind();

                    LableCount.Text = v1.Count().ToStringRemoveZero();
                }
            }
        }
    }

    public void LoadReturn()
    {
        if (returnObj != null)
        {
            imgBarcode.ImageUrl = BarcodeBLL.Url4Return(returnObj.ID);

            txtNote.Text = returnObj.Note;
            lblActor.Text = returnObj.Actor;

            if (returnObj.Date != null)
                txtDate.Text = returnObj.Date.ToStringVN_Hour();
        }
    }
}
