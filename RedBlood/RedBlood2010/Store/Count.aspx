﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="RedBlood.Store.Count" CodeBehind="Count.aspx.cs" %>

<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc" TagName="Date" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right;">
        <%= DateTime.Now.ToStringVNLong() %>
    </div>
    <div style="text-align: center;">
        <h3>
            KIỂM KÊ KHO
        </h3>
        Có thống kê số lượng túi máu hết hạn trong vòng
        <asp:TextBox ID="txtDays" runat="server" Width="30px" MaxLength="4"></asp:TextBox>
        ngày tới
        <asp:Button ID="btnOk1" runat="server" Text="Ok" OnClick="btnOk1_Click" />, hay
        ngày
        <uc:Date ID="ucInDays" runat="server" />
        <asp:Button ID="btnOk2" runat="server" Text="Ok" OnClick="btnOk2_Click" />
    </div>
    <%--<%# RedBloodSystem.Url4StoreCountList %>--%>
    <h3>
        Tổng Hợp
    </h3>
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" DataSourceID="LinqDataSource3"
        Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Mã">
                <ItemTemplate>
                    <asp:HyperLink ID="LinkButton1" runat="server" Text='<%# Eval("ProductCode") %>'
                        NavigateUrl='<%# RedBlood.RedBloodSystem.Url4StoreCountList + "ProductCode=" + Eval("ProductCode") + "&ExpiredInDays=" + ExpiredInDays.ToString() %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ProductDesc" HeaderText="Sản phẩm" />
            <asp:BoundField DataField="Total" HeaderText="TC" />
            <asp:BoundField DataField="TotalExpired" HeaderText="Hết hạn" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# "Hết hạn trong " + ExpiredInDays.ToString() + " ngày" %>'
                        Width="30"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("TotalExpiredInDays") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TotalTRNA" HeaderText="Chưa có KQXN" HeaderStyle-Width="30" />
            <asp:BoundField DataField="TotalTRNeg" HeaderText="Âm tính" />
            <asp:BoundField DataField="TotalTRPos" HeaderText="Dương tính" />
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
    <asp:LinqDataSource ID="LinqDataSource3" runat="server" TableName="vw_ProductCount"
        ContextTypeName="RedBlood.RedBloodDataContext" OnSelecting="LinqDataSource3_Selecting">
    </asp:LinqDataSource>
    <h3>
        Âm Tính
    </h3>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataSourceID="LinqDataSource1"
        Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Mã">
                <ItemTemplate>
                    <asp:HyperLink ID="LinkButton1" runat="server" Text='<%# Eval("ProductCode") %>'
                        NavigateUrl='<%# RedBlood.RedBloodSystem.Url4StoreCountList + "ProductCode=" + Eval("ProductCode") + "&ExpiredInDays=" + ExpiredInDays.ToString() %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ProductDesc" HeaderText="Sản phẩm" />
            <asp:BoundField DataField="Total" HeaderText="TC" />
            <asp:BoundField DataField="TotalExpired" HeaderText="Hết hạn" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# "Hết hạn trong " + ExpiredInDays.ToString() + " ngày" %>'
                        Width="30"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("TotalExpiredInDays") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TotalTRNA" HeaderText="Chưa có KQXN" HeaderStyle-Width="30" />
            <asp:BoundField DataField="TotalTRNeg" HeaderText="Âm tính" />
            <asp:BoundField DataField="TotalTRPos" HeaderText="Dương tính" />
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
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" TableName="vw_ProductCount"
        ContextTypeName="RedBlood.RedBloodDataContext" OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
    <h3>
        Dương Tính
    </h3>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" DataSourceID="LinqDataSource2"
        Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Mã">
                <ItemTemplate>
                    <asp:HyperLink ID="LinkButton1" runat="server" Text='<%# Eval("ProductCode") %>'
                        NavigateUrl='<%# RedBlood.RedBloodSystem.Url4StoreCountList + "ProductCode=" + Eval("ProductCode") + "&ExpiredInDays=" + ExpiredInDays.ToString() %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ProductDesc" HeaderText="Sản phẩm" />
            <asp:BoundField DataField="Total" HeaderText="TC" />
            <asp:BoundField DataField="TotalExpired" HeaderText="Hết hạn" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# "Hết hạn trong " + ExpiredInDays.ToString() + " ngày" %>'
                        Width="30"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("TotalExpiredInDays") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TotalTRNA" HeaderText="Chưa có KQXN" HeaderStyle-Width="30" />
            <asp:BoundField DataField="TotalTRNeg" HeaderText="Âm tính" />
            <asp:BoundField DataField="TotalTRPos" HeaderText="Dương tính" />
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
    <asp:LinqDataSource ID="LinqDataSource2" runat="server" TableName="vw_ProductCount"
        ContextTypeName="RedBlood.RedBloodDataContext" OnSelecting="LinqDataSource2_Selecting">
    </asp:LinqDataSource>
</asp:Content>
