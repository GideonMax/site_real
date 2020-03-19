using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace site_real
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using(DBHandler db = new DBHandler())
            {
                string text = db.Texts["main"];
                var paragraph = new LiteralControl(text);
                texttest.Controls.Add(paragraph);
            }
        }
    }
}