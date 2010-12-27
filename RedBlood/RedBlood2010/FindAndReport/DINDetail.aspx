<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="FindAndReport_DINDetail" CodeBehind="DINDetail.aspx.cs" %>

<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<%@ Register Src="~/UserControl/PeopleDonationLog.ascx" TagPrefix="uc" TagName="PeopleDonationLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr valign="top">
            <td>
                <uc:People runat="server" ID="People1" />
            </td>
            <td>
                <uc:PeopleDonationLog runat="server" ID="PeopleHistory1" />
                <br />
                <asp:GridView runat="server" ID="GridViewPacks" AutoGenerateRows="False" DataKeyNames="ID"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="ID" Visible="false" />
                        <asp:BoundField DataField="DIN" HeaderText="DIN" />
                        <asp:BoundField DataField="ProductCode" HeaderText="Mã sản phẩm" />
                        <asp:BoundField DataField="Volume" HeaderText="(ml)" SortExpression="Volume" />
                        <asp:BoundField DataField="Date" HeaderText="Ngày thu" />
                        <asp:BoundField DataField="ExpirationDate" HeaderText="Hết hạn lúc" />
                        <asp:BoundField DataField="Status" HeaderText="Tình trạng" />
                        <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Button ID="btnSelectedPack" runat="server" Text="In nhãn tổng chọn lọc" OnClick="btnSelectedPack_Click" />
                <h4>
                    Nhật ký nhập kết quả
                </h4>
                <asp:GridView ID="GridViewDINLog" runat="server" AutoGenerateRows="False" DataKeyNames="ID"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Date" HeaderText="Thời gian" />
                        <asp:BoundField DataField="Actor" HeaderText="Tên" />
                        <asp:BoundField DataField="Type" HeaderText="Loại" />
                        <asp:BoundField DataField="Result" HeaderText="Kết quả" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table>
        <tr valign="top">
            <td>
                <br />
                <%--TODO: List Side Effects--%>
            </td>
        </tr>
    </table>
</asp:Content>
