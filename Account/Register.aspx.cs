using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Movie_Review.Account {
    public partial class Register : Page {
        private readonly string _connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + @"database\MovieReview.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["username"] != null) {
                Response.Redirect("~/");
            }
            passwordField.Attributes["oninput"] = "complexity(event);";
            passwordField.Attributes["onblur"] = "hideCheck(event);";
            confirmPasswordField.Attributes["oninput"] = "validateMatch(event);";
            confirmPasswordField.Attributes["onblur"] = "hideCheck(event);";
        }

        public enum ValidationCase {
            UsernameAndNameLength,
            PasswordMatch,
            PasswordComplexity,
            Success
        }

        private ValidationCase ValidateInput(string username, string name, string password, string confirmPassword) {
            if (username.Length < 5 && name.Length < 5)
                return ValidationCase.UsernameAndNameLength;

            if (!password.Equals(confirmPassword))
                return ValidationCase.PasswordMatch;

            string regex = @"(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{8,}$";
            bool isMatch = Regex.IsMatch(password, regex);
            if (!isMatch)
                return ValidationCase.PasswordComplexity;

            return ValidationCase.Success;
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            string username = usernameField.Text.Trim();
            string password = passwordField.Text;
            string confirmPassword = confirmPasswordField.Text;
            string name = nameField.Text.Trim();
            string email = emailField.Text;
            UserControl uc = new UserControl();

            ValidationCase validationResult = ValidateInput(username, name, password, confirmPassword);
            switch (validationResult) {
                case ValidationCase.UsernameAndNameLength:
                    uc.SetMessage(headerContainer, headerMsg, "Username and Name length should be at least 5 characters.");
                    return;
                case ValidationCase.PasswordMatch:
                    uc.SetMessage(headerContainer, headerMsg, "Password does not match.");
                    return;
                case ValidationCase.PasswordComplexity:
                    uc.SetMessage(headerContainer, headerMsg, "Password does not meet the complexity requirements.");
                    return;
                case ValidationCase.Success:
                    break;
            }

            try {
                int userId;
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction()) {
                        try {
                            userId = uc.InsertUser(connection, transaction, username, password);
                            uc.InsertUserInfo(connection, transaction, userId, name, email);
                            transaction.Commit();
                        }
                        catch(SqlException ex) {
                            if (ex.Number == 2601 || ex.Number == 2627) {
                                uc.SetMessage(headerContainer, headerMsg, "Username/Email Already Exist!");
                                uc.LogError(ex.Message + ": " + ex.StackTrace);
                                return;
                            }
                            uc.SetMessage(headerContainer, headerMsg, "Unexpected Error occurred. Please contact Network Administrator");
                            uc.LogError(ex.Message + ": " + ex.StackTrace);
                            return;
                        }
                        catch (Exception ex) {
                            transaction.Rollback();
                            uc.Reseed(connection);
                            uc.SetMessage(headerContainer, headerMsg, "An error occurred during registration, Please try again later");
                            uc.LogError(ex.Message + ": " + ex.StackTrace);
                            return;
                        }
                    }
                }
                Session["message"]="Registration Successful, Please login.";
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex) {
                uc.SetMessage(headerContainer, headerMsg, "Unexpected Error occurred. Please contact System Administrator");
                uc.LogError(ex.Message + ": " + ex.StackTrace);
                return;
            }
        }
    }
}
