<%@ Page Title="Reviews" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="Movie_Review.Pages.Review" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%: Styles.Render("~/bundles/fontawesome") %>
    <div class="container">
        <div class="page">
            <div class="breadcrumbs">
                <a id="A1" runat="server" href="~/">Home</a>
                <span>Movie Review</span>
            </div>
            <div class="containers">
                <div class="filters">
                    <select name="#" id="MovieType" placeholder="Choose Category">
                        <option value="#">Action</option>
                        <option value="#">Drama</option>
                        <option value="#">Fantasy</option>
                        <option value="#">Horror</option>
                        <option value="#">Adventure</option>
                    </select>
                    <select name="#" id="Select1">
                        <option value="#">2012</option>
                        <option value="#">2013</option>
                        <option value="#">2014</option>
                    </select>
                </div>
                <div class="searchBar">
                    <form id="searchForm" action="#" class="search-form" onsubmit="return false;">
                        <asp:TextBox ID="searchBox" runat="server" placeholder="Search..."></asp:TextBox>
                        <button id="Button1" runat="server" onclick="SearchButton_Click"><i class="fa fa-search"></i></button>
                    </form>
                </div>
            </div>
            <div class="movie-list">
                <div class="movie">
                    <figure class="movie-poster"><img id="Img1" runat="server" src="~/Images/dummy/thumb-3.jpg" alt="#"></figure>
                    <div class="movie-title"><a id="A2" runat="server" href="~/Pages/Single.aspx">Maleficient</a></div>
                    <p>Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.</p>
                </div>
                <div class="movie">
                    <figure class="movie-poster"><img id="Img2" runat="server" src="~/Images/dummy/thumb-4.jpg" alt="#"></figure>
                    <div class="movie-title"><a id="A3" runat="server" href="~/Pages/Single.aspx">The adventure of Tintin</a></div>
                    <p>Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.</p>
                </div>
                <div class="movie">
                    <figure class="movie-poster"><img id="Img3" runat="server" src="~/Images/dummy/thumb-5.jpg" alt="#"></figure>
                    <div class="movie-title"><a id="A4" runat="server" href="~/Pages/Single.aspx">Hobbit</a></div>
                    <p>Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.</p>
                </div>
                <div class="movie">
                    <figure class="movie-poster"><img id="Img4" runat="server" src="~/Images/dummy/thumb-6.jpg" alt="#"></figure>
                    <div class="movie-title"><a id="A5" runat="server" href="~/Pages/Single.aspx">Exists</a></div>
                    <p>Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.</p>
                </div>
                <div class="movie">
                    <figure class="movie-poster"><img id="Img5" runat="server" src="~/Images/dummy/thumb-1.jpg" alt="#"></figure>
                    <div class="movie-title"><a id="A6" runat="server" href="~/Pages/Single.aspx">Drive hard</a></div>
                    <p>Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.</p>
                </div>
                <div class="movie">
                    <figure class="movie-poster"><img id="Img6" runat="server" src="~/Images/dummy/thumb-2.jpg" alt="#"></figure>
                    <div class="movie-title"><a id="A7" runat="server" href="~/Pages/Single.aspx">Robocop</a></div>
                    <p>Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.</p>
                </div>
                <div class="movie">
                    <figure class="movie-poster"><img id="Img7" runat="server" src="~/Images/dummy/thumb-7.jpg" alt="#"></figure>
                    <div class="movie-title"><a id="A8" runat="server" href="~/Pages/Single.aspx">Life of Pi</a></div>
                    <p>Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.</p>
                </div>
                <div class="movie">
                    <figure class="movie-poster"><img id="Img8" runat="server" src="~/Images/dummy/thumb-8.jpg" alt="#"></figure>
                    <div class="movie-title"><a id="A9" runat="server" href="~/Pages/Single.aspx">The Colony</a></div>
                    <p>Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.</p>
                </div>
            </div> <!-- .movie-list -->
            <div class="pagination">
                <a href="#" class="page-number prev"><i class="fa fa-angle-left"></i></a>
                <span class="page-number current">1</span>
                <a href="#" class="page-number">2</a>
                <a href="#" class="page-number">3</a>
                <a href="#" class="page-number">4</a>
                <a href="#" class="page-number">5</a>
                <a href="#" class="page-number next"><i class="fa fa-angle-right"></i></a>
            </div>
        </div>
    </div> <!-- .container -->
</asp:Content>
