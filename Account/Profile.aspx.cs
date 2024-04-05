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
        
        protected override void OnPreLoad(EventArgs e) {
           
        }
        
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
        }

        public enum ValidationCase {
            UsernameAndNameLength,
            PasswordMatch,
            PasswordComplexity,
            Success
        }

        private static ValidationCase ValidateInput(string username, string name, string password, string confirmPassword) {
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

        [WebMethod]
        public static string changePassword(string curPass, string newPass, string conPass) {
            string message = "";

            ValidationCase validationResult = ValidateInput("", "", newPass, conPass);
            switch (validationResult) {
                case ValidationCase.PasswordMatch:
                    message = "Password does not match.";
                    break;
                case ValidationCase.PasswordComplexity:
                    message = "Password does not meet the complexity requirements.";
                    break;
                case ValidationCase.Success:
                    break;
            }
            if(message.Equals(""))
                return new JavaScriptSerializer().Serialize(new { success = false, message });

            

            return "";
        }
    }
}