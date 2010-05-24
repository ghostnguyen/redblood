<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignDetail4Rpt.ascx.cs"
    Inherits="UserControl_CampaignDetail4Rpt" %>
<div style="width: 100%; border: solid 0px red;">
    <table>
        <tr valign="top">
            <td>
                <asp:Image ID="ImageCodabar" runat="server" ImageUrl="none" />
            </td>
            <td style="width: 200px;">
                <asp:Label runat="server" ID="lblName"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblDate"></asp:Label>
            </td>
            <td style="width: 350px;">
                <asp:Label runat="server" ID="lblCoopOrg"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblHostOrg"></asp:Label>
            </td>
            <td style="width: 10px;">
            </td>
            <td style="width: 150px;">
                <asp:Label runat="server" ID="lblSrc"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblNote"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
