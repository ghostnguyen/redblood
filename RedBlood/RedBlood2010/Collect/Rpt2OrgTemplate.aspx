<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt.master" AutoEventWireup="true"
    Inherits="Collect_Rpt2OrgTemplate" CodeBehind="Rpt2OrgTemplate.aspx.cs" %>

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
                <uc:CampaignDetail runat="server" ID="CampaignDetail1" />
            </td>
        </tr>
        <tr align="center">
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="DIN"
                                DataSourceID="LinqDataSource1" SkinID="GridViewRpt" Font-Size="Smaller" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="DIN" HeaderText="Túi máu" />
                                    <asp:BoundField DataField="CMND" HeaderText="CMND" />
                                    <asp:BoundField DataField="Name" HeaderText="Họ & Tên" />
                                    <asp:BoundField DataField="DOB" HeaderText="Ngày sinh" />
                                    <asp:BoundField DataField="OrgVolume" HeaderText="Vol" />
                                    <asp:TemplateField HeaderText="ABO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblABO" runat="server" Text='<%# Eval("BloodGroupDesc") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HIV">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHIV" runat="server" Text='<%# Eval("HIV") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HCV">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHCV" runat="server" Text='<%# Eval("HCV_Ab") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HBsAg">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHBsAg" runat="server" Text='<%# Eval("HBs_Ag") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Giang Mai">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSyphilis" runat="server" Text='<%# Eval("Syphilis") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="KSTSR">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMalaria" runat="server" Text='<%# Eval("Malaria") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ResidentAddress" HeaderText="Địa chỉ" />
                                    <asp:BoundField DataField="Geo3Name" HeaderText="Phường/xã" />
                                    <asp:BoundField DataField="Geo2Name" HeaderText="Quận/huyện" />
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
                                EnableUpdate="True" OnSelecting="LinqDataSource1_Selecting" TableName="Donations"
                                OrderBy="DIN" EnableDelete="True" OnSelected="LinqDataSource1_Selected">
                            </asp:LinqDataSource>
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
                <div runat="server" id="divNote" style="float: left; width: 450px; font-size: small;
                    text-align: left;">
                    <span style="text-decoration: underline;">Ghi chú:</span> - Trên đây là kết quả
                    xét nghiệm lần 1 từ mẫu máu của người cho máu.
                    <br />
                    <span style="text-decoration: underline;">Đề nghị:</span> - Quí cơ quan chuyên môn
                    của Tỉnh xét nghiệm lần 2 và thực hiện tiếp tục các quy dịnh theo luật Phòng chống
                    HIV/AIDS và Nghị định số 108/2007/NĐ-CP của Bộ Y tế và Chính phủ.
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        //XN lần 2
        $("span:contains('Chưa xác định'),span:contains('Dương tính'),span:contains('XN lần 2')").css('font-weight', 'bolder').css('color', 'red');

    </script>
</asp:Content>
