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
        public DataTable GetListUser(string month)
        {
            string sql = "SELECT EmployeeID,FullName,Count(BugTitleID) AS NUMBER FROM tbl_BugTitle,tbl_User WHERE tbl_BugTitle.UserEnterID = tbl_User.UserID " +
                        "AND RIGHT(CONVERT(VARCHAR(10), DateEnter, 103), 7) = '" + month + "' " +
                        "AND BugStatusID = '1' "+
                        "GROUP BY EmployeeID, FullName";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return tb;
            else return null;
        }
    }
}