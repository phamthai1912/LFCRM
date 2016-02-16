using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check loggin permission
            if ((bool)Session["LoggedIn"] == false) Response.Redirect("User/Login.aspx");
        }
    }
}