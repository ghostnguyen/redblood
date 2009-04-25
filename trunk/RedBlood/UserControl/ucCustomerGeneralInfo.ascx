<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCustomerGeneralInfo.ascx.cs"
    Inherits="UserControl_CustomerGeneralInfo" %>
<%--This text box is used as a parameter for LinqDataSource.--%>
<asp:TextBox runat="server" ID="txtCustomerID" Visible="false"></asp:TextBox>
<table width="100%">
    <tr>
        <td class="propertyHeaderCell">
            Thông tin chung
        </td>
        <td>
            <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
            <asp:DetailsView ID="DetailsViewCustomer" runat="server" AutoGenerateRows="False"
                DataKeyNames="ID" DataSourceID="LinqDataSourceCustomer" OnItemUpdated="DetailsViewCustomer_ItemUpdated"
                OnItemCommand="DetailsViewCustomer_ItemCommand">
                <Fields>
                    <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                    <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="BankAccountNumber" HeaderText="Số tài khoản" SortExpression="BankAccountNumber" />
                    <asp:BoundField DataField="BankName" HeaderText="Ngân hàng" SortExpression="BankName" />
                    <asp:BoundField DataField="CMND" HeaderText="CMND" SortExpression="CMND" />
                    <asp:CommandField ShowEditButton="True" ButtonType="Link" EditImageUrl="~/Image/Icon/Edit.png"
                        UpdateImageUrl="~/Image/Icon/Update.png" CancelImageUrl="~/Image/Icon/Cancel.png"
                        EditText="<%$ Resources:Resource,Edit %>" UpdateText="<%$ Resources:Resource,Update %>"
                        CancelText="<%$ Resources:Resource,Cancel %>" />
                </Fields>
            </asp:DetailsView>
            <asp:LinqDataSource ID="LinqDataSourceCustomer" runat="server" ContextTypeName="RedBloodDataContext"
                EnableUpdate="True" TableName="Customers" Where="(ID) == Guid(@ID)">
                <WhereParameters>
                    <%--The TextBox parameter must have default value in case of null/empty string that cannot convert to Guid.--%>
                    <asp:ControlParameter ControlID="txtCustomerID" Name="ID" PropertyName="Text" Type="Object"
                        DefaultValue="{00000000-0000-0000-0000-000000000000}" />
                </WhereParameters>
            </asp:LinqDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            RedBlood
        </td>
        <td>
            <asp:DetailsView ID="DetailsViewRedBlood" runat="server" AutoGenerateRows="False" DataKeyNames="ID"
                DataSourceID="LinqDataSourceRedBlood" OnItemUpdated="DetailsViewCustomer_ItemUpdated"
                OnItemCommand="DetailsViewCustomer_ItemCommand">
                <Fields>
                    <asp:BoundField DataField="RedBlood" ShowHeader="false" />
                </Fields>
            </asp:DetailsView>
            <asp:LinqDataSource ID="LinqDataSourceRedBlood" runat="server" ContextTypeName="RedBloodDataContext"
                TableName="Customers" Where="(ID) == Guid(@ID)">
                <WhereParameters>
                    <%--The TextBox parameter must have default value in case of null/empty string that cannot convert to Guid.--%>
                    <asp:ControlParameter ControlID="txtCustomerID" Name="ID" PropertyName="Text" Type="Object"
                        DefaultValue="{00000000-0000-0000-0000-000000000000}" />
                </WhereParameters>
            </asp:LinqDataSource>
            Cộng :
            <asp:TextBox ID="txtRedBlood" runat="server" CssClass="txtPoint"></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" Text="+" OnClick="btnAdd_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            Điểm thưởng
        </td>
        <td>
            <asp:GridView ID="GridViewCusPoint" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                DataSourceID="LinqDataSourcePoint" ShowHeader="False"                 
                onrowupdating="GridViewCusPoint_RowUpdating">
                <Columns>                    
                    <asp:BoundField DataField="Name" />
                    <asp:BoundField DataField="Point" />                                        
                    <asp:TemplateField>
                        <ItemTemplate>
                            Cộng:                            
                            <asp:TextBox runat="server" CssClass="txtPoint" ID="txtPointAdd"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button runat="server" CommandName="Update" Text="<%$ Resources:Resource,AddPoint %>" />                                
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:LinqDataSource ID="LinqDataSourcePoint" runat="server" ContextTypeName="RedBloodDataContext"
                EnableUpdate="True" TableName="PointDefs" OnSelecting="LinqDataSourcePoint_Selecting">
            </asp:LinqDataSource>
        </td>
    </tr>
</table>
