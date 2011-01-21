using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Store_PrintDelete : System.Web.UI.Page
{
    public Delete delete { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParamID = Request["DeleteID"];

            if (!string.IsNullOrEmpty(strParamID))
            {
                delete = DeleteBLL.Get(strParamID.ToInt());

                if (delete != null)
                {
                    LabelTitle1.Text = "BIÊN BẢN HỦY MÁU";

                    LoadOrder();

                    RedBloodDataContext db = new RedBloodDataContext();

                    var v1 = db.Packs.Where(r => r.DeleteID.Value == delete.ID).ToList();

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

    public void LoadOrder()
    {
        if (delete != null)
        {
            imgBarcode.ImageUrl = BarcodeBLL.Url4Delete(delete.ID);

            txtNote.Text = delete.Note;
            lblActor.Text = delete.Actor;

            if (delete.Date != null)
                txtDate.Text = delete.Date.ToStringVN_Hour();
        }
    }
}
