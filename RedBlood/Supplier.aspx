<%@ Page Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="Supplier.aspx.cs" Inherits="SupplierPage" Title="Nhà cung cấp" %>

<%@ Register Src="~/UserControl/ucSupplierEditAccount.ascx" TagPrefix="ucSupplier"
    TagName="EditAccount" %>
<%@ Register Src="~/UserControl/ucSupplierGeneralInfo.ascx" TagPrefix="ucSupplier"
    TagName="GeneralInfo" %>
<%@ Register Src="~/UserControl/ucSupplierEditLocation.ascx" TagPrefix="ucSupplier"
    TagName="EditLocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divSupplierList" style="float: left; margin-left: 10px;">
                <asp:LinkButton ID="LinkButtonNew" runat="server" OnClick="LinkButtonNew_Click">Tạo nhà cung cấp mới.</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonDelete" runat="server" OnClientClick='return confirm("Xóa nhà cung cấp đang chọn?");'
                    OnClick="LinkButtonDelete_Click">Xóa nhà cấp đang chọn.</asp:LinkButton>
                <br />
                <br />
                Tìm nhà cung cấp:
                <br />
                <asp:TextBox ID="txtFind" runat="server"></asp:TextBox>
                <asp:Button ID="btnFind" runat="server" Text="Tìm" OnClick="btnFind_Click" />
                <br />
                <br />
                <asp:GridView ID="GridViewSupplierList" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="ID" DataSourceID="LinqDataSourceSupplierList" OnRowCommand="GridViewSupplierList_RowCommand"
                    ShowHeader="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" CommandArgument='<%# Eval("ID") %>'><%# Eval("Name") %></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourceSupplierList" runat="server" ContextTypeName="RedBloodDataContext"
                    TableName="Suppliers" Where="Name.Contains(@Name) and CompanyID == Guid(@CompanyID)" EnableUpdate="True" OrderBy="Name">
                    <WhereParameters>
                        <asp:ControlParameter ControlID="txtFind" Name="Name" PropertyName="Text" Type="String"
                            ConvertEmptyStringToNull="false" />
                        <asp:ControlParameter ControlID="txtCompanyID" Name="CompanyID" PropertyName="Text" Type="String"
                            DefaultValue="{00000000-0000-0000-0000-000000000000}" />
                    </WhereParameters>
                </asp:LinqDataSource>
                <br />
            </div>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal">
                <div id="divSupplier" style="float: left; margin-left: 10px;" runat="server">
                    <h2>
                        <asp:DetailsView ID="DetailsViewTitle" runat="server" AutoGenerateRows="False" DataKeyNames="ID"
                            SkinID="DetailViewBlank" DataSourceID="LinqDataSourceSupplier">
                            <Fields>
                                <asp:BoundField DataField="Name" ShowHeader="False" ControlStyle-Width="500px">
                                    <ControlStyle Width="500px" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                            </Fields>
                        </asp:DetailsView>
                        <asp:LinqDataSource ID="LinqDataSourceSupplier" runat="server" ContextTypeName="RedBloodDataContext"
                            TableName="Suppliers" Where="ID == Guid?(@ID)">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="GridViewSupplierList" Name="ID" PropertyName="SelectedValue"
                                    Type="Object" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </h2>
                    <asp:Menu ID="MenuTab" runat="server" Orientation="Horizontal" SkinID="menuTab" OnMenuItemClick="MenuTab_MenuItemClick">
                        <Items>
                            <asp:MenuItem Text="Thông tin chung" Value="GenaralInfo" Selected="true"></asp:MenuItem>
                            <asp:MenuItem Text="Thay đổi tài khoản" Value="EditAccount"></asp:MenuItem>
                            <asp:MenuItem Text="Thay đổi chi nhánh" Value="EditLocation"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                    <br />
                    <div runat="server" id="divGeneralInfo" style="float: left;" visible="true">
                        <ucSupplier:GeneralInfo runat="server" ID="ucGeneralInfo" />
                    </div>
                    <div runat="server" id="divEditAccount" style="float: left;" visible="false">
                        <ucSupplier:EditAccount runat="server" ID="ucEditAccount" />
                    </div>
                    <div runat="server" id="divEditLocation" style="float: left;" visible="false">
                        <ucSupplier:EditLocation runat="server" ID="ucEditLocation" />
                    </div>
                    <div style="height: 30px; width: 30px; float: left; clear: both;">
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
