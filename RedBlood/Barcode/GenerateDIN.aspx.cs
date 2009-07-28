using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Codabar_GenerateDIN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BarcodeBLL codabarBLL = new BarcodeBLL();
        DonationBLL.New(5);
        
        //foreach (Pack r in l)
        //{
        //    string strCode = BarcodeBLL.GenStringCode(Resources.Codabar.packSSC, r.Autonum.ToString());
        //    r.Note = strCode;
        //}

        //DataList1.DataSource = l;
        //DataList1.DataBind();
    }
   
}
