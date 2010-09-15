<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword4Admin.aspx.cs" Inherits="RedBlood.ResetPassword4Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
    </div>
    </form>
</body>
</html>
