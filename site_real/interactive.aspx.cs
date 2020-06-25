using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace site_real
{
    public partial class interactive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CookieValue;
            if (Session["user_id"] != null)
            {
                CookieValue = "" + (int)Session["user_id"];
            }
            else
            {
                CookieValue = "0";
            }
            HttpCookie Cookie = new HttpCookie("ID", CookieValue);
            if (Response.Cookies.AllKeys.Contains("ID"))
            {
                Response.Cookies.Set(Cookie);
            }
            else
            {
                Response.Cookies.Add(Cookie);
            }
        }
    }
}