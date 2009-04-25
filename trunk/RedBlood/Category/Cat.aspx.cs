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

public partial class Category_Cat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    CatBLL bll = new CatBLL();

    protected void btnLevel1New_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLevel1.Text.Trim())) return;

        string r = bll.Insert(txtLevel1.Text.Trim(), 1, null);

        ActionStatus.Text = r;

        txtLevel1.Text = "";

        GridView1.DataBind();
    }

    protected void btnLevel2New_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLevel2.Text.Trim())) return;

        if (GridView1.SelectedValue == null) return;

        string r = bll.Insert(txtLevel2.Text.Trim(), 2, (Guid)GridView1.SelectedValue);

        ActionStatus.Text = r;

        txtLevel2.Text = "";

        GridView2.DataBind();
    }

    protected void btnLevel3New_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLevel3.Text.Trim())) return;

        if (GridView2.SelectedValue == null) return;

        string r = bll.Insert(txtLevel3.Text.Trim(), 3, (Guid)GridView2.SelectedValue);

        ActionStatus.Text = r;

        txtLevel3.Text = "";

        GridView3.DataBind();
    }

    protected void btnLevel4New_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLevel4.Text.Trim())) return;

        if (GridView3.SelectedValue == null) return;

        string r = bll.Insert(txtLevel4.Text.Trim(), 4, (Guid)GridView3.SelectedValue);

        ActionStatus.Text = r;

        txtLevel4.Text = "";

        GridView4.DataBind();
    }

    protected void btnLevel5New_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLevel5.Text.Trim())) return;

        if (GridView4.SelectedValue == null) return;

        string r = bll.Insert(txtLevel5.Text.Trim(), 5, (Guid)GridView4.SelectedValue);

        ActionStatus.Text = r;

        txtLevel5.Text = "";

        GridView5.DataBind();
    }

    protected void GridView_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            ActionStatus.Text = e.Exception.Message;
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;
        }
        else
        {
            ActionStatus.Text = "";
        }
    }


    protected void GridView_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            ActionStatus.Text = "Không thể xóa khi tồn tại cấp nhỏ hơn.";
            e.ExceptionHandled = true;
        }
        else
        {
            ActionStatus.Text = "";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
