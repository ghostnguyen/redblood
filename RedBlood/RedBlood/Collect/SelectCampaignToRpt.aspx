<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SelectCampaignToRpt.aspx.cs" Inherits="Collect_SelectCampaignToRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    Ngày:
    <asp:TextBox runat="server" ID="txtDate" Width="150px"></asp:TextBox>
    <ajk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
        Format="dd/MM/yyyy">
    </ajk:CalendarExtender>
    <asp:Button ID="btnView" runat="server" Text="Xem" onclick="btnView_Click" />
    <br />
    <asp:ListView ID="ListView1" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:HyperLink ID="LinkButton1" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# Eval("Link") %>'></asp:HyperLink>
            <br />
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
