using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LFCRM.Class
{
    public class csCoreTracking
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();

        public DataTable getListCore(String _month)
        {
            String sql = "SELECT tbl_User.UserID,FullName " +
                    "FROM tbl_ResourceAllocation,tbl_User,tbl_ProjectRole "+
                    "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID "+
                    "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID "+
                    "AND ProjectRoleName = 'Core' "+
                    "AND RIGHT(CONVERT(VARCHAR(10), Date, 103), 7) = '" + _month + "' " +
                    "GROUP BY tbl_User.UserID,FullName";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                return tb;
            }
            else return null;
        }

        public String getTitleID(String _userid, String _date)
        {
            String sql = "SELECT TitleID "+
                        "FROM tbl_ResourceAllocation,tbl_ProjectRole "+
                        "WHERE Date = '" + _date + "' " +
                        "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID "+
                        "AND UserID = '" + _userid + "' " +
                        "AND ProjectRoleName = 'Core'";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return tb.Rows[0][0].ToString();
            else return "";
        }

        public String getNumberHoursMember(String _titleid, String _date)
        {
            String sql = "SELECT SUM(Value) AS NUMBER " +
                        "FROM tbl_ResourceAllocation,tbl_ProjectRole,tbl_WorkingHours " +
                        "WHERE Date = '" + _date + "' " +
                        "AND tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID "+
                        "AND tbl_ResourceAllocation.WorkingHoursID = tbl_WorkingHours.WorkingHoursID "+
                        "AND TitleID = '" + _titleid + "' " +
                        "AND (ProjectRoleName = 'Core' OR ProjectRoleName = 'Billable')";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                return tb.Rows[0][0].ToString();
            }
            return "";
        }
    }
}