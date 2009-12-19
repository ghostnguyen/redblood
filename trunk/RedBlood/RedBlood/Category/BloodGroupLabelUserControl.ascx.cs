using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BloodGroupLabelUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Fill_Letter(string code, string note)
    {
        Image1.ImageUrl = BarcodeBLL.Url4BloodGroup(code);
        Image1.Style.Apply(PrintSettingBLL.BloodGroupLabel.Barcode);

        txtNote.Text = note;
        txtNote.Style.Apply(PrintSettingBLL.BloodGroupLabel.Note);
    }

    public void ResizeLabel1()
    {
        divLabel.Style.Apply(PrintSettingBLL.BloodGroupLabel.Label1);
    }

    public void ResizeLabel2()
    {
        divLabel.Style.Apply(PrintSettingBLL.BloodGroupLabel.Label2);
    }

    public void ResizeLabel3()
    {
        divLabel.Style.Apply(PrintSettingBLL.BloodGroupLabel.Label3);
    }
}
