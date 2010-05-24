<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ResetPassword4CurrentUser.aspx.cs" Inherits="ResetPassword4CurrentUser"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
    <h3>
        Tạo mật khẩu mới
    </h3>
    Tên:
    <asp:Label ID="txtUsername" runat="server"></asp:Label>
    <br />
    Mật khẩu cũ:
    <br />
    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    Mật khẩu mới lần 1:
    <br />
    <asp:TextBox ID="txtPassword1" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    Mật khẩu mới lần 2:
    <br />
    <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Lưu" OnClick="Button1_Click" />
</asp:Content>
