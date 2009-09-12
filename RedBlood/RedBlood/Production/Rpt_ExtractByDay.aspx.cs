using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindAndReport_Rpt_ExtractByDay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDateFrom.Text = DateTime.Now.Date.ToStringVN();
            txtDateTo.Text = DateTime.Now.Date.ToStringVN();
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

        RedBloodDataContext db = new RedBloodDataContext();

        GridView1.DataSource = db.Packs.Where(r => r.Date.Value.Date >= dtFrom && r.Date.Value.Date <= dtTo
            && r.Donation.OrgPackID != r.ID).OrderBy(r => r.Date);
        GridView1.DataBind();
    }

}
