<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default5.aspx.cs" Inherits="Default5" Title="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here

        $(document).bind('keydown', '7', function() {
            window.location = ("Order/Order4CR.aspx");
        });

//        $(document).bind('keydown', '8', function() {
//            window.location = ("Order/Order4Org.aspx");
//        });

        $(document).bind('keydown', '9', function() {
            window.location = ("Order/SideEffects.aspx");
        });
//        $(document).bind('keydown', '4', function() {
//            window.location = ("Order/Return.aspx");
//        });

//        $(document).bind('keydown', '5', function() {
//            window.location = ("Category/DIN.aspx");
//        });

//        $(document).bind('keydown', '6', function() {
//            window.location = ("Category/SideEffect.aspx");
//        });

//        $(document).bind('keydown', '1', function() {
//            window.location = ("Category/Department.aspx");
//        });

//        $(document).bind('keydown', '2', function() {
//            window.location = ("Category/Product.aspx");
//        });

//        $(document).bind('keydown', '3', function() {
//            window.location = ("Category/BloodGroup.aspx");
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
                                        <a href="Order/Order4CR.aspx">
                                            <img src="Image/Icon/number7.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Order/Order4CR.aspx">Cấp phát cho bệnh viện</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="Order/Order4Org.aspx">
                                            <img src="Image/Icon/number8.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Order/Order4Org.aspx">Cấp phát cho tỉnh</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="Order/SideEffects.aspx">
                                            <img src="Image/Icon/number9.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Order/SideEffects.aspx">Ghi nhận phản ứng truyền máu</a>
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
                           <%-- <table>
                                <tr>
                                    <td>
                                        <a href="Order/Return.aspx">
                                            <img src="Image/Icon/number4.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Order/Return.aspx">Thu hồi</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                      <%--      <table>
                                <tr>
                                    <td>
                                        <a href="/Category/DIN.aspx">
                                            <img src="Image/Icon/number5.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Category/DIN.aspx">Tạo mã thu máu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="Category/SideEffect.aspx">
                                            <img src="Image/Icon/number6.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Category/SideEffect.aspx">Phản ứng truyền máu</a>
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
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="Category/Department.aspx">
                                            <img src="Image/Icon/number1.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Category/Department.aspx">Danh mục khoa</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="Category/Product.aspx">
                                            <img src="Image/Icon/number2.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Category/Product.aspx">Danh mục sản phẩm</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="column next">
                            <%--<table>
                                <tr>
                                    <td>
                                        <a href="Category/BloodGroup.aspx">
                                            <img src="Image/Icon/number3.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="Category/BloodGroup.aspx">Nhóm máu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
 
</asp:Content>
