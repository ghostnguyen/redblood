<%@ MasterType VirtualPath="~/MasterPageUserMenuWithBarcode.master" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUserMenuWithBarcode.master"
    AutoEventWireup="true" CodeFile="Extract.aspx.cs" Inherits="Production_Extract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // Your code goes here
        //        function doLoadPackCombined() {
        //            if (confirm("Túi máu đã sản xuất. Xem chi tiết?")) {
        //                $("input[id*='btnLoad']").click();
        //            }
        //        }
    </script>

    <table width="100%" style="background: url(../../Image/PackExtractLayout.png);">
        <tr>
            <td>
                <div runat="server" id="divExtract">
                    Sản xuất:
                    <br />
                    <asp:CheckBoxList runat="server" ID="CheckBoxListExtractTo" DataValueField="ID" DataTextField="Name">
                    </asp:CheckBoxList>
                    <asp:Button runat="server" ID="btnExtract" Text="Sản xuất" OnClick="btnExtract_Click" />
                </div>
            </td>
            <td>
                <div runat="server" id="divFull">
                    <asp:DetailsView runat="server" ID="DetailsViewFull" AutoGenerateRows="False" DataKeyNames="ID">
                        <Fields>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codabar" HeaderText="Codabar" SortExpression="Codabar" />
                            <asp:BoundField DataField="PeopleID" HeaderText="PeopleID" SortExpression="PeopleID" />
                            <asp:BoundField DataField="CollectedDate" HeaderText="CollectedDate" SortExpression="CollectedDate" />
                            <asp:BoundField DataField="Volume" HeaderText="Volume" SortExpression="Volume" />
                            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
                            <asp:BoundField DataField="Autonum" HeaderText="Autonum" InsertVisible="False" SortExpression="Autonum" />
                            <asp:BoundField DataField="SourceID" HeaderText="SourceID" SortExpression="SourceID" />
                            <asp:BoundField DataField="ComponentID" HeaderText="ComponentID" SortExpression="ComponentID" />
                            <asp:BoundField DataField="Actor" HeaderText="Actor" SortExpression="Actor" />
                            <asp:BoundField DataField="CampaignID" HeaderText="CampaignID" SortExpression="CampaignID" />
                            <asp:BoundField DataField="FeedbackID" HeaderText="FeedbackID" SortExpression="FeedbackID" />
                            <asp:BoundField DataField="SubstanceID" HeaderText="SubstanceID" SortExpression="SubstanceID" />
                            <asp:BoundField DataField="Code" HeaderText="Code" ReadOnly="True" SortExpression="Code" />
                            <asp:BoundField DataField="DeleteNote" HeaderText="DeleteNote" ReadOnly="True" SortExpression="DeleteNote" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />
                            <asp:BoundField DataField="TestResultStatus" HeaderText="TestResultStatus" ReadOnly="True"
                                SortExpression="TestResultStatus" />
                            <asp:BoundField DataField="DeliverStatus" HeaderText="DeliverStatus" ReadOnly="True"
                                SortExpression="DeliverStatus" />
                        </Fields>
                    </asp:DetailsView>
                </div>
                <br />
                <div runat="server" id="divRBC">
                    <asp:DetailsView runat="server" ID="DetailsViewRBC" AutoGenerateRows="False" DataKeyNames="ID">
                        <Fields>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codabar" HeaderText="Codabar" SortExpression="Codabar" />
                            <asp:BoundField DataField="CollectedDate" HeaderText="CollectedDate" SortExpression="CollectedDate" />
                            <asp:BoundField DataField="DeleteNote" HeaderText="DeleteNote" ReadOnly="True" SortExpression="DeleteNote" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />
                            <asp:BoundField DataField="TestResultStatus" HeaderText="TestResultStatus" ReadOnly="True"
                                SortExpression="TestResultStatus" />
                            <asp:BoundField DataField="DeliverStatus" HeaderText="DeliverStatus" ReadOnly="True"
                                SortExpression="DeliverStatus" />
                        </Fields>
                    </asp:DetailsView>
                </div>
                <div runat="server" id="divFFPlasma">
                    <asp:DetailsView runat="server" ID="DetailsViewFFPlasma" AutoGenerateRows="False"
                        DataKeyNames="ID">
                        <Fields>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codabar" HeaderText="Codabar" SortExpression="Codabar" />
                            <asp:BoundField DataField="CollectedDate" HeaderText="CollectedDate" SortExpression="CollectedDate" />
                            <asp:BoundField DataField="DeleteNote" HeaderText="DeleteNote" ReadOnly="True" SortExpression="DeleteNote" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />
                            <asp:BoundField DataField="TestResultStatus" HeaderText="TestResultStatus" ReadOnly="True"
                                SortExpression="TestResultStatus" />
                            <asp:BoundField DataField="DeliverStatus" HeaderText="DeliverStatus" ReadOnly="True"
                                SortExpression="DeliverStatus" />
                        </Fields>
                    </asp:DetailsView>
                </div>
                <div runat="server" id="divFFPlasma_Poor">
                    <asp:DetailsView runat="server" ID="DetailsViewFFPlasma_Poor" AutoGenerateRows="False"
                        DataKeyNames="ID">
                        <Fields>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codabar" HeaderText="Codabar" SortExpression="Codabar" />
                            <asp:BoundField DataField="CollectedDate" HeaderText="CollectedDate" SortExpression="CollectedDate" />
                            <asp:BoundField DataField="DeleteNote" HeaderText="DeleteNote" ReadOnly="True" SortExpression="DeleteNote" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />
                            <asp:BoundField DataField="TestResultStatus" HeaderText="TestResultStatus" ReadOnly="True"
                                SortExpression="TestResultStatus" />
                            <asp:BoundField DataField="DeliverStatus" HeaderText="DeliverStatus" ReadOnly="True"
                                SortExpression="DeliverStatus" />
                        </Fields>
                    </asp:DetailsView>
                </div>
                <div runat="server" id="divWBC">
                    <asp:DetailsView runat="server" ID="DetailsViewWBC" AutoGenerateRows="False" DataKeyNames="ID">
                        <Fields>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codabar" HeaderText="Codabar" SortExpression="Codabar" />
                            <asp:BoundField DataField="CollectedDate" HeaderText="CollectedDate" SortExpression="CollectedDate" />
                            <asp:BoundField DataField="DeleteNote" HeaderText="DeleteNote" ReadOnly="True" SortExpression="DeleteNote" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />
                            <asp:BoundField DataField="TestResultStatus" HeaderText="TestResultStatus" ReadOnly="True"
                                SortExpression="TestResultStatus" />
                            <asp:BoundField DataField="DeliverStatus" HeaderText="DeliverStatus" ReadOnly="True"
                                SortExpression="DeliverStatus" />
                        </Fields>
                    </asp:DetailsView>
                </div>
                <div runat="server" id="divPlatelet">
                    <asp:DetailsView runat="server" ID="DetailsViewPlatelet" AutoGenerateRows="False"
                        DataKeyNames="ID">
                        <Fields>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codabar" HeaderText="Codabar" SortExpression="Codabar" />
                            <asp:BoundField DataField="CollectedDate" HeaderText="CollectedDate" SortExpression="CollectedDate" />
                            <asp:BoundField DataField="DeleteNote" HeaderText="DeleteNote" ReadOnly="True" SortExpression="DeleteNote" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />
                            <asp:BoundField DataField="TestResultStatus" HeaderText="TestResultStatus" ReadOnly="True"
                                SortExpression="TestResultStatus" />
                            <asp:BoundField DataField="DeliverStatus" HeaderText="DeliverStatus" ReadOnly="True"
                                SortExpression="DeliverStatus" />
                        </Fields>
                    </asp:DetailsView>
                </div>
                <div runat="server" id="divFactorVIII">
                    <asp:DetailsView runat="server" ID="DetailsViewFactorVIII" AutoGenerateRows="False"
                        DataKeyNames="ID">
                        <Fields>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codabar" HeaderText="Codabar" SortExpression="Codabar" />
                            <asp:BoundField DataField="CollectedDate" HeaderText="CollectedDate" SortExpression="CollectedDate" />
                            <asp:BoundField DataField="DeleteNote" HeaderText="DeleteNote" ReadOnly="True" SortExpression="DeleteNote" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />
                            <asp:BoundField DataField="TestResultStatus" HeaderText="TestResultStatus" ReadOnly="True"
                                SortExpression="TestResultStatus" />
                            <asp:BoundField DataField="DeliverStatus" HeaderText="DeliverStatus" ReadOnly="True"
                                SortExpression="DeliverStatus" />
                        </Fields>
                    </asp:DetailsView>
                </div>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
