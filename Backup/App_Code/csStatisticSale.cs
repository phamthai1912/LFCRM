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
/// Summary description for csStatisticSale
/// </summary>
public class csStatisticSale
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;

	public csStatisticSale()
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

    public DataTable CatalogHasInOrderDetailByDate(string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT DISTINCT c.Loaihang " +
                                "FROM Chitietdonhang a, Mathang b, Loaihang c, Donhang d " +
                                "WHERE a.id_mathang = b.id_mathang " +
                                "AND b.id_loaihang = c.id_loaihang " +
                                "AND d.id_donhang = a.id_donhang " +
                                "AND  d.tinhtrang = 1 " +
                                "AND convert(varchar,NgayNhan,103) BETWEEN '" + startdate + "' AND '" + enddate + "'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable ProductHasInOrderDetailByDateAndByCatalog(string catalogname, string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT DISTINCT Tenmathang  " +
                                "FROM Chitietdonhang, Mathang, Donhang, LoaiHang " +
                                "WHERE Mathang.id_mathang = Chitietdonhang.id_mathang " +
                                "AND Mathang.id_loaihang = Loaihang.id_loaihang " +
                                "AND Donhang.id_donhang = Chitietdonhang.id_donhang " +
                                "AND Donhang.Tinhtrang = 1 " +
                                "AND convert(varchar,NgayNhan,103) BETWEEN '" + startdate + "' AND '" + enddate + "' " +
                                "AND Loaihang ='" + catalogname + "'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable SelectStatisticsSalesByDateCatalogProduct(string catalogname, string productname, string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT Mathang.id_mathang,Loaihang,Tenmathang,Chitietdonhang.Soluong,Chitietdonhang.Dongia,convert(varchar,NgayNhan,103) as NgayNhan " +
                                "FROM Chitietdonhang, Mathang, Loaihang, Donhang " +
                                "WHERE Mathang.id_mathang = Chitietdonhang.id_mathang " +
                                "AND Mathang.id_loaihang = Loaihang.id_loaihang " +
                                "AND Donhang.id_donhang = Chitietdonhang.id_donhang " +
                                "AND Donhang.Tinhtrang = 1 " +
                                "AND convert(varchar,NgayNhan,103) BETWEEN '" + startdate + "' AND '" + enddate + "' " +
                                "AND Loaihang ='" + catalogname + "' " +
                                "AND Tenmathang ='" + productname + "' " +
                                "GROUP BY loaihang, tenmathang, Mathang.id_mathang, Chitietdonhang.Soluong, Ngaynhan , Chitietdonhang.Dongia";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }
}
