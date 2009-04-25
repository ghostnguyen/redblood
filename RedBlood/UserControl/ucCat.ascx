<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCat.ascx.cs" Inherits="UserControl_Cat" %>

<div>
<asp:DropDownList ID="DropDownListCat1" runat="server" DataSourceID="LinqDataSourceCat1"
    DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DropDownListCat1_SelectedIndexChanged"
    AutoPostBack="True" AppendDataBoundItems="true">
    <asp:ListItem Text="--Tất cả--" Value="00000000-0000-0000-0000-000000000000"></asp:ListItem>
</asp:DropDownList>
<asp:LinqDataSource ID="LinqDataSourceCat1" runat="server" ContextTypeName="RedBloodDataContext"
    OrderBy="Name" TableName="Cats" Where="Level == @Level">
    <WhereParameters>
        <asp:Parameter DefaultValue="1" Name="Level" Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>
<br />
<asp:DropDownList ID="DropDownListCat2" runat="server" DataSourceID="LinqDataSourceCat2"
    DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DropDownListCat2_SelectedIndexChanged"
    AutoPostBack="True" AppendDataBoundItems="true">
    <asp:ListItem Text="--Tất cả--" Value="00000000-0000-0000-0000-000000000000"></asp:ListItem>
</asp:DropDownList>
<asp:LinqDataSource ID="LinqDataSourceCat2" runat="server" ContextTypeName="RedBloodDataContext"
    OrderBy="Name" TableName="Cats" Where="Level == @Level and ParentID == Guid(@ParentID)">
    <WhereParameters>
        <asp:Parameter DefaultValue="2" Name="Level" Type="Int32" />
        <asp:ControlParameter ControlID="DropDownListCat1" Name="ParentID" PropertyName="SelectedValue"
            Type="Object"></asp:ControlParameter>
    </WhereParameters>
</asp:LinqDataSource>
<br />
<asp:DropDownList ID="DropDownListCat3" runat="server" DataSourceID="LinqDataSourceCat3"
    DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DropDownListCat3_SelectedIndexChanged"
    AutoPostBack="True" AppendDataBoundItems="true">
    <asp:ListItem Text="--Tất cả--" Value="00000000-0000-0000-0000-000000000000"></asp:ListItem>
</asp:DropDownList>
<asp:LinqDataSource ID="LinqDataSourceCat3" runat="server" ContextTypeName="RedBloodDataContext"
    OrderBy="Name" TableName="Cats" Where="Level == @Level and ParentID == Guid(@ParentID)">
    <WhereParameters>
        <asp:Parameter DefaultValue="3" Name="Level" Type="Int32" />
        <asp:ControlParameter ControlID="DropDownListCat2" Name="ParentID" PropertyName="SelectedValue"
            Type="Object"></asp:ControlParameter>
    </WhereParameters>
</asp:LinqDataSource>
<br />
<asp:DropDownList ID="DropDownListCat4" runat="server" DataSourceID="LinqDataSourceCat4"
    DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DropDownListCat4_SelectedIndexChanged"
    AutoPostBack="True" AppendDataBoundItems="true">
    <asp:ListItem Text="--Tất cả--" Value="00000000-0000-0000-0000-000000000000"></asp:ListItem>
</asp:DropDownList>
<asp:LinqDataSource ID="LinqDataSourceCat4" runat="server" ContextTypeName="RedBloodDataContext"
    OrderBy="Name" TableName="Cats" Where="Level == @Level and ParentID == Guid(@ParentID)">
    <WhereParameters>
        <asp:Parameter DefaultValue="4" Name="Level" Type="Int32" />
        <asp:ControlParameter ControlID="DropDownListCat3" Name="ParentID" PropertyName="SelectedValue"
            Type="Object"></asp:ControlParameter>
    </WhereParameters>
</asp:LinqDataSource>
<br />
<asp:DropDownList ID="DropDownListCat5" runat="server" DataSourceID="LinqDataSourceCat5"
    DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DropDownListCat5_SelectedIndexChanged"
    AutoPostBack="True" AppendDataBoundItems="true">
    <asp:ListItem Text="--Tất cả--" Value="00000000-0000-0000-0000-000000000000"></asp:ListItem>
</asp:DropDownList>
<asp:LinqDataSource ID="LinqDataSourceCat5" runat="server" ContextTypeName="RedBloodDataContext"
    OrderBy="Name" TableName="Cats" Where="Level == @Level and ParentID == Guid(@ParentID)">
    <WhereParameters>
        <asp:Parameter DefaultValue="5" Name="Level" Type="Int32" />
        <asp:ControlParameter ControlID="DropDownListCat4" Name="ParentID" PropertyName="SelectedValue"
            Type="Object"></asp:ControlParameter>
    </WhereParameters>
</asp:LinqDataSource>
</div>