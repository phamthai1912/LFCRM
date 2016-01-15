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
        SqlDataSource sqlDS = new SqlDataSource();
        SqlConnection Connection;

        csDBConnect dbconnect = new csDBConnect();

        public DataSet getTitlelist() 
        {
            
            Connection = dbconnect.InitialConnect(sqlDS, Connection);
            Connection.Open();

            sqlDS.SelectCommand = "SELECT TitleID,TitleName,TOCKCode,Category,ColorCode"+ 
                "FROM tbl_Title,tbl_TitleCategory,tbl_Color "+
                "WHERE tbl_Title.TitleCategoryID = tbl_TitleCategory.TitleCategoryID AND tbl_Title.ColorID = tbl_Color.ColorID";
            
            DataSet ds = (DataSet)sqlDS.Select(DataSourceSelectArguments.Empty);
            //DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
            
            Connection.Close();
            return ds;
        }
    }
}