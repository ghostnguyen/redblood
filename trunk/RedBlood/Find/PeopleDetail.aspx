<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFind.master" AutoEventWireup="true"
    CodeFile="PeopleDetail.aspx.cs" Inherits="Find_PeopleDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DetailsView runat="server" ID="DetailsView1" AutoGenerateRows="False" 
        DataKeyNames="ID" DataSourceID="LinqDataSource1">
        <Fields>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Codabar" HeaderText="Codabar" 
                SortExpression="Codabar" />
            <asp:BoundField DataField="CMND" HeaderText="CMND" SortExpression="CMND" />
            <asp:BoundField DataField="DOB" HeaderText="DOB" SortExpression="DOB" />
            <asp:BoundField DataField="SexID" HeaderText="SexID" SortExpression="SexID" />
            <asp:BoundField DataField="Job" HeaderText="Job" SortExpression="Job" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
            <asp:BoundField DataField="HopistalID" HeaderText="HopistalID" 
                SortExpression="HopistalID" />
            <asp:BoundField DataField="Status" HeaderText="Status" 
                SortExpression="Status" />
            <asp:BoundField DataField="ResidentAddress" HeaderText="ResidentAddress" 
                SortExpression="ResidentAddress" />
            <asp:BoundField DataField="ResidentGeoID1" HeaderText="ResidentGeoID1" 
                SortExpression="ResidentGeoID1" />
            <asp:BoundField DataField="ResidentGeoID2" HeaderText="ResidentGeoID2" 
                SortExpression="ResidentGeoID2" />
            <asp:BoundField DataField="ResidentGeoID3" HeaderText="ResidentGeoID3" 
                SortExpression="ResidentGeoID3" />
            <asp:BoundField DataField="MailingAddress" HeaderText="MailingAddress" 
                SortExpression="MailingAddress" />
            <asp:BoundField DataField="MailingGeoID1" HeaderText="MailingGeoID1" 
                SortExpression="MailingGeoID1" />
            <asp:BoundField DataField="MailingGeoID2" HeaderText="MailingGeoID2" 
                SortExpression="MailingGeoID2" />
            <asp:BoundField DataField="MailingGeoID3" HeaderText="MailingGeoID3" 
                SortExpression="MailingGeoID3" />
            <asp:CheckBoxField DataField="EnableMailingAddress" 
                HeaderText="EnableMailingAddress" SortExpression="EnableMailingAddress" />
            <asp:BoundField DataField="NameNoDiacritics" HeaderText="NameNoDiacritics" 
                SortExpression="NameNoDiacritics" />
            <asp:BoundField DataField="FullResidentalGeo" HeaderText="FullResidentalGeo" 
                ReadOnly="True" SortExpression="FullResidentalGeo" />
            <asp:BoundField DataField="FullResidentalAddress" 
                HeaderText="FullResidentalAddress" ReadOnly="True" 
                SortExpression="FullResidentalAddress" />
            <asp:BoundField DataField="FullMaillingGeo" HeaderText="FullMaillingGeo" 
                ReadOnly="True" SortExpression="FullMaillingGeo" />
            <asp:BoundField DataField="FullMailingAddress" HeaderText="FullMailingAddress" 
                ReadOnly="True" SortExpression="FullMailingAddress" />
        </Fields>
    </asp:DetailsView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="RedBloodDataContext" onselecting="LinqDataSource1_Selecting" 
        TableName="Peoples">
    </asp:LinqDataSource>
</asp:Content>
