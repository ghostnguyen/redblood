<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Count.aspx.cs" Inherits="Store_Count" %>

<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc" TagName="Date" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align:center;">
        Xem hết hạn trong vòng
        <asp:TextBox ID="txtDays" runat="server" Width="30px" MaxLength="4"></asp:TextBox>
        ngày tới
        <asp:Button ID="btnOk1" runat="server" Text="Ok" OnClick="btnOk1_Click" />. Hoặc
        ngày
        <uc:Date ID="ucInDays" runat="server" />
        <asp:Button ID="btnOk2" runat="server" Text="Ok" OnClick="btnOk2_Click" />
        <br />
        <%= DateTime.Now.ToStringVNLong() %>
    </div>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataSourceID="LinqDataSource1">
        <Columns>
            <asp:TemplateField HeaderText="Mã">
                <ItemTemplate>
                    <asp:HyperLink ID="LinkButton1" runat="server" Text='<%# Eval("ProductCode") %>'
                        NavigateUrl='<%# RedBloodSystem.Url4StoreCountList + "ProductCode=" + Eval("ProductCode") + "&ExpiredInDays=" + ExpiredInDays.ToString() %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ProductDesc" HeaderText="Sản phẩm" />
            <asp:BoundField DataField="Total" HeaderText="TC" />
            <asp:BoundField DataField="TotalExpired" HeaderText="Hết hạn" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# "Hết hạn trong " + ExpiredInDays.ToString() + " ngày" %>'></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("TotalExpiredInDays") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TotalTRNA" HeaderText="Chưa có KQXN" />
            <asp:BoundField DataField="TotalTRNeg" HeaderText="Âm tính" />
            <asp:BoundField DataField="TotalTRPos" HeaderText="Dương tính" />
            <asp:TemplateField HeaderText="Nhóm máu">
                <ItemTemplate>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("BloodGroupSumary") %>'
                        ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField="BloodGroupDesc" HeaderText="Nhóm máu" />
                            <asp:BoundField DataField="Total" HeaderText="TC" />
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="(ml)">
                <ItemTemplate>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("VolumeSumary") %>'
                        ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField="Volume" HeaderText="(ml)" />
                            <asp:BoundField DataField="Total" HeaderText="TC" />
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" TableName="vw_ProductCount"
        ContextTypeName="RedBloodDataContext" OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
</asp:Content>
