<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Order.ascx.cs" Inherits="UserControl_Order" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:Panel runat="server" ID="Panel1">
    <table>
        <tr>
            <td colspan="2" align="right">
                <div class="img_codabar">
                    <asp:Image ID="imgCodabar" runat="server" ImageUrl="none" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Tên
                <asp:TextBox ID="txtName" runat="server" CssClass="campaign_cellvalue" />
                <div id="divErrName" runat="server" class="hidden" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Ngày giờ
                <asp:TextBox ID="txtDate" runat="server" CssClass="campaign_cellvalue" />
                <div id="divErrDate" runat="server" class="hidden" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Ghi chú
                <asp:TextBox ID="txtNote" runat="server" CssClass="campaign_cellvalue" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
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
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Resource,Update %>"
                    OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Resource,Delete %>"
                    OnClick="btnDelete_Click" OnClientClick="return confirm('Xóa đợt hiến máu này.');" />
            </td>
        </tr>
    </table>
</asp:Panel>
