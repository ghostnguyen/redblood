﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ExtractFFPlasmaInList.aspx.cs" Inherits="Production_ExtractFFPlasmaInList" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td align="center">
                <div runat="server" id="divExtract">
                    Sản xuất:
                    <asp:CheckBoxList runat="server" ID="CheckBoxListExtractTo" DataValueField="ID" DataTextField="Name"
                        RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxListExtractTo_SelectedIndexChanged">
                    </asp:CheckBoxList>
                    <asp:Button runat="server" ID="btnExtract" Text="Sản xuất" OnClick="btnExtract_Click" />
                    <hr />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="position: relative; height: 700px;">
                    <div id="divFull" runat="server">
                        <asp:GridView runat="server" ID="GridViewFull" AutoGenerateColumns="False" DataKeyNames="Autonum"
                            OnRowCommand="GridViewFull_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Toàn phần">
                                    <ItemTemplate>
<%--                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tủa lạnh">
                                    <ItemTemplate>
<%--                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("CanExtractToFactorVIII") as int?) %>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Huyết tương dự trữ">
                                    <ItemTemplate>
<%--                                        <asp:Image ID="Image6" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("CanExtractToFFPlasma_Poor") as int?) %>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Loại" CommandName="Remove"
                                            CommandArgument='<%# Eval("Autonum") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
