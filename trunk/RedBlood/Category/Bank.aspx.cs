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

public partial class Category_Bank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Bank bank = new Bank();

        bank.Name = "Điền tên ngân hàng";
        bank.Level = 1;

        db.Banks.InsertOnSubmit(bank);
        db.SubmitChanges();

        GridView1.DataBind();
    }

    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            ActionStatus.Text = "Tồn tại tài khoản thuộc ngân hàng/chi nhánh này. Hay ngân hàng có chi nhánh.";
            e.ExceptionHandled = true;
        }
        else
        {
            ActionStatus.Text = "";
        }
    }

    protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            ActionStatus.Text = "Tồn tại tài khoản thuộc chi nhánh này.";
            e.ExceptionHandled = true;
        }
        else
        {
            ActionStatus.Text = "";
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (GridView1.SelectedValue == null) return;

        RedBloodDataContext db = new RedBloodDataContext();

        Bank bank = new Bank();

        bank.Name = "Điền tên chi nhánh";
        bank.Level = 2;
        bank.ParentID = (Guid)GridView1.SelectedValue;

        db.Banks.InsertOnSubmit(bank);
        db.SubmitChanges();

        GridView2.DataBind();
    }
}
