<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Store_Rpt_DeleteByDay" CodeBehind="Rpt_DeleteByDay.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    Từ ngày:
    <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
    <ajk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom"
        Format="dd/MM/yyyy">
    </ajk:CalendarExtender>
    giờ
    <asp:TextBox ID="txtHourFrom" runat="server" Width="50"></asp:TextBox>
    Đến ngày:
    <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
    <ajk:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo"
        Format="dd/MM/yyyy">
    </ajk:CalendarExtender>
    <asp:TextBox ID="txtHourTo" runat="server" Width="50"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Xem" OnClick="Button1_Click" />
    <br />
    <br />
    
    <asp:GridView ID="GridViewSum" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Sản phẩm">
                <ItemTemplate>
                    <asp:Image ID="ImagePackCodabar" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product( Eval("ProductCode") as string) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Sản Phẩm" DataField="ProductDesc" />
            <asp:BoundField HeaderText="TC" DataField="Sum" />
            <asp:TemplateField HeaderText="Nhóm máu">
                <ItemTemplate>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("BloodGroupSumary") %>'
                        ShowHeader="false" SkinID="Inner">
                        <Columns>
                            <asp:BoundField DataField="BloodGroupDesc" HeaderText="Nhóm máu" />
                            <asp:BoundField DataField="Total" HeaderText="TC" />
                            <asp:TemplateField HeaderText="(ml)">
                                <ItemTemplate>
                                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("VolumeSumary") %>'
                                        ShowHeader="false" SkinID="Inner">
                                        <Columns>
                                            <asp:BoundField DataField="Volume" HeaderText="(ml)" />
                                            <asp:BoundField DataField="Total" HeaderText="TC" />
                                        </Columns>
                                    </asp:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Đợt hủy" DataField="ID" />
            <asp:BoundField HeaderText="Ngày giờ" DataField="Date" />
            <asp:BoundField HeaderText="Tên" DataField="Actor" />
            <asp:BoundField HeaderText="Ghi chú" DataField="Note" />
            <asp:TemplateField HeaderText="Túi máu">
                <ItemTemplate>
                    <asp:DataList ID="DataList1" runat="server" DataSource='<%# Eval("Packs") %>' RepeatDirection="Horizontal"
                        RepeatColumns="2">
                        <ItemTemplate>
                            <div style="margin: 0px 10px 0px 10px;">
                                <%# Eval("DIN") %>
                                &nbsp
                                <%# Eval("ProductCode")%>
                                <%--<asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN(Eval("DIN") as string) %>' />
                                <asp:Image ID="Image2" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("ProductCode") as string) %>'
                                    Style="margin-left: 10px;" />--%>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
