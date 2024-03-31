<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Movie_Review.Account.Profile" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <style>
        body, html {
            height: 100%;
        }
    </style>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container" style="padding: 10px!important;">
        <div class="bigBarContainer">
            <div class="barTitle">
                <h1>User Profile</h1>
            </div>
            <div class="barContent">
                <br />
                <asp:Panel ID="userPanel" runat="server" CssClass="userinfo">
                    <div class="col-ui">
                        <asp:Label ID="displayName" runat="server" CssClass="lblSignUp" Text="Display Name: "></asp:Label>
                        <asp:Label ID="displayNameVal" runat="server" CssClass="txtSignUp profileText" Enabled="false"></asp:Label>
                    </div>
                    <div class="col-ui">
                        <asp:Label ID="username" runat="server" CssClass="lblSignUp" Text="Username       : "></asp:Label>
                        <asp:Label ID="usernameVal" runat="server" CssClass="txtSignUp profileText" Enabled="false"></asp:Label>
                    </div>
                    <div class="col-ui">
                        <asp:Label ID="email" runat="server" CssClass="lblSignUp" Text="Email       : "></asp:Label>
                        <asp:Label ID="emailVal" runat="server" CssClass="txtSignUp profileText" Enabled="false"></asp:Label>
                    </div>
                </asp:Panel>
                <br />
                <button type="button" class="btnRegisterUpdate" id="btnUpdateInfo">Update Information</button>
                
                    <div id="updateInfo"><!-- Changing Details -->
                        
                        <div class="userinfo">
                            <div class="col-ui">
                                <asp:Label ID="dNameLbl" runat="server" CssClass="lblSignUp" Text="Display Name: "></asp:Label>
                                <asp:TextBox ID="changeDNameFld" runat="server" CssClass="txtSignUp" Placeholder="New Display Name"></asp:TextBox>
                            </div>
                            <div class="col-ui">
                                <asp:Label ID="emailLbl" runat="server" CssClass="lblSignUp" Text="Email: "></asp:Label>
                                <asp:TextBox ID="changeEmailFld" runat="server" CssClass="txtSignUp" Placeholder="New Email"></asp:TextBox>
                            </div>
                        </div>
                        <button type="button" class="btnRegisterUpdate" id="btnUpdatePass">Change Password</button>
                        <div id="updatePass" class="userInfo">
                            <div class="col-ui">
                                <asp:Label ID="curPassLbl" runat="server" CssClass="lblSignUp" Text="Current Password: "></asp:Label>
                                <asp:TextBox ID="curPassFld" runat="server" CssClass="txtSignUp" TextMode="Password" ></asp:TextBox>
                            </div>
                            <div class="col-ui">
                                <asp:Label ID="newPassLbl" runat="server" CssClass="lblSignUp" Text="New Password: "></asp:Label>
                                <asp:TextBox ID="newPassFld" runat="server" CssClass="txtSignUp" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="passwordRequiredValidator" ControlToValidate="newPassFld" runat="server" ErrorMessage="Password is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                <div style="margin-left: 160px" class="passwordCheck" id="passwordComplexityContainer">
                                    <ul>
                                        <li id="passwordLength">Password length should be at least 8 characters</li>
                                        <li id="PasswordLetterChar">Password should contain at least 1 letter (alphabetical character).</li>
                                        <li id="PasswordNumChar">Password should contain at least 1 number (numeric character).</li>
                                        <li id="passwordSpecialChar">Password should have at least 1 special character (e.g., !, $, #, or %)</li>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-ui">
                                <asp:Label ID="conNewPassLbl" runat="server" CssClass="lblSignUp" Text="Confirm Password: "></asp:Label>
                                <asp:TextBox ID="conNewPassFld" runat="server" CssClass="txtSignUp" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="cpassRequiredValidator" ControlToValidate="conNewPassFld" runat="server" ErrorMessage="Password Confirmation is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                <div style="margin-left: 160px" class="passwordCheck" id="matchMsgContainer">
                                    <p id="matchMsg" style="font-size: 16px"></p>
                                </div>
                            </div>
                        </div>
                    </div>
                        <div id="userUpdates">
                            <button runat="server" id="saveUpdate" class="btnRegisterUpdate" OnClick="saveUpdates()">Save Changes</button>
                        </div>
            </div>
        </div>
    </div>
</asp:Content>
