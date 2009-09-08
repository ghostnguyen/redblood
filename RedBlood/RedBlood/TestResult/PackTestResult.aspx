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
                                <%--<asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("DIN") %>' />--%>
                                <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("DIN") %>' Visible='<%# Eval("IsTRLocked") %>' />
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
                        <asp:TemplateField HeaderText="ABO" HeaderStyle-Width="190">
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
                                <asp:Label ID="lblHIV" runat="server" Text='<%# Eval("Markers.HIV") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListHIV" runat="server" DataSource='<%# TR.TRList %>'
                                    DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("Markers.HIV") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HCV">
                            <ItemTemplate>
                                <asp:Label ID="lblHCV" runat="server" Text='<%# Eval("Markers.HCV_Ab") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListHCV" runat="server" DataSource='<%# TR.TRList %>'
                                    DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("Markers.HCV_Ab") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HBs_Ag">
                            <ItemTemplate>
                                <asp:Label ID="lblHBs_Ag" runat="server" Text='<%# Eval("Markers.HBs_Ag") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListHBs_Ag" runat="server" DataSource='<%# TR.TRList %>'
                                    DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("Markers.HBs_Ag") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Syphilis">
                            <ItemTemplate>
                                <asp:Label ID="lblSyphilis" runat="server" Text='<%# Eval("Markers.Syphilis") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListSyphilis" runat="server" DataSource='<%# TR.TRList %>'
                                    DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("Markers.Syphilis") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Malaria">
                            <ItemTemplate>
                                <asp:Label ID="lblHIV" runat="server" Text='<%# Eval("Markers.Malaria") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListMalaria" runat="server" DataSource='<%# TR.TRList %>'
                                    DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("Markers.Malaria") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="true" EditText="Nhập" UpdateText="Lưu" CancelText="Ko lưu"
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
