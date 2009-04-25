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

        StringBuilder script = new StringBuilder();

        script.Append("function checkLength(text) \n");
        script.Append("{ \n");
        script.Append("var len = text.length;  \n");

        script.Append("if (len == "
            + Resources.Codabar.testResultLength
            + " && text[0] == "
            + "\"" + Resources.Codabar.testResultStartCode + "\""
            + " && text[len - 1] == "
            + "\"" + Resources.Codabar.testResultStopCode + "\""
            + ") \n");
        script.Append("{ \n");
        script.Append("document.forms[0].submit(); \n");
        script.Append("} \n");

        script.Append("if (len == "
            + Resources.Codabar.packLength
            + " && text[0] == "
            + "\"" + Resources.Codabar.packStarCode + "\""
            + " && text[len - 1] == "
            + "\"" + Resources.Codabar.packStopCode + "\""
            + ") \n");
        script.Append("{ \n");
        script.Append("document.forms[0].submit(); \n");
        script.Append("} \n");

        script.Append("if (len == "
            + Resources.Codabar.peopleLength
            + " && text[0] == "
            + "\"" + Resources.Codabar.peopleStarCode + "\""
            + " && text[len - 1] == "
            + "\"" + Resources.Codabar.peopleStopCode + "\""
            + ") \n");
        script.Append("{ \n");
        script.Append("document.forms[0].submit(); \n");
        script.Append("} \n");


        script.Append("} \n");

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "txtCode_PostBack", script.ToString(), true);

        
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {

    }
}
