using AjaxControlToolkit;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;

namespace LFCRM.Class
{
    public class csPerformanceTracking : IHttpModule
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

        Class.csDBConnect dbconnect = new Class.csDBConnect();

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public DataTable getUserListbyMonth(int month, int year)
        {
            DataTable dt = dbconnect.getDataTable("Select distinct(UserID) from tbl_ResourceAllocation where MONTH(date) = "+month+" AND YEAR(date) = "+year+"AND ProjectRoleID <> 4");
            return dt;
        }

        public DataTable getTitleListbyMonth(int month, int year)
        {
            DataTable dt = dbconnect.getDataTable("Select distinct([3LD]), Colorcode From tbl_resourceallocation, tbl_title where tbl_resourceallocation.titleID = tbl_title.titleID AND MONTH(date) = " + month + " AND YEAR(date) = " + year);
            return dt;
        }

        public string getFullName(string UID)
        {
            string fullname = "";
            string sql = "SELECT FullName FROM tbl_User WHERE UserID = '" + UID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) fullname = tb.Rows[0][0].ToString();
            return fullname;
        }

        public DataTable getNoBug_ColorCode(string UID, string date)
        {
            DataTable dt = dbconnect.getDataTable("Select NumberOfBugs, ColorCode From tbl_BugTracking, tbl_Title Where tbl_BugTracking.TitleID = tbl_Title.TitleID AND tbl_BugTracking.date = '"+date+"' AND tbl_BugTracking.UserID = "+UID);
            return dt;
        }

        public DataTable getRole_Hour(string UID, string date)
        {
            DataTable dt = dbconnect.getDataTable("Select ProjectRoleName, Value From tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours Where tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID AND tbl_WorkingHours.WorkingHoursID = tbl_ResourceAllocation.WorkingHoursID AND UserID="+UID+" AND Date='"+date+"'");
            return dt;
        }
    }
}
