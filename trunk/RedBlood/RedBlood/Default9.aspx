﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default9.aspx.cs" Inherits="Default9" Title="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here

        $(document).bind('keydown', '7', function() {
            window.location = ("/RedBlood/Category/Geo.aspx");
        });

        $(document).bind('keydown', '8', function() {
            window.location = ("/RedBlood/Category/Sex.aspx");
        });

        $(document).bind('keydown', '9', function() {
            window.location = ("/RedBlood/Category/TestDef.aspx");
        });
        $(document).bind('keydown', '4', function() {
            window.location = ("/RedBlood/Category/Org.aspx");
        });

        $(document).bind('keydown', '5', function() {
            window.location.replace("/RedBlood/Barcode/GenerateDIN.aspx");
        });

        $(document).bind('keydown', '6', function() {
            window.location.replace("/RedBlood/Category/SideEffect.aspx");
        });

        $(document).bind('keydown', '1', function() {
            window.location.replace("/RedBlood/Category/Department.aspx");
        });

        $(document).bind('keydown', '2', function() {
            window.location.replace("/RedBlood/Category/Product.aspx");
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
                                        <a href="/RedBlood/Category/Geo.aspx">
                                            <img src="Image/Icon/number7.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Category/Geo.aspx">Địa chính</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Category/Sex.aspx">
                                            <img src="Image/Icon/number8.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Category/Sex.aspx">Giới tính</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Category/TestDef.aspx">
                                            <img src="Image/Icon/number9.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Category/TestDef.aspx">Kết quả xét nghiệm</a>
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
                                        <a href="/RedBlood/Category/Org.aspx">
                                            <img src="Image/Icon/number4.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Category/Org.aspx">Đơn vị</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Barcode/GenerateDIN.aspx">
                                            <img src="Image/Icon/number5.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Barcode/GenerateDIN.aspx">Tạo mã thu máu</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Category/SideEffect.aspx">
                                            <img src="Image/Icon/number6.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Category/SideEffect.aspx">Phản ứng truyền máu</a>
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
                                        <a href="/RedBlood/Category/Department.aspx">
                                            <img src="Image/Icon/number1.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Category/Department.aspx">Danh mục khoa</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="column next">
                            <table>
                                <tr>
                                    <td>
                                        <a href="/RedBlood/Category/Product.aspx">
                                            <img src="Image/Icon/number2.gif" alt="" />
                                        </a>
                                    </td>
                                    <td>
                                        <h4>
                                            <a href="/RedBlood/Category/Product.aspx">Danh mục sản phẩm</a>
                                        </h4>
                                    </td>
                                </tr>
                            </table>
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
