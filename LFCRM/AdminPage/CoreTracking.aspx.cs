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
            DateTime dt = Convert.ToDateTime(date);
            int numOfDays = DateTime.DaysInMonth(dt.Year, dt.Month);

            List<string> listweekend = new List<string>();
            string str = string.Empty;
            for (int i = 1; i <= numOfDays; i++)
            {
                listweekend.Add(dt.AddDays(i-1).DayOfWeek.ToString().Substring(0, 3));
            }

            //Config Header of Gridview
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes["style"] = "background-color:#215867;color:#ffffff;width:180px;font-size: 20px;"; 
                for (int i = 1; i <= numOfDays; i++)
                {
                    e.Row.Cells[i].Attributes["style"] = "font-size:10px;background-color:#31849b;color:#ffffff;border-left:#31849b;border-right:#31849b;";
                             
                }

                e.Row.Cells[numOfDays + 1].Attributes["style"] = "background-color:#215867;color:#ffffff;font-size: 13px;";
                e.Row.Cells[numOfDays + 2].Attributes["style"] = "background-color:#215867;color:#ffffff;width:50px;font-size: 13px;";
                
            }
            //Config Content of Gridview
            if (e.Row.DataItem != null)
            {                                
                //Config the last row of Gridview
                if (e.Row.Cells[0].Text == "BILLABLE/Day:")
                {
                    e.Row.Cells[0].Attributes["style"] = "background-color:#215867;vertical-align:middle;color:#ffffff;font-weight: 800;";
                    for (int i = 1; i <= numOfDays+2; i++)
                    {
                        e.Row.Cells[i].Attributes["style"] = "background-color: #dbeef3;font-size:12px;font-weight: bold;";
                    }
                }
                else
                {
                    e.Row.Attributes["style"] = "font-size:13px;";
                    e.Row.Cells[0].Attributes["style"] = "text-align:left;background-color:#dbe5f1;";
                    for (int i = 1; i <= numOfDays; i++)
                    {
                        if (listweekend[i - 1].ToString() == "Sun" || listweekend[i - 1].ToString() == "Sat")
                            e.Row.Cells[i].Attributes["style"] = "background-color:#7f7f7f;border:#7f7f7f";
                    }

                    if (e.Row.Cells[numOfDays + 2].Text != "&nbsp;" && Double.Parse(e.Row.Cells[numOfDays + 2].Text) > 2)
                        e.Row.Cells[numOfDays + 2].Attributes["style"] = "background-color:#ffff99;";
                    else e.Row.Cells[numOfDays + 2].Attributes["style"] = "background-color:#7f7f7f;";

                    if (e.Row.Cells[numOfDays + 1].Text != "&nbsp;" && Double.Parse(e.Row.Cells[numOfDays + 1].Text) > 8)
                        e.Row.Cells[numOfDays + 1].Attributes["style"] = "background-color:#92d050;";
                    else e.Row.Cells[numOfDays + 1].Attributes["style"] = "background-color:#7f7f7f;";
                }
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
        }

        public void loadGridCoreTracking(String _monthyear)
        {
            DateTime dt = Convert.ToDateTime(_monthyear);
            int numOfDays = DateTime.DaysInMonth(dt.Year, dt.Month);

            //Total billing per day
            String[] totalbilling = new String[numOfDays];
            for (int i = 0; i < numOfDays; i++)
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
            List<String> date = new List<String>();

            //Generate Column Gridview
            dtMonthDetails.Columns.Add("Core Tracking");
            for (int i = 1; i <= numOfDays; i++)
            {
                str = dt.ToString("MMM") + " " + i.ToString("00") + " " + dt.AddDays(i - 1).DayOfWeek.ToString().Substring(0, 3);

                dc = new DataColumn();
                dc.ColumnName = str;
                dtMonthDetails.Columns.Add(dc);

                //Create day list
                String newday = String.Empty;
                newday = dt.ToString("MM") + "/" + i.ToString("00") + "/" + dt.Year.ToString();
                date.Add(newday);
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
                for (int j=1; j <= numOfDays+2; j++)
                {
                    if (j <= numOfDays)
                    {
                        String titleid = core.getTitleID(listuserid[count].ToString(), date[j - 1].ToString());
                        if (titleid != "")
                        {
                            Int16 numbermember = Int16.Parse(core.getNumberMember(titleid, date[j - 1].ToString()));
                            if (numbermember >= 3)
                            {
                                totalmember = totalmember + numbermember;
                                totaldays++;
                                totalbilling[j-1] = (int.Parse(totalbilling[j-1].ToString()) + numbermember) + "";
                                dr[j] = numbermember + "";
                            }
                            else dr[j] = "";
                        }
                        else dr[j] = "";
                    }
                    if (j == numOfDays+1)
                    {
                        dr[j] = totaldays + "";
                    }
                    if (j == numOfDays + 2)
                    {
                        if (totaldays != 0)
                        {
                            average = (totalmember / totaldays);
                            dr[j] = average.ToString("0.0") + "";
                        }
                        else dr[j] = "0";
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
                for (int j = 1; j <= numOfDays; j++)
                {
                    if (totalbilling[j - 1] == "0")
                        totalbilling[j - 1] = "";
                    dr[j] = totalbilling[j - 1];
                }
                dr[numOfDays + 1] = "";
                dr[numOfDays + 2] = "";
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

    }
}