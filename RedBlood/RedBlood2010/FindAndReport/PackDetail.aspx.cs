using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindAndReport_PackDetail : System.Web.UI.Page
{
    public string DIN
    {
        get
        {
            return (string)ViewState["DIN"];
        }
        set
        {
            ViewState["DIN"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DIN = Request.Params["key"];
            DetailView1.DataBind();
        }
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        Donation d = DonationBLL.Get(DIN);

        if (d == null || d.Pack == null)
        {
            e.Result = null;
            e.Cancel = true;
        }
        else
            e.Result = d;
    }

    protected void LinqDataSourcePackRelative_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        
    }


}
