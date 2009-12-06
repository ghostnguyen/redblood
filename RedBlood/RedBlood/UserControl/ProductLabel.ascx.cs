using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ProductLabel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Fill_Letter(string code,string note)
    {
        Image1.ImageUrl = BarcodeBLL.Url4Product(code);
        Image1.Style.Apply(PrintSettingBLL.ProductLabel.Barcode);

        txtNote.Text = note;
        txtNote.Style.Apply(PrintSettingBLL.ProductLabel.Note);
    }
}
