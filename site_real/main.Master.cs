using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace site_real
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["user_name"]!=null)
            {
                JoinLink.Visible = false;
                LoginLink.Visible = false;
                LogoutLink.Visible = true;
                if((bool)Session["is_admin"])
                {
                    AdminControlsLink.Visible = true;
                    GreetingLabel.Text = "ברוך הבא אדמין " + Session["user_name"];
                }
                else
                {
                    GreetingLabel.Text= "ברוך הבא "+ Session["user_name"];
                }
            }
        }
        protected void Logout(object sender,EventArgs e)
        {
            Session.Clear();
            Response.Redirect(".");
        }
    }
}