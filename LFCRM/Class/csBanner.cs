using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LFCRM.Class
{
    public class csBanner
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();
        public string GetUserRoleOnTitle(string _3ld, string employeeid)
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
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public ArrayList GetEvent()
        {
            ArrayList list = new ArrayList();
            String sql = "SELECT EventName,Time,Location,Banner,Link FROM tbl_Event WHERE Status = 'True'";

            DataTable tbl = dbconnect.getDataTable(sql);
            if (tbl.Rows.Count != 0)
            {
                for (int i = 0; i < tbl.Columns.Count; i++)
                    list.Add(tbl.Rows[0][i].ToString());
            }

            return list;
        }

        public ArrayList GetAnnounce()
        {
            ArrayList list = new ArrayList();
            String sql = "SELECT AnnounceName,Link,Urgency,Status,AnnounceType " +
                        "FROM tbl_Announcement,tbl_Urgency,tbl_AnnounceType " +
                        "WHERE tbl_Announcement.UrgencyID = tbl_Urgency.UrgencyID " +
                        "AND tbl_Announcement.AnnounceTypeID = tbl_AnnounceType.AnnounceTypeID " +
                        "AND Status = 'True'";

            DataTable tbl = dbconnect.getDataTable(sql);
            if (tbl.Rows.Count != 0)
            {
                for (int i = 0; i < tbl.Columns.Count; i++)
                    list.Add(tbl.Rows[0][i].ToString());
            }

            return list;
        }
    }
}