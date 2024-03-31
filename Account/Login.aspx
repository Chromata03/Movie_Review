<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Movie_Review.Account.Login" %>
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
            <div class="barTitle" runat="server" id="headerContainer" visible="true" style="color: #080000">
                <asp:Label runat="server" ID="headerMsg"></asp:Label>
            </div>
            <div class="barTitle" style="margin-bottom: 34px;">
                <h1>flick fusion login</h1>
            </div>
            <div class="barContent">
                <asp:Panel ID="loginPanel" runat="server">
                    <div>
                        <asp:Label ID="username" CssClass="lblSignUp" runat="server" Text="Username:"></asp:Label>
                        <asp:TextBox ID="usernameField" runat="server" CssClass="txtSignUp" MaxLength="50" Required="true"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Label ID="password" CssClass="lblSignUp" runat="server" Text="Password:"></asp:Label>
                        <asp:TextBox ID="passwordField" runat="server" CssClass="txtSignUp" TextMode="Password" Required="true"></asp:TextBox>
                    </div>
                    <div>
                        <p style="padding: 13.3px 0px 0px 67.2px;">No Account yet?<a id="A1" runat="server" href="~/Account/Register.aspx">Click Here!</a></p>
                    </div>
                    <div style="padding: 20px 0px 0px 184px;">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/signin.png" CssClass="button" onClick="btnSubmit_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <!-- 
     style="padding-left: 67.2px; padding-top: 13.3px;"    
-->
</asp:Content>
