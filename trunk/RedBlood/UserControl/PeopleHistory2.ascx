<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PeopleHistory2.ascx.cs"
    Inherits="UserControl_PeopleHistory2" %>
<asp:Label runat="server" ID="LabelTotal"></asp:Label>
<asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource1" AutoGenerateColumns="False">
    <Columns>
        <asp:TemplateField HeaderText="Túi máu">
            <ItemTemplate>
                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("Autonum") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ngày thu">
            <ItemTemplate>
                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("CollectedDate","{0:dd/MM/yyyy HH:mm}") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label ID="Label1" runat="server" Text="Thành phần" />
                <br />
                <asp:Label ID="lblAutonum" runat="server" Text="(ml)"/>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Component.Name") %>' />
                <br />
                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("Volume") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label ID="Label1" runat="server" Text="ABO" />
                <br />
                <asp:Label ID="lblAutonum" runat="server" Text="RH"/>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("BloodType2.ABO.Name") %>' />
                <br />
                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("BloodType2.RH.Name") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ghi chú">
            <ItemTemplate>
                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("Note") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" OnSelecting="LinqDataSource1_Selecting"
    EnableUpdate="true" ContextTypeName="RedBloodDataContext" TableName="Packs" OrderBy="CollectedDate desc"
    OnSelected="LinqDataSource1_Selected">
</asp:LinqDataSource>
