<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Rpt910.aspx.cs" Inherits="TestResult_Rpt910" %>

<%@ Register Src="~/UserControl/DateRange.ascx" TagPrefix="uc" TagName="DateR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right;">
        <%= DateTime.Now.ToStringVNLong() %>
    </div>
    <div style="text-align: center;">
        <h3>
            THỐNG KÊ KẾT QUẢ XÉT NGHIỆM SÀNG LỌC
        </h3>
        <uc:DateR ID="ucDateRange" runat="server" />
        <asp:Button ID="btnOk" runat="server" Text="Xem" OnClick="btnOk_Click" />
    </div>
    <br />
    <asp:GridView ID="GridViewRpt" runat="server" DataSourceID="LinqDataSourceRpt" AutoGenerateColumns="false"
        Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Đợt">
                <ItemTemplate>
                    <asp:HyperLink ID="LinkButton1" runat="server" Text='<%# Eval("ID") %>' NavigateUrl='<%# Eval("Url")%>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Tên" />
            <asp:BoundField DataField="Date" HeaderText="Ngày" />
            <asp:BoundField DataField="CoopName" HeaderText="Phối hợp" />
            <asp:BoundField DataField="HostName" HeaderText="Địa điểm" />
            <asp:BoundField DataField="Total" HeaderText="Tổng cộng" />
            
            <asp:TemplateField HeaderText="Dương tính">
                <ItemTemplate>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("TestResultPos") %>'
                        ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="XN" />
                            <asp:BoundField DataField="Total" HeaderText="TC" />
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Chưa xác định">
                <ItemTemplate>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("TestResultNA") %>'
                        ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="XN" />
                            <asp:BoundField DataField="Total" HeaderText="TC" />
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
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
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSourceRpt" runat="server" TableName="vw_ProductCount"
        ContextTypeName="RedBloodDataContext" OnSelecting="LinqDataSourceRpt_Selecting">
    </asp:LinqDataSource>
    <br />
</asp:Content>
