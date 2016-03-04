using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LFCRM.Class
{
    public class csByTester
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();

        public ArrayList getProfile(String _name)
        {
            ArrayList list = new ArrayList();
            String sql = "SELECT EmployeeID,FullName,Email,PhoneNumber,Active "+
                    "FROM tbl_User "+
                    "WHERE FullName = '" + _name + "'";

            DataTable tbl = dbconnect.getDataTable(sql);
            if (tbl.Rows.Count != 0)
            {
                for (int i = 0; i < tbl.Columns.Count;i++ )
                    list.Add(tbl.Rows[0][i].ToString());
            }

            return list;
        }

        //Get List Worked Titles
        public DataSet getListTitle(String _id,String _start,String _end)
        {
            String sql = "SELECT RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) AS Date,[3LD] " +
                        "FROM tbl_ResourceAllocation,tbl_Title,tbl_User " +
                        "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                        "AND EmployeeID = '" + _id + "' " +
                        "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID "+
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "GROUP BY RIGHT(CONVERT(VARCHAR(10), Date, 103), 7),[3LD] " +
                        "ORDER BY Date ASC";

            DataSet ds = dbconnect.getDataSet(sql);
            return ds;
        }

        //Get Top 10 User Interaction
        public DataSet getListPeople(String _id, String _fullname,String _start, String _end)
        {
            String sql = "SELECT TOP 10 A.FullName, COUNT(A.Date) AS NUMBER " +
                        "FROM (SELECT FullName,[3LD],Date " +
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title " +
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID " +
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "')) AS A " +
                        "INNER JOIN (SELECT FullName,[3LD],Date " +
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title " +
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID " +
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID " +
                        "AND EmployeeID = '" + _id + "' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "')) AS B " +
                        "ON A.Date = B.Date AND A.[3LD] = B.[3LD] " +
                        "WHERE A.FullName != '" + _fullname + "' " +
                        "GROUP BY A.FullName "+
                        "ORDER BY NUMBER DESC";

            DataSet ds = dbconnect.getDataSet(sql);
            return ds;
        }

        //Get List Title which works with someone
        public DataTable getListTitleWithUser(String _id, String _fullname, String _start, String _end)
        {
            String sql = "SELECT A.[3LD] "+
                        "FROM (SELECT FullName,[3LD],Date "+
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title "+
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID "+
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID "+
                        "AND (Date BETWEEN '"+_start+"' AND '"+_end+"')) AS A "+
                        "INNER JOIN (SELECT FullName,[3LD],Date "+
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title "+
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID "+
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID "+
                        "AND EmployeeID = '"+_id+"' "+
                        "AND (Date BETWEEN '"+_start+"' AND '"+_end+"')) AS B  "+
                        "ON A.Date = B.Date AND A.[3LD] = B.[3LD] "+
                        "WHERE A.FullName = '"+_fullname+"' "+
                        "GROUP BY A.[3LD]";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return tb;
            return null;
        }

        //Get Time BugHunter
        public DataTable getTimeBugHunter(String _id)
        {
            String sql = "SELECT SUBSTRING(CONVERT(VARCHAR(11), Month, 113), 4, 8) AS Month "+
                        "FROM tbl_BugHunter,tbl_User "+
                        "WHERE tbl_BugHunter.UserID = tbl_User.UserID "+
                        "AND tbl_User.EmployeeID = '" + _id + "' " +
                        "ORDER BY Month ASC";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return tb;
            return null;
        }

        public String getMonth(String _date)
        {
            String[] temp;
            temp = _date.Split('/');

            return temp.First();
        }

        public String getYear(String _date)
        {
            String[] temp;
            temp = _date.Split('/');

            return temp.Last();
        }

        //Get Billing Days
        public String getBill(String _id, String _3ld, String _start, String _end, String _month)
        {
            String sql = "SELECT COUNT(ProjectRoleName) as NUMBER " +
                        "FROM tbl_ResourceAllocation,tbl_ProjectRole,tbl_User,tbl_Title " +
                        "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID " +
                        "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND EmployeeID = '" + _id + "' " +
                        "AND ProjectRoleName = 'Billable' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "AND RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) = '" + _month + "'";

            String bill = "";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                bill = tb.Rows[0][0].ToString();
            }

            return bill;
        }

        //Get Cores Days
        public String getCore(String _id, String _3ld, String _start, String _end, String _month)
        {
            String sql = "SELECT COUNT(ProjectRoleName) as NUMBER " +
                        "FROM tbl_ResourceAllocation,tbl_ProjectRole,tbl_User,tbl_Title " +
                        "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID " +
                        "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND EmployeeID = '" + _id + "' " +
                        "AND ProjectRoleName = 'Core' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') "+
                        "AND RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) = '" + _month + "'";

            String core = "";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                core = tb.Rows[0][0].ToString();
            }

            return core;
        }

        //Get Back-Up Days
        public String getBackup(String _id, String _3ld, String _start, String _end, String _month)
        {
            String sql = "SELECT COUNT(ProjectRoleName) as NUMBER " +
                        "FROM tbl_ResourceAllocation,tbl_ProjectRole,tbl_User,tbl_Title " +
                        "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID " +
                        "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND EmployeeID = '" + _id + "' " +
                        "AND ProjectRoleName = 'Backup' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "AND RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) = '" + _month + "'";

            String backup = "";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                backup = tb.Rows[0][0].ToString();
            }

            return backup;
        }

        //Get Total Bug of User based on Date and Month
        public String getTotalBugUser(String _id, String _3ld, String _start, String _end, String _month)
        {
            String sql = "SELECT SUM(NumberOfBugs) as Number " +
                        "FROM tbl_BugTracking,tbl_User,tbl_Title " +
                        "WHERE tbl_User.UserID = tbl_BugTracking.UserID " +
                        "AND tbl_BugTracking.TitleID = tbl_Title.TitleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND EmployeeID = '" + _id + "' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "AND RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) = '" + _month + "' ";

            String total = "0";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                total = tb.Rows[0][0].ToString();
                if (total == null)
                    total = "0";
            }

            return total;
        }

        //Get List cores worked with current user based on Date, Month and Title
        public DataTable getListCoresOnTitle(String _id, String _3ld, String _start, String _end, String _month)
        {
            String sql = "SELECT A.FullName,SUM(A.Number) AS NUMBER " +
                        "FROM (SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,FullName,COUNT(Date) as Number " +
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title,tbl_ProjectRole " +
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID " +
                        "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                        "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND ProjectRoleName = 'Core' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "GROUP BY Date,FullName) AS A " +
                        "INNER JOIN (SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,FullName,COUNT(Date) as Number " +
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title,tbl_ProjectRole " +
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID " +
                        "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                        "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND EmployeeID = '" + _id + "' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "GROUP BY Date,FullName) AS B " +
                        "ON A.Date = B.Date " +
                        "WHERE RIGHT(CONVERT(VARCHAR(10), CONVERT(Datetime, A.Date, 120), 103), 7) = '" + _month + "' " +
                        "GROUP BY A.FullName ";
            DataTable tbl = dbconnect.getDataTable(sql);
            
            if (tbl.Rows.Count != 0)
                return tbl;
            else return null;
        }

        //Get List user worked with current User based on Date, Month and Title
        public DataTable getListCoWorkerOnTitle(String _id, String _3ld, String _start, String _end, String _month)
        {
            String sql = "SELECT A.FullName,SUM(A.Number) AS NUMBER " +
                        "FROM (SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,FullName,COUNT(Date) as Number " +
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title,tbl_ProjectRole " +
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID " +
                        "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                        "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "GROUP BY Date,FullName) AS A " +
                        "INNER JOIN (SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,FullName,COUNT(Date) as Number " +
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title,tbl_ProjectRole " +
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID " +
                        "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                        "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND EmployeeID = '" + _id + "' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "GROUP BY Date,FullName) AS B " +
                        "ON A.Date = B.Date " +
                        "WHERE RIGHT(CONVERT(VARCHAR(10), CONVERT(Datetime, A.Date, 120), 103), 7) = '" + _month + "' " +
                        "GROUP BY A.FullName " +
                        "ORDER BY NUMBER DESC";
            DataTable tbl = dbconnect.getDataTable(sql);

            if (tbl.Rows.Count != 0)
                return tbl;
            else return null;
        }

        //Get Total people worked with current User on Title according Date and Month
        public String getTotalPeopleOnTitle(String _id, String _3ld, String _start, String _end, String _month)
        {
            String sql = "SELECT A.FullName,SUM(A.Number) AS NUMBER " +
                        "FROM (SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,FullName,COUNT(Date) as Number " +
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title,tbl_ProjectRole " +
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID " +
                        "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                        "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "GROUP BY Date,FullName) AS A " +
                        "INNER JOIN (SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,FullName,COUNT(Date) as Number " +
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title,tbl_ProjectRole " +
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID " +
                        "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                        "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND EmployeeID = '" + _id + "' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "GROUP BY Date,FullName) AS B " +
                        "ON A.Date = B.Date " +
                        "WHERE RIGHT(CONVERT(VARCHAR(10), CONVERT(Datetime, A.Date, 120), 103), 7) = '" + _month + "' " +
                        "GROUP BY A.FullName " +
                        "ORDER BY NUMBER DESC";
            DataTable tbl = dbconnect.getDataTable(sql);

            String total = "0";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                total = Convert.ToString(tb.Rows.Count);
                if (total == null)
                    total = "0";
            }

            return total;
        }

        //Get Tottal Bug of team on Title according to Date and Month
        public String getTotalBugTeam(String _employeeid, String _3ld, String _start, String _end, String _month)
        {
            String sql = "SELECT SUM(A.Number)" +
                        "FROM (SELECT FullName,CONVERT(VARCHAR(10), Date, 101) as Date, SUM(NumberOfBugs) as Number " +
                        "FROM tbl_BugTracking,tbl_User,tbl_Title " +
                        "WHERE tbl_User.UserID = tbl_BugTracking.UserID " +
                        "AND tbl_BugTracking.TitleID = tbl_Title.TitleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "GROUP BY CONVERT(VARCHAR(10), Date, 101),Fullname) as A " +
                        "INNER JOIN (SELECT FullName,CONVERT(VARCHAR(10), Date, 101) as Date, SUM(NumberOfBugs) as Number " +
                        "FROM tbl_BugTracking,tbl_User,tbl_Title " +
                        "WHERE tbl_User.UserID = tbl_BugTracking.UserID " +
                        "AND tbl_BugTracking.TitleID = tbl_Title.TitleID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND EmployeeID = '" + _employeeid + "' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "GROUP BY CONVERT(VARCHAR(10), Date, 101),Fullname) As B " +
                        "ON A.Date = B.Date " +
                        "WHERE RIGHT(CONVERT(VARCHAR(10), CONVERT(Datetime, A.Date, 120), 103), 7) = '" + _month + "' ";

            String total = "0";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                total = tb.Rows[0][0].ToString();
                if (total == null)
                    total = "0";
            }

            return total;
        }

        //Get List Date which people worked with current User according to Date
        public DataTable getListDatePeopleWorking(String _id, String _name, String _start, String _end)
        {
            String sql = "SELECT A.[3LD], COUNT(A.Date) as NUMBER "+
                        "FROM (SELECT FullName,[3LD],Date "+
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title "+
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID "+
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID "+
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "')) AS A " +
                        "INNER JOIN (SELECT FullName,[3LD],Date "+
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title "+
                        "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID "+
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID "+
                        "AND EmployeeID = '" + _id + "' " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "')) AS B  " +
                        "ON A.Date = B.Date AND A.[3LD] = B.[3LD] "+
                        "WHERE A.FullName = '" + _name + "' " +
                        "GROUP BY A.[3LD] "+
                        "ORDER BY NUMBER DESC";

            DataTable tbl = dbconnect.getDataTable(sql);

            if (tbl.Rows.Count != 0)
                return tbl;
            else return null;
        }

        //Get Feed Back
        public DataSet getFeedBack(String _id, String _start, String _end)
        {
            String sql = "SELECT UserSendID,CONVERT(VARCHAR(10), Date, 101) AS Date,[3LD],Message,Point " +
                        "FROM tbl_FeedBack,tbl_Title,tbl_User " +
                        "WHERE tbl_User.UserID = tbl_FeedBack.UserReceiveID " +
                        "AND tbl_Title.TitleID = tbl_FeedBack.TitleID " +
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') "+
                        "AND EmployeeID = '" + _id + "'";

            DataSet ds = dbconnect.getDataSet(sql);
            if (ds.Tables.Count != 0)
                return ds;
            else
                return null;
        }
        //Get Name user sent feedback
        public String getUserSentFeedBack(String _userid)
        {
            String sql = "SELECT FullName FROM tbl_User WHERE UserID = '" + _userid + "'";
            String username = "Anonymous";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                username = tb.Rows[0][0].ToString();
                if (username == null)
                    username = "Anonymous";
            }

            return username;
        }

        //Get Point for Rating
        public DataTable getRating(String _id, String _start, String _end)
        {
            String sql = "SELECT Point " +
                        "FROM tbl_FeedBack,tbl_User "+
                        "WHERE tbl_User.UserID = tbl_FeedBack.UserReceiveID "+
                        "AND (Date BETWEEN '" + _start + "' AND '" + _end + "') " +
                        "AND EmployeeID = '" + _id + "'";

            DataTable tbl = dbconnect.getDataTable(sql);

            if (tbl.Rows.Count != 0)
                return tbl;
            else return null;
        }
    }
}