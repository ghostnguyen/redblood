<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt2.master" AutoEventWireup="true"
    Inherits="RedBlood.TestResult.TestResult_Rpt920" CodeBehind="Rpt920.aspx.cs" %>

<%@ Register Src="~/UserControl/CampaignDetail4Rpt.ascx" TagPrefix="uc" TagName="CampaignDetailUC" %>
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
                <uc:CampaignDetailUC runat="server" ID="CampaignDetail1" />
            </td>
        </tr>
        <tr align="center">
            <td>
                <table>
                    <tr align="left">
                        <td>
                            <asp:GridView ID="GridViewPackSum" runat="server" AutoGenerateColumns="False" SkinID="GridViewRpt">
                                <Columns>
                                    <asp:BoundField DataField="Volume" HeaderText="(ml)" />
                                    <asp:BoundField DataField="Count" HeaderText="TC" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="LinqDataSource1"
                                SkinID="GridViewRpt">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Xét nghiệm" />
                                    <asp:TemplateField HeaderText="Dương tính">
                                        <ItemTemplate>
                                            <asp:DataList ID="DataList1" runat="server" DataSource='<%# Eval("PosList") %>' RepeatDirection="Horizontal"
                                                RepeatColumns="2">
                                                <ItemTemplate>
                                                    <div style="margin: 0px 10px 20px 10px;">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN(Eval("DIN") as string) %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Không xác định">
                                        <ItemTemplate>
                                            <asp:DataList ID="DataList1" runat="server" DataSource='<%# Eval("NAList") %>' RepeatDirection="Horizontal"
                                                RepeatColumns="2">
                                                <ItemTemplate>
                                                    <div style="margin: 0px 10px 20px 10px;">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN(Eval("DIN") as string) %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
                                EnableUpdate="True" OnSelecting="LinqDataSource1_Selecting" TableName="Donations"
                                OnSelected="LinqDataSource1_Selected">
                            </asp:LinqDataSource>
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
                                <%= Resources.Resource.FooterLine1 %>
                            </div>
                        </td>
                    </tr>
                </table>
                <%-- <div runat="server" id="divNote" style="float: left; width: 450px; font-size: small;
                    text-align: left;">
                    <span style="text-decoration: underline;">Ghi chú:</span> - Trên đây là kết quả
                    xét nghiệm lần 1 từ mẫu máu của người cho máu.
                    <br />
                    <span style="text-decoration: underline;">Đề nghị:</span> - Quí cơ quan chuyên môn
                    của Tỉnh xét nghiệm lần 2 và thực hiện tiếp tục các quy dịnh theo luật Phòng chống
                    HIV/AIDS và Nghị định số 108/2007/NĐ-CP của Bộ Y tế và Chính phủ.
                </div>--%>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
