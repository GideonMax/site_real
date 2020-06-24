﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
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
            DBHandler.Open();
            using (DbDataReader row = DBHandler.GetAllBugReports())
            {
                while (row.Read())
                {
                    html += "<li><a href='Subject.aspx?id=" + row["ID"] +
                        "'>" + row["Title"] + "</a> – " + row["user_name"] +
                        "</li>";
                }
            }
            DBHandler.Close();
            ulSubjects.InnerHtml = html;
            
            
            /*if (Session["user_id"] == null)
                pnlUser.Visible = false;
            else // הגולש מחובר
                linkLoginToPost.Visible = false;*/
        }
        /*
        protected void OpenSubject_Click(object sender, EventArgs e)
        {
            if (Session["user_id"] == null) // הגולש אינו מחובר
                Response.Redirect("Login.aspx");
            else
            {
                int user = (int)Session["user_id"];
                string title = Title.Text, content = TextBox2.Text;
                DBHandler.Open();
                    int subject = DBHandler.OpenBugReport(title, content, user);
                    Response.Redirect("Subject.aspx?id=" + subject);
                DBHandler.Close();
            }
        }*/
    }
}