<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default7.aspx.cs" Inherits="Default1" Title="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here

        $(document).bind('keydown', '7', function() {
            window.location = ("/RedBlood/Collect/AssignDIN.aspx");
        });

        $(document).bind('keydown', '8', function() {
            window.location = ("/RedBlood/Collect/CollectPack.aspx");
        });

        $(document).bind('keydown', '9', function() {
            window.location = ("/RedBlood/Collect/SelectCampaignToRpt.aspx");
        });

        $(document).bind('keydown', '4', function() {
            window.location = ("/RedBlood/Collect/Rpt2Org.aspx");
        });

        //        $(document).bind('keydown', '5', function() {
        //            window.location.replace("/RedBlood/Codabar/Pack.aspx");
        //        });

        $(document).bind('keydown', '3', function() {
            window.location = ("/RedBlood/Collect/PrintSetting.aspx");
        });
    </script>

    <table width="100%">
        <tr>
            <td align="center">
                <br />
                <table id="menu_lvl2">
                    <tr>
                        <td class="column">
                            <table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Collect/AssignDIN.aspx">
                                            <img src="Image/Icon/number7.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Collect/AssignDIN.aspx">Nhập thông tin người cho và cấp mã số</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Collect/CollectPack.aspx">
                                            <img src="Image/Icon/number8.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Collect/CollectPack.aspx">Nhập túi máu và thể tích thu được</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Collect/SelectCampaignToRpt.aspx">
                                            <img src="Image/Icon/number9.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Collect/SelectCampaignToRpt.aspx">Tổng kết từng đợt thu máu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="rowline">
                        </td>
                    </tr>
                    <tr>
                        <td class="column">
                            <table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Collect/Rpt2Org.aspx">
                                            <img src="Image/Icon/number4.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Collect/Rpt2Org.aspx">In kết quả trả địa phương</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Codabar/Pack.aspx">
                                            <img src="Image/Icon/number5.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Codabar/Pack.aspx">Tạo mã túi máu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/ReceiveBlood.aspx">
                                            <img src="Image/Icon/number6.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/ReceiveBlood.aspx"></a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="rowline">
                        </td>
                    </tr>
                    <tr>
                        <td class="column">
                            <table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Collect/PrintSetting.aspx">
                                            <img src="Image/Icon/number3.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Collect/PrintSetting.aspx">Canh chỉnh vị trí dòng in.</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/ReceiveBlood.aspx">
                                            <img src="Image/Icon/number2.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/ReceiveBlood.aspx"></a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <%--<h3>
                                <a href="/RedBlood/PackManually.aspx">3. Thêm</a></h3>
                            <p>
                            </p>
                            <a href="/RedBlood/PackManually.aspx" />
                            <img src="Image/Icon/books-256.png" alt="" />--%>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
