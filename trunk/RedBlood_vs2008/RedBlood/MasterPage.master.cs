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
            return txtMasterCode;
        }
        set
        { }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "txtCode_PostBack", BarcodeBLL.JScript4Postback(), true);

        if (Request.Url.ToString().ToLower().Contains("default")
            || Request.Url.ToString().ToLower().Contains("din.aspx"))
        {
            
        }
        else
        {
            txtMasterCode.Focus();
        }
    }
    protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    {
        ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
    }
}
