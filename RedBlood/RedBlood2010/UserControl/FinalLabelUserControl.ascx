<%@ Control Language="C#" AutoEventWireup="true" Inherits="RedBlood.FinalLabelUserControl"
    CodeBehind="FinalLabelUserControl.ascx.cs" %>
<div id="DivUC" style="position: absolute;" runat="server">
    <asp:Image ID="imgDINBarcode" runat="server" Style="position: absolute;" />
    <asp:Label ID="lblDIN" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblCheckChar" Style="position: absolute;" runat="server" CssClass="txtCheckChar"></asp:Label>

    <asp:Image ID="imgABOBarcode" runat="server" Style="position: absolute;" />
    <asp:Label ID="lblABOCode" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblABOLetter" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblABORh" Style="position: absolute;" runat="server"></asp:Label>

    <asp:Label ID="lblGeo" Style="position: absolute;" runat="server"></asp:Label>

    <asp:Label ID="lblCollectedDateLabel" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblCollectedDate" Style="position: absolute;" runat="server"></asp:Label>

    <asp:Image ID="imgProductBarcode" runat="server" Style="position: absolute;" />
    <asp:Label ID="lblProductCode" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblProductDesc" Style="position: absolute;" runat="server"></asp:Label>

    <asp:Label ID="lblVolumeLabel" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblVolume" Style="position: absolute;" runat="server"></asp:Label>

    <asp:Label ID="lblExpiryDateLabel" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblExpiryDate" Style="position: absolute;" runat="server"></asp:Label>

    <asp:Label ID="lblOrgLine1" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblOrgLine2" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblOrgLine3" Style="position: absolute;" runat="server"></asp:Label>
    <asp:Label ID="lblOrgLine4" Style="position: absolute;" runat="server"></asp:Label>
</div>
