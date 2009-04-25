<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenu.master" AutoEventWireup="true"
    CodeFile="EnterTestResult.aspx.cs" Inherits="EnterTestResult" %>

<%@ Register Src="~/UserControl/People.ascx" TagPrefix="ucpeople" TagName="people" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:TextBox ID="txtPackID" runat="server" Visible="false" Text="00000000-0000-0000-0000-000000000000"></asp:TextBox>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div>
                <img src="Image/indicator_mozilla_blu.gif" />
                Đang xử lý ...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="mainContent">
                <div style="float: left;">
                    <asp:Panel DefaultButton="" runat="server" ID="PanelSearch">
                        Nhập barcode túi máu:
                        <br />
                        <asp:TextBox runat="server" ID="txtCode" onkeyup="checkLength_CMND(this.value);"
                            OnTextChanged="txtCode_TextChanged" Width="200px"></asp:TextBox>
                    </asp:Panel>
                    <br />
                    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AutoGenerateRows="False"
                        DataKeyNames="ID" DataSourceID="LinqDataSource1">
                        <Fields>
                            <asp:BoundField DataField="CollectedDate" HeaderText="Ngày" SortExpression="CollectedDate"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Volume" HeaderText="(ml)" DataFormatString="{0:F0}" />
                            <asp:BoundField DataField="Note" HeaderText="Ghi chú" />
                        </Fields>
                    </asp:DetailsView>
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
                        TableName="Packs" Where="ID == Guid(@ID)" EnableUpdate="True">
                        <WhereParameters>
                            <asp:ControlParameter ControlID="txtPackID" DbType="Guid" Name="ID" PropertyName="Text"
                                DefaultValue="00000000-0000-0000-0000-000000000000" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                    <br />
                    <asp:GridView ID="GridViewTestResult" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="ID" DataSourceID="LinqDataSourceTestResult" ShowHeader="False"
                        OnRowUpdating="GridViewTestResult_RowUpdating">
                        <Columns>
                            <asp:BoundField DataField="TrName1" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtTrID1" Text='<%# Bind("ID")%>' Visible="false"></asp:TextBox>
                                    <asp:DropDownList runat="server" ID="ddlTR2" DataSourceID="LinqDataSourceTR2" DataTextField="Name"
                                        DataValueField="ID" SelectedValue='<%# Bind("Tr2.ID2")%>' AppendDataBoundItems="true"
                                        OnSelectedIndexChanged="ddlTR2_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="--Chọn kết quả--" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:LinqDataSource ID="LinqDataSourceTR2" runat="server" ContextTypeName="RedBloodDataContext"
                                        TableName="TestResults" Where="Level == @Level and ParentID = Guid(@ParentID)">
                                        <WhereParameters>
                                            <asp:Parameter DefaultValue="2" Name="Level" Type="Int32" />
                                            <asp:ControlParameter ControlID="txtTrID1" DbType="Guid" Name="ParentID" PropertyName="Text"
                                                DefaultValue="{00000000-0000-0000-0000-000000000000}" />
                                        </WhereParameters>
                                    </asp:LinqDataSource>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValue" runat="server" Text='<%# Bind("Tr2.Value")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNote" runat="server" Text='<%# Bind("Tr2.Note")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqDataSourceTestResult" runat="server" ContextTypeName="RedBloodDataContext"
                        EnableUpdate="True" TableName="PointDefs" OnSelecting="LinqDataSourceTestResult_Selecting">
                    </asp:LinqDataSource>
                </div>
                <div style="float: left;">
                    <ucpeople:people runat="server" ID="People1" PanelSearchVisible="false" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function checkLength_CMND(text) {
            if (text.length == 34) {
                document.forms[0].submit();
            }
        }

    </script>

</asp:Content>
