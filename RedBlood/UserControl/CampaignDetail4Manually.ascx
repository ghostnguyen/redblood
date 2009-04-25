<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignDetail4Manually.ascx.cs"
    Inherits="UserControl_CampaignDetail4Manually" %>
<div>
    <table>
        <tr>
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
                <asp:Label runat="server" ID="lblEst"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblCoopOrg"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblHostOrg"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblNote"></asp:Label>
            </td>
        </tr>
    </table>
</div>
