<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="Default" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here
        $(document).bind('keydown', '7', function() {
            if (selectedInput) return;
            window.location = ("Default7.aspx");
        });

        $(document).bind('keydown', '8', function() {
            if (selectedInput) return;
            window.location = ("TestResult/PackTestResult.aspx");
        });

        $(document).bind('keydown', '9', function() {
            if (selectedInput) return;
            window.location = ("Default9.aspx");
        });

        $(document).bind('keydown', '4', function() {
            if (selectedInput) return;
            window.location = ("CampaignPage.aspx");
        });

        $(document).bind('keydown', '5', function() {
            if (selectedInput) return;
            window.location = ("Default5.aspx");
        });

        $(document).bind('keydown', '6', function() {
            if (selectedInput) return;
            window.location = ("Order/Order.aspx");
        });

        $(document).bind('keydown', '1', function() {
            if (selectedInput) return;
            window.location = ("Default1.aspx");
        });

        $(document).bind('keydown', '2', function() {
            if (selectedInput) return;
            window.location = ("Default2.aspx");
        });
        
    </script>

    <table width="100%">
        <tr>
            <td align="center">
                <br />
                <table id="menu1">
                    <tr>
                        <td class="column">
                            <h3>
                                <a href="Default7.aspx">7. Thu máu</a></h3>
                            <p>
                            </p>
                            <a href="Default7.aspx" />
                            <img src="Image/Icon/Heart.png" alt="" />
                        </td>
                        <td class="column next">
                            <h3>
                                <a href="TestResult/PackTestResult.aspx">8. Nhập kết quả</a></h3>
                            <p>
                            </p>
                            <a href="TestResult/PackTestResult.aspx" />
                            <img src="Image/Icon/Colba.png" alt="" />
                        </td>
                        <td class="column next">
                            <h3>
                                <a href="Default9.aspx" id="link9">9. Danh mục</a></h3>
                            <p>
                            </p>
                            <a href="Default9.aspx" />
                            <img src="Image/Icon/books-256.png" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="rowline">
                        </td>
                    </tr>
                    <tr>
                        <td class="column">
                            <h3>
                                <a href="CampaignPage.aspx">4. Đợt thu máu</a></h3>
                            <p>
                            </p>
                            <a href="CampaignPage.aspx" />
                            <img src="Image/Icon/Ambulance.png" alt="" />
                        </td>
                        <td class="column next">
                            <h3>
                                <a href="Default5.aspx">5. Sản xuất</a></h3>
                            <p>
                            </p>
                            <a href="Default5.aspx" />
                            <img src="Image/Icon/extract.png" alt="" />
                        </td>
                        <td class="column next">
                            <h3>
                                <a href="Order/Order.aspx">6. Cấp phát</a></h3>
                            <p>
                            </p>
                            <a href="Order/Order.aspx" />
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
                            <a href="Default1.aspx" />
                            <img src="Image/Icon/Hospital2Search.png" alt="" />
                        </td>
                        <td class="column next">
                            <h3>
                                <a href="Default2.aspx">2. Báo cáo</a></h3>
                            <p>
                            </p>
                            <a href="Default2.aspx" />
                            <img src="Image/Icon/Medical-invoice-information.png" alt="" />
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
