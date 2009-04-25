<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCompanyEditLocation.ascx.cs"
    Inherits="UserControl_CompanyEditLocation" %>
<%--This text box is used as a parameter for LinqDataSource.--%>
<asp:TextBox runat="server" ID="txtCompanyID" Visible="false"></asp:TextBox>
<asp:TextBox runat="server" ID="txtLocationID" Visible="false"></asp:TextBox>
<div id="divGridViewLocation" style="float: left;">
    <h4>
        Chi nhánh
    </h4>
    <asp:LinkButton ID="ImageButtonNewLocation" runat="server" ImageUrl="~/Image/Icon/New.png"
        Text="Thêm chi nhánh mới" OnClick="ImageButtonNewLocation_Click" />
    <br />
    <asp:GridView ID="GridViewLocation" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        DataSourceID="LinqDataSourceLocation" OnRowCommand="GridViewLocation_RowCommand"
        OnDataBinding="GridViewLocation_DataBinding" 
        onrowdeleted="GridViewLocation_RowDeleted">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton SkinID="InGrid" ID="LinkButton22" runat="server" CausesValidation="False"
                        CommandName="Select" Text="<%$ Resources:Resource,Edit %>" CommandArgument='<%# Eval("ID") %>'
                        ImageUrl="~/Image/Icon/Edit.png" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton SkinID="InGrid" ID="LinkButton23" runat="server" CausesValidation="False"
                        CommandName="Delete" Text="<%$ Resources:Resource,Delete %>" ImageUrl="~/Image/Icon/Delete.png"
                        OnClientClick='return confirm("Xóa chi nhánh này?");' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
            <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
            <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
            <asp:BoundField DataField="Address" HeaderText="Địa chỉ" SortExpression="Address" />
            <asp:TemplateField HeaderText="Phường/xã">
                <ItemTemplate>
                    <%# Eval("Geo3.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quận/huyện/thành phố">
                <ItemTemplate>
                    <%# Eval("Geo2.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tỉnh/thành phố">
                <ItemTemplate>
                    <%# Eval("Geo1.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSourceLocation" runat="server" ContextTypeName="RedBloodDataContext"
        TableName="CompanyLocations" Where="CompanyID == Guid(@CompanyID)" EnableDelete="True"
        EnableUpdate="True">
        <WhereParameters>
            <asp:ControlParameter ControlID="txtCompanyID" Name="CompanyID" PropertyName="Text"
                Type="Object" DefaultValue="{00000000-0000-0000-0000-000000000000}"></asp:ControlParameter>
        </WhereParameters>
    </asp:LinqDataSource>
