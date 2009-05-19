﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Order.ascx.cs" Inherits="UserControl_Order" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<%@ Register Src="~/UserControl/PeopleOrder.ascx" TagPrefix="uc" TagName="PeopleOrder" %>
<asp:Panel runat="server" ID="Panel1">
    <table>
        <tr valign="top">
            <td>
                <div class="img_codabar" style="width: 120px;">
                    <asp:Image ID="imgCodabar" runat="server" ImageUrl="none" />
                </div>
            </td>
            <td>
                <table>
                    <tr>
                        <td colspan="2">
                            <div>
                                <span style="float: left;">Tên&nbsp;</span>
                                <asp:TextBox ID="txtName" runat="server" Width="350" Style="float: left;" />
                                <div id="divErrName" runat="server" class="hidden" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                Ngày giờ
                                <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" Width="100" />
                                <div id="divErrDate" runat="server" class="hidden" />
                            </div>
                        </td>
                        <td>
                            <div>
                                Ghi chú
                                <asp:TextBox ID="txtNote" runat="server" Width="250" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Resource,Update %>"
                                OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Resource,Delete %>"
                                OnClick="btnDelete_Click" OnClientClick="return confirm('Xóa đợt hiến máu này.');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="dotLineBottom" colspan="2">
            </td>
        </tr>
        <tr runat="server" id="rowOrg">
            <td>
                Đơn vị nhận
            </td>
            <td>
                <asp:TextBox ID="txtOrgName" runat="server" CssClass="campaign_cellvalue" autocomplete="off" />
                <ajk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtOrgName"
                    ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListOrg" MinimumPrefixLength="3"
                    CompletionSetCount="15" EnableCaching="true">
                </ajk:AutoCompleteExtender>
                <div id="divErrOrgName" runat="server" class="hidden" />
            </td>
        </tr>
        <div runat="server" id="rowPeople">
            <tr>
                <td>
                    Người nhận
                </td>
                <td>
                    <uc:People runat="server" ID="People1" HideMoreDetail="true" />
                </td>
            </tr>
            <tr>
                <td class="dotLineBottom" colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                    Nằm viện
                </td>
                <td>
                    Khoa:&nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <br />
                    Phòng:&nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <br />
                    Giường:&nbsp;<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </div>
        <%--<tr runat="server" id="rowPeople">
            <td>
                Người nhận
            </td>
            <td>
                <uc:PeopleOrder runat="server" ID="PeopleOrder1" />
                <ajk:ModalPopupExtender ID="MPE1" runat="server" TargetControlID="LinkButtonAppPeople"
                    PopupControlID="PanelPeople" CancelControlID="btnClose" OkControlID="btnSelect"
                    BackgroundCssClass="modalBackground">
                </ajk:ModalPopupExtender>
                <asp:LinkButton ID="LinkButtonAppPeople" Text="Thêm người mới" runat="server"></asp:LinkButton>
                <asp:Panel runat="server" ID="PanelPeople" Style="display: none;" CssClass="modalPopup">
                    <uc:People runat="server" ID="People1" />
                    <div class="dotLineBottom" style="width: 100%;">
                    </div>
                    <asp:Button runat="server" ID="btnSelect" Text='<%$ Resources:Resource,Select %>' />
                    <asp:Button runat="server" ID="btnClose" Text='<%$ Resources:Resource,Close %>' />
                </asp:Panel>
            </td>
        </tr>--%>
        <tr>
            <td class="dotLineBottom" colspan="2">
            </td>
        </tr>
        <%--        <tr>
            <td class="dotLineBottom">
            </td>
            <td class="dotLineBottom">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Đơn vị nhận
                <asp:TextBox ID="txtOrgName" runat="server" CssClass="campaign_cellvalue" autocomplete="off" />
                <ajk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtOrgName"
                    ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListOrg" MinimumPrefixLength="3"
                    CompletionSetCount="15" EnableCaching="true">
                </ajk:AutoCompleteExtender>
                <div id="divErrOrgName" runat="server" class="hidden" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Danh sách túi máu
                <asp:GridView ID="GridViewPack" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSourcePack">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                            SortExpression="ID" />
                        <asp:BoundField DataField="PackID" HeaderText="PackID" SortExpression="PackID" />
                        <asp:BoundField DataField="OrderID" HeaderText="OrderID" SortExpression="OrderID" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePack" runat="server" ContextTypeName="RedBloodDataContext"
                    TableName="PackOrders">
                </asp:LinqDataSource>
            </td>
        </tr>--%>
    </table>
</asp:Panel>