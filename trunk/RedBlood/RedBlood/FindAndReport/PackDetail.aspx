<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PackDetail.aspx.cs" Inherits="FindAndReport_PackDetail" %>

<%@ Register Src="~/UserControl/PackSideEffect.ascx" TagPrefix="uc" TagName="PSE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr valign="top">
            <td>
                <asp:DetailsView runat="server" ID="DetailView1" AutoGenerateRows="False" DataKeyNames="DIN"
                    DataSourceID="LinqDataSource1">
                    <Fields>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN(Eval("DIN") as string,"00") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("People.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OrgProductDesc" HeaderText="Sản phẩm" />
                        <asp:BoundField DataField="CollectedDate" HeaderText="Ngày thu" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:BoundField DataField="OrgVolume" HeaderText="Thể tích (ml)"  />
                        <asp:BoundField DataField="BloodGroupDesc" HeaderText="Nhóm máu" />
                        <asp:BoundField DataField="TestResultStatus" HeaderText="KQXN" />
                       
                       <%-- <asp:TemplateField HeaderText="HBsAg">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("HBsAg.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HIV">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("HIV.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HCV">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("HCV.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Syphilis">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Syphilis.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Malaria">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Malaria.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
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
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ABO.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RH">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("RH.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HBsAg">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("HBsAg.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HIV">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("HIV.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HCV">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("HCV.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Syphilis">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Syphilis.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Malaria">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Malaria.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePackRelative" runat="server" ContextTypeName="RedBloodDataContext"
                    OnSelecting="LinqDataSourcePackRelative_Selecting" TableName="Packs">
                </asp:LinqDataSource>
                <br />
                <%--<uc:PSE runat="server" ID="PSE1" />--%>
            </td>
        </tr>
    </table>
</asp:Content>
