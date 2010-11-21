<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="RedBlood.TestResult.TestResult_BloodGroup" MaintainScrollPositionOnPostback="true"
    CodeBehind="BloodGroup.aspx.cs" %>

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
                    DataKeyNames="DIN" DataSourceID="LinqDataSourcePack" OnRowUpdating="GridView1_RowUpdating">
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
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CollectedDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Các lần nhập ABO" HeaderStyle-Width="190">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblABO" runat="server" Text='<%# Eval("BloodGroupDesc")  %>' />--%>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("ABOLog") %>'
                                    ShowHeader="false" SkinID="Inner">
                                    <Columns>
                                        <asp:BoundField DataField="Date" />
                                        <asp:BoundField DataField="BloodGroupDesc" />
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ABO" HeaderStyle-Width="190">
                            <ItemTemplate>
                                <asp:Label ID="lblABO" runat="server" Text='<%# Eval("BloodGroupDesc")  %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlBloodGroup" runat="server" DataSource='<%# BloodGroup.BloodGroupList %>'
                                    DataTextField="Description" DataValueField="Code" SelectedValue='<%# Bind("BloodGroup") %>'
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourceHIV" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
                                    TableName="TestDefs" Where="ParentID == 18">
                                </asp:LinqDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="true" EditText="Nhập" UpdateText="Lưu" CancelText="Ko lưu"
                            HeaderStyle-Width="80" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePack" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
                    EnableUpdate="True" OnSelecting="LinqDataSourcePack_Selecting" TableName="Donations"
                    OrderBy="DIN" EnableDelete="True">
                </asp:LinqDataSource>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <hr />
                <h4>
                    Kết quả bị khóa
                </h4>
                <asp:GridView ID="GridViewLock" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="DIN" DataSourceID="LinqDataSourcePackLock">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label3" runat="server" Text="Túi máu" />
                                <br />
                                <asp:Label ID="Label4" runat="server" Text="Tình trạng" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%--<asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("DIN") %>' Visible='<%# Eval("IsTRLocked") %>' />--%>
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
                        <asp:TemplateField HeaderText="ABO">
                            <HeaderTemplate>
                                <asp:Label ID="Label1" runat="server" Text="ABO" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblABO" runat="server" Text='<%# BloodGroupBLL.GetDescription(Eval("BloodGroup") as string) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePackLock" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
                    EnableUpdate="True" OnSelecting="LinqDataSourcePackLock_Selecting" TableName="Donations"
                    OrderBy="DIN" EnableDelete="True">
                </asp:LinqDataSource>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <hr />
                <h4>
                    Không thu máu
                </h4>
                <asp:GridView ID="GridViewUnCollect" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="DIN" DataSourceID="LinqDataSourceUnCollect">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label5" runat="server" Text="Họ & Tên" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("People.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourceUnCollect" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
                    EnableUpdate="True" OnSelecting="LinqDataSourceUnCollect_Selecting" TableName="Donations"
                    OrderBy="DIN" EnableDelete="True">
                </asp:LinqDataSource>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
