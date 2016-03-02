using LFCRM.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.AdminPage
{
    public partial class PerformanceTracking : System.Web.UI.Page
    {
        csPerformanceTracking PT = new csPerformanceTracking();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public static List<DateTime> GetDates(int month, int year)
        {
            var dates = new List<DateTime>();

            // Loop from the first day of the month until we hit the next month, moving forward a day at a time
            for (var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
            {
                dates.Add(date);
            }
            return dates;
        }

        public void printPTbyMonth(int month, int year)
        {
            List<DateTime> list_date = GetDates(month, year);
            DataTable list_user = PT.getUserListbyMonth(month, year);

            //-------------------------------Header--------------------------------------
            string header_PT = "<tr style='background-color: #00502F; color:white; font-weight: bold;'>"
                                +"<td data-toggle='tooltip' title='Add new Title'>No</td><td>Name</td>";
            for (int i=1; i<= list_date.Count; i++)
            {
                DateTime date = Convert.ToDateTime(month.ToString()+"/"+i.ToString()+"/"+year.ToString());
                if (date.ToString("ddd") == "Sun" || date.ToString("ddd") == "Sat") header_PT += "<td style='background-color: grey'>" + i.ToString() + "</td>";
                else header_PT += "<td>" + i.ToString() + "</td>";
            }
            header_PT = header_PT + "<td>Bug</td><td>Day</td></tr>";


            //------------------------------Content-----------------------------------
            string content_PT = "";

            for (int i = 0; i < list_user.Rows.Count; i++)
            {
                int sum = 0;
                string UID = list_user.Rows[i].ItemArray[0].ToString();
                content_PT = content_PT + "<tr><td>"+(i+1).ToString()+"</td><td style='text-align:left;'>" + PT.getFullName(UID) + "</td>";
                for (int j = 1; j <= list_date.Count; j++)
                {
                    string back_color = "";
                    string text_color = "";
                    string no_bug = "";
                    string bg_image = "";
                    string date = month.ToString() + "/" + j.ToString() + "/" + year.ToString();

                    //------------------------------------ Grey Sun & Sat ----------------------------------------
                    DateTime dt_date = Convert.ToDateTime(date);
                    if (dt_date.ToString("ddd") == "Sun" || dt_date.ToString("ddd") == "Sat") back_color = "grey";
                    else
                    {
                        DataTable Role_Hour = PT.getRole_Hour(UID, date);
                        if (Role_Hour.Rows.Count == 1)
                        {
                            string role = Role_Hour.Rows[0].ItemArray[0].ToString();
                            string hour = Role_Hour.Rows[0].ItemArray[1].ToString();

                            if (role == "Off") { no_bug = "Off"; back_color = "grey"; text_color = "white"; }
                            else if ((role == "Trainee") || (role == "Trainer")) { no_bug = "T"; back_color = "yellow"; }
                            else if ((role == "Billable") || (role == "Core") || (role == "Backup"))
                            {
                                DataTable data = PT.getNoBug_ColorCode(UID, date);
                                if (data.Rows.Count > 0)
                                {
                                    back_color = data.Rows[0].ItemArray[1].ToString();
                                    no_bug = data.Rows[0].ItemArray[0].ToString();

                                    sum += Convert.ToInt32(no_bug);
                                }
                                else { no_bug = "M"; text_color = "red"; } 
                            }
                        }
                        else if (Role_Hour.Rows.Count == 0) back_color = "lightgrey";
                        else if (Role_Hour.Rows.Count > 1)
                        {
                            bg_image = "background-image:url(../Image/2T.gif);background-repeat:no-repeat;background-size:30px 30px;";
                            no_bug = "2T";
                            text_color = "black; font-weight: bold";
                        }
                    }
                    content_PT = content_PT + "<td style='background-color: " + back_color + "; color: "+text_color+"; "+ bg_image +"'>" + no_bug + "</td>";
                }
                content_PT = content_PT + "<td><b>" + sum.ToString() + "</b></td><td><b>0</b></td>";
            }
            content_PT = content_PT + "</tr>";

            //-----------------------------Print--------------------------------------
            lbl_PT.Text = "<table style='width: 1050px' class='table table-striped table-bordered table-responsive table-condensed table-hover'>"
                            + header_PT + content_PT + "</table>";
        }

        public void printTitlebyMonth(int month, int year)
        {
            DataTable list_title = PT.getTitleListbyMonth(month, year);
            string title_table = "<table style='width:120px;' class='table table-striped table-bordered table-responsive table-condensed table-hover'>"
                            +"<tr><td style='background-color: #00502F; color:white; font-weight: bold;'>Title</td></tr>";

            if (list_title.Rows.Count > 0)
            {
                for (int i = 0; i < list_title.Rows.Count; i++)
                {
                    title_table = title_table + "<tr><td style='background-color: " + list_title.Rows[i].ItemArray[1].ToString() + ";'>" + list_title.Rows[i].ItemArray[0].ToString() + "</td></tr>";
                }
            }
            title_table = title_table + "</table>";

            lbl_Title.Text = title_table;
        }

        protected void txt_date_TextChanged(object sender, EventArgs e)
        {
            string[] s = txt_date.Text.Split('/');

            printPTbyMonth(Convert.ToInt32(s[0]),Convert.ToInt32(s[1]));
            printTitlebyMonth(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]));
        }
    }
}