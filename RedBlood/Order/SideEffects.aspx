<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SideEffects.aspx.cs" Inherits="Order_SideEffects" %>
<%@ Register Src="~/UserControl/PackSideEffect.ascx" TagPrefix="uc" TagName="PackSideEffect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<uc:PackSideEffect ID="PackSideEffect1" runat="server" />

</asp:Content>

