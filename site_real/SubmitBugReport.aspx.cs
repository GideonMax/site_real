using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using site_real.App_Code;
namespace site_real
{
    public partial class SubmitBugReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitReport(object sender, EventArgs e)
        {
            string title = ReportTitle.Text;
            string body = Body.Text;
            using (DBHandler DB = new DBHandler())
            {
                DB.OpenBugReport(title, body, (int)Session["user_id"]);
            }
        }
    }
}