<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PeopleDonationLog.ascx.cs"
    Inherits="UserControl_PeopleHistory" %>
<asp:Label runat="server" ID="LabelTotal"></asp:Label>
<asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource1" AutoGenerateColumns="False"
    ShowHeader="false">
    <Columns>
        <asp:BoundField DataField="DIN" />
        <asp:BoundField DataField="CollectedDate" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="Note" />
    </Columns>
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" OnSelecting="LinqDataSource1_Selecting"
    EnableUpdate="true" ContextTypeName="RedBloodDataContext" TableName="Donations" OrderBy="CollectedDate desc"
    OnSelected="LinqDataSource1_Selected">
</asp:LinqDataSource>
