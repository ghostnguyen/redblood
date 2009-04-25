using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category_PointDef : System.Web.UI.Page
{
    PointDefBLL bll = new PointDefBLL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridViewPointDef_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            LinkButton btn = (LinkButton)e.Row.FindControl("btnStatus");
            PointDef p = (PointDef)e.Row.DataItem;

            if (btn != null)
            {
                switch (p.Status)
                {
                    case 1:
                        btn.Text = Resources.Resource.Lock;
                        break;
                    case 2:
                        btn.Text = Resources.Resource.Unlock;
                        break;
                    default:
                        btn.Text = Resources.Resource.Unknown;
                        break;
                }
            }
        }
    }
    protected void GridViewPointDef_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ChangeStatus")
        {
            bll.UpdateStatus(e.CommandArgument.ToString().ToGuid());
            GridViewPointDef.DataBind();
        }
    }
    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text)) return;

        Guid ID = bll.Insert(txtName.Text);

        GridViewPointDef.DataBind();
        //SelectCustomer(ID);
    }
}
