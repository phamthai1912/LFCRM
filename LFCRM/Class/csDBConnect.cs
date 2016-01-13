using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace LFCRM.Class
{
    public class csDBConnect : IHttpModule
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cm;
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

        public SqlConnection InitialConnect(SqlDataSource sqlDS, SqlConnection Connection)
        {
            sqlDS.ConnectionString = ConfigurationManager.ConnectionStrings["LFCRMConnectionString"].ConnectionString;
            sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
            Connection = new SqlConnection(sqlDS.ConnectionString);
            return Connection;
        }

        //ham lay du lieu trong database
        public DataSet get(String sql)
        {
            SqlDataSource sqlDS = new SqlDataSource();
            con = InitialConnect(sqlDS, con);
            con.Open();

            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            da.Fill(ds);
            con.Close();
            return ds;
        }

        //ham them/xoa/sua du lieu bang SQL
        public void ExecuteQuery(String sql)
        {
            SqlDataSource sqlDS = new SqlDataSource();
            con = InitialConnect(sqlDS, con);
            con.Open();

            cm = new SqlCommand(sql, con);
            cm.ExecuteNonQuery();
            con.Close();
        }
    }
}
