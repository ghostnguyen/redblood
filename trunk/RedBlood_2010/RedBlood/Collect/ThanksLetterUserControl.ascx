<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ThanksLetterUserControl.ascx.cs"
    Inherits="ThanksLetterUserControl" %>
<div style="clear: both; width: 650px;">
    <table style="width: 100%;">
        <tr>
            <td>
                <div style="width: 100%; clear: both;">
                    <div style="float: left; text-align: center;">
                        <%= Resources.Resource.HdrLine3 %>
                        <br />
                        <%= Resources.Resource.HdrLine4 %>
                    </div>
                    <div style="float: right; text-align: center;">
                        <%= Resources.Resource.HdrLine1 %>
                        <br />
                        <%= Resources.Resource.HdrLine2 %>
                        <br />
                        ------o0o------
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="width: 100%; text-align: center; clear: both;">
                    <h3>
                        <%= Resources.Resource.HdrLine4 %>
                        <br />
                        Chân thành cảm ơn
                    </h3>
                </div>
            </td>
        </tr>
    </table>
    <div style="width: 100%; line-height: 30px;">
        Quý Ông/Bà:
        <asp:Label ID="LabelName" runat="server"></asp:Label>
        <br />
        Năm sinh:
        <asp:Label ID="LabelDOB" runat="server"></asp:Label>
        <span style="margin-left: 150px;">Mã túi máu: </span>
        <asp:Label ID="LabelPackCode" runat="server"></asp:Label>
        <br />
        Địa chỉ:
        <asp:Label ID="LabelAddress" runat="server"></asp:Label>
        <br />
        Ông bà đã có nghĩa cử cao đẹp tham gia Hiến Máu Tình Nguyện.
        <br />
        Nay chúng tôi kính gởi đến Ông/Bà kết quả xét nghiệm:
        <div>
            <table border="1" cellspacing="0" cellpadding="0">
                <tr align="center">
                    <td style="width: 200px;">
                        Xét nghiệm
                    </td>
                    <td style="width: 150px;">
                        Kết quả
                    </td>
                </tr>
                <tr>
                    <td>
                        HIV 1-2 (Ag/Ab)
                    </td>
                    <td>
                        <asp:Label ID="LabelHIV" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        HBsAg
                    </td>
                    <td>
                        <asp:Label ID="LabelHBsAg" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Anti-HCV
                    </td>
                    <td>
                        <asp:Label ID="LabelHCV" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        KST Sốt Rét
                    </td>
                    <td>
                        <asp:Label ID="LabelMalaria" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        VDRL
                    </td>
                    <td>
                        <asp:Label ID="LabelSyphilis" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nhóm máu ABO-Rhesus
                    </td>
                    <td>
                        <asp:Label ID="LabelABO_Rh" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        Mọi thắc mắc xin vui lòng liên hệ:
        <br />
        &nbsp;&nbsp;&nbsp; - BQLDA TT Truyền máu khu vực Chợ Rẫy : 083.955.5885
        <br />
        <div style="width: 300px; text-align: center; float: right;">
            TpHCM, ngày
            <%= DateTime.Now.Day %>
            tháng
            <%= DateTime.Now.Month %>
            năm
            <%= DateTime.Now.Year %>
            <br />
            <%= Resources.Resource.HdrLine4 %>
            <br />
            <%= Resources.Resource.FooterLine1 %>
        </div>
    </div>
</div>
