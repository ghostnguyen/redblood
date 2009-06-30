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

    <table width="100%">
        <tr>
            <td align="center">
                <div runat="server" id="divExtract">
                    Sản xuất:
                    <asp:CheckBoxList runat="server" ID="CheckBoxListExtractTo" DataValueField="ID" DataTextField="Name"
                        RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                    <asp:Button runat="server" ID="btnExtract" Text="Sản xuất" OnClick="btnExtract_Click" />
                    <hr />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="background: url(../Image/PackExtractLayout.png); position: relative;
                    height: 800px;">
                    <div style="position: absolute; left: 15px; top: 125px;" id="divFull" runat="server">
                        <asp:DataList runat="server" ID="DataListFull" AutoGenerateRows="False" DataKeyNames="ID">
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                <br />
                                Tên:
                                <%# Eval("People.Name") %>
                                <br />
                                Ngày:
                                <%# Eval("CollectedDate","{0:dd/MM/yyyy}") %>
                                -
                                <%# Eval("Volume") %>
                                (ml)
                                <br />
                                Nguồn:
                                <%# Eval("Campaign.Source.Name") %>
                                <br />
                                Thành phần:
                                <%# Eval("Component.Name") %>
                                <br />
                                Chất nuôi hồng cầu:
                                <%# Eval("Substance.Name") %>
                                <br />
                                Đợt thu:
                                <%# Eval("CampaignID") %>
                                -
                                <%# Eval("Campaign.Name") %>
                                <br />
                                Trạng thái:
                                <%# Eval("Status") %>
                                <br />
                                Xét nghiệm:
                                <%# Eval("TestResultStatus") %>
                                <br />
                                Cấp phát:
                                <%# Eval("DeliverStatus") %>
                                <br />
                                Ghi chú:
                                <%# Eval("Note") %>
                                <br />
                                <%# Eval("DeleteNote") %>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div id="divRBC" style="position: absolute; left: 290px; top: 300px;" runat="server">
                        <asp:DataList runat="server" ID="DataListRBC" AutoGenerateRows="False" DataKeyNames="ID">
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                <br />
                                Ngày:
                                <%# Eval("CollectedDate","{0:dd/MM/yyyy}") %>
                                <br />
                                Cấp phát:
                                <%# Eval("DeliverStatus") %>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div runat="server" id="divFFPlasma" style="position: absolute; left: 290px; top: 155px;"
                        runat="server">
                        <asp:DataList runat="server" ID="DataListFFPlasma" AutoGenerateRows="False" DataKeyNames="ID">
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                <br />
                                Ngày:
                                <%# Eval("CollectedDate","{0:dd/MM/yyyy}") %>
                                <br />
                                Cấp phát:
                                <%# Eval("DeliverStatus") %>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div runat="server" id="divFFPlasma_Poor" style="position: absolute; top: 10px; left: 290px;">
                        <asp:DataList runat="server" ID="DataListFFPlasma_Poor" AutoGenerateRows="False"
                            DataKeyNames="ID">
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                <br />
                                Ngày:
                                <%# Eval("CollectedDate","{0:dd/MM/yyyy}") %>
                                <br />
                                Cấp phát:
                                <%# Eval("DeliverStatus") %>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div runat="server" id="divWBC" style="position: absolute; top: 445px; left: 290px;">
                        <asp:DataList runat="server" ID="DataListWBC" AutoGenerateRows="False" DataKeyNames="ID">
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                <br />
                                Ngày:
                                <%# Eval("CollectedDate","{0:dd/MM/yyyy}") %>
                                <br />
                                Cấp phát:
                                <%# Eval("DeliverStatus") %>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div runat="server" id="divPlatelet" style="position: absolute; top: 30px; left: 650px;">
                        <asp:DataList runat="server" ID="DataListPlatelet" AutoGenerateRows="False" DataKeyNames="ID">
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                <br />
                                Ngày:
                                <%# Eval("CollectedDate","{0:dd/MM/yyyy}") %>
                                <br />
                                Cấp phát:
                                <%# Eval("DeliverStatus") %>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div runat="server" id="divFactorVIII" style="position: absolute; top: 160px; left: 650px;">
                        <asp:DataList runat="server" ID="DataListFactorVIII" AutoGenerateRows="False" DataKeyNames="ID">
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                <br />
                                Ngày:
                                <%# Eval("CollectedDate","{0:dd/MM/yyyy}") %>
                                <br />
                                Cấp phát:
                                <%# Eval("DeliverStatus") %>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div runat="server" id="divFFPlasma_Poor2" style="position: absolute; top: 295px;
                        left: 650px;">
                        <asp:DataList runat="server" ID="DataListFFPlasma_Poor2" AutoGenerateRows="False"
                            DataKeyNames="ID">
                            <ItemTemplate>
                                <asp:Image ID="ImageCodabar" runat="server" ImageUrl='<%# CodabarBLL.Url4Pack(Eval("Autonum") as int?) %>' />
                                <br />
                                Ngày:
                                <%# Eval("CollectedDate","{0:dd/MM/yyyy}") %>
                                <br />
                                Cấp phát:
                                <%# Eval("DeliverStatus") %>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
