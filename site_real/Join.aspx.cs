using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using site_real.App_Code;

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
                using(DBHandler db = new DBHandler())
                {
                    bool is_admin = admin_code.Text == db.GetData("admin_key");
                    int id = db.Adduser(u_name.Text, u_password.Text, is_admin);
                    if (id == 0)
                    {
                        message.Text = "user name or password already exist";
                    }
                    else
                    {
                        message.Text = "signed up successfully";
                        Session["user_name"] = u_name.Text;
                        Session["user_id"] = id;
                        Session["is_admin"] = is_admin;
                    }
                }
            }
        }
        public void Unnamed_CheckedChanged(object sender, EventArgs e)
        {
            admin_code.Visible = admin_check.Checked;
        }
    }
}