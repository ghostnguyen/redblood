<%@ Page Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="Bank.aspx.cs" Inherits="Category_Bank" Title="Bank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h2>
                Danh mục các ngân hàng và chi nhánh
            </h2>
            <p>
                <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
            </p>
            <h4>
                Ngân hàng
            </h4>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                DataKeyNames="ID" DataSourceID="LinqDataSource1" OnRowDeleted="GridView1_RowDeleted">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" DeleteText='<%$ Resources:Resource,Delete %>'
                        EditText="<%$ Resources:Resource,Edit %>" UpdateText="<%$ Resources:Resource,Update %>"
                        CancelText="<%$ Resources:Resource,Cancel %>" SelectText="<%$ Resources:Resource,Select %>" ShowSelectButton="true" />
                    <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                    <asp:BoundField DataField="Adddress" HeaderText="Địa chỉ" SortExpression="Adddress" />
                    <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
                    <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
                    <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
                </Columns>
            </asp:GridView>
            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
                EnableDelete="True" EnableInsert="True" EnableUpdate="True" 
                TableName="Banks" Where="Level == @Level">
                <WhereParameters>
                    <asp:Parameter DefaultValue="1" Name="Level" Type="Int32" />
                </WhereParameters>
            </asp:LinqDataSource>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="<%$ Resources:Resource,New %>" />
            <br />
            <h4>
                Chi nhánh
            </h4>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ID" DataSourceID="LinqDataSource2" 
                onrowdeleted="GridView2_RowDeleted">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" DeleteText='<%$ Resources:Resource,Delete %>'
                        EditText="<%$ Resources:Resource,Edit %>" UpdateText="<%$ Resources:Resource,Update %>"
                        CancelText="<%$ Resources:Resource,Cancel %>" SelectText="<%$ Resources:Resource,Select %>"  />
                    <asp:TemplateField HeaderText="Ngân hàng">
                    <ItemTemplate>
                    <%# Eval("Bank1.Name") %>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                    <asp:BoundField DataField="Adddress" HeaderText="Địa chỉ" 
                        SortExpression="Adddress" />
                    <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
                    <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
                    <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
                </Columns>
            </asp:GridView>
            <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
                ContextTypeName="RedBloodDataContext" TableName="Banks" 
                Where="Level == @Level and ParentID == Guid?(@ParentID)" 
                EnableDelete="True" EnableUpdate="True">
                <WhereParameters>
                    <asp:Parameter DefaultValue="2" Name="Level" Type="Int32" />
                    <asp:ControlParameter ControlID="GridView1" Name="ParentID" 
                        PropertyName="SelectedValue" Type="Object" />
                </WhereParameters>
            </asp:LinqDataSource>
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" 
                Text="<%$ Resources:Resource,New %>" />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
