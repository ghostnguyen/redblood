<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Geo.aspx.cs" Inherits="Category_Geo" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Danh mục địa lý hành chính
    </h2>
    
    <div style="float: left; margin-left: 10px; width: 30%;">
        <h3>
            Tỉnh/thành phố
        </h3>
        <asp:TextBox ID="txtLevel1" runat="server"></asp:TextBox>
        <asp:Button ID="btnLevel1New" runat="server" Text="<%$ Resources:Resource,New %>"
            OnClick="btnLevel1New_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None"
            OnRowUpdated="GridView_RowUpdated" ShowHeader="False" OnRowDeleted="GridView_RowDeleted"
            >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" ShowEditButton="True"
                    SelectText="<%$ Resources:Resource,Select %>" EditText="<%$ Resources:Resource,Edit %>"
                    DeleteText="<%$ Resources:Resource,Delete %>"></asp:CommandField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
            EnableDelete="True" EnableUpdate="True" TableName="Geos" Where="Level == @Level" OrderBy="Name">
            <WhereParameters>
                <asp:Parameter DefaultValue="1" Name="Level" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    <div style="float: left; margin-left: 10px; width: 30%;">
        <h3>
            Quận/huyện/thành phố
        </h3>
        <asp:TextBox ID="txtLevel2" runat="server"></asp:TextBox>
        <asp:Button ID="btnLevel2New" runat="server" Text="<%$ Resources:Resource,New %>"
            OnClick="btnLevel2New_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource2" CellPadding="4" ForeColor="#333333" GridLines="None"
            ShowHeader="False" OnRowUpdated="GridView_RowUpdated" OnRowDeleted="GridView_RowDeleted">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" ShowEditButton="True"
                    SelectText="<%$ Resources:Resource,Select %>" EditText="<%$ Resources:Resource,Edit %>"
                    DeleteText="<%$ Resources:Resource,Delete %>"></asp:CommandField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="RedBloodDataContext"
            TableName="Geos" EnableDelete="True" EnableUpdate="True" Where="ParentID == Guid?(@ParentID) and Level == 2" OrderBy="Name">
            <WhereParameters>
                <asp:ControlParameter ControlID="GridView1" Name="ParentID" PropertyName="SelectedValue"
                    Type="Object" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    <div style="float: left; margin-left: 10px; width: 30%">
        <h3>
            Phường/xã
        </h3>
        <asp:TextBox ID="txtLevel3" runat="server"></asp:TextBox>
        <asp:Button ID="btnLevel3New" runat="server" Text="<%$ Resources:Resource,New %>"
            OnClick="btnLevel3New_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource3" CellPadding="4" ForeColor="#333333" GridLines="None"
            OnRowDeleted="GridView_RowDeleted" OnRowUpdated="GridView_RowUpdated" ShowHeader="False">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" ShowEditButton="True" SelectText="<%$ Resources:Resource,Select %>" EditText="<%$ Resources:Resource,Edit %>" DeleteText="<%$ Resources:Resource,Delete %>"  /> 
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource3" runat="server" ContextTypeName="RedBloodDataContext"
            EnableDelete="True" EnableUpdate="True" TableName="Geos" Where="ParentID == Guid?(@ParentID) and Level == 3" OrderBy="Name">
            <WhereParameters>
                <asp:ControlParameter ControlID="GridView2" Name="ParentID" PropertyName="SelectedValue"
                    Type="Object" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
</asp:Content>
