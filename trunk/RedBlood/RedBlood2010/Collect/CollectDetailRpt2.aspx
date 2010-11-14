<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt.master" AutoEventWireup="true"
    Inherits="Collect_CollectDetailRpt2" CodeBehind="CollectDetailRpt2.aspx.cs" %>

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
                                DataSourceID="LinqDataSource1" SkinID="GridViewRpt" Font-Size="Smaller">
                                <Columns>
                                    <asp:BoundField DataField="DIN" HeaderText="Túi máu" />
                                    <asp:BoundField DataField="ProductCode" HeaderText="Sản phẩm" />
                                    <asp:BoundField DataField="CMND" HeaderText="CMND" />
                                    <asp:BoundField DataField="Name" HeaderText="Họ & Tên" />
                                    <asp:BoundField DataField="DOB" HeaderText="Ngày sinh" />
                                    <asp:BoundField DataField="BloodGroupDesc" HeaderText="Nhóm máu" />
                                    <asp:BoundField DataField="OrgVolume" HeaderText="Thể tích" />
                                    <asp:BoundField DataField="ResidentAddress" HeaderText="Địa chỉ" />
                                    <asp:BoundField DataField="ResidentGeo3Name" HeaderText="Phường/xã" />
                                    <asp:BoundField DataField="ResidentGeo2Name" HeaderText="Quận/huyện" />
                                    <asp:BoundField DataField="ResidentGeo1Name" HeaderText="Tỉnh/thành" />
                                    <asp:BoundField DataField="Collector" HeaderText="KTV" />
                                    <asp:BoundField DataField="Note" HeaderText="Ghi chú" />
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
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
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
