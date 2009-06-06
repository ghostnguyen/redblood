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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "txtCode_PostBack", CodabarBLL.JScript4Postback(), true);
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
                Response.Redirect("~/FindAndReport/PeopleDetail.aspx?key=" + r.ID.ToString());
            }
        }
        else if (CodabarBLL.IsValidPackCode(key))
        {
            Pack r = PackBLL.Get(CodabarBLL.ParsePackAutoNum(key));
            if (r != null)
            {
                Response.Redirect("~/FindAndReport/PackDetail.aspx?key=" + r.Autonum.ToString());
            }            
        }
        else if (CodabarBLL.IsValidCampaignCode(key))
        {
            Campaign r = CampaignBLL.GetByID(CodabarBLL.ParseCampaignID(key));
            if (r != null)
            {
                Response.Redirect("~/FindAndReport/CampaignDetail.aspx?key=" + r.ID.ToString());
            }
        }
        else if (regx.IsMatch(key) && key.Length >= Resources.Codabar.CMNDLength.ToInt())
        {
            People r = PeopleBLL.GetByCMND(key);
            if (r != null)
            {
                Response.Redirect("~/FindAndReport/PeopleDetail.aspx?key=" + r.ID.ToString());
            }
        }
        else if (key.Length > 1)
        {
            Response.Redirect("~/FindAndReport/FindPeople.aspx?key=" + key);
        }

        //Master.TextBoxCode.Text = "";
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {

    }
}
