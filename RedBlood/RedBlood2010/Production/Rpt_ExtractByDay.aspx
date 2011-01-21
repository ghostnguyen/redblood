<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="FindAndReport_Rpt_ExtractByDay" CodeBehind="Rpt_ExtractByDay.aspx.cs" %>

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
            <asp:TemplateField HeaderText="In nhãn tổng">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("PrintUrl") %>'><%# Eval("PrintCount") %></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:Button ID="btnSelectedPack" runat="server" Text="In nhãn tổng chọn lọc" OnClick="btnSelectedPack_Click"
        Visible="false" />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="ID">
        <Columns>
            <%--Need to for DataKeyNames--%>
            <asp:BoundField DataField="ID" Visible="false" />
            <asp:TemplateField HeaderText="Ngày">
                <ItemTemplate>
                    <asp:Label ID="TextBox2" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                    <%--<asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("DIN") as string) %>' />--%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mã gốc">
                <ItemTemplate>
                    <asp:Label ID="TextBox3" runat="server" Text='<%# Eval("DIN") %>'></asp:Label>
                    <%--<asp:Image ID="Image1" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("DIN") as string) %>' />--%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sản phẩm">
                <ItemTemplate>
                    <%--<asp:Image ID="Image2" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product(Eval("ProductCode") as string) %>' />
                    <br />--%>
                    <asp:Label ID="TextBox4" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ghi chú">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Note") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
