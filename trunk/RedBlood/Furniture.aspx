<%@ Page Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="Furniture.aspx.cs" Inherits="FurniturePage" Title="Furniture" %>


<%@ Register Src="~/UserControl/ucCatTree.ascx" TagPrefix="uc" TagName="CatTree" %>
<%@ Register Src="~/UserControl/ucFurniture.ascx" TagPrefix="uc" TagName="Fur" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
                <table border="1" width="1000">
                    <tr valign="top">
                        <td >
                            <div>
                                Mã sản phẩm
                                <br />
                                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                                <asp:Button ID="btnFind" runat="server" Text="<%$ Resources:Resource,Find %>" OnClick="btnFind_Click" />
                                <br />
                                
                                <br />
                                <uc:CatTree runat="server" ID="ucCat" />
                            </div>
                        </td>
                        <td>
                            <div style="height:340px;">
                                <asp:GridView ID="GridViewFur" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                    DataSourceID="LinqDataSourceFur" OnRowCommand="GridViewFur_RowCommand" 
                                    AllowPaging="True" PageSize="10" 
                                    onpageindexchanged="GridViewFur_PageIndexChanged" EditRowStyle-Wrap="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton SkinID="InGrid" ID="LinkButtonSelect" runat="server" CausesValidation="False"
                                                    CommandName="Select" Text="<%$ Resources:Resource,Select %>" CommandArgument='<%# Eval("ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton SkinID="InGrid" ID="LinkButton23" runat="server" CausesValidation="False"
                                                    CommandName="Delete" Text="<%$ Resources:Resource,Delete %>" ImageUrl="~/Image/Icon/Delete.png"
                                                    OnClientClick='return confirm("Xóa sản phẩm này?");' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                                        <asp:BoundField DataField="Code" HeaderText="Mã" SortExpression="Code" />
                                        <asp:BoundField DataField="SerialNumber" HeaderText="Số Serial" SortExpression="SerialNumber" />
                                        <asp:BoundField DataField="Dimension" HeaderText="Kích thước" SortExpression="Dimension" />
                                        <asp:BoundField DataField="Color" HeaderText="Màu sắc" SortExpression="Color" />
                                        <asp:BoundField DataField="Material" HeaderText="Chất liệu" SortExpression="Material" />
                                    </Columns>
                                </asp:GridView>
                                <asp:LinqDataSource ID="LinqDataSourceFur" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="Furnitures" OrderBy="ID" EnableUpdate="True" 
                                    EnableDelete="True" onselecting="LinqDataSourceFur_Selecting">
                                </asp:LinqDataSource>
                            </div>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:LinkButton ID="LinkButtonNew" runat="server" OnClick="LinkButtonNew_Click">Thêm mặc hàng Furniture</asp:LinkButton>
                        </td>
                        <td>
                            <uc:Fur runat="server" ID="ucFur1" />
                        </td>
                    </tr>
                </table>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
