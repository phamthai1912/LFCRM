using AjaxControlToolkit;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;

namespace LFCRM.Class
{
    public class csPerformanceTracking : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            context.LogRequest += new EventHandler(OnLogRequest);
        }

        #endregion

        Class.csDBConnect dbconnect = new Class.csDBConnect();

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public DataTable getUserListbyMonth(int month, int year)
        {
            DataTable dt = dbconnect.getDataTable("Select distinct(a.UserID), b.FullName"
                                                    +" from tbl_ResourceAllocation a, tbl_User b"
                                                    + " where MONTH(date) = '" + month + "'"
                                                    + " AND YEAR(date) = '" + year + "'"
                                                    +" AND ProjectRoleID <> 4"
                                                    +" AND a.UserID = b.UserID");
            return dt;
        }

        public DataTable getTitleListbyMonth(int month, int year)
        {
            DataTable dt = dbconnect.getDataTable("Select distinct([3LD]), Colorcode From tbl_resourceallocation, tbl_title where tbl_resourceallocation.titleID = tbl_title.titleID AND MONTH(date) = " + month + " AND YEAR(date) = " + year);
            return dt;
        }

        public string getFullName(string UID)
        {
            string fullname = "";
            string sql = "SELECT FullName FROM tbl_User WHERE UserID = '" + UID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) fullname = tb.Rows[0][0].ToString();
            return fullname;
        }

        public string getColorCode(string UID, string date)
        {
            string colorCode = "";
            string sql = "Select ColorCode "
                        +"FROM tbl_ResourceAllocation, tbl_Title "
                        +"Where tbl_ResourceAllocation.TitleID = tbl_Title.TitleID "
                        + "AND tbl_ResourceAllocation.date = '" + date + "' "
                        + "AND tbl_ResourceAllocation.UserID = '" + UID + "' ";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) colorCode = tb.Rows[0][0].ToString();
            return colorCode;
        }

        public DataTable getNoBug_ColorCode(string UID, string date)
        {
            string sql = "Select Count(BugTitleID) AS NumberOfBugs, ColorCode "+
                                    "From tbl_BugTitle, tbl_Title "+
                                    "Where tbl_BugTitle.TitleID = tbl_Title.TitleID "+
                                    "AND tbl_BugTitle.DateEnter = '"+date+"' "+
                                    "AND tbl_BugTitle.UserEnterID = "+UID+
                                    " AND tbl_BugTitle.BugStatusID = 1 " +
                                    "GROUP BY ColorCode";
            
            DataTable dt = dbconnect.getDataTable(sql);
            return dt;
        }

        public DataTable get_Mul_NoBug_ColorCode(string UID, string date, string titleID)
        {
            DataTable dt = dbconnect.getDataTable("Select COUNT(BugTitleID) AS NumberOfBugs, ColorCode "+ 
                                                    "From tbl_BugTitle, tbl_Title "+
                                                    "Where tbl_BugTitle.TitleID = tbl_Title.TitleID "+
                                                    "AND tbl_BugTitle.DateEnter = '" + date + "' " +
                                                    "AND tbl_BugTitle.UserEnterID = '" + UID + "' " +
                                                    "AND tbl_BugTitle.TitleID = '" + titleID + "' " +
                                                    "AND tbl_BugTitle.BugStatusID = 1 "+
                                                    "GROUP BY ColorCode ");
            return dt;
        }

        public DataTable getRole_Hour(string UID, string date)
        {
            DataTable dt = dbconnect.getDataTable("Select ProjectRoleName, Value, TitleID"
                                                   +" From tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours, tbl_Title"
                                                   +" Where tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID" 
                                                   +" AND tbl_WorkingHours.WorkingHoursID = tbl_ResourceAllocation.WorkingHoursID"
                                                   + " AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID" 
                                                   +" AND UserID='"+UID+"'"
                                                   +" AND Date='"+date+"'");
            return dt;
        }

        public DataTable getDataForProfileByDate(string UID, string date)
        {
            DataTable dt = dbconnect.getDataTable("Select * "
                                                + " FROM ("
                                                    + " Select UserID, table1.TitleID, ColorCode, sum(Value) as Hour, ProjectRoleName, [3LD]"
                                                    + " FROM ("
                                                        + " Select UserID, a.TitleID, value, ProjectRoleName"
                                                        + " From tbl_ResourceAllocation a, tbl_WorkingHours b, tbl_ProjectRole c"
                                                        + " WHERE a.WorkingHoursID = b.WorkingHoursID"
                                                        + " AND a.ProjectRoleID = c.ProjectRoleID"
                                                        + " AND date = '" + date + "'"
                                                    + " ) as table1"
                                                    + " LEFT OUTER JOIN ("
                                                        + " Select TitleID, ColorCode, [3LD]"
                                                        + " From tbl_Title"
                                                    + " ) as table2"
                                                    + " ON table1.TitleID = table2.TitleID"
                                                    + " group by UserID, table1.TitleID, ColorCode,ProjectRoleName, [3LD]"
                                                + " ) as table1"
                                                + " LEFT OUTER JOIN ("
                                                    + " Select count(BugTitleID) as NumOfBugs, TItleID, UserEnterID"
                                                    + " From tbl_Bugtitle"
                                                    + " Where dateenter = '" + date + "'"
                                                    + " and bugstatusID=1"
                                                    + " group by UserEnterID, TItleID"
                                                + " ) as table2"
                                                + " ON (table1.UserID = table2.UserEnterID"
                                                + " AND table1.TitleID = table2.TitleID)"
                                                + " Where UserID='" + UID + "'");

            return dt;
        }

        public DataTable getSpecificTitleForProfileByDate(string UID, string date, string _LD)
        {
            DataTable dt = dbconnect.getDataTable("Select UserID, table1.TitleID, ColorCode, Hour, ProjectRoleName, [3LD], NumOfBugs "
                                                + " FROM ("
                                                    + " Select UserID, table1.TitleID, ColorCode, sum(Value) as Hour, ProjectRoleName, [3LD]"
                                                    + " FROM ("
                                                        + " Select UserID, a.TitleID, value, ProjectRoleName"
                                                        + " From tbl_ResourceAllocation a, tbl_WorkingHours b, tbl_ProjectRole c"
                                                        + " WHERE a.WorkingHoursID = b.WorkingHoursID"
                                                        + " AND a.ProjectRoleID = c.ProjectRoleID"
                                                        + " AND date = '" + date + "'"
                                                    + " ) as table1"
                                                    + " LEFT OUTER JOIN ("
                                                        + " Select TitleID, ColorCode, [3LD]"
                                                        + " From tbl_Title"
                                                    + " ) as table2"
                                                    + " ON table1.TitleID = table2.TitleID"
                                                    + " group by UserID, table1.TitleID, ColorCode,ProjectRoleName, [3LD]"
                                                + " ) as table1"
                                                + " LEFT OUTER JOIN ("
                                                    + " Select count(BugTitleID) as NumOfBugs, TItleID, UserEnterID"
                                                    + " From tbl_Bugtitle"
                                                    + " Where dateenter = '" + date + "'"
                                                    + " and bugstatusID=1"
                                                    + " group by UserEnterID, TItleID"
                                                + " ) as table2"
                                                + " ON (table1.UserID = table2.UserEnterID"
                                                + " AND table1.TitleID = table2.TitleID)"
                                                + " Where UserID='" + UID + "'"
                                                + " and [3LD] = '" + _LD + "'");

            return dt;
        }

        public DataTable getUserDataByMonth(string UID, string month, string year)
        {
            DataTable dt = dbconnect.getDataTable("Select UserID, table1.TitleID, date, ColorCode, Hour, ProjectRoleName, [3LD], NumOfBugs"
                                                +" FROM ("
	                                                +" Select UserID, table1.TitleID, date, ColorCode, sum(Value) as Hour, ProjectRoleName, [3LD]"
	                                                +" FROM ("
		                                                +" Select UserID, a.TitleID, value, ProjectRoleName, date"
		                                                +" From tbl_ResourceAllocation a, tbl_WorkingHours b, tbl_ProjectRole c"
		                                                +" WHERE a.WorkingHoursID = b.WorkingHoursID"
		                                                +" AND a.ProjectRoleID = c.ProjectRoleID"
		                                                +" AND MONTH(date) = '"+month+"'"
		                                                +" AND YEAR(date) = '"+year+"'"
	                                                +" ) as table1"
	                                                +" LEFT OUTER JOIN ("
		                                                +" Select TitleID, ColorCode, [3LD]"
		                                                +" From tbl_Title"
	                                                +" ) as table2"
	                                                +" ON table1.TitleID = table2.TitleID"
	                                                +" group by UserID, table1.TitleID, date, ColorCode, ProjectRoleName, [3LD]"
                                                +" ) as table1"
                                                +" LEFT OUTER JOIN ("
	                                                +" Select count(BugTitleID) as NumOfBugs, UserEnterID, TItleID, DateEnter"
	                                                +" From tbl_Bugtitle"
	                                                +" where MONTH(dateenter) = '"+month+"'"
                                                    + " AND YEAR(dateenter) = '" + year + "'"
	                                                +" and bugstatusID=1"
	                                                +" group by UserEnterID, TItleID, DateEnter"
                                                +" ) as table2"
                                                +" ON (table1.UserID = table2.UserEnterID"
                                                +" AND table1.TitleID = table2.TitleID"
                                                +" AND table1.date = table2.DateEnter)"
                                                + " Where table1.UserID = '"+UID+"'"
                                                +" Order By date");

            return dt;
        }

        public DataTable getAllTitle(string month, string year)
        {
            DataTable dt = dbconnect.getDataTable("Select distinct tbl_title.TitleID, [3LD]"
                                                +" From tbl_resourceallocation, tbl_title "
                                                +" where tbl_resourceallocation.titleID = tbl_title.titleID "
                                                +" AND MONTH(date) = '"+month+"'"
                                                +" AND YEAR(date) = '"+year+"'");

            return dt;
        } 

        public DataTable getTopTen(string month, string year)
        {
            DataTable dt = dbconnect.getDataTable("Select * "
                                                +" From ("
                                                    + " Select TOP 10 PERCENT UserEnterID, EmployeeID, FullName, count(BugTitleID) as Count"
	                                                +" From tbl_BugTitle a, tbl_User b"
	                                                +" Where MONTH(dateenter) = '"+month+"'"
	                                                +" AND YEAR(dateenter) = '"+year+"'"
	                                                +" And b.UserID = a.UserEnterID"
	                                                +" And BugStatusID = '1'"
	                                                +" group by UserEnterID, FullName, EmployeeID"
	                                                +" order by Count desc"
                                                +" ) as table1"
                                                +" LEFT OUTER JOIN ("
	                                                +" Select UserID, count(date) as countday"
	                                                +" From tbl_ResourceAllocation"
	                                                +" Where MONTH(date) = '"+month+"'"
	                                                +" AND YEAR(date) = '"+year+"'"
	                                                +" group by UserID"
                                                +" ) "
                                                +" as table2"
                                                +" ON table1.UserEnterID = table2.UserID");

            return dt;
        }

        public DataTable getTotalBugs(string month, string year)
        {
            DataTable dt = dbconnect.getDataTable("Select count(BugTitleID) as Count"
	                                            +" From tbl_BugTitle"
	                                            +" Where MONTH(dateenter) = '"+month+"'"
                                                + " AND YEAR(dateenter) = '"+year+"'"
	                                            +" And BugStatusID = '1'");

            return dt;
        }

        public DataTable getTopPerTitle(string month, string year, string _LD)
        {
            DataTable dt = dbconnect.getDataTable(" Select * "
                                                + " From ("
	                                                + " Select Top 3 UserEnterID, EmployeeID, FullName, count(BugTitleID) as Count"
	                                                + " From tbl_BugTitle a, tbl_User b, tbl_Title c"
	                                                + " Where MONTH(dateenter) = '"+month+"'"
	                                                + " AND YEAR(dateenter) = '"+year+"'"
	                                                + " And b.UserID = a.UserEnterID"
	                                                + " And c.TitleID = a.TitleID"
	                                                + " And [3LD] = '"+_LD+"'"
	                                                + " And BugStatusID = '1'"
	                                                + " group by UserEnterID, FullName, EmployeeID"
	                                                + " order by Count desc"
                                                + " ) as table1"
                                                + " LEFT OUTER JOIN ("
	                                                + " Select UserID, count(date) as countday"
	                                                + " From tbl_ResourceAllocation a, tbl_Title b"
	                                                + " Where MONTH(date) = '"+month+"'"
	                                                + " AND YEAR(date) = '"+year+"'"
	                                                + " And a.titleID = b.TitleID"
                                                    + " and [3LD] = '" + _LD + "'"
	                                                + " group by UserID"
                                                + " ) "
                                                + " as table2"
                                                + " ON table1.UserEnterID = table2.UserID");

            return dt;
        }

        public DataTable getTotalBugsPerTitle(string month, string year, string _LD)
        {
            DataTable dt = dbconnect.getDataTable("Select count(BugTitleID) as Totalbugs, [3LD]"
	                                            +" From tbl_BugTitle a, tbl_Title b"
	                                            +" Where MONTH(dateenter) = '"+month+"'"
	                                            +" AND YEAR(dateenter) = '"+year+"'"
	                                            +" ANd a.titleID = b.TitleID"
                                                +" And BugStatusID = '1'"
                                                +" and [3LD] = '" + _LD + "'"
                                                +" group by [3LD]" );

            return dt;
        }
    }
}
