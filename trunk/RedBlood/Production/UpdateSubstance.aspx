<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateSubstance.aspx.cs" Inherits="Production_UpdateSubstance" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DataGrid ID="GridView1" runat="server" DataSourceID="LinqDataSource1" CellPadding="4"
        ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <Columns>
        </Columns>
    </asp:DataGrid>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
        OnSelecting="LinqDataSource1_Selecting" TableName="Packs" EnableUpdate="True">
    </asp:LinqDataSource>
</asp:Content>
