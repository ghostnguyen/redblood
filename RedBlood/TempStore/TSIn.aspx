<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="TSIn.aspx.cs" Inherits="TempStore_TSIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr valign="top">
            <td style="width: 300px;">
                <div class="part">
                    <div class="partHeader">
                        Các đợt thu máu mới
                    </div>
                    <div class="partLinkLast">
                        <div class="partHeader">
                            Dài hạn
                        </div>
                        <asp:ListView ID="ListViewLongRunCampaign" runat="server" DataKeyNames="ID" DataSourceID="LinqDataSourceLongRunCampaign"
                            OnSelectedIndexChanged="ListViewLongRunCampaign_SelectedIndexChanged">
                            <ItemTemplate>
                                <span>
                                    <div style="border-bottom: dotted 1px;">
                                        <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                                        -
                                        <asp:LinkButton ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' CommandName="Select" />
                                        <br />
                                        <asp:Label ID="SrcLabel" runat="server" Text='<%# Eval("SourceName") %>' />
                                        -
                                        <asp:Label ID="TotalLabel" runat="server" Text='<%#"SL: " + Eval("Total")  %>' />
                                        <br />
                                        <asp:Label ID="NoteLabel" runat="server" Text='<%# Eval("Note") %>' />
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
                        <asp:LinqDataSource ID="LinqDataSourceLongRunCampaign" runat="server" ContextTypeName="RedBloodDataContext"
                            TableName="Campaigns" EnableUpdate="True" OnSelecting="LinqDataSourceLongRunCampaign_Selecting">
                        </asp:LinqDataSource>
                        <div class="partHeader">
                            Ngắn hạn
                        </div>
                        <asp:ListView ID="ListViewShortRunCampaign" runat="server" DataKeyNames="ID" DataSourceID="LinqDataSourceShortRunCampaign"
                            OnSelectedIndexChanged="ListViewShortRunCampaign_SelectedIndexChanged">
                            <ItemTemplate>
                                <span>
                                    <div style="border-bottom: dotted 1px;">
                                        <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                                        -
                                        <asp:LinkButton ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' CommandName="Select" />
                                        <br />
                                        <asp:Label ID="SrcLabel" runat="server" Text='<%# Eval("SourceName") %>' />
                                        -
                                        <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy HH:mm}") %>' />
                                        -
                                        <asp:Label ID="TotalLabel" runat="server" Text='<%#"SL: " + Eval("Total")  %>' />
                                        <br />
                                        <asp:Label ID="NoteLabel" runat="server" Text='<%# Eval("Note") %>' />
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
                        <asp:LinqDataSource ID="LinqDataSourceShortRunCampaign" runat="server" ContextTypeName="RedBloodDataContext"
                            TableName="Campaigns" EnableUpdate="True" OnSelecting="LinqDataSourceShortRunCampaign_Selecting">
                        </asp:LinqDataSource>
                    </div>
                </div>
            </td>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="ID" DataSourceID="LinqDataSourcePack">
                    <Columns>
                        <asp:BoundField DataField="Autonum" HeaderText="Mã" InsertVisible="False" SortExpression="Autonum" />
                        <asp:TemplateField HeaderText="Tên">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("People.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ngày">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CollectedDate","{0:dd/MM/yyyy HH:mm}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tình trạng">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thành phần">
                            <ItemTemplate>
                                <asp:Label ID="lblComponent" runat="server" Text='<%# Eval("Component.Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Volume" HeaderText="(ml)" SortExpression="Volume" />
                        <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePack" runat="server" ContextTypeName="RedBloodDataContext"
                    EnableUpdate="True" OnSelecting="LinqDataSourcePack_Selecting" TableName="Packs"
                    OrderBy="Autonum">
                </asp:LinqDataSource>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
