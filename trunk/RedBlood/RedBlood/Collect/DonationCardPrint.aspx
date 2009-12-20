<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt.master" AutoEventWireup="true"
    CodeFile="DonationCardPrint.aspx.cs" Inherits="Collect_DonationCardPrint" %>

<%@ Register Src="~/Collect/DonationCardUserControl.ascx" TagPrefix="uc" TagName="DonationCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div runat="server" id="divCon">
    </div>
</asp:Content>
