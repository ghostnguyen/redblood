using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public partial class GenCodabar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = "";
        bool hasText = false;

        try
        {
            code = Request["code"].ToString();
        }
        catch (Exception)
        {

        }


        try
        {
            hasText = bool.Parse(Request["hasText"]);
        }
        catch (Exception)
        {

        }

        CodabarImg gen = new CodabarImg();

        Bitmap b = gen.Draw(code, 1, 40, hasText);

        MemoryStream m = new MemoryStream();

        b.Save(m, ImageFormat.Png);

        Response.ContentType = "image/png";

        m.WriteTo(Response.OutputStream);

        b.Dispose();
        m.Dispose();
    }
}
