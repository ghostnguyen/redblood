<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="PackTempStore.aspx.cs" Inherits="PackTempStore"
    MaintainScrollPositionOnPostback="true" %>

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
                    DataKeyNames="Autonum" DataSourceID="LinqDataSourcePack" OnRowUpdating="GridView1_RowUpdating"
                    OnRowUpdated="GridView1_RowUpdated">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" EditText="Nhập" UpdateText="Lưu" CancelText="Ko lưu" HeaderStyle-Width="80" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label3" runat="server" Text="Túi máu" />
                                <br />
                                <asp:Label ID="Label4" runat="server" Text="Tình trạng" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("Autonum") %>' />
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
                        <asp:TemplateField HeaderStyle-Width="100">
                            <HeaderTemplate>
                                <asp:Label ID="Label5" runat="server" Text="Thành phần" />
                                <br />
                                <asp:Label ID="Label7" runat="server" Text="Thể tích (ml)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblComponent" runat="server" Text='<%# Eval("Component.Name") %>' />
                                <br />
                                <asp:Label ID="lblVolume" runat="server" Text='<%# Eval("Volume") %>' />
                                <br />
                                <asp:Label ID="lblSubstance" runat="server" Text='<%# Eval("Substance.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListComponent" runat="server" DataSourceID="LinqDataSourceComponent"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("ComponentID") %>'
                                    AppendDataBoundItems="true" Enabled="false">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceComponent" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ID == 25 || ID == 30">
                                </asp:LinqDataSource>
                                <br />
                                <asp:DropDownList ID="DropDownListVol" runat="server" DataSourceID="LinqDataSourceVol"
                                    DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("Volume") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceVol" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 31">
                                </asp:LinqDataSource>
                                <br />
                                <asp:DropDownList ID="DropDownListSubstance" runat="server" DataSourceID="LinqDataSourceSubstance"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("SubstanceID") %>'
                                    AppendDataBoundItems="true" Enabled="false">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceSubstance" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 48">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ABO" HeaderStyle-Width="60">
                            <HeaderTemplate>
                                <asp:Label ID="Label1" runat="server" Text="ABO" />
                                <br />
                                <asp:Label ID="Label2" runat="server" Text="RH" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblABO" runat="server" Text='<%# Eval("BloodType2.ABO.Name") %>' />
                                <br />
                                <asp:Label ID="lblRH" runat="server" Text='<%# Eval("BloodType2.RH.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListABO" runat="server" DataSourceID="LinqDataSourceABO"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("BloodType2.ABO.ID") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceABO" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 1">
                                </asp:LinqDataSource>
                                <br />
                                <asp:DropDownList ID="DropDownListRH" runat="server" DataSourceID="LinqDataSourceRH"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("BloodType2.RH.ID") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceRH" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 6">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" EditText="Nhập" UpdateText="Lưu" CancelText="Ko lưu" HeaderStyle-Width="80" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePack" runat="server" ContextTypeName="RedBloodDataContext"
                    EnableUpdate="True" OnSelecting="LinqDataSourcePack_Selecting" TableName="Packs"
                    OrderBy="Autonum desc" EnableDelete="True">
                </asp:LinqDataSource>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <hr />
                <uc:DeletePack runat="server" ID="DeletePack1" />
            </td>
        </tr>
    </table>
</asp:Content>
