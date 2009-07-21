<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="CampaignDetail.aspx.cs" Inherits="FindAndReport_CampaignDetail" %>

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
                <asp:HyperLink runat="server" Text="DS Âm tính" ID="HyperLinkNeg">                
                </asp:HyperLink>
                <asp:HyperLink runat="server" Text="Thư cảm ơn" ID="HyperLinkNegThankLetter">                
                </asp:HyperLink>
                <asp:HyperLink runat="server" Text="In bìa thư" ID="HyperLinkNegEnvolope">                
                </asp:HyperLink>
                <br />
                <asp:HyperLink runat="server" Text="DS Dương tính (không bao gồm HIV)" ID="HyperLinkPos">                
                </asp:HyperLink>
                <asp:HyperLink runat="server" Text="Thư cảm ơn" ID="HyperLinkPosThankLetter">                
                </asp:HyperLink>
                <asp:HyperLink runat="server" Text="In bìa thư" ID="HyperLinkPosEnvelope">                
                </asp:HyperLink>
                <br />
                <asp:HyperLink runat="server" Text="DS Dương tính HIV" ID="HyperLinkHIV">                                
                </asp:HyperLink>
                <asp:HyperLink runat="server" Text="Thư mời" ID="HyperLinkHIVInvitationLetter">                
                </asp:HyperLink>
                <asp:HyperLink runat="server" Text="In bìa thư" ID="HyperLinkHIVEnvelope">                
                </asp:HyperLink>
            </td>
        </tr>
        <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource1">
            <Columns>
                <asp:BoundField DataField="Autonum" HeaderText="Túi máu" />
                <asp:BoundField DataField="CollectedDate" HeaderText="Ngày thu" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Status" HeaderText="Tình trạng" />
                <asp:BoundField DataField="Volume" HeaderText="(ml)" SortExpression="Volume" />
                <asp:TemplateField HeaderText="Thành phần">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Component.Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ABO">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ABO.Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ABO">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ABO.Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RH">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("RH.Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HIV">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("HIV.Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HCV">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("HCV.Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HBsAg">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("HBsAg.Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Syphilis">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Syphilis.Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Malaria">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Malaria.Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
            OnSelecting="LinqDataSource1_Selecting" TableName="Packs">
        </asp:LinqDataSource>
    </table>
</asp:Content>