</div>
<div id="divDetailView" style="margin-left: 10px; clear: both;">
    <h4>
        <asp:Literal Text="<%$Resources:Resource,Edit %>" runat="server" />
        &nbsp;thông tin chi nhánh
    </h4>
    <table>
        <tr>
            <td>
                Tên
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Điện thoại
            </td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Fax
            </td>
            <td>
                <asp:TextBox ID="txtFax" runat="server">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Tỉnh/thành phố
            </td>
            <td>
                <asp:DropDownList ID="DropDownListGeo1" runat="server" DataSourceID="LinqDataSourceGeo1"
                    DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DropDownListGeo1_SelectedIndexChanged"
                    AutoPostBack="True" AppendDataBoundItems="true">
                    <asp:ListItem Text="--Chưa rõ--" Value="00000000-0000-0000-0000-000000000000"></asp:ListItem>
                </asp:DropDownList>
                <asp:LinkButton ID="ImageButtonRefeshGeo1" runat="server" Text="<%$ Resources:Resource,Refresh %>"
                    ImageUrl="~/Image/Icon/Refresh.png" OnClick="ImageButtonRefeshGeo1_Click" />
                <asp:LinqDataSource ID="LinqDataSourceGeo1" runat="server" ContextTypeName="RedBloodDataContext"
                    OrderBy="Name" TableName="Geos" Where="Level == @Level">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="1" Name="Level" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </td>
        </tr>
        <tr>
            <td>
                Quận/huyện/thành phố
            </td>
            <td>
                <asp:DropDownList ID="DropDownListGeo2" runat="server" DataSourceID="LinqDataSourceGeo2"
                    DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DropDownListGeo2_SelectedIndexChanged"
                    AutoPostBack="True" AppendDataBoundItems="true">
                    <asp:ListItem Text="--Chưa rõ--" Value="00000000-0000-0000-0000-000000000000"></asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqDataSourceGeo2" runat="server" ContextTypeName="RedBloodDataContext"
                    OrderBy="Name" TableName="Geos" Where="Level == @Level and ParentID == Guid(@ParentID)">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="2" Name="Level" Type="Int32" />
                        <asp:ControlParameter ControlID="DropDownListGeo1" Name="ParentID" PropertyName="SelectedValue"
                            Type="Object"></asp:ControlParameter>
                    </WhereParameters>
                </asp:LinqDataSource>
            </td>
        </tr>
        <tr>
            <td>
                Phường/xã
            </td>
            <td>
                <asp:DropDownList ID="DropDownListGeo3" runat="server" DataSourceID="LinqDataSourceGeo3"
                    DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true">
                    <asp:ListItem Text="--Chưa rõ--" Value="00000000-0000-0000-0000-000000000000"></asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqDataSourceGeo3" runat="server" ContextTypeName="RedBloodDataContext"
                    OrderBy="Name" TableName="Geos" Where="Level == @Level and ParentID == Guid(@ParentID)">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="3" Name="Level" Type="Int32" />
                        <asp:ControlParameter ControlID="DropDownListGeo2" Name="ParentID" PropertyName="SelectedValue"
                            Type="Object" DefaultValue="{00000000-0000-0000-0000-000000000000}" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </td>
        </tr>
        <tr>
            <td>
                Địa chỉ
            </td>
            <td>
                <asp:TextBox ID="txtAddess" runat="server">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:LinkButton ID="ImageButtonSave" runat="server" ImageUrl="~/Image/Icon/Update24.png"
                    Text="<%$ Resources:Resource,Update %>" OnClick="ImageButtonSave_Click" />
            </td>
        </tr>
    </table>
</div>
<div id="divContact" style="float: left; clear: both;">
    <h4>
        Liên hệ
    </h4>
    <p>
        <asp:Label ID="ActionStatus" runat="server" CssClass="Important"> </asp:Label>
    </p>
    <asp:LinkButton ID="ImageButtonNewContact" runat="server" ImageUrl="~/Image/Icon/New.png"
        Text="Thêm người liên hệ." OnClick="ImageButtonNewContact_Click" />
    <br />
    <asp:GridView ID="GridViewContact" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        DataSourceID="LinqDataSourceContact" OnDataBinding="GridViewContact_DataBinding">
        <Columns>
            <asp:CommandField ShowEditButton="True" ButtonType="Link" EditText="<%$Resources:Resource,Edit %>"
                UpdateText="<%$Resources:Resource,Update %>" CancelText="<%$Resources:Resource,Cancel %>"
                EditImageUrl="~/Image/Icon/Edit.png" UpdateImageUrl="~/Image/Icon/Update.png"
                CancelImageUrl="~/Image/Icon/Cancel.png" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton SkinID="InGrid" ID="LinkButton24" runat="server" CausesValidation="False"
                        CommandName="Delete" Text="<%$ Resources:Resource,Delete %>" ImageUrl="~/Image/Icon/Delete.png"
                        OnClientClick='return confirm("Xóa người liên hệ này?");' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Chi nhánh">
                <ItemTemplate>
                    <%# Eval("CompanyLocation.Name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FullName" HeaderText="Họ và tên" SortExpression="FullName" />
            <asp:BoundField DataField="Phone" HeaderText="Điện thoại" SortExpression="Phone" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Birthday" HeaderText="Ngày sinh" SortExpression="Birthday"
                DataFormatString="{0:dd MMMM yyyy}" />
            <asp:BoundField DataField="Title" HeaderText="Chức vụ" SortExpression="Title" />
            <asp:BoundField DataField="Note" HeaderText="Ghi chú" SortExpression="Note" />
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSourceContact" runat="server" ContextTypeName="RedBloodDataContext"
        EnableDelete="True" EnableUpdate="True" OrderBy="FullName" TableName="CompanyContactPersons"
        Where="CompanyLocation.CompanyID == Guid(@CompanyID)">
        <WhereParameters>
            <asp:ControlParameter ControlID="txtCompanyID" Name="CompanyID" PropertyName="Text"
                Type="Object" DefaultValue="{00000000-0000-0000-0000-000000000000}" />
        </WhereParameters>
    </asp:LinqDataSource>
</div>
