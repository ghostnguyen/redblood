using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageUserMenuWithBarcode : System.Web.UI.MasterPage
{
    public TextBox TextBoxCode
    {
        get
        {
            return txtCode;
        }
        set
        { }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtCode.Focus();

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "txtCode_PostBack", CodabarBLL.JScript4Postback(), true);
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {

    }
}
