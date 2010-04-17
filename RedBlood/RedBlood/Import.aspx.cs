using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Import : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PackBLL.New(30000);
        //ExcelBLL.Import(User.Identity.Name);
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ImportBLL.Importing();
    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        e.Result = db.PrintSettings;
    }
}
