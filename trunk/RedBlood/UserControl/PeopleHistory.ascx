<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PeopleHistory.ascx.cs"
    Inherits="UserControl_PeopleHistory" %>
<asp:Label runat="server" ID="LabelTotal"></asp:Label>
<asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource1" AutoGenerateColumns="False" ShowHeader="false">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("Autonum") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("CollectedDate","{0:dd/MM/yyyy HH:mm}") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("Note") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" OnSelecting="LinqDataSource1_Selecting"
    EnableUpdate="true" ContextTypeName="RedBloodDataContext" 
    TableName="Packs" OrderBy="CollectedDate desc" 
    onselected="LinqDataSource1_Selected">
</asp:LinqDataSource>
