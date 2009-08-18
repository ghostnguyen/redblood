<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PackSideEffect.ascx.cs"
    Inherits="UserControl_PackSideEffect" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<div>
    <asp:Panel runat="server" ID="Panel1">
        <h4>
            Phản ứng truyền máu
        </h4>
        <asp:Image ID="ImageCurrentDIN" runat="server" ImageUrl="none" />
        <asp:Image ID="ImageProduct" runat="server" ImageUrl="none" />
        <br />
        Triệu chứng
        <br />
        <asp:TextBox runat="server" ID="txtSideEffect" Width="420px"></asp:TextBox>
        <ajk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSideEffect"
            ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListSideEffect" MinimumPrefixLength="3"
            CompletionSetCount="15" EnableCaching="true">
        </ajk:AutoCompleteExtender>
        <br />
        Ghi chú
        <br />
        <asp:TextBox runat="server" ID="txtNote" Width="420px"></asp:TextBox>
        <br />
        <asp:Button runat="server" ID="btnOk" Text="Ghi nhận" OnClick="btnOk_Click" />
        <div id="divErr" runat="server" class="hidden">
        </div>
    </asp:Panel>
    <asp:GridView ID="GridViewSideEffect" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="ID" DataSourceID="LinqDataSourceSideEffect">
        <Columns>
            <asp:BoundField DataField="FullSideEffect" HeaderText="Triệu chứng" ReadOnly="True"
                SortExpression="FullSideEffect" />
            <asp:BoundField DataField="Date" HeaderText="Ngày" SortExpression="Date" DataFormatString="{0:dd/mm/yyyy}" />
            <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSourceSideEffect" runat="server" ContextTypeName="RedBloodDataContext"
        EnableUpdate="True" OnSelecting="LinqDataSourceSideEffect_Selecting" TableName="PackSideEffects"
        EnableDelete="True">
    </asp:LinqDataSource>
</div>
