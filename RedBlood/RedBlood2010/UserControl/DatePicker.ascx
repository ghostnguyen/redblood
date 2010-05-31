<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControl_DatePicker" Codebehind="DatePicker.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:TextBox runat="server" ID="txtDate" Width="70px"></asp:TextBox>
<ajk:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"
    Format="dd/MM/yyyy">
</ajk:CalendarExtender>
