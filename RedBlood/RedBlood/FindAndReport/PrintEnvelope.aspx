<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt.master" AutoEventWireup="true"
    CodeFile="PrintEnvelope.aspx.cs" Inherits="FindAndReport_PrintEnvelope" %>
<%@ Register Src="~/UserControl/Envelope.ascx" TagPrefix="uc" TagName="Envelope" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div runat="server" id="divCon">
    </div>
</asp:Content>
