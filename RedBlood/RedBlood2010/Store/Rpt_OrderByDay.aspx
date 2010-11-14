<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="RedBlood.Store.Rpt_OrderByDay" CodeBehind="Rpt_OrderByDay.aspx.cs" %>

<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc" TagName="Date" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right;">
        <%= DateTime.Now.ToStringVNLong() %>
    </div>
    <div style="text-align: center;">
        <h3>
            THỐNG KÊ CẤP PHÁT
        </h3>
        Từ ngày:
        <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
        <ajk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom"
            Format="dd/MM/yyyy">
        </ajk:CalendarExtender>
        giờ
        <asp:TextBox ID="txtHourFrom" runat="server" Width="50"></asp:TextBox>
        Đến ngày:
        <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
        <ajk:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo"
            Format="dd/MM/yyyy">
        </ajk:CalendarExtender>
        <asp:TextBox ID="txtHourTo" runat="server" Width="50"></asp:TextBox>
        <asp:Button ID="btnOk2" runat="server" Text="Ok" OnClick="btnOk2_Click" />
    </div>
    <%--<%# RedBloodSystem.Url4StoreCountList %>--%>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%">
        <Columns>
            <%--<asp:TemplateField HeaderText="Mã">
                <ItemTemplate>
                    <asp:HyperLink ID="LinkButton1" runat="server" Text='<%# Eval("ProductCode") %>'
                        NavigateUrl='<%# RedBlood.RedBloodSystem.Url4StoreCountList + "ProductCode=" + Eval("ProductCode") + "&ExpiredInDays=" + ExpiredInDays.ToString() %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="ProductCode" HeaderText="Mã" />
            <asp:BoundField DataField="ProductDesc" HeaderText="Sản phẩm" />
            <asp:BoundField DataField="Total" HeaderText="TC" />
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
</asp:Content>
