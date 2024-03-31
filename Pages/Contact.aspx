<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Movie_Review.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="page">
            <div class="breadcrumbs">
                <a href="index.html">Home</a>
                <span>Contact</span>
            </div>

            <div class="content">
                <div class="row">
                    <div class="col-md-4">
                        <h2>Contact</h2>
                        <ul class="contact-detail">
                            <li>
                                <img runat="server" src="~/Images/icon-contact-map.png" alt="#">
                                <address><span>Company name. INC</span> <br>550 Avenue Street, New York</address>
                            </li>
                            <li>
                                <img runat="server" src="~/Images/icon-contact-phone.png" alt="">
                                <a href="tel:1590912831">+1 590 912 831</a>
                            </li>
                            <li>
                                <img runat="server" src="~/Images/icon-contact-message.png" alt="">
                                <a href="mailto:contact@companyname.com">contact@companyname.com</a>
                            </li>
                        </ul>
                        <div class="contact-form">
                            <input type="text" class="name" placeholder="Name...">
                            <input type="text" class="email" placeholder="Email...">
                            <input type="text" class="website" placeholder="Website...">
                            <textarea class="message" placeholder="Message..."></textarea>
                            <input type="submit" value="Send Message">
                        </div>
                    </div>
                    <div class="col-md-7 col-md-offset-1">
                        <div class="map"></div>
                    </div>
                </div>
            </div>
        </div>
    </div> <!-- .container -->
    <script src="http://maps.google.com/maps/api/js?sensor=false&amp;language=en"></script>
</asp:Content>
