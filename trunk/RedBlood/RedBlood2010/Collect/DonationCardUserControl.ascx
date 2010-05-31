<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="DonationCardUserControl" Codebehind="DonationCardUserControl.ascx.cs" %>
<div runat="server" id="divLabel" style="position: absolute;">
    <asp:Label ID="lblName" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblDOB" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Image ID="imgAutonum" runat="server" Style="position: absolute;" />
    <asp:Label ID="lblBloodGroup" Style="position: absolute; font-weight:bold;" runat="server"></asp:Label>
    <asp:Label ID="lblAddress" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblDonation1" Style="position: absolute;" runat="server" Text="1"></asp:Label>
    <asp:Label ID="lblDonationDate1" Style="position: absolute;" runat="server"></asp:Label>
</div>
