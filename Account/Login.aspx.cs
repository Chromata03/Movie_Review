using System;
using System.Text;  
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI.HtmlControls;

namespace Movie_Review.Account
{
    public partial class Login : Page
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + @"database\MovieReview.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null) {
                Response.Redirect("~/");
            }
            if (Session["message"] != null) {
                headerMsg.Text = Session["message"].ToString();
                headerContainer.Visible = true;
                Session.Remove("message");
            }
        }

        protected void btnSubmit_Click(object sender, ImageClickEventArgs e) {
            string username = usernameField.Text.Trim();
            string password = passwordField.Text;
            bool redirect = true;
            string query = "SELECT keys, password, id FROM users WHERE Username = @Username";
            UserControl uc = new UserControl();
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                try {
                    connection.Open();
                    var userDetails = uc.User(connection, username, password);
                    if (userDetails.ContainsKey("state")) {
                        foreach (var item in userDetails) {
                            Session[item.Key] = item.Value;
                        }
                        int id = Convert.ToInt32(Session["id"]);
                        uc.UpdateTable(connection, "userInfo", id, new Dictionary<string, object> { { "last_login", DateTime.Now } });
                    }
                    else if (userDetails.Count == 0) {
                        uc.SetMessage(headerContainer, headerMsg, "username does not exist!");
                    }
                    else {
                        uc.SetMessage(headerContainer, headerMsg, "Invalid username/password");
                        return;
                    }
                }
                catch (Exception ex) {
                    uc.SetMessage(headerContainer, headerMsg, "Unexpected error occurred. Please try again later.");
                    uc.LogError(ex.Message + ": " + ex.StackTrace);
                    return;
                }
            }
            Response.Redirect("~/");
        }


    }
}