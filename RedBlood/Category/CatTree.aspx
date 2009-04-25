<%@ Page Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="CatTree.aspx.cs" Inherits="Category_CatTree" Title="Danh mục sản phẩm" %>

<%@ Register Src="~/UserControl/ucCatTree.ascx" TagPrefix="uc" TagName="CatTree" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePannel1">
        <ContentTemplate>
            <div style="float: left;border-right:dotted 1px black; padding-right:5px;">
                <uc:CatTree runat="server" ID="ucCatTree1" />
            </div>
            <div style="float: left; margin-left: 10px;">
                <p>
                    <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
                </p>
                <h4>
                    Tên danh mục
                </h4>
                <asp:TextBox ID="txtCat" runat="server"></asp:TextBox>
                <asp:LinkButton ID="LinkButtonUpdate" Text="<%$ Resources:Resource,Update %>" runat="server"
                    OnClick="LinkButtonUpdate_Click"></asp:LinkButton>
                <asp:LinkButton ID="LinkButtonDelete" Text="<%$ Resources:Resource,Delete %>" runat="server"
                    OnClick="LinkButtonDelete_Click" OnClientClick='return confirm("Xóa danh mục này và tất cả danh mục con nếu có?");'></asp:LinkButton>
                <h4>
                    Tạo danh mục con
                </h4>
                <asp:TextBox ID="txtChildName" runat="server"></asp:TextBox>
                <asp:LinkButton ID="LinkButtonNew" Text="<%$ Resources:Resource,New %>" runat="server"
                    OnClick="LinkButtonNew_Click">LinkButton</asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
