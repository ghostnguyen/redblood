<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="PackErrPage.aspx.cs" Inherits="PackErrPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr valign="top">
            <td>
            </td>
            <td>
                <asp:GridView ID="GridViewEnterPackErr" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="ID" DataSourceID="LinqDataSourceEnterPackErr">
                    <Columns>
                        <asp:TemplateField HeaderText="Tên">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("People.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Autonum" HeaderText="Pack #" InsertVisible="False" />
                        <asp:BoundField DataField="CollectedDate" HeaderText="Ngày thu" />
                        <asp:BoundField DataField="Volume" HeaderText="Thể tích" />
                        <asp:TemplateField HeaderText="Đợt">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Campaign.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nguồn">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Campaign.Source.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Note" HeaderText="Ghi chú" />
                        <asp:BoundField DataField="Status" HeaderText="Tình trạng" />
                        <asp:BoundField DataField="Actor" HeaderText="Nhân viên" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourceEnterPackErr" runat="server" ContextTypeName="RedBloodDataContext"
                    OnSelecting="LinqDataSourceEnterPackErr_Selecting" TableName="Packs" EnableUpdate="true">
                </asp:LinqDataSource>
                <asp:Button ID="btnDelete" runat="server" Text="Hủy tất cả" OnClick="btnDelete_Click" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
