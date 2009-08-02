<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CollectPack.aspx.cs" Inherits="Collect_CollectPack" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    Tên KTV:
    <asp:TextBox ID="txtCollector" runat="server" Width="390"> </asp:TextBox>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="DIN"
        DataSourceID="LinqDataSource1">
        <Columns>
            <asp:BoundField DataField="DIN" InsertVisible="False" SortExpression="DIN" />
            <asp:TemplateField HeaderText="Tên">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("People.Name") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CollectedDate" HeaderText="Ngày thu" SortExpression="CollectedDate"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="Collector" HeaderText="KTV thu máu" SortExpression="Collector" />
            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
        TableName="Donations" OnSelecting="LinqDataSource1_Selecting" EnableUpdate="True">
    </asp:LinqDataSource>
    <br />
    <asp:Button ID="btnUpdate" runat="server" Text='<%$ Resources:Resource,Update %>'
        OnClick="btnUpdate_Click" />
    <asp:Button ID="btnClear" runat="server" Text="Xóa danh sách" OnClick="btnClear_Click"
        BorderStyle="Solid" />
    <br />
    <br />
</asp:Content>
