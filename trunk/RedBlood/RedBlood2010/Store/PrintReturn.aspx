<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt.master" AutoEventWireup="true"
    Inherits="Store_PrintReturn" CodeBehind="PrintReturn.aspx.cs" %>

<%@ Register Src="~/UserControl/CampaignDetail4Rpt.ascx" TagPrefix="uc" TagName="CampaignDetail" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" style="size: landscape;">
        <tr valign="top">
            <td align="center">
                <h3>
                    <asp:Label runat="server" ID="LabelTitle1"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="LabelTitle2"></asp:Label>
                </h3>
            </td>
        </tr>
        <tr align="center">
            <td>
                <table>
                    <tr valign="top">
                        <td>
                            <div style="">
                                <asp:Image ID="imgBarcode" runat="server" ImageUrl="none" />
                            </div>
                        </td>
                        <td>
                            <table cellspacing="5">
                                <tr>
                                    <td>
                                        Ngày hủy
                                    </td>
                                    <td>
                                        <asp:Label ID="txtDate" runat="server" ReadOnly="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nhân viên
                                    </td>
                                    <td>
                                        <asp:Label ID="lblActor" runat="server" ReadOnly="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ghi chú
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNote" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tổng cộng
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LableCount"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr align="center">
            <td>
                <asp:GridView ID="GridViewSum" runat="server" AutoGenerateColumns="False" SkinID="GridViewRpt">
                    <Columns>
                        <asp:TemplateField HeaderText="Sản phẩm">
                            <ItemTemplate>
                                <asp:Image ID="ImagePackCodabar" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product( Eval("ProductCode") as string) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="TC" DataField="Sum" />
                        <asp:TemplateField HeaderText="Nhóm máu">
                            <ItemTemplate>
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("BloodGroupSumary") %>'
                                    ShowHeader="false" SkinID="Inner">
                                    <Columns>
                                        <asp:BoundField DataField="BloodGroupDesc" HeaderText="Nhóm máu" />
                                        <asp:BoundField DataField="Total" HeaderText="TC" />
                                        <asp:TemplateField HeaderText="(ml)">
                                            <ItemTemplate>
                                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("VolumeSumary") %>'
                                                    ShowHeader="false" SkinID="Inner">
                                                    <Columns>
                                                        <asp:BoundField DataField="Volume" HeaderText="(ml)" />
                                                        <asp:BoundField DataField="Total" HeaderText="TC" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:DataList ID="DataListDINIn" runat="server" RepeatDirection="Horizontal" DataSource='<%# Eval("DINList")%>'
                                                                    RepeatColumns="4">
                                                                    <ItemTemplate>
                                                                        <div style="margin: 0px 10px 0px 10px;">
                                                                            <%# Eval("DIN") %>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                <table>
                    <tr>
                        <td>
                            <div style="width: 300px; text-align: center;">
                                <%--<br />
                                <asp:Label ID="lblOrgFooter" runat="server" CssClass="campaign_cellvalue" autocomplete="off"></asp:Label>
                                <br />
                                Người nhận--%>
                            </div>
                        </td>
                        <td>
                            <div style="width: 300px; text-align: center;">
                                TpHCM, ngày
                                <%= DateTime.Now.Day %>
                                tháng
                                <%= DateTime.Now.Month %>
                                năm
                                <%= DateTime.Now.Year %>
                                <br />
                                TTTM BV Chợ Rẫy - Đơn vị sản xuất
                                <br />
                                Nhân viên
                                <br />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
