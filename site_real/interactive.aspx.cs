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
            /*
            DBHandler.Open();
                string[] names = DBHandler.GetAllCountryNames();
                if (names != null)
                {
                    foreach (var name in names)
                    {
                        var button = new Button();
                        button.Click += (object send, EventArgs args) =>
                        {
                            Load_Article(name);
                        };
                        button.Text = name;
                        buttons.Controls.Add(button);
                    }
                }
            DBHandler.Close();*/
        }
        void Load_Article(string country)
        {
            DBHandler.Open();
            string articletext = DBHandler.GetArticleByCountryName(country);
            articletext = articletext.Replace("\n", "<br>");
            article.Text = articletext;
            Console.WriteLine(articletext);
            DBHandler.Close();
        }
    }
}