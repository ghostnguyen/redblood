using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace RedBlood
{
    public partial class ResetPassword4Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "admin";
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
                    if (user.IsLockedOut)
                    {
                        user.UnlockUser();
                    }

                    if (!user.IsApproved)
                    {
                        user.IsApproved = true;
                        Membership.UpdateUser(user);
                    }

                    string oldPass = user.ResetPassword();
                    //bool r = user.ChangePassword(txtOldPassword.Text, txtPassword1.Text);
                    bool r = user.ChangePassword(oldPass, txtPassword1.Text);
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
}