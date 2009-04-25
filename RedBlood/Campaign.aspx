<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="Campaign.aspx.cs" Inherits="Campaign" %>

<%@ Register Src="~/UserControl/Campaign.ascx" TagPrefix="uc" TagName="Campaign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr valign="top">
            <td style="width: 210px;">
                <div class="part">
                    <div class="partHeader">
                        Nguồn thu
                    </div>
                    <div class="partLink">
                        <asp:LinkButton ID="btnNew" runat="server" Text="Thêm đợt thu máu" 
                            onclick="btnNew_Click" />
                    </div>
                    <div class="partLinkLast">
                        <asp:BulletedList runat="server" ID="lbSource" DataSourceID="LinqDataSourceSrc" DataTextField="Name"
                            DataValueField="ID" DisplayMode="LinkButton" BulletStyle="NotSet" OnClick="lbSource_Click">
                        </asp:BulletedList>
                        <asp:LinqDataSource ID="LinqDataSourceSrc" runat="server" ContextTypeName="RedBloodDataContext"
                            TableName="TestDefs" Where="ParentID = 35">
                        </asp:LinqDataSource>
                    </div>
                </div>
            </td>
            <td align="center">
                <asp:ListView ID="ListView1" runat="server" DataKeyNames="ID" DataSourceID="LinqDataSourceCampaign"
                    OnSelectedIndexChanged="ListView1_SelectedIndexChanged">
                    <ItemTemplate>
                        <span>
                            <div style="border-bottom:dotted 1px;">
                                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                                -
                                <asp:LinkButton ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' CommandName="Select" />
                                <br />
                                <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy HH:mm}") %>' />
                                -
                                <asp:Label ID="EstLabel" runat="server" Text='<%# Eval("Est") %>' />
                                -
                                <asp:Label ID="SrcLabel" runat="server" Text='<%# Eval("Source.Name") %>' />
                                <br />
                                <asp:Label ID="CoopOrgLabel" runat="server" Text='<%# Eval("CoopOrg.Name") %>' />
                                -
                                <asp:Label ID="HostOrgLabel" runat="server" Text='<%# Eval("HostOrg.Name") %>' />
                                <br />
                                <asp:Label ID="ContactNameLabel" runat="server" Text='<%# Eval("ContactName") %>' />
                                -
                                <asp:Label ID="ContactPhoneLabel" runat="server" Text='<%# Eval("ContactPhone") %>' />
                                -
                                <asp:Label ID="ContactTitleLabel" runat="server" Text='<%# Eval("ContactTitle") %>' />
                                <br />
                                <asp:Label ID="NoteLabel" runat="server" Text='<%# Eval("Note") %>' />
                                <br />
                            </div>
                        </span>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <span>Không có dữ liệu.</span>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <div id="itemPlaceholderContainer" runat="server">
                            <span id="itemPlaceholder" runat="server" />
                        </div>
                        <div>
                        </div>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:LinqDataSource ID="LinqDataSourceCampaign" runat="server" ContextTypeName="RedBloodDataContext"
                    TableName="Campaigns" OrderBy="Date desc" EnableUpdate="True" 
                    Where="SourceID == @SourceID">
                    <WhereParameters>
                        <asp:ControlParameter ControlID="lbSource" DefaultValue="0" 
                            Name="SourceID" PropertyName="SelectedValue" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </td>
            <td style="width: 380px;">
                <uc:Campaign runat="server" ID="ucCampaign1" />
            </td>
        </tr>
    </table>
</asp:Content>
