<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestDef.aspx.cs" Inherits="Codabar_TestDef" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="LinqDataSource1">
            <Columns>
                <asp:BoundField DataField="ParentName" HeaderText="ParentName" SortExpression="Name" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <img src='<%# Eval("Codabar") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="RedBloodDataContext"
            TableName="TestResults" Where="Level == @Level" EnableUpdate="True" OnSelecting="LinqDataSource1_Selecting">
            <WhereParameters>
                <asp:Parameter DefaultValue="2" Name="Level" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
        <h5>
            Hủy kết quả
        </h5>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ID" DataSourceID="LinqDataSource2">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                            <img src='<%# Eval("Codabar") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
            ContextTypeName="RedBloodDataContext" TableName="TestResults" 
            Where="Level == @Level" onselecting="LinqDataSource2_Selecting">
            <WhereParameters>
                <asp:Parameter DefaultValue="1" Name="Level" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    </form>
</body>
</html>
