<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default9.aspx.cs" Inherits="_Default" Title="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td align="center">
                <br />
                <table id="menu_lvl2">
                    <tr>
                        <td class="column">
                            <h4>
                                <a href="/RedBlood/ReceiveBlood.aspx" />
                                <img src="Image/Icon/number9.gif" alt="" />Thu máu</h4>
                            <p>
                            </p>
                        </td>
                        <td class="column next">
                           <h4>
                                <a href="/RedBlood/ReceiveBlood.aspx" />
                                <img src="Image/Icon/number8.gif" alt="" />Thu máu</h4>
                            <p>
                            </p>
                        </td>
                        <td class="column next">
                            <h4>
                                <a href="/RedBlood/ReceiveBlood.aspx" />
                                <img src="Image/Icon/number7.gif" alt="" />Thu máu</h4>
                            <p>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="rowline">
                        </td>
                    </tr>
                    <tr>
                        <td class="column">
                            <h4>
                                <a href="/RedBlood/ReceiveBlood.aspx" />
                                <img src="Image/Icon/number6.gif" alt="" />Thu máu</h4>
                            <p>
                            </p>
                        </td>
                        <td class="column next">
                            <h4>
                                <a href="/RedBlood/ReceiveBlood.aspx" />
                                <img src="Image/Icon/number5.gif" alt="" />Thu máu</h4>
                            <p>
                            </p>
                        </td>
                        <td class="column next">
                           <h4>
                                <a href="/RedBlood/ReceiveBlood.aspx" />
                                <img src="Image/Icon/number4.gif" alt="" />Thu máu</h4>
                            <p>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="rowline">
                        </td>
                    </tr>
                    <tr>
                        <td class="column">
                            <h4>
                                <a href="/RedBlood/ReceiveBlood.aspx" />
                                <img src="Image/Icon/number3.gif" alt="" />Thu máu</h4>
                            <p>
                            </p>
                        </td>
                        <td class="column next">
                           <h4>
                                <a href="/RedBlood/ReceiveBlood.aspx" />
                                <img src="Image/Icon/number2.gif" alt="" />Thu máu</h4>
                            <p>
                            </p>
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
