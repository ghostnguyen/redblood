using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EnvelopeUserControl : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Fill_Letter(People e)
    {
        lblName.Text = e.Name;
        lblName.Style.Apply(PrintSettingBLL.Envelope.Name);

        lblAddress.Text = e.ResidentAddress;
        lblAddress.Style.Apply(PrintSettingBLL.Envelope.Address);

        lblGeo.Text = e.FullResidentalGeo;
        lblGeo.Style.Apply(PrintSettingBLL.Envelope.Geo);
    }
}
