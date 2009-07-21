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

public partial class Membership_RecoverPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("SysAdmin"))
        {
            Response.Redirect("~/AccessDenied.aspx", true);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
        {
            ActionStatus.Text = "Nhập mật khẩu mới.";
            return;
        }

        MembershipUser user = Membership.GetUser(txtUsername.Text.Trim());
        if (user == null)
        {
            ActionStatus.Text = "Không tồn tại tài khoản này.";
        }
        else
        {
            string pass = user.ResetPassword();

            try
            {
                bool r = user.ChangePassword(pass, txtPassword.Text.Trim());
                if (r)
                    ActionStatus.Text = "Mật khẩu đã được đổi";
                else
                    ActionStatus.Text = "Mật khẩu KHÔNG đổi được.";

            }
            catch (Exception)
            {

                ActionStatus.Text = "Mật khẩu KHÔNG đổi được. Mật khẩu mới không đúng quy định.";
            }
            
            
        }
    }
}
