using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.Class
{
    public class csLogin : IHttpModule
    {
        SqlDataSource sqlDS = new SqlDataSource();
        SqlConnection Connection;

        csDBConnect dbconnect = new csDBConnect();

        public csLogin()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        

        public bool CheckLogin(string ID, string pass)
        {
            Connection = dbconnect.InitialConnect(sqlDS, Connection);
            Connection.Open();
            bool ok;

            sqlDS.SelectCommand = "SELECT * FROM tbl_User WHERE EmployeeID=@ID AND Password=@pass";
            sqlDS.SelectParameters.Add("ID", ID);
            sqlDS.SelectParameters.Add("pass", pass);
            DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
            if (dv.Count > 0) ok = true;
            else ok = false;

            Connection.Close();
            return ok;
        }

        public string GetFullName(string ID)
        {
            Connection = dbconnect.InitialConnect(sqlDS, Connection);
            Connection.Open();
            string fullname = "";

            sqlDS.SelectCommand = "SELECT FullName FROM tbl_User WHERE EmployeeID=@ID";
            DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
            if (dv.Count > 0) fullname = Convert.ToString(dv.Table.Rows[0]["FullName"]);

            Connection.Close();
            return fullname;
        }

        public string GetUserRole(string ID)
        {
            Connection = dbconnect.InitialConnect(sqlDS, Connection);
            Connection.Open();
            string UserRole = "";

            sqlDS.SelectCommand = "SELECT RoleName FROM tbl_UserRole, tbl_User WHERE EmployeeID=@ID AND tbl_UserRole.UserRoleID = tbl_User.UserRoleID";
            DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
            if (dv.Count > 0) UserRole = Convert.ToString(dv.Table.Rows[0]["RoleName"]);

            Connection.Close();
            return UserRole;
        }

        public string GetUserRoleProject(string ID)
        {
            string sql = "SELECT ProjectRoleName " +
                    "FROM tbl_ResourceAllocation,tbl_ProjectRole,tbl_User " +
                    "WHERE tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                    "AND tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                    "AND EmployeeID = '" + ID + "'" +
                    "AND Date = '" + DateTime.Now.ToString("MM/dd/yyyy") + "'" +
                    "AND ProjectRoleName = 'Core'";
            
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetTitleID(string employeeid)
        {
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            string sql = "SELECT TitleID "+
                        "FROM tbl_ResourceAllocation,tbl_User "+
                        "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID "+
                        "AND EmployeeID = '" + employeeid + "'  " +
                        "AND Date = '" + date + "'";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() == "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string Get3LDTitle(string employeeid)
        {
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            string sql = "SELECT [3LD] " +
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title " +
                        "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID "+
                        "AND EmployeeID = '" + employeeid + "'  " +
                        "AND Date = '" + date + "'";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

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

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            throw new NotImplementedException();
        }

        void IHttpModule.Dispose()
        {
            throw new NotImplementedException();
        }

        void IHttpModule.Init(HttpApplication context)
        {
            throw new NotImplementedException();
        }
    }
}
