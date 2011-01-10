<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="RedBlood.Production.Devide" CodeBehind="Devide.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        jQuery(document).bind('keydown', 'esc', function (evt) {
            if (confirm('Exit?')) {
                window.location = ("/RedBlood/Default.aspx");
                return false;
            }
        });
    </script>
    <h3>
        Sản xuất đồng loạt
        <asp:Button ID="btnReset" runat="server" Text="Tạo đợt mới" OnClick="btnReset_Click" />
    </h3>
    <hr />
    <table>
        <tr>
            <td>
                <h4>
                    <asp:RadioButton ID="rdbProductCodeIn" runat="server" GroupName="InputBarcode" Text="Quét barcode các sản phẩm đầu vào"
                        Checked="true" />
                </h4>
                <asp:DataList ID="DataListProductIn" runat="server" RepeatDirection="Horizontal"
                    DataSourceID="LinqDataSourceProductIn">
                    <ItemTemplate>
                        <div style="margin: 0px 10px 0px 10px;">
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("Code") as string) %>' />
                            <br />
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <asp:LinqDataSource ID="LinqDataSourceProductIn" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
                    OnSelecting="LinqDataSourceProductIn_Selecting" TableName="Products">
                </asp:LinqDataSource>
            </td>
            <td style="border-left: solid 1px;">
                <h4>
                    <asp:RadioButton ID="rdbProductCodeOut" runat="server" GroupName="InputBarcode" Text="Quét barcode các sản phẩm đầu ra" />
                </h4>
                <asp:DataList ID="DataListProductOut" runat="server" RepeatDirection="Horizontal"
                    DataSourceID="LinqDataSourceProductOut">
                    <ItemTemplate>
                        <div style="margin: 0px 10px 0px 10px;">
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("Code") as string) %>' />
                            <br />
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <asp:LinqDataSource ID="LinqDataSourceProductOut" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
                    OnSelecting="LinqDataSourceProductOut_Selecting" TableName="Products">
                </asp:LinqDataSource>
                as12
                <asp:GridView ID="GridViewVolume" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Eval("Division") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txtVolume" runat="server" Text='<%# Eval("Volume") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <hr />
    <h4>
        <asp:RadioButton ID="rdbDINIn" runat="server" GroupName="InputBarcode" Text="Quét barcode túi máu cần sản xuất" />
    </h4>
    <hr />
    <asp:DataList ID="DataListDINIn" runat="server" RepeatDirection="Horizontal" DataSourceID="LinqDataSourceDINIn"
        RepeatColumns="6">
        <ItemTemplate>
            <div style="margin: 0px 10px 0px 10px;">
                <%--<asp:ImageButton BorderStyle="None" ID="ImageButton1" runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN(Eval("DIN") as string) %>'
                    OnClick="btnDINRemove_Click" CommandArgument='<%# Eval("DIN") %>' OnClientClick="return confirm('Xóa túi máu?');" />--%>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("DIN") %>'
                    OnClick="LinkButton1_Click" OnClientClick="return confirm('Xóa túi máu?');" Text='<%# Eval("DIN") %>'></asp:LinkButton>
            </div>
        </ItemTemplate>
    </asp:DataList>
    <asp:LinqDataSource ID="LinqDataSourceDINIn" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
        OnSelecting="LinqDataSourceDINIn_Selecting" TableName="Donations">
    </asp:LinqDataSource>
    <br />
    <hr />
    <asp:Button ID="btnSave" runat="server" Text='<%$ Resources:Resource,Update %>' OnClick="btnSave_Click" />
</asp:Content>
