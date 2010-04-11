<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatePicker.ascx.cs" Inherits="UserControl_DatePicker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:TextBox runat="server" ID="txtDate" Width="150px"></asp:TextBox>
<ajk:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"
    Format="dd/MM/yyyy">
</ajk:CalendarExtender>
