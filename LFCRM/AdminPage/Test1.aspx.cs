using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.AdminPage
{
    public partial class Test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, System.EventArgs e)
        {
            if (TextBox1.Text == "a")
                Label1.Text = "A";
            else
                Label1.Text = "X";
        }
    }
}