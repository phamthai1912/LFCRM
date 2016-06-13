using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LFCRM.Class
{
    public class csMyFavorite
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();
        public DataSet GetListBugFavorite(string employeeid)
        {
            string userid = GetUserID(employeeid);
            string sql = "SELECT tbl_BugTitle.BugTitleID,Summary,Link FROM tbl_BugTitle,tbl_BugFavourite "+
                        "WHERE tbl_BugFavourite.BugTitleID = tbl_BugTitle.BugTitleID "+
                        "AND UserID = '" + userid + "'";
            DataSet ds = dbconnect.getDataSet(sql);
            if (ds != null)
                return ds;
            else return null;
        }
        public string GetUserID(string employeeid)
        {
            string sql = "SELECT UserID FROM tbl_User WHERE EmployeeID = '" + employeeid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetDescription(string bugid)
        {
            string sql = "SELECT Description FROM tbl_BugTitle WHERE BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetSteps(string bugid)
        {
            string sql = "SELECT Steps FROM tbl_BugTitle WHERE BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetSeverity(string bugid)
        {
            string sql = "SELECT BugSeverity FROM tbl_BugTitle, tbl_BugSeverity WHERE tbl_BugTitle.BugSeverityID = tbl_BugSeverity.BugSeverityID AND BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetReproduce(string bugid)
        {
            string sql = "SELECT BugReproduce FROM tbl_BugTitle, tbl_BugReproduce WHERE tbl_BugTitle.BugReproduceID = tbl_BugReproduce.BugReproduceID AND BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetLink(string bugid)
        {
            string sql = "SELECT Link FROM tbl_BugTitle WHERE BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public void RemoveFromFavorite(string employeeid, string bugid)
        {
            string userid = GetUserID(employeeid);
            string sql = "DELETE FROM tbl_BugFavourite WHERE UserID = '" + userid + "' AND BugTitleID = '" + bugid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }
    }
}