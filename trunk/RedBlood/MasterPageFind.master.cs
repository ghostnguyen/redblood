using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class MasterPageFind : System.Web.UI.MasterPage
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

        if (!IsPostBack)
        {
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

        string key = TextBoxCode.Text.Trim();

        if (key.Length == 0) return;

        string pattern = @"\d+";
        Regex regx = new Regex(pattern);

        if (CodabarBLL.IsValidPeopleCode(key))
        {
            People r = PeopleBLL.GetByCode(key);
            if (r != null)
            {
                Response.Redirect("~/Find/PeopleDetail.aspx?key=" + r.ID.ToString());
            }
        }
        else if (CodabarBLL.IsValidPackCode(key))
        {
            Pack r = PackBLL.Get(CodabarBLL.ParsePackAutoNum(key));
            if (r != null)
            {
                Response.Redirect("~/Find/PackDetail.aspx?key=" + r.Autonum.ToString());
            }            
        }
        else if (CodabarBLL.IsValidCampaignCode(key))
        {
            Campaign r = CampaignBLL.GetByID(CodabarBLL.ParseCampaignID(key));
            if (r != null)
            {
                Response.Redirect("~/Find/CampaignDetail.aspx?key=" + r.ID.ToString());
            }
        }
        else if (regx.IsMatch(key) && key.Length >= Resources.Codabar.CMNDLength.ToInt())
        {
            People r = PeopleBLL.GetByCMND(key);
            if (r != null)
            {
                Response.Redirect("~/Find/PeopleDetail.aspx?key=" + r.ID.ToString());
            }
        }
        else if (key.Length > 1)
        {
            Response.Redirect("~/Find/FindPeople.aspx?key=" + key);
        }

        //Master.TextBoxCode.Text = "";
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {

    }
}
