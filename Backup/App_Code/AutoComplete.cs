using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : WebService
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;

    public void OpenConnect()
    {
        sqlDS.ConnectionString = ConfigurationManager.ConnectionStrings["TSKN"].ConnectionString;
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        ketnoi = new SqlConnection(sqlDS.ConnectionString);
        ketnoi.Open();
    }

    public void CloseConnect()
    {
        ketnoi.Close();
    }

    public AutoComplete()
    {
    
    }

    [WebMethod]
    public string[] GetCompletionListProvider(string prefixText)
    {
        OpenConnect();

        sqlDS.SelectCommand = "SELECT TenNhaCungCap FROM NhaCungCap WHERE TenNhaCungCap LIKE N'%" + prefixText + "%'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);

        List<string> items = new List<string>(dv.Count);
        for (int i = 0; i < dv.Count; i++)
        {
            items.Add(Convert.ToString(dv.Table.Rows[i]["TenNhaCungCap"]));
        }

        CloseConnect();
        return items.ToArray();
    }

    [WebMethod]
    public string[] GetCompletionListProduct(string prefixText)
    {
        OpenConnect();

        sqlDS.SelectCommand = "SELECT TenMatHang FROM MatHang WHERE TenMatHang LIKE N'%" + prefixText + "%'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);

        List<string> items = new List<string>(dv.Count);
        for (int i = 0; i < dv.Count; i++)
        {
            items.Add(Convert.ToString(dv.Table.Rows[i]["TenMatHang"]));
        }

        CloseConnect();
        return items.ToArray();
    }

    [WebMethod]
    public string[] GetCompletionListSerial(string prefixText)
    {
        OpenConnect();

        sqlDS.SelectCommand = "SELECT Serial FROM SoBaoHanh WHERE Serial LIKE N'%" + prefixText + "%'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);

        List<string> items = new List<string>(dv.Count);
        for (int i = 0; i < dv.Count; i++)
        {
            items.Add(Convert.ToString(dv.Table.Rows[i]["Serial"]));
        }

        CloseConnect();
        return items.ToArray();
    }

    [WebMethod]
    public string[] GetCompletionListUserName(string prefixText)
    {
        OpenConnect();

        sqlDS.SelectCommand = "SELECT * FROM NguoiDung WHERE TenDangNhap LIKE N'%" + prefixText + "%'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);

        List<string> items = new List<string>(dv.Count);
        for (int i = 0; i < dv.Count; i++)
        {
            items.Add(Convert.ToString(dv.Table.Rows[i]["TenDangNhap"]));
        }

        CloseConnect();
        return items.ToArray();
    }
}

