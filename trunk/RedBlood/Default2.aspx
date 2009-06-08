<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <asp:ScriptManager ID="ScriptManager10" runat="server">
                </asp:ScriptManager>
                <asp:LinkButton runat="server" ID="LinkButton1" Text="fasfas"></asp:LinkButton>
                <ajk:ModalPopupExtender ID="MPE1" runat="server" TargetControlID="LinkButton1" PopupControlID="PanelPeople"
                    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                </ajk:ModalPopupExtender>
                <asp:Panel runat="server" ID="PanelPeople" CssClass="modalPopup" Style="display: none;">
                    Lý do hủy:
                    <asp:TextBox runat="server" ID="txtRemoveNote"> </asp:TextBox>
                    <div class="dotLineBottom" style="width: 100%;">
                    </div>
                    <asp:Button runat="server" ID="btnSelect" Text='<%$ Resources:Resource,Delete %>'
                        OnClick="btnSelect_Click" />
                    <asp:Button runat="server" ID="btnClose" Text='<%$ Resources:Resource,Close %>' />
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
