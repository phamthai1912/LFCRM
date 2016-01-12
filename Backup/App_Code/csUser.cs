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
/// Summary description for csUser
/// </summary>
public class csUser
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;
    string chuoiketnoi = ConfigurationManager.ConnectionStrings["TSKN"].ConnectionString;

    public void OpenConnect()
    {
        sqlDS.ConnectionString = chuoiketnoi;
        ketnoi = new SqlConnection(sqlDS.ConnectionString);
        ketnoi.Open();
    }

    public void CloseConnect()
    {
        ketnoi.Close();
    }

    public DataTable ShowUserInformation(string iduser)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT * FROM NguoiDung WHERE ID_NguoiDung =" + iduser + "";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public bool CheckOldPass(string iduser, string pass)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT MatKhau FROM NguoiDung WHERE MatKhau=@pass AND ID_NguoiDung=@iduser ";
        sqlDS.SelectParameters.Add("iduser", iduser);
        sqlDS.SelectParameters.Add("pass", pass);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;
        else ok = false;

        CloseConnect();
        return ok;
    }

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

    public bool CheckUserHasInImport(string id)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM PhieuNhap WHERE Id_NguoiDung=" + id;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public bool CheckUserHasInExport(string id)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM PhieuXuat WHERE Id_NguoiDung=" + id;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public bool CheckUserHasInOrder(string id)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM DonHang WHERE Id_NguoiDung=" + id;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public bool CheckUserBeforeDel(string id)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * "+
                                "FROM Thongbao a, DonHang c, SoBaoHanh d, PhieuXuat e, PhieuNhap f "+
                                "WHERE a.id_nguoinhan ='"+id+"' "+
                                "OR a.id_nguoigoi = '" + id + "' " +
                                "OR c.id_nguoidung= '" + id + "' " +
                                "OR d.id_nguoidung = '" + id + "' " +
                                "OR e.id_nguoidung = '" + id + "' " +
                                "OR f.id_nguoidung = '" + id + "'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public string ShowPass(string user_id)
    {
        OpenConnect();

        string pass = "";
        sqlDS.SelectCommand = "SELECT * FROM NguoiDung WHERE ID_NguoiDung=@user_id";
        sqlDS.SelectParameters.Add("user_id", user_id);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) pass = Convert.ToString(dv.Table.Rows[0]["MatKhau"]);

        CloseConnect();
        return pass;
    }

    public string ShowAddress(string iduser)
    {
        OpenConnect();

        string address = "";
        sqlDS.SelectCommand = "SELECT * FROM NguoiDung WHERE ID_NguoiDung=@iduser";
        sqlDS.SelectParameters.Add("iduser", iduser);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) address = Convert.ToString(dv.Table.Rows[0]["DiaChi"]);

        CloseConnect();
        return address;
    }

    public string ShowPhone(string id_user)
    {
        OpenConnect();

        string phone = "";
        sqlDS.SelectCommand = "SELECT * FROM NguoiDung WHERE ID_NguoiDung=@id_user";
        sqlDS.SelectParameters.Add("id_user", id_user);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) phone = Convert.ToString(dv.Table.Rows[0]["Phone"]);

        CloseConnect();
        return phone;
    }

    public string ShowCity(string userid)
    {
        OpenConnect();

        string city = "";
        sqlDS.SelectCommand = "SELECT * FROM NguoiDung WHERE ID_NguoiDung=@userid";
        sqlDS.SelectParameters.Add("userid", userid);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) city = Convert.ToString(dv.Table.Rows[0]["ThanhPho"]);

        CloseConnect();
        return city;
    }

    public DataTable SelectUser()
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT ID_NguoiDung,HoTen,Email,TenDangNhap,MatKhau,DiaChi,ThanhPho,Phone,QuyenHan FROM NguoiDung,PhanQuyen WHERE NguoiDung.ID_Quyen = PhanQuyen.ID_Quyen ORDER BY ID_NguoiDung DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public void InsertUser(string fullname, string email, string user, string pass ,string address, string city, int phone, int roleid)
    {
        OpenConnect();
        sqlDS.InsertCommand = "insert into NguoiDung values(@fullname,@email,@user,@pass,@address,@city,@phone,@roleid)";
        sqlDS.InsertParameters.Add("fullname", fullname);
        sqlDS.InsertParameters.Add("email", email);
        sqlDS.InsertParameters.Add("user", user);
        sqlDS.InsertParameters.Add("pass", pass);
        sqlDS.InsertParameters.Add("address", address);
        sqlDS.InsertParameters.Add("city", city);
        sqlDS.InsertParameters.Add("phone", Convert.ToString(phone));
        sqlDS.InsertParameters.Add("roleid", Convert.ToString(roleid));
        sqlDS.Insert();
        CloseConnect();
    }

    public void UpdateUser(int userid, string fullname, string address, string city, string phone, int roleid)
    {
        OpenConnect();
        sqlDS.UpdateCommand = "Update NguoiDung SET HoTen=@fullname, DiaChi=@address, ThanhPho=@city, Phone=@phone ,ID_Quyen=@roleid  WHERE ID_NguoiDung=@userid";
        sqlDS.UpdateParameters.Add("userid", Convert.ToString(userid));
        sqlDS.UpdateParameters.Add("fullname", fullname);
        sqlDS.UpdateParameters.Add("address", address);
        sqlDS.UpdateParameters.Add("city", city);
        sqlDS.UpdateParameters.Add("phone", phone);
        sqlDS.UpdateParameters.Add("roleid", Convert.ToString(roleid));
        sqlDS.Update();
        CloseConnect();
    }

    public void UpdatePass(string userid, string pass)
    {
        OpenConnect();
        sqlDS.UpdateCommand = "Update NguoiDung SET MatKhau=@pass WHERE ID_NguoiDung=@userid";
        sqlDS.UpdateParameters.Add("userid", userid);
        sqlDS.UpdateParameters.Add("pass", pass);

        sqlDS.Update();
        CloseConnect();
    }

    public void UpdateOneUser(string userid, string fullname, string address, string city, string phone)
    {
        OpenConnect();
        sqlDS.UpdateCommand = "Update NguoiDung SET HoTen=@fullname,DiaChi=@address,ThanhPho=@city,Phone=@phone WHERE ID_NguoiDung=@userid";
        sqlDS.UpdateParameters.Add("userid", userid);
        sqlDS.UpdateParameters.Add("fullname", fullname);
        sqlDS.UpdateParameters.Add("address", address);
        sqlDS.UpdateParameters.Add("city", city);
        sqlDS.UpdateParameters.Add("phone", phone);
        sqlDS.Update();
        CloseConnect();
    }

    public void DeleteUser(string userid)
    {
        OpenConnect();
        sqlDS.DeleteCommandType = SqlDataSourceCommandType.StoredProcedure;
        sqlDS.DeleteCommand = "DeleteUser";
        sqlDS.DeleteParameters.Add("userid", userid);
        sqlDS.Delete();
        CloseConnect();
    }

    public string UserNameToUserId(string username)
    {
        OpenConnect();

        string userid = "";
        sqlDS.SelectCommand = "SELECT id_nguoidung FROM nguoidung WHERE Tendangnhap ='" + username + "'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) userid = Convert.ToString(dv.Table.Rows[0]["Id_nguoidung"]);

        CloseConnect();
        return userid;
    }
}
