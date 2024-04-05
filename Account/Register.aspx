<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Movie_Review.Account.Register" %>
<asp:Content runat="server" ID="headerContent" ContentPlaceHolderID="HeadContent">
    <script>
        var passwordFieldId = '<%= passwordField.ClientID %>';
    </script>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container" style="padding: 10px!important">
        <div class="bigBarContainer">
            <div class="barTitle" runat="server" ClientIDMode="static" id="headerContainer" visible="true" style="color: #080000">
                <asp:Label runat="server" ClientIDMode="static" ID="headerMsg"></asp:Label>
            </div>
            <div class="barTitle">
                <h1>Create an Account</h1>
            </div>
            <div class="barContent">
                <asp:Panel ID="loginPanel" runat="server">
                    <p id="passwordMatchError"></p>
                    <div>
                        <asp:Label ID="username" CssClass="lblSignUp" runat="server" Text="Username:"></asp:Label>
                        <asp:TextBox ID="usernameField" runat="server" CssClass="txtSignUp" MaxLength="50" Required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="usernameRequiredValidator" ControlToValidate="usernameField" runat="server" ErrorMessage="Username is required." Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:Label ID="password" CssClass="lblSignUp" runat="server" Text="Password:"></asp:Label>
                        <asp:TextBox ID="passwordField" runat="server" CssClass="txtSignUp" TextMode="Password" Required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="passwordRequiredValidator" ControlToValidate="passwordField" runat="server" ErrorMessage="Password is required." Display="Dynamic"></asp:RequiredFieldValidator>
                        <div style="margin-left: 160px" class="passwordCheck" id="passwordComplexityContainer">
                            <ul>
                                <li id="passwordLength">Password length should be at least 8 characters</li>
                                <li id="PasswordLetterChar">Password should contain at least 1 letter (alphabetical character).</li>
                                <li id="PasswordNumChar">Password should contain at least 1 number (numeric character).</li>
                                <li id="passwordSpecialChar">Password should have at least 1 special character (e.g., !, $, #, or %)</li>
                            </ul>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="confirmPassword" CssClass="lblSignUp" runat="server" Text="Confirm Password:"></asp:Label>
                        <asp:TextBox ID="confirmPasswordField" runat="server" CssClass="txtSignUp" TextMode="Password" Required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="cpassRequiredValidator" ControlToValidate="confirmPasswordField" runat="server" ErrorMessage="Password Confirmation is required." Display="Dynamic"></asp:RequiredFieldValidator>
                        <div style="margin-left: 160px" class="passwordCheck" id="matchMsgContainer">
                            <p id="matchMsg" style="font-size: 16px"></p>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="displayName" CssClass="lblSignUp" runat="server" Text="Display Name:"></asp:Label>
                        <asp:TextBox ID="nameField" runat="server" CssClass="txtSignUp" Required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="nameRequiredValidator" ControlToValidate="nameField" runat="server" ErrorMessage="Display Name is required." Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:Label ID="email" CssClass="lblSignUp" runat="server" Text="Email Address:"></asp:Label>
                        <asp:TextBox ID="emailField" runat="server" CssClass="txtSignUp" Required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="emailRequiredValidator" ControlToValidate="emailField" runat="server" ErrorMessage="Email address is required." Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="emailFormatValidator" ControlToValidate="emailField" runat="server" ErrorMessage="Invalid email format." ValidationExpression="\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                    <br />
                    <div style="padding-top: 20px; padding-left: 184px;">
                        <asp:Button runat="server" class="btnRegisterUpdate" OnClick="btnSubmit_Click" Text="Register" />

                    </div>
                    <div style="padding-top: 10px; padding-left: 200px;">
                        <p class="redirect">
                            <a runat="server" href="~/">Back to Homepage </a> | <a id="A2" runat="server" href="~/Account/Login.aspx">Login</a>
                        </p>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
