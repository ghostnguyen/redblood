<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TestDef.aspx.cs" Inherits="Category_TestDef" Title="Danh mục kết quả xét nghiệm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Danh mục kết quả xét nghiệm
    </h2>
    <p>
        <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
    </p>
    <div style="float: left; margin-left: 10px;">
        <h3>
            Xét nghiệm
        </h3>
        <asp:TextBox ID="txtLevel1" runat="server"></asp:TextBox>
        <asp:Button ID="btnLevel1New" runat="server" Text="<%$ Resources:Resource,New %>"
            OnClick="btnLevel1New_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource1" OnRowUpdated="GridView_RowUpdated" ShowHeader="False"
            OnRowDeleted="GridView_RowDeleted">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" ShowEditButton="True"
                    SelectText="<%$ Resources:Resource,Select %>" EditText="<%$ Resources:Resource,Edit %>"
                    DeleteText="<%$ Resources:Resource,Delete %>" UpdateText="<%$ Resources:Resource,Update %>"
                    CancelText="<%$ Resources:Resource,Cancel %>"></asp:CommandField>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
            EnableDelete="True" EnableUpdate="True" TableName="TestDefs" Where="Level == 1">
        </asp:LinqDataSource>
    </div>
    <div style="float: left; margin-left: 10px;">
        <h3>
            Kết quả
        </h3>
        <asp:TextBox ID="txtLevel2" runat="server"></asp:TextBox>
        <asp:Button ID="btnLevel2New" runat="server" Text="<%$ Resources:Resource,New %>"
            OnClick="btnLevel2New_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource2" ShowHeader="False" OnRowUpdated="GridView_RowUpdated"
            OnRowDeleted="GridView_RowDeleted">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" ShowEditButton="True"
                    SelectText="<%$ Resources:Resource,Select %>" EditText="<%$ Resources:Resource,Edit %>"
                    DeleteText="<%$ Resources:Resource,Delete %>" UpdateText="<%$ Resources:Resource,Update %>"
                    CancelText="<%$ Resources:Resource,Cancel %>"></asp:CommandField>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="RedBloodDataContext"
            TableName="TestDefs" EnableDelete="True" EnableUpdate="True" Where="ParentID == (@ParentID)">
            <WhereParameters>
                <asp:ControlParameter ControlID="GridView1" Name="ParentID" PropertyName="SelectedValue"
                    Type="Int32" DefaultValue="0" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
</asp:Content>
