using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace LFCRM.Class
{
    public class csBugStatistic
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();
        public DataSet getResourceAlocation(String date) 
        {
            String sql;
            if (date == "")
                sql = "SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,EmployeeID,FullName,tbl_ResourceAllocation.TitleID,[3LD],ProjectRoleName,Value " +
                           "FROM tbl_User, tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours,tbl_Title " +
                           "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                           "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID "+
                           "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                           "AND tbl_ResourceAllocation.WorkingHoursID = tbl_WorkingHours.WorkingHoursID " +
                           "AND RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) = RIGHT(CONVERT(VARCHAR(10), GETDATE(), 103), 7) " +
                           "ORDER BY Date DESC";
            else
            {
                DateTime datetime = Convert.ToDateTime(date);
                date = datetime.ToString("MM/yyyy");
                sql = "SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,EmployeeID,FullName,tbl_ResourceAllocation.TitleID,[3LD],ProjectRoleName,Value " +
                "FROM tbl_User, tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours,tbl_Title " +
                "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                "AND tbl_ResourceAllocation.WorkingHoursID = tbl_WorkingHours.WorkingHoursID " +
                "AND RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) = '" + date + "'"+
                "ORDER BY Date DESC";
            }
            
            DataSet dt = dbconnect.getDataSet(sql);

            return dt;
        }

        public String getEmployeeID(String name)
        {
            String sql = "SELECT EmployeeID FROM tbl_User WHERE FullName = '" + name + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            String id = tb.Rows[0][0].ToString();

            return id;
        }

        public String getNoBugs(String _date, String _employeeid, String _titleid)
        {
            String _id = getUserID(_employeeid);
            String sql = "SELECT NumberOfBugs FROM tbl_BugTracking WHERE Date = '" + _date + "' AND UserID = '" + _id + "' AND TitleID = '" + _titleid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            String number;

            if (tb.Rows.Count != 0)
                number = tb.Rows[0][0].ToString()+" bug(s)";
            else number = "N/A"; 

            return number;
        }

        public String getBugID(String _date, String _userid, String _titleID)
        {
            String sql = "SELECT BugID FROM tbl_BugTracking WHERE Date = '" + _date + "' AND UserID = '" + _userid + "' AND TitleID = '" + _titleID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return tb.Rows[0][0].ToString();
            return "";
        }

        public String getUserID(String _employeeID)
        {
            String sql = "SELECT UserID FROM tbl_User WHERE EmployeeID = '" + _employeeID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return tb.Rows[0][0].ToString();
            return "";
        }

        public void updateBugs(String _date, String _employeeid, String _titleid,String _bugs)
        {
            String bugid = getBugID(_date, getUserID(_employeeid), _titleid);
            String sql = "UPDATE tbl_BugTracking SET NumberOfBugs = '" + _bugs + "' " +
                "WHERE BugID = '" + bugid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void addBugs(String _date, String _employeeid, String _titleid, String number)
        {
            String userid = getUserID(_employeeid);

            String sql = "INSERT INTO tbl_BugTracking (Date,UserID,TitleID,NumberOfBugs) "+
                    "VALUES ('" + _date + "','" + userid + "','" + _titleid + "','" + number + "')";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void deleteBugs(String _date, String _employeeid, String _titleid)
        {
            String userid = getUserID(_employeeid);

            String sql = "DELETE FROM tbl_BugTracking WHERE Date = '" + _date + "' AND UserID = '" + userid + "' AND TitleID = '" + _titleid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public String getNumberBugs(String text)
        {
            String[] temp;
            temp = text.Split(' ');

            return temp[0];

        }

        public DataSet searchName(String _search, String date)
        {
            String sql;

            String[] temp;
            temp = _search.Split(' ');
            String newsearch = "";
            for (int i = 0; i < temp.Length; i++)
            {
                newsearch = newsearch + "%" + temp[i];
            }
            if (date == "")
                sql = "SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,EmployeeID,FullName,tbl_ResourceAllocation.TitleID,[3LD],ProjectRoleName,Value " +
                           "FROM tbl_User, tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours,tbl_Title " +
                           "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                           "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID "+
                           "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                           "AND tbl_ResourceAllocation.WorkingHoursID = tbl_WorkingHours.WorkingHoursID " +
                           "AND FullName LIKE '" + newsearch + "%'" +
                           "ORDER BY Date DESC";
            else
            {
                DateTime datetime = Convert.ToDateTime(date);
                date = datetime.ToString("MM/yyyy");

                sql = "SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,EmployeeID,FullName,tbl_ResourceAllocation.TitleID,[3LD],ProjectRoleName,Value " +
                "FROM tbl_User, tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours,tbl_Title " +
                "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                "AND tbl_ResourceAllocation.WorkingHoursID = tbl_WorkingHours.WorkingHoursID " +
                "AND RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) = '" + date + "' " +
                "AND FullName LIKE '" + newsearch + "%'"+
                "ORDER BY Date DESC";
            }
            DataSet dt = dbconnect.getDataSet(sql);

            return dt;
        }

        public DataSet searchTitle(String _search, String _date, String _title)
        {
            DataSet ds = new DataSet();
            if (_date == "")
                _date = DateTime.Now.ToString("MM/yyyy");

            String[] temp;
            temp = _search.Split(' ');
            String newsearch = "";
                        for (int i = 0; i < temp.Length; i++)
            {
                newsearch = newsearch + "%" + temp[i];
            }
            String sql = "SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,EmployeeID,FullName,tbl_ResourceAllocation.TitleID,[3LD],ProjectRoleName,Value " +
                "FROM tbl_User, tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours,tbl_Title " +
                "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                "AND tbl_ResourceAllocation.WorkingHoursID = tbl_WorkingHours.WorkingHoursID " +
                "AND RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) = '" + _date + "' " +
                "AND FullName LIKE '" + newsearch + "%' " +
                "AND [3LD] = '"+_title+"' "+
                "ORDER BY Date DESC";

            if (_title == "")
            {
                ds = searchName(_search, _date);
            }
            else
            {
                if(_date == "")
                    sql = "SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,EmployeeID,FullName,tbl_ResourceAllocation.TitleID,[3LD],ProjectRoleName,Value " +
                           "FROM tbl_User, tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours,tbl_Title " +
                           "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                           "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                           "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                           "AND tbl_ResourceAllocation.WorkingHoursID = tbl_WorkingHours.WorkingHoursID " +
                           "AND FullName LIKE '" + newsearch + "%'" +
                           "AND [3LD] = '" + _title + "' " +
                           "ORDER BY Date DESC";

                ds = dbconnect.getDataSet(sql);
            }

            return ds;
        }

        public DataSet getListDateOfTitle(String _titleid,String _date)
        {
            String sql = "SELECT CONVERT(VARCHAR(10), Date, 101) AS Date,[3LD] "+
                        "FROM tbl_ResourceAllocation,tbl_Title "+
                        "WHERE tbl_ResourceAllocation.TitleID = '" + _titleid + "' " +
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID "+
                        "AND RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) = '" + _date + "' " +
                        "GROUP BY Date,[3LD] "+
                        "ORDER BY Date DESC";

            DataSet ds = dbconnect.getDataSet(sql);
            if (ds != null)
                return ds;
            else return null;
        }

        public String getTotalBugsTitle(String _titleid, String _date)
        {
            String sql = "SELECT TotalBugs "+
                        "FROM tbl_TitleBugTracking,tbl_Title "+
                        "WHERE Date = '" + _date + "' " +
                        "AND tbl_TitleBugTracking.TitleID = tbl_Title.TitleID "+
                        "AND tbl_Title.TitleID = '" + _titleid + "'";
            DataTable dt = dbconnect.getDataTable(sql);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else return "";
        }

        public String getCurrentBugTitle(String _titleid, String _date)
        {
            String sql = "SELECT SUM(NumberOfBugs) AS NUMBER "+
                        "FROM tbl_BugTracking "+
                        "WHERE Date = '" + _date + "' " +
                        "AND TitleID = '" + _titleid + "'";

            DataTable dt = dbconnect.getDataTable(sql);
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0][0].ToString() != "")
                    return dt.Rows[0][0].ToString();
                else return "0";
            }
            else return "0";
        }

        public Boolean checkTotalBugsTitle(String _titleid, String _date)
        {
            String sql = "SELECT * FROM tbl_TitleBugTracking WHERE Date = '" + _date + "' AND TitleID = '" + _titleid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return true;
            else return false;
        }

        public void addTotalBugsTitle(String _titleid, String _date, String _totalbugs)
        {
            String sql = "INSERT INTO tbl_TitleBugTracking VALUES('" + _date + "','" + _titleid + "','" + _totalbugs + "')";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void updateTotalBugsTitle(String _titleid, String _date, String _totalbugs)
        {
            String sql = "UPDATE tbl_TitleBugTracking SET TotalBugs = '" + _totalbugs + "' WHERE TitleID = '" + _titleid + "' AND Date = '" + _date + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public Boolean checkBugsOfCoreTitle(String _titleid, String _date)
        {
            String sql = "SELECT NumberOfBugs "+
                        "FROM tbl_ResourceAllocation,tbl_ProjectRole,tbl_BugTracking "+
                        "WHERE tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID "+
                        "AND ProjectRoleName = 'Core' "+
                        "AND tbl_ResourceAllocation.TitleID = '" + _titleid + "' " +
                        "AND tbl_ResourceAllocation.Date = '" + _date + "' " +
                        "AND tbl_ResourceAllocation.Date = tbl_BugTracking.Date "+
                        "AND tbl_ResourceAllocation.UserID = tbl_BugTracking.UserID "+
                        "AND tbl_ResourceAllocation.TitleID = tbl_BugTracking.TitleID";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return true;
            else return false;
        }

        public String getCoreID(String _titleid, String _date)
        {
            String sql = "SELECT EmployeeID "+
                        "FROM tbl_ResourceAllocation,tbl_ProjectRole,tbl_User " +
                        "WHERE tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID "+
                        "AND ProjectRoleName = 'Core' "+
                        "AND tbl_ResourceAllocation.TitleID = '" + _titleid + "' " +
                        "AND tbl_ResourceAllocation.Date = '" + _date + "' " +
                        "AND tbl_ResourceAllocation.UserID = tbl_User.UserID";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return tb.Rows[0][0].ToString();
            else return "";
        }

        public void updateBugsOfCoreTitle(String _titleid, String _date,String total,String _currentbugs)
        {
            String coreid = getCoreID(_titleid,_date);
            int totalbugs = int.Parse(total);
            String _corebugs = getNoBugs(_date, coreid, _titleid);
            if (_corebugs == "N/A")
                _corebugs = "0";
            else _corebugs = getNumberBugs(_corebugs);
            int getcorebugs = int.Parse(_corebugs);
            int currentbugs = int.Parse(_currentbugs);

            if (totalbugs >= currentbugs)
            {
                int memberbugs = int.Parse(getMemberBugsTitle(_titleid, _date));
                _corebugs = (int)(totalbugs - memberbugs) + "";
                if (coreid != "")
                {
                    if (checkBugsOfCoreTitle(_titleid, _date) == true)
                        updateBugs(_date, coreid, _titleid, _corebugs);
                    if (checkBugsOfCoreTitle(_titleid, _date) == false)
                        addBugs(_date, coreid, _titleid, _corebugs);
                }
            }
        }

        public String getMemberBugsTitle(String _titleid, String _date) 
        {
            String sql = "SELECT SUM(NumberOfBugs) "+
                        "FROM (SELECT UserID,TitleID "+
                        "FROM tbl_ResourceAllocation,tbl_ProjectRole "+
                        "WHERE TitleID = '" + _titleid + "' " +
                        "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID "+
                        "AND ProjectRoleName != 'Core' "+
                        "AND Date = '" + _date + "') AS A,tbl_BugTracking " +
                        "WHERE A.UserID = tbl_BugTracking.UserID "+
                        "AND A.TitleID = tbl_BugTracking.TitleID "+
                        "AND Date = '" + _date + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() == "")
                    return "0";
                else return tb.Rows[0][0].ToString();
            }
            else return "0";
        }
    }
}