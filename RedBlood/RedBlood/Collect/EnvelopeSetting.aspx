<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EnvelopeSetting.aspx.cs" Inherits="Collect_EnvelopeSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
    
    <asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource1"></asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server">
    </asp:LinqDataSource>
</asp:Content>
