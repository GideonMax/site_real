using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using site_real.App_Code;
namespace site_real
{
    public partial class BR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((bool)Session["is_admin"])
            {
                Response.Redirect("Default.aspx");
            }
            string html = "";
            using (DBHandler db = new DBHandler())
            using (DbDataReader row = db.GetAllBugReports())
            {
                while (row.Read())
                {
                    html += "<li><a href='Subject.aspx?id=" + row["ID"] +
                        "'>" + row["Title"] + "</a> – " + row["user_name"] +
                        "</li>";
                }
            }
            ulSubjects.InnerHtml = html;
            
            
            if (Session["user_id"] == null) 
                pnlUser.Visible = false;
            else // הגולש מחובר
                linkLoginToPost.Visible = false;
        }
        protected void OpenSubject_Click(object sender, EventArgs e)
            // "פעולה הנקראת בהקלקה על הכפתור "פתח נושא
            // הפעולה מוסיפה את הנושא לפורום, ומפנה לדף הנושא
            {
                if (Session["user_id"] == null) // הגולש אינו מחובר
                    Response.Redirect("Login.aspx");
                else
                {
                    int user = (int)Session["user_id"];
                    string title = txtTitle.Text, content = txtContent.Text;
                    using (DBHandler db = new DBHandler())
                    {
                        int subject = db.OpenBugReport(title, content, user);
                        Response.Redirect("Subject.aspx?id=" + subject);
                    }
                }
            }
    }
}