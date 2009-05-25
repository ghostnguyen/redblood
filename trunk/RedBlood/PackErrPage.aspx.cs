using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackErrPage : System.Web.UI.Page
{
    PackBLL bll = new PackBLL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinqDataSourceEnterPackErr_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        //e.Result = PackBLL.GetEnterPackErr();        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //PackBLL.Delete_EnterPackErr(Page.User.Identity.Name);
        GridViewEnterPackErr.DataBind();
    }
}
