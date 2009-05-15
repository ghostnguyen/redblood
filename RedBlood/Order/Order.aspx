<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Order_Order" %>

<%@ MasterType VirtualPath="~/MasterPageUserMenuWithBarcode.master" %>
<%@ Register Src="~/UserControl/Order.ascx" TagPrefix="uc" TagName="Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here
        $(document).bind('keydown', 'Ctrl+m', function() {
            $("input[id*='btnNew']").click();
        });
    </script>

    <table width="100%">
        <tr valign="top">
            <td style="width: 180px;">
                <div class="part">
                    <div class="partHeader">
                        Tạo mới
                    </div>
                    <div class="partLinkLast">
                        <asp:Button ID="btnNew" runat="server" Text="Thêm cấp phát" OnClick="btnNew_Click"
                            ToolTip="Ctrl+M" />
                    </div>
                </div>
            </td>
            <td align="left">
                <uc:Order runat="server" ID="Order1" />
            </td>
            
        </tr>
    </table>
</asp:Content>
