<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CollectPack.aspx.cs" Inherits="Collect_CollectPack" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Thông tin mặc định
    </h4>
    Tên KTV:
    <asp:TextBox ID="txtDefaultCollector" runat="server" Width="250"> </asp:TextBox>
    Thể tích:
    <asp:TextBox ID="txtDefaultVolume" runat="server" Width="50"> </asp:TextBox>
    (ml)
    <br />
    <hr />
    <br />
    <table>
        <tr>
            <td>
                Tên
            </td>
            <td>
                <asp:Label ID="lblName" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Mã
            </td>
            <td>
                <asp:Image ID="imgDIN" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Ngày
            </td>
            <td>
                <asp:Label ID="lblDate" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Thu máu
            </td>
            <td>
                <asp:Image ID="imgProduct" runat="server" />
                <br />
                <asp:Label ID="lblDesc" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Thể tích (ml)
            </td>
            <td>
                <asp:TextBox ID="txtVolume" runat="server" Width="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                KTV
            </td>
            <td>
                <asp:TextBox ID="txtCollector" runat="server" Width="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Ghi chú
            </td>
            <td>
                <asp:TextBox ID="txtNote" runat="server" Width="350"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
             
            </td>
            <td>
                <asp:Button ID="txtSave" Text='<%$ Resources:Resource,Update %>' runat="server" 
                    onclick="txtSave_Click" />
            </td>
        </tr>
    </table>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
        TableName="Donations" OnSelecting="LinqDataSource1_Selecting" EnableUpdate="True">
    </asp:LinqDataSource>
    <br />
    <%--<asp:Button ID="btnUpdate" runat="server" Text='<%$ Resources:Resource,Update %>'
        OnClick="btnUpdate_Click" />
    <asp:Button ID="btnClear" runat="server" Text="Xóa danh sách" OnClick="btnClear_Click"
        BorderStyle="Solid" />--%>
    <br />
    <br />
</asp:Content>
