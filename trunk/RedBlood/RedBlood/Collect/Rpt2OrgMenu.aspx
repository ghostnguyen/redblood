<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Rpt2OrgMenu.aspx.cs" Inherits="Collect_Rpt2OrgMenu" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
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
                <table border="0">
                    <tr>
                        <td>
                            <asp:HyperLink runat="server" Text="DS Âm tính" ID="HyperLinkNeg">                
                            </asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink runat="server" Text="Thư cảm ơn" ID="HyperLinkNegThankLetter">                
                            </asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink runat="server" Text="In bìa thư" ID="HyperLinkNegEnvolope">                
                            </asp:HyperLink>
                        </td>
                        <td rowspan="3">
                            <asp:HyperLink runat="server" Text="In thẻ cho tất cả" ID="HyperLinkAllCard">                
                            </asp:HyperLink>
                        </td>
                        <td rowspan="3">
                            <asp:HyperLink runat="server" Text="In Giấy chứng nhận cho tất cả" ID="HyperLinkAllDINCert">                
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink runat="server" Text="DS Dương tính (không bao gồm HIV)" ID="HyperLinkPos">                
                            </asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink runat="server" Text="Thư cảm ơn" ID="HyperLinkPosThankLetter">                
                            </asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink runat="server" Text="In bìa thư" ID="HyperLinkPosEnvelope">                
                            </asp:HyperLink>
                        </td>
                        <td>
                           <%-- <asp:HyperLink runat="server" Text="In thẻ" ID="HyperLinkPosCard">                
                            </asp:HyperLink>--%>
                        </td>
                        <td>
                            <%--<asp:HyperLink runat="server" Text="In Giấy chứng nhận" ID="HyperLinkPosDINCert">                
                            </asp:HyperLink>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink runat="server" Text="DS Dương tính HIV" ID="HyperLinkHIV">                                
                            </asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink runat="server" Text="Thư mời" ID="HyperLinkHIVInvitationLetter">                
                            </asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink runat="server" Text="In bìa thư" ID="HyperLinkHIVEnvelope">                
                            </asp:HyperLink>
                        </td>
                        <td>
                           <%-- <asp:HyperLink runat="server" Text="In thẻ" ID="HyperLinkHIVCard">                
                            </asp:HyperLink>--%>
                        </td>
                        <td>
                          <%--  <asp:HyperLink runat="server" Text="In Giấy chứng nhận" ID="HyperLinkHIV_DINCert">                
                            </asp:HyperLink>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="False" DataKeyNames="DIN"
            DataSourceID="LinqDataSource1">
            <Columns>
                <asp:BoundField DataField="DIN" HeaderText="Túi máu" />
                <asp:BoundField DataField="CollectedDate" HeaderText="Ngày thu" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:TemplateField HeaderText="Thành phần">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Pack.Product.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ABO">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("BloodGroup") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HIV">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Markers.HIV") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HCV">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Markers.HCV_Ab") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HBsAg">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Markers.HBs_Ag") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Syphilis">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Markers.Syphilis") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Malaria">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Markers.Malaria") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
            OnSelecting="LinqDataSource1_Selecting" TableName="Donations">
        </asp:LinqDataSource>
    </table>
</asp:Content>
