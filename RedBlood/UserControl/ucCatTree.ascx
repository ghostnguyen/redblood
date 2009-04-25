<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCatTree.ascx.cs" Inherits="UserControl_CatTree" %>

<script runat="server">

    
</script>

<asp:TreeView ID="TreeViewCat" runat="server" 
    OnTreeNodePopulate="TreeViewCat_TreeNodePopulate" ExpandDepth="1" 
    onselectednodechanged="TreeViewCat_SelectedNodeChanged" >
    <Nodes>
        <asp:TreeNode Text="Danh mục sản phẩm" PopulateOnDemand="true"></asp:TreeNode>
    </Nodes>
    
</asp:TreeView>
