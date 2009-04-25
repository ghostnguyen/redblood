<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFurniture.ascx.cs" Inherits="UserControl_Furniture" %>
<%@ Register Src="~/UserControl/ucCat.ascx" TagPrefix="uc" TagName="Cat" %>
<%@ Register Src="~/UserControl/ucCatTree.ascx" TagPrefix="uc" TagName="CatTree" %>
<asp:TextBox ID="txtFurnitureID" runat="server" Visible="false"></asp:TextBox>
<p>
    <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
</p>
<div style="float: left;">
    <div style="float: left;">
        <table>
            <tr>
                <td>
                    Tên
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Mã hàng
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Serial
                </td>
                <td>
                    <asp:TextBox ID="txtSerialNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    Kích thước
                </td>
                <td>
                    <asp:TextBox ID="txtDimension" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Màu sắc
                </td>
                <td>
                    <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Chất liệu
                </td>
                <td>
                    <asp:TextBox ID="txtMaterial" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <b>Đơn vị tính</b>
                </td>
            </tr>
            <tr>
                <td>
                    Cấp 1
                    <asp:TextBox ID="txtUnit1Name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Cấp 2
                    <asp:TextBox ID="txtUnit2Name" runat="server"></asp:TextBox>
                    =
                    <asp:TextBox ID="txtUnit12Factor" runat="server"></asp:TextBox>
                    (Hệ số) x Cấp 1
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorUnit12Factor" runat="server"
                        ErrorMessage="Nhập hệ số" ControlToValidate="txtUnit12Factor" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Cấp 3
                    <asp:TextBox ID="txtUnit3Name" runat="server"></asp:TextBox>
                    =
                    <asp:TextBox ID="txtUnit23Factor" runat="server"></asp:TextBox>
                    (Hệ số) x Cấp 2
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorUnit23Factor" runat="server"
                        ErrorMessage="Nhập hệ số" ControlToValidate="txtUnit23Factor" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </div>
</div>
<div style="float:left;">
    <table>
        
        <tr>
            <td>
                <uc:CatTree runat="server" ID="ucCatTree" />
            </td>
        </tr>
    </table>
</div>
<div style="clear: both; float: left;">
    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Resource,Update %>" OnClick="btnSave_Click" />
</div>
