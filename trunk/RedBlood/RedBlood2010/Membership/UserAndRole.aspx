<%@ Page Language="C#" MasterPageFile="~/MasterPageAdminMenu.master" AutoEventWireup="true" Inherits="Membership_UserAndRole" Title="Untitled Page" Codebehind="UserAndRole.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p align="center">
        <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
    </p>
    <table>
        <tr valign="top">
            <td>
                <h3>
                    Quản lý theo người dùng</h3>
                <p>
                    <b>Chọn người dùng:</b>
                    <asp:DropDownList ID="UserList" runat="server" AutoPostBack="True" DataTextField="UserName"
                        DataValueField="UserName" OnSelectedIndexChanged="UserList_SelectedIndexChanged" AppendDataBoundItems="true">
                        <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
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
            </td>
            <td style="width: 20px; border-left: solid 1px;">
            </td>
            <td>
                <h3>
                    Quản lý theo quyền
                </h3>
                <p>
                    <b>Chọn quyền:</b>
                    <asp:DropDownList ID="RoleList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RoleList_SelectedIndexChanged">
                    
                    </asp:DropDownList>
                </p>
                <p>
                    <asp:GridView ID="RolesUserList" runat="server" AutoGenerateColumns="False" EmptyDataText="No users belong to this role."
                        OnRowDeleting="RolesUserList_RowDeleting">
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
                    <asp:Button ID="AddUserToRoleButton" runat="server" Text="Gán quyền" OnClick="AddUserToRoleButton_Click" />
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
