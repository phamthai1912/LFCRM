using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class frmLogin : System.Web.UI.UserControl
{
    csLogin login = new csLogin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((bool)Session["DaDangNhap"])
        {
            ShowInfoUser();
            if (Session["Level"].ToString() != "Khách hàng") btn_Admin.Visible = true;
            else btn_Admin.Visible = false;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (login.CheckLogin(txtUser.Text, txtPass.Text))
        {
            Session["DaDangNhap"] = true;
            Session["TenDangNhap"] = txtUser.Text;
            Session["UserID"] = login.ShowID(txtUser.Text);
            Session["FullName"] = login.ShowFullName(txtUser.Text);
            Session["Level"] = login.ShowLevel(txtUser.Text);
            if (Session["Level"].ToString() != "Khách hàng") btn_Admin.Visible = true;
            else btn_Admin.Visible = false;
            ShowInfoUser();
        }
        else lblThongBao.Text = "Incorrect!";
    }

    void ShowInfoUser()
    {
        tbThongTin.Visible = true;
        tbLogin.Visible = false;
        lblThongTin.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Chào: " + Session["FullName"] + " <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Session["Level"];
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["DaDangNhap"] = false;
        tbLogin.Visible = true;
        tbThongTin.Visible = false;
        Response.Redirect("Default.aspx");
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }
}
