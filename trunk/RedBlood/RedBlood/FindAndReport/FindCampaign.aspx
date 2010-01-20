<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FindCampaign.aspx.cs" Inherits="FindAndReport_FindCampaign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<%@ Register Src="~/UserControl/CampaignListByProvince.ascx" TagPrefix="uc" TagName="CampaignListByProvince" %>
<%@ Register Src="~/UserControl/PackCountByProvince.ascx" TagPrefix="uc" TagName="PackCountByProvince" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr valign="top">
            <td style="width: 210px;">
                <div class="part">
                    <div class="partHeader">
                        Tìm
                    </div>
                    <div class="partLinkLast">
                        <asp:Button runat="server" ID="btnFind" Text="Tìm" OnClick="btnFind_Click" />
                    </div>
                </div>
                <div class="part">
                    <div class="partHeader">
                        Thời gian
                    </div>
                    <div class="partLinkLast">
                        Từ ngày
                        <asp:TextBox runat="server" ID="txtFrom" Width="150px"></asp:TextBox>
                        <ajk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom"
                            Format="dd/MM/yyyy">
                        </ajk:CalendarExtender>
                        <br />
                        Tới ngày
                        <asp:TextBox runat="server" ID="txtTo" Width="150px"></asp:TextBox>
                        <ajk:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo"
                            Format="dd/MM/yyyy">
                        </ajk:CalendarExtender>
                    </div>
                </div>
                <div class="part">
                    <div class="partHeader">
                        Tỉnh/thành phố
                    </div>
                    <div class="partLinkLast">
                        <asp:CheckBoxList runat="server" DataTextField="Name" DataValueField="ID" ID="CheckBoxListGeo1">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="part">
                    <div class="partHeader">
                        Nguồn thu
                    </div>
                    <div class="partLinkLast">
                        <asp:CheckBoxList runat="server" DataTextField="Name" DataValueField="ID" ID="CheckBoxListSource">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </td>
            <td>
                <asp:ListView ID="ListView1" runat="server">
                    <LayoutTemplate>
                        <table>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <uc:CampaignListByProvince runat="server" From='<%# Eval("From") %>' To='<%# Eval("To") %>'
                                    ProvinceID='<%# Eval("ProvinceID") %>' />
                                <%--<uc:PackCountByProvince ID="PackCountByProvince1" runat="server" From='<%# Eval("From") %>'
                                    To='<%# Eval("To") %>' ProvinceID='<%# Eval("ProvinceID") %>' />--%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </td>
        </tr>
    </table>
</asp:Content>
