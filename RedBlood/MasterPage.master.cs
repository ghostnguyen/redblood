using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
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
        if (!Request.IsAuthenticated)
            Response.Redirect("~/Login.aspx");

        
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "txtCode_PostBack", CodabarBLL.JScript4Postback(), true);

        if (!Request.Url.ToString().ToLower().Contains("default"))
        {
            txtCode.Focus();
        }
    }
}
