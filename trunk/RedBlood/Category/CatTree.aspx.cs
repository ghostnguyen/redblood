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

public partial class Category_CatTree : System.Web.UI.Page
{
    CatBLL bll = new CatBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        ucCatTree1.Tree_SelectedNodeChanged += new EventHandler(CatTree_Tree_SelectedNodeChanged);
    }

    void CatTree_Tree_SelectedNodeChanged(object sender, EventArgs e)
    {
        txtCat.Text = ucCatTree1.SelectedNode.Text;
    }

    protected void LinkButtonUpdate_Click(object sender, EventArgs e)
    {
        ucCatTree1.Rename_SelectedNode(txtCat.Text.Trim());
    }
    protected void LinkButtonDelete_Click(object sender, EventArgs e)
    {
        string mess = ucCatTree1.Delete_SelectedNode();
        
        if(!string.IsNullOrEmpty(mess))
            ScriptManager.RegisterStartupScript(LinkButtonDelete, this.GetType(), "openpopup", "alert('" + mess + "');", true);
    }
    protected void LinkButtonNew_Click(object sender, EventArgs e)
    {
        ucCatTree1.Create_Child_Of_SelectedNode(txtChildName.Text.Trim());        
    }
}
