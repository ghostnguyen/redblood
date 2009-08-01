<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StoreCount.aspx.cs" Inherits="FindAndReport_StoreCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            <asp:Label runat="server" ID="lblFull250_AB_RhNeg"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull250_A_RhPos"> </asp:Label>
                            <asp:Label runat="server" ID="lblFull250_A_RhNeg"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull250_B_RhPos"> </asp:Label>
                            <asp:Label runat="server" ID="lblFull250_B_RhNeg"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull250_O_RhPos"> </asp:Label>
                            <asp:Label runat="server" ID="lblFull250_O_RhNeg"> </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            350
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull350_AB_RhPos"> </asp:Label>
                            <asp:Label runat="server" ID="lblFull350_AB_RhNeg"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull350_A_RhPos"> </asp:Label>
                            <asp:Label runat="server" ID="lblFull350_A_RhNeg"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull350_B_RhPos"> </asp:Label>
                            <asp:Label runat="server" ID="lblFull350_B_RhNeg"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull350_O_RhPos"> </asp:Label>
                            <asp:Label runat="server" ID="lblFull350_O_RhNeg"> </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            450
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull450_AB_RhPos"> </asp:Label>
                            <asp:Label runat="server" ID="lblFull450_AB_RhNeg"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull450_A_RhPos"> </asp:Label>
                            <asp:Label runat="server" ID="lblFull450_A_RhNeg"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull450_B_RhPos"> </asp:Label>
                            <asp:Label runat="server" ID="lblFull450_B_RhNeg"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFull450_O_RhPos"> </asp:Label>
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
                Hồng cầu lắng
            </td>
            <td>
                <asp:Label runat="server" ID="lblRBCNonTR"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblRBCPos"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblRBCNeg"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblRBCDeliver"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblRBCExpire"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblRBCDelete"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Bạch cầu
            </td>
            <td>
                <asp:Label runat="server" ID="lblWBCNonTR"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblWBCPos"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblWBCNeg"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblWBCDeliver"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblWBCExpire"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblWBCDelete"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Huyết tương tươi đông lạnh
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaNonTR"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaPos"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaNeg"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaDeliver"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaExpire"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaDelete"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Tiểu cầu
            </td>
            <td>
                <asp:Label runat="server" ID="lblPlateletNonTR"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblPlateletPos"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblPlateletNeg"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblPlateletDeliver"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblPlateletExpire"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblPlateletDelete"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Tủa lạnh
            </td>
            <td>
                <asp:Label runat="server" ID="lblFactorVIIINonTR"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFactorVIIIPos"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFactorVIIINeg"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFactorVIIIDeliver"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFactorVIIIExpire"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFactorVIIIDelete"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Huyết tương dự trữ
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaPoorNonTR"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaPoorPos"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaPoorNeg"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaPoorDeliver"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaPoorExpire"> </asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFFPlasmaPoorDelete"> </asp:Label>
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
</asp:Content>
