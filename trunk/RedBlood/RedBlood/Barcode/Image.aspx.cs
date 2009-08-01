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
        BarcodeLib.Barcode b = new BarcodeLib.Barcode();

        string IdChar = "";
        bool checkChar = false;
        string code = "";
        bool hasText = false;

        string topleft = "";
        string topright = "";

        try
        {
            IdChar = Request["IdChar"].ToString();
        }
        catch (Exception)
        {

        }

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

        try
        {
            topleft = Request["topleft"];
        }
        catch (Exception)
        {

        }

        try
        {
            topright = Request["topright"];
        }
        catch (Exception)
        {

        }

        try
        {
            checkChar = bool.Parse(Request["checkChar"]);
        }
        catch (Exception)
        {

        }


        BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;

        try
        {
            b.IncludeLabel = hasText;
            System.Drawing.Image img;
            //===== Encoding performed here =====
            if (IdChar == BarcodeBLL.DINIdChar)
                img = b.Encode(type, IdChar + code, Color.Black, Color.White, 150, 50, IdChar + code.Substring(0, 5) + " " + code.Substring(5, 2) + " " + code.Substring(7, 6) + " " + code.Substring(13, 2));
                //img = b.Encode(type, IdChar + code, Color.Black, Color.White, 150, 50, "afsdf");
            else
                img = b.Encode(type, IdChar + code, Color.Black, Color.White, 150, 50, "");
            //===================================

            MemoryStream m = new MemoryStream();

            img.Save(m, ImageFormat.Png);

            Response.ContentType = "image/png";

            m.WriteTo(Response.OutputStream);

            m.Dispose();

        }//try
        catch (Exception ex)
        {
        }//catch
    }
}
