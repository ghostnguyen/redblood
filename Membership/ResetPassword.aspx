<%@ Page Language="C#" MasterPageFile="~/MasterPageAdminMenu.master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="Membership_RecoverPassword" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
    <h3>
        Tạo mật khẩu mới
    </h3>
    Tên tài khoản:
    <br />
    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
    <br />
    Mật khẩu mới:
    <br />
    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Mật khẩu mới" OnClick="Button1_Click" />
</asp:Content>
