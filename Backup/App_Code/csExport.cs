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
/// Summary description for Export
/// </summary>
public class csExport
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;

	public csExport()
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

    public int ShowIdPXMax()
    {
        OpenConnect();

        int IdPXMax = 0;
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT Id_PhieuXuat FROM PhieuXuat ORDER BY Id_PhieuXuat DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) IdPXMax = Convert.ToInt32(dv.Table.Rows[0]["Id_PhieuXuat"]);

        CloseConnect();
        return IdPXMax;
    }

    public void ExportProduct(string idphieuxuat, string idnguoidung, string ngayxuat, string idproduct, string iddonvitinh, string quantity, string notes)
    {
        sqlDS.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
        sqlDS.InsertCommand = "ExportProduct";
        sqlDS.InsertParameters.Add("idphieuxuat", TypeCode.Int32, idphieuxuat);
        sqlDS.InsertParameters.Add("idnguoidung", TypeCode.Int32, idnguoidung);
        sqlDS.InsertParameters.Add("ngayxuat", ngayxuat);
        sqlDS.InsertParameters.Add("idproduct", TypeCode.Int32, idproduct);
        sqlDS.InsertParameters.Add("iddonvitinh", TypeCode.Int32, iddonvitinh);
        sqlDS.InsertParameters.Add("quantity", TypeCode.Int32, quantity);
        sqlDS.InsertParameters.Add("notes", notes);
        sqlDS.Insert();
    }

    public DataTable SelectExportDetail(string idphieuxuat)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT MatHang.id_mathang, MatHang.TenMatHang, DonViTinh, ChiTietPhieuXuat.SoLuong, ChiTietPhieuXuat.GhiChu FROM MatHang, ChiTietPhieuXuat, DonViTinh WHERE DonViTinh.Id_DonViTinh = ChiTietPhieuXuat.Id_DonViTinh AND MatHang.Id_MatHang = ChiTietPhieuXuat.Id_MatHang AND ChiTietPhieuXuat.Id_PhieuXuat =" + idphieuxuat;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public void DeleteExportDetail(string idproduct, string idphieuxuat)
    {
        OpenConnect();

        sqlDS.DeleteCommandType = SqlDataSourceCommandType.Text;
        sqlDS.DeleteCommand = "Delete From ChiTietPhieuXuat Where ID_MatHang="+idproduct+" AND Id_PhieuXuat = "+idphieuxuat;
        sqlDS.Delete();

        CloseConnect();
    }

    public bool CheckExportProductDulicate(string idphieuxuat, string idproduct)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM ChiTietPhieuXuat WHERE Id_PhieuXuat=" + idphieuxuat + " AND Id_MatHang=" + idproduct;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public void ExportProductMore(string idphieuxuat, string idmathang, string iddonvitinh, string soluong, string notes)
    {
        OpenConnect();

        sqlDS.InsertCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "INSERT INTO ChiTietPhieuXuat(Id_PhieuXuat, Id_Mathang, Id_DonViTinh, SoLuong, GhiChu) VALUES (@idphieuxuat, @idmathang, @iddonvitinh, @soluong, @notes)";
        sqlDS.InsertParameters.Add("idphieuxuat", TypeCode.Int32, idphieuxuat);
        sqlDS.InsertParameters.Add("idmathang", TypeCode.Int32, idmathang);
        sqlDS.InsertParameters.Add("iddonvitinh", TypeCode.Int32, iddonvitinh);
        sqlDS.InsertParameters.Add("soluong", TypeCode.Int32, soluong);
        sqlDS.InsertParameters.Add("notes", notes);
        sqlDS.Insert();

        CloseConnect();
    }

    public int GetQuantityInExportDetail(string idphieuxuat, string idproduct)
    {
        OpenConnect();

        int quantity = 0;
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT SoLuong FROM ChiTietPhieuXuat WHERE id_phieuxuat="+idphieuxuat+" AND id_mathang="+idproduct;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) quantity = Convert.ToInt32(dv.Table.Rows[0]["SoLuong"]);

        CloseConnect();
        return quantity;
    }

    public void UpdateImportProductDetail(string idphieuxuat, string idproduct, string iddonvitinh, int quantity, string notes)
    {
        OpenConnect();

        sqlDS.UpdateCommand = "Update ChiTietPhieuXuat SET Id_DonViTinh=@iddonvitinh, SoLuong=@quantity, GhiChu =@notes WHERE ID_MatHang=@idproduct AND Id_PhieuXuat=@idphieuxuat";
        sqlDS.UpdateParameters.Add("quantity",Convert.ToString(quantity));
        sqlDS.UpdateParameters.Add("notes", notes);
        sqlDS.UpdateParameters.Add("idproduct", idproduct);
        sqlDS.UpdateParameters.Add("iddonvitinh", iddonvitinh);
        sqlDS.UpdateParameters.Add("idphieuxuat", idphieuxuat);
        sqlDS.Update();

        CloseConnect();
    }

    public void DeleteAllExportDetail(string idphieuxuat)
    {
        OpenConnect();

        sqlDS.DeleteCommandType = SqlDataSourceCommandType.Text;
        sqlDS.DeleteCommand = "Delete From ChiTietPhieuXuat Where Id_PhieuXuat = " + idphieuxuat;
        sqlDS.Delete();

        CloseConnect();
    }

    public DataTable ShowExportInformation(string idphieuxuat)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT PhieuXuat.Id_PhieuXuat, HoTen, NgayXuat, TenMatHang, ChiTietPhieuXuat.SoLuong, ChiTietPhieuXuat.Id_MatHang, ChiTietPhieuXuat.GhiChu, nhasanxuat, loaihang, DonViTinh " +
                                "FROM PhieuXuat, NguoiDung, ChiTietPhieuXuat, MatHang, LoaiHang, NhaSanXuat, DonViTinh " +
	                            "WHERE NguoiDung.Id_NguoiDung = PhieuXuat.Id_NguoiDung "+
	                            "AND Loaihang.id_loaihang = mathang.id_loaihang "+
                                "AND ChiTietPhieuXuat.Id_DonViTinh = DonViTinh.Id_DonViTinh " +
	                            "AND mathang.id_nhasanxuat = nhasanxuat.id_nhasanxuat "+
	                            "AND ChiTietPhieuXuat.Id_PhieuXuat = PhieuXuat.Id_PhieuXuat "+
                                "AND MatHang.Id_MatHang = ChiTietPhieuXuat.Id_MatHang " +
	                            "AND PhieuXuat.Id_PhieuXuat =" + idphieuxuat;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable ShowExportByDate(string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT Id_PhieuXuat, Hoten,  NgayXuat FROM PhieuXuat,NguoiDung " +
                                "WHERE Phieuxuat.id_nguoidung = nguoidung.id_nguoidung " +
                                "AND convert(varchar, Ngayxuat, 103) BETWEEN '"+startdate+"' AND  '"+enddate+"' ORDER BY Id_Phieuxuat DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }
}
