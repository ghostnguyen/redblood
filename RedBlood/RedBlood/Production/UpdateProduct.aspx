<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateProduct.aspx.cs" Inherits="Production_UpdateProduct" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Sản xuất đồng loạt
    </h3>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Text="Nhập sản phẩm đầu ra" Value="1" Selected="True">
        </asp:ListItem>
        <asp:ListItem Text="Nhập mã chính" Value="2">
        </asp:ListItem>
    </asp:RadioButtonList>
    <asp:Button ID="Button1" runat="server" Text="Đợt mới" 
        onclick="Button1_Click" />
    <hr />
    <h4>
        Danh sách sản phẩm đầu ra
    </h4>
    <asp:DataList ID="DataListProduct" runat="server" RepeatDirection="Horizontal" DataSourceID="LinqDataSourceProduct">
        <ItemTemplate>
            <div style="margin: 0px 10px 0px 10px;">
                <asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("Code") as string) %>' />
                <br />
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
            </div>
        </ItemTemplate>
    </asp:DataList>
    <asp:LinqDataSource ID="LinqDataSourceProduct" runat="server" ContextTypeName="RedBloodDataContext"
        OnSelecting="LinqDataSourceProduct_Selecting" TableName="Products">
    </asp:LinqDataSource>
    <hr />
    <h4>
        Danh sách mã chính và sản phẩm đầu vào
    </h4>
    <asp:Image ID="ImageCurrentDIN" runat="server" ImageUrl="none" />
    <br />
    <asp:DataList ID="DataListPack" runat="server" DataSourceID="LinqDataSourcePack">
        <ItemTemplate>
            <asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("DIN") as string) %>' />
            <asp:Image ID="Image2" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("ProductCode") as string) %>' />
            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Note") %>'></asp:Label>
        </ItemTemplate>
    </asp:DataList>
    <asp:LinqDataSource ID="LinqDataSourcePack" runat="server" ContextTypeName="RedBloodDataContext"
        OnSelecting="LinqDataSourcePack_Selecting" TableName="Packs">
    </asp:LinqDataSource>
    <br />
    <br />
    <asp:Button ID="btnSave" runat="server" Text='<%$ Resources:Resource,Update %>' 
        onclick="btnSave_Click" />
</asp:Content>
