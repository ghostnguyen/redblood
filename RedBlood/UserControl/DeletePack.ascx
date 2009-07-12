<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeletePack.ascx.cs" Inherits="UserControl_DeletePack" %>
<div>
    <asp:Panel runat="server" ID="Panel1">
        <h4>
            Hủy túi máu
        </h4>
        Túi máu
        <asp:TextBox runat="server" ID="txtPackAutonum" Width="50px"></asp:TextBox>
        Ghi chú
        <asp:TextBox runat="server" ID="txtDeleteNote"></asp:TextBox>
        <asp:Button runat="server" ID="btnDelete" Text="Hủy" OnClientClick='return confirm ("Hủy túi máu?")'
            OnClick="btnDelete_Click" />
        <div id="divErr" runat="server" class="hidden">
        </div>
    </asp:Panel>
    <asp:GridView ID="GridViewDeletePack" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="Autonum" DataSourceID="LinqDataSourceDeletePack">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="Label3" runat="server" Text="Túi máu" />
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Tình trạng" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblAutonum" runat="server" Text='<%# Eval("Autonum") %>' />
                    <br />
                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="Label5" runat="server" Text="Họ & Tên" />
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="Ngày giờ thu" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("People.Name") %>' />
                    <br />
                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CollectedDate","{0:dd/MM/yyyy HH:mm}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="Label5" runat="server" Text="Thành phần" />
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Thể tích (ml)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblComponent" runat="server" Text='<%# Eval("Component.Name") %>' />
                    <br />
                    <asp:Label ID="lblVolume" runat="server" Text='<%# Eval("Volume") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ABO">
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" Text="ABO" />
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="RH" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblABO" runat="server" Text='<%# Eval("ABO.Name") %>' />
                    <br />
                    <asp:Label ID="lblRH" runat="server" Text='<%# Eval("RH.Name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ghi chú">
                <ItemTemplate>
                    <asp:Label ID="lblDelNote" runat="server" Text='<%# Eval("DeleteNote") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSourceDeletePack" runat="server" ContextTypeName="RedBloodDataContext"
        EnableUpdate="True" OnSelecting="LinqDataSourceDeletePack_Selecting" TableName="Packs"
        OrderBy="Autonum desc" EnableDelete="True">
    </asp:LinqDataSource>
</div>
