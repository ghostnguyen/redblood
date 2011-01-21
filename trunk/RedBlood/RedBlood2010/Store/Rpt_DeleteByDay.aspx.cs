using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Store_Rpt_DeleteByDay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDateFrom.Text = DateTime.Now.Date.ToStringVN();
            txtHourFrom.Text = "00:01";

            txtDateTo.Text = DateTime.Now.Date.ToStringVN();
            txtHourTo.Text = "23:59";
            LoadData();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        LoadData();
    }

    void LoadData()
    {
        DateTime? dtFrom = txtDateFrom.Text.ToDatetimeFromVNFormat();
        DateTime? dtTo = txtDateTo.Text.ToDatetimeFromVNFormat();

        if (dtFrom.HasValue)
        {
            DateTime hourFrom;
            if (DateTime.TryParse(txtHourFrom.Text, out hourFrom))
            {
                dtFrom = dtFrom.Value.AddHours(hourFrom.Hour).AddMinutes(hourFrom.Minute);
            }
        }

        if (dtTo.HasValue)
        {
            DateTime hourTo;
            if (DateTime.TryParse(txtHourTo.Text, out hourTo))
            {
                dtTo = dtTo.Value.AddHours(hourTo.Hour).AddMinutes(hourTo.Minute);
            }
        }

        RedBloodDataContext db = new RedBloodDataContext();

        //var packs = db.Packs.Where(r => r.Date.Value >= dtFrom && r.Date.Value <= dtTo
        //   && r.Donation.OrgPackID != r.ID).OrderBy(r => r.Date);

        var v = db.Deletes.Where(r => r.Date.Value >= dtFrom && r.Date.Value <= dtTo)
            .OrderBy(r => r.Date)
            .ToList()
            .Select(r => new
            {
                r.ID,
                Date = r.Date.ToStringVN_Hour(),
                r.Note,
                r.Actor,
                r.Packs
            });



        GridView1.DataSource = v;
        GridView1.DataBind();

        var s = v.SelectMany(r => r.Packs).ToList();
        
        var v1 = s.ToList().GroupBy(r => r.ProductCode)
            .Select(r => new
            {
                ProductCode = r.Key,
                ProductDesc = ProductBLL.GetDesc(r.Key),
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

        GridViewSum.DataSource = v1;
        GridViewSum.DataBind();

    }

}
