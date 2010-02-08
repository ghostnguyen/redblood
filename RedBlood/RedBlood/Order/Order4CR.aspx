<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Order4CR.aspx.cs" Inherits="Order_Order4CR" %>

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

    <h4>
        Cấp phát cho bệnh viện
        <asp:Button ID="btnNew4CR" runat="server" Text="Tạo đợt mới" OnClick="btnNew4CR_Click"
            ToolTip="Ctrl+M" />
        <asp:TextBox ID="txtRemoveNoteGlobal" runat="server" Style="visibility: collapse;"></asp:TextBox>
    </h4>
    <table>
        <tr valign="top" align="left">
            <td>
                <table>
                    <tr valign="top">
                        <td>
                            <div class="img_codabar" style="width: 140px;">
                                <asp:Image ID="imgCodabar" runat="server" ImageUrl="none" />
                            </div>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Ngày cấp
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
                        <td colspan="2">
                            <uc:People runat="server" ID="People1" HideMoreDetail="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="dotLineBottom" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        Khoa
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDept" Width="299" runat="server"></asp:TextBox>
                                        <ajk:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtDept"
                                            ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListDepartment" MinimumPrefixLength="3"
                                            CompletionSetCount="15" EnableCaching="true">
                                        </ajk:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Phòng
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRoom" Width="30" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Giường
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBed" Width="30" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Chuẩn đoán bệnh
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiagnosis" Width="299" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mã nhập viện
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatientCode" Width="299" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ghi chú truyền máu
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTransfusionNote" Width="299" runat="server"></asp:TextBox>
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
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Resource,Update %>"
                                OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Resource,Delete %>"
                                OnClick="btnDelete_Click" OnClientClick="return confirm('Xóa đợt hiến máu này.');" />
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
                    <asp:Image ID="ImageCurrentDIN" runat="server" ImageUrl="none" />
                </div>
                <br />
                Danh sách cấp phát
                <br />
                <asp:GridView ID="GridViewPack" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSourcePack" OnRowUpdating="GridViewPack_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Túi máu">
                            <ItemTemplate>
                                <asp:Image ID="ImageDIN" runat="server" ImageUrl='<%# BarcodeBLL.Url4DIN( Eval("Pack.DIN") as string) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sản phẩm">
                            <ItemTemplate>
                                <asp:Image ID="ImagePackCodabar" runat="server" ImageUrl='<%# BarcodeBLL.Url4Product( Eval("Pack.ProductCode") as string) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" EditText="Thu hồi" UpdateText="Ok" CancelText='<%$ Resources:Resource,Cancel %>' />
                        <asp:TemplateField HeaderText="">
                            <EditItemTemplate>
                                Lý do:
                                <asp:TextBox ID="txtRemoveNote" runat="server" onkeyup="txtRemoveNoteKeyUp(this.value);"></asp:TextBox>
                            </EditItemTemplate>
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
