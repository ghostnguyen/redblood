<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PackTestResult.aspx.cs" Inherits="PackTestResult" MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Src="~/UserControl/CampaignDetail4Manually.ascx" TagPrefix="uc" TagName="CampaignDetail" %>
<%@ Register Src="~/UserControl/DeletePack.ascx" TagPrefix="uc" TagName="DeletePack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr valign="top">
            <td>
                <uc:CampaignDetail runat="server" ID="CampaignDetail1" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="DIN" DataSourceID="LinqDataSourcePack" OnRowUpdating="GridView1_RowUpdating"
                    OnRowUpdated="GridView1_RowUpdated">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" EditText="Nhập" UpdateText="Lưu" CancelText="Ko lưu"
                            HeaderStyle-Width="80" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label3" runat="server" Text="Túi máu" />
                                <br />
                                <asp:Label ID="Label4" runat="server" Text="Tình trạng" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("DIN") %>' />
                                <br />
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label5" runat="server" Text="Họ & Tên" />
                                <br />
                                <asp:Label ID="Label6" runat="server" Text="Ngày giờ thu" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("People.Name") %>' />
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CollectedDate","{0:dd/MM/yyyy HH:mm}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ABO" HeaderStyle-Width="60">
                            <HeaderTemplate>
                                <asp:Label ID="Label1" runat="server" Text="ABO" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblABO" runat="server" Text='<%# BloodGroupBLL.GetDescription(Eval("BloodGroup") as string) %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlBloodGroup" runat="server" DataSource='<%# BloodGroup.BloodGroupList %>'
                                    DataTextField="Description" DataValueField="Code" SelectedValue='<%# Bind("BloodGroup") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceHIV" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 18">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HIV">
                            <ItemTemplate>
                                <asp:Label ID="lblHIV" runat="server" Text='<%# Eval("HIV.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListHIV" runat="server" DataSourceID="LinqDataSourceHIV"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("HIV.ID") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceHIV" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 18">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HCV">
                            <ItemTemplate>
                                <asp:Label ID="lblHCV" runat="server" Text='<%# Eval("HCV.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListHCV" runat="server" DataSourceID="LinqDataSourceHCV"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("HCV.ID") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceHCV" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 15">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HBsAg">
                            <ItemTemplate>
                                <asp:Label ID="lblHBsAg" runat="server" Text='<%# Eval("HBsAg.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListHBsAg" runat="server" DataSourceID="LinqDataSourceHBsAg"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("HBsAg.ID") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceHBsAg" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 12">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Syphilis">
                            <ItemTemplate>
                                <asp:Label ID="lblSyphilis" runat="server" Text='<%# Eval("Syphilis.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListSyphilis" runat="server" DataSourceID="LinqDataSourceSyphilis"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("Syphilis.ID") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceSyphilis" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 9">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Malaria">
                            <ItemTemplate>
                                <asp:Label ID="lblMalaria" runat="server" Text='<%# Eval("Malaria.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListMalaria" runat="server" DataSourceID="LinqDataSourceMalaria"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("Malaria.ID") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceMalaria" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 21">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" EditText="Nhập" UpdateText="Lưu" CancelText="Ko lưu"
                            HeaderStyle-Width="80" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePack" runat="server" ContextTypeName="RedBloodDataContext"
                    EnableUpdate="True" OnSelecting="LinqDataSourcePack_Selecting" TableName="Donations"
                    OrderBy="DIN" EnableDelete="True">
                </asp:LinqDataSource>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <hr />
               <%-- <uc:DeletePack runat="server" ID="DeletePack1" />--%>
            </td>
        </tr>
    </table>
</asp:Content>
