<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControl_CampaignListByProvince" Codebehind="CampaignListByProvince.ascx.cs" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
    DataSourceID="LinqDataSource1">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" InsertVisible="False"
            ReadOnly="True" />
        <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label ID="Label4" runat="server" Text="Ngày"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Nguồn"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text='<%# Eval("SourceName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label ID="Label4" runat="server" Text="ĐVPH"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text="ĐVTC"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Eval("CoopOrg") %>'></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text='<%# Eval("HostOrg") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Est" HeaderText="Dự kiến" SortExpression="Est" />
        <asp:TemplateField HeaderText="TC">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("PacksCount") %>'></asp:Label>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CountPack450" HeaderText="450" />
        <asp:BoundField DataField="CountPack350" HeaderText="350" />
        <asp:BoundField DataField="CountPack250" HeaderText="250" />
        <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
    </Columns>
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
    EnableUpdate="True" TableName="Campaigns" OnSelecting="LinqDataSource1_Selecting">
</asp:LinqDataSource>
