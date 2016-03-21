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

        public DataTable getRole_Hour(string UID, string date)
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
            DataTable dt = dbconnect.getDataTable("Select distinct(a.UserID), b.EmployeeID"
                                                   + " from tbl_ResourceAllocation a, tbl_User b"
                                                   + " where MONTH(date) = '"+month+"'"
                                                   + " AND a.UserID = b.UserID"
                                                   + " AND YEAR(date) = '"+year+"'"
                                                   + " AND ProjectRoleID <> 4");
            return dt;
        }
    }
}
