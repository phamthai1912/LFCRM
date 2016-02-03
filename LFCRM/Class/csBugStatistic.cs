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
            if(date == "")
                sql = "SELECT Date,FullName,[3LD],ProjectRoleName,Value " +
                           "FROM tbl_User, tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours " +
                           "WHERE tbl_ResourceAllocation.EmployeeID = tbl_User.EmployeeID " +
                           "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                           "AND tbl_ResourceAllocation.WorkingHoursID = tbl_WorkingHours.WorkingHoursID ";
            else
                sql = "SELECT Date,FullName,[3LD],ProjectRoleName,Value "+
                "FROM tbl_User, tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours "+
                "WHERE tbl_ResourceAllocation.EmployeeID = tbl_User.EmployeeID " +
                "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                "AND tbl_ResourceAllocation.WorkingHoursID = tbl_WorkingHours.WorkingHoursID " +
                "AND Date = '" + date + "'";
            
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

        public String getNoBugs(String _date, String name, String _3ld)
        {
            String _id = getEmployeeID(name);
            String sql = "SELECT NumberOfBugs FROM tbl_BugTracking WHERE Date = '" + _date + "' AND EmployeeID = '" + _id + "' AND [3LD] = '" + _3ld + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            String number;

            if (tb.Rows.Count != 0)
                number = tb.Rows[0][0].ToString()+" bug(s)";
            else number = "N/A"; 

            return number;
        }

        public void updateBugs(String _date, String name, String _3ld, String number)
        {
            String _id = getEmployeeID(name);
            String sql = "UPDATE tbl_BugTracking SET NumberOfBugs = '" + number + "' " +
                "WHERE Date = '" + _date + "' AND EmployeeID = '" + _id + "' AND [3LD] = '" + _3ld + "' ";
            dbconnect.ExeCuteNonQuery(sql);
        }

        //public Boolean checkEmployeeHasBugs(String _date, String name, String _3ld, String number)
        //{
        //    String _id = getEmployeeID(name);
        //    String sql = "UPDATE tbl_BugTracking SET NumberOfBugs = '" + number + "' " +
        //        "WHERE Date = '" + _date + "' AND EmployeeID = '" + _id + "' AND [3LD] = '" + _3ld + "' ";
        //    dbconnect.ExeCuteNonQuery(sql);
        //}

        public void addBugs(String _date, String name, String _3ld, String number)
        {
            String _id = getEmployeeID(name);

            String sql = "INSERT INTO tbl_BugTracking (Date,EmployeeID,[3LD],NumberOfBugs) "+
                    "VALUES ('" + _date + "','" + _id + "','" + _3ld + "','" + number + "')";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public String getNumberBugs(String text)
        {
            String[] temp;
            temp = text.Split(' ');

            return temp[0];

        }
    }
}