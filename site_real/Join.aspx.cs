using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace site_real
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void join(object sender, EventArgs e)
        {
            if(c_password.Text!=u_password.Text)
            {
                message.Text = "passwords must match";
            }
            else
            {
                DBHandler.Open();
                    bool is_admin = admin_code.Text == DBHandler.Data["admin_key"];
                    int id = DBHandler.Adduser(u_name.Text, u_password.Text, is_admin);
                    if (id == 0)
                    {
                        message.Text = "user name or password already exist";
                    }
                    else
                    {
                        Session["user_name"] = u_name.Text;
                        Session["user_id"] = id;
                        Session["is_admin"] = is_admin;
                    }
                DBHandler.Close();
                Response.Redirect(".");
            }
        }
        public void Unnamed_CheckedChanged(object sender, EventArgs e)
        {
            admin_code.Visible = admin_check.Checked;
        }
    }
}