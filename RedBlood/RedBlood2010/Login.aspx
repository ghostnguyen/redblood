<%@ Page Language="C#" AutoEventWireup="true" Inherits="RedBlood.Login" Codebehind="Login.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">
    <title></title>

    <script src="jquery-1.3.2.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function() { WindowOnResize(); });

        function WindowOnResize() {
            var winWidth = $(window).width() - 30;
            var winHeight = $(window).height() - 30;

            $("#PanelLogin").css("width", winWidth);
            $("#PanelLogin").css("height", winHeight);
        }
    
    </script>

</head>
<body onresize="WindowOnResize();">
    <form id="form1" runat="server">
    <div>
        <asp:Panel runat="server" ID="PanelLogin">
            <table width="100%" style="height: 100%;">
                <tr align="center" valign="middle">
                    <td>
                        <asp:Login ID="Login1" runat="server" LoginButtonText="Đăng nhập" FailureText="Đăng nhập không thành công."
                            PasswordLabelText="Mật khẩu:" PasswordRequiredErrorMessage="Nhập mật khẩu." RememberMeText="Đăng nhập tự động cho lần sau."
                            TitleText="Đăng nhập" UserNameLabelText="Tài khoản:" UserNameRequiredErrorMessage="Nhập tài khoản."
                            DestinationPageUrl="~/Default.aspx" BackColor="#F7F6F3" BorderColor="#E6E2D8"
                            BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                            Font-Size="0.8em" ForeColor="#333333">
                            <TextBoxStyle Font-Size="0.8em" />
                            <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
                        </asp:Login>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
