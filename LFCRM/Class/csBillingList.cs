using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace LFCRM.Class
{
    public class csBillingList
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();

        public DataTable getBillingList(String _date)
        {
            String sql;
            String currentdate = DateTime.Now.ToString("MM/dd/yyyy");
            if(_date == "")
                sql = "SELECT FullName, [3LD], Value as Hours " +
                "FROM tbl_ResourceAllocation,tbl_User,tbl_WorkingHours,tbl_Title " +
                "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID " +
                "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID "+
                "AND (ProjectRoleID = '2' OR ProjectRoleID = '1') " +
                "AND tbl_WorkingHours.WorkingHoursID = tbl_ResourceAllocation.WorkingHoursID " +
                "AND Date = '" + currentdate + "'"+
                "ORDER BY [3LD] ASC";
            else
                sql = "SELECT FullName, [3LD], Value as Hours "+
                "FROM tbl_ResourceAllocation,tbl_User,tbl_WorkingHours,tbl_Title " +
                "WHERE tbl_User.UserID = tbl_ResourceAllocation.UserID " +
                "AND tbl_Title.TitleID = tbl_ResourceAllocation.TitleID " +
                "AND (ProjectRoleID = '2' OR ProjectRoleID = '1') " +
                "AND tbl_WorkingHours.WorkingHoursID = tbl_ResourceAllocation.WorkingHoursID "+
                "AND Date = '" + _date + "'"+
                "ORDER BY [3LD] ASC";

            DataTable tb = dbconnect.getDataTable(sql);

            return tb;
        }

        public DataTable getBillingProject(String _date)
        {
            String sql;
            String currentdate = DateTime.Now.ToString("MM/dd/yyyy");
            if (_date == "")
                sql = "SELECT [3LD],TOCKCode,SUM(tbl_WorkingHours.Value) as Hours " +
                "FROM tbl_ResourceAllocation,tbl_WorkingHours,tbl_Title " +
                "WHERE tbl_ResourceAllocation.TitleID = tbl_Title.TitleID " +
                "AND (ProjectRoleID = '2' OR ProjectRoleID = '1') " +
                "AND	tbl_WorkingHours.WorkingHoursID = tbl_ResourceAllocation.WorkingHoursID " +
                "AND Date = '" + currentdate + "' "+
                "GROUP BY [3LD],TOCKCode";
            else
                sql = "SELECT [3LD],TOCKCode,SUM(tbl_WorkingHours.Value) as Hours " +
                "FROM tbl_ResourceAllocation,tbl_WorkingHours,tbl_Title " +
                "WHERE tbl_ResourceAllocation.TitleID = tbl_Title.TitleID " +
                "AND (ProjectRoleID = '2' OR ProjectRoleID = '1') " +
                "AND	tbl_WorkingHours.WorkingHoursID = tbl_ResourceAllocation.WorkingHoursID " +
                "AND Date = '" + _date + "' "+
                "GROUP BY [3LD],TOCKCode";

            DataTable tb = dbconnect.getDataTable(sql);

            return tb;
        }

        public Boolean checkFwTitle(String _3ld)
        {
            String sql = "SELECT Category " +
            "FROM tbl_TitleCategory,tbl_Title " +
            "WHERE tbl_Title.TitleCategoryID = tbl_TitleCategory.TitleCategoryID " +
            "AND [3LD] = '" + _3ld + "'";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() == "Platform projects")
                    return true;
                else
                    return false;
            }
            else 
                return false;
        }

        public String getLastName(String _fullname)
        {
            String[] temp;
            temp = _fullname.Split(' ');

            return temp.First();
        }

        public String getFirstName(String _fullname)
        {
            String[] temp;
            temp = _fullname.Split(' ');

            return temp.Last();
        }

        public String getMidleName(String _fullname)
        {
            String[] temp;
            temp = _fullname.Split(' ');
            if (temp.Length>2)
                return temp[2];
            return temp[1];
        }

        public ArrayList getListName(DataTable table)
        {
            ArrayList listname = new ArrayList();
            foreach (DataRow row in table.Rows)
            {
                String FullName = row[0].ToString();
                listname.Add(FullName);
            }

            return listname;
        }

        public ArrayList getNewListName(DataTable table)
        {
            ArrayList listname1 = getListName(table);
            ArrayList listname2 = new ArrayList();

            for (int i = 0; i < listname1.Count; i++)
            {
                String list = getFirstName(listname1[i] as string) + " " + getLastName(listname1[i] as string);
                listname2.Add(list);
            }

            for (int i = 0; i < listname1.Count; i++)
            {
                String name = getFirstName(listname1[i] as string) + " " + getLastName(listname1[i] as string);
                for (int j = i+1; j < listname1.Count; j++)
                {
                    String name2 = getFirstName(listname1[j] as string) + " " + getLastName(listname1[j] as string);
                    if (name.Equals(name2))
                    {
                        listname2[i] = getFirstName(listname1[i] as string) + " " + getLastName(listname1[i] as string) + " " + getMidleName(listname1[i] as string);
                        listname2[j] = getFirstName(listname1[j] as string) + " " + getLastName(listname1[j] as string) + " " + getMidleName(listname1[j] as string);

                        break;
                    }                    
                }
            }

            return listname2;
        }
    }
}