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
        List<Donation> l = DonationBLL.New(5);

        foreach (Donation r in l)
        {
            //string strCode = BarcodeBLL.GenStringCode(Resources.Codabar.packSSC, r.Autonum.ToString());
            //r.Note = BarcodeLib.Code128.CalculateISO7064Mod37_2(BarcodeBLL.DINIdChar + r.DIN + "00").ToString();
            r.Note = "K";
        }



        DataList1.DataSource = l;
        DataList1.DataBind();
    }
   
}
