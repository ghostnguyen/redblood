<%@ Page Language="C#" MasterPageFile="~/MasterPageAdminMenu.master" AutoEventWireup="true"
    CodeFile="ManageRole.aspx.cs" Inherits="Membership_ManageRole" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <b>Tên quyền:</b>
    <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
    
    <asp:Button ID="btnCreateRole" runat="server" Text="<%$ Resources:Resource,New %>" OnClick="btnCreateRole_Click" />
    
    <br />
    <br />
    
    <asp:GridView ID="gvRoleList" runat="server" AutoGenerateColumns="False" 
        onrowdeleting="gvRoleList_RowDeleting">
        <Columns>
        <asp:CommandField DeleteText="<%$ Resources:Resource,Delete %>" ShowDeleteButton="True" />
            <asp:TemplateField HeaderText="Quyền">
                <ItemTemplate>
                    <asp:Label runat="server" ID="RoleNameLabel" Text='<%# Container.DataItem %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
