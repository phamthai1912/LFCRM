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
/// Summary description for csImport
/// </summary>
public class csImport
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;

	public csImport()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void OpenConnect()
    {
        sqlDS.ConnectionString = ConfigurationManager.ConnectionStrings["TSKN"].ConnectionString;
        ketnoi = new SqlConnection(sqlDS.ConnectionString);
        ketnoi.Open();
    }

    public void CloseConnect()
    {
        ketnoi.Close();
    }

    public int ShowIdPNMax()
    {
        OpenConnect();

        int IdPNMax = 0;
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT Id_PhieuNhap FROM PhieuNhap ORDER BY Id_PhieuNhap DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) IdPNMax = Convert.ToInt32(dv.Table.Rows[0]["Id_PhieuNhap"]);

        CloseConnect();
        return IdPNMax;
    }

    public void ImportProduct(string idphieunhap, string idnhacungcap, string idnguoidung, string ngaynhap, string idmathang, string iddonvitinh, string soluong, string dongia, string notes)
    {
        OpenConnect();

        sqlDS.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
        sqlDS.InsertCommand = "ImportProduct";
        sqlDS.InsertParameters.Add("idphieunhap",TypeCode.Int32, idphieunhap);
        sqlDS.InsertParameters.Add("idnhacungcap", TypeCode.Int32, idnhacungcap);
        sqlDS.InsertParameters.Add("idnguoidung", TypeCode.Int32, idnguoidung);
        sqlDS.InsertParameters.Add("ngaynhap",  ngaynhap);
        sqlDS.InsertParameters.Add("idmathang", TypeCode.Int32, idmathang);
        sqlDS.InsertParameters.Add("iddonvitinh", TypeCode.Int32, iddonvitinh);
        sqlDS.InsertParameters.Add("soluong", TypeCode.Int32, soluong);
        sqlDS.InsertParameters.Add("dongia", TypeCode.Int32, dongia);
        sqlDS.InsertParameters.Add("notes", notes);
        sqlDS.Insert();

        CloseConnect();
    }

    public void ImportProductMore(string idphieunhap, string idmathang, string iddonvitinh, string soluong, string dongia, string notes)
    {
        OpenConnect();

        sqlDS.InsertCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "INSERT INTO ChiTietPhieuNhap(Id_PhieuNhap, Id_Mathang, Id_DonViTinh, SoLuong, DonGia, GhiChu) VALUES (@idphieunhap, @idmathang, @iddonvitinh, @soluong, @dongia, @notes)";
        sqlDS.InsertParameters.Add("idphieunhap", TypeCode.Int32, idphieunhap);
        sqlDS.InsertParameters.Add("idmathang", TypeCode.Int32, idmathang);
        sqlDS.InsertParameters.Add("iddonvitinh", TypeCode.Int32, iddonvitinh);
        sqlDS.InsertParameters.Add("soluong", TypeCode.Int32, soluong);
        sqlDS.InsertParameters.Add("dongia", TypeCode.Int32, dongia);
        sqlDS.InsertParameters.Add("notes", notes);
        sqlDS.Insert();

        CloseConnect();
    }

    public int GetQuantityInImportDetail(string idphieunhap, string idproduct)
    {
        OpenConnect();

        int quantity = 0;
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT SoLuong FROM ChiTietPhieuNhap WHERE id_phieunhap=" + idphieunhap + " AND id_mathang=" + idproduct;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) quantity = Convert.ToInt32(dv.Table.Rows[0]["SoLuong"]);

        CloseConnect();
        return quantity;
    }

    public DataTable SelectImportDetail(string idphieunhap)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT MatHang.TenMatHang, DonViTinh, ChiTietPhieuNhap.SoLuong, ChiTietPhieuNhap.DonGia, ChiTietPhieuNhap.GhiChu FROM MatHang, ChiTietPhieuNhap, DonViTinh WHERE DonViTinh.Id_DonViTinh = ChiTietPhieuNhap.Id_DonViTinh AND MatHang.Id_MatHang = ChiTietPhieuNhap.Id_MatHang AND ChiTietPhieuNhap.Id_PhieuNhap =" + idphieunhap;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public void DeleteImportDetail(string idproduct, string idphieunhap)
    {
        OpenConnect();

        sqlDS.DeleteCommandType = SqlDataSourceCommandType.Text;
        sqlDS.DeleteCommand = "Delete From ChiTietPhieuNhap Where ID_MatHang=@idproduct AND Id_PhieuNhap = @idphieunhap";
        sqlDS.DeleteParameters.Add("idproduct", idproduct);
        sqlDS.DeleteParameters.Add("idphieunhap", idphieunhap);
        sqlDS.Delete();

        CloseConnect();
    }

    public bool CheckImportProductDulicate(string idphieunhap, string idproduct)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM ChiTietPhieuNhap WHERE Id_PhieuNhap="+idphieunhap+" AND Id_MatHang="+idproduct;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public void UpdateImportProductDetail(string idphieunhap, string idproduct, string iddonvitinh, string quantity, string unitprice, string notes)
    {
        OpenConnect();
        sqlDS.UpdateCommand = "Update ChiTietPhieuNhap SET Id_DonViTinh=@iddonvitinh, SoLuong=@quantity, DonGia=@unitprice, GhiChu =@notes WHERE ID_MatHang=@idproduct AND Id_PhieuNhap=@idphieunhap";
        sqlDS.UpdateParameters.Add("quantity", quantity);
        sqlDS.UpdateParameters.Add("unitprice", unitprice);
        sqlDS.UpdateParameters.Add("notes", notes);
        sqlDS.UpdateParameters.Add("idproduct", idproduct);
        sqlDS.UpdateParameters.Add("iddonvitinh", iddonvitinh);
        sqlDS.UpdateParameters.Add("idphieunhap", idphieunhap);
        sqlDS.Update();
        CloseConnect();
    }

    public void DeleteAllImportDetail(string idphieunhap)
    {
        OpenConnect();

        sqlDS.DeleteCommandType = SqlDataSourceCommandType.Text;
        sqlDS.DeleteCommand = "Delete From ChiTietPhieuNhap Where Id_PhieuNhap = "+idphieunhap;
        sqlDS.Delete();

        CloseConnect();
    }

    public DataTable ShowImportInformation(string idphieunhap)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT PhieuNhap.Id_PhieuNhap, TenNhaCungCap, HoTen, NgayNhap, NhaCungCap.DiaChi, NhaCungCap.Phone, TenMatHang, ChiTietPhieuNhap.SoLuong, ChiTietPhieuNhap.DonGia, ChiTietPhieuNhap.Id_MatHang, ChiTietPhieuNhap.GhiChu, DonViTinh "+
                                "FROM PhieuNhap, NguoiDung, Nhacungcap, ChiTietPhieuNhap, MatHang, DonViTinh "+
                                "WHERE PhieuNhap.Id_NhaCungCap = NhaCungCap.Id_NhaCungCap "+
                                "AND ChiTietPhieuNhap.Id_DonViTinh = DonViTinh.Id_DonViTinh " +
                                "AND NguoiDung.Id_NguoiDung = PhieuNhap.Id_NguoiDung "+
                                "AND ChiTietPhieuNhap.Id_PhieuNhap = PhieuNhap.Id_PhieuNhap "+
                                "AND MatHang.Id_MatHang = ChiTietPhieuNhap.Id_MatHang "+
                                "AND PhieuNhap.Id_PhieuNhap ="+idphieunhap;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public bool CheckProductHasInImport(string idproduct)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM ChiTietPhieuNhap WHERE Id_MatHang=" + idproduct;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public DataTable ShowImportByDate(string datestart, string dateend)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT Id_PhieuNhap, Hoten, TenNhaCungCap, NgayNhap FROM PhieuNhap, NhaCungCap, NguoiDung "+
                                "WHERE Phieunhap.id_nhacungcap = nhacungcap.id_nhacungcap "+
                                "AND Phieunhap.id_nguoidung = nguoidung.id_nguoidung " +
                                "AND convert(varchar, Ngaynhap, 103) BETWEEN '"+datestart+"' AND  '"+dateend+"' ORDER BY Id_PhieuNhap DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }
}
