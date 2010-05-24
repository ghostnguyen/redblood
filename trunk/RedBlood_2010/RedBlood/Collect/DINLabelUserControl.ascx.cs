using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DINLabelUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Fill_Letter(string DIN)
    {
        Image1.ImageUrl = BarcodeBLL.Url4DIN(DIN);
        Image1.Style.Apply(PrintSettingBLL.DINLabel.ImageDIN);

        txtCheckChar.Text = BarcodeBLL.CalculateISO7064Mod37_2(DIN);
        txtCheckChar.Style.Apply(PrintSettingBLL.DINLabel.CheckChar);
    }

    public void ResizeLabel(PrintSetting ps)
    {
        divLabel.Style.Apply(ps);
    }
}
