﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pack.aspx.cs" Inherits="Codabar_Pack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<link href="../App_Themes/Default/PackLabel.css" rel="stylesheet" type="text/css" />--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataList ID="DataList1" runat="server">
            <ItemTemplate>
                <asp:Panel runat="server" ID="Panel1" CssClass="PackLabel_PannelBorder">
                    <p class="PackLabel_Img">
                        <%--<asp:Image runat="server" ImageUrl='<%# CodabarBLL.Url4Pack( Eval("Autonum") as int?) %>' />--%>
                        <br />
                    </p>
                    <asp:Label runat="server" ID="Label2" Text="Tên:" CssClass="PackLabel_Name" />
                </asp:Panel>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>