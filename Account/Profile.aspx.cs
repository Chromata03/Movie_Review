using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
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
            changeEmailFld.Text = "";
            changeDNameFld.Text = "";

            newPassFld.Attributes["oninput"] = "complexity(event);";
            newPassFld.Attributes["onblur"] = "hideCheck(event);";
            conNewPassFld.Attributes["oninput"] = "validateMatch(event);";
            conNewPassFld.Attributes["onblur"] = "hideCheck(event);";
        }
        protected void saveUpdates(Object sender, EventArgs e) { 
            
        }
    }
}