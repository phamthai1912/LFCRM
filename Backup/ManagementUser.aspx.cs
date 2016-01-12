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

public partial class ManagementUser : System.Web.UI.Page
{
    csUser user = new csUser();
    csRole role = new csRole();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_Mess.Text = "";
        SelectUser();
        
    }

    private void SelectUser()
    {
        grv_User.DataSource = user.SelectUser();
        grv_User.DataBind();
    }

    protected void btn_Insert_Click(object sender, EventArgs e)
    {
        if (!user.CheckEmail(txt_Email.Text))
            lbl_Mess.Text = "Email này đã tồn tại";
        else if (!user.CheckUser(txt_User.Text))
            lbl_Mess.Text = "Tên đăng nhập này đã tồn tại";
        else
        {
            user.InsertUser(txt_Fullname.Text, txt_Email.Text, txt_User.Text, txt_Pass.Text, txt_Address.Text, ddl_City.SelectedValue, int.Parse(txt_Phone.Text), int.Parse(ddl_Role.SelectedValue));
            lbl_Mess.Text = "Thêm thành công !!!";
        }
        SelectUser();
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        if (lbl_UserID.Text == "")
        {
            lbl_Mess.Text = "Chọn người dùng cần sửa !!!";
            return;
        }
        else
        {
            user.UpdateUser(int.Parse(lbl_UserID.Text), txt_Fullname.Text, txt_Address.Text, ddl_City.SelectedValue, txt_Phone.Text, int.Parse(ddl_Role.SelectedValue));
            SelectUser();
            lbl_Mess.Text = "Cập nhập thành công !!!";
        }
    }

    protected void grv_User_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            if (user.CheckUserBeforeDel(grv_User.Rows[index].Cells[0].Text))
                lbl_Mess.Text = "Người dùng này hiện tại không thể xóa!";
            else
            {
                user.DeleteUser(grv_User.Rows[index].Cells[0].Text);
                lbl_Mess.Text = "Xóa người dùng thành công !!!";
            }
            SelectUser();
        }
    }

    protected void grv_User_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lbl_Mess.Text = "";       
        btn_Update.Visible = true;
        btn_Cancel.Visible = true;
        btn_Insert.Visible = false;
        txt_User.Enabled = false;
        txt_Pass.Enabled = false;
        txt_Email.Enabled = false;
        lbl_UserID.Text = HttpUtility.HtmlDecode(grv_User.Rows[e.NewEditIndex].Cells[0].Text);
        txt_Address.Text = user.ShowAddress(grv_User.Rows[e.NewEditIndex].Cells[0].Text);
        txt_Phone.Text = user.ShowPhone(grv_User.Rows[e.NewEditIndex].Cells[0].Text);
        ddl_City.SelectedValue = user.ShowCity(HttpUtility.HtmlDecode(grv_User.Rows[e.NewEditIndex].Cells[0].Text));
        txt_Fullname.Text = HttpUtility.HtmlDecode(grv_User.Rows[e.NewEditIndex].Cells[1].Text);
        txt_User.Text = HttpUtility.HtmlDecode(grv_User.Rows[e.NewEditIndex].Cells[2].Text);
        txt_Email.Text = HttpUtility.HtmlDecode(grv_User.Rows[e.NewEditIndex].Cells[3].Text);
        ddl_Role.Text = role.RoleNameToRoleId(HttpUtility.HtmlDecode(grv_User.Rows[e.NewEditIndex].Cells[4].Text));
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txt_User.Text = "";
        lbl_UserID.Text = "";
        txt_Email.Text = "";
        txt_Address.Text = "";
        txt_Phone.Text = "";
        txt_Fullname.Text = "";
        txt_Pass.Enabled = true;
        btn_Update.Visible = false;
        btn_Cancel.Visible = false;
        btn_Insert.Visible = true;
        txt_User.Enabled = true;
        txt_Email.Enabled = true;
    }

    protected void grv_User_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_User.PageIndex = e.NewPageIndex;
        SelectUser();
    }
}
