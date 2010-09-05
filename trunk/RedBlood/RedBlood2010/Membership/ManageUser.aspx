<%@ Page Language="C#" MasterPageFile="~/MasterPageAdminMenu.master" AutoEventWireup="true" Inherits="Membership_ManageUser" Title="Untitled Page" Codebehind="ManageUser.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <p align="center">
        <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
    </p>
    <asp:GridView ID="UserAccounts" runat="server" AutoGenerateColumns="False" DataKeyNames="UserId"
        DataSourceID="LinqDataSource1" OnRowUpdating="UserAccounts_RowUpdating" 
        AllowSorting="True" onrowdeleting="UserAccounts_RowDeleting">
        <Columns>
            <asp:CommandField ShowEditButton="True" EditText="<%$ Resources:Resource,Edit %>" CancelText="<%$ Resources:Resource,Cancel %>" 
                UpdateText="<%$ Resources:Resource,Update %>" ShowDeleteButton="True" DeleteText="<%$ Resources:Resource,Delete %>" />
            <asp:BoundField DataField="UserName" HeaderText="Tên đăng nhập" SortExpression="UserName"
                ReadOnly="true" />
            <asp:TemplateField HeaderText="Họ và tên">
                <ItemTemplate>
                    <%#Eval("aspnet_UserProfile.Fullname")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFullname" runat="server" Text='<%#Eval("aspnet_UserProfile.Fullname")%>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <%#Eval("aspnet_Membership.Email")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Text='<%#Eval("aspnet_Membership.Email")%>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Số điện thoại">
                <ItemTemplate>
                    <%#Eval("aspnet_UserProfile.Phone")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPhone" runat="server" Text='<%#Eval("aspnet_UserProfile.Phone")%>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tài khoản bị khóa">
                <ItemTemplate>
                    <asp:CheckBox Checked='<%#Eval("aspnet_Membership.IsApproved")%>' runat="server"
                        Enabled="false"></asp:CheckBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="chkIsApproved" Checked='<%#Eval("aspnet_Membership.IsApproved")%>'
                        runat="server"></asp:CheckBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ghi chú">
                <ItemTemplate>
                    <%#Eval("aspnet_Membership.Comment")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtComment" runat="server" Text='<%#Eval("aspnet_Membership.Comment")%>' />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBlood.RedBloodDataContext"
        EnableUpdate="True" TableName="aspnet_Users" OrderBy="UserName" 
        EnableDelete="True">
    </asp:LinqDataSource>
</asp:Content>
