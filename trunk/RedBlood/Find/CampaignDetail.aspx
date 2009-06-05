<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFind.master" AutoEventWireup="true"
    CodeFile="CampaignDetail.aspx.cs" Inherits="Find_CampaignDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DetailsView runat="server" ID="DetailView1" AutoGenerateRows="False" 
        DataKeyNames="ID" DataSourceID="LinqDataSource1">
        <Fields>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                SortExpression="ID" InsertVisible="False" />
            <asp:BoundField DataField="Name" HeaderText="Name" 
                SortExpression="Name" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Note" HeaderText="Note" 
                SortExpression="Note" />
            <asp:BoundField DataField="ContactName" HeaderText="ContactName" 
                SortExpression="ContactName" />
            <asp:BoundField DataField="ContactPhone" HeaderText="ContactPhone" 
                SortExpression="ContactPhone" />
            <asp:BoundField DataField="ContactTitle" HeaderText="ContactTitle" 
                SortExpression="ContactTitle" />
            <asp:BoundField DataField="SourceID" HeaderText="SourceID" 
                SortExpression="SourceID" />
            <asp:BoundField DataField="Est" HeaderText="Est" 
                SortExpression="Est" />
            <asp:BoundField DataField="CoopOrgID" HeaderText="CoopOrgID" 
                SortExpression="CoopOrgID" />
            <asp:BoundField DataField="HostOrgID" HeaderText="HostOrgID" 
                SortExpression="HostOrgID" />
            <asp:BoundField DataField="NameNoDiacritics" HeaderText="NameNoDiacritics" 
                SortExpression="NameNoDiacritics" />
        </Fields>
    </asp:DetailsView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="RedBloodDataContext" onselecting="LinqDataSource1_Selecting" 
        TableName="Campaigns">
    </asp:LinqDataSource>
</asp:Content>
