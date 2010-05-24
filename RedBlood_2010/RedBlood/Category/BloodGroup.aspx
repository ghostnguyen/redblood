<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="BloodGroup.aspx.cs" Inherits="Category_BloodGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
            <div class="mainContent">
                <h4>
                    Nhóm máu
                </h4>
                <div id="Div1" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand"
                        DataSource='<%# BloodGroup.BloodGroupList %>'>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image runat="server" ImageUrl='<%# BarcodeBLL.Url4BloodGroup( Eval("Code") as string) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="Ghi chú" />
                            <asp:TemplateField HeaderText="Số lượng">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCount" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button runat="server" CommandName="Select" Text="In mã" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
     
</asp:Content>
