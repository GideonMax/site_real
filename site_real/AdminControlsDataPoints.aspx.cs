using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace site_real
{
    public partial class AdminControls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["is_admin"] == null || !(bool)Session["is_admin"]) Response.Redirect(".");
            DBHandler.Open();
            DataPair[] DataPairs = DBHandler.GetAllDataPairs();
            DBHandler.Close();
            foreach(DataPair dataPair in DataPairs)
            {

                PlaceHolder PairElement = new PlaceHolder();
                Button button = new Button();
                button.Click += (a,b)=> { Button_Click(dataPair); };
                button.Text = "change";
                LiteralControl text = new LiteralControl();
                text.Text = dataPair.Key + ": " + dataPair.value+"\r\n<br>";
                PairElement.Controls.Add(button);
                PairElement.Controls.Add(text);
                datapairs.Controls.Add(PairElement);
            }
            Button Add = new Button();
            Add.Click += Add_Click;
            Add.Text = "Add";
            //datapairs.InnerHtml += "<br>";
            datapairs.Controls.Add(Add);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            DBHandler.Open();
            DBHandler.Data[key.Text] = Value.Text;
            DBHandler.Close();
            Response.Redirect(Request.RawUrl);
        }

        private void Button_Click(DataPair dataPair)
        {
            DBHandler.Open();
            DBHandler.Data[dataPair.Key] = Value.Text;
            DBHandler.Close();
            Response.Redirect(Request.RawUrl);
        }
    }
}