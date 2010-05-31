using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Collect_DIN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtNumOfDIN.Focus();
    }
    protected void btnGen_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Collect/DINPrintLabel.aspx?numOfDIN=" + txtNumOfDIN.Text, true);
    }
}
