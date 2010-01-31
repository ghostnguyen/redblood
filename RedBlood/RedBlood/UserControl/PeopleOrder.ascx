<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PeopleOrder.ascx.cs" Inherits="UserControl_PeopleOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>

<script type="text/javascript">
    // Your code goes here
    $(document).bind('keydown', 'Ctrl+l', function() {
        $("input[name*='btnUpdate']").click();
    });
</script>

<asp:Panel runat="server" ID="Panel1">
    <table>
        <tr>
            <td class="people_cellheader">
                <asp:Literal style="color: Red;">*</asp:Literal>Tên
            </td>
            <td class="people_cellvalue">
                <asp:TextBox ID="txtName" runat="server" CssClass="people_cellvalue" Font-Size="Medium"
                    autocomplete="off" />
                <div id="divErrName" runat="server" class="hidden">
                </div>
            </td>
        </tr>
        <tr>
            <td class="people_cellheader">
                CMND
            </td>
            <td>
                <asp:TextBox ID="txtCMND" runat="server" CssClass="people_cellvalue" />
                <div id="divErrCMND" runat="server" class="hidden">
                </div>
            </td>
        </tr>
        <tr>
            <td class="people_cellheader">
                Ngày sinh
            </td>
            <td>
                <asp:TextBox ID="txtDOB" runat="server" />
                <div id="divErrDOB" runat="server" class="hidden">
                </div>
            </td>
        </tr>
        <tr>
            <td class="people_cellheader">
                Giới tính
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlSex" DataSourceID="LinqDataSourceSex" DataTextField="Name"
                    DataValueField="ID" AppendDataBoundItems="true">
                    <asp:ListItem Text="--Chọn giới tính--" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqDataSourceSex" runat="server" ContextTypeName="RedBloodDataContext"
                    TableName="Sexes">
                </asp:LinqDataSource>
                <div id="divErrSex" runat="server" class="hidden">
                </div>
            </td>
        </tr>
        <tr>
            <td class="dotLineBottom">
            </td>
            <td class="dotLineBottom">
            </td>
        </tr>
        <tr>
            <td class="people_cellheader">
                Địa chỉ
            </td>
            <td>
                <asp:TextBox ID="txtResidentAddress" runat="server" CssClass="people_cellvalue" />
                <br />
                P./Xã, Q./Huyện, Tỉnh/Tp
                <br />
                <asp:TextBox ID="txtResidentGeo" runat="server" autocomplete="off" CssClass="people_cellvalue"></asp:TextBox>
                <ajk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtResidentGeo"
                    ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListGeo" MinimumPrefixLength="3"
                    CompletionSetCount="15" EnableCaching="true">
                </ajk:AutoCompleteExtender>
                <div id="divErrResidentalGeo" runat="server" class="hidden">
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
