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

public partial class Membership_CreatingUserAccount : System.Web.UI.Page
{
    public aspnet_UserProfilesBLL aspnet_UserProfilesBLL { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!User.IsInRole("SysAdmin"))
        //{
        //    Response.Redirect("~/AccessDenied.aspx", true);
        //}

        if (!Page.IsPostBack)
        {
            // Reference the SpecifyRolesStep WizardStep           
            WizardStep SpecifyRolesStep = RegisterUser.FindControl("SpecifyRolesStep") as WizardStep;

            // Reference the RoleList CheckBoxList           
            CheckBoxList RoleList = SpecifyRolesStep.FindControl("RoleList") as CheckBoxList;

            // Bind the set of roles to RoleList           
            RoleList.DataSource = Roles.GetAllRoles();
            RoleList.DataBind();
        }
    }
    protected void RegisterUser_CreatingUser(object sender, LoginCancelEventArgs e)
    {
        string trimmedUserName = RegisterUser.UserName.Trim();
        if (RegisterUser.UserName.Length != trimmedUserName.Length)
        {
            // Show the error message
            InvalidUserNameOrPasswordMessage.Text = "Tên đăng nhập không được có khoảng trống ở đầu và cuối.";
            InvalidUserNameOrPasswordMessage.Visible = true;

            // Cancel the create user workflow
            e.Cancel = true;
        }
        else
        {
            // Username is valid, make sure that the password does not contain the username
            if (RegisterUser.Password.IndexOf(RegisterUser.UserName, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Show the error message
                InvalidUserNameOrPasswordMessage.Text = "Mật khẩu không được chứa tên đăng nhập.";
                InvalidUserNameOrPasswordMessage.Visible = true;

                // Cancel the create user workflow
                e.Cancel = true;
            }
        }
    }

    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {
        aspnet_UserProfilesBLL = new aspnet_UserProfilesBLL();

        TextBox txtFullname = RegisterUser.FindControl("CreateUserStepContainer").FindControl("txtFullname") as TextBox;
        TextBox txtPhone = RegisterUser.FindControl("CreateUserStepContainer").FindControl("Phone") as TextBox;

        aspnet_UserProfilesBLL.NewProfile(RegisterUser.UserName, txtFullname.Text, txtPhone.Text);
    }
    protected void RegisterUser_ActiveStepChanged(object sender, EventArgs e)
    {
        // Have we JUST reached the Complete step? 
        if (RegisterUser.ActiveStep.Title == "Complete")
        {
            // Reference the SpecifyRolesStep WizardStep          
            WizardStep SpecifyRolesStep = RegisterUser.FindControl("SpecifyRolesStep") as WizardStep;
            // Reference the RoleList CheckBoxList           
            CheckBoxList RoleList = SpecifyRolesStep.FindControl("RoleList") as CheckBoxList;
            // Add the checked roles to the just-added user           
            foreach (ListItem li in RoleList.Items)
            {
                if (li.Selected)
                    Roles.AddUserToRole(RegisterUser.UserName, li.Text);
            }
        }
    }
}

