using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Codabar_Pack : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CodabarBLL codabarBLL = new CodabarBLL();
        PackBLL bll = new PackBLL();
        Pack[] l = PackBLL.New(5);

        foreach (Pack r in l)
        {
            string strCode = CodabarBLL.GenStringCode(Resources.Codabar.packSSC, r.Autonum.ToString());
            //r.Codabar = "Image.aspx?&code=" + strCode;
            r.Note = strCode;
        }

        DataList1.DataSource = l;
        DataList1.DataBind();
    }
   
}
