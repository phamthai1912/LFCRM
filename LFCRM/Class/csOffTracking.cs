using System;
using System.Data;
using System.Web;

namespace LFCRM.Class
{
    public class csOffTracking : IHttpModule
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

        public DataTable getRoleHour(string UID, string date)
        {
            DataTable dt = dbconnect.getDataTable("Select ProjectRoleName, Value, TitleID"
                                                   + " From tbl_ResourceAllocation, tbl_ProjectRole, tbl_WorkingHours"
                                                   + " Where tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID"
                                                   + " AND tbl_WorkingHours.WorkingHoursID = tbl_ResourceAllocation.WorkingHoursID"
                                                   + " AND UserID='" + UID + "'"
                                                   + " AND Date='" + date + "'"
                                                   + " AND ProjectRoleName='Off'");
            return dt;
        }

        public DataTable getUserListbyMonth(int month, int year)
        {
            DataTable dt = dbconnect.getDataTable("Select distinct(a.UserID), b.EmployeeID, b.FullName"
                                                    + " from tbl_ResourceAllocation a, tbl_User b"
                                                    + " where MONTH(date) = '"+month+"'"
                                                    + " AND YEAR(date) = '"+year+"'"
                                                    + " AND a.UserID = b.UserID"
                                                    + " UNION"
                                                    + " Select distinct(a.UserID), b.EmployeeID, b.FullName"
                                                    + " from tbl_OffTracking a, tbl_User b"
                                                    + " where MONTH(date) = '"+month+"'"
                                                    + " AND YEAR(date) = '"+year+"'"
                                                    + " AND a.UserID = b.UserID");
            return dt;
        }

        public void addOffTracking(string date, string UserID, int WorkingHoursID)
        {
            string sql = "INSERT INTO tbl_OffTracking (Date,UserID,WorkingHoursID) " +
                        "VALUES ('" + date + "','" + UserID + "','" + WorkingHoursID + "')";

            dbconnect.ExeCuteNonQuery(sql);
        }

        public Boolean checkOffUserExistByDate(string date, string UID)
        {
            string sql = "SELECT * FROM tbl_OffTracking WHERE Date = '" + date + "' AND UserID = '" + UID + "'";
            DataTable tbcolor = dbconnect.getDataTable(sql);

            if (tbcolor.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public DataTable getRoleHourFromTracking(string UID, string date)
        {
            DataTable dt = dbconnect.getDataTable("SELECT value"
                                                    +" FROM tbl_OffTracking a, tbl_WorkingHours b"
                                                    +" WHERE Date = '"+date+"'"
                                                    + " AND UserID='"+UID+"'"
                                                    +" AND a.WorkingHoursID = b. WorkingHoursID");
            return dt;
        }

        public DataTable getOffTracking()
        {
            DataTable dt = dbconnect.getDataTable("SELECT EmployeeID, FullName, date, value, OffID "
                                                    +" FROM tbl_OffTracking a, tbl_User b, tbl_WorkingHours c"
                                                    +" WHERE a.UserID = b.UserID"
                                                    +" AND a.WorkingHoursId = c.WorkingHoursID"
                                                    + " AND date > '" + DateTime.Now + "' "
                                                    +" Order by EmployeeID, date");
            return dt;
        }

        public void deleteOffTrackingByID(string offID)
        {
            String sql = "DELETE FROM tbl_OffTracking WHERE OffID = '" + offID + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }
    }
}
