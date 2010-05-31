<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Collect_Rpt11" Codebehind="Rpt11.aspx.cs" %>

<%@ Register Src="~/UserControl/DateRange.ascx" TagPrefix="uc" TagName="DateR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right;">
        <%= DateTime.Now.ToStringVNLong() %>
    </div>
    <div style="text-align: center;">
        <h3>
            TỔNG KẾT THU MÁU THEO TỈNH
        </h3>
        <h4>
            <asp:Label ID="lblProvince" runat="server"></asp:Label>
        </h4>
        <uc:DateR ID="ucDateRange" runat="server" />
        <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" />
    </div>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataSourceID="LinqDataSource1"
        Width="100%">
        <Columns>
            <asp:BoundField DataField="CoopOrg" HeaderText="ĐV phối hợp" />
            <asp:BoundField DataField="HostOrg" HeaderText="ĐV tổ chức" />
            <asp:BoundField DataField="Date" HeaderText="Ngày" />
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
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" TableName="Packs" ContextTypeName="RedBloodDataContext"
        OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
</asp:Content>
