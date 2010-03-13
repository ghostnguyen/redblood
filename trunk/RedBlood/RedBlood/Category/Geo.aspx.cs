using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Category_Geo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLevel1New_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLevel1.Text.Trim())) return;

        GeoBLL.Insert(txtLevel1.Text.Trim(), 1, null);

        txtLevel1.Text = "";

        GridView1.DataBind();
    }

    protected void btnLevel2New_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLevel2.Text.Trim())) return;

        if (GridView1.SelectedValue == null) return;

        GeoBLL.Insert(txtLevel2.Text.Trim(), 2, (Guid)GridView1.SelectedValue);

        txtLevel2.Text = "";

        GridView2.DataBind();
    }

    protected void btnLevel3New_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLevel3.Text.Trim())) return;

        if (GridView2.SelectedValue == null) return;

        GeoBLL.Insert(txtLevel3.Text.Trim(), 3, (Guid)GridView2.SelectedValue);

        txtLevel3.Text = "";

        GridView3.DataBind();
    }

    protected void GridView_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
            this.Alert(e.Exception.Message);
        }
    }


    protected void GridView_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ExceptionHandled = true;
            this.Alert("Không thể xóa khi tồn tại cấp nhỏ hơn.");
        }
    }
}
