using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LFCRM.Class
{
    public class csCoreOption
    {
        Class.csDBConnect dbconnection = new Class.csDBConnect();

        public DataSet GetListUser(string _3ld, string _date)
        {
            string sql = "SELECT EmployeeID, Fullname, Email "+
                    "FROM tbl_ResourceAllocation, tbl_User, tbl_Title "+
                    "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID "+
                    "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID "+
                    "AND Date = '" + _date + "' " +
                    "AND [3LD] = '" + _3ld + "'";
            DataSet ds = dbconnection.getDataSet(sql);
            if (ds != null)
                return ds;
            else return null;
        }

        public DataSet GetListBuild(string _3ld)
        {
            string sql = "SELECT [3LD],BuildNumber FROM tbl_BuildTitle,tbl_Title WHERE tbl_BuildTitle.TitleID = tbl_Title.TitleID AND [3LD] = '" + _3ld + "' ORDER BY BuildNumber DESC";
            DataSet ds = dbconnection.getDataSet(sql);
            if (ds != null)
                return ds;
            else return null;
        }

        public string Get3LDTitle(string employeeid, string date)
        {
            string sql = "SELECT [3LD] FROM tbl_Title, tbl_User, tbl_ResourceAllocation "+
                    "WHERE tbl_ResourceAllocation.TitleID = tbl_Title.TitleID "+
                    "AND tbl_ResourceAllocation.UserID = tbl_User.UserID "+
                    "AND EmployeeID = '" + employeeid + "' " +
                    "AND Date = '" + date + "'";
            DataTable dt = dbconnection.getDataTable(sql);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else return "";
        }

        public string GetRoleUserByAllocation(string _3ld, string employeeid)
        {
            string sql = "SELECT ProjectRoleName " +
                        "FROM tbl_ResourceAllocation,tbl_ProjectRole,tbl_Title,tbl_User " +
                        "WHERE tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID " +
                        "AND tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND EmployeeID = '" + employeeid + "' " +
                        "AND ProjectRoleName = 'Core' " +
                        "GROUP BY ProjectRoleName";
            DataTable tb = dbconnection.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetUserApprovalRole(string _3ld, string employeeid)
        {
            string sql = "SELECT ProjectRoleName " +
                    "FROM tbl_BugApproval,tbl_ProjectRole,tbl_Title,tbl_User " +
                    "WHERE tbl_BugApproval.UserID = tbl_User.UserID " +
                    "AND tbl_BugApproval.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                    "AND tbl_BugApproval.TitleID = tbl_Title.TitleID " +
                    "AND EmployeeID = '" + employeeid + "' " +
                    "AND [3LD] = '" + _3ld + "'";
            DataTable tb = dbconnection.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetUserRole(string _employeeid)
        {
            string sql = "SELECT RoleName FROM tbl_UserRole, tbl_User WHERE EmployeeID='" + _employeeid + "' AND tbl_UserRole.UserRoleID = tbl_User.UserRoleID";
            DataTable tb = dbconnection.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        //Get Title ID
        public string GetTitleID(string _3ld)
        {
            string sql = "SELECT TitleID FROM tbl_Title WHERE [3LD] = '" + _3ld + "'";
            DataTable tb = dbconnection.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        //Get User ID
        public string GetUserID(string _employeeid)
        {
            string sql = "SELECT UserID FROM tbl_User WHERE EmployeeID = '" + _employeeid + "'";
            DataTable tb = dbconnection.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public void SetApprovalRole(string _3ld, string employeeid)
        {
            string userid = GetUserID(employeeid);
            string titleid = GetTitleID(_3ld);
            string sql = "INSERT INTO tbl_BugApproval (UserID,TitleID,ProjectRoleID) VALUES ('" + userid + "', '" + titleid + "', '2')";
            dbconnection.ExeCuteNonQuery(sql);
        }

        public void DeleteApprovalRole(string _3ld, string employeeid)
        {
            string userid = GetUserID(employeeid);
            string titleid = GetTitleID(_3ld);
            string sql = "DELETE FROM tbl_BugApproval WHERE UserID = '" + userid + "' AND TitleID = '" + titleid + "'";
            dbconnection.ExeCuteNonQuery(sql);
        }

        public Boolean CheckApprovalRole(string _3ld, string employeeid)
        {
            string userid = GetUserID(employeeid);
            string titleid = GetTitleID(_3ld);
            string sql = "SELECT * FROM tbl_BugApproval WHERE UserID = '" + userid + "' AND TitleID = '" + titleid + "'";
            DataTable tb = dbconnection.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return true;
                else return false;
            }
            else return false;
        }

        //Add Build Number for Title
        public void AddBuildTitle(string _3ld, string buildnumber)
        {
            string titleid = GetTitleID(_3ld);
            string sql = "INSERT INTO tbl_BuildTitle (TitleID, BuildNumber) VALUES ('" + titleid + "', '" + buildnumber + "')";
            dbconnection.ExeCuteNonQuery(sql);
        }

        public Boolean CheckBuildTitle(string _3ld, string buildnumber)
        {
            string titleid = GetTitleID(_3ld);
            string sql = "SELECT * FROM tbl_BuildTitle WHERE TitleID = '" + titleid + "' AND BuildNumber = '" + buildnumber + "'";

            DataTable tb = dbconnection.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return true;
                else return false;
            }
            else return false;
        }

    }
}