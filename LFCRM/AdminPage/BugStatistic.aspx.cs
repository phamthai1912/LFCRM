using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.AdminPage
{
    public partial class BugStatistic : System.Web.UI.Page
    {
        Class.csBugStatistic statistic = new Class.csBugStatistic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //check admin permission
                if ((bool)Session["LoggedIn"] == false) Response.Redirect("../UserPage/Login.aspx");
                if (((bool)Session["LoggedIn"] == true) && ((string)Session["UserRole"] != "Admin")) Response.Redirect("../UserPage/Default.aspx");

                ViewState["bugs"] = 0;
                ViewState["date"] = 0;
                ViewState["employeeid"] = 0;
                ViewState["name"] = 0;
                ViewState["title"] = 0;
                ViewState["bill"] = 0;
                ViewState["working"] = 0;

                //Check Sorting
                ViewState["_bugs"] = 0;
                ViewState["_date"] = 0;
                ViewState["_employeeid"] = 0;
                ViewState["_name"] = 0;
                ViewState["_title"] = 0;
                ViewState["_bill"] = 0;
                ViewState["_working"] = 0;

                lb_datestatus.Text = "Bug Statistics of "+DateTime.Now.ToString("MMM")+", "+DateTime.Now.Year.ToString();
                load_grid("");
            }
                
        }

        protected void txt_date_TextChanged(object sender, EventArgs e)
        {
            String dd = txt_date.Text;
            String title = txt_title.Text;
            if (dd == "")
                lb_datestatus.Text = "Bug Statistics of " + DateTime.Now.ToString("MMM") + ", " + DateTime.Now.Year.ToString();
            else
            {
                DateTime newdate = Convert.ToDateTime(txt_date.Text);
                lb_datestatus.Text = "Bug Statistics of " + newdate.ToString("MMM") + ", " + newdate.Year.ToString();
            }
            if(txt_title.Text != "")
                GridView1.DataSource = statistic.searchTitle(txt_newsearch.Text, dd, title);
            else 
                GridView1.DataSource = statistic.getResourceAlocation(dd);
            GridView1.DataBind();

        }

        public void load_grid(String date)
        {
            String dd = txt_date.Text;
            GridView1.DataSource = statistic.getResourceAlocation(dd);
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                String date = ((Label)e.Row.FindControl("lb_date")).Text;
                String employeeID = ((Label)e.Row.FindControl("lb_employeeid")).Text;
                String titleID = ((Label)e.Row.FindControl("lb_titleid")).Text;

                Label lbtitle = ((Label)e.Row.FindControl("lb_title"));
                Label lb = e.Row.Cells[7].FindControl("lb_nobugs") as Label;

                lb.Text = statistic.getNoBugs(date, employeeID, titleID);

                CheckBox cb = ((CheckBox)e.Row.FindControl("chkid"));
                if (lbtitle.Text == "")
                {
                    cb.Visible = false;
                    lb.Attributes["class"] = "label label-default";
                    lb.Text = "None";
                }
                if (lb.Text == "N/A")
                {
                    lb.Attributes["class"] = "label label-danger";
                }
            }
        }

        protected void btn_updatebugs_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[8].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked)
                    {

                        String date = ((Label)row.FindControl("lb_date")).Text;
                        String employeeid = ((Label)row.FindControl("lb_employeeid")).Text;
                        String titlid = ((Label)row.FindControl("lb_titleid")).Text;
                        Label lbtitle = ((Label)row.FindControl("lb_title"));
                        String numberbugs = row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text;

                        String currentbug = row.Cells[7].Controls.OfType<Label>().FirstOrDefault().Text;

                        if (numberbugs != "")
                        {
                            if (currentbug == "N/A")
                                statistic.addBugs(date, employeeid, titlid, numberbugs);
                            statistic.updateBugs(date, employeeid, titlid, numberbugs);
                        }
                        if (numberbugs == "")
                        {
                            statistic.deleteBugs(date, employeeid, titlid);
                        }
                    }
                }
            }
            btn_updatebugs.Visible = false;

            String dd = txt_date.Text;
            String title = txt_title.Text;
            if (txt_newsearch.Text != "")
            {
                GridView1.DataSource = statistic.searchTitle(txt_newsearch.Text, dd, title);
                GridView1.DataBind();
            }
            else load_grid(dd);

            CheckSortingGrid(GridView1);
        }

        public void CheckSortingGrid(GridView grid)
        {
            //Check Soring Grid
            DataTable dt = getDataTablefromGridview(grid);
            if ((int)ViewState["_bugs"] != 0)
            {
                if ((int)ViewState["_bugs"] % 2 == 0)
                    SortingGridview(dt, "Bugs ASC");
                else
                    SortingGridview(dt, "Bugs DESC");
            }
            if ((int)ViewState["_date"] != 0)
            {
                if ((int)ViewState["_date"] % 2 == 0)
                    SortingGridview(dt, "Date ASC");
                else
                    SortingGridview(dt, "Date DESC");
            }
            if ((int)ViewState["_employeeid"] != 0)
            {
                if ((int)ViewState["_employeeid"] % 2 == 0)
                    SortingGridview(dt, "EmployeeID ASC");
                else
                    SortingGridview(dt, "EmployeeID DESC");
            }
            if ((int)ViewState["_name"] != 0)
            {
                if ((int)ViewState["_name"] % 2 == 0)
                    SortingGridview(dt, "FullName ASC");
                else
                    SortingGridview(dt, "FullName DESC");
            }
            if ((int)ViewState["_title"] != 0)
            {
                if ((int)ViewState["_title"] % 2 == 0)
                    SortingGridview(dt, "3LD ASC");
                else
                    SortingGridview(dt, "3LD DESC");
            }
            if ((int)ViewState["_bill"] != 0)
            {
                if ((int)ViewState["_bill"] % 2 == 0)
                    SortingGridview(dt, "ProjectRoleName ASC");
                else
                    SortingGridview(dt, "ProjectRoleName DESC");
            }
            if ((int)ViewState["_working"] != 0)
            {
                if ((int)ViewState["_working"] % 2 == 0)
                    SortingGridview(dt, "Value ASC");
                else
                    SortingGridview(dt, "Value DESC");
            }

            grid.DataSource = dt;
            grid.DataBind();
        }

        protected void OnCheckedChanged(object sender, EventArgs e)
        {

            bool isUpdateVisible = false;
            CheckBox chk = (sender as CheckBox);
            if (chk.ID == "chkAll")
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        row.Cells[8].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                    }
                }
            }
            CheckBox chkAll = (GridView1.HeaderRow.FindControl("chkAll") as CheckBox);
            chkAll.Checked = true;
            int _index = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                _index++;
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[8].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    row.Cells[7].Controls.OfType<Label>().FirstOrDefault().Visible = !isChecked;
                    Label lbtitle = ((Label)row.FindControl("lb_title"));
                    if (lbtitle.Text != "")
                    { 
                        if (row.Cells[7].Controls.OfType<TextBox>().ToList().Count > 0)
                        {
                             row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Visible = isChecked;
                             String bug = statistic.getNumberBugs(row.Cells[7].Controls.OfType<Label>().FirstOrDefault().Text);

                             if (row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Visible == true)
                             {
                                 row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text = row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text;
                                 if (row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text == "")
                                    row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text = bug;
                             }
                             else
                                 row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text = bug;
                             if (row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text == "N/A")
                                 row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text = "";
                        }
                        if (isChecked && !isUpdateVisible)
                        {
                            isUpdateVisible = true;
                        }
                        if (!isChecked)
                        {
                            chkAll.Checked = false;
                        }
                    }
                }
            }            
            btn_updatebugs.Visible = isUpdateVisible;

            //Navigate to current row
            //if (chk.ID != "chkAll")
            //{
            //    GridViewRow grid = (sender as CheckBox).Parent.Parent as GridViewRow;
            //    if (grid.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Visible == true)                
            //        GridView1.Rows[grid.RowIndex].Cells[7].Focus();
            //    else
            //        GridView1.Rows[grid.RowIndex].Cells[8].Focus();
            //}
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            String dd = txt_date.Text;
            String title = txt_title.Text;

            GridView1.DataSource = statistic.searchTitle(txt_newsearch.Text, dd, title);
            GridView1.DataBind();
            CheckSortingGrid(GridView1);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("sorting_bugs"))
            {
                //Check Sorting
                ViewState["_date"] = 0;
                ViewState["_employeeid"] = 0;
                ViewState["_name"] = 0;
                ViewState["_title"] = 0;
                ViewState["_bill"] = 0;
                ViewState["_working"] = 0;

                DataTable dt = getDataTablefromGridview(GridView1);

                ViewState["bugs"] = (int)ViewState["bugs"] + 1;

                if ((int)ViewState["bugs"] % 2 == 0)
                {
                    SortingGridview(dt, "Bugs ASC");
                    ViewState["_bugs"] = 2;
                }
                else
                {
                    SortingGridview(dt, "Bugs DESC");
                    ViewState["_bugs"] = 1;
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            if (e.CommandName.Equals("sorting_date"))
            {
                //Check Sorting
                ViewState["_bugs"] = 0;                
                ViewState["_employeeid"] = 0;
                ViewState["_name"] = 0;
                ViewState["_title"] = 0;
                ViewState["_bill"] = 0;
                ViewState["_working"] = 0;

                DataTable dt = getDataTablefromGridview(GridView1);

                ViewState["date"] = (int)ViewState["date"] + 1;

                if ((int)ViewState["date"] % 2 == 0)
                {
                    SortingGridview(dt, "Date ASC");
                    ViewState["_date"] = 2;
                }
                else
                {
                    SortingGridview(dt, "Date DESC");
                    ViewState["_date"] = 1;
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            if (e.CommandName.Equals("sorting_id"))
            {
                //Check Sorting
                ViewState["_bugs"] = 0;
                ViewState["_date"] = 0;                
                ViewState["_name"] = 0;
                ViewState["_title"] = 0;
                ViewState["_bill"] = 0;
                ViewState["_working"] = 0;

                DataTable dt = getDataTablefromGridview(GridView1);

                ViewState["employeeid"] = (int)ViewState["employeeid"] + 1;

                if ((int)ViewState["employeeid"] % 2 == 0)
                {
                    SortingGridview(dt, "EmployeeID ASC");
                    ViewState["_employeeid"] = 2;
                }
                else
                {
                    SortingGridview(dt, "EmployeeID DESC");
                    ViewState["_employeeid"] = 1;
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            if (e.CommandName.Equals("sorting_fullname"))
            {
                //Check Sorting
                ViewState["_bugs"] = 0;
                ViewState["_date"] = 0;
                ViewState["_employeeid"] = 0;                
                ViewState["_title"] = 0;
                ViewState["_bill"] = 0;
                ViewState["_working"] = 0;

                DataTable dt = getDataTablefromGridview(GridView1);

                ViewState["name"] = (int)ViewState["name"] + 1;

                if ((int)ViewState["name"] % 2 == 0)
                {
                    SortingGridview(dt, "FullName ASC");
                    ViewState["_name"] = 2;
                }
                else
                {
                    SortingGridview(dt, "FullName DESC");
                    ViewState["_name"] = 1;
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            if (e.CommandName.Equals("sorting_title"))
            {
                //Check Sorting
                ViewState["_bugs"] = 0;
                ViewState["_date"] = 0;
                ViewState["_employeeid"] = 0;
                ViewState["_name"] = 0;
                ViewState["_bill"] = 0;
                ViewState["_working"] = 0;

                DataTable dt = getDataTablefromGridview(GridView1);

                ViewState["title"] = (int)ViewState["title"] + 1;

                if ((int)ViewState["title"] % 2 == 0)
                {
                    SortingGridview(dt, "3LD ASC");
                    ViewState["_title"] = 2;
                }
                else
                {
                    SortingGridview(dt, "3LD DESC");
                    ViewState["_title"] = 1;
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            if (e.CommandName.Equals("sorting_billing"))
            {
                //Check Sorting
                ViewState["_bugs"] = 0;
                ViewState["_date"] = 0;
                ViewState["_employeeid"] = 0;
                ViewState["_name"] = 0;
                ViewState["_title"] = 0;                
                ViewState["_working"] = 0;

                DataTable dt = getDataTablefromGridview(GridView1);

                ViewState["bill"] = (int)ViewState["bill"] + 1;

                if ((int)ViewState["bill"] % 2 == 0)
                {
                    SortingGridview(dt, "ProjectRoleName ASC");
                    ViewState["_bill"] = 2;
                }
                else
                {
                    SortingGridview(dt, "ProjectRoleName DESC");
                    ViewState["_bill"] = 1;
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            if (e.CommandName.Equals("sorting_working"))
            {
                //Check Sorting
                ViewState["_bugs"] = 0;
                ViewState["_date"] = 0;
                ViewState["_employeeid"] = 0;
                ViewState["_name"] = 0;
                ViewState["_title"] = 0;
                ViewState["_bill"] = 0;                

                DataTable dt = getDataTablefromGridview(GridView1);

                ViewState["working"] = (int)ViewState["working"] + 1;

                if ((int)ViewState["working"] % 2 == 0)
                {
                    SortingGridview(dt, "Value ASC");
                    ViewState["_working"] = 2;
                }
                else
                {
                    SortingGridview(dt, "Value DESC");
                    ViewState["_working"] = 1;
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        public void SortingGridview(DataTable dt,String type)
        {
            dt.DefaultView.Sort = type;
            dt = dt.DefaultView.ToTable();
        }

        public DataTable getDataTablefromGridview(GridView gridview)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            dt.Columns.Add("EmployeeID");
            dt.Columns.Add("FullName");
            dt.Columns.Add("TitleID");
            dt.Columns.Add("3LD");
            dt.Columns.Add("ProjectRoleName");
            dt.Columns.Add("Value");
            dt.Columns.Add("Bugs");

            for (int i = 0; i < gridview.Rows.Count; i++)
            {
                GridViewRow gvrow = gridview.Rows[i];
                String date = ((Label)gvrow.FindControl("lb_date")).Text;
                String employeeID = ((Label)gvrow.FindControl("lb_employeeid")).Text;
                String name = ((Label)gvrow.FindControl("lb_fullname")).Text;
                String titlid = ((Label)gvrow.FindControl("lb_titleid")).Text;

                Label lbtt = gvrow.Cells[4].FindControl("lb_title") as Label;
                String title = lbtt.Text;
                String billing = ((Label)gvrow.FindControl("lb_billing")).Text;
                String working = ((Label)gvrow.FindControl("lb_working")).Text;
                Label lb = gvrow.Cells[7].FindControl("lb_nobugs") as Label;
                String bugs = lb.Text;

                dt.Rows.Add(date, employeeID, name, titlid, title, billing, working, bugs);
            }

            return dt;
        }
    }
}