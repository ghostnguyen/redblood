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

public partial class Category_TestDef : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    TestDefBLL bll = new TestDefBLL();

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

        string r = bll.Insert(txtLevel2.Text.Trim(), 2, (int)GridView1.SelectedValue);

        ActionStatus.Text = r;

        txtLevel2.Text = "";

        GridView2.DataBind();
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
    
}
