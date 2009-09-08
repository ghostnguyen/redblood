﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt.master" AutoEventWireup="true"
    CodeFile="Rpt_Campaign.aspx.cs" Inherits="Collect_Rpt_Campaign" %>

<%@ Register Src="~/UserControl/CampaignDetail4Rpt.ascx" TagPrefix="uc" TagName="CampaignDetail" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" style="size: landscape;">
        <tr>
            <td>
                <div style="width: 100%; clear: both;">
                    <div style="float: left; text-align: center;">
                        <%= Resources.Resource.HdrLine3 %>
                        <br />
                        <%= Resources.Resource.HdrLine4 %>
                    </div>
                    <div style="float: right; text-align: center;">
                        <%= Resources.Resource.HdrLine1 %>
                        <br />
                        <%= Resources.Resource.HdrLine2 %>
                        <br />
                        ------o0o------
                    </div>
                </div>
            </td>
        </tr>
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
                                    <asp:TemplateField HeaderText="CMND">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("People.CMND") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Họ & Tên">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("People.Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ngày sinh">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("People.DOB","{0:dd/MM/yyyy}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Địa chỉ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("People.ResidentAddress") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phường/xã">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGeo3" runat="server" Text='<%# Eval("People.ResidentGeo3.Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quận/huyện">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGeo2" runat="server" Text='<%# Eval("People.ResidentGeo2.Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tỉnh/thành">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGeo2" runat="server" Text='<%# Eval("People.ResidentGeo1.Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nhóm máu">
                                        <ItemTemplate>
                                            <asp:Label ID="lblABO" runat="server" Text='<%# Eval("BloodGroupDesc") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Thu máu">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrgProductDesc" runat="server" Text='<%# Eval("OrgProductDesc") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Thể tích">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrgVolume" runat="server" Text='<%# Eval("OrgVolume") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Ghi chú">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrgVolume" runat="server" Text='<%# Eval("Note") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
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
                                Khoa Huyết học - Truyền máu
                                <br />
                                Trưởng khoa
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
