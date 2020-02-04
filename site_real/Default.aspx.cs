using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using site_real.App_Code;

namespace site_real
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using(DBHandler db = new DBHandler())
            {
                string text= db.GetText("main");
                var paragraph = new LiteralControl(text);
                texttest.Controls.Add(paragraph);
            }
        }
    }
}