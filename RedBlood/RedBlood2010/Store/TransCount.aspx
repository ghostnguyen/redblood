<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Store_TransCount" Codebehind="TransCount.aspx.cs" %>

<%@ Register Src="~/UserControl/DateRange.ascx" TagPrefix="uc" TagName="DateR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right;">
        <%= DateTime.Now.ToStringVNLong() %>
    </div>
    <div style="text-align: center;">
        <h3>
            XUẤT - NHẬP - TỒN
        </h3>
        <uc:DateR ID="ucDateRange" runat="server" />
        <asp:Button ID="btnOk" runat="server" Text="Xem" OnClick="btnOk_Click" />
        <br />
        <asp:CheckBox ID="chkStart" runat="server" Text="Tồn đầu" OnCheckedChanged="chkStart_CheckedChanged"
            AutoPostBack="true" Checked="true" />
        <asp:CheckBox ID="chkIn" runat="server" Text="Nhập" OnCheckedChanged="chkIn_CheckedChanged"
            AutoPostBack="true" Checked="true" />
        <asp:CheckBox ID="chkOut" runat="server" Text="Xuất" OnCheckedChanged="chkOut_CheckedChanged"
            AutoPostBack="true" Checked="true" />
        <asp:CheckBox ID="chkEnd" runat="server" Text="Tồn cuối" OnCheckedChanged="chkEnd_CheckedChanged"
            AutoPostBack="true" Checked="true" />
    </div>
    <asp:Panel ID="PanelStart" runat="server">
        <h4>
            Tồn đầu
        </h4>
        <asp:GridView ID="GridViewStart" runat="server" DataSourceID="LinqDataSourceStart"
            AutoGenerateColumns="false" Width="100%">
            <Columns>
                <asp:BoundField DataField="ProductCode" HeaderText="Mã" />
                <asp:BoundField DataField="ProductDesc" HeaderText="Sản phẩm" />
                <asp:BoundField DataField="Total" HeaderText="TC" />
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
        <asp:LinqDataSource ID="LinqDataSourceStart" runat="server" TableName="vw_ProductCount"
            ContextTypeName="RedBlood.RedBloodDataContext" OnSelecting="LinqDataSourceStart_Selecting">
        </asp:LinqDataSource>
    </asp:Panel>
    <asp:Panel ID="PanelIn" runat="server">
        <h4>
            Nhập
        </h4>
        <asp:GridView ID="GridViewIn" runat="server" DataSourceID="LinqDataSourceIn" AutoGenerateColumns="false"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="ProductCode" HeaderText="Mã" />
                <asp:BoundField DataField="ProductDesc" HeaderText="Sản phẩm" />
                <asp:BoundField DataField="Total" HeaderText="TC" />
                <asp:BoundField DataField="TotalInCollect" HeaderText="Thu máu" />
                <asp:BoundField DataField="TotalInProduct" HeaderText="Sản xuất" />
                <asp:BoundField DataField="TotalInReturn" HeaderText="Trả lại" />
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
        <asp:LinqDataSource ID="LinqDataSourceIn" runat="server" TableName="vw_ProductCount"
            ContextTypeName="RedBlood.RedBloodDataContext" OnSelecting="LinqDataSourceIn_Selecting">
        </asp:LinqDataSource>
    </asp:Panel>
    <asp:Panel ID="PanelOut" runat="server">
        <h4>
            Xuất
        </h4>
        <asp:GridView ID="GridViewOut" runat="server" DataSourceID="LinqDataSourceOut" AutoGenerateColumns="false"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="ProductCode" HeaderText="Mã" />
                <asp:BoundField DataField="ProductDesc" HeaderText="Sản phẩm" />
                <asp:BoundField DataField="Total" HeaderText="TC" />
                <asp:BoundField DataField="TotalOutOrder" HeaderText="Cấp phát" />
                <asp:BoundField DataField="TotalOutProduct" HeaderText="Sản xuất" />
                <asp:BoundField DataField="TotalOutDelete" HeaderText="Hủy" />
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
        <asp:LinqDataSource ID="LinqDataSourceOut" runat="server" TableName="vw_ProductCount"
            ContextTypeName="RedBlood.RedBloodDataContext" OnSelecting="LinqDataSourceOut_Selecting">
        </asp:LinqDataSource>
    </asp:Panel>
    <asp:Panel ID="PanelEnd" runat="server">
        <h4>
            Tồn cuối
        </h4>
        <asp:GridView ID="GridViewEnd" runat="server" DataSourceID="LinqDataSourceEnd" AutoGenerateColumns="false"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="ProductCode" HeaderText="Mã" />
                <asp:BoundField DataField="ProductDesc" HeaderText="Sản phẩm" />
                <asp:BoundField DataField="Total" HeaderText="TC" />
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
        <asp:LinqDataSource ID="LinqDataSourceEnd" runat="server" TableName="vw_ProductCount"
            ContextTypeName="RedBlood.RedBloodDataContext" OnSelecting="LinqDataSourceEnd_Selecting">
        </asp:LinqDataSource>
    </asp:Panel>
</asp:Content>
