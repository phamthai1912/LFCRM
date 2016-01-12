using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM
{
    public partial class Login1 : System.Web.UI.Page
    {
        Class.csLogin login = new Class.csLogin();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_error.Visible = false;
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            bool result = login.CheckLogin(txt_EmployeeID.Text, txt_Password.Text);

            if (result)
            {
                Session["LoggedIn"] = true;
                Session["FullName"] = login.GetFullName(txt_EmployeeID.Text);
                Session["UserRole"] = login.GetUserRole(txt_EmployeeID.Text);
                Response.Redirect("Default.aspx");
            }
            else
            {
                lbl_error.Visible = true;
            }
        }
    }
}