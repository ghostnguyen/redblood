<%@ Control Language="C#" AutoEventWireup="true" CodeFile="People.ascx.cs" Inherits="UserControl_People" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajk" %>

<script type="text/javascript">
    // Your code goes here
    $(document).bind('keydown', 'Ctrl+l', function() {
        $("input[name*='btnUpdate']").click();
    });
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel runat="server" ID="Panel1">
            <table>
                <tr>
                    <td class="img_codabar" colspan="2" align="right">
                        <asp:Image ID="imgCodabar" runat="server" ImageUrl="none" />
                    </td>
                </tr>
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
                        <asp:Literal style="color: Red;">*</asp:Literal>Ngày sinh
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
                <div runat="server" id="divMoreDetail">
                    <tr>
                        <td class="dotLineBottom">
                        </td>
                        <td class="dotLineBottom">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkEnableMaillingAddress" runat="server" Text="Thêm địa chỉ liên lạc."
                                OnCheckedChanged="chkEnableMaillingAddress_CheckedChanged" AutoPostBack="true" />
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
                            <br />
                            liên lạc
                        </td>
                        <td>
                            <asp:Panel runat="server" ID="panelMaillingAddress">
                                <asp:TextBox ID="txtMailingAddress" runat="server" CssClass="people_cellvalue" Enabled="false" />
                                <br />
                                P./Xã, Q./Huyện, Tỉnh/Tp
                                <br />
                                <asp:TextBox ID="txtMailingGeo" runat="server" autocomplete="off" CssClass="people_cellvalue"
                                    Enabled="false"></asp:TextBox>
                                <ajk:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtMailingGeo"
                                    ServicePath="~/AutoComplete.asmx" ServiceMethod="GetListGeo" MinimumPrefixLength="3"
                                    CompletionSetCount="15" EnableCaching="true">
                                </ajk:AutoCompleteExtender>
                                <div id="divErrMailingGeo" runat="server" class="hidden">
                                </div>
                            </asp:Panel>
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
                            Nghề nghiệp
                        </td>
                        <td>
                            <asp:TextBox ID="txtJob" runat="server" CssClass="people_cellvalue" />
                        </td>
                    </tr>
                    <tr>
                        <td class="people_cellheader">
                            Email
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="people_cellvalue" />
                        </td>
                    </tr>
                    <tr>
                        <td class="people_cellheader">
                            Điện thoại
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="people_cellvalue" />
                        </td>
                    </tr>
                    <tr>
                        <td class="people_cellheader">
                            Ghi chú
                        </td>
                        <td>
                            <asp:TextBox ID="txtNote" runat="server" CssClass="people_cellvalue" TextMode="MultiLine" />
                        </td>
                    </tr>
                </div>
                <tr>
                    <td class="dotLineBottom">
                    </td>
                    <td class="dotLineBottom">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Resource,Update %>"
                            OnClick="btnUpdate_Click" ToolTip="Ctrl+L" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
