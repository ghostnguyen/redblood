<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EnterPack.ascx.cs" Inherits="UserControl_EnterPack" %>
<%@ Register Src="~/UserControl/CampaignDetail.ascx" TagPrefix="uc" TagName="CamDetail" %>

<div class="part">
    <div class="partHeader">
        Thu máu
    </div>
    <div class="partLink">
        <asp:Image ID="ImageCodabar" runat="server" ImageUrl="none" />
        <br />
        <asp:Button ID="btnRemove" runat="server" OnClientClick="return confirm('Nhập lại túi máu?')"
            Text="Nhập lại túi máu" Visible="false" OnClick="btnRemove_Click"></asp:Button>
        <asp:Button ID="btnDelete" runat="server" OnClientClick="return confirm('Hủy túi máu?')"
            OnClick="btnDelete_Click" Text="Hủy túi máu" Visible="false"></asp:Button>
        <br />
        <asp:Label ID="lblPackMsg" runat="server" ForeColor="red"></asp:Label>
    </div>
    <div class="partLink" style="visibility:collapse;">
        <table width="100%" >
            <tr>
                <td>
                    Thành phần<br />
                    <asp:DropDownList ID="DropDownListComponent" runat="server" DataSourceID="LinqDataSourceComponent"
                        DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" CssClass="packProperty"
                        AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="DropDownListComponent_SelectedIndexChanged">
                        <asp:ListItem Text="" Value="0" />
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="LinqDataSourceComponent" runat="server" ContextTypeName="RedBloodDataContext"
                        TableName="TestDefs" Where="ID == 25 || ID == 30">
                    </asp:LinqDataSource>
                </td>
                <td>
                    Thể tích (ml)<br />
                    <asp:DropDownList ID="DropDownListVolume" runat="server" DataSourceID="LinqDataSourceVolume"
                        DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" CssClass="packProperty"
                        AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="DropDownListVolume_SelectedIndexChanged">
                        <asp:ListItem Text="" Value="0" />
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="LinqDataSourceVolume" runat="server" ContextTypeName="RedBloodDataContext"
                        TableName="TestDefs" Where="ParentID == 31">
                    </asp:LinqDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border-bottom: dotted 1px; padding: 0px 0px 8px 0px;">
                    <asp:Button ID="btnCommitWithout" runat="server" OnClientClick="return confirm('Xác nhận thu máu (không ABO test)?');"
                        Visible="false" Text="Xác nhận (không ABO test)" OnClick="btnCommitWithout_Click">
                    </asp:Button>
                    <br />
                    <asp:Label ID="Label1" runat="server" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    ABO<br />
                    <asp:DropDownList ID="DropDownListABO" runat="server" DataSourceID="LinqDataSource1"
                        DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" CssClass="packProperty"
                        AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="DropDownListABO_SelectedIndexChanged">
                        <asp:ListItem Text="" Value="0" />
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
                        TableName="TestDefs" Where="ParentID == @ParentID">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="1" Name="ParentID" Type="Int32" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                </td>
                <td>
                    RH<br />
                    <asp:DropDownList ID="DropDownListRH" runat="server" DataSourceID="LinqDataSource2"
                        DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" CssClass="packProperty"
                        AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="DropDownListRH_SelectedIndexChanged">
                        <asp:ListItem Text="" Value="0" />
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="RedBloodDataContext"
                        TableName="TestDefs" Where="ParentID == @ParentID">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="6" Name="ParentID" Type="Int32" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnCommit" runat="server" OnClientClick="return confirm('Xác nhận thu máu (có ABO test)?');"
                        Visible="false" Text="Xác nhận (có ABO test)" OnClick="btnCommit_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <div class="partLinkLast">
        <div class="partHeader">
            Túi máu thu trong đợt
        </div>
        <div class="partLinkLast">
        <uc:CamDetail runat="server" ID="ucCampaign" />
        
        </div>
    </div>
</div>
