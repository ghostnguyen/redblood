<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Category_Sex" Codebehind="Sex.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainContent">
        <h4>
            Danh mục giới tính.
        </h4>
        <div class="leftBigPart">
            <div class="part">
                <div class="partHeader">
                    Tạo mới
                </div>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:Button ID="ButtonNew" runat="server" Text="Tạo" OnClick="ButtonNew_Click" />
            </div>
        </div>
        <div id="Div1" runat="server" class="rightBigPart">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                DataSourceID="LinqDataSource1">
                <Columns>
                    <asp:CommandField ShowEditButton="True" ButtonType="Link" EditImageUrl="~/Image/Icon/Edit.png"
                        UpdateImageUrl="~/Image/Icon/Update.png" CancelImageUrl="~/Image/Icon/Cancel.png"
                        EditText="<%$ Resources:Resource,Edit %>" UpdateText="<%$ Resources:Resource,Update %>"
                        CancelText="<%$ Resources:Resource,Cancel %>" ShowDeleteButton="True" DeleteText="<%$ Resources:Resource,Delete %>" />
                    <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                </Columns>
            </asp:GridView>
            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
                EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="Sexes"
                OrderBy="Name">
            </asp:LinqDataSource>
        </div>
    </div>
</asp:Content>
