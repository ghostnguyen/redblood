<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Order4Org.ascx.cs" Inherits="UserControl_Order4Org" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<%@ Register Src="~/UserControl/PeopleOrder.ascx" TagPrefix="uc" TagName="PeopleOrder" %>
<asp:Panel runat="server" ID="Panel1">

    <script type="text/javascript">
        // Your code goes here
        function txtRemoveNoteKeyUp(text) {
            $("input[name*='txtRemoveNoteGlobal']").val(text);
        }

        //
        //        function PanelOnKeyPress(args) {
        //            if (args.keyCode == Sys.UI.Key.esc) {
        //                alert($("id[name*='ModalPopupExtender1']").value);
        //                //$("input[name*='ModalPopupExtender1']").hide();
        //                //$("input[id*='ModalPopupExtender1']").css("color", "red");
        //            }
        //        }

        //        function pageLoad(sender, args) {
        //            $addHandler(document, "keydown", PanelOnKeyPress);
        //        }


        
    </script>

    <%--<asp:TextBox ID="txtRemoveNoteGlobal" runat="server" Style="visibility: collapse;"></asp:TextBox>--%>
    <table>
        <tr valign="top">
            <td>
                <div class="img_codabar" style="width: 190px;">
                    <asp:Image ID="imgCodabar" runat="server" ImageUrl="none" />
                </div>
            </td>
            <td>
                <table>
                    <%-- <tr>
                        <td>
                            Tên
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="290" />
                        </td>
                    </tr>--%>
                    <tr runat="server" id="rowOrg">
                        <td>
                            Đơn vị nhận
                        </td>
                        <td>
                            <asp:TextBox ID="txtOrgName" runat="server" CssClass="campaign_cellvalue" autocomplete="off" />
                            <ajk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtOrgName"
                                ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListOrg" MinimumPrefixLength="3"
                                CompletionSetCount="15" EnableCaching="true">
                            </ajk:AutoCompleteExtender>
                            <div id="divErrOrgName" runat="server" class="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ghi chú
                        </td>
                        <td>
                            <asp:TextBox ID="txtNote" runat="server" Width="250" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ngày giờ
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" Width="100" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="dotLineBottom" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="ImageCurrentDIN" runat="server" ImageUrl="none" />
                <br />
                Danh sách túi máu
                <asp:GridView ID="GridViewPack" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSourcePack" OnRowUpdating="GridViewPack_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Túi máu">
                            <ItemTemplate>
                                <asp:Image ID="ImagePackCodabar" runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN( Eval("Pack.DIN") as string) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sản phẩm">
                            <ItemTemplate>
                                <asp:Image ID="ImagePackCodabar" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product( Eval("Pack.ProductCode") as string) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSourcePack" runat="server" ContextTypeName="RedBloodDataContext"
                    TableName="PackOrders" OnSelecting="LinqDataSourcePack_Selecting" EnableDelete="True"
                    EnableUpdate="True">
                </asp:LinqDataSource>
            </td>
        </tr>
        <tr>
            <td class="dotLineBottom" colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Resource,Update %>"
                    OnClick="btnUpdate_Click" />
            </td>
        </tr>
        <%--        <tr>
            <td class="dotLineBottom">
            </td>
            <td class="dotLineBottom">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Đơn vị nhận
                <asp:TextBox ID="txtOrgName" runat="server" CssClass="campaign_cellvalue" autocomplete="off" />
                <ajk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtOrgName"
                    ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListOrg" MinimumPrefixLength="3"
                    CompletionSetCount="15" EnableCaching="true">
                </ajk:AutoCompleteExtender>
                <div id="divErrOrgName" runat="server" class="hidden" />
            </td>
        </tr>
        --%>
    </table>
</asp:Panel>
