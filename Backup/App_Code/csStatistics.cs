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
/// Summary description for csStatistics
/// </summary>
public class csStatistics
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;

	public csStatistics()
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

    public DataTable CatalogHasInImportDetailByDate(string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT DISTINCT c.Loaihang "+
                                "FROM Chitietphieunhap a, Mathang b, Loaihang c, Phieunhap d "+
                                "WHERE a.id_mathang = b.id_mathang "+
                                "AND b.id_loaihang = c.id_loaihang "+
                                "AND d.id_phieunhap = a.id_phieunhap " +
                                "AND convert(varchar,NgayNhap,103) BETWEEN '"+startdate+"' AND '"+enddate+"'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable ProductHasInImportDetailByDateAndByCatalog(string catalogname, string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT DISTINCT Tenmathang "+
                                "FROM Chitietphieunhap, Mathang, PhieuNhap, LoaiHang "+
                                "WHERE Mathang.id_mathang = Chitietphieunhap.id_mathang "+
                                "AND Mathang.id_loaihang = Loaihang.id_loaihang "+
                                "AND Phieunhap.id_phieunhap = Chitietphieunhap.id_phieunhap "+
                                "AND convert(varchar,NgayNhap,103) BETWEEN '"+startdate+"' AND '"+enddate+"' "+
                                "AND Loaihang ='"+ catalogname +"'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable SelectStatisticsImportByDateCatalogProduct(string catalogname, string productname, string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT Mathang.id_mathang, Loaihang, Tenmathang, Chitietphieunhap.Soluong, Donvitinh, Chitietphieunhap.Dongia, Nhacungcap.Tennhacungcap, NgayNhap, Ghichu "+
                                "FROM Chitietphieunhap, Mathang, Loaihang, Phieunhap, Nhacungcap, Donvitinh "+
                                "WHERE Mathang.id_mathang = Chitietphieunhap.id_mathang "+
                                "AND Mathang.id_loaihang = Loaihang.id_loaihang "+
                                "AND Chitietphieunhap.id_donvitinh = Donvitinh.id_donvitinh " +
                                "AND Phieunhap.id_phieunhap = Chitietphieunhap.id_phieunhap "+
                                "AND Phieunhap.id_nhacungcap = Nhacungcap.id_nhacungcap "+
                                "AND convert(varchar,NgayNhap,103) BETWEEN '"+startdate+"' AND '"+enddate+"' "+
                                "AND Loaihang ='"+catalogname+"' " +
                                "AND Tenmathang ='"+productname+"' " +
                                "GROUP BY loaihang, tenmathang, Mathang.id_mathang, Chitietphieunhap.Soluong, Ngaynhap, Ghichu, Nhacungcap.Tennhacungcap, Chitietphieunhap.Dongia, Donvitinh.Donvitinh";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable CatalogHasInExportDetailByDate(string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT DISTINCT c.Loaihang " +
                                "FROM Chitietphieuxuat a, Mathang b, Loaihang c, Phieuxuat d " +
                                "WHERE a.id_mathang = b.id_mathang " +
                                "AND b.id_loaihang = c.id_loaihang " +
                                "AND d.id_phieuxuat= a.id_phieuxuat " +
                                "AND convert(varchar,Ngayxuat,103) BETWEEN '" + startdate + "' AND '" + enddate + "'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable ProductHasInExportDetailByDateAndByCatalog(string catalogname, string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT DISTINCT Tenmathang " +
                                "FROM Chitietphieuxuat, Mathang, Phieuxuat, LoaiHang " +
                                "WHERE Mathang.id_mathang = Chitietphieuxuat.id_mathang " +
                                "AND Mathang.id_loaihang = Loaihang.id_loaihang " +
                                "AND Phieuxuat.id_phieuxuat = Chitietphieuxuat.id_phieuxuat " +
                                "AND convert(varchar,Ngayxuat,103) BETWEEN '" + startdate + "' AND '" + enddate + "' " +
                                "AND Loaihang ='" + catalogname + "'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable SelectStatisticsExportByDateCatalogProduct(string catalogname, string productname, string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT Mathang.id_mathang, Loaihang, Tenmathang, Chitietphieuxuat.Soluong, Donvitinh, Ngayxuat, Ghichu " +
                                "FROM Chitietphieuxuat, Mathang, Loaihang, Phieuxuat, Donvitinh " +
                                "WHERE Mathang.id_mathang = Chitietphieuxuat.id_mathang " +
                                "AND Mathang.id_loaihang = Loaihang.id_loaihang " +
                                "AND Chitietphieuxuat.id_donvitinh = Donvitinh.id_Donvitinh " +
                                "AND Phieuxuat.id_phieuxuat = Chitietphieuxuat.id_phieuxuat " +
                                "AND convert(varchar,Ngayxuat,103) BETWEEN '" + startdate + "' AND '" + enddate + "' " +
                                "AND Loaihang ='" + catalogname + "' " +
                                "AND Tenmathang ='" + productname + "' " +
                                "GROUP BY loaihang, tenmathang, Mathang.id_mathang, Chitietphieuxuat.Soluong, Ngayxuat, Ghichu, Donvitinh.Donvitinh";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable SelectMMYYYYInImport()
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT DISTINCT right(convert(varchar, Ngaynhap, 103),7) as NgayNhap FROM PhieuNhap";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable SelectMMYYYYInExport()
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT DISTINCT right(convert(varchar, Ngayxuat, 103),7) as NgayXuat FROM Phieuxuat";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable SelectIdLoaiHang()
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT DISTINCT a.id_loaihang, b.loaihang FROM mathang a, loaihang b WHERE a.id_loaihang = b.id_loaihang";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable SelectIdProductInCatalog(string idloaihang)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT Id_mathang "+
                                "FROM Mathang a, Loaihang b "+
                                "WHERE a.id_loaihang = b.id_loaihang " +
                                "AND b.id_loaihang ="+idloaihang;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public bool CheckCatalogHasInImportInMonth(string id_loaihang, string month)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * "+
                                "FROM Chitietphieunhap a, Phieunhap b, mathang c, loaihang d "+
                                "WHERE a.id_phieunhap = b.id_phieunhap "+
                                "AND a.id_mathang = c.id_mathang "+
                                "AND c.id_loaihang= d.id_loaihang "+
                                "AND right(convert(varchar, Ngaynhap, 103),7) = '" + month + "'" +
                                "AND d.id_loaihang = " + id_loaihang;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;
        else ok = false;

        CloseConnect();
        return ok;
    }

    public bool CheckCatalogHasInExportInMonth(string id_loaihang, string month)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * "+
                                "FROM Chitietphieuxuat a, Phieuxuat b, mathang c, loaihang d "+
                                "WHERE a.id_phieuxuat = b.id_phieuxuat "+
                                "AND a.id_mathang = c.id_mathang "+
                                "AND c.id_loaihang= d.id_loaihang "+
                                "AND right(convert(varchar, Ngayxuat, 103),7) = '"+month+"'"+
                                "AND d.id_loaihang = " + id_loaihang;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;
        else ok = false;

        CloseConnect();
        return ok;
    }

    public bool CheckProductHasInImportInMonth(string id_mathang, string month)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT DISTINCT id_mathang " +
                                "FROM chitietphieunhap b, phieunhap d " +
                                "WHERE d.id_phieunhap = b.id_phieunhap " +
                                "AND right(convert(varchar, Ngaynhap, 103),7) = '"+month+"' " +
                                "AND id_mathang = "+id_mathang;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;
        else ok = false;

        CloseConnect();
        return ok;
    }

    public bool CheckProductHasInExportInMonth(string id_mathang, string month)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT DISTINCT id_mathang " +
                                "FROM chitietphieuxuat b, phieuxuat d " +
                                "WHERE d.id_phieuxuat = b.id_phieuxuat " +
                                "AND right(convert(varchar, Ngayxuat, 103),7) = '"+month+"' " +
                                "AND id_mathang = " + id_mathang;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;
        else ok = false;

        CloseConnect();
        return ok;
    }

    public DataTable InfoOfProductInImportMonth(string id_mathang, string month)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT a.id_mathang, Tenmathang, Donvitinh, SUM(b.Soluong) as SoLuong " +
                                "FROM mathang a, chitietphieunhap b, loaihang c, phieunhap d , donvitinh e " +
                                "WHERE a.id_loaihang = c.id_loaihang "+
                                "AND b.id_mathang = a.id_mathang "+
                                "AND d.id_phieunhap = b.id_phieunhap "+
                                "AND e.id_donvitinh = b.id_donvitinh " +
                                "AND b.id_mathang = "+id_mathang +
                                "AND right(convert(varchar, Ngaynhap, 103),7) = '"+month+"' " +
                                "GROUP BY Tenmathang,a.id_mathang, donvitinh ";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable InfoOfProductInExportMonth(string id_mathang, string month)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT a.id_mathang, Tenmathang, Donvitinh, SUM(b.Soluong) as SoLuong " +
                                "FROM mathang a, chitietphieuxuat b, loaihang c, phieuxuat d , donvitinh e " +
                                "WHERE a.id_loaihang = c.id_loaihang " +
                                "AND b.id_mathang = a.id_mathang " +
                                "AND d.id_phieuxuat = b.id_phieuxuat " +
                                "AND e.id_donvitinh = b.id_donvitinh " +
                                "AND b.id_mathang = " + id_mathang +
                                "AND right(convert(varchar, Ngayxuat, 103),7) = '" + month + "' " +
                                "GROUP BY Tenmathang, a.id_mathang, donvitinh ";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public int GetSumQuantityInExport(string idproduct, string month)
    {
        OpenConnect();
        int sum = 0;

        sqlDS.SelectCommand = "SELECT a.id_mathang, SUM(b.Soluong) as SoLuong " +
                                "FROM mathang a, chitietphieuxuat b, phieuxuat d " +
                                "WHERE b.id_mathang = a.id_mathang  " +
                                "AND d.id_phieuxuat = b.id_phieuxuat " +
                                "AND right(convert(varchar, Ngayxuat, 103),7) < '" + month + "' " +
                                "AND a.id_mathang = " + idproduct +
                                "GROUP BY Tenmathang, a.id_mathang";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) sum = Convert.ToInt32(dv.Table.Rows[0]["SoLuong"]);

        CloseConnect();
        return sum;
    }

    public int GetQuantityStartInventory(string idproduct, string month)
    {
        OpenConnect();
        int Tondau = 0;

        sqlDS.SelectCommand = "SELECT a.id_mathang, SUM(b.Soluong) as SoLuong "+
                                "FROM mathang a, chitietphieunhap b, phieunhap d "+
                                "WHERE b.id_mathang = a.id_mathang  "+
                                "AND d.id_phieunhap = b.id_phieunhap  "+
                                "AND right(convert(varchar, Ngaynhap, 103),7) < '"+month+"' "+
                                "AND a.id_mathang ="+ idproduct +
                                "GROUP BY Tenmathang, a.id_mathang";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) Tondau = Convert.ToInt32(dv.Table.Rows[0]["SoLuong"]);

        Tondau = Tondau - GetSumQuantityInExport(idproduct, month);

        CloseConnect();
        return Tondau;
    }

    public int GetLatestUnitPriceInImport(string idproduct, string month)
    {
        OpenConnect();
        int Dongia = 0;

        sqlDS.SelectCommand = "SELECT a.id_phieunhap, dongia "+
                                "FROM Chitietphieunhap a, Phieunhap b "+
                                "WHERE id_mathang="+ idproduct+
                                "AND a.id_phieunhap = b.id_phieunhap "+
                                "AND right(convert(varchar, Ngaynhap, 103),7) = '"+month+"' " +
                                "ORDER BY a.id_phieunhap DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) Dongia = Convert.ToInt32(dv.Table.Rows[0]["DonGia"]);

        CloseConnect();
        return Dongia;
    }
}
