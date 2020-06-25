using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace site_real
{
    public partial class AdminControlsTexts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["is_admin"] == null || !(bool)Session["is_admin"]) Response.Redirect(".");
            DBHandler.Open();
            string[] Names = DBHandler.GetAllTextNames();
            DBHandler.Close();
            foreach(string name in Names)
            {
                var button = new Button();
                button.Click += (a, b) => { LoadText(name); };
                button.Text = name;
                TextNames.Controls.Add(button);
                LiteralControl text = new LiteralControl();
                text.Text = "<br>";
                TextNames.Controls.Add(text);
            }
            Button SetButton = new Button();
            SetButton.Text = "Set";
            SetButton.Click += (a, b) => { SetArticle(); };
            Button AddButton = new Button();
            AddButton.Text = "Add";
            AddButton.Click += (a, b) => { AddArticle(); };
            TextNames.Controls.Add(SetButton);
            TextNames.Controls.Add(AddButton);
        }

        void SetArticle()
        {
            DBHandler.Open();
            DBHandler.Texts[(string)Session["current"]] = Text.Text;
            DBHandler.Close();
            Response.Redirect(Request.RawUrl);
        }
        void AddArticle()
        {
            DBHandler.Open();
            DBHandler.AddText(name.Text, Text.Text);
            //DBHandler.Texts[name.Text] = Text.Text;
            DBHandler.Close();
            Response.Redirect(Request.RawUrl);

        }
        void LoadText(string name)
        {
            Session["current"] = name;
            DBHandler.Open();
            string Body = DBHandler.Texts[name];
            DBHandler.Close();
            TextDisplay.Text = Body;
            TextDisplay.Visible = true;
            Text.Text = Body;
            //Response.Redirect(Request.RawUrl);
        }
    }
}