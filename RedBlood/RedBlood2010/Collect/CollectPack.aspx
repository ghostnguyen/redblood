<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Collect_CollectPack" Codebehind="CollectPack.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here
        $(document).bind('keydown', 'Ctrl+l', function(evt) {
            $("input[id*='btnSave']").click();
            evt.stopPropagation();
            evt.preventDefault();
        });

    </script>

    <h4>
        Thông tin mặc định
    </h4>
    Tên điều dưỡng:
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
                Ngày đăng kí
            </td>
            <td>
                <asp:Label ID="lblDINDate" Text="" runat="server"></asp:Label>
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
                Ngày thu
            </td>
            <td>
                <asp:Label ID="lblDate" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Sản phẩm
            </td>
            <td>
                <asp:Image ID="imgProduct" runat="server" />
                <br />
                <asp:Label ID="lblProductDesc" runat="server"></asp:Label>
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
                Nhóm máu
            </td>
            <td>
                <asp:Image ID="ImageBloodGroup" runat="server" />
                <br />
                <asp:Label runat="server" ID="lblBloodGroup"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Điều dưỡng
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
                <asp:Button ID="btnSave" Text='<%$ Resources:Resource,Update %>' runat="server" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>
