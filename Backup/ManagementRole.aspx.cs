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

public partial class ManagementRole : System.Web.UI.Page
{
    csRole role = new csRole();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_Mess.Text = "";
    }
    protected void btn_Insert_Click(object sender, EventArgs e)
    {
        if (txt_RoleName.Text == "")
        {
            lbl_Mess.Text = "Please enter a Role Name !!!";
            return;
        }
        if (role.CheckRoleName(txt_RoleName.Text))
        {
            role.InsertRole(txt_RoleName.Text);
            lbl_Mess.Text = "Role inserted successfully !!!";
            grv_Role.DataBind();
            txt_RoleName.Text = "";
        }
        else
        {
            lbl_Mess.Text = "Role Name already exists !!!";
            txt_RoleName.Text = "";
        }
    }
    protected void grv_Role_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            role.DeleteRole(int.Parse(grv_Role.Rows[index].Cells[0].Text));
            grv_Role.DataBind();
            lbl_Mess.Text = "Role deleted successfully !!!";
        }
    }
}
