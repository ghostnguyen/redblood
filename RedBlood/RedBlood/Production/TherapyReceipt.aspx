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
                                    DataSourceID="LinqDataSourceFind" Width="100%" Style="margin-bottom: 0px" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tên">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" Text='<%# Eval("Name") %>' 
                                                    CommandArgument='<%# Eval("ID") %>' onclick="btnEdit_Click" >
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
                <td>
                    <asp:Panel ID="Panel2" runat="server">
                        Tên<br />
                        <asp:TextBox ID="txtName" runat="server" CssClass="org_cellvalue"></asp:TextBox>
                        <div id="divErrName" runat="server" style="visibility: hidden; height: 0px; width: 0px;
                            color: Red;">
                        </div>
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
            </tr>
        </table>
    </div>
</asp:Content>
