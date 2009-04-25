<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="Org.aspx.cs" Inherits="Category_Org" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <table width="100%">
            <tr valign="top">
                <td style="width: 220px;">
                    <div class="part">
                        <div class="partHeader">
                            Tạo mới
                        </div>
                        <div class="partLinkLast">
                            <asp:LinkButton ID="btnNew" runat="server" Text="Thêm tổ chức" OnClick="btnNew_Click" />
                        </div>
                    </div>
                    <div class="part">
                        <div class="partHeader">
                            Danh sách tổ chức
                        </div>
                        <div class="partLinkLast">
                            <asp:Panel runat="server" ID="Panel1">
                                <asp:TextBox runat="server" ID="txtNameFind"></asp:TextBox>
                                <asp:Button runat="server" ID="btnFind" Text="Tìm" OnClick="btnFind_Click" />
                                <br />
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                    DataSourceID="LinqDataSourceFind" Width="100%" Style="margin-bottom: 0px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="Mã" InsertVisible="False" ReadOnly="True"
                                            SortExpression="ID" />
                                        <asp:TemplateField HeaderText="Tên">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" Text='<%# Eval("Name") %>' CommandName="Select" CommandArgument='<%# Eval("ID") %>'>
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
                        <div class="img_codabar">
                            <asp:Image ID="imgCodabar" runat="server" ImageUrl="none" />
                        </div>
                        Tên<br />
                        <asp:TextBox ID="txtName" runat="server" CssClass="org_cellvalue"></asp:TextBox>
                        <div id="divErrName" runat="server" style="visibility: hidden; height: 0px; width: 0px;
                            color: Red;">
                        </div>
                        Địa chỉ<br />
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="org_cellvalue"></asp:TextBox>
                        <br />
                        P./Xã, Q./Huyện, Tỉnh/Tp<br />
                        <asp:TextBox ID="txtGeo" runat="server" CssClass="org_cellvalue"></asp:TextBox>
                        <ajk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtGeo"
                            ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListGeo" MinimumPrefixLength="3"
                            CompletionSetCount="15" EnableCaching="true">
                        </ajk:AutoCompleteExtender>
                        <div id="divErrGeo" runat="server" style="visibility: hidden; height: 0px; width: 0px;
                            color: Red;">
                        </div>
                        Ghi chú
                        <br />
                        <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Height="50px" CssClass="org_cellvalue"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnSave" runat="server" Text='<%$ Resources:Resource,Update %>' OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Resource,Delete %>"
                            OnClick="btnDelete_Click" OnClientClick="return confirm('Xóa đơn vị này?');" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
