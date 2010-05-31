<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControl_PackCountByProvince" Codebehind="PackCountByProvince.ascx.cs" %>
<table border="1">
    <tr>
        <td>
        </td>
        <td>
            Chưa có KQNX
        </td>
        <td>
            Dương tính
        </td>
        <td>
            Âm tính
        </td>
        <td>
            Đã cấp phát
        </td>
        <td>
            Hết hạn
        </td>
        <td>
            Hủy
        </td>
    </tr>
    <tr>
        <td>
            Toàn phần
        </td>
        <td>
            <asp:Label runat="server" ID="lblFullNonTR"> </asp:Label>
        </td>
        <td>
            <asp:Label runat="server" ID="lblFullPos"> </asp:Label>
        </td>
        <td>
            <asp:Label runat="server" ID="lblFullNeg"> </asp:Label>
            <br />
            <table border="1">
                <tr>
                    <td>
                    </td>
                    <td>
                        AB
                    </td>
                    <td>
                        A
                    </td>
                    <td>
                        B
                    </td>
                    <td>
                        0
                    </td>
                </tr>
                <tr>
                    <td>
                        250
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull250_AB_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull250_AB_RhNeg"> </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull250_A_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull250_A_RhNeg"> </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull250_B_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull250_B_RhNeg"> </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull250_O_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull250_O_RhNeg"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        350
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull350_AB_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull350_AB_RhNeg"> </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull350_A_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull350_A_RhNeg"> </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull350_B_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull350_B_RhNeg"> </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull350_O_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull350_O_RhNeg"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        450
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull450_AB_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull450_AB_RhNeg"> </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull450_A_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull450_A_RhNeg"> </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull450_B_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull450_B_RhNeg"> </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFull450_O_RhPos"> </asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFull450_O_RhNeg"> </asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <asp:Label runat="server" ID="lblFullDeliver"> </asp:Label>
        </td>
        <td>
            <asp:Label runat="server" ID="lblFullExpire"> </asp:Label>
        </td>
        <td>
            <asp:Label runat="server" ID="lblFullDelete"> </asp:Label>
        </td>
    </tr>

    <tr>
        <td>
            Tiểu cầu Apheresis
        </td>
        <td>
            <asp:Label runat="server" ID="lblPlateletApheresisNonTR"> </asp:Label>
        </td>
        <td>
            <asp:Label runat="server" ID="lblPlateletApheresisPos"> </asp:Label>
        </td>
        <td>
            <asp:Label runat="server" ID="lblPlateletApheresisNeg"> </asp:Label>
        </td>
        <td>
            <asp:Label runat="server" ID="lblPlateletApheresisDeliver"> </asp:Label>
        </td>
        <td>
            <asp:Label runat="server" ID="lblPlateletApheresisExpire"> </asp:Label>
        </td>
        <td>
            <asp:Label runat="server" ID="lblPlateletApheresisDelete"> </asp:Label>
        </td>
    </tr>
</table>
