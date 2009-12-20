<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InvitationLetterUserControl.ascx.cs"
    Inherits="InvitationLetterUserControl" %>
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
                        Giấy mời
                    </h3>
                </div>
            </td>
        </tr>
    </table>
    <div style="width: 100%; line-height: 30px;">
        Kính mời Ông/Bà:
        <asp:Label ID="LabelName" runat="server"></asp:Label>
        <br />
        Năm sinh:
        <asp:Label ID="LabelDOB" runat="server"></asp:Label>
        <br />
        Địa chỉ:
        <asp:Label ID="LabelAddress" runat="server"></asp:Label>
        <br />
        - Trước hết chúng tôi chân cảm ơn về nghĩa cử cao đẹp của Ông(Bà), đã Hiến Máu Nhân
        Đạo vào ngày
        <asp:Label ID="LabelCollectedDate" runat="server"></asp:Label>
        có mã số túi máu:
        <asp:Label ID="LabelPackCode" runat="server"></asp:Label>
        <br />
        - Sau khi có các kết quả xét nghiệm sàng lọc, chúng tôi muốn trao đổi trực tiếp
        với Ông (Bà) về một số vấn đề cụ thể.
        <br />
        - Vậy kính mời Ông (Bà) có mặt tại phòng xét nghiệm miễn dịch số 03 Khoa Huyết Học
        – Truyền Máu, Bệnh viện Chợ Rẫy, số 201B Nguyễn Chí Thanh, Q5, TP.HCM, vào lúc 08
        giờ ngày
        <asp:Label ID="LabelDate" runat="server"></asp:Label>, xin liên hệ trước qua số
        điện thoại: 08.8554137 – Xin số 157.
        <br />
        Trân trọng kính chào.
        <br />
        <div style="width: 300px; text-align: center; float: right;">
            TpHCM, ngày
            <%= DateTime.Now.Day %>
            tháng
            <%= DateTime.Now.Month %>
            năm
            <%= DateTime.Now.Year %>
            <br />
            Khoa Huyết học - Truyền máu
            <br />
            Trưởng khoa
        </div>
    </div>
</div>
