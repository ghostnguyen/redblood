using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category_PrintSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //DropDownList1.DataSource =  PrintSetting.TypeX.

            RedBloodDataContext db = new RedBloodDataContext();

            DropDownList1.DataSource = db.PrintSettings.Select(r => r.Type).Distinct();
            DropDownList1.DataBind();
            
            //foreach (PrintSetting.TypeX r in Enum.GetValues(typeof(PrintSetting.TypeX)))
            //{
            //    ListItem item = new ListItem(Enum.GetName(typeof(PrintSetting.TypeX), r), ((int)r).ToString() );
            //    DropDownList1.Items.Add(item);
            //}

        }

    }
    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();
        
        //PrintSetting.TypeX type = (PrintSetting.TypeX)DropDownList1.SelectedValue.ToInt();

        //e.Result = db.PrintSettings.Where(r => r.Type == type).ToList();
        e.Result = db.PrintSettings.Where(r => r.Type == DropDownList1.SelectedValue).ToList();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
}
