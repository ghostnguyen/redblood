﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StoreCountOpt.aspx.cs" Inherits="FindAndReport_StoreCountOpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:CheckBox ID="chkIncludeBloodGroup" runat="server" Text="" />
    <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="Ok" />
</asp:Content>
