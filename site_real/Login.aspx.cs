using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace site_real
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Login(object sender, EventArgs e)
        {
            DBHandler.Open();
                int a = DBHandler.GetUser(u_name.Text, u_password.Text);
                if (a == 0)
                {
                    Message.Text = "username or password wrong";
                }
                else
                {
                    Session["user_id"] = a;
                    Session["user_name"] = u_name.Text;
                    Session["is_admin"] = DBHandler.IsAdmin(a);
                    DBHandler.Close();
                    Response.Redirect(".");
                }
            DBHandler.Close();
        }
    }
}