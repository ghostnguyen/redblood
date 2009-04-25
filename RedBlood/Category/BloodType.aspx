<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="BloodType.aspx.cs" Inherits="Category_BloodType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h4>
                Danh mục nhóm máu.
            </h4>
            <div class="mainContent">
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
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
                        EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="BloodTypes"
                        OrderBy="Name">
                    </asp:LinqDataSource>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
