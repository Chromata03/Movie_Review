using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            changeDNameFld.Text = displayNameVal.Text = textInfo.ToTitleCase(Session["name"].ToString());
            usernameVal.Text = Session["username"].ToString();
            changeEmailFld.Text = emailVal.Text = Session["email"].ToString();

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
                string regex = @"(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{8,}$";
                bool isMatch = Regex.IsMatch(password, regex);
                if (!isMatch)
                    return ValidationCase.PasswordComplexity;

                if (!password.Equals(confirmPassword))
                    return ValidationCase.PasswordMatch;
            }
            if (!string.IsNullOrEmpty(name)) {
                if (name.Length < 5)
                    return ValidationCase.NameLength;
            }
            if (!string.IsNullOrEmpty(email)) {
                string emailRegEx = @"(^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$)";
                bool emailIsMatch = Regex.IsMatch(email, emailRegEx);
                if (!emailIsMatch)
                    return ValidationCase.EmailComplexity;
            }
            return ValidationCase.Success;
        }

        private static string validateData(string name, string email, string password, string confirmPassword) {
            ValidationCase validationResult = ValidateInput(name, email, password, confirmPassword);

            switch (validationResult) {
                case ValidationCase.NameLength:
                    return "Name should be atleast 5 characters!";
                case ValidationCase.PasswordMatch:
                    return "New Password does not match!";
                case ValidationCase.PasswordComplexity:
                    return "Password does not meet the complexity requirements.";
                case ValidationCase.EmailComplexity:
                    return "Invalid Email Address!";
                case ValidationCase.Success:
                    break;
            }
            return "";
        }

        [WebMethod]
        public static string changePassword(string curPass, string newPass, string conPass) {
            string message = "";
            UserControl uc = new UserControl();

            message = validateData("", "", newPass, conPass);

            if (!string.IsNullOrEmpty(message)) {
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
            name = name.Trim();
            email = email.Trim();
            string message = "";
            UserControl uc = new UserControl();

            message = validateData(name, email, "", "");

            if (!string.IsNullOrEmpty(message)) {
                return Message(false, message);
            }

            try {
                using(SqlConnection connection = new SqlConnection(_connectionString)){
                    connection.Open();
                    Dictionary<string, object> userDetails = new Dictionary<string,object>();

                    userDetails.Add("name", name);
                    userDetails.Add("email", email);

                    int results = uc.UpdateTable(connection,"userInfo",id,userDetails);

                    if (results > -1) {
                        message = "User details has been successfully updated";
                        HttpContext.Current.Session["name"] = name;
                        HttpContext.Current.Session["email"] = email;
                    }
                    else {
                        message = "No Information was changed, Detail is still the same";
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex) {
                uc.LogError(ex.Message + ": " + ex.StackTrace);
            }
            return Message(true,message);
        }

        public static string Message(bool result, string message) {
            return new JavaScriptSerializer().Serialize(new { success = result, message });
        }
    }
}
