<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StoreCount.aspx.cs" Inherits="FindAndReport_StoreCount" %>

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
    
</asp:Content>
