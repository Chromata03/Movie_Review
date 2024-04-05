using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI.HtmlControls;

namespace Movie_Review.Account {
    public partial class Profile : Page {
        private static readonly string _connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + @"database\MovieReview.mdf;Integrated Security=True";
        private static int id = -1;

        protected void Page_Load(object sender, EventArgs e) {
            if (!(Session["username"] != null)) {
                Response.Redirect("~/");
            }

            displayNameVal.Text = Session["name"].ToString();
            usernameVal.Text = Session["username"].ToString();
            emailVal.Text = Session["email"].ToString();

            newPassFld.Attributes["onkeyup"] = "complexity(event);";
            newPassFld.Attributes["onblur"] = "hideCheck(event);";
            conNewPassFld.Attributes["onkeyup"] = "validateMatch(event);";
            conNewPassFld.Attributes["onblur"] = "hideCheck(event);";

            id = Convert.ToInt32(HttpContext.Current.Session["id"]);
        }

        public enum ValidationCase {
            NameLength,
            PasswordMatch,
            PasswordComplexity,
            EmailComplexity,
            Success
        }

        private static ValidationCase ValidateInput(string name, string email, string password, string confirmPassword) {
            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmPassword)) {
                if (name.Length < 5)
                    return ValidationCase.NameLength;

                if (!password.Equals(confirmPassword))
                    return ValidationCase.PasswordMatch;
            }
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email)) {
                string regex = @"(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{8,}$";
                bool isMatch = Regex.IsMatch(password, regex);
                if (!isMatch)
                    return ValidationCase.PasswordComplexity;

                string emailRegEx = @"(^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
                bool emailIsMatch = Regex.IsMatch(email, emailRegEx);
                if (!emailIsMatch)
                    return ValidationCase.EmailComplexity;
            }
            return ValidationCase.Success;
        }

        [WebMethod]
        public static string changePassword(string curPass, string newPass, string conPass) {
            string message = "";
            UserControl uc = new UserControl();

            ValidationCase validationResult = ValidateInput("", "", newPass, conPass);
            switch (validationResult) {
                case ValidationCase.PasswordMatch:
                    message = "New Password does not match.";
                    return Message(false, message);
                case ValidationCase.PasswordComplexity:
                    message = "Password does not meet the complexity requirements.";
                    return Message(false, message);
                case ValidationCase.Success:
                    break;
            }
            if (string.IsNullOrEmpty(message)) {
                return Message(false, message);
            }
            try {
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    connection.Open();
                    //check if inputted current password match in the database
                    string checkPasswordQuery = "SELECT password, keys FROM users WHERE id = @userId";
                    using (SqlCommand checkPasswordCommand = new SqlCommand(checkPasswordQuery, connection)) {
                        checkPasswordCommand.Parameters.AddWithValue("@userId", id);
                        SqlDataReader reader = checkPasswordCommand.ExecuteReader();
                        if (reader.Read()) {
                            byte[] salt = (byte[])reader["keys"];
                            string hashedPassword = uc.GenerateHash(curPass, salt);
                            string saltedPassword = reader["password"].ToString();
                            reader.Close();
                            if (saltedPassword.Equals(hashedPassword)) {
                                //update Password information
                                Dictionary<string, object> userData = new Dictionary<string, object>();
                                string newhashedPasword = uc.GenerateHash(newPass);
                                byte[] newSalt = uc.Salt;
                                userData.Add("password", newhashedPasword);
                                userData.Add("keys", newSalt);

                                int result = uc.UpdateTable(connection, "users", id, userData);
                                if (result > -1) {
                                    message = "Password has been successfully changed";
                                    HttpContext.Current.Session.Clear();
                                    HttpContext.Current.Session["message"] = "Password has been successfully changed. Please login";
                                }
                                else {
                                    message = "New password cannot be the same as old password!";
                                    throw new Exception();
                                }
                            }
                            else {
                                message = "Password does not match with the current password";
                                throw new Exception();
                            }
                        }
                        else {
                            message = "Unexpected error occured. Please try again later " + id;
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex) {
                message = (string.IsNullOrEmpty(message)) ? "Unexpected error occured. Please contact System Administrator" : message;
                uc.LogError(ex.Message + ": " + ex.StackTrace);
                return Message(false, message);
            }
            return Message(true, message);
        }

        [WebMethod]
        public static string changeDetails(string name, string email) {
            return "";
        }

        public static string Message(bool result, string message) {
            return new JavaScriptSerializer().Serialize(new { success = result, message });
        }
    }
}
