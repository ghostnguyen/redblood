<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Category_PrintSetting" Codebehind="PrintSetting.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
        AutoPostBack="true">
    </asp:DropDownList>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource1" AutoGenerateColumns="False"
        DataKeyNames="ID">
        <Columns>
            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" ReadOnly="true"
                ControlStyle-Width="150" ItemStyle-Width="150" />
            <asp:BoundField DataField="Top" HeaderText="Top" SortExpression="Top" ControlStyle-Width="50"
                ItemStyle-Width="50" />
            <asp:BoundField DataField="Left" HeaderText="Left" SortExpression="Left" ControlStyle-Width="50"
                ItemStyle-Width="50" />
            <asp:BoundField DataField="Font" HeaderText="Font" SortExpression="Font" ControlStyle-Width="80"
                ItemStyle-Width="80" />
            <asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size" ControlStyle-Width="80"
                ItemStyle-Width="80" />
            <asp:BoundField DataField="Height" HeaderText="Height" SortExpression="Height" ControlStyle-Width="50"
                ItemStyle-Width="50" />
            <asp:BoundField DataField="Width" HeaderText="Width" SortExpression="Width" ControlStyle-Width="50"
                ItemStyle-Width="50" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
        TableName="PrintSettings" EnableUpdate="True" OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
    <br />
    Size: xx-large, x-large, larger, large, medium, small, smaller, x-small, xx-small
</asp:Content>
