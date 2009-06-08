<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Order.ascx.cs" Inherits="UserControl_Order" %>
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

    <asp:TextBox ID="txtRemoveNoteGlobal" runat="server" Style="visibility: collapse;"></asp:TextBox>
    <table>
        <tr valign="top">
            <td>
                <div class="img_codabar" style="width: 120px;">
                    <asp:Image ID="imgCodabar" runat="server" ImageUrl="none" />
                </div>
            </td>
            <td>
                <table>
                    <tr>
                        <td colspan="2">
                            <div>
                                <span style="float: left;">Tên&nbsp;</span>
                                <asp:TextBox ID="txtName" runat="server" Width="290" Style="float: left;" />
                                <div id="divErrName" runat="server" class="hidden" style="float: left;" />
                                <span style="float: left;">Ngày giờ&nbsp;</span>
                                <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" Width="100" Style="float: left;" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <div id="divErrDate" runat="server" class="hidden" />
                            </div>
                        </td>
                        <td>
                            <div>
                                Ghi chú
                                <asp:TextBox ID="txtNote" runat="server" Width="250" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="dotLineBottom" colspan="2">
            </td>
        </tr>
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
        <div runat="server" id="rowPeople">
            <tr>
                <td>
                    Người nhận
                </td>
                <td>
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
        </div>
        <%--<tr runat="server" id="rowPeople">
            <td>
                Người nhận
            </td>
            <td>
                <uc:PeopleOrder runat="server" ID="PeopleOrder1" />
            
            </td>
        </tr>--%>
        <tr>
            <td class="dotLineBottom" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Danh sách túi máu
                <asp:GridView ID="GridViewPack" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    DataSourceID="LinqDataSourcePack" OnRowCommand="GridViewPack_RowCommand" OnRowDeleting="GridViewPack_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Túi máu">
                            <ItemTemplate>
                                <asp:Image ID="ImagePackCodabar" runat="server" ImageUrl='<%# this.GetItemUrl( Eval("Pack.Autonum") as int?) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Túi máu">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Pack.Autonum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OrderID" HeaderText="OrderID" SortExpression="OrderID" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="LinkButtonDelete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>'
                                    Text='<%$ Resources:Resource,Delete %>'>
                                </asp:LinkButton>
                                <ajk:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="LinkButtonDelete"
                                    PopupControlID="PanelPeople" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                </ajk:ModalPopupExtender>
                                <asp:Panel runat="server" ID="PanelPeople" Style="display: none;" CssClass="modalPopup">
                                    Lý do hủy:
                                    <asp:TextBox runat="server" ID="txtRemoveNote" onkeyup="txtRemoveNoteKeyUp(this.value);"> </asp:TextBox>
                                    <div class="dotLineBottom" style="width: 100%;">
                                    </div>
                                    <asp:Button runat="server" ID="btnSelect" Text='<%$ Resources:Resource,Delete %>'
                                        OnClick="btnSelect_Click" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' />
                                    <asp:Button runat="server" ID="btnClose" Text='<%$ Resources:Resource,Close %>' />
                                </asp:Panel>
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
</asp:Panel>
