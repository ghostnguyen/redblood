<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="RedBlood.Collect.Rpt2OrgMenu" CodeBehind="Rpt2OrgMenu.aspx.cs" %>

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
                <table border="0" cellspacing="5">
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
                            <%--<asp:HyperLink runat="server" Text="In thẻ cho tất cả" ID="HyperLinkAllCard">                
                            </asp:HyperLink>--%>
                        </td>
                        <td rowspan="3">
                            <%--<asp:HyperLink runat="server" Text="In Giấy chứng nhận cho tất cả" ID="HyperLinkAllDINCert">                
                            </asp:HyperLink>--%>
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
        <br />
        <asp:HyperLink runat="server" Text="In thẻ cho tất cả" ID="HyperLinkAllCard">                
        </asp:HyperLink>
        | 
        <asp:HyperLink runat="server" Text="In Giấy chứng nhận cho tất cả" ID="HyperLinkAllDINCert">                
        </asp:HyperLink>
        <asp:Button ID="btnSelectedCard" runat="server" Text="In the chon loc" OnClick="btnSelectedCard_Click" />
        <asp:Button ID="btnSelectedDINCert" runat="server" Text="In Giay chung nhan chon loc"
            OnClick="btnSelectedDINCert_Click" />
        <br />
        <br />
        <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="False" DataKeyNames="DIN"
            DataSourceID="LinqDataSource1">
            <Columns>
                <asp:TemplateField HeaderText="Ten">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DIN" HeaderText="Túi máu" />
                <asp:BoundField DataField="CollectedDate" HeaderText="Ngày thu" />
                <asp:TemplateField HeaderText="Thành phần">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ABO">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("BloodGroupDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HIV">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Markers_HIV") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HCV">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Markers_HCV_Ab") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HBsAg">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Markers_HBs_Ag") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Syphilis">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("Markers_Syphilis") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Malaria">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("Markers_Malaria") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
            OnSelecting="LinqDataSource1_Selecting" TableName="Donations">
        </asp:LinqDataSource>
    </table>
</asp:Content>
