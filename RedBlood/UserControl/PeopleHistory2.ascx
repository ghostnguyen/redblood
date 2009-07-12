<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PeopleHistory2.ascx.cs"
    Inherits="UserControl_PeopleHistory2" %>
<asp:Label runat="server" ID="LabelTotal"></asp:Label>
<asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource1" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="Autonum" />
        <asp:BoundField DataField="CollectedDate" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("Component.Name") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Volume" />
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label ID="Label1" runat="server" Text="ABO" />
                <br />
                <asp:Label ID="lblAutonum" runat="server" Text="RH" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ABO.Name") %>' />
                <br />
                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("RH.Name") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Note" />
    </Columns>
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" OnSelecting="LinqDataSource1_Selecting"
    EnableUpdate="true" ContextTypeName="RedBloodDataContext" TableName="Packs" OrderBy="CollectedDate desc"
    OnSelected="LinqDataSource1_Selected">
</asp:LinqDataSource>
