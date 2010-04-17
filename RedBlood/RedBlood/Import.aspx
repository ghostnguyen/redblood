<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Import.aspx.cs" Inherits="Import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" />
    <asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource1">
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
</asp:Content>
