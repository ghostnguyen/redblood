<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="Collect_ThankLetter"
    CodeBehind="ThankLetter.aspx.cs" %>

<%@ Register Src="~/Collect/ThanksLetterUserControl.ascx" TagPrefix="uc" TagName="ThankLetter" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/jquery-1.4.1.min.js" />
        </Scripts>
    </asp:ScriptManager>
    <div runat="server" id="divCon">
    </div>
    <script type="text/javascript">
        //XN lần 2
        $("span:contains('Chưa xác định'),span:contains('Dương tính'),span:contains('XN lần 2')").css('font-weight', 'bolder').css('color', 'red');
    </script>
    </form>
</body>
</html>
