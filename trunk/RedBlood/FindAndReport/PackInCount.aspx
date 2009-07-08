<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PackInCount.aspx.cs" Inherits="FindAndReport_PackInCount" %>
<%@ Register Src="~/UserControl/PackCountByProvince.ascx" TagPrefix="uc" TagName="PackCountByProvince" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table width="100%">
        <tr valign="top">
            <td style="width: 210px;">
                <div class="part">
                    <div class="partHeader">
                        Tìm
                    </div>
                    <div class="partLinkLast">
                        <%--<asp:Button runat="server" ID="btnFind" Text="Tìm" OnClick="btnFind_Click" />--%>
                    </div>
                </div>
                <div class="part">
                    <div class="partHeader">
                        Thời gian
                    </div>
                    <div class="partLinkLast">
                        Từ ngày
                        <asp:TextBox runat="server" ID="txtFrom" Width="150px"></asp:TextBox>
                        <ajk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom"
                            Format="dd/MM/yyyy">
                        </ajk:CalendarExtender>
                        <br />
                        Tới ngày
                        <asp:TextBox runat="server" ID="txtTo" Width="150px"></asp:TextBox>
                        <ajk:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo"
                            Format="dd/MM/yyyy">
                        </ajk:CalendarExtender>
                    </div>
                </div>
                <div class="part">
                    <div class="partHeader">
                        Tỉnh/thành phố
                    </div>
                    <div class="partLinkLast">
                        <asp:CheckBoxList runat="server" DataTextField="Name" DataValueField="ID" ID="CheckBoxListGeo1">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="part">
                    <div class="partHeader">
                        Nguồn thu
                    </div>
                    <div class="partLinkLast">
                        <asp:CheckBoxList runat="server" DataTextField="Name" DataValueField="ID" ID="CheckBoxListSource">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </td>
            <td>
               <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSource1">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" InsertVisible="False"
                            ReadOnly="True" />
                        <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label4" runat="server" Text="Ngày"></asp:Label>
                                <br />
                                <asp:Label ID="Label3" runat="server" Text="Nguồn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                <br />
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Source.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label4" runat="server" Text="ĐVPH"></asp:Label>
                                <br />
                                <asp:Label ID="Label3" runat="server" Text="ĐVTC"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("CoopOrg.Name") %>'></asp:Label>
                                <br />
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("HostOrg.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Est" HeaderText="Dự kiến" SortExpression="Est" />
                        <asp:BoundField DataField="CountPack" HeaderText="TC" />
                        <asp:BoundField DataField="CountPack450" HeaderText="450" />
                        <asp:BoundField DataField="CountPack350" HeaderText="350" />
                        <asp:BoundField DataField="CountPack250" HeaderText="250" />
                        <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
                        
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
                    EnableUpdate="True" TableName="Campaigns" OnSelecting="LinqDataSource1_Selecting">
                </asp:LinqDataSource>--%>
            </td>
        </tr>
    </table>
</asp:Content>

