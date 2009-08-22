<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BloodGroupPrint.aspx.cs" Inherits="Category_BloodGroupPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataList ID="DataList1" runat="server">
            <ItemTemplate>
                <asp:Panel runat="server" ID="Panel1" CssClass="PackLabel_PannelBorder">
                    <p class="PackLabel_Img">
                        <asp:Image runat="server" ImageUrl='<%# BarcodeBLL.Url4BloodGroup( Eval("Code") as string) %>' />
                        <br />
                        <asp:Label runat="server" ID="Label2" Text='<%# Eval("Description")  %>' CssClass="PackLabel_Name" />
                    </p>
                </asp:Panel>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
