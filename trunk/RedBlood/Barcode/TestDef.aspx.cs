using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Codabar_TestDef : System.Web.UI.Page
{
    BarcodeBLL bll = new BarcodeBLL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        //RedBloodDataContext db = new RedBloodDataContext();

        //var v = (from r in db.TestDefs
        //         where r.Level == 2
        //         select r).AsEnumerable().Select(r => new
        //         {
        //             r.ID,
        //             r.Name,
        //             ParentName = r.Parent.Name,
        //             r.Note,
        //             r.Level,
        //             Codabar = ("Image.aspx?code=" + BarcodeBLL.GenStringCode(Resources.Codabar.testResultSSC, r.ID.ToString()))
        //         });

        //e.Result = v.OrderBy(p => p.ParentName);
    }
    protected void LinqDataSource2_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        //RedBloodDataContext db = new RedBloodDataContext();

        //var v = (from r in db.TestDefs
        //         where r.Level == 1
        //         select r).AsEnumerable().Select(r => new
        //         {
        //             r.ID,
        //             r.Name,
        //             r.Note,
        //             r.Level,
        //             Codabar = ("Image.aspx?code=" + BarcodeBLL.GenStringCode(Resources.Codabar.testResultSSC, r.ID.ToString()))
        //         });

        //e.Result = v.OrderBy(p => p.Name);
    }
}
