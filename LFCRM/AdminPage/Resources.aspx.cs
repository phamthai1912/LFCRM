using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.AdminPage
{
    public partial class Resources : System.Web.UI.Page
    {
        Class.csResource resource = new Class.csResource();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.Equals("edit_resource"))
            {
                GridViewRow gvrow = GridView1.Rows[index];

                lb_id.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text).ToString();
                txt_id.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text).ToString();
                txt_name.Text = HttpUtility.HtmlDecode(gvrow.Cells[1].Text).ToString();
                txt_email.Text = HttpUtility.HtmlDecode(gvrow.Cells[2].Text).ToString();
                txt_phone.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString();
                String role = HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();
                drop_role.SelectedValue = role;

                CheckBox cb101 = (CheckBox)gvrow.FindControl("cb_activegrid");
                cb_active.Checked = cb101.Checked;                

                lb_id.Style.Add("visibility", "hidden");
                lb_id1.Visible = false;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
            }
            if (e.CommandName.Equals("change_pass"))
            {
                GridViewRow gvrow = GridView1.Rows[index];
                txt_newpass.Text = "";
                txt_confirmnewpass.Text = "";

                lb_passid.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text).ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#PassModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
            }

            if (e.CommandName.Equals("delete_resource"))
            {
                GridViewRow gvrow = GridView1.Rows[index];
                lb_deleteid.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text).ToString();
                lb_deleteuser.Text = HttpUtility.HtmlDecode(gvrow.Cells[1].Text).ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#DeleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
            }
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {

                GridViewRow row = e.Row;
                row.Attributes["id"] = "gridrow";
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            String id = lb_passid.Text;
            String newpass = txt_confirmnewpass.Text;
            resource.updatePassword(id, newpass);
            GridView1.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#PassModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            String origid = lb_id.Text;
            String id = txt_id.Text;
            String name = txt_name.Text;
            String email = txt_email.Text;
            String phone = txt_phone.Text;
            String role = drop_role.SelectedItem.Text;
            String active = cb_active.Checked.ToString();
            
            if (id.Equals(origid) == false)
            {
                if (resource.idExist(id))
                {
                    lb_id1.Visible = true;
                    lb_id1.Text = "This ID exists. Please try another ID";
                }
                else
                {
                    lb_id1.Visible = false;
                    resource.updateResource(origid, id, name, email, phone, role, active);
                    GridView1.DataBind();

                    //Close modal
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#editModal').modal('hide');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
                }
            }
            else
            {
                lb_id1.Visible = false;
                resource.updateResource(origid, id, name, email, phone, role, active);
                GridView1.DataBind();

                //Close modal
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editModal').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
            }
        }

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            String id = lb_deleteid.Text;
            resource.deleteResource(id);
            GridView1.DataBind();

            //Close modal
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#DeleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            String id = txt_newid.Text;
            String name = txt_newname.Text;
            String email = txt_newmail.Text;
            String phone = txt_newphone.Text;
            String role = drop_newrole.SelectedItem.Text;
            String active1 = "True";
            if (cb_newactive.Checked == true)
                active1 = "True";
            else
                active1 = "False";

            if (resource.idExist(id) == true)
            {
                lb_newid.Visible = true;
                lb_newid.Text = "This ID exist. Please enter another ID.";
            }
            else
            {
                resource.addResource(id,name,email,phone,role,active1);
                GridView1.DataBind();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#AddModal').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddModalScript", sb.ToString(), false);
            }

        }

        protected void btn_addresource_Click(object sender, EventArgs e)
        {
            txt_newid.Text = "";
            txt_newname.Text = "";
            txt_newmail.Text = "";
            txt_newphone.Text = "";
            cb_active.Checked = true;

            lb_newid.Visible = false;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#AddModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddModalScript", sb.ToString(), false);
        }

    }
}