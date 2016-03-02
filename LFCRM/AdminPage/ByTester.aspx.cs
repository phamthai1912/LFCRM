using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.AdminPage
{
    public partial class ByTester : System.Web.UI.Page
    {
        Class.csByTester tester = new Class.csByTester();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            String name = txt_username.Text;
            String _start = txt_startdate.Text;
            String _end = txt_enddate.Text;
            list = tester.getProfile(name);

            if (list.Count > 0)
            {
                lb_userstatus.Visible = false;
                lb_id.Text = list[0].ToString();
                lb_fullname.Text = list[1].ToString();
                lb_email.Text = list[2].ToString();
                lb_phone.Text = list[3].ToString();
                cb_active.Checked = Convert.ToBoolean(list[4].ToString());

                loadBugHunter(lb_id.Text);
                loadTitles(lb_id.Text, _start, _end);
                loadPeoples(lb_id.Text, lb_fullname.Text, _start, _end);
                loadMessage(lb_id.Text, _start, _end);
            }
            else
            {
                lb_userstatus.Visible = true;
                lb_userstatus.Text = "This user does not exist";
            }
        }

        //Get List Worked Titles
        public void loadTitles(String _id, String _start, String _end)
        {
            GridViewTitles.DataSource = tester.getListTitle(_id, _start, _end);
            GridViewTitles.DataBind();
        }

        //Get Top 10 People Interaction
        public void loadPeoples(String _id, String _fullanem, String _start, String _end)
        {
            GridViewPeople.DataSource = tester.getListPeople(_id, _fullanem,_start, _end);
            GridViewPeople.DataBind();
        }

        //Get Date Bug Hunter
        public void loadBugHunter(String _id)
        {
            StringBuilder html = new StringBuilder();

            DataTable tb = tester.getTimeBugHunter(_id);
            String hunter = "";
            if (tb != null)
            {
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    hunter = hunter + tb.Rows[i][0].ToString() + "<br />";
                }
            }
            html.Append(hunter);
            PlaceHolder_bughunter.Controls.Add(new Literal { Text = html.ToString() });
        }

        //Get Feed Back
        public void loadMessage(String _id, String _start, String _end)
        {
            DataSet listfeedback = tester.getFeedBack(_id, _start, _end);
            GridViewFeedback.DataSource = listfeedback;
            GridViewFeedback.DataBind();
        }

        //Interact with List Titles GridView
        protected void GridViewTitles_RowDataBound(object sender, GridViewRowEventArgs e)
        {     
            if (e.Row.DataItem != null)
            {
                String _id = lb_id.Text;
                Label lb3ld = (Label)e.Row.Cells[1].FindControl("lb_3ld");
                String _3ld = lb3ld.Text;
                String _start = txt_startdate.Text;
                String _end = txt_enddate.Text;
                String _month = ((Label)e.Row.FindControl("lb_date")).Text;

                Label lbbill = ((Label)e.Row.FindControl("lb_bills"));
                lbbill.Text = tester.getBill(_id, _3ld, _start, _end, _month) + " Day(s)";

                Label lbcore = ((Label)e.Row.FindControl("lb_cores"));
                lbcore.Text = tester.getCore(_id, _3ld, _start, _end, _month) + " Day(s)";

                Label lbbackup = ((Label)e.Row.FindControl("lb_backup"));
                lbbackup.Text = tester.getBackup(_id, _3ld, _start, _end, _month) + " Day(s)";

                Label lbtotalday = ((Label)e.Row.FindControl("lb_numberdays"));
                int x = Convert.ToInt32(tester.getBill(_id, _3ld, _start, _end, _month)) + Convert.ToInt32(tester.getCore(_id, _3ld, _start, _end, _month)) + Convert.ToInt32(tester.getBackup(_id, _3ld, _start, _end, _month));
                lbtotalday.Text = Convert.ToString(x) + " Day(s)";

                Label lbtotalbug = ((Label)e.Row.FindControl("lb_totalbugs"));
                String totalBugOfTeam = tester.getTotalBugTeam(_id, _3ld, _start, _end, _month);
                if (totalBugOfTeam == "")
                    totalBugOfTeam = "0";
                lbtotalbug.Attributes["Title"] = "Bug in " + lbtotalday.Text;

                String totlbuguser = tester.getTotalBugUser(_id, _3ld, _start, _end, _month);
                if (totlbuguser == "")
                    totlbuguser = "0";
                lbtotalbug.Text = totlbuguser + "/" + totalBugOfTeam;

                Label lbavebugs = ((Label)e.Row.FindControl("lb_bugperformance"));
                float totalday = x;
                float totalpeople = Convert.ToInt64(tester.getTotalPeopleOnTitle(_id, _3ld, _start, _end, _month));
                float totalbugteam = Convert.ToInt64(totalBugOfTeam);
                float ave = (totalbugteam / totalday / totalpeople);
                lbavebugs.Text = ave.ToString("0.00") + " Bug(s)/Day";

            }


        }
        
        protected void GridViewTitles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String username = txt_username.Text;
            String userid = lb_id.Text;
            String _start = txt_startdate.Text;
            String _end = txt_enddate.Text;

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.Equals("DetailTitles"))
            {
                GridViewRow gvrow = GridViewTitles.Rows[index];
                String _3ld = ((Label)gvrow.Cells[1].FindControl("lb_3ld")).Text;
                String _month = ((Label)gvrow.Cells[0].FindControl("lb_date")).Text;

                DataTable table = tester.getListCoresOnTitle(userid, _3ld, _start, _end,_month);
                DataTable table1 = tester.getListCoWorkerOnTitle(userid, _3ld, _start, _end, _month);

                StringBuilder html1 = new StringBuilder();
                StringBuilder html2 = new StringBuilder();
                String cores = "";
                if (table != null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i][0].ToString() == username)
                            cores = cores + "<h4><span class='label label-info'>" + table.Rows[i][0].ToString() + " (" + table.Rows[i][1].ToString() + " days)</span></h4>";
                        else cores = cores + table.Rows[i][0].ToString() + " (" + table.Rows[i][1].ToString() + " days)<br />";
                    }
                }
                else cores = "No cores";

                html1.Append(cores);

                String people = "";
                if (table1 != null)
                {
                    for (int i = 0; i < table1.Rows.Count; i++)
                    {
                        
                        if (table1.Rows[i][0].ToString() != username)
                            people = people + "<tr><td class='modal-body'>" + table1.Rows[i][0].ToString() + "</td><td  class='modal-body'>" + table1.Rows[i][1].ToString() + " Day(s)</td></tr>";
                    }
                }
                else people = "No people";

                html2.Append(people);

                //Append the HTML string to Placeholder.
                place_coreslist.Controls.Add(new Literal { Text = html1.ToString() });
                place_peopledetails.Controls.Add(new Literal { Text = html2.ToString() });

                //Load bug hunter
                loadBugHunter(userid);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#detailTitle').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
            }
        }

        //Interact with List People GridView
        protected void GridViewPeople_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            String userid = lb_id.Text;
            String _start = txt_startdate.Text;
            String _end = txt_enddate.Text;
            if (e.Row.DataItem != null)
            {
                String _fullname = ((Label)e.Row.FindControl("lb_P_fullname")).Text;
                if (_fullname == lb_fullname.Text)
                    e.Row.Visible = false;

                Label _titles = (Label)e.Row.FindControl("lb_P_title");

                DataTable tb = tester.getListTitleWithUser(userid,_fullname,_start,_end);
                String listtitle = "";
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    listtitle = listtitle + tb.Rows[i][0].ToString() + ", ";
                }
                _titles.Text = listtitle;
            }
        }

        protected void GridViewPeople_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String username = txt_username.Text;
            String userid = lb_id.Text;
            String _start = txt_startdate.Text;
            String _end = txt_enddate.Text;

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.Equals("DetailsPeople"))
            {
                GridViewRow gvrow = GridViewPeople.Rows[index];
                String _name = ((Label)gvrow.Cells[0].FindControl("lb_P_fullname")).Text;

                DataTable table = tester.getListDatePeopleWorking(userid, _name, _start, _end);
                StringBuilder html = new StringBuilder();

                String people = "";

                if (table != null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        people = people + "<tr><td class='modal-body'>" + table.Rows[i][0].ToString() + "</td><td class='modal-body'>" +
                            table.Rows[i][1].ToString() + "</td></tr>";
                    }
                }
                else people = "No days";

                html.Append(people);
                PlaceHolder_people.Controls.Add(new Literal { Text = html.ToString() });

                //Load bug hunter
                loadBugHunter(userid);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#detailPeople').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
            }
        }

        //Sorting Gridview
        public void SortingGridview(DataTable dt, String type)
        {
            dt.DefaultView.Sort = type;
            dt = dt.DefaultView.ToTable();
        }

        public DataTable getDataTablefromGridview(GridView gridview)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FullName");
            dt.Columns.Add("3LD");
            dt.Columns.Add("NUMBER");

            for (int i = 0; i < gridview.Rows.Count; i++)
            {
                GridViewRow gvrow = gridview.Rows[i];
                String fullname = ((Label)gvrow.FindControl("lb_P_fullname")).Text;
                String _3ld = ((Label)gvrow.FindControl("lb_P_title")).Text;
                String _number = ((Label)gvrow.FindControl("lb_P_days")).Text;

                dt.Rows.Add(fullname, _3ld, _number);
            }

            return dt;
        }

        protected void GridViewTitles_DataBound(object sender, EventArgs e)
        {

            //Merge GRID
            for (int i = GridViewTitles.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = GridViewTitles.Rows[i];
                GridViewRow previousRow = GridViewTitles.Rows[i - 1];
                Label lbrow1 = (Label)row.Cells[0].FindControl("lb_date");
                Label lbrow2 = (Label)previousRow.Cells[0].FindControl("lb_date");
                for (int j = 0; j < 1; j++)
                {
                    if (lbrow1.Text == lbrow2.Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                                previousRow.Cells[j].Attributes["style"] = "padding-top:"+previousRow.Cells[j].RowSpan*3+"%;";
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                                previousRow.Cells[j].Attributes["style"] = "padding-top:" + previousRow.Cells[j].RowSpan * 3 + "%;";
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }
        }
    }
}