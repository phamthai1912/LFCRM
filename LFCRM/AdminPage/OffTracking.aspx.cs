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
    public partial class OffTracking : System.Web.UI.Page
    {
        csPerformanceTracking PT = new csPerformanceTracking();
        csResourceAllocation RA = new csResourceAllocation();
        csCommonClass CC = new csCommonClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //check admin permission
                if ((bool)Session["LoggedIn"] == false) Response.Redirect("../UserPage/Login.aspx");
                if (((bool)Session["LoggedIn"] == true) && ((string)Session["UserRole"] != "Admin")) Response.Redirect("../UserPage/Default.aspx");
            }
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

        public void printPTbyMonth(int month, int year, string b_TitleID)
        {
            List<DateTime> list_date = GetDates(month, year);
            DataTable list_user = PT.getUserListbyMonth(month, year);

            //-------------------------------Header--------------------------------------
            string header_PT = "<tr style='background-color: #00502F; color:white; font-weight: bold;' valign='top'>"
                                + "<td>No</td><td>Name</td>";
            for (int i = 1; i <= list_date.Count; i++)
            {
                DateTime date = Convert.ToDateTime(month.ToString() + "/" + i.ToString() + "/" + year.ToString());
                if (date.ToString("ddd") == "Sun" || date.ToString("ddd") == "Sat") header_PT += "<td style='background-color: grey'>" + i.ToString() + "</td>";
                else header_PT += "<td>" + i.ToString() + "</td>";
            }
            header_PT = header_PT + "<td>B</td><td>D</td></tr>";

            //------------------------------Content-----------------------------------
            string content_PT = "";

            for (int i = 0; i < list_user.Rows.Count; i++)
            {
                int sum = 0;
                int no_day = 0;
                string UID = list_user.Rows[i].ItemArray[0].ToString();
                content_PT = content_PT + "<tr><td>" + (i + 1).ToString() + "</td><td style='text-align:left;'>" + PT.getFullName(UID) + "</td>";
                for (int j = 1; j <= list_date.Count; j++)
                {
                    string back_color = "";
                    string text_color = "";
                    string no_bug = "";
                    string bg_image = "";
                    string date = month.ToString() + "/" + j.ToString() + "/" + year.ToString();

                    DateTime dt_date = Convert.ToDateTime(date);
                    if (dt_date.ToString("ddd") == "Sun" || dt_date.ToString("ddd") == "Sat") back_color = "grey";

                    //bg_image = "background-image:url(../Image/2T.png);background-repeat:no-repeat;background-size:100% 100%;";
                    //text_color = text_color + "; font-weight: bold;";

                    content_PT = content_PT + "<td style='background-color: " + back_color + "; color: " + text_color + "; " + bg_image + "' >" + no_bug + "</td>";
                }
                    
                content_PT = content_PT + "<td><b>" + sum.ToString() + "</b></td><td><b>" + no_day.ToString() + "</b></td>";
            }
            content_PT = content_PT + "</tr>";

            //-----------------------------Print--------------------------------------
            lbl_OT.Text = "<table style='width: 1050px' class='table table-striped table-bordered table-responsive table-condensed table-hover' valign='top'>"
                            + header_PT + content_PT + "</table>";
        }
    }
}