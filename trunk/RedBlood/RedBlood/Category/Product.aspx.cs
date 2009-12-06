using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category_Product : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //SexBLL bll = new SexBLL();
    //protected void ButtonNew_Click(object sender, EventArgs e)
    //{
    //    if (string.IsNullOrEmpty(txtName.Text)) return;

    //    Guid ID = bll.Insert(txtName.Text);

    //    GridView1.DataBind();        
    //}
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {

        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //int count = (GridView1.SelectedRow.Controls[2].Controls[1] as TextBox).Text.ToInt();
            int count = (GridView1.SelectedRow.FindControl("txtCount") as TextBox).Text.ToInt();
            
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Lỗi",
                        "window.open('" + System.Web.VirtualPathUtility.ToAbsolute("~/Category/ProductPrint.aspx") + "?count=" + count.ToString() + "&code=" + GridView1.SelectedValue.ToString() + "');", true);
        }
        catch (Exception)
        {

        }


    }
}
