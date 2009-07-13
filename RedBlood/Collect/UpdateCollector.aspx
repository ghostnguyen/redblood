<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateCollector.aspx.cs" Inherits="Collect_UpdateCollector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    Tên:
    <asp:TextBox ID="txtCollector" runat="server" Width="390"> </asp:TextBox>
    <br />
    Túi máu:
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ID" DataSourceID="LinqDataSource1">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="PeopleID" HeaderText="PeopleID" 
                SortExpression="PeopleID" />
            <asp:BoundField DataField="CollectedDate" HeaderText="CollectedDate" 
                SortExpression="CollectedDate" />
            <asp:BoundField DataField="Volume" HeaderText="Volume" 
                SortExpression="Volume" />
            <asp:BoundField DataField="HospitalID" HeaderText="HospitalID" 
                SortExpression="HospitalID" />
            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
            <asp:BoundField DataField="Autonum" HeaderText="Autonum" InsertVisible="False" 
                SortExpression="Autonum" />
            <asp:BoundField DataField="ComponentID" HeaderText="ComponentID" 
                SortExpression="ComponentID" />
            <asp:BoundField DataField="Actor" HeaderText="Actor" SortExpression="Actor" />
            <asp:BoundField DataField="CampaignID" HeaderText="CampaignID" 
                SortExpression="CampaignID" />
            <asp:BoundField DataField="SubstanceID" HeaderText="SubstanceID" 
                SortExpression="SubstanceID" />
            <asp:BoundField DataField="MSTM" HeaderText="MSTM" SortExpression="MSTM" />
            <asp:BoundField DataField="MSNH" HeaderText="MSNH" SortExpression="MSNH" />
            <asp:BoundField DataField="HIVID" HeaderText="HIVID" SortExpression="HIVID" />
            <asp:BoundField DataField="HCVID" HeaderText="HCVID" SortExpression="HCVID" />
            <asp:BoundField DataField="HBsAgID" HeaderText="HBsAgID" 
                SortExpression="HBsAgID" />
            <asp:BoundField DataField="SyphilisID" HeaderText="SyphilisID" 
                SortExpression="SyphilisID" />
            <asp:BoundField DataField="MalariaID" HeaderText="MalariaID" 
                SortExpression="MalariaID" />
            <asp:BoundField DataField="ABOID" HeaderText="ABOID" SortExpression="ABOID" />
            <asp:BoundField DataField="RhID" HeaderText="RhID" SortExpression="RhID" />
            <asp:BoundField DataField="Collector" HeaderText="Collector" 
                SortExpression="Collector" />
            <asp:BoundField DataField="ExpiredDate" HeaderText="ExpiredDate" 
                SortExpression="ExpiredDate" />
            <asp:CheckBoxField DataField="CanUpdateTestResult" 
                HeaderText="CanUpdateTestResult" ReadOnly="True" 
                SortExpression="CanUpdateTestResult" />
            <asp:BoundField DataField="Code" HeaderText="Code" ReadOnly="True" 
                SortExpression="Code" />
            <asp:BoundField DataField="DeleteNote" HeaderText="DeleteNote" ReadOnly="True" 
                SortExpression="DeleteNote" />
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="RedBloodDataContext" TableName="Packs">
    </asp:LinqDataSource>
    <br />
    
    <asp:Button ID="btnUpdate" runat="server" Text='<%$ Resources:Resource,Update %>' />
    <br />
    <br />
    
    
    
</asp:Content>
