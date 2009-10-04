<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FindPeople.aspx.cs" Inherits="FindAndReport_FindPeople" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr valign="top">
            <td style="width: 210px;">
                <div class="part">
                    <div class="partHeader">
                        Điều kiện lọc
                    </div>
                    <div class="partLinkLast">
                        <asp:BulletedList ID="BulletedListFilter" runat="server" DisplayMode="LinkButton"
                            CssClass="noindent" OnClick="BulletedListFilter_Click">
                        </asp:BulletedList>
                    </div>
                </div>
                <div class="part">
                    <div class="partHeader">
                        Giới tính
                    </div>
                    <div class="partLinkLast">
                        <asp:BulletedList ID="BulletedListSex" runat="server" DisplayMode="LinkButton" CssClass="noindent"
                            OnClick="BulletedListSex_Click">
                        </asp:BulletedList>
                    </div>
                </div>
                <div class="part">
                    <div class="partHeader">
                        Năm sinh
                    </div>
                    <div class="partLinkLast">
                        <asp:BulletedList ID="BulletedListDOB" runat="server" DisplayMode="LinkButton" CssClass="noindent"
                            OnClick="BulletedListDOB_Click">
                        </asp:BulletedList>
                    </div>
                </div>
                <div class="part">
                    <div class="partHeader">
                        Nguyên quán
                    </div>
                    <div class="partLinkLast">
                        <asp:BulletedList ID="BulletedListGeo1" runat="server" DisplayMode="LinkButton" CssClass="noindent"
                            OnClick="BulletedListGeo1_Click">
                        </asp:BulletedList>
                    </div>
                </div>
            </td>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSource1">
                    <Columns>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:HyperLink ID="Label1" runat="server" Text='<%# Eval("Name")%>' NavigateUrl='<%# RedBloodSystem.Url4PeopleDetail + "key=" + Eval("ID")  %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CMND" HeaderText="CMND" SortExpression="CMND" />
                        <asp:BoundField DataField="DOB" HeaderText="Năm sinh" SortExpression="DOB" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Giới tính">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Sex.Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
                        <asp:BoundField DataField="FullResidentalAddress" HeaderText="Địa chỉ" ReadOnly="True"
                            SortExpression="FullResidentalAddress" />
                        <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
                    EnableUpdate="True" OnSelecting="LinqDataSource1_Selecting" TableName="Peoples">
                </asp:LinqDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
