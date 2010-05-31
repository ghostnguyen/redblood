<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControl_DateRange" Codebehind="DateRange.ascx.cs" %>
<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc" TagName="Date" %>
Từ ngày
<uc:Date ID="ucFromDate" runat="server" />
Đến ngày
<uc:Date ID="ucToDate" runat="server" />
