<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSupplierEditAccount.ascx.cs"
    Inherits="UserControl_SupplierEditAccount" %>
<%--This text box is used as a parameter for LinqDataSource.--%>
<asp:TextBox runat="server" ID="txtSupplierID" Visible="false"></asp:TextBox>
<p>
    <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
</p>
<h4>
    Tài khoản
</h4>
<asp:GridView ID="GridViewAccount" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
    DataSourceID="LinqDataSourceAccount" OnDataBinding="GridViewAccount_DataBinding"
    OnRowCommand="GridViewAccount_RowCommand" EmptyDataText="<%$ Resources:Resource,EmptyDataText %>"
    OnRowUpdated="GridViewAccount_RowUpdated">
    <Columns>
        <asp:CommandField ShowEditButton="True" ButtonType="Link" EditImageUrl="~/Image/Icon/Edit.png"
            UpdateImageUrl="~/Image/Icon/Update.png" CancelImageUrl="~/Image/Icon/Cancel.png"
            EditText="<%$ Resources:Resource,Edit %>" UpdateText="<%$ Resources:Resource,Update %>"
            CancelText="<%$ Resources:Resource,Cancel %>" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton SkinID="InGrid" ID="ImageButton1" runat="server" CausesValidation="False"
                    CommandName="Delete" Text="Xóa tài khoản" CommandArgument='<%# Eval("ID") %>'
                    ImageUrl="~/Image/Icon/Delete.png" OnClientClick='return confirm("Xóa tài khoản này?");'/>
                    
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton SkinID="InGrid" ID="ImageButton2" runat="server" CausesValidation="False"
                    CommandName="Select" Text="Chọn tài khoản chính" CommandArgument='<%# Eval("ID") %>'
                    ImageUrl="~/Image/Icon/Check.png" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ngân hàng">
            <ItemTemplate>
                <%# Eval("Bank.Bank1.Name") +  " - " + Eval("Bank.Name") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Tên tài khoản" SortExpression="Name" />
        <asp:BoundField DataField="No" HeaderText="Số tài khoản" SortExpression="No" />
        <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
    </Columns>
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSourceAccount" runat="server" ContextTypeName="RedBloodDataContext"
    EnableDelete="True" EnableUpdate="True" TableName="SupplierBankAccounts" Where="SupplierID == Guid(@SupplierID)">
    <WhereParameters>
        <asp:ControlParameter ControlID="txtSupplierID" Name="SupplierID" PropertyName="Text"
            Type="Object" DefaultValue="{00000000-0000-0000-0000-000000000000}" />
    </WhereParameters>
</asp:LinqDataSource>
<h4>
    Ngân hàng
</h4>
<asp:GridView ID="GridViewBank" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
    DataSourceID="LinqDataSourceBank" OnRowCommand="GridViewBank_RowCommand" EmptyDataText="<%$ Resources:Resource,EmptyDataText %>">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton SkinID="InGrid" ID="LinkButton1" runat="server" CausesValidation="False"
                    CommandName="AddAccount" Text="Thêm tài khoản" CommandArgument='<%# Eval("ID") %>'
                    ImageUrl="~/Image/Icon/Insert.png" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton SkinID="InGrid" ID="LinkButton2" runat="server" CausesValidation="False"
                    CommandName="Select" Text="Xem chi nhánh" ImageUrl="~/Image/Icon/List.png" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Tên ngân hàng" SortExpression="Name" />
        <asp:BoundField DataField="Adddress" HeaderText="Địa chỉ" SortExpression="Adddress" />
        <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
        <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Name") %>
            </ItemTemplate>
            <EditItemTemplate>
            </EditItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSourceBank" runat="server" ContextTypeName="RedBloodDataContext"
    TableName="Banks" Where="Level == @Level">
    <WhereParameters>
        <asp:Parameter DefaultValue="1" Name="Level" Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>
<div runat="server" id="divBankBrandList" visible="false" style="margin-left: 10px;"
    emptydatatext="<%$ Resources:Resource,EmptyDataText %>">
    <h4>
        Chi nhánh ngân hàng
    </h4>
    <asp:GridView ID="GridViewBankBrand" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        DataSourceID="LinqDataSourceBankBrand" OnRowCommand="GridViewBankBrand_RowCommand">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton SkinID="InGrid" ID="LinkButton3" runat="server" CausesValidation="False"
                        CommandName="AddAccount" Text="Thêm tài khoản" CommandArgument='<%# Eval("ID") %>'
                        ImageUrl="~/Image/Icon/Insert.png" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ngân hàng">
                <ItemTemplate>
                    <%# Eval("Bank1.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Chi nhánh" SortExpression="Name" />
            <asp:BoundField DataField="Adddress" HeaderText="Địa chỉ" SortExpression="Adddress" />
            <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
            <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
            <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSourceBankBrand" runat="server" ContextTypeName="RedBloodDataContext"
        TableName="Banks" Where="Level == @Level and ParentID == Guid?(@ParentID)" EnableUpdate="True">
        <WhereParameters>
            <asp:Parameter DefaultValue="2" Name="Level" Type="Int32" />
            <asp:ControlParameter ControlID="GridViewBank" Name="ParentID" PropertyName="SelectedValue"
                Type="Object" />
        </WhereParameters>
    </asp:LinqDataSource>
    <h4>
        Thêm nhanh 1 chi nhánh
    </h4>
    <p>
        <asp:Label ID="lblMess" runat="server" CssClass="Important"> </asp:Label>
    </p>
    <asp:DetailsView ID="DetailsViewBankBrand" runat="server" AutoGenerateRows="False"
        DataKeyNames="ID" DataSourceID="LinqDataSourceAddQuickBankBrand" DefaultMode="Insert"
        OnItemInserting="DetailsViewBankBrand_ItemInserting" OnItemInserted="DetailsViewBankBrand_ItemInserted">
        <Fields>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                SortExpression="ID" />
            <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
            <asp:BoundField DataField="Adddress" HeaderText="Địa chỉ" SortExpression="Adddress" />
            <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
            <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
            <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
            <asp:CommandField ShowInsertButton="True" ShowCancelButton="false" ButtonType="Link"
                InsertImageUrl="~/Image/Icon/Update.png" InsertText="<%$ Resources:Resource,Update %>" />
        </Fields>
    </asp:DetailsView>
    <asp:LinqDataSource ID="LinqDataSourceAddQuickBankBrand" runat="server" ContextTypeName="RedBloodDataContext"
        EnableInsert="True" TableName="Banks">
    </asp:LinqDataSource>
</div>
