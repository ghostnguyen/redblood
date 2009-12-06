using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Barcode_GenerateDIN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGen_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Barcode/PrintDINLabel.aspx?numOfDIN=" + txtNumOfDIN.Text + "&numOfCopy=" + txtNumOfCopy.Text, true);
    }
}
