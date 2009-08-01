<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt.master" AutoEventWireup="true"
    CodeFile="ThankLetter.aspx.cs" Inherits="FindAndReport_ThankLetter" %>

<%@ Register Src="~/UserControl/ThanksLetter.ascx" TagPrefix="uc" TagName="ThankLetter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div runat="server" id="divCon">
    </div>
</asp:Content>

