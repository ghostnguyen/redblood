using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Category_BloodGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GridView1.DataSource = BloodGroup.BloodGroupList;
            GridView1.DataBind();
        }
        
        
    }

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
            int count = (GridView1.SelectedRow.FindControl("txtCount") as TextBox).Text.ToInt();
            
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "In",
            //            "window.open('" + System.Web.VirtualPathUtility.ToAbsolute("~/Category/BloodGroupPrint.aspx") + "?count=" + count.ToString() + "&code=" + GridView1.SelectedValue.ToString() + "');", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "In",
                string.Format("window.open('{0}?count={1}&code={2}&addText={3}');",
                    System.Web.VirtualPathUtility.ToAbsolute("~/Category/BloodGroupPrint.aspx"),
                    count.ToString(),
                    GridView1.SelectedValue.ToString(),
                    txtMoreText.Text
                    )
                , true);
        }
        catch (Exception)
        {

        }


    }
}
