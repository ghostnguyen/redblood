using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Membership_UserAndRole : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("SysAdmin"))
        {
            Response.Redirect("~/AccessDenied.aspx", true);
        }


        if (!Page.IsPostBack)
        {
            BindUserToUserList();
            BindRoleToList();

            CheckRolesForSelectedUser();

            // Display those users belonging to the currently selected role        
            BindRolesToList();
            DisplayUsersBelongingToRole();
        }
    }

    private void BindUserToUserList()
    {
        MembershipUserCollection users = Membership.GetAllUsers();
        UserList.DataSource = users;
        UserList.DataBind();
    }

    private void BindRoleToList()
    {
        string[] roles = Roles.GetAllRoles();
        UsersRoleList.DataSource = roles;
        UsersRoleList.DataBind();
    }



    private void CheckRolesForSelectedUser()
    {
        //Determine what roles the selected user belongs to      
        string selectedUserName = UserList.SelectedValue;

        string[] selectedUsersRoles = Roles.GetRolesForUser(selectedUserName);

        // Loop through the Repeater's Items and check or uncheck the checkbox as needed      
        foreach (RepeaterItem ri in UsersRoleList.Items)
        {
            // Programmatically reference the CheckBox           
            CheckBox RoleCheckBox = (CheckBox)(ri.FindControl("RoleCheckBox"));

            // See if RoleCheckBox.Text is in selectedUsersRoles           
            if (selectedUsersRoles.Contains(RoleCheckBox.Text))
            {
                RoleCheckBox.Checked = true;
            }
            else
            {
                RoleCheckBox.Checked = false;
            }
        }
    }
    protected void UserList_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckRolesForSelectedUser();
    }

    protected void RoleCheckBox_CheckChanged(object sender, EventArgs e)
    {
        // Reference the CheckBox that raised this event      
        CheckBox RoleCheckBox = sender as CheckBox;
        // Get the currently selected user and role      
        string selectedUserName = UserList.SelectedValue;
        string roleName = RoleCheckBox.Text;

        // Determine if we need to add or remove the user from this role      
        if (RoleCheckBox.Checked)
        {
            // Add the user to the role           
            Roles.AddUserToRole(selectedUserName, roleName);
            // Display a status message           
            ActionStatus.Text = string.Format("{0} đã được gán quyền {1}.", selectedUserName, roleName);
        }
        else
        {
            // Remove the user from the role           
            Roles.RemoveUserFromRole(selectedUserName, roleName);
            // Display a status message           
            ActionStatus.Text = string.Format("{0} đã được gỡ khỏi quyền {1}.", selectedUserName, roleName);
        }

        // Refresh the "by role" interface 
        DisplayUsersBelongingToRole();
    }

    private void BindRolesToList()
    {
        // Get all of the roles      
        string[] roles = Roles.GetAllRoles();
        UsersRoleList.DataSource = roles;
        UsersRoleList.DataBind();
        RoleList.DataSource = roles;
        RoleList.DataBind();
    }

    private void DisplayUsersBelongingToRole()
    {
        // Get the selected role  
        string selectedRoleName = RoleList.SelectedValue;
        // Get the list of usernames that belong to the role   

        if (selectedRoleName != string.Empty)
        {
            string[] usersBelongingToRole = Roles.GetUsersInRole(selectedRoleName);
            // Bind the list of users to the GridView   
            RolesUserList.DataSource = usersBelongingToRole;
            RolesUserList.DataBind();
        }
    }
    protected void RolesUserList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Get the selected role  
        string selectedRoleName = RoleList.SelectedValue;
        // Reference the UserNameLabel     
        Label UserNameLabel = RolesUserList.Rows[e.RowIndex].FindControl("UserNameLabel") as Label;
        // Remove the user from the role  
        Roles.RemoveUserFromRole(UserNameLabel.Text, selectedRoleName);
        // Refresh the GridView     
        DisplayUsersBelongingToRole();
        // Display a status message    
        ActionStatus.Text = string.Format("{0} đã được gán quyền {1}.", UserNameLabel.Text, selectedRoleName);

        // Refresh the "by user" interface    
        CheckRolesForSelectedUser();
    }
    protected void AddUserToRoleButton_Click(object sender, EventArgs e)
    {
        // Get the selected role and username   
        string selectedRoleName = RoleList.SelectedValue;
        string userNameToAddToRole = UserNameToAddToRole.Text;
        // Make sure that a value was entered   
        if (userNameToAddToRole.Trim().Length == 0)
        {
            ActionStatus.Text = "Nhập tên đăng nhập.";
            return;
        }
        // Make sure that the user exists in the system     
        MembershipUser userInfo = Membership.GetUser(userNameToAddToRole);
        if (userInfo == null)
        {
            ActionStatus.Text = string.Format("Không có tên đăng nhập '{0}' trong hệ thống.", userNameToAddToRole);
            return;
        }
        // Make sure that the user doesn't already belong to this role    
        if (Roles.IsUserInRole(userNameToAddToRole, selectedRoleName))
        {
            ActionStatus.Text = string.Format("{0} đã được gán quyền {1}.", userNameToAddToRole, selectedRoleName);
            return;
        }
        // If we reach here, we need to add the user to the role     
        Roles.AddUserToRole(userNameToAddToRole, selectedRoleName);
        // Clear out the TextBox    
        UserNameToAddToRole.Text = string.Empty;
        // Refresh the GridView      
        DisplayUsersBelongingToRole();
        // Display a status message      
        ActionStatus.Text = string.Format("Đã thêm {0} vào quyền {1}.", userNameToAddToRole, selectedRoleName);

        // Refresh the "by user" interface 
        CheckRolesForSelectedUser();
    }
    protected void RoleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayUsersBelongingToRole(); 
    }
}
