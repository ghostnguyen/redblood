﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CollectDetailRptSelect.aspx.cs" Inherits="Collect_CollectDetailRptSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<%@ Register Src="~/UserControl/DateRange.ascx" TagPrefix="uc" TagName="DateR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc:DateR ID="ucDateRange" runat="server" />
    <asp:Button ID="btnOk" runat="server" Text="Xem" OnClick="btnOk_Click" />
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
