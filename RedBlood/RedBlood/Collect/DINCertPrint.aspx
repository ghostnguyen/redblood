<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRpt.master" AutoEventWireup="true"
    CodeFile="DINCertPrint.aspx.cs" Inherits="Collect_DINCertPrint" %>
<%@ Register Src="~/Collect/DINCertUserControl.ascx" TagPrefix="uc" TagName="DINCert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div runat="server" id="divCon">
    </div>
</asp:Content>
