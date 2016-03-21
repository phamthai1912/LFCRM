using LFCRM.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LFCRM.AdminPage
{
    public partial class OffTracking : System.Web.UI.Page
    {
        csPerformanceTracking PT = new csPerformanceTracking();
        csResourceAllocation RA = new csResourceAllocation();
        csCommonClass CC = new csCommonClass();
        csOffTracking OT = new csOffTracking();

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

        public void printOTbyMonth(int month, int year, string b_TitleID)
        {
            List<DateTime> list_date = GetDates(month, year);
            DataTable list_user = OT.getUserListbyMonth(month, year);

            //-------------------------------Header--------------------------------------
            string header_PT = "<tr style='background-color: #00502F; color:white; font-weight: bold;' valign='top'>"
                                + "<td>No</td><td>Name</td>";
            for (int i = 1; i <= list_date.Count; i++)
            {
                DateTime date = Convert.ToDateTime(month.ToString() + "/" + i.ToString() + "/" + year.ToString());
                if (date.ToString("ddd") == "Sun" || date.ToString("ddd") == "Sat") header_PT += "<td style='background-color: grey'>" + i.ToString() + "</td>";
                else header_PT += "<td>" + i.ToString() + "</td>";
            }
            header_PT = header_PT + "<td>Total (hrs)</td></tr>";

            //------------------------------Content-------------------------------------
            string content_PT = "";
            int count = 0;

            for (int i = 0; i < list_user.Rows.Count; i++)
            {
                int sum = 0;
                string UID = list_user.Rows[i].ItemArray[0].ToString();
                string EID = list_user.Rows[i].ItemArray[1].ToString();
                int int_EID;
                if (int.TryParse(EID, out int_EID))
                {
                    count++;
                    content_PT = content_PT + "<tr><td>" + count.ToString() + "</td><td style='text-align:left;'>" + PT.getFullName(UID) + "</td>";
                    for (int j = 1; j <= list_date.Count; j++)
                    {
                        string back_color = "";
                        string no_hours = "";
                        string cssclass = "";
                        string date = month.ToString() + "/" + j.ToString() + "/" + year.ToString();

                        //---------------------------------Highlight Weekends and current day-------------------------
                        DateTime dt_date = Convert.ToDateTime(date);
                        if (dt_date.ToString("ddd") == "Sun" || dt_date.ToString("ddd") == "Sat") back_color = "grey";
                        if (dt_date.ToString() == DateTime.Today.ToString()) back_color = "lightblue";

                        //---------------------------------Print from past and current -------------------------------
                        if (j <= Convert.ToInt16(DateTime.Today.ToString("dd")))
                        {
                            DataTable Role_Hour = OT.getRole_Hour(UID, date);

                            if (Role_Hour.Rows.Count > 0)
                            {
                                string role = Role_Hour.Rows[0].ItemArray[0].ToString();
                                string hour = Role_Hour.Rows[0].ItemArray[1].ToString();

                                no_hours = hour;
                                sum = sum + Convert.ToInt16(no_hours);
                                if (hour == "8") back_color = "lightgreen";
                                else back_color = "yellow";
                            }
                        }
                        //-----------------------------Print from future--------------------------------------------
                        else
                        {

                        }
                        content_PT = content_PT + "<td style='background-color: " + back_color + ";' "+cssclass+" >" + no_hours + "</td>";
                    } 
                    content_PT = content_PT + "<td><b>" + sum.ToString() + "</b></td>";
                }
            }
            content_PT = content_PT + "</tr>";

            //-----------------------------Print--------------------------------------
            lbl_OT.Text = "<table style='width: 1180px' class='table table-striped table-bordered table-responsive table-condensed table-hover' valign='top'>"
                            + header_PT + content_PT + "</table>";
        }

        protected void txt_date_TextChanged(object sender, EventArgs e)
        {
            string[] s = txt_date.Text.Split('/');

            lbl_header.Text = "PTO Tracking on " + (Convert.ToDateTime(txt_date.Text)).ToString("MMM, yyyy");

            printOTbyMonth(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), "");
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }

        protected void txt_From_TextChanged(object sender, EventArgs e)
        {
        }

        protected void txt_To_TextChanged(object sender, EventArgs e)
        {
            if(txt_From.Text!="")
            {
                DateTime from;
                DateTime to;
                if (DateTime.TryParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out from))

                lbl_Message.Text = (to - from).TotalDays.ToString();
            }
        }

        protected void btn_AddUpcomingPTO_Click(object sender, EventArgs e)
        {

        }
    }
}