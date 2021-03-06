﻿using System;
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

        public DataSet GetTitles(string searchstring)
        {
            String search = GetSearchString(searchstring);
            String sql = "SELECT [3LD],TitleName,TOCKCode,Category,ColorCode "+
                        "FROM tbl_Title,tbl_TitleCategory "+
                        "WHERE tbl_Title.TitleCategoryID = tbl_TitleCategory.TitleCategoryID "+
                        "AND ([3LD] LIKE '" + search.Replace("'", "''") + "%' OR TitleName LIKE '" + search.Replace("'", "''") + "%' OR TOCKCode LIKE '" + search.Replace("'", "''") + "%' OR Category LIKE '" + search.Replace("'", "''") + "%') " +
                        "ORDER BY [3LD] DESC";
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

        public void updateTitle(String _titleid,String _3ld, String titlename, String tockcode, String Category, String Color)
        {
            string CategoryID = getCateID(Category);

            string currentcolor = getColor(_titleid);
            if (Color.Equals(currentcolor))
                Color = currentcolor;
            else Color = getRandomColor(Color);

            string update = "UPDATE tbl_Title " +
                    "SET [3LD] = '" + _3ld + "', ColorCode = '" + Color + "', TitleName = '" + titlename + "', TOCKCode = '" + tockcode + "', TitleCategoryID = '" + CategoryID + "' " +
                    "WHERE TitleID = '" + _titleid + "'";
            dbconnect.ExeCuteNonQuery(update);
            
        }

        public String getTitleID(String _3ld)
        {
            String sql = "SELECT TitleID FROM tbl_Title WHERE [3LD] = '" + _3ld + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return tb.Rows[0][0].ToString();
            return "";
        }

        public void deleteTitle(String _id)
        {
            String sql = "DELETE FROM tbl_Title WHERE TitleID = '" + _id + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public Boolean checkTitle(String _id)
        {
            String sql = "SELECT * FROM tbl_ResourceAllocation WHERE TitleID = '" + _id + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return true;
            return false;
        }

        public String getCateID(String catename)
        {
            string cateid = "";
            string sql1 = "SELECT TitleCategoryID FROM tbl_TitleCategory WHERE Category = '" + catename + "'";
            DataTable tb = dbconnect.getDataTable(sql1);
            if (tb.Rows.Count!=0) cateid = tb.Rows[0][0].ToString();
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

        public String getColor(String _id)
        {
            string sql = "SELECT ColorCode FROM tbl_Title WHERE TitleID = '" + _id + "'";
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