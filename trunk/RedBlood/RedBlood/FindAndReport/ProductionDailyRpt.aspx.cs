using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindAndReport_ProductionDailyRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.Date.ToStringVN();
            LoadData();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        LoadData();
    }

    void LoadData()
    {
        DateTime? dt = txtDate.Text.ToDatetimeFromVNFormat();

        RedBloodDataContext db = new RedBloodDataContext();

        GridView1.DataSource = db.Packs.Where(r => r.Date.Value.Date == dt).OrderBy(r => r.ProductCode);
        GridView1.DataBind();
    }

}
