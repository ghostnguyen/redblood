<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CountList.aspx.cs" Inherits="Store_CountList" %>

<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc" TagName="Date" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center;">
        <h4>
            <asp:Label ID="lblProduct" runat="server"></asp:Label>
        </h4>
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
            <asp:BoundField DataField="DIN" HeaderText="DIN" />
            <asp:BoundField DataField="TestResultStatus" HeaderText="KQXN" />
            <asp:BoundField DataField="BloodGroupDesc" HeaderText="Nhóm máu" />
            <asp:BoundField DataField="Volume" HeaderText="Thể tích" />
            <asp:BoundField DataField="ExpirationDate" HeaderText="Ngày hết hạn" />
            <asp:BoundField DataField="Expired" HeaderText="Hết hạn" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# "Hết hạn trong " + ExpiredInDays.ToString() + " ngày" %>'></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ExpiredInDays") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" TableName="Packs" ContextTypeName="RedBloodDataContext"
        OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
</asp:Content>
