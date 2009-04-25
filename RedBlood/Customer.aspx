<%@ Page Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="Customer.aspx.cs" Inherits="CustomerPage" Title="Khách hàng" %>

<%@ Register Src="~/UserControl/ucCustomerEditAccount.ascx" TagPrefix="ucCustomer"
    TagName="EditAccount" %>
<%@ Register Src="~/UserControl/ucCustomerGeneralInfo.ascx" TagPrefix="ucCustomer"
    TagName="GeneralInfo" %>
<%@ Register Src="~/UserControl/ucCustomerEditLocation.ascx" TagPrefix="ucCustomer"
    TagName="EditLocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    EnableViewState="true">
    <%--This text box is used as a parameter for LinqDataSource.--%>
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="mainContent">
                <div class="leftBigPart">
                    <div class="part">
                        <div class="partHeader">
                            Tạo mới
                        </div>
                        Tên khách hàng
                        <br />
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <asp:Button ID="btnNew" runat="server" Text="Tạo" OnClick="btnNew_Click" />
                    </div>
                    <div class="part">
                        <div class="partHeader">
                            Tìm
                        </div>
                        <asp:TextBox ID="txtFind" runat="server"></asp:TextBox>
                        <asp:Button ID="btnFind" runat="server" Text="Tìm" OnClick="btnFind_Click" />
                        <br />
                        <%--<div style="height: 300px; overflow: auto;" runat="server">--%>
                        <asp:Panel runat="server" Height="300" ScrollBars="Vertical" ID="pGridView" EnableViewState="true">
                            <asp:HiddenField ID="hfScrollPosition" runat="server" EnableViewState="true" />
                            <asp:GridView ID="GridViewCustomerList" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="ID" DataSourceID="LinqDataSourceCustomerList" OnRowCommand="GridViewCustomerList_RowCommand"
                                ShowHeader="False" SkinID="GridViewBlank">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" CommandArgument='<%# Eval("ID") %>'> <%# Eval("Name") %></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="LinqDataSourceCustomerList" runat="server" ContextTypeName="RedBloodDataContext"
                                TableName="Customers" Where="Name.Contains(@Name) and CompanyID == Guid(@CompanyID)"
                                EnableUpdate="True" OrderBy="Name">
                                <WhereParameters>
                                    <asp:ControlParameter ControlID="txtFind" Name="Name" PropertyName="Text" Type="String"
                                        ConvertEmptyStringToNull="false" />
                                    <asp:ControlParameter ControlID="txtCompanyID" Name="CompanyID" PropertyName="Text"
                                        Type="String" DefaultValue="{00000000-0000-0000-0000-000000000000}" />
                                </WhereParameters>
                            </asp:LinqDataSource>
                        </asp:Panel>
                        <%--</div>--%>
                    </div>
                </div>
                <div runat="server" class="rightBigPart">
                    <h3>
                        <asp:DetailsView ID="DetailsViewTitle" runat="server" AutoGenerateRows="False" DataKeyNames="ID"
                            SkinID="DetailViewBlank" DataSourceID="LinqDataSourceCustomer">
                            <Fields>
                                <asp:BoundField DataField="Name" ShowHeader="False"></asp:BoundField>
                            </Fields>
                        </asp:DetailsView>
                        <asp:LinqDataSource ID="LinqDataSourceCustomer" runat="server" ContextTypeName="RedBloodDataContext"
                            TableName="Customers" Where="ID == Guid?(@ID)">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="GridViewCustomerList" Name="ID" PropertyName="SelectedValue"
                                    Type="Object" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </h3>
                    <asp:LinkButton ID="LinkButtonDelete" runat="server" OnClientClick='return confirm("Xóa khách hàng?");'
                        OnClick="LinkButtonDelete_Click">Xóa</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButtonLock" runat="server" OnClientClick='return confirm("Tạm khóa khách hàng?");'>Tạm khóa</asp:LinkButton>
                    <br />
                    <br />
                    <div>
                        <asp:Menu ID="MenuTab" runat="server" Orientation="Horizontal" SkinID="menuTab" OnMenuItemClick="MenuTab_MenuItemClick">
                            <Items>
                                <asp:MenuItem Text="Thông tin chung" Value="GenaralInfo" Selected="true"></asp:MenuItem>
                                <asp:MenuItem Text="Cộng điểm" Value="AddPoint"></asp:MenuItem>
                            </Items>
                        </asp:Menu>
                    </div>
                    <div class="tabPanel">
                        <div runat="server" id="divGeneralInfo" visible="true">
                            <ucCustomer:GeneralInfo runat="server" ID="ucGeneralInfo" />
                        </div>
                        <div style="height: 30px; width: 30px; float: left; clear: both;">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
