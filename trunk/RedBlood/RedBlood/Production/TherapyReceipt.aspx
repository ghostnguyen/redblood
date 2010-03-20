<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TherapyReceipt.aspx.cs" Inherits="Production_TherapyReceipt" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <table width="100%">
            <tr valign="top">
                <td style="width: 220px;">
                    <div class="part">
                        <div class="partHeader">
                            Tạo mới
                        </div>
                        <div class="partLinkLast">
                            <asp:LinkButton ID="btnNew" runat="server" Text="Thêm công thức" OnClick="btnNew_Click" />
                        </div>
                    </div>
                    <div class="part">
                        <div class="partHeader">
                            Danh sách công thức
                        </div>
                        <div class="partLinkLast">
                            <asp:Panel runat="server" ID="Panel1">
                                <asp:TextBox runat="server" ID="txtNameFind"></asp:TextBox>
                                <asp:Button runat="server" ID="btnFind" Text="Tìm" OnClick="btnFind_Click" />
                                <br />
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                    DataSourceID="LinqDataSourceFind" Width="100%" Style="margin-bottom: 0px" ShowHeader="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("ID") %>'
                                                    OnClick="btnEdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:LinqDataSource ID="LinqDataSourceFind" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="Orgs" OnSelecting="LinqDataSourceFind_Selecting">
                                </asp:LinqDataSource>
                                <br />
                                <br />
                            </asp:Panel>
                        </div>
                    </div>
                </td>
                <td style="width: 0px;">
                    <asp:Panel ID="Panel2" runat="server">
                        Tên<br />
                        <asp:TextBox ID="txtName" runat="server" CssClass="org_cellvalue"></asp:TextBox>
                        Ghi chú
                        <br />
                        <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Height="50px" CssClass="org_cellvalue"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnSave" runat="server" Text='<%$ Resources:Resource,Update %>' OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Resource,Delete %>"
                            OnClick="btnDelete_Click" OnClientClick="return confirm('Xóa công thức này?');" />
                    </asp:Panel>
                </td>
                <td>
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
                                            <asp:ImageButton BorderStyle="None" ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("Code") as string) %>'
                                                OnClick="btnProductCodeIn_Click" CommandArgument='<%# Eval("Code") %>' />
                                            <br />
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                                <asp:LinqDataSource ID="LinqDataSourceProductIn" runat="server" ContextTypeName="RedBloodDataContext"
                                    OnSelecting="LinqDataSourceProductIn_Selecting" TableName="Products">
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h4>
                                    <asp:RadioButton ID="rdbProductCodeOut" runat="server" GroupName="InputBarcode" Text="Quét barcode các sản phẩm đầu ra" />
                                </h4>
                                <asp:DataList ID="DataListProductOut" runat="server" RepeatDirection="Horizontal"
                                    DataSourceID="LinqDataSourceProductOut">
                                    <ItemTemplate>
                                        <div style="margin: 0px 10px 0px 10px;">
                                            <asp:ImageButton BorderStyle="None" ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("Code") as string) %>'
                                                OnClick="btnProductCodeOut_Click" CommandArgument='<%# Eval("Code") %>' />
                                            <br />
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                                <asp:LinqDataSource ID="LinqDataSourceProductOut" runat="server" ContextTypeName="RedBloodDataContext"
                                    OnSelecting="LinqDataSourceProductOut_Selecting" TableName="Products">
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
