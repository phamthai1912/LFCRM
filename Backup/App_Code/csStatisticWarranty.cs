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
/// Summary description for csStatisticWarranty
/// </summary>
public class csStatisticWarranty
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;

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

	public csStatisticWarranty()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable StatisticWarranty(string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "select ID_PhieuBaoHanh,TenMatHang,Serial,convert(varchar, NgayNhan, 103) as NgayNhan,convert(varchar, NgayTra, 103) as NgayTra,GhiChu FROM PhieuNhanBaoHanh,SoBaoHanh,MatHang WHERE PhieuNhanBaoHanh.ID_BaoHanh = SoBaoHanh.ID_BaoHanh AND SoBaoHanh.ID_MatHang = MatHang.ID_MatHang AND convert(varchar,NgayNhan,103) BETWEEN '" + startdate + "' AND '" + enddate + "' ORDER BY GhiChu ASC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }
}
