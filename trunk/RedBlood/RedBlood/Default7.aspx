<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default7.aspx.cs" Inherits="Default1" Title="Default" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here

        $(document).bind('keydown', '7', function() {
            window.location = ("Collect/DIN.aspx");
        });

        $(document).bind('keydown', '8', function() {
            window.location = ("Collect/CampaignPage.aspx");
        });

        $(document).bind('keydown', '9', function() {
            window.location = ("Collect/AssignDIN.aspx");
        });

        $(document).bind('keydown', '4', function() {
            window.location = ("Collect/CollectPack.aspx");
        });

        $(document).bind('keydown', '5', function() {
            window.location = ("Collect/CollectDetailRptSelect.aspx");
        });

        $(document).bind('keydown', '6', function() {
            window.location = ("Collect/Rpt2OrgMenu.aspx");
        });

        //        $(document).bind('keydown', '5', function() {
        //            window.location = ("Codabar/Pack.aspx");
        //        });



//        $(document).bind('keydown', '1', function() {
//            window.location = ("Collect/PrintSetting.aspx");
//        });

        $(document).bind('keydown', '2', function() {
            window.location = ("Collect/Rpt1.aspx");
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
                                        <a href="/Collect/DIN.aspx">
                                            <img src="Image/Icon/number7.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Collect/DIN.aspx">Tạo mã thu máu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="Collect/CampaignPage.aspx">
                                            <img src="Image/Icon/number8.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Collect/CampaignPage.aspx">Tạo đợt thu máu</a>
                                        </h4>
                                    </td>
                                </tr>
                                <%--  <tr>
                                    <td class="column icon" colspan="2">
                                        <a href="Collect/CampaignPage.aspx" />
                                        <img src="Image/Icon/Ambulance.png" alt="" class="iconImg" />
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="Collect/AssignDIN.aspx">
                                            <img src="Image/Icon/number9.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Collect/AssignDIN.aspx">Nhập thông tin người cho máu và cấp mã số</a>
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
                                        <a href="Collect/CollectPack.aspx">
                                            <img src="Image/Icon/number4.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Collect/CollectPack.aspx">Nhập loại túi máu, nhóm máu và thể tích máu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="Collect/CollectDetailRptSelect.aspx">
                                            <img src="Image/Icon/number5.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Collect/CollectDetailRptSelect.aspx">Tổng kết từng đợt thu máu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="Collect/Rpt2OrgMenu.aspx">
                                            <img src="Image/Icon/number6.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Collect/Rpt2OrgMenu.aspx">In kết quả trả địa phương</a>
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
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="Collect/PrintSetting.aspx">
                                            <img src="Image/Icon/number1.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Collect/PrintSetting.aspx">Canh chỉnh vị trí dòng in.</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="Collect/Rpt1.aspx">
                                            <img src="Image/Icon/number2.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Collect/Rpt1.aspx">Báo cáo theo tỉnh</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
