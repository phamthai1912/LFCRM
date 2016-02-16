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

        public void updateResource(String oridi,String id, String name, String email, String phone, String role, String active)
        {
            String sqlrole = "SELECT UserRoleID FROM tbl_UserRole WHERE RoleName='" + role + "'";
            DataTable tb = dbconnect.getDataTable(sqlrole);
            String roleid=tb.Rows[0][0].ToString();

            String sql = "UPDATE tbl_User "+
                "SET EmployeeID = '" + id + "', UserRoleID = '" + roleid + "', FullName = '" + name + "', Email = '" + email + "', PhoneNumber = '" + phone + "', Active = '" + active + "'" +
                "WHERE EmployeeID = '" + oridi + "'";

            dbconnect.ExeCuteNonQuery(sql);
        }

        public Boolean checkUserAllocation(String id)
        {
            String sql = "SELECT * FROM tbl_ResourceAllocation WHERE EmployeeID = '" + id + "'";
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
                    "WHERE EmployeeID = '" + id + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void deleteResource(String id)
        {
            String sql = "DELETE FROM tbl_User WHERE EmployeeID = '" + id + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void addResource(String id, String name, String mail, String phone, String role, String active)
        {
            String pass = newPassword(mail);

            String sqlrole = "SELECT UserRoleID FROM tbl_UserRole WHERE RoleName='" + role + "'";
            DataTable tb = dbconnect.getDataTable(sqlrole);
            String roleid = tb.Rows[0][0].ToString();

            string sql = "INSERT INTO tbl_User (EmployeeID,UserRoleID,FullName,Email,Password,PhoneNumber,Active) " +
                        "VALUES ('" + id + "','" + roleid + "','" + name + "','" + mail + "','" + pass + "','" + phone + "','" + active + "')";
            
            dbconnect.ExeCuteNonQuery(sql);
        }

        public String newPassword(String email)
        {
            String[] temp;
            temp = email.Split('@');

            return temp[0];

        }
    }
}