<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="ReceiveBlood.aspx.cs" Inherits="Enter" MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/MasterPageUserMenuWithBarcode.master" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<%@ Register Src="~/UserControl/PeopleHistory.ascx" TagPrefix="uc" TagName="PeopleHistory" %>
<%@ Register Src="~/UserControl/EnterPack.ascx" TagPrefix="uc" TagName="EnterPack" %>
<%@ Register Src="~/UserControl/CampaignDetail.ascx" TagPrefix="uc" TagName="CamDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here
        $(document).bind('keydown', 'Ctrl+m', function() {
            $("input[id*='btnNew']").click();
        });
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr valign="top">
                    <td style="width: 210px;">
                        <div class="part">
                            <div class="partHeader">
                                Tạo mới
                            </div>
                            <div class="partLinkLast">
                                <%--<asp:LinkButton ID="btnNew" runat="server" Text="Thêm người cho máu" OnClick="btnNew_Click" />--%>
                                <asp:Button ID="btnNew" runat="server" Text="Thêm người cho máu" OnClick="btnNew_Click" ToolTip="Ctrl+M" />
                                <%--<asp:ImageButton ID="btnNew" runat="server" Text="Thêm người cho máu" OnClick="btnNew_Click"  />--%>
                            </div>
                        </div>
                        <div class="part">
                            <div class="partHeader">
                                Đợt thu máu
                            </div>
                            <div class="partLinkLast">
                                <uc:CamDetail runat="server" ID="CamDetailLeft" />
                            </div>
                        </div>
                    </td>
                    <td align="center">
                        <uc:People runat="server" ID="ucPeople" />
                    </td>
                    <td style="width: 300px;">
                        <uc:EnterPack runat="server" ID="ucEnterPack" />
                        <div class="part">
                            <div class="partHeader">
                                Tiểu sử cho máu
                            </div>
                            <div class="partLinkLast">
                                <uc:PeopleHistory runat="server" ID="PeopleHistory1" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
