using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LFCRM.Class
{
    public class csAnnouncement
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();
        public DataSet GetAllAnnouncement()
        {
            string sql = "SELECT AnnounceID,AnnounceName,AnnounceTime,AnnounceLocation,Banner,Link,Urgency,Status,AnnounceType " +
                        "FROM tbl_Announcement,tbl_Urgency,tbl_AnnounceType "+
                        "WHERE tbl_Announcement.UrgencyID = tbl_Urgency.UrgencyID "+
                        "AND tbl_Announcement.AnnounceTypeID = tbl_AnnounceType.AnnounceTypeID "+
                        "ORDER BY AnnounceID DESC";
            DataSet ds = dbconnect.getDataSet(sql);
            return ds;
        }

        public void PushAnnouncement(string announceid)
        {
            string sql = "UPDATE tbl_Announcement SET Status = 'True' WHERE AnnounceID = " + announceid;
            string sql2 = "UPDATE tbl_Announcement SET Status = 'False'";
            dbconnect.ExeCuteNonQuery(sql2);
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void RemoveAnnouncement(string announceid)
        {
            string sql = "DELETE FROM tbl_Announcement WHERE AnnounceID = " + announceid;
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void AddNewAnnouncement(string name, string time, string location, string link, string urgency, string type)
        {
            string urgencyid = GetUrgencyID(urgency);
            string typeid = GetTypeID(type);
            string sql = "INSERT INTO tbl_Announcement (AnnounceName,AnnounceTime,AnnounceLocation,Link,UrgencyID,Status,AnnounceTypeID) "+
                        "VALUES ('" + name + "','" + time + "','" + location + "','" + link + "','" + urgencyid + "','True','" + typeid + "')";
            string sql2 = "UPDATE tbl_Announcement SET Status = 'False'";
            dbconnect.ExeCuteNonQuery(sql2);
            dbconnect.ExeCuteNonQuery(sql);
        }

        public string GetUrgencyID(string urgency)
        {
            string sql = "SELECT UrgencyID FROM tbl_Urgency WHERE Urgency = '" + urgency + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetTypeID(string type)
        {
            string sql = "SELECT AnnounceTypeID FROM tbl_AnnounceType WHERE AnnounceType = '" + type + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }
    }
}