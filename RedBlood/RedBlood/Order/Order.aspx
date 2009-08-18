<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Order_Order" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Src="~/UserControl/Order4Org.ascx" TagPrefix="uc" TagName="Order4Org" %>
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
                        Tạo mới cấp phát
                    </div>
                    <div class="partLinkLast">
                        <%--<asp:Button ID="btnNew4Peple" runat="server" Text="Cho bệnh nhân" OnClick="btnNew4People_Click"
                            ToolTip="Ctrl+M" />--%>
                        <asp:Button ID="btnNew4Org" runat="server" Text="Cho Bệnh viện" OnClick="btnNew4Org_Click"
                            ToolTip="Ctrl+M" />
                    </div>
                </div>
            </td>
            <td align="left">
                <uc:Order4Org runat="server" ID="Order1" />
            </td>
        </tr>
    </table>
</asp:Content>
