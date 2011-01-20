using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Login1.LoginError += new EventHandler(Login1_LoginError);

    }

    void Login1_LoginError(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
