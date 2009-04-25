<%@ Page Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="Warehouse.aspx.cs" Inherits="WarehousePage" Title="Kho hàng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divWarehouseList" style="float: left; margin-left: 10px;">
                <asp:LinkButton ID="LinkButtonNew" runat="server" OnClick="LinkButtonNew_Click">Tạo kho hàng mới.</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonDelete" runat="server" OnClientClick='return confirm("Xóa kho hàng đang chọn?");'
                    OnClick="LinkButtonDelete_Click">Xóa kho hàng đang chọn.</asp:LinkButton>
                <br />
                <br />
                Tìm kho hàng:
                <br />
                <asp:TextBox ID="txtFind" runat="server"></asp:TextBox>
                <asp:Button ID="btnFind" runat="server" Text="Tìm" OnClick="btnFind_Click" />
                <br />
                <br />
                <br />
                <asp:GridView ID="GridViewWarehouseList" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="ID" DataSourceID="LinqDataSourceWarehouseList" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" CommandArgument='<%# Eval("ID") %>'>
                                <%# Eval("Name") %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourceWarehouseList" runat="server" ContextTypeName="RedBloodDataContext"
                    TableName="Warehouses" Where="Name.Contains(@Name) and CompanyID == Guid(@CompanyID)"
                    EnableUpdate="True">
                    <WhereParameters>
                        <asp:ControlParameter ControlID="txtFind" Name="Name" PropertyName="Text" Type="String"
                            ConvertEmptyStringToNull="false" />
                        <asp:ControlParameter ControlID="txtCompanyID" Name="CompanyID" PropertyName="Text"
                            Type="String" DefaultValue="{00000000-0000-0000-0000-000000000000}" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </div>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal">
                <div id="divWarehouse" style="float: left; margin-left: 10px;" runat="server">
                    <p>
                        <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
                    </p>
                    <asp:DetailsView ID="DetailsViewWarehouse" runat="server" AutoGenerateRows="False"
                        DataKeyNames="ID" DataSourceID="LinqDataSourceWarehouse" OnItemUpdated="DetailsViewWarehouse_ItemUpdated">
                        <Fields>
                            <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                            <asp:BoundField DataField="Code" HeaderText="Mã" SortExpression="Code" />
                            <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
                            <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
                            <asp:BoundField DataField="Address" HeaderText="Địa chỉ" SortExpression="Address" />
                            <asp:CommandField ShowEditButton="True" EditText="<%$ Resources:Resource,Edit %>"
                                UpdateText="<%$ Resources:Resource,Update %>" CancelText="<%$ Resources:Resource,Cancel %>" />
                        </Fields>
                    </asp:DetailsView>
                    <asp:LinqDataSource ID="LinqDataSourceWarehouse" runat="server" ContextTypeName="RedBloodDataContext"
                        TableName="Warehouses" Where="ID == Guid?(@ID)" EnableDelete="True" EnableUpdate="True">
                        <WhereParameters>
                            <asp:ControlParameter ControlID="GridViewWarehouseList" Name="ID" PropertyName="SelectedValue"
                                Type="Object" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                    <h4>
                        Quản lý kho
                    </h4>
                    <asp:LinkButton ID="LinkButtonNewWarehouseKeeper" runat="server" OnClick="LinkButtonNewWarehouseKeeper_Click">Thêm quản lý kho.</asp:LinkButton>
                    <asp:GridView ID="GridViewWarehouseKeeper" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="ID" DataSourceID="LinqDataSourceWarehouseKeeper" EmptyDataText="<%$ Resources:Resource,EmptyDataText %>">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" EditText="<%$ Resources:Resource,Edit %>"
                                UpdateText="<%$ Resources:Resource,Update %>" CancelText="<%$ Resources:Resource,Cancel %>" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton SkinID="InGrid" ID="LinkButtonDeleteWarehouseKeeper" runat="server"
                                        OnClientClick='return confirm("Xóa quản lý kho này?");' Text="<%$ Resources:Resource,Delete %>"
                                        CommandName="Delete">
                                    
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FullName" HeaderText="Họ và tên" SortExpression="FullName" />
                            <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqDataSourceWarehouseKeeper" runat="server" ContextTypeName="RedBloodDataContext"
                        TableName="WarehouseKeepers" Where="WarehouseID == Guid?(@WarehouseID)" EnableDelete="True"
                        EnableUpdate="True">
                        <WhereParameters>
                            <asp:ControlParameter ControlID="GridViewWarehouseList" Name="WarehouseID" PropertyName="SelectedValue"
                                Type="Object" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                    <h4>
                        Vị trí sắp xếp kho.
                    </h4>
                    <p>
                        <asp:Label ID="ActionStatusViTriKho" runat="server" CssClass="Important"> </asp:Label>
                    </p>
                    <asp:LinkButton ID="LinkButtonNewWarehouseDivision" runat="server" OnClick="LinkButtonNewWarehouseDivision_Click">Thêm vị trí sắp xếp kho.</asp:LinkButton>
                    <asp:GridView ID="GridViewWarehouseDivision" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="ID" DataSourceID="LinqDataSourceWarehouseDivision" EmptyDataText="<%$ Resources:Resource,EmptyDataText %>"
                        OnRowUpdated="GridViewWarehouseDivision_RowUpdated">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" EditText="<%$ Resources:Resource,Edit %>"
                                UpdateText="<%$ Resources:Resource,Update %>" CancelText="<%$ Resources:Resource,Cancel %>" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton SkinID="InGrid" ID="LinkButtonDeleteWarehouseKeeper" runat="server"
                                        OnClientClick='return confirm("Xóa vị trí sắp xếp này?");' Text="<%$ Resources:Resource,Delete %>"
                                        CommandName="Delete">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Code" HeaderText="Mã" SortExpression="Code" />
                            <asp:BoundField DataField="Location" HeaderText="Vị trí" SortExpression="Location" />
                            <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqDataSourceWarehouseDivision" runat="server" ContextTypeName="RedBloodDataContext"
                        EnableDelete="True" EnableUpdate="True" OrderBy="Code" TableName="WarehouseDivisions"
                        Where="WarehouseID == Guid?(@WarehouseID)">
                        <WhereParameters>
                            <asp:ControlParameter ControlID="GridViewWarehouseList" Name="WarehouseID" PropertyName="SelectedValue"
                                Type="Object" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                    <div style="height: 30px; width: 30px; float: left; clear: both;">
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
