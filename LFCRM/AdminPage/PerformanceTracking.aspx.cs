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

        public void printAllPTbyMonth(int month, int year)
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
                int no_day = 0;
                string UID = list_user.Rows[i].ItemArray[0].ToString();
                string fullName = list_user.Rows[i].ItemArray[1].ToString();
                content_PT = content_PT + "<tr><td>" + (i + 1).ToString() + "</td><td style='text-align:left;'>" + fullName + "</td>";

                DataTable dataPerProfileByMonth = PT.getUserDataByMonth(UID, month.ToString(), year.ToString());
                
                for (int j = 1; j <= list_date.Count; j++)
                {
                    string back_color = "";
                    string text_color = "";
                    string no_bug = "";
                    string bg_image = "";
                    string tooltip = "";
                    string str_tooltip = "";
                    string date = month.ToString() + "/" + j.ToString() + "/" + year.ToString();

                    //------------------------------------ Grey Sun & Sat ----------------------------------------
                    DateTime dt_date = Convert.ToDateTime(date);
                    if (dt_date.ToString("ddd") == "Sun" || dt_date.ToString("ddd") == "Sat") back_color = "grey";

                    for (int k=0; k < dataPerProfileByMonth.Rows.Count; k++)
                    {
                        int i_userID = 0;
                        int i_titleID = 1;
                        int i_date = 2;
                        int i_colorcode = 3;
                        int i_hour = 4;
                        int i_projectrolename = 5;
                        int i_3LD = 6;
                        int i_numberOfBugs = 7;
                        int count = 1;

                        string role = dataPerProfileByMonth.Rows[k].ItemArray[i_projectrolename].ToString();
                        string hour = dataPerProfileByMonth.Rows[k].ItemArray[i_hour].ToString();
                        string colorcode = dataPerProfileByMonth.Rows[k].ItemArray[i_colorcode].ToString();
                        string numberOfBugs = dataPerProfileByMonth.Rows[k].ItemArray[i_numberOfBugs].ToString();
                        string _LD = dataPerProfileByMonth.Rows[k].ItemArray[i_3LD].ToString();
                        string db_date1 = dataPerProfileByMonth.Rows[k].ItemArray[i_date].ToString();

                        DateTime date1 = DateTime.Parse(date);
                        DateTime date2 = DateTime.Parse(db_date1);

                        for (int l = k + 1; l < dataPerProfileByMonth.Rows.Count; l++)
                        {
                            string db_dateTemp = dataPerProfileByMonth.Rows[l].ItemArray[i_date].ToString();
                            DateTime dateTemp = DateTime.Parse(db_dateTemp);
                            if (date1 == dateTemp) count++;
                        }

                        if (date1 == date2)
                        {
                            //1 title per day
                            if (count == 1)
                            {
                                if (numberOfBugs == "") numberOfBugs = "0";
                                no_bug = numberOfBugs;
                                back_color = colorcode;
                                str_tooltip = _LD;
                            }
                            //multiple titles per day
                            else
                            {
                                no_bug = "ee";
                            }
                            no_day++;
                        }
                    }
                    content_PT = content_PT + "<td style='background-color: " + back_color + "; color: " + text_color + "; " + bg_image + "' " + tooltip + ">" + no_bug + "</td>";
                }
                content_PT = content_PT + "<td><b>" + sum.ToString() + "</b></td><td><b>"+no_day.ToString()+"</b></td>";
            }
            content_PT = content_PT + "</tr> ";

            //-----------------------------Print--------------------------------------
            lbl_thead.Text = header_PT;
            lbl_PT.Text = content_PT;
        }

        public void printSpecificTitlebyMonth(int month, int year, string _LD)
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
                string fullName = list_user.Rows[i].ItemArray[1].ToString();
                content_PT = content_PT + "<tr><td>" + (i + 1).ToString() + "</td><td style='text-align:left;'>" + fullName + "</td>";
                for (int j = 1; j <= list_date.Count; j++)
                {
                    string back_color = "";
                    string text_color = "";
                    string no_bug = "";
                    string bg_image = "";
                    string tooltip = "";
                    string str_tooltip = "";
                    string date = month.ToString() + "/" + j.ToString() + "/" + year.ToString();

                    //------------------------------------ Grey Sun & Sat ----------------------------------------
                    DateTime dt_date = Convert.ToDateTime(date);
                    if (dt_date.ToString("ddd") == "Sun" || dt_date.ToString("ddd") == "Sat") back_color = "grey";

                    DataTable allData = PT.getSpecificTitleForProfileByDate(UID, date, _LD);
                    int i_userID = 0;
                    int i_titleID = 1;
                    int i_colorcode = 2;
                    int i_hour = 3;
                    int i_projectrolename = 4;
                    int i_3LD = 5;
                    int i_numberOfBugs = 6;
                    int i_userEnterID = 8;
                    //================================== 1 Title ===============================================
                    if (allData.Rows.Count == 1)
                    {
                        string role = allData.Rows[0].ItemArray[i_projectrolename].ToString();
                        string hour = allData.Rows[0].ItemArray[i_hour].ToString();
                        string colorcode = allData.Rows[0].ItemArray[i_colorcode].ToString();
                        string numberOfBugs = allData.Rows[0].ItemArray[i_numberOfBugs].ToString();
                        //string _LD = allData.Rows[0].ItemArray[i_3LD].ToString();

                        if ((role == "Trainee") || (role == "Trainer"))
                        {
                            no_bug = "T";
                            back_color = "yellow";
                            text_color = "red";
                        }
                        else
                        {
                            back_color = colorcode;

                            if (Convert.ToInt32(hour) != 8) str_tooltip = _LD + ": " + hour + "h working \r\n";
                            else str_tooltip = _LD;

                            if (numberOfBugs == "") no_bug = "0";
                            else
                            {
                                no_bug = numberOfBugs;
                                sum += Convert.ToInt32(no_bug);
                            }
                        }
                        no_day++;
                    }
                    tooltip = "data-toggle='tooltip' title='" + str_tooltip + "'";
                    content_PT = content_PT + "<td style='background-color: " + back_color + "; color: " + text_color + "; " + bg_image + "' " + tooltip + ">" + no_bug + "</td>";
                }
                content_PT = content_PT + "<td><b>" + sum.ToString() + "</b></td><td><b>" + no_day.ToString() + "</b></td>";
            }
            content_PT = content_PT + "</tr>";

            //-----------------------------Print--------------------------------------
            lbl_thead.Text = header_PT;
            lbl_PT.Text = content_PT;
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

            lbl_header.Text = "Performance Tracking on " + (Convert.ToDateTime(txt_date.Text)).ToString("MMM, yyyy");
            tb_Reference.Visible = true;

            ddl_TitleList.Items.Clear();
            ListItem l = new ListItem();
            l.Value = "";
            l.Text = "- All titles -";
            ddl_TitleList.Items.Add(l);
            CC.AddDBToDDL(ddl_TitleList, "Select distinct tbl_title.TitleID, [3LD] From tbl_resourceallocation, tbl_title where tbl_resourceallocation.titleID = tbl_title.titleID AND MONTH(date) = " + Convert.ToInt32(s[0]) + " AND YEAR(date) = " + Convert.ToInt32(s[1]));

            printAllPTbyMonth(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]));
            printTitlebyMonth(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]));
        }

        protected void ddl_TitleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] s = txt_date.Text.Split('/');

            if (ddl_TitleList.SelectedItem.Text == "- All titles -") printAllPTbyMonth(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]));
            else printSpecificTitlebyMonth(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), ddl_TitleList.SelectedItem.Text);
        }

        protected void btn_ViewTop_Click(object sender, EventArgs e)
        {
            this.lbl_TopTen.Text = this.getTopTen();
            if (this.txt_date.Text != "")
            {
                string[] strArray = this.txt_date.Text.Split(new char[] { '/' });
                DataTable table = this.PT.getAllTitle(Convert.ToInt32(strArray[0]).ToString(), Convert.ToInt32(strArray[1]).ToString());
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string str = table.Rows[i].ItemArray[1].ToString();
                    this.lbl_TopPerTitle.Text = this.lbl_TopPerTitle.Text + this.getTopPerTitle(str);
                }
            }
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("<script type='text/javascript'>");
            builder.Append("$('#viewTopPopup').modal('show');");
            builder.Append("</script>");
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ViewTopModalScript", builder.ToString(), false);
        }

        public string getTopPerTitle(string _LD)
        {
            string str = "<table style='width: 570px' class='table table-striped table-bordered table-responsive table-condensed table-hover'><tr><td colspan='6'><b>" + _LD + "</b></td></tr><tr><td>ID</td><td>Name</td><td>Bugs</td><td>Days</td><td>Average</td><td>Total Bugs</td></tr>";
            string str2 = "";
            string str3 = "</table>";
            string str4 = "";
            if (this.txt_date.Text != "")
            {
                string[] strArray = this.txt_date.Text.Split(new char[] { '/' });
                DataTable table = this.PT.getTopPerTitle(Convert.ToInt32(strArray[0]).ToString(), Convert.ToInt32(strArray[1]).ToString(), _LD);
                DataTable table2 = this.PT.getTotalBugsPerTitle(Convert.ToInt32(strArray[0]).ToString(), Convert.ToInt32(strArray[1]).ToString(), _LD);
                if (table2.Rows.Count > 0)
                {
                    str4 = table2.Rows[0].ItemArray[0].ToString();
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string str5 = table.Rows[i].ItemArray[1].ToString();
                    string str6 = table.Rows[i].ItemArray[2].ToString();
                    string str7 = table.Rows[i].ItemArray[3].ToString();
                    string str8 = table.Rows[i].ItemArray[5].ToString();
                    string str9 = "";
                    double num2 = 0.0;
                    if (str7 == "") str7 = "0";
                    if (str8 == "") str8 = "0";
                    if (str8 != "0") num2 = Convert.ToDouble(str7) / Convert.ToDouble(str8);
                    if (num2 == 0.0) str9 = "N/A";
                    else str9 = num2.ToString("#.##");
                    str2 = str2 + "<tr><td>" + str5 + "</td><td>" + str6 + "</td><td>" + str7 + "</td><td>" + str8 + "</td><td>" + str9 + "</td><td>" + str4 + "</td></tr>";
                }
            }
            return (str + str2 + str3);
        }

        public string getTopTen()
        {
            string str = "";
            string str2 = "";
            if (this.txt_date.Text != "")
            {
                string[] strArray = this.txt_date.Text.Split(new char[] { '/' });
                DataTable table = this.PT.getTopTen(Convert.ToInt32(strArray[0]).ToString(), Convert.ToInt32(strArray[1]).ToString());
                DataTable table2 = this.PT.getTotalBugs(Convert.ToInt32(strArray[0]).ToString(), Convert.ToInt32(strArray[1]).ToString());
                if (table2.Rows.Count > 0)
                {
                    str2 = table2.Rows[0].ItemArray[0].ToString();
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string str3 = table.Rows[i].ItemArray[1].ToString();
                    string str4 = table.Rows[i].ItemArray[2].ToString();
                    string str5 = table.Rows[i].ItemArray[3].ToString();
                    string str6 = table.Rows[i].ItemArray[5].ToString();
                    double num2 = Convert.ToDouble(str5) / Convert.ToDouble(str6);
                    str = str + "<tr><td>" + str3 + "</td><td>" + str4 + "</td><td>" + str5 + "</td><td>" + str6 + "</td><td>" + num2.ToString("#.##") + "</td></tr>";
                }
            }
            return str;
        }
    }
}