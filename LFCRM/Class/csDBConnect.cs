using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

namespace LFCRM.Class
{    
    public class csDBConnect : IHttpModule
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommand cmd;
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

        public void conncet()
        {
            if (con == null)
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["LFCRMConnectionString"].ConnectionString);
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        public void disconnect()
        {
            if ((con != null) && (con.State == ConnectionState.Open))
                con.Close();
        }

        public DataSet getDataSet(string sql)
        {
            conncet();
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            da.Fill(ds);
            disconnect();

            return ds;
        }

        public DataTable getDataTable(string sql)
        {
            conncet();
            da = new SqlDataAdapter(sql, con);
            dt = new DataTable();
            da.Fill(dt);
            disconnect();

            return dt;
        }

        public void ExeCuteNonQuery(string sql)
        {
            conncet();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            disconnect();

        }
    }
}