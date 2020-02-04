using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using site_real.App_Code;

namespace site_real
{
    public partial class interactive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using(DBHandler db = new DBHandler())
            {
                string[] names = db.GetAllCountryNames();
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
            }
        }
        void Load_Article(string country)
        {
            using(DBHandler db = new DBHandler())
            {
                string articletext = db.GetArticleByCountryName(country);
                articletext= articletext.Replace("\n", "<br>");
                article.Text = articletext;
                Console.WriteLine(articletext);
            }
        }
    }
}