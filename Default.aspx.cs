using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Movie_Review
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["message"] != null) {
                headerMsg.InnerText = Session["message"].ToString();
                headerContainer.Visible = true;
                Session.Remove("message");
            }
            if (Session["state"] != null && Session["state"].Equals("logged")) {
                headerMsg.InnerText = "Welcome " + Session["name"].ToString();
                headerContainer.Visible = true;
                Session.Remove("state");
            }
            else {
                headerContainer.Visible = false;
            }
        }
    }
}