<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PrintSetting.aspx.cs" Inherits="Collect_PrintSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
        AutoPostBack="true">
    </asp:DropDownList>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource1" AutoGenerateColumns="False"
        DataKeyNames="ID">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" ReadOnly="true" />
            <asp:BoundField DataField="Top" HeaderText="Top" SortExpression="Top" />
            <asp:BoundField DataField="Left" HeaderText="Left" SortExpression="Left" />
            <asp:BoundField DataField="Font" HeaderText="Font" SortExpression="Font" />
            <asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size" />
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
        TableName="PrintSettings" EnableUpdate="True" OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
    <br />
    Size: xx-large, x-large, larger, large, medium, small, smaller, x-small, xx-small
</asp:Content>
