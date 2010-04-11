<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Count.aspx.cs" Inherits="Store_Count" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="" />
            <asp:BoundField DataField="TRNon" HeaderText="Chưa có KQXN" />
            <asp:BoundField DataField="TRPos" HeaderText="Dương tính" />
            <asp:BoundField DataField="TRNeg" HeaderText="Âm tính" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:GridView ID="GridView2" runat="server" DataSource='<%# Eval("bgCountList") %>'
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="VolumeText" />
                            <asp:TemplateField HeaderText="O">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("O_RhD_Pos") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("O_RhD_Neg") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="A">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("A_RhD_Pos") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("A_RhD_Neg") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="B">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("B_RhD_Pos") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("B_RhD_Neg") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AB">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("AB_RhD_Pos") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("AB_RhD_Neg") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Expire" HeaderText="Hết hạn" />
        </Columns>
    </asp:GridView>
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" DataSourceID="LinqDataSource1">
        <Columns>
            <asp:BoundField DataField="ProductCode" HeaderText="Mã" />
            <asp:BoundField DataField="ProductDesc" HeaderText="Sản phẩm" />
            <asp:BoundField DataField="Total" HeaderText="TC" />
            <asp:BoundField DataField="TotalExpired" HeaderText="Hết hạn" />
            <asp:BoundField DataField="TotalExpiredInDays" HeaderText="Hết hạn trong 3 ngày" />
            <asp:BoundField DataField="TotalTRNA" HeaderText="Chưa có KQXN" />
            <asp:BoundField DataField="TotalTRNeg" HeaderText="Âm tính" />
            <asp:BoundField DataField="TotalTRPos" HeaderText="Dương tính" />
            <asp:TemplateField HeaderText="Nhóm máu">
                <ItemTemplate>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("BloodGroupSumary") %>' ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField="BloodGroupDesc" HeaderText="Nhóm máu" />
                            <asp:BoundField DataField="Total" HeaderText="TC" />
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="(ml)">
                <ItemTemplate>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("VolumeSumary") %>' ShowHeader="false">
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
