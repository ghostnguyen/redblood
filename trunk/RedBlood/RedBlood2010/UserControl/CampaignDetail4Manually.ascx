<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="RedBlood.UserControl.CampaignDetail4Manually" Codebehind="CampaignDetail4Manually.ascx.cs" %>
<div>
    <table border="1" cellspacing="0">
        <tr valign="top">
            <td>
                <asp:Image ID="ImageCodabar" runat="server" ImageUrl="none" />
            </td>
            <td>
                <asp:Label runat="server" ID="lblName"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblDate"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblSrc"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblEst"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblCoopOrg"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblHostOrg"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblContactName"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblTitle"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblPhone"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblNote"></asp:Label>
            </td>
        </tr>
        
    </table>
</div>
