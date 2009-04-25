<%@ Page Language="C#" MasterPageFile="~/MasterPageAdminMenu.master" AutoEventWireup="true"
    CodeFile="CreatingUserAccount.aspx.cs" Inherits="Membership_CreatingUserAccount"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Tạo tài khoản mới
    </h2>
    <p>
        <asp:CreateUserWizard ID="RegisterUser" runat="server" CancelDestinationPageUrl="~/Admin.aspx"
            ContinueDestinationPageUrl="~/Admin.aspx" DisplayCancelButton="True" OnCreatingUser="RegisterUser_CreatingUser"
            OnCreatedUser="RegisterUser_CreatedUser" OnActiveStepChanged="RegisterUser_ActiveStepChanged"
            LoginCreatedUser="false" CancelButtonText="<%$ Resources:Resource,Cancel %>" CompleteSuccessText="Tạo tài khoản thành công"
            ContinueButtonText="<%$ Resources:Resource,Continue %>" CreateUserButtonText="<%$ Resources:Resource,New %>" FinishCompleteButtonText="Hoàn tất"
            FinishPreviousButtonText="<%$ Resources:Resource,Back %>" StartNextButtonText="<%$ Resources:Resource,Continue %>" StepNextButtonText="<%$ Resources:Resource,Continue %>"
            StepPreviousButtonText="<%$ Resources:Resource,Back %>" 
            DuplicateEmailErrorMessage="Trùng email." 
            DuplicateUserNameErrorMessage="Trùng tên đăng nhập." 
            PasswordRegularExpression="">
            <WizardSteps>
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                    <ContentTemplate>
                        <table border="0">
                            <tr>
                                <td align="center" colspan="2">
                                    Tạo tài khoản mới
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Họ và Tên:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFullname" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFullname"
                                        ErrorMessage="Điền họ và tên." ToolTip="Điền họ và tên." ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Tên đăng nhập:
                                </td>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        ErrorMessage="Điền tên đang nhập." ToolTip="Điền tên đang nhập." ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Mật khẩu:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="Điền mật khẩu." ToolTip="Điền mật khẩu." ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Điền lại mật khẩu:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                        ErrorMessage="Điền mật khẩu." ToolTip="Điền mật khẩu." ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                        ErrorMessage="Điền e-mail." ToolTip="Điền e-mail." ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="PhoneLable" runat="server">Điện thoại:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Phone" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Phone"
                                        ErrorMessage="Điền số điện thoại." ToolTip="Điền số điện thoại." ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <%--<tr>
                                <td align="right">
                                    <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Security 
                                    Question:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" 
                                        ControlToValidate="Question" ErrorMessage="Security question is required." 
                                        ToolTip="Security question is required." ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security 
                                    Answer:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" 
                                        ControlToValidate="Answer" ErrorMessage="Security answer is required." 
                                        ToolTip="Security answer is required." ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                        ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="Mật khẩu nhập 2 lần không trùng."
                                        ValidationGroup="RegisterUser"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color: Red;">
                                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:CreateUserWizardStep>
                <asp:WizardStep ID="SpecifyRolesStep" runat="server" AllowReturn="False" StepType="Step"
                    Title="Specify Roles">
                    <p>
                        Gán quyền
                    </p>
                    <asp:CheckBoxList ID="RoleList" runat="server">
                    </asp:CheckBoxList>
                </asp:WizardStep>
                <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server" />
            </WizardSteps>
        </asp:CreateUserWizard>
    </p>
    <p>
        <asp:Label runat="server" ID="InvalidUserNameOrPasswordMessage" Visible="false" EnableViewState="false"
            ForeColor="Red"></asp:Label>
    </p>
</asp:Content>
