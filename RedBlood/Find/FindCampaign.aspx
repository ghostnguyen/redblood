<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFind.master" AutoEventWireup="true"
    CodeFile="FindCampaign.aspx.cs" Inherits="Find_FindCampaign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr valign="top">
            <td style="width: 210px;">
                <div class="part">
                    <div class="partHeader">
                        Tìm
                    </div>
                    <div class="partLinkLast">
                        <asp:Button runat="server" ID="btnFind" Text="Tìm" onclick="btnFind_Click" />
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
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSource1">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" 
                            InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                        <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
                        <asp:BoundField DataField="ContactName" HeaderText="ContactName"
                            SortExpression="ContactName" />
                        <asp:BoundField DataField="ContactPhone" HeaderText="ContactPhone" 
                            SortExpression="ContactPhone" />
                        <asp:BoundField DataField="ContactTitle" HeaderText="ContactTitle" 
                            SortExpression="ContactTitle" />
                        <asp:BoundField DataField="SourceID" HeaderText="SourceID" 
                            SortExpression="SourceID" />
                        <asp:BoundField DataField="Est" HeaderText="Est" SortExpression="Est" />
                        <asp:BoundField DataField="CoopOrgID" HeaderText="CoopOrgID" 
                            SortExpression="CoopOrgID" />
                        <asp:BoundField DataField="HostOrgID" HeaderText="HostOrgID" 
                            SortExpression="HostOrgID" />
                        <asp:BoundField DataField="NameNoDiacritics" HeaderText="NameNoDiacritics" 
                            SortExpression="NameNoDiacritics" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
                    EnableUpdate="True" TableName="Campaigns" 
                    onselecting="LinqDataSource1_Selecting">
                </asp:LinqDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
