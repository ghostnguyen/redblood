﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Order.ascx.cs" Inherits="UserControl_Order" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:Panel runat="server" ID="Panel1">
    <table>
        <tr valign="top">
            <td>
                <div class="img_codabar">
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
        <tr>
            <asp:Panel runat="server" ID="PanelOrg">
                <td>
                    Đơn vị nhận
                </td>
                <td>
                    <ajk:ModalPopupExtender ID="MPE1" runat="server" TargetControlID="LinkButton1" PopupControlID="Panel12"
                        CancelControlID="Button12" OkControlID="Button14" DropShadow="true">
                    </ajk:ModalPopupExtender>
                    <asp:LinkButton ID="LinkButton1" Text="Popup" runat="server"></asp:LinkButton>
                    <asp:Panel runat="server" ID="Panel12" style="display:none;">
                        <asp:TextBox ID="txtOrgName" runat="server" CssClass="campaign_cellvalue" autocomplete="off" />
                        <ajk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtOrgName"
                            ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListOrg" MinimumPrefixLength="3"
                            CompletionSetCount="15" EnableCaching="true">
                        </ajk:AutoCompleteExtender>
                        <div id="divErrOrgName" runat="server" class="hidden" />
                        <asp:Button runat="server" ID="Button12" Text="Cancel" />
                        <asp:Button runat="server" ID="Button14" Text="Ok" />
                    </asp:Panel>
                </td>
            </asp:Panel>
        </tr>
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
