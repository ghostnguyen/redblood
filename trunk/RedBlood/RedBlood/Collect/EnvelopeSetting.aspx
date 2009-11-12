<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EnvelopeSetting.aspx.cs" Inherits="Collect_EnvelopeSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ID" DataSourceID="SqlDataSource1" 
        EmptyDataText="There are no data records to display.">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                SortExpression="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Top" HeaderText="Top" SortExpression="Top" />
            <asp:BoundField DataField="Left" HeaderText="Left" SortExpression="Left" />
            <asp:BoundField DataField="Font" HeaderText="Font" SortExpression="Font" />
            <asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:RedBlood_DBConnectionString %>" 
        DeleteCommand="DELETE FROM [EnvelopeSetting] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [EnvelopeSetting] ([Name], [Top], [Left], [Font], [Size]) VALUES (@Name, @Top, @Left, @Font, @Size)" 
        ProviderName="<%$ ConnectionStrings:RedBlood_DBConnectionString.ProviderName %>" 
        SelectCommand="SELECT [ID], [Name], [Top], [Left], [Font], [Size] FROM [EnvelopeSetting]" 
        UpdateCommand="UPDATE [EnvelopeSetting] SET [Name] = @Name, [Top] = @Top, [Left] = @Left, [Font] = @Font, [Size] = @Size WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Top" Type="String" />
            <asp:Parameter Name="Left" Type="String" />
            <asp:Parameter Name="Font" Type="String" />
            <asp:Parameter Name="Size" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Top" Type="String" />
            <asp:Parameter Name="Left" Type="String" />
            <asp:Parameter Name="Font" Type="String" />
            <asp:Parameter Name="Size" Type="String" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>



</asp:Content>

