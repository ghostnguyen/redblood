<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default5.aspx.cs" Inherits="Default5" Title="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here

        $(document).bind('keydown', '7', function() {
            window.location = ("Production/Extract.aspx");
        });

        //        $(document).bind('keydown', '8', function() {
        //            window.location = ("Production/Devide.aspx");

        //        });

        //        $(document).bind('keydown', '9', function() {
        //            window.location = ("Production/Extract.aspx");
        //        });

        $(document).bind('keydown', '4', function() {

        window.location = ("Production/Rpt_ExtractByDay.aspx");
        });

        //        $(document).bind('keydown', '5', function() {
        //            window.location = ("Production/Extract.aspx");
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
                                        <a href="Production/Extract.aspx">
                                            <img src="Image/Icon/number7.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Production/Extract.aspx">Sản xuất</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="Production/Divide.aspx">
                                            <img src="Image/Icon/number8.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Production/Divide.aspx">Chia túi</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="Production/Extract.aspx">
                                            <img src="Image/Icon/number9.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Production/Extract.aspx">Tra cứu</a>
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
                                        <a href="Production/Rpt_ExtractByDay.aspx">
                                            <img src="Image/Icon/number4.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Production/Rpt_ExtractByDay.aspx">Báo cáo ngày</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="Production/Extract.aspx">
                                            <img src="Image/Icon/number5.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Production/Extract.aspx">Tra cứu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="ReceiveBlood.aspx">
                                            <img src="Image/Icon/number6.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="ReceiveBlood.aspx"></a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td colspan="3" class="rowline">
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="column">
                            <%-- <table>
                                <tr>
                                    <td>
                                        <a href="ReceiveBlood.aspx">
                                            <img src="Image/Icon/number3.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="ReceiveBlood.aspx"></a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="ReceiveBlood.aspx">
                                            <img src="Image/Icon/number2.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="ReceiveBlood.aspx"></a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <%--<h3>
                                <a href="PackManually.aspx">3. Thêm</a></h3>
                            <p>
                            </p>
                            <a href="PackManually.aspx" />
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
