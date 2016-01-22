using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.Class
{
    public class csTitleManager
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();

        public void updateTitle(String _3ld, String titlename, String tockcode, String Category, String Color)
        {
            string CategoryID = getCateID(Category);            

            string currentcolor = getColor(_3ld);
            if (Color.Equals(currentcolor))
                Color = currentcolor;
            else Color = getRandomColor(Color);

            string update = "UPDATE tbl_Title " +
                    "SET ColorCode = '" + Color + "', TitleName = '" + titlename + "', TOCKCode = '" + tockcode + "', TitleCategoryID = '" + CategoryID + "' " +
                    "WHERE [3LD] = '" + _3ld + "'";
            dbconnect.ExeCuteNonQuery(update);
            
        }

        public String getCateID(String catename)
        {
            string sql1 = "SELECT TitleCategoryID FROM tbl_TitleCategory WHERE Category = '" + catename + "'";
            DataTable tb = dbconnect.getDataTable(sql1);
            string cateid = tb.Rows[0][0].ToString();
            return cateid;
        }

        public void addTitle(String _3ld, String titlename, String tockcode, String Category, String Color)
        {
            string CategoryID = getCateID(Category);

            string newcolor = getRandomColor(Color);

            string sql = "INSERT INTO tbl_Title ([3LD],TitleCategoryID,ColorCode,TOCKCode,TitleName) " +
                        "VALUES ('" + _3ld + "','" + CategoryID + "','" + newcolor + "','" + tockcode + "','" + titlename + "')";
            
            dbconnect.ExeCuteNonQuery(sql);
        }

        public String getRandomColor(String _color)
        {
            var random = new Random();
            string colorrandom = String.Format("#{0:X6}", random.Next(0x1000000));

            string sqlcolor = "SELECT ColorCode FROM tbl_Title";
            DataTable tbcolor = dbconnect.getDataTable(sqlcolor);

            int row = tbcolor.Rows.Count;
            for (int i = 0; i < row; i++)
            {
                string temp = tbcolor.Rows[i][0].ToString();
                if (_color.Equals(temp))
                {
                    _color = colorrandom;
                    getRandomColor(_color);
                    break;
                }
            }

            return _color;
        }

        public String getColor(String _3ld)
        {
            string sql = "SELECT ColorCode FROM tbl_Title WHERE [3LD] = '" + _3ld + "'";
            DataTable tbcolor = dbconnect.getDataTable(sql);
            string color = tbcolor.Rows[0][0].ToString();

            return color;
        }

        public Boolean checkcolorExist(String color)
        {
            string sql = "SELECT ColorCode FROM tbl_Title WHERE ColorCode = '" + color + "'";
            DataTable tbcolor = dbconnect.getDataTable(sql);
            if (tbcolor.Rows.Count == 0)
                return false;
            else
                return true;

        }

        public Boolean LDExist(string text)
        {
            string sql = "SELECT * FROM tbl_Title WHERE [3LD] = '" + text + "'";
            DataTable tbcolor = dbconnect.getDataTable(sql);

            if (tbcolor.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}