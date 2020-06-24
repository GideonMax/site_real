using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace site_real
{
    public partial class SubmitBugReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null) Response.Redirect(".");
        }

        protected void SubmitReport(object sender, EventArgs e)
        {
            string title = ReportTitle.Text;
            string body = Body.Text;
            DBHandler.Open();
                DBHandler.OpenBugReport(title, body, (int)Session["user_id"]);
            DBHandler.Close();
        }
    }
}