using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Category_BloodGroupPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = "";
        int count = 0;

        try
        {
            code = Request["code"];
        }
        catch (Exception)
        {

        }

        try
        {
            count = Request["count"].ToInt();
        }
        catch (Exception)
        {

        }

        RedBloodDataContext db = new RedBloodDataContext();

        string desc = BloodGroupBLL.GetDescription(code);


        DataTable t = new DataTable();
        t.Columns.Add("ID");
        t.Columns.Add("Code");
        t.Columns.Add("Description");

        for (int i = 0; i < count; i++)
        {
            DataRow r = t.NewRow();
            r.ItemArray = new object[] { i, code, desc };
            t.Rows.Add(r);
        }

        DataList1.DataSource = t;
        DataList1.DataBind();
    }


}
