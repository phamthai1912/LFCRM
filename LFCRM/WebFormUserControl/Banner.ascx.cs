using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lbl_fullname.Text = Session["FullName"].ToString();

            if ((string)Session["UserRole"] == "Admin") menu_admin.Visible = true;
            else if ((string)Session["UserRole"] == "User") menu_user.Visible = true;
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session["LoggedIn"] = false;
            Response.Redirect("Login.aspx");
        }
    }
}