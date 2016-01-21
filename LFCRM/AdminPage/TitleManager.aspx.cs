using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace LFCRM.AdminPage
{
    public partial class TitleManager : System.Web.UI.Page
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();
        Class.csTitleManager _titleManager = new Class.csTitleManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.Equals("edit_Title"))
            {
                GridViewRow gvrow = GridView1.Rows[index];
                lb_3ld.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text).ToString();
                txt_titlename.Text = HttpUtility.HtmlDecode(gvrow.Cells[1].Text).ToString();
                txt_tockcode.Text = HttpUtility.HtmlDecode(gvrow.Cells[2].Text).ToString();
                drop_category.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString();
                lb_color1.Text = HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();
                lb_color1.Attributes["style"] = "background-color: " + lb_color1.Text.ToString() + ";";

                //string sql = "SELECT Category FROM tbl_TitleCategory";
                //drop_category.DataSource = dbconnect.getDataTable(sql);
                //drop_category.DataTextField = "Category";
                //drop_category.DataBind();

                lb_titlename.Visible = false;
                lb_color.Visible = false;
                txt_color.Style.Add("display", "none");

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                string color = e.Row.Cells[4].Text.ToString();
                e.Row.Cells[4].Attributes["style"] = "background-color: " + color + ";";
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string _3ld = lb_3ld.Text;
            string titlename = txt_titlename.Text;
            string code = txt_tockcode.Text;
            string cate = drop_category.SelectedItem.Text;
            string colorcode = lb_color1.Text;

            if (titlename == "")
            {
                lb_titlename.Visible = true;
                lb_titlename.Text = "Title Name should not be empty";
                lb_titlename.Attributes["class"] = "label label-danger";
            }
            else if (colorcode != "")
            {
                string currentcolor = lb_color1.Text;
                string check = _titleManager.checkcolor(currentcolor);
                if (currentcolor.Equals(check))
                    lb_color.Visible = false;
                else
                {
                    lb_color.Visible = true;
                    lb_color.Text = "This color exists. Please try other color";
                    lb_color.Attributes["class"] = "label label-danger";
                }
            }
            else
            {
                _titleManager.updateTitle(_3ld, titlename, code, cate, colorcode);
                GridView1.DataBind();

                //Close modal
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editModal').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
            }            
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            string _3ld = txt_new3ld.Text;
            string titlename = txt_newtitlename.Text;
            string code = txt_newtockcode.Text;
            string cate = drop_newcate.SelectedItem.Text;
            string colorcode = lb_newcolor.Text;

            if (txt_new3ld.Text == "")
            {
                lb_new3ld.Visible = true;
                lb_new3ld.Text = "3LD should not be empty";
                lb_new3ld.Attributes["class"] = "label label-danger";
            }
            else if (_titleManager.LDExist(txt_new3ld.Text) == true)
            {
                lb_new3ld.Visible = true;
                lb_new3ld.Text = "This 3LD exists, please enter other 3LD";
                lb_new3ld.Attributes["class"] = "label label-danger";
            }
            else if (txt_newtitlename.Text == "")
            {
                lb_newtitlename.Visible = true;
                lb_newtitlename.Text = "Title Name should not be empty";
                lb_newtitlename.Attributes["class"] = "label label-danger";
            }
            else 
            {
                lb_new3ld.Visible = false;
                lb_newtitlename.Visible = false;
                _titleManager.addTitle(_3ld, titlename, code, cate, colorcode);
                GridView1.DataBind();

                //Close modal
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#AddModal').modal('hide');");
                sb.Append("alert('Title Added Successfully');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
 
            }
        }

        protected void btn_addnew_Click(object sender, EventArgs e)
        {
            lb_new3ld.Visible = false;
            lb_newtitlename.Visible = false;

            string color = _titleManager.checkcolor("#9900CC");
            lb_newcolor.Text = color;
            lb_newcolor.Attributes["style"] = "background-color:"+color;

            //Open Modal
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#AddModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }

        protected void txt_titlename_TextChanged(object sender, EventArgs e)
        {
            if (txt_titlename.Text == "")
            {
                lb_titlename.Visible = true;
                lb_titlename.Text = "Title Name should not be empty";
                lb_titlename.Attributes["class"] = "label label-danger";
            }
            else
            {
                lb_titlename.Visible = false;
            }
        }

        protected void txt_new3ld_TextChanged(object sender, EventArgs e)
        {
            if (txt_new3ld.Text == "")
            {
                lb_new3ld.Visible = true;
                lb_new3ld.Text = "3LD should not be empty";
                lb_new3ld.Attributes["class"] = "label label-danger";
            }
            else if (_titleManager.LDExist(txt_new3ld.Text) == true)
            {
                lb_new3ld.Visible = true;
                lb_new3ld.Text = "This 3LD is exist, please enter other 3LD";
                lb_new3ld.Attributes["class"] = "label label-danger";
            }
            else
            {
                lb_new3ld.Visible = false;
            }
        }

        protected void txt_newtitlename_TextChanged(object sender, EventArgs e)
        {
            if (txt_newtitlename.Text == "")
            {
                lb_newtitlename.Visible = true;
                lb_newtitlename.Text = "Title Name should not be empty";
                lb_newtitlename.Attributes["class"] = "label label-danger";
            }
            else
            {
                lb_newtitlename.Visible = false;
            }
        }

        protected void txt_color_TextChanged(object sender, EventArgs e)
        {
            string currentcolor = lb_color1.Text;
        }       
    }
}