﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PeopleDetail.aspx.cs" Inherits="FindAndReport_PeopleDetail" %>

<%@ Register Src="~/UserControl/People.ascx" TagPrefix="uc" TagName="People" %>
<%@ Register Src="~/UserControl/PeopleHistory2.ascx" TagPrefix="uc" TagName="PeopleHistory2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr valign="top">
            <td>
                <uc:People runat="server" ID="People1" />
            </td>
            <td>
                <uc:PeopleHistory2 runat="server" ID="PeopleHistory1" />
            </td>
        </tr>
    </table>
</asp:Content>
