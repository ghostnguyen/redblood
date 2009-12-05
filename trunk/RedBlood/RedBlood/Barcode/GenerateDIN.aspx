<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateDIN.aspx.cs" Inherits="Codabar_GenerateDIN" %>
<%@ Register Src="~/UserControl/DINLabel.ascx" TagPrefix="uc" TagName="DINLabel"  %>
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
                <asp:Panel runat="server" ID="Panel1">
                    <p class="PackLabel_Img">
                        <asp:Image runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN( Eval("DIN") as string, "00") %>' />
                        <asp:TextBox ID="txtCheckChar" runat="server" Text='<%# Eval("Note") %>' CssClass="txtCheckChar"></asp:TextBox>
                        <br />
                    </p>
                </asp:Panel>
            </ItemTemplate>
        </asp:DataList>
    </div>
     <div runat="server" id="divCon">
    </div>
    </form>
</body>
</html>
