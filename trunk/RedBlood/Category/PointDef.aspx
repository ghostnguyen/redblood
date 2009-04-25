<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="PointDef.aspx.cs" Inherits="Category_PointDef" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="mainContent">
                <div class="leftBigPart">
                    <div class="part">
                        <div class="partHeader">
                            Tạo mới
                        </div>
                        Tên điểm thưởng
                        <br />
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <asp:Button ID="ButtonNew" runat="server" Text="Tạo" 
                            onclick="ButtonNew_Click" />
                    </div>
                </div>
                <div runat="server" class="rightBigPart">
                    <asp:GridView ID="GridViewPointDef" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                        DataSourceID="LinqDataSourcePointDef" 
                        OnRowDataBound="GridViewPointDef_RowDataBound" 
                        onrowcommand="GridViewPointDef_RowCommand">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" ButtonType="Link" EditImageUrl="~/Image/Icon/Edit.png"
                                UpdateImageUrl="~/Image/Icon/Update.png" CancelImageUrl="~/Image/Icon/Cancel.png"
                                EditText="<%$ Resources:Resource,Edit %>" UpdateText="<%$ Resources:Resource,Update %>"
                                CancelText="<%$ Resources:Resource,Cancel %>" ShowDeleteButton="True" DeleteText="<%$ Resources:Resource,Delete %>" />
                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnStatus" runat="server" CommandName="ChangeStatus" CommandArgument='<%# Eval("ID") %>' />
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqDataSourcePointDef" runat="server" ContextTypeName="RedBloodDataContext"
                        EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="PointDefs">
                    </asp:LinqDataSource>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
