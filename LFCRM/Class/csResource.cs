using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace LFCRM.Class
{
    public class csResource
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();

        public DataSet getResource(String searchstring)
        {
            String search = GetSearchString(searchstring);
            String sql = "SELECT EmployeeID,FullName,Email,PhoneNumber,RoleName,Active "+
                        "FROM tbl_User,tbl_UserRole "+
                        "WHERE tbl_User.UserRoleID = tbl_UserRole.UserRoleID "+
                        "AND (EmployeeID LIKE '" + search.Replace("'", "''") + "%' OR FullName LIKE '" + search.Replace("'", "''") + "%' OR Email LIKE '" + search.Replace("'", "''") + "%' OR RoleName LIKE '" + search.Replace("'", "''") + "%')" +
                        "ORDER BY EmployeeID ASC";
            DataSet ds = dbconnect.getDataSet(sql);
            return ds;
        }
        public string GetSearchString(string searchstring)
        {
            String[] temp;
            temp = searchstring.Split(' ');
            string searchstring1 = "";
            for (int i = 0; i < temp.Length; i++)
            {
                searchstring1 = searchstring1 + "%" + temp[i];
            }

            return searchstring1;
        }

        public void updateResource(String _userid,String emid, String name, String email, String phone, String birth, String role, String active)
        {
            String sqlrole = "SELECT UserRoleID FROM tbl_UserRole WHERE RoleName='" + role + "'";
            DataTable tb = dbconnect.getDataTable(sqlrole);
            String roleid=tb.Rows[0][0].ToString();

            if (birth == "")
                birth = "NULL";
            else birth = "'" + birth + "'";

            String sql = "UPDATE tbl_User "+
                "SET EmployeeID = '" + emid + "', UserRoleID = '" + roleid + "', FullName = '" + name + "', Email = '" + email + "', PhoneNumber = '" + phone + "', Birthday = " + birth + ", Active = '" + active + "'" +
                "WHERE UserID = '" + _userid + "'";

            dbconnect.ExeCuteNonQuery(sql);
        }

        public String getUserID(String emid)
        {
            String sql = "SELECT UserID FROM tbl_User WHERE EmployeeID = '" + emid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return tb.Rows[0][0].ToString();
            return "";
        }

        public Boolean checkUserAllocation(String userid)
        {
            String sql = "SELECT * FROM tbl_ResourceAllocation WHERE UserID = '" + userid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public Boolean idExist(String id)
        {
            String sql = "SELECT EmployeeID FROM tbl_User WHERE EmployeeID = '" + id + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public void updatePassword(String id, String newpass)
        {
            String sql = "UPDATE tbl_User " +
                    "SET Password = '" + newpass + "'" +
                    "WHERE UserID = '" + id + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void deleteResource(String id)
        {
            String sql = "DELETE FROM tbl_User WHERE UserID = '" + id + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void addResource(String id, String name, String mail, String phone, String birth, String role, String active)
        {
            String pass = newPassword(mail);
            if (birth == "")
                birth = "NULL";
            else birth = "'" + birth + "'";
            String sqlrole = "SELECT UserRoleID FROM tbl_UserRole WHERE RoleName='" + role + "'";
            DataTable tb = dbconnect.getDataTable(sqlrole);
            String roleid = tb.Rows[0][0].ToString();

            string sql = "INSERT INTO tbl_User (EmployeeID,UserRoleID,FullName,Email,Password,PhoneNumber,Birthday,Active) " +
                        "VALUES ('" + id + "','" + roleid + "','" + name + "','" + mail + "','" + pass + "','" + phone + "'," + birth + ",'" + active + "')";
            
            dbconnect.ExeCuteNonQuery(sql);
        }

        public String newPassword(String email)
        {
            String[] temp;
            temp = email.Split('@');

            return temp[0];

        }

        public String getBirthday(String userid)
        {
            String sql = "SELECT CONVERT(VARCHAR(10), Birthday, 101) AS DATE FROM tbl_User WHERE UserID = '" + userid + "'";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() == "NULL")
                    return "";
                return tb.Rows[0][0].ToString();
            }
            return "";
        }
    }
}