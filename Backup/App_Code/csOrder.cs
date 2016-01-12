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
/// Summary description for csCheckout
/// </summary>
public class csOrder
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

    public int ShowIdDHMax()
    {
        OpenConnect();

        int IdDHMax = 0;
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT ID_DonHang FROM DonHang ORDER BY ID_DonHang DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) IdDHMax = Convert.ToInt32(dv.Table.Rows[0]["ID_DonHang"]);

        CloseConnect();
        return IdDHMax;
    }

    public bool CheckDeleteOrder(string id)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM DonHang WHERE TinhTrang=1 AND Id_DonHang=" + id;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public DataTable SelectAllOrders(string startdate, string enddate)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT ID_DonHang, HoTen, convert(varchar, NgayDatHang, 103) as NgayDatHang, convert(varchar, NgayNhan, 103) as NgayNhan,DiaChiGiaoHang,DonHang.ThanhPho,TinhTrang FROM NguoiDung,DonHang WHERE NguoiDung.ID_NguoiDung = DonHang.ID_NguoiDung AND convert(varchar,NgayDatHang,103)  BETWEEN '" + startdate + "' AND '" + enddate + "' ORDER BY ID_DonHang DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable ShowOrderInformation(string iddonhang)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT DonHang.ID_DonHang,ChiTietDonHang.ID_MatHang,TenMatHang,ChiTietDonHang.SoLuong,ChiTietDonHang.DonGia,convert(varchar, NgayDatHang, 103) as NgayDatHang,DiaChiGiaoHang,DonHang.ThanhPho,HoTen,Phone FROM DonHang,ChiTietDonHang,MatHang,NguoiDung WHERE DonHang.ID_DonHang = ChiTietDonHang.ID_DonHang AND ChiTietDonHang.ID_MatHang = MatHang.ID_MatHang AND DonHang.ID_NguoiDung = NguoiDung.ID_NguoiDung AND DonHang.ID_DonHang =" + iddonhang + "";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    //public DataTable ShowBillInformation(string iddonhang)
    //{
    //    OpenConnect();

    //    DataTable dt = new DataTable();
    //    sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
    //    sqlDS.SelectCommand = "SELECT DonHang.ID_DonHang,ChiTietDonHang.ID_MatHang,TenMatHang,ChiTietDonHang.SoLuong,MatHang.DonGia, Serial FROM DonHang,ChiTietDonHang,MatHang,SoBaoHanh WHERE DonHang.ID_DonHang = ChiTietDonHang.ID_DonHang AND ChiTietDonHang.ID_MatHang = MatHang.ID_MatHang AND ChiTietDonHang.ID_MatHang = SoBaoHanh.ID_MatHang AND DonHang.ID_DonHang =" + iddonhang + "";
    //    DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
    //    dt = dv.ToTable();

    //    CloseConnect();
    //    return dt;
    //}


    public void SendOrder(string from, string content, string date)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "insert into ThongBao(ID_NguoiGoi,ID_NguoiNhan,TieuDe,NoiDung,NgayGoi,TrangThai) values(@from,12,N'Đơn đặt hàng',@content,@date,'new.gif')";
        sqlDS.InsertParameters.Add("from", from);
        sqlDS.InsertParameters.Add("content", content);
        sqlDS.InsertParameters.Add("date", date);
        sqlDS.Insert();
        CloseConnect();
    }

    public void SendExport(string from , string content , string date)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "insert into ThongBao(ID_NguoiGoi,ID_NguoiNhan,TieuDe,NoiDung,NgayGoi,TrangThai) values(@from,10,N'Yêu cầu xuất kho',@content,@date,'new.gif')";
        sqlDS.InsertParameters.Add("from", from);
        sqlDS.InsertParameters.Add("content", content);
        sqlDS.InsertParameters.Add("date", date);
        sqlDS.Insert();
        CloseConnect();
    }

    public void InsertOrder(string orderid, string userid, string orderdate, string shipaddress, string city)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "insert into DonHang(ID_DonHang,ID_NguoiDung,NgayDatHang,DiaChiGiaoHang,ThanhPho,TinhTrang) values(@orderid,@userid,@orderdate,@shipaddress,@city,0)";
        sqlDS.InsertParameters.Add("orderid", orderid);
        sqlDS.InsertParameters.Add("userid", userid);
        sqlDS.InsertParameters.Add("orderdate",orderdate);
        sqlDS.InsertParameters.Add("shipaddress", shipaddress);
        sqlDS.InsertParameters.Add("city", city);
        sqlDS.Insert();
        CloseConnect();
    }

    public void InsertOrderDetail(int orderid, int productid , int quantity , double price)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "insert into ChiTietDonHang values(" + orderid + "," + productid + "," + quantity + "," + price + ")";
        sqlDS.Insert();
        CloseConnect();
    }

    public void DeleteOrder(string orderid)
    {
        OpenConnect();
        sqlDS.DeleteCommandType = SqlDataSourceCommandType.StoredProcedure;
        sqlDS.DeleteCommand = "DeleteOrder";
        sqlDS.DeleteParameters.Add("orderid", orderid);
        sqlDS.Delete();
        CloseConnect();
    }

    public void DeleteOrderDetail(int orderid, int productid)
    {
        OpenConnect();
        sqlDS.DeleteCommand = "DELETE FROM ChiTietDonHang WHERE ID_MatHang=@productid AND ID_DonHang=@orderid ";
        sqlDS.DeleteParameters.Add("orderid", Convert.ToString(orderid));
        sqlDS.DeleteParameters.Add("productid", Convert.ToString(productid));
        sqlDS.Delete();
        CloseConnect();
    }

    public void ApproveOrder(int orderid, string requiredate, bool status)
    {
        OpenConnect();
        sqlDS.UpdateCommand = "Update DonHang SET NgayNhan=@requiredate, TinhTrang=@status  WHERE ID_DonHang=@orderid";
        sqlDS.UpdateParameters.Add("orderid", Convert.ToString(orderid));
        sqlDS.UpdateParameters.Add("requiredate", requiredate);
        sqlDS.UpdateParameters.Add("status", Convert.ToString(status));
        sqlDS.Update();
        CloseConnect();
    }
}
