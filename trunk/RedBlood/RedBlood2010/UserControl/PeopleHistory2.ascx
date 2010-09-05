<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControl_PeopleHistory2" Codebehind="PeopleHistory2.ascx.cs" %>
<asp:Label runat="server" ID="LabelTotal"></asp:Label>
<asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource1" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="DIN" HeaderText="DIN" />
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label ID="Label1" runat="server" Text="Sản phẩm"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Nhóm máu"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductDesc") %>'></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text='<%# Eval("BloodGroupDesc") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CollectedDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Ngày thu" />
        <asp:BoundField DataField="Volume" HeaderText="(ml)" />
        <asp:BoundField DataField="Note" HeaderText="Ghi chú" />
    </Columns>
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" OnSelecting="LinqDataSource1_Selecting"
    EnableUpdate="true" ContextTypeName="RedBlood.RedBloodDataContext" TableName="Packs" OrderBy="CollectedDate desc"
    OnSelected="LinqDataSource1_Selected">
</asp:LinqDataSource>
