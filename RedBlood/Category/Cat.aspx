<%@ Page Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="Cat.aspx.cs" Inherits="Category_Cat" Title="Danh mục sản phẩm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Danh mục phân loại sản phẩm
    </h2>
    <p>
        <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
    </p>
    <div style="float: left; margin-left: 10px; ">
        <h3>
            Cấp 1
        </h3>
        <asp:TextBox ID="txtLevel1" runat="server"></asp:TextBox>
        <asp:Button ID="btnLevel1New" runat="server" Text="<%$ Resources:Resource,New %>"
            OnClick="btnLevel1New_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource1"
            OnRowUpdated="GridView_RowUpdated" ShowHeader="False" OnRowDeleted="GridView_RowDeleted"
            OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" ShowEditButton="True"
                    SelectText="<%$ Resources:Resource,Select %>" EditText="<%$ Resources:Resource,Edit %>"
                    DeleteText="<%$ Resources:Resource,Delete %>"></asp:CommandField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
            EnableDelete="True" EnableUpdate="True" TableName="Cats" Where="Level == @Level">
            <WhereParameters>
                <asp:Parameter DefaultValue="1" Name="Level" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    <div style="float: left; margin-left: 10px; ">
        <h3>
            Cấp 2
        </h3>
        <asp:TextBox ID="txtLevel2" runat="server"></asp:TextBox>
        <asp:Button ID="btnLevel2New" runat="server" Text="<%$ Resources:Resource,New %>"
            OnClick="btnLevel2New_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource2"
            ShowHeader="False" OnRowUpdated="GridView_RowUpdated" 
            OnRowDeleted="GridView_RowDeleted">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" ShowEditButton="True"
                    SelectText="<%$ Resources:Resource,Select %>" EditText="<%$ Resources:Resource,Edit %>"
                    DeleteText="<%$ Resources:Resource,Delete %>"></asp:CommandField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="RedBloodDataContext"
            TableName="Cats" EnableDelete="True" EnableUpdate="True" Where="ParentID == Guid?(@ParentID) and Level == 2">
            <WhereParameters>
                <asp:ControlParameter ControlID="GridView1" Name="ParentID" PropertyName="SelectedValue"
                    Type="Object" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    <div style="float: left; margin-left: 10px; ">
        <h3>
            Cấp 3
        </h3>
        <asp:TextBox ID="txtLevel3" runat="server"></asp:TextBox>
        <asp:Button ID="btnLevel3New" runat="server" Text="<%$ Resources:Resource,New %>"
            OnClick="btnLevel3New_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource3"
            OnRowDeleted="GridView_RowDeleted" OnRowUpdated="GridView_RowUpdated" 
            ShowHeader="False">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" ShowEditButton="True" SelectText="<%$ Resources:Resource,Select %>" EditText="<%$ Resources:Resource,Edit %>" DeleteText="<%$ Resources:Resource,Delete %>"  /> 
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource3" runat="server" ContextTypeName="RedBloodDataContext"
            EnableDelete="True" EnableUpdate="True" TableName="Cats" Where="ParentID == Guid?(@ParentID) and Level == 3">
            <WhereParameters>
                <asp:ControlParameter ControlID="GridView2" Name="ParentID" PropertyName="SelectedValue"
                    Type="Object" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    
    <div style="float: left; margin-left: 10px; ">
        <h3>
            Cấp 4
        </h3>
        <asp:TextBox ID="txtLevel4" runat="server"></asp:TextBox>
        <asp:Button ID="btnLevel4New" runat="server" Text="<%$ Resources:Resource,New %>"
            OnClick="btnLevel4New_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource4"
            OnRowDeleted="GridView_RowDeleted" OnRowUpdated="GridView_RowUpdated" 
            ShowHeader="False">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" ShowEditButton="True" SelectText="<%$ Resources:Resource,Select %>" EditText="<%$ Resources:Resource,Edit %>" DeleteText="<%$ Resources:Resource,Delete %>"  /> 
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource4" runat="server" ContextTypeName="RedBloodDataContext"
            EnableDelete="True" EnableUpdate="True" TableName="Cats" Where="ParentID == Guid?(@ParentID) and Level == 4">
            <WhereParameters>
                <asp:ControlParameter ControlID="GridView3" Name="ParentID" PropertyName="SelectedValue"
                    Type="Object" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    
    <div style="float: left; margin-left: 10px;">
        <h3>
            Cấp 5
        </h3>
        <asp:TextBox ID="txtLevel5" runat="server"></asp:TextBox>
        <asp:Button ID="btnLevel5New" runat="server" Text="<%$ Resources:Resource,New %>"
            OnClick="btnLevel5New_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource5"
            OnRowDeleted="GridView_RowDeleted" OnRowUpdated="GridView_RowUpdated" 
            ShowHeader="False">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" ShowEditButton="True" SelectText="<%$ Resources:Resource,Select %>" EditText="<%$ Resources:Resource,Edit %>" DeleteText="<%$ Resources:Resource,Delete %>"  /> 
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource5" runat="server" ContextTypeName="RedBloodDataContext"
            EnableDelete="True" EnableUpdate="True" TableName="Cats" Where="ParentID == Guid?(@ParentID) and Level == 5">
            <WhereParameters>
                <asp:ControlParameter ControlID="GridView4" Name="ParentID" PropertyName="SelectedValue"
                    Type="Object" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
</asp:Content>
