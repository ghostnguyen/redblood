<%@ Page Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <table id="menu1">
        <tr>
            <td class="column">
                <h3>
                    <a href="/RedBlood/ReceiveBlood.aspx">Thu máu</a></h3>
                <p>
                </p>
                <a href="/RedBlood/ReceiveBlood.aspx" />
                <img src="Image/Icon/Heart.png" alt="" />
            </td>
            <td class="column next">
                <h3>
                    <a href="/RedBlood/PackManually.aspx">Nhập kết quả</a></h3>
                <p>
                </p>
                <a href="/RedBlood/PackManually.aspx" />
                <img src="Image/Icon/Colba.png" alt="" />
            </td>
            <td class="column next">
                <h3>
                    <a href="/RedBlood/PackManually.aspx">Danh mục</a></h3>
                <p>
                </p>
                <a href="/RedBlood/PackManually.aspx" />
                <img src="Image/Icon/books-256.png" alt=""  />
            </td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <div id="route">
        <div class="maincap top">
        </div>
        <div class="grid4col">
            <div class="column first">
            </div>
            <!--/column.first-->
            <div class="column">
                <h3>
                    <a href="/iphone/apps-for-iphone/">Apps for iPhone</a></h3>
                <p>
                    Discover apps from the App Store that help you do more with iPhone. From games to
                    business to health and fitness, there’s an app for that.</p>
                <a href="/iphone/apps-for-iphone/" class="more" title="Learn more about Apps for iPhone">
                    Learn more</a> <a href="/iphone/apps-for-iphone/">
                        <img src="http://images.apple.com/iphone/home/images/route-apps-20090608.jpg" width="243"
                            height="120" alt="iPhone surrounded by application icons."></a>
            </div>
            <!--/column-->
            <div class="column last">
                <h3>
                    <a href="/mobileme/">New in MobileMe</a></h3>
                <p>
                    MobileMe gives you push email, contacts, and calendars. Lose your iPhone? MobileMe
                    can help you find it and protect your privacy with Find My iPhone.</p>
                <a href="/mobileme/" class="more">Visit the MobileMe site</a> <a href="/mobileme/">
                    <img src="http://images.apple.com/iphone/home/images/route-mobileme-20090608.jpg"
                        width="243" height="120" alt="MobileMe icon." />
                </a>
            </div>
            <!--/column.last-->
        </div>
        <!--/grid4col-->
        <div class="maincap bottom">
        </div>
    </div>
    <!--/route-->
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
