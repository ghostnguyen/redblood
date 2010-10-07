<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="_Default" Title="Default" CodeBehind="Default.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        // Your code goes here
        $(document).bind('keydown', '7', function () {
            if (selectedInput) return;
            window.location = ("Default7.aspx");
        });

        $(document).bind('keydown', '8', function () {
            if (selectedInput) return;
            window.location = ("Default8.aspx");
        });

        $(document).bind('keydown', '9', function () {
            if (selectedInput) return;
            window.location = ("Default9.aspx");
        });

        $(document).bind('keydown', '4', function () {
            if (selectedInput) return;
            window.location = ("Default4.aspx");
        });

        $(document).bind('keydown', '5', function () {
            if (selectedInput) return;
            window.location = ("Default5.aspx");
        });

        $(document).bind('keydown', '6', function () {
            if (selectedInput) return;
            window.location = ("Default6.aspx");
        });

        $(document).bind('keydown', '1', function () {
            if (selectedInput) return;
            window.location = ("Default1.aspx");
        });

        $(document).bind('keydown', '2', function () {
            if (selectedInput) return;
            window.location = ("Default2.aspx");
        });

        $(document).bind('keydown', '3', function () {
            if (selectedInput) return;
            window.location = ("Default3.aspx");
        });
        
    </script>
    <div align="center">
        <table id="menu1">
            <tr>
                <td class="column">
                    <h3>
                        <a href="Default7.aspx">7. Thu máu</a></h3>
                    <p>
                    </p>
                    <a href="Default7.aspx"></a>
                    <img src="Image/Icon/Heart.png" alt="" />
                </td>
                <td class="column next">
                    <h3>
                        <a href="Default8.aspx">8. Sản xuất</a></h3>
                    <p>
                    </p>
                    <a href="Default8.aspx"></a>
                    <img src="Image/Icon/extract.png" alt="" />
                </td>
                <td class="column next">
                    <h3>
                        <a href="Default9.aspx">9. Sàng lọc</a></h3>
                    <p>
                    </p>
                    <a href="Default9.aspx"></a>
                    <img src="Image/Icon/Colba.png" alt="" />
                </td>
            </tr>
            <tr>
                <td colspan="3" class="rowline">
                </td>
            </tr>
            <tr>
                <td class="column">
                    <h3>
                        <a href="Default4.aspx">4. Tiếp nhận</a></h3>
                    <p>
                    </p>
                    <a href="Default4.aspx" />
                    <img src="Image/Icon/Shopcart-Add_128.png" alt="" />
                </td>
                <td class="column next">
                    <h3>
                        <a href="Default5.aspx">5. Cấp phát</a></h3>
                    <p>
                    </p>
                    <a href="Default5.aspx"></a>
                    <img src="Image/Icon/Shopcart-Add_128.png" alt="" />
                </td>
                <td class="column next">
                    <h3>
                        <a href="Default6.aspx">6. Kho lưu trữ</a></h3>
                    <p>
                    </p>
                    <a href="Default6.aspx"></a>
                    <img src="Image/Icon/Shopcart-Add_128.png" alt="" />
                </td>
            </tr>
            <tr>
                <td colspan="3" class="rowline">
                </td>
            </tr>
            <tr>
                <td class="column">
                    <h3>
                        <a href="Default1.aspx">1. Tìm kiếm</a></h3>
                    <p>
                    </p>
                    <a href="Default1.aspx"></a>
                    <img src="Image/Icon/Hospital2Search.png" alt="" />
                </td>
                <td class="column next">
                    <h3>
                        <a href="Default2.aspx">2. Báo cáo</a></h3>
                    <p>
                    </p>
                    <a href="Default2.aspx"></a>
                    <img src="Image/Icon/Medical-invoice-information.png" alt="" />
                </td>
                <td class="column next">
                    <h3>
                        <a href="Default3.aspx" id="link9">3. Danh mục</a></h3>
                    <p>
                    </p>
                    <a href="Default3.aspx"></a>
                    <img src="Image/Icon/books-256.png" alt="" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
