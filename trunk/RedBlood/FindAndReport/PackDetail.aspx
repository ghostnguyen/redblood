<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFind.master" AutoEventWireup="true"
    CodeFile="PackDetail.aspx.cs" Inherits="FindAndReport_PackDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DetailsView runat="server" ID="DetailView1" AutoGenerateRows="False" 
    DataKeyNames="ID" DataSourceID="LinqDataSource1">
        <Fields>
            <asp:BoundField DataField="Code" HeaderText="Code" ReadOnly="True" 
                SortExpression="Code" />
            <asp:BoundField DataField="DeleteNote" HeaderText="DeleteNote" ReadOnly="True" 
                SortExpression="DeleteNote" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Codabar" HeaderText="Codabar" 
                SortExpression="Codabar" />
            <asp:BoundField DataField="PeopleID" HeaderText="PeopleID" 
                SortExpression="PeopleID" />
            <asp:BoundField DataField="CollectedDate" HeaderText="CollectedDate" 
                SortExpression="CollectedDate" />
            <asp:BoundField DataField="Volume" HeaderText="Volume" 
                SortExpression="Volume" />
            <asp:BoundField DataField="HospitalID" HeaderText="HospitalID" 
                SortExpression="HospitalID" />
            <asp:BoundField DataField="CampaignID" HeaderText="CampaignID" 
                SortExpression="CampaignID" />
            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
            <asp:BoundField DataField="Autonum" HeaderText="Autonum" InsertVisible="False" 
                SortExpression="Autonum" />
            <asp:BoundField DataField="SourceID" HeaderText="SourceID" 
                SortExpression="SourceID" />
            <asp:BoundField DataField="ComponentID" HeaderText="ComponentID" 
                SortExpression="ComponentID" />
            <asp:BoundField DataField="Actor" HeaderText="Actor" SortExpression="Actor" />
        </Fields>
    </asp:DetailsView>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" 
    ContextTypeName="RedBloodDataContext" onselecting="LinqDataSource1_Selecting" 
    TableName="Packs">
</asp:LinqDataSource>
</asp:Content>
