<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt.master" AutoEventWireup="true"
    Inherits="Store_PrintOrder" CodeBehind="PrintOrder.aspx.cs" %>

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
                            <div class="img_codabar" style="width: 140px;">
                                <asp:Image ID="imgOrder" runat="server" ImageUrl="none" />
                            </div>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Ngày cấp
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" Width="100" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ghi chú
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNote" runat="server" Width="180" />
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
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        Đơn vị nhận
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOrg" runat="server" CssClass="campaign_cellvalue" autocomplete="off" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ghi chú truyền máu
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTransfusionNote" Width="299" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="dotLineBottom" colspan="2">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr align="center">
            <td>
                <table>
                    <tr>
                        <td>
                            Tổng cộng<h3>
                            </h3>
                            <asp:GridView ID="GridViewSum" runat="server" AutoGenerateColumns="False">
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
                                                                    <asp:BoundField DataField="DINList" HeaderText="DINList" ItemStyle-Wrap="true" ControlStyle-Width="600px"
                                                                        ItemStyle-Width="200" />
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
                            <br />
                            <asp:GridView ID="GridViewPack" runat="server" AutoGenerateColumns="False" DataKeyNames="ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="Túi máu">
                                        <ItemTemplate>
                                            <%--<asp:Image ID="ImageDIN" runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN( Eval("Pack.DIN") as string) %>' />--%>
                                            <%# Eval("Pack.DIN")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sản phẩm">
                                        <ItemTemplate>
                                            <%--<asp:Image ID="ImagePackCodabar" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product( Eval("Pack.ProductCode") as string) %>' />--%>
                                            <%# Eval("Pack.ProductCode")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nhóm máu" ItemStyle-Font-Size="Larger" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("Pack.Donation.BloodGroupDesc")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr align="right">
                        <td>
                            <asp:Label runat="server" ID="LableCount"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <div style="width: 300px; text-align: center;">
                                <br />
                                <br />
                                TpHCM, ngày
                                <%= DateTime.Now.Day %>
                                tháng
                                <%= DateTime.Now.Month %>
                                năm
                                <%= DateTime.Now.Year %>
                                <br />
                                <%= Resources.Resource.HdrLine4 %>
                                <br />
                                <%= Resources.Resource.FooterLine1 %>
                                <br />
                                <img src="../Image/chuky.png" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
