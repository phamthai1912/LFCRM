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

public partial class Register : System.Web.UI.Page
{
    csRegister registe = new csRegister();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (!registe.CheckEmail(txt_Email.Text))
            lbl_Mess.Text = "Email đã tồn tại!";
        else if (!registe.CheckUser(txt_User.Text))
            lbl_Mess.Text = "Tên đăng nhập đã tồn tại!";
        else
        {
            registe.InsertUser(txt_FullName.Text, txt_Email.Text, txt_User.Text, txt_Pass.Text, txt_Address.Text, ddl_City.SelectedValue, txt_Phone.Text);
            lbl_Mess.Text = "Bạn đã đăng ký thành công! Bạn đã có thể đăng nhập.";
        }
    }
}
