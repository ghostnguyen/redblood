<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Store_Rpt_ReturnByDay" CodeBehind="Rpt_ReturnByDay.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <asp:Button ID="Button1" runat="server" Text="Xem" OnClick="Button1_Click" />
    <br />
    <br />
    <asp:GridView ID="GridViewSummary" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Sản phẩm" DataField="ProductCode" />
            <asp:BoundField HeaderText="Số lượng" DataField="Count" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="Ngày giờ" DataField="Date" />
            <asp:BoundField HeaderText="Tên" DataField="Actor" />
            <asp:BoundField HeaderText="Ghi chú" DataField="Note" />
            <asp:TemplateField HeaderText="Túi máu">
                <ItemTemplate>
                    <asp:DataList ID="DataList1" runat="server" DataSource='<%# Eval("Packs") %>' RepeatDirection="Horizontal"
                        RepeatColumns="1">
                        <ItemTemplate>
                            <div style="margin: 0px 10px 0px 10px;">
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN(Eval("DIN") as string) %>' />
                                <asp:Image ID="Image2" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("ProductCode") as string) %>' style="margin-left:10px;" />
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
