<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFind.master" AutoEventWireup="true"
    CodeFile="PackDetail.aspx.cs" Inherits="FindAndReport_PackDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr valign="top">
            <td>
                <asp:DetailsView runat="server" ID="DetailView1" AutoGenerateRows="False" DataKeyNames="ID"
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
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Component.Name") %>'></asp:Label>
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
            <td>
                <asp:GridView runat="server" ID="GridViewRelative" AutoGenerateRows="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSourcePackRelative" AutoGenerateColumns="false">
                    <Columns>
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
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Component.Name") %>'></asp:Label>
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
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePackRelative" runat="server" ContextTypeName="RedBloodDataContext"
                    OnSelecting="LinqDataSourcePackRelative_Selecting" TableName="Packs">
                </asp:LinqDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
