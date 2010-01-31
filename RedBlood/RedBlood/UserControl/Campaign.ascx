<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Campaign.ascx.cs" Inherits="UserControl_Campaign" %>
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
            <td class="campaign_cellheader">
                Tên
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" CssClass="campaign_cellvalue" />
                <div id="divErrName" runat="server" class="hidden" />
            </td>
        </tr>
        <tr>
            <td class="campaign_cellheader">
                SL dự kiến
            </td>
            <td>
                <asp:TextBox ID="txtEst" runat="server" CssClass="campaign_cellvalue" />
            </td>
        </tr>
        <tr>
            <td class="campaign_cellheader">
                Ngày giờ
            </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" CssClass="campaign_cellvalue" />
                <div id="divErrDate" runat="server" class="hidden" />
            </td>
        </tr>
        <tr>
            <td class="campaign_cellheader">
                Nguồn thu
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlSource" DataSourceID="LinqDataSourceSrc"
                    DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                </asp:DropDownList>
                Dài hạn
                <asp:CheckBox ID="chkInfiCam" runat="server" />
                <div id="divErrSrc" runat="server" class="hidden" />
                <asp:LinqDataSource ID="LinqDataSourceSrc" runat="server" ContextTypeName="RedBloodDataContext"
                    TableName="TestDefs" Where="ParentID = 35">
                </asp:LinqDataSource>
            </td>
            <tr>
                <td class="dotLineBottom">
                </td>
                <td class="dotLineBottom">
                </td>
            </tr>
            <tr>
                <td class="campaign_cellheader">
                    Đơn vị phối hợp
                </td>
                <td>
                    <asp:TextBox ID="txtCoopOrgName" runat="server" CssClass="campaign_cellvalue" autocomplete="off" />
                    <ajk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtCoopOrgName"
                        ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListOrg" MinimumPrefixLength="3"
                        CompletionSetCount="15" EnableCaching="true">
                    </ajk:AutoCompleteExtender>
                    <div id="divErrCoopOrgName" runat="server" class="hidden" />
                </td>
            </tr>
            <tr>
                <td class="campaign_cellheader">
                    Địa điểm tổ chức
                </td>
                <td>
                    <asp:TextBox ID="txtHostOrgName" runat="server" CssClass="campaign_cellvalue" autocomplete="off" />
                    <ajk:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtHostOrgName"
                        ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListOrg" MinimumPrefixLength="3"
                        CompletionSetCount="15" EnableCaching="true">
                    </ajk:AutoCompleteExtender>
                    <div id="divErrHostOrgName" runat="server" class="hidden" />
                </td>
            </tr>
            <tr>
                <td class="dotLineBottom">
                </td>
                <td class="dotLineBottom">
                </td>
            </tr>
            <tr>
                <td class="campaign_cellheader">
                    Người liên hệ
                </td>
                <td>
                    <asp:TextBox ID="txtContactName" runat="server" CssClass="campaign_cellvalue" />
                </td>
            </tr>
            <tr>
                <td class="campaign_cellheader">
                    Điện thoại
                </td>
                <td>
                    <asp:TextBox ID="txtContactPhone" runat="server" CssClass="campaign_cellvalue" />
                </td>
            </tr>
            <tr>
                <td class="campaign_cellheader">
                    Chức vụ
                </td>
                <td>
                    <asp:TextBox ID="txtContactTitle" runat="server" CssClass="campaign_cellvalue" />
                </td>
            </tr>
            <tr>
                <td class="campaign_cellheader">
                    Ghi chú
                </td>
                <td>
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
                    <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Resource,Update %>"
                        OnClick="btnUpdate_Click" ToolTip="Ctrl+L" />
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Resource,Delete %>"
                        OnClick="btnDelete_Click" OnClientClick="return confirm('Xóa đợt hiến máu này.');" />
                </td>
            </tr>
    </table>
</asp:Panel>
