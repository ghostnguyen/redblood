using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedBlood;
using RedBlood.BLL;
public partial class Category_Sex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    SexBLL bll = new SexBLL();
    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text)) return;

        Guid ID = bll.Insert(txtName.Text);

        GridView1.DataBind();        
    }
}
