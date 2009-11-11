<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt.master" AutoEventWireup="true"
    CodeFile="PrintEnvelop.aspx.cs" Inherits="FindAndReport_PrintEnvelop" %>

<%@ Register Src="~/UserControl/Envelop.ascx" TagPrefix="uc" TagName="Envelop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div runat="server" id="divCon">
    </div>
</asp:Content>

