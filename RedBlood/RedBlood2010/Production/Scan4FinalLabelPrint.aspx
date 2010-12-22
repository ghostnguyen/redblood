<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Production_Scan4FinalLabelPrint" CodeBehind="Scan4FinalLabelPrint.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<%@ Register Src="~/UserControl/PeopleOrder.ascx" TagPrefix="uc" TagName="PeopleOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        In nhãn tổng hợp
    </h3>
    <asp:GridView ID="GridViewSum" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Sản phẩm">
                <ItemTemplate>
                    <asp:Image ID="ImagePackCodabar" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product( Eval("ProductCode") as string) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="TC" DataField="Sum" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnOk" runat="server" Text="Print" OnClick="btnOk_Click" OnClientClick="return confirm('In?');" />
    <h4>
        Mã túi máu đang quét
    </h4>
    <div style="height: 30px;">
        <asp:Image ID="imgCurrentDIN" runat="server" ImageUrl="none" />
    </div>
    <br />
    Danh sách in
    <br />
    <asp:DataList ID="DataListPack" runat="server" RepeatDirection="Horizontal" RepeatColumns="6">
        <ItemTemplate>
            <div style="margin: 0px 10px 0px 10px;">
                <asp:Label ID="Label2" runat="server" Text='<%# Eval("DIN") %>'></asp:Label>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductCode") %>'></asp:Label>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("ID") %>'
                    OnClick="btnPackRemove_Click" OnClientClick="return confirm('Xóa?');" Text='Xóa'></asp:LinkButton>
            </div>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
