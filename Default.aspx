<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Movie_Review._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
  <div class="barTitle headerContainer" runat="server" id="headerContainer" visible="false">
    <p runat="server" id="headerMsg" class="header-update"> </p>
  </div>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  <%: Styles.Render("~/bundles/fontawesome") %>
  <div class="container">
    <div class="page">
      <div class="row">
        <div class="col-md-9">
          <div class="slider">
            <ul class="slides">
              <li><a href="#"><img src="Images/dummy/slide-1.jpg" alt="Slide 1"></a></li>
              <li><a href="#"><img src="Images/dummy/slide-2.jpg" alt="Slide 2"></a></li>
              <li><a href="#"><img src="Images/dummy/slide-3.jpg" alt="Slide 3"></a></li>
            </ul>
          </div>
        </div>
        <div class="col-md-3">
          <div class="row">
            <div class="col-sm-6 col-md-12">
              <div class="latest-movie">
                <a href="#"><img src="Images/dummy/thumb-1.jpg" alt="Movie 1"></a>
              </div>
            </div>
            <div class="col-sm-6 col-md-12">
              <div class="latest-movie">
                <a href="#"><img src="Images/dummy/thumb-2.jpg" alt="Movie 2"></a>
              </div>
            </div>
          </div>
        </div>
      </div> <!-- .row -->
      <div class="row">
        <div class="col-sm-6 col-md-3">
          <div class="latest-movie">
            <a href="#"><img src="Images/dummy/thumb-3.jpg" alt="Movie 3"></a>
          </div>
        </div>
        <div class="col-sm-6 col-md-3">
          <div class="latest-movie">
            <a href="#"><img src="Images/dummy/thumb-4.jpg" alt="Movie 4"></a>
          </div>
        </div>
        <div class="col-sm-6 col-md-3">
          <div class="latest-movie">
            <a href="#"><img src="Images/dummy/thumb-5.jpg" alt="Movie 5"></a>
          </div>
        </div>
        <div class="col-sm-6 col-md-3">
          <div class="latest-movie">
            <a href="#"><img src="Images/dummy/thumb-6.jpg" alt="Movie 6"></a>
          </div>
        </div>
      </div> <!-- .row -->

      <div class="row">
        <div class="col-md-4">
          <h2 class="section-title">December premiere</h2>
          <p>Lorem ipsum dolor sit amet consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore.</p>
          <ul class="movie-schedule">
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
          </ul> <!-- .movie-schedule -->
        </div>
        <div class="col-md-4">
          <h2 class="section-title">November premiere</h2>
          <p>Lorem ipsum dolor sit amet consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore.</p>
          <ul class="movie-schedule">
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
          </ul> <!-- .movie-schedule -->
        </div>
        <div class="col-md-4">
          <h2 class="section-title">October premiere</h2>
          <p>Lorem ipsum dolor sit amet consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore.</p>
          <ul class="movie-schedule">
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
            <li>
              <div class="date">16/12</div>
              <h2 class="entry-title"><a href="#">Perspiciatis unde omnis</a></h2>
            </li>
          </ul> <!-- .movie-schedule -->
        </div>
      </div>
    </div>
  </div> <!-- .container -->
</asp:Content>
