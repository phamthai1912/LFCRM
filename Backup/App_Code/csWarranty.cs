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
/// Summary description for csWarranty
/// </summary>
public class csWarranty
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

	public csWarranty()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int ShowIdBHMax()
    {
        OpenConnect();

        int IdBHMax = 0;
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT ID_BaoHanh FROM SoBaoHanh ORDER BY ID_BaoHanh DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) IdBHMax = Convert.ToInt32(dv.Table.Rows[0]["ID_BaoHanh"]);

        CloseConnect();
        return IdBHMax;
    }

    public bool CheckSerial(string serial)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM SoBaoHanh WHERE Serial=@serial ";
        sqlDS.SelectParameters.Add("serial", serial);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = false;
        else ok = true;

        CloseConnect();
        return ok;
    }

    public bool CheckWarrantyHasInWarrantyReceiptNote(string id_baohanh)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM PhieuNhanBaoHanh WHERE Id_BaoHanh=" + id_baohanh;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public bool CheckSerialUpdate(string idbaohanh, string serial)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM SoBaoHanh WHERE Serial=@serial AND ID_BaoHanh!=@idbaohanh";
        sqlDS.SelectParameters.Add("idbaohanh", idbaohanh);
        sqlDS.SelectParameters.Add("serial", serial);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = false;
        else ok = true;

        CloseConnect();
        return ok;
    }

    public string ProductNameToProductId(string productname)
    {
        OpenConnect();

        string IdProduct = "0";
        sqlDS.SelectCommand = "SELECT Id_MatHang FROM MatHang WHERE TenMatHang='" + productname + "'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) IdProduct = Convert.ToString(dv.Table.Rows[0]["Id_MatHang"]);

        CloseConnect();
        return IdProduct;
    }

    public DataTable SelectWarranty()
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT ID_BaoHanh, SoBaoHanh.ID_NguoiDung, HoTen, Serial, TenMatHang, DiaChi, convert(varchar, NgayKichHoat, 101) as NgayKichHoat, convert(varchar, NgayHetHan, 101) as NgayHetHan FROM SoBaoHanh, NguoiDung, MatHang WHERE  NguoiDung.ID_NguoiDung = SoBaoHanh.ID_NguoiDung AND MatHang.ID_MatHang = SoBaoHanh.ID_MatHang ORDER BY ID_BaoHanh DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable SelectIdWarranty(string mabh)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT ID_BaoHanh, SoBaoHanh.ID_NguoiDung, HoTen, Serial, TenMatHang, DiaChi,ThanhPho, convert(varchar, NgayKichHoat, 101) as NgayKichHoat, convert(varchar, NgayHetHan, 101) as NgayHetHan FROM SoBaoHanh, NguoiDung, MatHang WHERE  NguoiDung.ID_NguoiDung = SoBaoHanh.ID_NguoiDung AND MatHang.ID_MatHang = SoBaoHanh.ID_MatHang AND ID_BaoHanh = '" + mabh + "'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable FindSerialWarranty(string serial)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT ID_BaoHanh, SoBaoHanh.ID_NguoiDung, HoTen, Serial, TenMatHang,convert(varchar, NgayKichHoat, 101) as NgayKichHoat, convert(varchar, NgayHetHan, 101) as NgayHetHan FROM SoBaoHanh, NguoiDung, MatHang WHERE  NguoiDung.ID_NguoiDung = SoBaoHanh.ID_NguoiDung AND MatHang.ID_MatHang = SoBaoHanh.ID_MatHang AND Serial LIKE '" + serial + "'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable FindWarrantyIDandReceiptDateFromSerial(string serial)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "select ID_BaoHanh,NgayHetHan FROM SoBaoHanh WHERE Serial LIKE '" + serial + "'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public void InsertWarranty(int warrantyid, string serial, DateTime begindate , DateTime enddate, int productid, int userid)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "insert into SoBaoHanh values(@warrantyid,@serial,@begindate,@enddate,@productid,@userid)";
        sqlDS.InsertParameters.Add("warrantyid", Convert.ToString(warrantyid));
        sqlDS.InsertParameters.Add("serial", serial);
        sqlDS.InsertParameters.Add("begindate", Convert.ToString(begindate));
        sqlDS.InsertParameters.Add("enddate", Convert.ToString(enddate));
        sqlDS.InsertParameters.Add("productid", Convert.ToString(productid));
        sqlDS.InsertParameters.Add("userid", Convert.ToString(userid));
        sqlDS.Insert();
        CloseConnect();
    }

    public void UpdateWarranty(string warrantyid, string serial, string enddate)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.UpdateCommand = "Update SoBaoHanh SET Serial=@serial,NgayHetHan=@enddate WHERE ID_BaoHanh=@warrantyid";
        sqlDS.UpdateParameters.Add("warrantyid", warrantyid);
        sqlDS.UpdateParameters.Add("serial", serial);
        sqlDS.UpdateParameters.Add("enddate", enddate);
        sqlDS.Update();
        CloseConnect();
    }

    public void DeleteWarranty(string baohanhid)
    {
        OpenConnect();
        sqlDS.DeleteCommandType = SqlDataSourceCommandType.Text;
        sqlDS.DeleteCommand = "Delete FROM SoBaoHanh WHERE ID_BaoHanh = @baohanhid";
        sqlDS.DeleteParameters.Add("baohanhid", baohanhid);
        sqlDS.Delete();
        CloseConnect();
    }

    // Phần nhận bảo hành
    public DataTable ShowWarrantyReceipt()
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "select  ID_PhieuBaoHanh,PhieuNhanBaoHanh.ID_BaoHanh,TenMatHang,Serial,convert(varchar, NgayNhan, 101) as NgayNhan,convert(varchar, NgayTra, 101) as NgayTra,GhiChu FROM PhieuNhanBaoHanh,SoBaoHanh,MatHang WHERE PhieuNhanBaoHanh.ID_BaoHanh = SoBaoHanh.ID_BaoHanh AND SoBaoHanh.ID_MatHang = MatHang.ID_MatHang ORDER BY ID_PhieuBaoHanh DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable ShowWarrantyReceiptInformation(string idphieunhan)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "select ID_PhieuBaoHanh,PhieuNhanBaoHanh.ID_BaoHanh,TenMatHang,Serial,convert(varchar, NgayNhan, 103) as NgayNhan,convert(varchar, NgayTra, 103) as NgayTra,GhiChu,HoTen,DiaChi FROM PhieuNhanBaoHanh,SoBaoHanh,MatHang,NguoiDung WHERE PhieuNhanBaoHanh.ID_BaoHanh = SoBaoHanh.ID_BaoHanh AND	SoBaoHanh.ID_MatHang = MatHang.ID_MatHang AND SoBaoHanh.ID_NguoiDung = NguoiDung.ID_NguoiDung AND ID_PhieuBaoHanh =" + idphieunhan + "";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public bool CheckInsertReceipt(string id_baohanh)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "select ID_PhieuBaoHanh,PhieuNhanBaoHanh.ID_BaoHanh,TenMatHang,Serial,convert(varchar, NgayNhan, 101) as NgayNhan,convert(varchar, NgayTra, 101) as NgayTra,GhiChu FROM PhieuNhanBaoHanh,SoBaoHanh,MatHang WHERE PhieuNhanBaoHanh.ID_BaoHanh = SoBaoHanh.ID_BaoHanh AND SoBaoHanh.ID_MatHang = MatHang.ID_MatHang AND GhiChu = N'NO' AND PhieuNhanBaoHanh.ID_BaoHanh=" + id_baohanh;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public bool CheckReceiptNote(string id_phieunhan)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "select GhiChu FROM PhieuNhanBaoHanh WHERE GhiChu = N'NO' AND ID_PhieuBaoHanh = " + id_phieunhan;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public void InsertReceipt(string ngaynhan, string ngaytra, string id_baohanh)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "insert into PhieuNhanBaoHanh(NgayNhan,NgayTra,GhiChu,ID_BaoHanh) values(@ngaynhan,@ngaytra,N'NO',@id_baohanh)";
        sqlDS.InsertParameters.Add("ngaynhan", ngaynhan);
        sqlDS.InsertParameters.Add("ngaytra", ngaytra);
        sqlDS.InsertParameters.Add("id_baohanh", id_baohanh);

        sqlDS.Insert();
        CloseConnect();
    }

    public void UpdateReturnDateReceiptWarranty(string id_phieunhan, string ngaytra)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "Update PhieuNhanBaoHanh SET NgayTra=@ngaytra WHERE ID_PhieuBaoHanh=@id_phieunhan";
        sqlDS.InsertParameters.Add("id_phieunhan", id_phieunhan);
        sqlDS.InsertParameters.Add("ngaytra", ngaytra);

        sqlDS.Insert();
        CloseConnect();
    }

    public void UpdateReceiptWarrantyNote(string id_phieunhan, string ghichu)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "Update PhieuNhanBaoHanh SET GhiChu=@ghichu WHERE ID_PhieuBaoHanh=@id_phieunhan";
        sqlDS.InsertParameters.Add("id_phieunhan", id_phieunhan);
        sqlDS.InsertParameters.Add("ghichu", ghichu);

        sqlDS.Insert();
        CloseConnect();
    }

    public bool CheckDeleteReceiptWarranty(string id)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM PhieuNhanBaoHanh WHERE GhiChu = N'OK' AND ID_PhieuBaoHanh=" + id;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public void DeleteReceiptWarranty(string id_phieunhan)
    {
        OpenConnect();
        sqlDS.DeleteCommandType = SqlDataSourceCommandType.Text;
        sqlDS.DeleteCommand = "Delete From PhieuNhanBaoHanh WHERE ID_PhieuBaoHanh=@id_phieunhan";
        sqlDS.DeleteParameters.Add("id_phieunhan", id_phieunhan);
        sqlDS.Delete();
        CloseConnect();
    }
}
