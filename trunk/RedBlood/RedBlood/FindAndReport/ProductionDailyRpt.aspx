<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ProductionDailyRpt.aspx.cs" Inherits="FindAndReport_ProductionDailyRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    Chọn ngày:
    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
    <ajk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
        Format="dd/MM/yyyy">
    </ajk:CalendarExtender>
    <asp:Button ID="Button1" runat="server" Text="Xem" OnClick="Button1_Click" />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Sản phẩm">
                <ItemTemplate>
                    <asp:Image ID="Image2" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("ProductCode") as string) %>' />
                    <br />
                    <asp:Label ID="TextBox2" runat="server" Text='<%# Eval("Product.Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mã gốc">
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("DIN") as string) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ghi chú">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Note") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
