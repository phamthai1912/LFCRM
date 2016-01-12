using System;
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

/// <summary>
/// Summary description for Login
/// </summary>
public class csLogin
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;

	public csLogin()
	{
		//
		// TODO: Add constructor logic here
		//
	}

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

    public bool CheckLogin(string user, string pass)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM NguoiDung WHERE TenDangNhap=@user AND MatKhau=@pass";
        sqlDS.SelectParameters.Add("user", user);
        sqlDS.SelectParameters.Add("pass", pass);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;
        else ok = false;

        CloseConnect();
        return ok;
    }

    public string ShowID(string user)
    {
        OpenConnect();
        string userid = "";

        sqlDS.SelectCommand = "SELECT ID_NguoiDung FROM NguoiDung WHERE TenDangNhap=@user";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) userid = Convert.ToString(dv.Table.Rows[0]["ID_NguoiDung"]);

        CloseConnect();
        return userid;
    }

    public string ShowFullName(string user)
    {
        OpenConnect();
        string fullname = "";

        sqlDS.SelectCommand = "SELECT HoTen FROM NguoiDung WHERE TenDangNhap=@user";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) fullname = Convert.ToString(dv.Table.Rows[0]["HoTen"]);

        CloseConnect();
        return fullname;
    }

    public string ShowLevel(string user)
    {
        OpenConnect();
        string level = "";

        sqlDS.SelectCommand = "SELECT QuyenHan FROM NguoiDung, PhanQuyen WHERE TenDangNhap=@user AND NguoiDung.ID_Quyen = PhanQuyen.ID_Quyen";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) level = Convert.ToString(dv.Table.Rows[0]["QuyenHan"]);

        CloseConnect();
        return level;
    }
}
