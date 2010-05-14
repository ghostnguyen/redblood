<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Return.aspx.cs" Inherits="Store_Return" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>
<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<%@ Register Src="~/UserControl/PeopleOrder.ascx" TagPrefix="uc" TagName="PeopleOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here
        $(document).bind('keydown', 'Ctrl+m', function() {
            $("input[id*='btnNew']").click();
        });


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

    <h4>
        Thu hồi máu
        <asp:Button ID="btnNewReturn" runat="server" Text="Tạo đợt mới" OnClick="btnNewReturn_Click"
            ToolTip="Ctrl+M" />
    </h4>
    <table>
        <tr valign="top" align="left">
            <td>
                <table>
                    <tr valign="top">
                        <td>
                            <div class="img_codabar" style="width: 140px;">
                                <asp:Image ID="imgOrder" runat="server" ImageUrl="none" />
                            </div>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Ngày thu hồi
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" Width="100" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ghi chú
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNote" runat="server" Width="180" />
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
                        <td class="dotLineBottom" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnOk" runat="server" Text="<%$ Resources:Resource,Update %>" OnClick="btnOk_Click" OnClientClick="return confirm('Thu hồi?');" />
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
            </td>
            <td>
                Mã túi máu đang quét
                <br />
                <div style="height: 30px;">
                    <asp:Image ID="imgCurrentDIN" runat="server" ImageUrl="none" />
                </div>
                <br />
                Danh sách thu hồi
                <br />
                <asp:GridView ID="GridViewPack" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSourcePack">
                    <Columns>
                        <asp:BoundField HeaderText="Đợt cấp phát" DataField="OrderID" />
                        <asp:BoundField HeaderText="Cho" DataField="OrderType" />
                        <asp:TemplateField HeaderText="Túi máu">
                            <ItemTemplate>
                                <asp:Image ID="ImageDIN" runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN( Eval("DIN") as string) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sản phẩm">
                            <ItemTemplate>
                                <asp:Image Style="margin-left: 10px;" ID="ImagePackCodabar" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product( Eval("ProductCode") as string) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hết hạn">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked='<%# Eval("Expired") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnRemove" runat="server" Text='<%$ Resources:Resource,Delete %>'
                                    OnClick="btnRemove_Click" CommandArgument='<%# Eval("ID")  %>' Visible='<%# this.ReturnID == 0 %>'  />
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
    </table>
</asp:Content>
