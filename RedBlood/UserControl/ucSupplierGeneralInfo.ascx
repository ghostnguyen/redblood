<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSupplierGeneralInfo.ascx.cs"
    Inherits="UserControl_SupplierGeneralInfo" %>
<%--This text box is used as a parameter for LinqDataSource.--%>
<asp:TextBox runat="server" ID="txtSupplierID" Visible="false"></asp:TextBox>
<p>
    <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
</p>
<div id="divGeneralInfo" style="float: left;">
    <asp:DetailsView ID="DetailsViewSupplier" runat="server" AutoGenerateRows="False"
        DataKeyNames="ID" DataSourceID="LinqDataSourceSupplier" OnItemUpdated="DetailsViewSupplier_ItemUpdated">
        <Fields>
            <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
            <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
            <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Website" HeaderText="Website" SortExpression="Website" />
            <asp:BoundField DataField="TaxNo" HeaderText="MST" SortExpression="TaxNo" />
            <asp:CommandField ShowEditButton="True" ButtonType="Link" EditImageUrl="~/Image/Icon/Edit.png"
                UpdateImageUrl="~/Image/Icon/Update.png" CancelImageUrl="~/Image/Icon/Cancel.png" 
                EditText="<%$ Resources:Resource,Edit %>" UpdateText="<%$ Resources:Resource,Update %>" CancelText="<%$ Resources:Resource,Cancel %>"/>
        </Fields>
    </asp:DetailsView>
    <asp:LinqDataSource ID="LinqDataSourceSupplier" runat="server" ContextTypeName="RedBloodDataContext"
        EnableUpdate="True" TableName="Suppliers" Where="(ID) == Guid(@ID)">
        <WhereParameters>
            <%--The TextBox parameter must have default value in case of null/empty string that cannot convert to Guid.--%>
            <asp:ControlParameter ControlID="txtSupplierID" Name="ID" PropertyName="Text" Type="Object"
                DefaultValue="{00000000-0000-0000-0000-000000000000}" />
        </WhereParameters>
    </asp:LinqDataSource>
</div>
<div style="clear:both;"></div>
<div id="divAccountReadOnly">
    <h4>
        Tài khoản
    </h4>
    <asp:GridView ID="GridViewAccount" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        DataSourceID="LinqDataSourceAccount" EmptyDataText="<%$ Resources:Resource,EmptyDataText %>">
        <Columns>
            <asp:TemplateField HeaderText="Ngân hàng">
                <ItemTemplate>
                    <%# Eval("Bank.Bank1.Name") +  " - " + Eval("Bank.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
            <asp:BoundField DataField="No" HeaderText="Số tài khoản" SortExpression="No" />
            <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSourceAccount" runat="server" ContextTypeName="RedBloodDataContext"
        TableName="SupplierBankAccounts" Where="SupplierID == Guid(@SupplierID)" EnableUpdate="True">
        <WhereParameters>
            <asp:ControlParameter ControlID="txtSupplierID" Name="SupplierID" PropertyName="Text"
                Type="Object" DefaultValue="{00000000-0000-0000-0000-000000000000}" />
        </WhereParameters>
    </asp:LinqDataSource>
</div>
<div style="clear:both;"></div>
<div id="divLocation"">
    <h4>
        Địa chỉ
    </h4>
    <asp:GridView ID="GridViewLocation" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        DataSourceID="LinqDataSourceLocation" EmptyDataText="<%$ Resources:Resource,EmptyDataText %>">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
            <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
            <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
            <asp:BoundField DataField="Address" HeaderText="Địa chỉ" SortExpression="Address" />
            <asp:TemplateField HeaderText="Phường/xã">
                <ItemTemplate>
                    <%# Eval("Geo3.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quận/huyện/thành phố">
                <ItemTemplate>
                    <%# Eval("Geo2.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tỉnh/thành phố">
                <ItemTemplate>
                    <%# Eval("Geo1.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSourceLocation" runat="server" ContextTypeName="RedBloodDataContext"
        TableName="SupplierLocations" Where="SupplierID == Guid(@SupplierID)" 
        EnableUpdate="True">
        <WhereParameters>
            <asp:ControlParameter ControlID="txtSupplierID" Name="SupplierID" PropertyName="Text"
                Type="Object" DefaultValue="{00000000-0000-0000-0000-000000000000}" />
        </WhereParameters>
    </asp:LinqDataSource>
    <h4>
        Liên hệ
    </h4>
    <asp:GridView ID="GridViewContact" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        DataSourceID="LinqDataSourceContact" EmptyDataText="<%$ Resources:Resource,EmptyDataText %>">
        <Columns>
            <asp:TemplateField HeaderText="Chi nhánh">
                <ItemTemplate>
                    <%# Eval("SupplierLocation.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FullName" HeaderText="Họ và tên" SortExpression="FullName" />
            <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Birthday" HeaderText="Ngày SN" SortExpression="Birthday" DataFormatString="{0:dd MMMM yyyy}" />
            <asp:BoundField DataField="Title" HeaderText="Chức vụ" SortExpression="Title" />
            <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
        </Columns>
    </asp:GridView>
      <asp:LinqDataSource ID="LinqDataSourceContact" runat="server" ContextTypeName="RedBloodDataContext"
        EnableUpdate="True" OrderBy="SupplierLocation.Name, FullName" TableName="SupplierContactPersons"
        Where="SupplierLocation.SupplierID == Guid(@SupplierID)">
        <WhereParameters>
            <asp:ControlParameter ControlID="txtSupplierID" Name="SupplierID" PropertyName="Text"
                Type="Object" DefaultValue="{00000000-0000-0000-0000-000000000000}" />
        </WhereParameters>
    </asp:LinqDataSource>
</div>
