using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.Class
{
    public class csAutoComplete : IHttpModule
    {
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

        SqlDataSource sqlDS = new SqlDataSource();
        SqlConnection Connection;

        csDBConnect dbconnect = new csDBConnect();

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public string[] AT_GetTitleList()
        {
            Connection = dbconnect.InitialConnect(sqlDS, Connection);
            Connection.Open();

            sqlDS.SelectCommand = "SELECT TitleName FROM tbl_Title";
            DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);

            List<string> items = new List<string>(dv.Count);
            for (int i = 0; i < dv.Count; i++)
            {
                items.Add(Convert.ToString(dv.Table.Rows[i]["TenNhaCungCap"]));
            }

            Connection.Close();
            return items.ToArray();
        }
    }
}
