<%@ Page Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="Company.aspx.cs" Inherits="CompanyPage" Title="Công ty" %>

<%@ Register Src="~/UserControl/ucCompanyEditAccount.ascx" TagPrefix="ucCompany"
    TagName="EditAccount" %>
<%@ Register Src="~/UserControl/ucCompanyGeneralInfo.ascx" TagPrefix="ucCompany"
    TagName="GeneralInfo" %>
<%@ Register Src="~/UserControl/ucCompanyEditLocation.ascx" TagPrefix="ucCompany"
    TagName="EditLocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal">
                <div id="divCompany" style="float: left; margin-left: 10px;" runat="server">
                    <h2>
                        <asp:DetailsView ID="DetailsViewTitle" runat="server" AutoGenerateRows="False" DataKeyNames="ID"
                            SkinID="DetailViewBlank" DataSourceID="LinqDataSourceCompany">
                            <Fields>
                                <asp:BoundField DataField="Name" ShowHeader="False" ControlStyle-Width="500px">
                                    <ControlStyle Width="500px" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                            </Fields>
                        </asp:DetailsView>
                        <asp:LinqDataSource ID="LinqDataSourceCompany" runat="server" ContextTypeName="RedBloodDataContext"
                            TableName="aspnet_Memberships" 
                            EnableUpdate="True" onselecting="LinqDataSourceCompany_Selecting" >
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
                        <ucCompany:GeneralInfo runat="server" ID="ucGeneralInfo" />
                    </div>
                    <div runat="server" id="divEditAccount" style="float: left;" visible="false">
                        <ucCompany:EditAccount runat="server" ID="ucEditAccount" />
                    </div>
                    <div runat="server" id="divEditLocation" style="float: left;" visible="false">
                        <ucCompany:EditLocation runat="server" ID="ucEditLocation" />
                    </div>
                    <div style="height: 30px; width: 30px; float: left; clear: both;">
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
