<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Category_Product" Codebehind="Product.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainContent">
        <h4>
            Danh mục sản phẩm
        </h4>
        <div id="Div1" runat="server">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
                DataSourceID="LinqDataSource1" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# BarcodeBLL.Url4Product( Eval("Code") as string) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="Ghi chú" />
                    <asp:BoundField DataField="DurationInDays" HeaderText="HSD" />
                    <asp:TemplateField HeaderText="Số lượng">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCount" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button runat="server" CommandName="Select" Text="In mã" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LabelDesc" />
                    <asp:CommandField ShowEditButton="true" />
                </Columns>
            </asp:GridView>
            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
                EnableUpdate="True" TableName="Products"
                OrderBy="Description">
            </asp:LinqDataSource>
        </div>
    </div>
</asp:Content>
