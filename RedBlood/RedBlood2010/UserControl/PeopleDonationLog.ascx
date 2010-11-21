<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControl_PeopleDonationLog" Codebehind="PeopleDonationLog.ascx.cs" %>
<asp:Label runat="server" ID="LabelTotal"></asp:Label>
<asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource1" AutoGenerateColumns="False"
    ShowHeader="false">
    <Columns>
        <asp:BoundField DataField="CollectedDate"/>
        <asp:BoundField DataField="DIN" />
        <asp:BoundField DataField="Note" />
    </Columns>
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" OnSelecting="LinqDataSource1_Selecting"
    EnableUpdate="true" ContextTypeName="RedBlood.RedBloodDataContext" TableName="Donations"
    OnSelected="LinqDataSource1_Selected">
</asp:LinqDataSource>
