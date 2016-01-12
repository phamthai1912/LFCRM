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
/// Summary description for Register
/// </summary>
public class csRegister
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

    // đăng ký người dùng
    public void InsertUser(string fullname, string email, string user, string pass, string diachi, string city, string phone)
    {
        OpenConnect();
        sqlDS.InsertCommand = "insert into NguoiDung values(@HoTen,@Email,@TenDangNhap,@MatKhau,@DiaChi,@ThanhPho,@Phone,2)";
        sqlDS.InsertParameters.Add("HoTen", fullname);
        sqlDS.InsertParameters.Add("Email", email);
        sqlDS.InsertParameters.Add("TenDangNhap", user);
        sqlDS.InsertParameters.Add("MatKhau", pass);
        sqlDS.InsertParameters.Add("DiaChi", diachi);
        sqlDS.InsertParameters.Add("ThanhPho", city);
        sqlDS.InsertParameters.Add("Phone", phone);
        sqlDS.Insert();
        CloseConnect();
    }

    //
    public bool CheckUser(string user)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM NguoiDung WHERE TenDangNhap=@user ";
        sqlDS.SelectParameters.Add("user", user);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = false;
        else ok = true;

        CloseConnect();
        return ok;
    }

    //
    public bool CheckEmail(string email)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM NguoiDung WHERE Email=@email ";
        sqlDS.SelectParameters.Add("email", email);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = false;
        else ok = true;

        CloseConnect();
        return ok;
    }
}
