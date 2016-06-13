using System;
using System.Data;
using System.Web;
using System.Web.UI;

namespace LFCRM.Class
{
    public class csFeedback : IHttpModule
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();        
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

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }
        public void addfeedback (string usersendid, string userreceiveid, string titleid, string message, int point)
        {
            string sql = "INSERT INTO tbl_FeedBack (Date,UserSendID,UserReceiveID,TitleID,Message,Point)" +
                        "VALUES (GETDATE(),'" + usersendid + "','" + userreceiveid + "','" + titleid + "',N'" + message + "','" + point + "')";
            dbconnect.ExeCuteNonQuery(sql);
        }
        public void addfeedback_nonsendid(string userreceiveid, string titleid, string message, int point)
        {
            string sql = "INSERT INTO tbl_FeedBack (Date,UserSendID,UserReceiveID,TitleID,Message,Point)" +
                        "VALUES (GETDATE(),NULL,'" + userreceiveid + "','" + titleid + "',N'" + message + "','" + point + "')";
            dbconnect.ExeCuteNonQuery(sql);
        }
       
        public string getEmployeeIDbyName(string fullname)
        {
                string EmployeeID = "";
                string sql = "SELECT EmployeeID FROM tbl_User WHERE FullName = N'" + fullname + "'";
                DataTable tb = dbconnect.getDataTable(sql);
                if (tb.Rows.Count != 0) EmployeeID = tb.Rows[0][0].ToString();
                return EmployeeID;
        }
        public Boolean FullNameExist(string fullname)
        {
            string sql = "SELECT * FROM tbl_User WHERE FullName = N'" + fullname + "'";
            DataTable tbcolor = dbconnect.getDataTable(sql);

            if (tbcolor.Rows.Count == 0)
                return false;
            else
                return true;
        }
        public string getEmployeeIDbyID(string fullname)
        {
            string UserID = "";
            string sql = "SELECT UserID FROM tbl_User WHERE FullName = N'" + fullname + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) UserID = tb.Rows[0][0].ToString();
            return UserID;
        }
        public Boolean check3LD(string _LD)
        {            
            string sql = "SELECT TitleID FROM tbl_title WHERE [3LD] = N'" + _LD + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count == 0)
                return false;
            else
                return true;
        }
        public string getTitleIDby3LD(string _LD)
        {
            string str = "";
            string sql = "SELECT TitleID FROM tbl_title WHERE [3LD] = N'" + _LD + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }
        public string getTitleEmployeeWorking(string _employee)
        {
            string str = "";
            string sql = "SELECT TitleID from tbl_ResourceAllocation WHERE (DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)= DATE) AND ([UserID]=N'" + _employee + "')";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }
        public string get3LDbyID(string titleID)
        {
            string str = "";
            string sql = "SELECT [3LD] FROM tbl_title WHERE titleID = N'" + titleID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }

        //Get List Email Of Admin
        public DataTable GetListAdminEmail()
        {
            string sql = "SELECT Email " +
                        "FROM tbl_User,tbl_UserRole " +
                        "WHERE tbl_User.UserRoleID = tbl_UserRole.UserRoleID " +
                        "AND RoleName = 'Admin'";
            DataTable dt = dbconnect.getDataTable(sql);
            if (dt != null)
                return dt;
            return null;
        }
    }
}
