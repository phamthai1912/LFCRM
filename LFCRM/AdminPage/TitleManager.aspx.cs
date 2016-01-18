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
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.Equals("edit_Title"))
            {
                GridViewRow gvrow = GridView1.Rows[index];
                lb_titleid.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text).ToString();
                txt_titlename.Text = HttpUtility.HtmlDecode(gvrow.Cells[1].Text).ToString();
                txt_tockcode.Text = HttpUtility.HtmlDecode(gvrow.Cells[2].Text).ToString();
                drop_category.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString();
                lb_color.Text = HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();

                UpdatePanel1.Visible = true;

                string sql = "SELECT Category FROM tbl_TitleCategory";
                drop_category.DataSource = dbconnect.getDataTable(sql);
                drop_category.DataTextField = "Category";
                drop_category.DataBind();
            }
        }

        protected void btn_close_Click1(object sender, EventArgs e)
        {
            UpdatePanel1.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string titleid = lb_titleid.Text;
            string titlename = txt_titlename.Text;
            string code = txt_tockcode.Text;
            string cate = drop_category.SelectedItem.Text;

            if(titlename != null && code != null && cate != null)
            {
                string sql1 = "SELECT TitleCategoryID FROM tbl_TitleCategory WHERE Category = '"+cate+"'";
                DataTable tb = dbconnect.getDataTable(sql1);
                string cateid = tb.Rows[0][0].ToString();

                string update = "UPDATE tbl_Title " +
                    "SET TitleName = '" + titlename + "', TOCKCode = '" + code + "', TitleCategoryID = '" + cateid + "' " +
                    "WHERE TitleID = '" + titleid + "'";

                dbconnect.ExeCuteNonQuery(update);
                GridView1.DataBind();
                UpdatePanel1.Visible = false;
                lb_success.Text = titlename + " has been updated sucessfully";
            }
        }
    }
}