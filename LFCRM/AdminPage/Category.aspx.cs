using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.AdminPage
{
    public partial class Category : System.Web.UI.Page
    {
        Class.csCategory category = new Class.csCategory();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //check admin permission
                if ((bool)Session["LoggedIn"] == false) Response.Redirect("../UserPage/Login.aspx");
                if (((bool)Session["LoggedIn"] == true) && ((string)Session["UserRole"] != "Admin")) Response.Redirect("../UserPage/Default.aspx");

                loadCategory();
            }
        }

        public void loadCategory()
        {
            GridView1.DataSource = category.GetCategories(txt_search.Text);
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                string color = e.Row.Cells[1].Text.ToString();
                e.Row.Cells[1].Attributes["style"] = "background-color: " + color + ";";

                GridViewRow row = e.Row;
                row.Attributes["id"] = "gridrow";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.Equals("edit_category"))
            {
                GridViewRow gvrow = GridView1.Rows[index];
                txt_cate.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text).ToString();
                lb_oricolor.Text = HttpUtility.HtmlDecode(gvrow.Cells[1].Text).ToString();
                lb_oricolor.Attributes["style"] = "background-color: " + lb_oricolor.Text + ";";

                lb_cateid.Text = category.getCategoryID(txt_cate.Text);

                lb_cate.Visible = false;
                lb_color.Visible = false;
                txt_color.Style.Add("visibility", "hidden");

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
            }
            if (e.CommandName.Equals("delete_category"))
            {
                GridViewRow gvrow = GridView1.Rows[index];
                lb_deletecategory.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text).ToString();

                lb_deletestatus.Visible = false;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#DeleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
            }
        }

        protected void txt_cate_TextChanged(object sender, EventArgs e)
        {
            string cateid = lb_cateid.Text;
            string oricate = category.getCategory(cateid);
            string cate = txt_cate.Text;

            if (cate.Equals(oricate) == false)
            {
                if (category.checkCategoryExist(cate) == true)
                {
                    lb_cate.Visible = true;
                    lb_cate.Text = "Category Name is exist. Please enter other Category";
                    lb_cate.Attributes["class"] = "label label-danger";
                }
            }
            else
                lb_cate.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string cate = txt_cate.Text;
            string cateid = lb_cateid.Text;
            string colorcode = txt_color.Text;
            string oricolor = lb_oricolor.Text;

            string oricate = category.getCategory(cateid);

            if (cate.Equals(oricate) == false)
            {
                if (category.checkCategoryExist(cate) == true)
                {
                    lb_cate.Visible = true;
                    lb_cate.Text = "Category Name is exist. Please enter other Category";
                    lb_cate.Attributes["class"] = "label label-danger";
                }
                else if (category.checkCategoryExist(cate) == false && colorcode == "")
                {
                    lb_color.Visible = false;

                    //Update Title
                    category.updateCategory(cateid, cate, oricolor);
                    GridView1.DataBind();

                    //Close modal
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#editModal').modal('hide');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

                    loadCategory();
                }
                else if (category.checkCategoryExist(cate) == false && colorcode != "")
                {
                    if (colorcode.Equals(oricolor) == false)
                    {
                        if (category.checkColorExist(colorcode) == false)
                        {
                            lb_color.Visible = false;

                            //Update Title
                            category.updateCategory(cateid, cate, colorcode);
                            GridView1.DataBind();

                            //Close modal
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#editModal').modal('hide');");
                            sb.Append(@"</script>");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

                            loadCategory();
                        }
                        else
                        {
                            lb_color.Visible = true;
                            lb_color.Text = "This color exists for other Category. Please try other color";
                            lb_color.Attributes["class"] = "label label-danger";
                        }
                    }
                    else
                    {
                        lb_color.Visible = false;

                        //Update Title
                        category.updateCategory(cateid, cate, colorcode);
                        GridView1.DataBind();

                        //Close modal
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#editModal').modal('hide');");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

                        loadCategory();
                    }
                }
            }

            else if (colorcode != "")
            {
                if (colorcode.Equals(oricolor) == false)
                {
                    if (category.checkColorExist(colorcode) == false)
                    {
                        lb_color.Visible = false;

                        //Update Title
                        category.updateCategory(cateid, cate, colorcode);
                        GridView1.DataBind();

                        //Close modal
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#editModal').modal('hide');");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
                    }
                    else
                    {
                        lb_color.Visible = true;
                        lb_color.Text = "This color exists for other Category. Please try other color";
                        lb_color.Attributes["class"] = "label label-danger";
                    }
                }
                else
                {
                    lb_color.Visible = false;

                    //Update Title
                    category.updateCategory(cateid, cate, colorcode);
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
                lb_color.Visible = false;
                //Update Title
                category.updateCategory(cateid, cate, oricolor);
                GridView1.DataBind();

                //Close modal
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editModal').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
            }

            txt_color.Text = "";
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            string cate = txt_newcategory.Text;
            string colorcode = lb_newcolor.Text;

            if (cate == "")
            {
                lb_newcategory.Visible = true;
                lb_newcategory.Text = "Category should not be empty";
                lb_newcategory.Attributes["class"] = "label label-danger";
            }
            else if (category.checkCategoryExist(cate) == true)
            {
                lb_newcategory.Visible = true;
                lb_newcategory.Text = "Category exists, please enter other Category";
                lb_newcategory.Attributes["class"] = "label label-danger";
            }
            else
            {
                lb_newcategory.Visible = false;
                category.addCategory(cate, colorcode);
                GridView1.DataBind();

                //Close modal
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#AddModal').modal('hide');");
                sb.Append("alert('Title Added Successfully');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
                loadCategory();
            }
        }

        protected void btn_addnew_Click(object sender, EventArgs e)
        {
            lb_newcategory.Visible = false;

            string color = category.getRandomColor("#9900CC");
            lb_newcolor.Text = color;
            lb_newcolor.Attributes["style"] = "background-color:" + color;

            //Open Modal
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#AddModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }

        protected void txt_newcategory_TextChanged(object sender, EventArgs e)
        {
            string cate = txt_newcategory.Text;
            if (cate == "")
            {
                lb_newcategory.Visible = true;
                lb_newcategory.Text = "Category should not be empty";
                lb_newcategory.Attributes["class"] = "label label-danger";
            }
            else if (category.checkCategoryExist(cate) == true)
            {
                lb_newcategory.Visible = true;
                lb_newcategory.Text = "Category exists, please enter other Category";
                lb_newcategory.Attributes["class"] = "label label-danger";
            }
            else
                lb_newcategory.Visible = false;
        }

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            string cate = lb_deletecategory.Text;

            if (category.checkCategory(cate) == true)
            {
                lb_deletestatus.Visible = true;
                lb_deletestatus.Text = "This category contains Titles, you cannot delete this Category.";
            }
            else
            {

                category.deleteCategory(cate);

                GridView1.DataBind();

                //Close modal
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#DeleteModal').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

                loadCategory();
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            loadCategory();
        }

    }
}