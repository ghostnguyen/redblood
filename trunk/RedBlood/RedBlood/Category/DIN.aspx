<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DIN.aspx.cs" Inherits="Category_DIN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Tạo nhãn thu máu
    </h4>
    In bao nhiêu bộ nhãn?
    <asp:TextBox ID="txtNumOfDIN" runat="server"></asp:TextBox>
    bộ.
    <br />
    <%--Mỗi bộ cần bao nhiêu nhãn con?
    <asp:TextBox ID="txtNumOfCopy" runat="server"></asp:TextBox>
    nhãn.
    <br />--%>
    <asp:Button ID="btnGen" runat="server" Text="Tạo nhãn" onclick="btnGen_Click" />
</asp:Content>
