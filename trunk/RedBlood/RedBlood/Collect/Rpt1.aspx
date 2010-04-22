<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Rpt1.aspx.cs" Inherits="Collect_Rpt1" %>

<%@ Register Src="~/UserControl/DateRange.ascx" TagPrefix="uc" TagName="DateR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right;">
        <%= DateTime.Now.ToStringVNLong() %>
    </div>
    <div style="text-align: center;">
        <h3>
            TỔNG KẾT THU MÁU THEO TỈNH
        </h3>
        <uc:DateR ID="ucDateRange" runat="server" />
        <asp:Button ID="btnOk" runat="server" Text="Xem" OnClick="btnOk_Click" />
    </div>
    <br />
    <asp:GridView ID="GridViewStart" runat="server" DataSourceID="LinqDataSourceStart"
        AutoGenerateColumns="false" Width="100%">
        <Columns>
            <asp:BoundField DataField="Province" HeaderText="Tỉnh" />
            <asp:BoundField DataField="Total" HeaderText="Tổng thu" />
            <asp:BoundField DataField="Total450" HeaderText="450ml" />
            <asp:BoundField DataField="Total350" HeaderText="350ml" />
            <asp:BoundField DataField="Total250" HeaderText="250ml" />
            <asp:BoundField DataField="TotalXXX" HeaderText="Thể tích khác" />
            <asp:BoundField DataField="TotalNeg" HeaderText="Âm tính" />
            <asp:BoundField DataField="TotalPos" HeaderText="Dương tính" />
            <asp:BoundField DataField="TotalNon" HeaderText="KQXN Chưa rõ" />
            <asp:BoundField DataField="TotalMiss" HeaderText="Không thu" />
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSourceStart" runat="server" TableName="vw_ProductCount"
        ContextTypeName="RedBloodDataContext" OnSelecting="LinqDataSourceStart_Selecting">
    </asp:LinqDataSource>
</asp:Content>
