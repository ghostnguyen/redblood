<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="PackManually.aspx.cs" Inherits="PackManually"
    MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/MasterPageUserMenuWithBarcode.master" %>
<%@ Register Src="~/UserControl/CampaignDetail4Manually.ascx" TagPrefix="uc" TagName="CampaignDetail" %>
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
                        <asp:CommandField ShowEditButton="True" EditText="Nhập KQXN" UpdateText="Lưu" CancelText="Ko lưu" />
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
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("PeopleName") %>' />
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CollectedDate","{0:dd/MM/yyyy HH:mm}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label5" runat="server" Text="Thành phần" />
                                <br />
                                <asp:Label ID="Label7" runat="server" Text="Thể tích (ml)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblComponent" runat="server" Text='<%# Eval("ComponentName") %>' />
                                <br />
                                <asp:Label ID="lblVolume" runat="server" Text='<%# Eval("Volume") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListComponent" runat="server" DataSourceID="LinqDataSourceComponent"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("ComponentID") %>'
                                    AppendDataBoundItems="true">
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
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ABO">
                            <HeaderTemplate>
                                <asp:Label ID="Label1" runat="server" Text="ABO" />
                                <br />
                                <asp:Label ID="Label2" runat="server" Text="RH" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblABO" runat="server" Text='<%# Eval("BT.ABO.Name") %>' />
                                <br />
                                <asp:Label ID="lblRH" runat="server" Text='<%# Eval("BT.RH.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListABO" runat="server" DataSourceID="LinqDataSourceABO"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("BT.ABOID") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceABO" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 1">
                                </asp:LinqDataSource>
                                <br />
                                <asp:DropDownList ID="DropDownListRH" runat="server" DataSourceID="LinqDataSourceRH"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("BT.RHID") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceRH" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 6">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HIV">
                            <ItemTemplate>
                                <asp:Label ID="lblHIV" runat="server" Text='<%# Eval("TR.HIV.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListHIV" runat="server" DataSourceID="LinqDataSourceHIV"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("TR.HIVID") %>'
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
                                <asp:Label ID="lblHCV" runat="server" Text='<%# Eval("TR.HCV.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListHCV" runat="server" DataSourceID="LinqDataSourceHCV"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("TR.HCVID") %>'
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
                                <asp:Label ID="lblHBsAg" runat="server" Text='<%# Eval("TR.HBsAg.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListHBsAg" runat="server" DataSourceID="LinqDataSourceHBsAg"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("TR.HBsAgID") %>'
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
                                <asp:Label ID="lblSyphilis" runat="server" Text='<%# Eval("TR.Syphilis.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListSyphilis" runat="server" DataSourceID="LinqDataSourceSyphilis"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("TR.SyphilisID") %>'
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
                                <asp:Label ID="lblMalaria" runat="server" Text='<%# Eval("TR.Malaria.Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListMalaria" runat="server" DataSourceID="LinqDataSourceMalaria"
                                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("TR.MalariaID") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceMalaria" runat="server" ContextTypeName="RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 21">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
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
                <asp:Panel>
                    <h4>
                        Hủy túi máu
                    </h4>
                    Túi máu
                    <asp:TextBox runat="server" ID="txtPackAutonum" Width="50px"></asp:TextBox>
                    Ghi chú
                    <asp:TextBox runat="server" ID="txtDeleteNote"></asp:TextBox>
                    <asp:Button runat="server" ID="btnDelete" Text="Hủy" OnClientClick='return confirm ("Hủy túi máu?")'
                        OnClick="btnDelete_Click" />
                    <div id="divErr" runat="server" class="hidden">
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridViewDeletePack" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="Autonum" DataSourceID="LinqDataSourceDeletePack">
                    <Columns>
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
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("PeopleName") %>' />
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CollectedDate","{0:dd/MM/yyyy HH:mm}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label5" runat="server" Text="Thành phần" />
                                <br />
                                <asp:Label ID="Label7" runat="server" Text="Thể tích (ml)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblComponent" runat="server" Text='<%# Eval("ComponentName") %>' />
                                <br />
                                <asp:Label ID="lblVolume" runat="server" Text='<%# Eval("Volume") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ABO">
                            <HeaderTemplate>
                                <asp:Label ID="Label1" runat="server" Text="ABO" />
                                <br />
                                <asp:Label ID="Label2" runat="server" Text="RH" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblABO" runat="server" Text='<%# Eval("BT.ABO.Name") %>' />
                                <br />
                                <asp:Label ID="lblRH" runat="server" Text='<%# Eval("BT.RH.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HIV">
                            <ItemTemplate>
                                <asp:Label ID="lblHIV" runat="server" Text='<%# Eval("TR.HIV.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HCV">
                            <ItemTemplate>
                                <asp:Label ID="lblHCV" runat="server" Text='<%# Eval("TR.HCV.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HBsAg">
                            <ItemTemplate>
                                <asp:Label ID="lblHBsAg" runat="server" Text='<%# Eval("TR.HBsAg.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Syphilis">
                            <ItemTemplate>
                                <asp:Label ID="lblSyphilis" runat="server" Text='<%# Eval("TR.Syphilis.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Malaria">
                            <ItemTemplate>
                                <asp:Label ID="lblMalaria" runat="server" Text='<%# Eval("TR.Malaria.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ghi chú">
                            <ItemTemplate>
                                <asp:Label ID="lblDelNote" runat="server" Text='<%# Eval("DeleteNote") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourceDeletePack" runat="server" ContextTypeName="RedBloodDataContext"
                    EnableUpdate="True" OnSelecting="LinqDataSourceDeletePack_Selecting" TableName="Packs"
                    OrderBy="Autonum desc" EnableDelete="True">
                </asp:LinqDataSource>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
