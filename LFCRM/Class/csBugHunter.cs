using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace LFCRM.Class
{
    public class csBugHunter : IHttpModule
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

        csDBConnect DBConnect = new csDBConnect();
        csResourceAllocation Allc = new csResourceAllocation();

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public DataTable GetBugHunterList(string year)
        {
            string sql = "SELECT EmployeeID,FullName FROM tbl_BugHunter b,tbl_User u WHERE b.UserID = u.UserID AND YEAR(Month) = "+year+" Group by EmployeeID,FullName";
            DataTable dt = DBConnect.getDataTable(sql);
            return dt;
        }

        public bool CheckBugHunter(string ID, string month, string year)
        {
            string sql = "SELECT EmployeeID FROM tbl_BugHunter b,tbl_User u WHERE b.UserID = u.UserID AND EmployeeID = "+ID+" AND YEAR(Month) = "+year+" And MONTH(Month) = '"+month+"'";
            DataTable dt = DBConnect.getDataTable(sql);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public int GetNumberOfBugHunterMonth(string year, string EmpID)
        {
            string sql = "SELECT Month FROM tbl_BugHunter b,tbl_User u WHERE b.UserID = u.UserID AND EmployeeID = " + EmpID + " AND YEAR(Month) = " + year + " Group by Month";
            DataTable dt = DBConnect.getDataTable(sql);
            int count = dt.Rows.Count;
            return count;
        }

        public void AddBugHunter(string date, string EmpID)
        {
            string UserID = Allc.getIDbyEmployeeID(EmpID).ToString();
            string sql = "Insert into tbl_BugHunter values('" + date + "'," + UserID + ")";
            DBConnect.ExeCuteNonQuery(sql);
        }
        public void RemoveBugHunter(string month, string year, string EmpID)
        {
            string UserID = Allc.getIDbyEmployeeID(EmpID).ToString();
            string sql = "delete from tbl_BugHunter where UserID= " + UserID + " and year(Month) = " + year + " and month(Month) = " + month + "";
            DBConnect.ExeCuteNonQuery(sql);
        }
    }
}
