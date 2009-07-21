using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Membership_ManageRole : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("SysAdmin"))
        {
            Response.Redirect("~/AccessDenied.aspx", true);
        }

        if (!Page.IsPostBack) DisplayRoleInGrid();
    }
    protected void btnCreateRole_Click(object sender, EventArgs e)
    {
        string roleName = txtRoleName.Text.Trim();

        if (!Roles.RoleExists(roleName))
        {
            Roles.CreateRole(roleName);
            DisplayRoleInGrid();
        }

        txtRoleName.Text = "";
    }

    private void DisplayRoleInGrid()
    {
        gvRoleList.DataSource = Roles.GetAllRoles();
        gvRoleList.DataBind();
    }
    protected void gvRoleList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Get the RoleNameLabel      
        Label lblRoleName = (Label)(gvRoleList.Rows[e.RowIndex].FindControl("RoleNameLabel"));

        Roles.DeleteRole(lblRoleName.Text.Trim(), false);
        
        DisplayRoleInGrid();
    }
}
