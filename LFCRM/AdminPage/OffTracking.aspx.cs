using LFCRM.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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

            //------------------------------Content--------------------------------------
            string content_PT = "";
            int count = 0;

            for (int i = 0; i < list_user.Rows.Count; i++)
            {
                int sum = 0;
                string UID = list_user.Rows[i].ItemArray[0].ToString();
                string EID = list_user.Rows[i].ItemArray[1].ToString();
                string fullName = list_user.Rows[i].ItemArray[2].ToString();
                int int_EID;
                if (int.TryParse(EID, out int_EID))
                {
                    count++;
                    content_PT = content_PT + "<tr><td>" + count.ToString() + "</td><td style='text-align:left;'>" + fullName + "</td>";
                    for (int j = 1; j <= list_date.Count; j++)
                    {
                        string back_color = "";
                        string no_hours = "";
                        string cssclass = "";
                        string date = month.ToString() + "/" + j.ToString() + "/" + year.ToString();
                        DateTime dt1 = DateTime.Parse(date);
                        DateTime dt2 = DateTime.Now;

                        //--------------------------------- Highlight Weekends and current day -------------------------
                        DateTime dt_date = Convert.ToDateTime(date);
                        if (dt_date.ToString("ddd") == "Sun" || dt_date.ToString("ddd") == "Sat") back_color = "grey";
                        if (dt_date.ToString() == DateTime.Today.ToString()) back_color = "lightblue";

                        //--------------------------------- Print from past and current -------------------------------
                        if (dt1.Date <= dt2.Date)
                        {
                            DataTable Role_Hour = OT.getRoleHour(UID, date);

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
                        //----------------------------- Print from future --------------------------------------------
                        else
                        {
                            DataTable Role_Hour = OT.getRoleHourFromTracking(UID, date);

                            if (Role_Hour.Rows.Count > 0)
                            {
                                string hour = Role_Hour.Rows[0].ItemArray[0].ToString();

                                no_hours = hour;
                                sum = sum + Convert.ToInt16(no_hours);
                                if (hour == "8") back_color = "lightgreen";
                                else back_color = "yellow";
                            }
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

        protected void txt_Month_TextChanged(object sender, EventArgs e)
        {
            string[] s = txt_Month.Text.Split('/');

            lbl_header.Text = "PTO Tracking on " + (Convert.ToDateTime(txt_Month.Text)).ToString("MMM, yyyy");

            printOTbyMonth(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), "");
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addUpcomingPTOModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }

        protected void btn_Remove_Click(object sender, EventArgs e)
        {
            DataTable offTracking = OT.getOffTracking();

            for (int i = 0; i < offTracking.Rows.Count; i++)
            {
                TableRow tbr = new TableRow();
                TableCell tbc1 = new TableCell();
                TableCell tbc2 = new TableCell();
                TableCell tbc3 = new TableCell();
                TableCell tbc4 = new TableCell();
                TableCell tbc5 = new TableCell();

                Label lbl_UID = new Label();
                Label lbl_FullName = new Label();
                Label lbl_Date = new Label();
                Label lbl_Hours = new Label();
                Button btn_Remove = new Button();

                string offID = offTracking.Rows[i].ItemArray[4].ToString();
                lbl_UID.ID = "lbl_UID" + i;
                lbl_UID.Text = offTracking.Rows[i].ItemArray[0].ToString();
                lbl_FullName.ID = "lbl_FullName" + i;
                lbl_FullName.Text = offTracking.Rows[i].ItemArray[1].ToString();
                lbl_Date.ID = "lbl_Date" + i;
                lbl_Date.Text = offTracking.Rows[i].ItemArray[2].ToString();
                lbl_Hours.ID = "lbl_Hours" + i;
                lbl_Hours.Text = offTracking.Rows[i].ItemArray[3].ToString();
                btn_Remove.ID = "btn_Remove" + offID;
                btn_Remove.Text = " - ";
                btn_Remove.CssClass = "btn btn-danger btn-sm";
                btn_Remove.Click += new EventHandler(btn_RemoveClick_event);

                tbc1.Controls.Add(lbl_UID);
                tbc2.Controls.Add(lbl_FullName);
                tbc3.Controls.Add(lbl_Date);
                tbc4.Controls.Add(lbl_Hours);
                tbc5.Controls.Add(btn_Remove);

                tbr.Cells.Add(tbc1);
                tbr.Cells.Add(tbc2);
                tbr.Cells.Add(tbc3);
                tbr.Cells.Add(tbc4);
                tbr.Cells.Add(tbc5);

                //ph_DynamicOffTracking.Controls.Add(tbr);
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteUpcomingPTOModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }

        protected void btn_RemoveClick_event(object sender, EventArgs e)
        {
            //Button btn = (Button)sender;
            //string buttonId = btn.ID;

            //OT.deleteOffTrackingByID(Regex.Match(buttonId, @"\d+").Value);

            //lbl_OffMessage.Text = Regex.Match(buttonId, @"\d+").Value + " is removed";

            //lbl_OffMessage.Text = "aaaaaaaa";
        }

        protected void txt_From_TextChanged(object sender, EventArgs e)
        {
            countDays();
        }

        protected void txt_To_TextChanged(object sender, EventArgs e)
        {
            countDays();
        }

        protected void btn_AddUpcomingPTO_Click(object sender, EventArgs e)
        {
            if (lbl_UID.Text != "")
            {
                string UID = RA.getIDbyEmployeeID(lbl_UID.Text);

                //------------------------------Half Day ----------------------------------
                if (ddl_Type.SelectedItem.Text == "Half day")
                {
                    if (txt_Date.Text != "")
                    {
                        DateTime halfdate = DateTime.Parse(txt_Date.Text);
                        if (halfdate > DateTime.Now)
                        {
                            if ((halfdate.ToString("ddd") != "Sun") && (halfdate.ToString("ddd") != "Sat"))
                            {
                                if (!OT.checkOffUserExistByDate(halfdate.ToString(), UID))
                                {
                                    OT.addOffTracking(txt_Date.Text, UID, 3);
                                    lbl_Message.Text = "Add successfully";
                                    lbl_Message.ForeColor = Color.Green;
                                }
                                else
                                {
                                    lbl_Message.Text = "This user already registered for this day";
                                    lbl_Message.ForeColor = Color.Red;
                                }
                            }
                            else
                            {
                                lbl_Message.Text = "Cannot add for weekends";
                                lbl_Message.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            lbl_Message.Text = "Cannot add for that day. Only add for future";
                            lbl_Message.ForeColor = Color.Red;
                        }
                    }
                }
                //------------------------------Full Day ----------------------------------
                else
                {
                    if ((txt_To.Text != "") && (txt_From.Text != ""))
                    {
                        DateTime dateTo = DateTime.Parse(txt_To.Text);
                        DateTime dateFrom = DateTime.Parse(txt_From.Text);

                        if (dateFrom > DateTime.Now)
                        {
                            if (checkDateExistbyRange(UID, dateFrom, dateTo))
                            {
                                while (dateFrom <= dateTo)
                                {
                                    if ((dateFrom.ToString("ddd") != "Sun") && (dateFrom.ToString("ddd") != "Sat"))
                                    {
                                        OT.addOffTracking(dateFrom.ToString(), UID, 1);
                                    }
                                    dateFrom = dateFrom.AddDays(1);
                                }
                                lbl_Message.ForeColor = Color.Green;
                                lbl_Message.Text = "Add successfully";
                            }
                            else
                            {
                                lbl_Message.ForeColor = Color.Red;
                                lbl_Message.Text = "Cannot add because of duplication";
                            }
                        }
                        else
                        {
                            lbl_Message.Text = "Cannot add for these days. Only add for future";
                            lbl_Message.ForeColor = Color.Red;
                        }
                    }
                }
            }
            else
            {
                lbl_Message.ForeColor = Color.Red;
                lbl_Message.Text = "Missing Name";
            }
        }

        protected void ddl_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_Message.Text = "";
            if (ddl_Type.SelectedItem.Text == "Half day")
            {
                tr_HalfDay.Visible = true;
                tr_FullDayFrom.Visible = false;
                tr_FullDayTo.Visible = false;
                btn_AddUpcomingPTO.Visible = true;
                txt_From.Text = "";
                txt_To.Text = "";
            }
            else if (ddl_Type.SelectedItem.Text == "Full day")
            {
                tr_HalfDay.Visible = false;
                tr_FullDayFrom.Visible = true;
                tr_FullDayTo.Visible = true;
                btn_AddUpcomingPTO.Visible = true;
                txt_Date.Text = "";
            }
            else btn_AddUpcomingPTO.Visible = false;
        }

        public void countDays()
        {
            if ((txt_To.Text != "") && (txt_From.Text != ""))
            {
                int temp = 0;
                DateTime dateTo = DateTime.Parse(txt_To.Text);
                DateTime dateFrom = DateTime.Parse(txt_From.Text);
                while (dateFrom <= dateTo)
                {
                    if (dateFrom.ToString("ddd") == "Sun" || dateFrom.ToString("ddd") == "Sat") temp++;
                    dateFrom = dateFrom.AddDays(1);
                }

                DateTime from = DateTime.ParseExact(txt_From.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime to = DateTime.ParseExact(txt_To.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                TimeSpan diffResult = to.Subtract(from);

                double count = diffResult.TotalDays + 1 - temp;

                if (count > 0)
                {
                    lbl_Message.ForeColor = Color.Green;
                    btn_Add.Enabled = true;
                }
                else
                {
                    lbl_Message.ForeColor = Color.Red;
                    btn_Add.Enabled = false;
                }

                lbl_Message.Text = "<b>" + count.ToString() + "</b> day(s) selected.";
            }
        }

        protected void txt_Name_TextChanged(object sender, EventArgs e)
        {
            lbl_UID.Text = txt_Name.Text;
            lbl_UID.Text = RA.getEmployeeIDbyName(txt_Name.Text);
        }

        public Boolean checkDateExistbyRange(string UID, DateTime from, DateTime to)
        {
            while (from <= to)
            {
                if (OT.checkOffUserExistByDate(from.ToString(), UID)) return false;
                from = from.AddDays(1);
            }

            return true;
        }
    }
}