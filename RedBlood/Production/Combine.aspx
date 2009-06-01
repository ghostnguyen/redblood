<%@ MasterType VirtualPath="~/MasterPageUserMenuWithBarcode.master" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="Combine.aspx.cs" Inherits="Production_Combine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <asp:GridView runat="server" ID="GridViewPackIn" AutoGenerateColumns="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSourcePackIn">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CollectedDate" HeaderText="Ngày thu" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:BoundField DataField="Volume" HeaderText="Volume" SortExpression="Volume" />
                        <asp:TemplateField HeaderText="Thành phần">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Component.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePackIn" runat="server" ContextTypeName="RedBloodDataContext"
                    OnSelecting="LinqDataSourcePackIn_Selecting" TableName="Packs">
                </asp:LinqDataSource>
            </td>
            <td>
                <asp:GridView runat="server" ID="GridViewPackOut" AutoGenerateColumns="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSourcePackOut">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CollectedDate" HeaderText="Ngày thu" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:BoundField DataField="Volume" HeaderText="Volume" SortExpression="Volume" />
                        <asp:TemplateField HeaderText="Thành phần">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Component.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePackOut" runat="server" ContextTypeName="RedBloodDataContext"
                    OnSelecting="LinqDataSourcePackOut_Selecting" TableName="Packs">
                </asp:LinqDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                Sản xuất tiểu cầu
                <br />
                <asp:Button runat="server" ID="btnOk" Text="Xác nhận" OnClick="btnOk_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
