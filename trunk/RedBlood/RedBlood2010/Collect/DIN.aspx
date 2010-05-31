<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Collect_DIN" Codebehind="DIN.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnGen">
        <h4>
            Tạo nhãn thu máu
        </h4>
        In bao nhiêu bộ nhãn?
        <asp:TextBox ID="txtNumOfDIN" runat="server" Width="30"></asp:TextBox>
        bộ.
        <br />
        <asp:Button ID="btnGen" runat="server" Text="Tạo nhãn" OnClick="btnGen_Click" />
    </asp:Panel>
</asp:Content>
