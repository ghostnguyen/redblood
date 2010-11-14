<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Default6" Title="Default" Codebehind="Default6.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here

        $(document).bind('keydown', '7', function() {
            window.location = ("Store/Count.aspx");
        });

        $(document).bind('keydown', '8', function() {
            window.location = ("Store/TransCount.aspx");
        });

        $(document).bind('keydown', '4', function() {
            window.location = ("Store/Order4Org.aspx");
        });
        
        $(document).bind('keydown', '5', function() {
            window.location = ("Store/Return.aspx");
        });

        $(document).bind('keydown', '6', function () {
            window.location = ("Store/Rpt_ReturnByDay.aspx");
        });

        $(document).bind('keydown', '1', function() {
            window.location = ("Store/Delete.aspx");
        });

        $(document).bind('keydown', '2', function () {
            window.location = ("Store/Rpt_DeleteByDay.aspx");
        });

        $(document).bind('keydown', '3', function () {
            window.location = ("Store/Rpt_OrderByDay.aspx");
        });




        //        $(document).bind('keydown', '9', function() {
        //            window.location = ("FindAndReport/PackOrderCount.aspx");
        //        });

        //        $(document).bind('keydown', '4', function() {
        //            window.location = ("Category/Org.aspx");
        //        });

        //        $(document).bind('keydown', '5', function() {
        //            window.location = ("Codabar/Pack.aspx");
        //        });
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
                                        <a href="Store/Count.aspx">
                                            <img src="Image/Icon/number7.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Store/Count.aspx">Kiểm kho</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                             <table>
                                <tr>
                                    <td>
                                        <a href="Store/TransCount.aspx">
                                            <img src="Image/Icon/number8.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Store/TransCount.aspx">Xuất - Nhập - Tồn</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="FindAndReport/PackOrderCount.aspx">
                                            <img src="Image/Icon/number9.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="FindAndReport/PackOrderCount.aspx">Cấp phát máu</a>
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
                                        <a href="Store/Order4Org.aspx">
                                            <img src="Image/Icon/number4.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Store/Order4Org.aspx">Cấp phát cho tỉnh</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="Store/Return.aspx">
                                            <img src="Image/Icon/number5.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Store/Return.aspx">Thu hồi</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="Store/Rpt_ReturnByDay.aspx">
                                            <img src="Image/Icon/number6.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Store/Rpt_ReturnByDay.aspx">Báo cáo thu hồi</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td colspan="3" class="rowline">
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="column">
                             <table>
                                <tr>
                                    <td>
                                        <a href="Store/Delete.aspx">
                                            <img src="Image/Icon/number1.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Store/Delete.aspx">Hủy túi máu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="Store/Rpt_DeleteByDay.aspx">
                                            <img src="Image/Icon/number2.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Store/Rpt_DeleteByDay.aspx">Báo cáo hủy</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="Store/Rpt_OrderByDay.aspx">
                                            <img src="Image/Icon/number1.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Store/Rpt_OrderByDay.aspx">Thống kê cấp phát</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
  
</asp:Content>
