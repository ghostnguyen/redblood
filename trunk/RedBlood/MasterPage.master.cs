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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
            Response.Redirect("~/Login.aspx");

        if (!Page.IsPostBack)
        {
            SiteMapNode node = SiteMap.RootNode.Find(this.Request.Path);

            if (node != null)
                lblTitle.Text = node.Title;
            else
            {
                if (Request.Path.Contains("Default.aspx"))
                    lblTitle.Text = "RedBlood";
                else
                    lblTitle.Text = Request.Path;
            }
        }

        //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"jquery", ResolveUrl("jquery-1.3.2.js"),true);
        //Page.ClientScript.RegisterClientScriptInclude("jquery", ResolveUrl("jquery-1.3.2.js"));

    }
}
