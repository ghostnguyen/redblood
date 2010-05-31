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
using RedBlood.BLL;

public partial class Membership_ManageUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!User.IsInRole("SysAdmin"))
        //{
        //    Response.Redirect("~/AccessDenied.aspx", true);
        //}

        ActionStatus.Text = "";
    }

    protected void UserAccounts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Guid id = (Guid)e.Keys[0];
        GridViewRow row = UserAccounts.Rows[e.RowIndex];
        string fullname = (row.FindControl("txtFullname") as TextBox).Text;
        string email = (row.FindControl("txtEmail") as TextBox).Text;
        string phone = (row.FindControl("txtPhone") as TextBox).Text;
        string comment = (row.FindControl("txtComment") as TextBox).Text;

        bool isApproved = (row.FindControl("chkIsApproved") as CheckBox).Checked;

        MembershipUser user = Membership.GetUser(id);

        if (user == null) return;

        user.Email = email;
        user.Comment = comment;
        user.IsApproved = isApproved;

        Membership.UpdateUser(user);

        aspnet_UserProfilesBLL bll = new aspnet_UserProfilesBLL();
        bll.UpdateProfile(user.UserName, fullname, phone);

        //LinqDataSource1.DataBind();
    }

    protected void UserAccounts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid id = (Guid)e.Keys[0];

        MembershipUser user = Membership.GetUser(id);

        if (user == null) return;

        if (user.CreationDate.Date == user.LastLoginDate.Date)
        {
            Membership.DeleteUser(user.UserName, true);
            ActionStatus.Text = "Đã xóa tài khoản.";
        }
        else
        {
            ActionStatus.Text = "Không thể xóa. Tài khoản này đã đăng nhập.";
            e.Cancel = true;
        }
    }
}
