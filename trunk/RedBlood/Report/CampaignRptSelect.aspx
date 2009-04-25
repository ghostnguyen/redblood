<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="CampaignRptSelect.aspx.cs" Inherits="Report_CampaignRptSelect" %>

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
                <asp:HyperLink runat="server" Text="Thư cảm ơn" ID="HyperLinkHIVThankLetter">                
                </asp:HyperLink>
                <asp:HyperLink runat="server" Text="In bìa thư" ID="HyperLinkHIVEnvelope">                
                </asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
