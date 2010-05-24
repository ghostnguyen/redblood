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

public partial class ResetPassword4CurrentUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!User.IsInRole("SysAdmin"))
        //{
        //    Response.Redirect("~/AccessDenied.aspx", true);
        //}
        txtUsername.Text = User.Identity.Name;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtPassword1.Text != txtPassword2.Text)
        {
            ActionStatus.Text = "Mật khẩu mới không trùng.";
            return;
        }

        //if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
        //{
        //    ActionStatus.Text = "Nhập mật khẩu mới.";
        //    return;
        //}

        MembershipUser user = Membership.GetUser(txtUsername.Text.Trim());
        if (user == null)
        {
            ActionStatus.Text = "Không tồn tại tài khoản này.";
        }
        else
        {
            //string pass = user.ResetPassword();

            try
            {
                bool r = user.ChangePassword(txtOldPassword.Text, txtPassword1.Text);
                if (r)
                    ActionStatus.Text = "Mật khẩu đã được đổi";
                else
                    ActionStatus.Text = "Mật khẩu KHÔNG đổi được.";

            }
            catch (Exception ex)
            {
                ActionStatus.Text = ex.Message;
            }


        }
    }
}
