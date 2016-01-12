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

        DBConnect dbconnect = new DBConnect();

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
