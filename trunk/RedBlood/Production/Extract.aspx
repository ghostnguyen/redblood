<%@ MasterType VirtualPath="~/MasterPageUserMenuWithBarcode.master" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="Extract.aspx.cs" Inherits="Production_Extract" %>

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
                <table>
                    <tr>
                        <td>
                            
                            <asp:DetailsView runat="server" ID="DetailsViewPack" AutoGenerateRows="False" DataKeyNames="ID"
                                DataSourceID="LinqDataSource1">
                                <Fields>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Status" HeaderText="Tình trạng" />
                                    <asp:BoundField DataField="CollectedDate" HeaderText="Ngày thu" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                    <asp:BoundField DataField="Volume" HeaderText="(ml)" SortExpression="Volume" />
                                    <asp:TemplateField HeaderText="Thành phần">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Component.Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ABO">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("BloodType2.ABO.Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RH">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("BloodType2.RH.Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HBsAg">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("TestResult2.HBsAg.Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HIV">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("TestResult2.HIV.Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HCV">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("TestResult2.HCV.Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Syphilis">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("TestResult2.Syphilis.Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Malaria">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("TestResult2.Malaria.Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
                                </Fields>
                            </asp:DetailsView>
                            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
                                OnSelecting="LinqDataSource1_Selecting" TableName="Packs">
                            </asp:LinqDataSource>
                        </td>
                        <td valign="top">
                            <div class="part">
                                <div class="partHeader">
                                    Sản xuất chế phẩm:
                                </div>
                                <div class="partLinkLast">
                                    <asp:GridView runat="server" ID="GridViewExtract" AutoGenerateRows="False" DataKeyNames="ID"
                                        DataSourceID="LinqDataSourceExtract" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ngày thu">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("CollectedDate","{0:dd/MM/yyyy HH:mm}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Thành phần">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Component.Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="LinqDataSourceExtract" runat="server" ContextTypeName="RedBloodDataContext"
                                        OnSelecting="LinqDataSourceExtract_Selecting" TableName="PackExtracts">
                                    </asp:LinqDataSource>
                                    <br />
                                    <asp:Button ID="btnProduct" runat="server" Text="Xác nhận sản xuất" OnClick="btnProduct_Click" />
                                    <asp:Button runat="server" ID="btnLoad" OnClick="btnLoad_Click" Style="visibility: hidden;" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
