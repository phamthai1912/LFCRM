using LFCRM.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoCompleteExample
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoComplete : System.Web.Services.WebService
    {
        SqlDataSource sqlDS = new SqlDataSource();
        SqlConnection Connection;

        csDBConnect dbconnect = new csDBConnect();

        [WebMethod]
        public string[] GetCompletionList3LD(string prefixText, int count)
        {
            Connection = dbconnect.InitialConnect(sqlDS, Connection);
            Connection.Open();

            sqlDS.SelectCommand = "SELECT [3LD] FROM tbl_Title WHERE [3LD] LIKE N'%" + prefixText + "%'";
            DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);

            List<string> items = new List<string>(dv.Count);
            for (int i = 0; i < dv.Count; i++)
            {
                items.Add(Convert.ToString(dv.Table.Rows[i]["3LD"]));
            }

            Connection.Close();
            return items.ToArray();
        }

        [WebMethod]
        public string[] GetCompletionListResource(string prefixText, int count)
        {
            Connection = dbconnect.InitialConnect(sqlDS, Connection);
            Connection.Open();

            sqlDS.SelectCommand = "SELECT FullName FROM tbl_User WHERE FullName LIKE N'%" + prefixText + "%'";
            DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);

            List<string> items = new List<string>(dv.Count);
            for (int i = 0; i < dv.Count; i++)
            {
                items.Add(Convert.ToString(dv.Table.Rows[i]["FullName"]));
            }

            Connection.Close();
            return items.ToArray();
        }
    }
}