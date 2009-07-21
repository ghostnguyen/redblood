<%@ Page Language="C#" MasterPageFile="~/MasterPageAdminMenu.master" AutoEventWireup="true"
    CodeFile="UserAndRole.aspx.cs" Inherits="Membership_UserAndRole" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p align="center">
        <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
    </p>
    <h3>
        Quản lý quyền cho mỗi người dùng.</h3>
    <p>
        <b>Chọn người dùng:</b>
        <asp:DropDownList ID="UserList" runat="server" AutoPostBack="True" DataTextField="UserName"
            DataValueField="UserName" OnSelectedIndexChanged="UserList_SelectedIndexChanged">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Repeater ID="UsersRoleList" runat="server">
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="RoleCheckBox" AutoPostBack="true" Text='<%# Container.DataItem %>'
                    OnCheckedChanged="RoleCheckBox_CheckChanged" />
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </p>
    <h3>
        Quản lý người dùng trong mỗi quyền.
    </h3>
    <p>
        <b>Chọn quyền:</b>
        <asp:DropDownList ID="RoleList" runat="server" AutoPostBack="true" 
            onselectedindexchanged="RoleList_SelectedIndexChanged">
        </asp:DropDownList>
    </p>
    <p>
        <asp:GridView ID="RolesUserList" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="No users belong to this role." 
            onrowdeleting="RolesUserList_RowDeleting">
            <Columns>
                <asp:CommandField DeleteText="<%$ Resources:Resource,Delete %>" ShowDeleteButton="True" />
                <asp:TemplateField HeaderText="Người dùng">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="UserNameLabel" Text='<%# Container.DataItem %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <b>Người dùng:</b>
        <asp:TextBox ID="UserNameToAddToRole" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="AddUserToRoleButton" runat="server" Text="Gán quyền" 
            onclick="AddUserToRoleButton_Click" />
    </p>
</asp:Content>
