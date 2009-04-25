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

public partial class UserControl_CatTree : System.Web.UI.UserControl
{
    public Guid? Cat1ID
    {
        get
        {
            return Get_CatID(1);
        }
    }
    public Guid? Cat2ID
    {
        get
        {
            return Get_CatID(2);
        }
    }
    public Guid? Cat3ID
    {
        get
        {
            return Get_CatID(3);
        }
    }
    public Guid? Cat4ID
    {
        get
        {
            return Get_CatID(4);
        }
    }
    public Guid? Cat5ID
    {
        get
        {
            return Get_CatID(5);
        }
    }

    private Guid? Get_CatID(int level)
    {
        try
        {
            return new Guid(TreeViewCat.SelectedNode.ValuePath.Split('/')[level]);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public TreeNode SelectedNode
    {
        get
        {
            return TreeViewCat.SelectedNode;
        }
    }

    public event EventHandler Tree_SelectedNodeChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    CatBLL catBLL = new CatBLL();

    public void Load_Cat(TreeNode parentNode, Guid? parentID, int level)
    {
        Load_Cat(parentNode, parentID, level, false);
    }
    public void Load_Cat(TreeNode parentNode, Guid? parentID, int level, bool reload)
    {
        if (reload)
        {
            parentNode.ChildNodes.Clear();
        }

        if (parentNode.ChildNodes.Count != 0) return;

        foreach (Cat c in catBLL.GetByLevelAndParentID(parentID, level))
        {
            TreeNode newNode = new TreeNode(c.Name, c.ID.ToString());
            newNode.PopulateOnDemand = true;
            parentNode.ChildNodes.Add(newNode);
        }
    }
    protected void TreeViewCat_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.Depth == 0)
        {
            Load_Cat(e.Node, null, 1);
        }
        else if (e.Node.Depth >= 1)
            Load_Cat(e.Node, new Guid(e.Node.Value), e.Node.Depth + 1);
    }

    protected void TreeViewCat_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (Tree_SelectedNodeChanged != null)
            Tree_SelectedNodeChanged(sender, e);
    }

    public void SelectCat(Guid? cat1ID, Guid? cat2ID, Guid? cat3ID, Guid? cat4ID, Guid? cat5ID)
    {
        TreeNode nodeLvl0 = TreeViewCat.Nodes[0];
        TreeNode nodeLvl1 = null, nodeLvl2 = null, nodeLvl3 = null, nodeLvl4 = null, nodeLvl5 = null;

        if (cat1ID == null) nodeLvl0.Select();
        else
        {
            nodeLvl0.Expand();
            nodeLvl1 = nodeLvl0.Find(cat1ID.Value.ToString());
        }

        if (cat2ID == null)
        {
            if (nodeLvl1 != null) nodeLvl1.Select();
        }
        else
        {
            nodeLvl1.Expand();
            nodeLvl2 = nodeLvl1.Find(cat2ID.Value.ToString());
        }

        if (cat3ID == null)
        {
            if (nodeLvl2 != null) nodeLvl2.Select();
        }
        else
        {
            nodeLvl2.Expand();
            nodeLvl3 = nodeLvl2.Find(cat3ID.Value.ToString());
        }

        if (cat4ID == null)
        {
            if (nodeLvl3 != null) nodeLvl3.Select();
        }
        else
        {
            nodeLvl3.Expand();
            nodeLvl4 = nodeLvl3.Find(cat4ID.Value.ToString());
        }

        if (cat5ID == null)
        {
            if (nodeLvl4 != null) nodeLvl4.Select();
        }
        else
        {
            nodeLvl4.Expand();
            nodeLvl5 = nodeLvl4.Find(cat5ID.Value.ToString());
        }

        if (nodeLvl5 != null) nodeLvl5.Select();
    }

    public string Delete_SelectedNode()
    {
        if (TreeViewCat.SelectedNode == null) return "";
        if (TreeViewCat.SelectedNode.Depth == 0) return "";

        Guid ID = TreeViewCat.SelectedNode.Value.ToGuid();

        string mess = catBLL.Delete(ID);

        TreeNode parent = TreeViewCat.SelectedNode.Parent;
        
        if(string.IsNullOrEmpty(mess))
            Load_Cat(parent, parent.Value.ToGuid(), parent.Depth + 1, true);

        return mess;
    }

    public void Rename_SelectedNode(string newName)
    {
        if (TreeViewCat.SelectedNode == null) return;

        Guid ID = TreeViewCat.SelectedNode.Value.ToGuid();

        if (ID == Guid.Empty) return;

        catBLL.Update(ID, newName);

        Cat c = catBLL.GetByID(ID);

        if (c != null)
            TreeViewCat.SelectedNode.Text = c.Name;
    }

    public void Create_Child_Of_SelectedNode(string childName)
    {
        if (string.IsNullOrEmpty(childName.Trim())) return;

        if (TreeViewCat.SelectedNode == null) return;
        if (TreeViewCat.SelectedNode.Depth == 5) return;

        Guid ID = TreeViewCat.SelectedNode.Value.ToGuid();

        if (ID == Guid.Empty)
        {
            catBLL.Insert(childName.Trim(), TreeViewCat.SelectedNode.Depth + 1, null);
        }
        else
        {
            catBLL.Insert(childName.Trim(), TreeViewCat.SelectedNode.Depth + 1, ID);
        }

        Load_Cat(TreeViewCat.SelectedNode, ID, TreeViewCat.SelectedNode.Depth + 1, true);
    }

}
