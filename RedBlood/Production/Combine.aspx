<%@ MasterType VirtualPath="~/MasterPageUserMenuWithBarcode.master" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="Combine.aspx.cs" Inherits="Production_Combine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here
        function doLoadPackCombined() {
            if (confirm("Túi máu đã sản xuất. Xem chi tiết?")) {
                $("input[id*='btnLoad']").click();
            }
        }
        
    </script>

    <table width="100%">
        <tr align="center">
            <td>
                <table cellspacing="1" border="1">
                    <tr align="center">
                        <td>
                            Đầu vào
                        </td>
                        <td>
                            Đầu ra
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView runat="server" ID="GridViewPackIn" AutoGenerateColumns="False" DataKeyNames="Autonum"
                                DataSourceID="LinqDataSourcePackIn" OnRowDeleting="GridViewPackIn_RowDeleting">
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
                                    <asp:TemplateField HeaderText="Tình trạng">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText='<%$ Resources:Resource,Delete %>' ShowDeleteButton="true" />
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
                                    <asp:TemplateField HeaderText="Tình trạng">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="LinqDataSourcePackOut" runat="server" ContextTypeName="RedBloodDataContext"
                                OnSelecting="LinqDataSourcePackOut_Selecting" TableName="Packs">
                            </asp:LinqDataSource>
                            Ghi chú:
                            <asp:TextBox runat="server" ID="txtNote"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <br />
                            Sản xuất tiểu cầu
                            <br />
                            <br />
                            <asp:Button runat="server" ID="btnOk" Text="Xác nhận" OnClick="btnOk_Click" />
                            <asp:Button runat="server" ID="btnLoad" OnClick="btnLoad_Click" Style="visibility: hidden;">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
