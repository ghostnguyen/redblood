using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
namespace RedBlood.BLL
{
    /// <summary>
    /// Summary description for aspnet_UserProfilesBLL
    /// </summary>

    public class aspnet_UserProfilesBLL
    {

        public aspnet_UserProfilesBLL()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        public void NewProfile(string username, string fullname, string phone)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            var users = from u in db.aspnet_Users
                        where u.UserName == username
                        select u;

            if (users.Count() != 1) return;

            aspnet_User user = users.First();

            aspnet_UserProfile user_profile = new aspnet_UserProfile();
            user_profile.UserID = user.UserId;
            user_profile.Fullname = fullname;
            user_profile.Phone = phone;

            if (user.aspnet_UserProfile == null)
            {
                db.aspnet_UserProfiles.InsertOnSubmit(user_profile);
                db.SubmitChanges();
            }
        }

        public void UpdateProfile(string username, string fullname, string phone)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            var users = from u in db.aspnet_Users
                        where u.UserName == username
                        select u;

            if (users.Count() != 1) return;

            aspnet_User user = users.First();

            if (user.aspnet_UserProfile != null)
            {
                user.aspnet_UserProfile.Fullname = fullname;
                user.aspnet_UserProfile.Phone = phone;

                db.SubmitChanges();
            }
        }
    }
}