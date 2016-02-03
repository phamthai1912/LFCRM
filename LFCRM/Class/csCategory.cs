using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace LFCRM.Class
{
    public class csCategory : IHttpModule
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

        public void updateCategory(String categoryid, String categoryname, String Color)
        {
            String sql = "UPDATE tbl_TitleCategory " +
                    "SET Category = '" + categoryname + "', Color = '" + Color + "'" +
                    "WHERE TitleCategoryID = '" + categoryid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void addCategory(String category, String color)
        {

            string newcolor = getRandomColor(color);

            string sql = "INSERT INTO tbl_TitleCategory (Category,Color) " +
                        "VALUES ('" + category + "','" + newcolor + "')";
            
            dbconnect.ExeCuteNonQuery(sql);
        }

        public Boolean checkCategory(String cate)
        {
            String id = getCategoryID(cate);
            String sql = "SELECT TitleName FROM tbl_Title WHERE TitleCategoryID = '" + id + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return true;
            else
                return false;
        }

        public void deleteCategory(String cate)
        {
            String sql = "DELETE FROM tbl_TitleCategory WHERE Category = '"+cate+"'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public String getCategory(String id)
        {

            String sql = "SELECT Category FROM tbl_TitleCategory WHERE TitleCategoryID="+ int.Parse(id);
            Console.WriteLine(id);
            DataTable tb = dbconnect.getDataTable(sql);
            String cate = tb.Rows[0][0].ToString();

            return cate;
        }

        public String getCategoryID(String cate)
        {
            String sql = "SELECT TitleCategoryID FROM tbl_TitleCategory WHERE Category='" + cate + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            String id = tb.Rows[0][0].ToString();

            return id;
        }

        public Boolean checkColorExist(String color)
        {
            string sql = "SELECT Color FROM tbl_TitleCategory WHERE Color = '" + color + "'";
            DataTable tbcolor = dbconnect.getDataTable(sql);
            if (tbcolor.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public Boolean checkCategoryExist(String cate)
        {
            string sql = "SELECT Category FROM tbl_TitleCategory WHERE Category = '" + cate + "'";
            DataTable tbcolor = dbconnect.getDataTable(sql);
            if (tbcolor.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public String getRandomColor(String _color)
        {
            var random = new Random();
            string colorrandom = String.Format("#{0:X6}", random.Next(0x1000000));

            string sqlcolor = "SELECT Color FROM tbl_TitleCategory";
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
    }
}
