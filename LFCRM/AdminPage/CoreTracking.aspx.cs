using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.AdminPage
{
    public partial class CoreTracking : System.Web.UI.Page
    {
        Class.csCoreTracking core = new Class.csCoreTracking();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //check admin permission
                if ((bool)Session["LoggedIn"] == false) Response.Redirect("../UserPage/Login.aspx");
                if (((bool)Session["LoggedIn"] == true) && ((string)Session["UserRole"] != "Admin")) Response.Redirect("../UserPage/Default.aspx");

                txt_date.Text = DateTime.Now.ToString("MM/yyyy");
                String date = txt_date.Text;
                if (date == "")
                {
                    date = DateTime.Now.ToString("MM/yyyy");
                }
                lb_status.Text = "Core Tracking on " + (Convert.ToDateTime(date)).ToString("MMM, yyyy");
                loadGridCoreTracking(date);
            }                        
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            String date = txt_date.Text;
            if (date == "")
                date = DateTime.Now.ToString("MM/yyyy");
            List<DateTime> listdatetime = GetDataDate(date);

            List<string> listweekend = new List<string>();
            string str = string.Empty;
            for (int i = 0; i < listdatetime.Count; i++)
            {
                listweekend.Add(listdatetime[i].DayOfWeek.ToString().Substring(0, 3));
            }

            //Config Header of Gridview
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes["style"] = "background-color:#215867;color:#ffffff;width:300px;font-size: 18px;";
                for (int i = 1; i <= listdatetime.Count + 2; i++)
                {
                    e.Row.Cells[i].Attributes["style"] = "font-size:10px;background-color:#31849b;color:#ffffff;border-left:#31849b;border-right:#31849b;";
                             
                }

                e.Row.Cells[listdatetime.Count + 1].Attributes["style"] = "background-color:#215867;color:#ffffff;font-size: 13px;";
                e.Row.Cells[listdatetime.Count + 2].Attributes["style"] = "background-color:#215867;color:#ffffff;width:50px;font-size: 13px;";
                
            }
            //Config Content of Gridview
            if (e.Row.DataItem != null)
            {                                
                //Config the last row of Gridview
                if (e.Row.Cells[0].Text == "BILLABLE/Day:")
                {
                    e.Row.Cells[0].Attributes["style"] = "background-color:#215867;vertical-align:middle;color:#ffffff;font-weight: 800;";
                    for (int i = 1; i <= listdatetime.Count + 2; i++)
                    {
                        e.Row.Cells[i].Attributes["style"] = "background-color: #dbeef3;font-size:11px;font-weight: bold;";
                    }
                }
                else
                {
                    e.Row.Attributes["style"] = "font-size:12px;";
                    e.Row.Cells[0].Attributes["style"] = "text-align:left;background-color:#dbe5f1;";
                    for (int i = 1; i <= listdatetime.Count; i++)
                    {
                        e.Row.Cells[i].Attributes["style"] = "font-size: 11px;";
                        if (listweekend[i - 1].ToString() == "Sun" || listweekend[i - 1].ToString() == "Sat")
                            e.Row.Cells[i].Attributes["style"] = "background-color:#7f7f7f;border:#7f7f7f;font-size: 11px;";
                    }

                    if (e.Row.Cells[listdatetime.Count + 2].Text != "&nbsp;" && Double.Parse(e.Row.Cells[listdatetime.Count + 2].Text) > 2)
                        e.Row.Cells[listdatetime.Count + 2].Attributes["style"] = "background-color:#ffff99;";
                    else e.Row.Cells[listdatetime.Count + 2].Attributes["style"] = "background-color:#7f7f7f;";

                    if (e.Row.Cells[listdatetime.Count + 1].Text != "&nbsp;" && Double.Parse(e.Row.Cells[listdatetime.Count + 1].Text) > 8)
                        e.Row.Cells[listdatetime.Count + 1].Attributes["style"] = "background-color:#92d050;";
                    else e.Row.Cells[listdatetime.Count + 1].Attributes["style"] = "background-color:#7f7f7f;";
                }
            }
        }
        
        public void loadGridCoreTracking(String _monthyear)
        {
            List<DateTime> listdatetime = GetDataDate(_monthyear);

            //Total billing per day
            String[] totalbilling = new String[listdatetime.Count];
            for (int i = 0; i < listdatetime.Count; i++)
            {
                totalbilling[i] = "0";
            }

            //Get username and userid
            List<string> listname = new List<string>();
            List<string> listuserid = new List<string>();
            DataTable listcore = core.getListCore(_monthyear);
            if (listcore != null)
            {
                for (int i = 0; i < listcore.Rows.Count; i++)
                {
                    listuserid.Add(listcore.Rows[i][0].ToString());
                    listname.Add(listcore.Rows[i][1].ToString());
                }
            }                

            DataTable dtMonthDetails = new DataTable("month");
            DataRow dr = null;       

            DataColumn dc = null;
            String str = String.Empty;

            //Generate Column Gridview
            dtMonthDetails.Columns.Add("Core Tracking");
            for (int i = 0; i < listdatetime.Count; i++)
            {
                str = listdatetime[i].ToString("MMM") + " " + listdatetime[i].ToString("dd") + " " + listdatetime[i].DayOfWeek.ToString().Substring(0, 3);
                
                dc = new DataColumn();
                dc.ColumnName = str;
                dtMonthDetails.Columns.Add(dc);
            }
            dtMonthDetails.Columns.Add("Total Day");
            dtMonthDetails.Columns.Add("Average number of member");

            //Generate Rows Gridview
            int count = 0;
            while(count < listname.Count)
            {
                dr = dtMonthDetails.NewRow();
                dr[0] = listname[count].ToString();
                float totalmember = 0;
                float totaldays = 0;
                float average = 0;
                for (int j = 0; j < listdatetime.Count + 2; j++)
                {
                    if (j < listdatetime.Count)
                    {
                        String titleid = core.getTitleID(listuserid[count].ToString(), listdatetime[j].ToString("MM/dd/yyyy"));
                        if (titleid != "")
                        {
                            float numberHoursmember = float.Parse(core.getNumberHoursMember(titleid, listdatetime[j].ToString("MM/dd/yyyy")));
                            float numbermember = numberHoursmember / 8;
                            if (numbermember >= 3)
                            {
                                totalmember = totalmember + numbermember;
                                totaldays++;

                                if (float.Parse(core.getTotalBillByDate(listdatetime[j].ToString("MM/dd/yyyy"))) % 8 == 0)
                                    totalbilling[j] = (float.Parse(core.getTotalBillByDate(listdatetime[j].ToString("MM/dd/yyyy"))) / 8) + "";
                                else
                                    totalbilling[j] = (float.Parse(core.getTotalBillByDate(listdatetime[j].ToString("MM/dd/yyyy"))) / 8).ToString("0.0") + "";
                                //totalbilling[j] = (float.Parse(core.getTotalBillByDate(listdatetime[j].ToString("MM/dd/yyyy"))) / 8).ToString("0.0") + "";

                                if (numberHoursmember%8 == 0 )
                                    dr[j+1] = numbermember + "";
                                else
                                    dr[j + 1] = numbermember.ToString("0.0") + "";
                            }
                            else dr[j+1] = "";
                        }
                        else dr[j+1] = "";
                    }
                    if (j == listdatetime.Count)
                    {
                        dr[j+1] = totaldays + "";
                    }
                    if (j == listdatetime.Count + 1)
                    {
                        if (totaldays != 0)
                        {
                            average = (totalmember / totaldays);
                            dr[j+1] = average.ToString("0.0") + "";
                        }
                        else dr[j+1] = "0";
                    }
                }
                dtMonthDetails.Rows.Add(dr);  
                count++;            
            }

            //Sorting Totodal by DESC
            SortingGridview(dtMonthDetails, "Total Day DESC");
            //Generate the last row
            if (count == listname.Count)
            {
                dr = dtMonthDetails.NewRow();
                dr[0] = "BILLABLE/Day:";
                for (int j = 0; j < listdatetime.Count; j++)
                {
                    if (totalbilling[j] == "0")
                        totalbilling[j] = "";
                    dr[j+1] = totalbilling[j];
                }
                dr[listdatetime.Count + 1] = "";
                dr[listdatetime.Count + 2] = "";
                dtMonthDetails.Rows.Add(dr);
            }  
            GridView1.DataSource = dtMonthDetails;
            GridView1.DataBind();
        }

        protected void txt_date_TextChanged(object sender, EventArgs e)
        {
            String date = txt_date.Text;
            if(date == "")
                date = DateTime.Now.ToString("MM/yyyy");
            lb_status.Text = "Core Tracking on " + (Convert.ToDateTime(date)).ToString("MMM, yyyy");
            loadGridCoreTracking(date);
        }

        //Sorting
        public void SortingGridview(DataTable dt, String type)
        {
            dt.DefaultView.Sort = type;
            dt = dt.DefaultView.ToTable();
        }

        //Get List Number Day
        public List<DateTime> GetDataDate(string _month)
        {
            List<DateTime> list = new List<DateTime>();

            DateTime dt = Convert.ToDateTime(_month);
            DateTime predt = Convert.ToDateTime(_month);
            predt = predt.AddMonths(-1);
            int numOfDays = DateTime.DaysInMonth(dt.Year, dt.Month);
            int numOfDaysLM = DateTime.DaysInMonth(predt.Year, predt.Month);

            for (int i = 1; i <= numOfDaysLM; i++)
            {
                if (i >= 19)
                {
                    list.Add(Convert.ToDateTime(predt.Month.ToString("00") + "/" + i.ToString("00") + "/" + predt.Year));
                }

            }
            for (int j = 1; j <= numOfDays; j++)
            {
                if (j <= 18)
                {
                    list.Add(Convert.ToDateTime(dt.Month.ToString("00") + "/" + j.ToString("00") + "/" + dt.Year));
                }
            }
            return list;
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {           

            //Config Header of Gridview
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[0].Attributes["style"] = "background-color:#215867;color:#ffffff;width:180px;font-size: 15px;vertical-align: middle;text-align: center;";

                e.Row.Cells[1].Attributes["style"] = "background-color:#215867;color:#ffffff;font-size: 13px;vertical-align: middle;text-align: center;width: 20px;";
                e.Row.Cells[2].Attributes["style"] = "background-color:#215867;color:#ffffff;width:50px;font-size: 13px;vertical-align: middle;text-align: center;";
            }

            //Config Content of Gridview
            if (e.Row.DataItem != null)
            {
                e.Row.Cells[3].Visible = false;
                e.Row.Attributes["style"] = "font-size:13px;";
                e.Row.Cells[0].Attributes["style"] = "text-align:left;background-color:#dbe5f1;";
            }
        }
        
        public DataTable GetListUserReceiveBonus(GridView gridview)
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("Employee");
            tb.Columns.Add("Total Day");
            tb.Columns.Add("Average number of member");
            tb.Columns.Add("Averge");

            for (int i = 0; i < gridview.Rows.Count-1; i++)
            {
                GridViewRow gvrow = gridview.Rows[i];
                float ave = float.Parse(gvrow.Cells[gvrow.Cells.Count - 2].Text.Replace("&nbsp;", "")) / float.Parse(gvrow.Cells[gvrow.Cells.Count - 1].Text.Replace("&nbsp;", ""));
                float numbercoreday = float.Parse(gvrow.Cells[gvrow.Cells.Count - 2].Text.Replace("&nbsp;", ""));
                if (ave.ToString() != "NaN" && numbercoreday >=10)
                    tb.Rows.Add(gvrow.Cells[0].Text, gvrow.Cells[gvrow.Cells.Count - 2].Text, gvrow.Cells[gvrow.Cells.Count - 1].Text, ave);
            }
            return tb;
        }

        public int GetNumberCore(GridView gridview)
        {
            float total = 0;
            int numberday = 0;
            for (int i = 1; i < gridview.Rows[gridview.Rows.Count-1].Cells.Count - 2; i++)
            {
                if(gridview.Rows[gridview.Rows.Count - 1].Cells[i].Text.Replace("&nbsp;", "0") != "0")
                    numberday++;
                total = total + float.Parse(gridview.Rows[gridview.Rows.Count - 1].Cells[i].Text.Replace("&nbsp;", "0"));
            }
            int number = 0;
            if(numberday!=0)
                number = Convert.ToInt16( (total / numberday)*20/100);

            return number;
        }

        protected void btn_view_Click(object sender, EventArgs e)
        {

            DataTable dt = GetListUserReceiveBonus(GridView1);
            SortingGridview(dt, "Averge DESC");

            int topcore = GetNumberCore(GridView1);

            if (dt != null)
            {
                DataTable newdt = new DataTable();
                newdt.Columns.Add("Core Name");
                newdt.Columns.Add("Total Day");
                newdt.Columns.Add("Average number of member");
                newdt.Columns.Add("Averge");

                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (count < topcore)
                        newdt.Rows.Add(dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][2], dt.Rows[i][3]);
                    count++;
                }

                GridView2.DataSource = newdt;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#topcore').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowTopCoreScript", sb.ToString(), false);
        }

    }
}