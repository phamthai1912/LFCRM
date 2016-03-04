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
        csResourceAllocation RA = new csResourceAllocation();

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
            string header_PT = "<tr style='background-color: #00502F; color:white; font-weight: bold;' valign='top'>"
                                +"<td>No</td><td>Name</td>";
            for (int i=1; i<= list_date.Count; i++)
            {
                DateTime date = Convert.ToDateTime(month.ToString()+"/"+i.ToString()+"/"+year.ToString());
                if (date.ToString("ddd") == "Sun" || date.ToString("ddd") == "Sat") header_PT += "<td style='background-color: grey'>" + i.ToString() + "</td>";
                else header_PT += "<td>" + i.ToString() + "</td>";
            }
            header_PT = header_PT + "<td>B</td><td>D</td></tr>";

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
                    string tooltip = "";
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
                            string str_tooltip = "";
                            bool m = false;
                            int sum_bug = 0;

                            for (int k = 0; k < Role_Hour.Rows.Count; k++)
                            {
                                string role = Role_Hour.Rows[k].ItemArray[0].ToString();
                                string hour = Role_Hour.Rows[k].ItemArray[1].ToString();
                                string titleID = Role_Hour.Rows[k].ItemArray[2].ToString();

                                if ((titleID == "") && (role == "Off")) str_tooltip = str_tooltip + "Off - " + hour + "h \r\n";
                                else if (titleID != "")
                                {
                                    DataTable data = PT.get_Mul_NoBug_ColorCode(UID, date, titleID);

                                    if (data.Rows.Count > 0)
                                    {
                                        str_tooltip = str_tooltip + RA.get3LDbyID(titleID) + " - " + hour + "h - " + data.Rows[0].ItemArray[0].ToString() + " bug(s) \r\n";
                                        sum_bug = sum_bug + (int)data.Rows[0].ItemArray[0];
                                    }
                                    else
                                    {
                                        str_tooltip = str_tooltip + RA.get3LDbyID(titleID) + " - " + hour + "h - " + "Not filled bug yet \r\n";
                                        m = true;
                                    }
                                }
                            }

                            if (m) { no_bug = "M"; text_color = "red"; }
                            else { no_bug = sum_bug.ToString(); sum += Convert.ToInt32(no_bug);  text_color = "black"; } 

                            bg_image = "background-image:url(../Image/2T.gif);background-repeat:no-repeat;background-size:100% 100%;";
                            text_color = text_color+ "; font-weight: bold;";
                            tooltip = "data-toggle='tooltip' title='" + str_tooltip + "'";
                        }
                    }
                    content_PT = content_PT + "<td style='background-color: " + back_color + "; color: "+text_color+"; "+ bg_image +"' "+tooltip+">" + no_bug + "</td>";
                }
                content_PT = content_PT + "<td><b>" + sum.ToString() + "</b></td><td><b>0</b></td>";
            }
            content_PT = content_PT + "</tr>";

            //-----------------------------Print--------------------------------------
            lbl_PT.Text = "<table style='width: 1050px' class='table table-striped table-bordered table-responsive table-condensed table-hover' valign='top'>"
                            + header_PT + content_PT + "</table>";
        }

        public void printTitlebyMonth(int month, int year)
        {
            DataTable list_title = PT.getTitleListbyMonth(month, year);
            string title_table = "<table style='width:120px;' class='table table-striped table-bordered table-responsive table-condensed table-hover' valign='top'>"
                            + "<tr valign='top'><td style='background-color: #00502F; color:white; font-weight: bold;' valign='top'>Title</td></tr>";

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