<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateRange.ascx.cs" Inherits="UserControl_DateRange" %>
<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc" TagName="Date" %>
Từ ngày
<uc:Date ID="ucFromDate" runat="server" />
Đến ngày
<uc:Date ID="ucToDate" runat="server" />
