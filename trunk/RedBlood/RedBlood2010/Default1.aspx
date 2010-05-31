<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Default1" Title="Default" Codebehind="Default1.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here

//        $(document).bind('keydown', '7', function() {
//            window.location = ("FindAndReport/FindPeople.aspx");
//        });

        $(document).bind('keydown', '8', function() {
            window.location = ("FindAndReport/FindCampaign.aspx");
        });

        $(document).bind('keydown', '9', function() {
            window.location = ("FindAndReport/FindPeople.aspx");
        });
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
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="FindAndReport/FindPeople.aspx">
                                            <img src="Image/Icon/number7.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="FindAndReport/FindPeople.aspx">Tìm theo barcode</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="FindAndReport/FindCampaign.aspx">
                                            <img src="Image/Icon/number8.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="FindAndReport/FindCampaign.aspx">Tìm đợt hiến máu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="FindAndReport/FindPeople.aspx">
                                            <img src="Image/Icon/number9.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="FindAndReport/FindPeople.aspx">Tìm người hiến máu</a>
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
                                        <a href="Category/Org.aspx">
                                            <img src="Image/Icon/number4.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Category/Org.aspx">Đơn vị</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="Codabar/Pack.aspx">
                                            <img src="Image/Icon/number5.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Codabar/Pack.aspx">Tạo mã túi máu</a>
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
   
</asp:Content>
