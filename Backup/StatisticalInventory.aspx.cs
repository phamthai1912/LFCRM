using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class StatisticalInventory : System.Web.UI.Page
{
    csStatistics Statistics = new csStatistics();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack) LoadDllChonThang();
    }

    protected void btn_View_Click(object sender, EventArgs e)
    {
        lbl_ThongKeHangTon.Text = PrintStatisticalInventory();
        btn_Print.Visible = true;
    }

    void LoadDllChonThang()
    {
        DataTable tb_SelectMMYYYYInImport = Statistics.SelectMMYYYYInImport();
        DataTable tb_SelectMMYYYYInExport = Statistics.SelectMMYYYYInExport();

        ddl_ChonThang.Items.Clear();
        if (tb_SelectMMYYYYInImport.Rows.Count < tb_SelectMMYYYYInExport.Rows.Count)
            for (int i = 0; i < tb_SelectMMYYYYInExport.Rows.Count; i++)
                ddl_ChonThang.Items.Add(Convert.ToString(tb_SelectMMYYYYInExport.Rows[i]["NgayXuat"]));
        else
            for (int i = 0; i < tb_SelectMMYYYYInImport.Rows.Count; i++)
                ddl_ChonThang.Items.Add(Convert.ToString(tb_SelectMMYYYYInImport.Rows[i]["NgayNhap"]));
    }

    public string PrintStatisticalInventory()
    {
        DataTable tb_idCatalog = Statistics.SelectIdLoaiHang();
        DataTable tb_idProductInCatalog;
        DataTable tb_InfoOfProductInImportMonth;
        DataTable tb_InfoOfProductInExportMonth;
        int tondau = 0;
        int toncuoi = 0;
        int dongia = 0;
        int giatri = 0;

        int k = 0;
        string str = "<table style='width: 770px; color: black; border-collapse:collapse; border-color: Black' border=1> " +
                        "<tr><td align=center><img src='Anh/fpt.jpg' height=100px width=150px /></td><td align=center><span style='font-size: 25px'><b>THỐNG KÊ HÀNG TỒN</b></span><br /><i>Tháng: " + ddl_ChonThang.Text + "</i></td></tr> " +
                        "<tr><td colspan=2 align=center> <br>" +
                                "<table style='width: 770px; border-collapse:collapse;' border='1' borderColor='#213943'> " +
                                    "<tr><td rowspan=2><b>Stt</b></td><td rowspan=2><b>Mã hàng</b></td><td rowspan=2><b>Tên hàng</b></td><td rowspan=2><b>ĐVT</b></td><td rowspan=2><b>Tồn đầu</b></td><td colspan=2><b>Trong kỳ</b></td><td rowspan=2><b>Tồn cuối</b></td><td rowspan=2><b>Giá nhập</b></td><td rowspan=2><b>Giá trị hàng tồn</b></td></tr>"+
                                    "<tr><td>Nhập</td><td>Xuất</td></tr> ";
        for (int i = 0; i < tb_idCatalog.Rows.Count; i++)
        {
            int tong = 0;
            tb_idProductInCatalog = Statistics.SelectIdProductInCatalog(Convert.ToString(tb_idCatalog.Rows[i]["Id_Loaihang"]));

            bool ok1 = Statistics.CheckCatalogHasInImportInMonth(Convert.ToString(tb_idCatalog.Rows[i]["id_loaihang"]), ddl_ChonThang.Text);
            bool ok2 = Statistics.CheckCatalogHasInExportInMonth(Convert.ToString(tb_idCatalog.Rows[i]["id_loaihang"]), ddl_ChonThang.Text);
            if (ok1 || ok2)
            {
                str = str + "<tr><td colspan=10 align=left><b>" + tb_idCatalog.Rows[i]["loaihang"] + "</b></td></tr>";
                for (int j = 0; j < tb_idProductInCatalog.Rows.Count; j++)
                {
                    bool ok3 = Statistics.CheckProductHasInImportInMonth(Convert.ToString(tb_idProductInCatalog.Rows[j]["Id_Mathang"]), ddl_ChonThang.Text);
                    bool ok4 = Statistics.CheckProductHasInExportInMonth(Convert.ToString(tb_idProductInCatalog.Rows[j]["Id_Mathang"]), ddl_ChonThang.Text);
                    dongia = Statistics.GetLatestUnitPriceInImport(Convert.ToString(tb_idProductInCatalog.Rows[j]["Id_Mathang"]), ddl_ChonThang.Text);
                    if (ok3 && ok4) //vua co nhap va xuat
                    {
                        tb_InfoOfProductInExportMonth = Statistics.InfoOfProductInExportMonth(Convert.ToString(tb_idProductInCatalog.Rows[j]["Id_Mathang"]), ddl_ChonThang.Text);
                        tb_InfoOfProductInImportMonth = Statistics.InfoOfProductInImportMonth(Convert.ToString(tb_idProductInCatalog.Rows[j]["Id_Mathang"]), ddl_ChonThang.Text);
                        tondau = Statistics.GetQuantityStartInventory(Convert.ToString(tb_InfoOfProductInImportMonth.Rows[0]["id_mathang"]), ddl_ChonThang.Text);
                        toncuoi = tondau + Convert.ToInt32(tb_InfoOfProductInImportMonth.Rows[0]["SoLuong"]) - Convert.ToInt32(tb_InfoOfProductInExportMonth.Rows[0]["SoLuong"]);
                        giatri = dongia * toncuoi;
                        tong = tong + giatri;
                        str = str + "<tr><td>" + (k + 1) + "</td><td>" + tb_InfoOfProductInImportMonth.Rows[0]["id_mathang"] + "</td><td>" + tb_InfoOfProductInImportMonth.Rows[0]["Tenmathang"] + "</td><td>" + tb_InfoOfProductInImportMonth.Rows[0]["Donvitinh"] + "</td><td>" + tondau + "</td><td>" + tb_InfoOfProductInImportMonth.Rows[0]["SoLuong"] + "</td><td>" + tb_InfoOfProductInExportMonth.Rows[0]["SoLuong"] + "</td><td>" + toncuoi + "</td><td>" + string.Format("{0:N0}",dongia) + " </td><td>" + string.Format("{0:N0}",giatri) + "</td></tr>";
                    }

                    else if (ok3 == true) //chi co nhap
                    {
                        tb_InfoOfProductInImportMonth = Statistics.InfoOfProductInImportMonth(Convert.ToString(tb_idProductInCatalog.Rows[j]["Id_Mathang"]), ddl_ChonThang.Text);
                        tondau = Statistics.GetQuantityStartInventory(Convert.ToString(tb_InfoOfProductInImportMonth.Rows[0]["id_mathang"]), ddl_ChonThang.Text);
                        toncuoi = tondau + +Convert.ToInt32(tb_InfoOfProductInImportMonth.Rows[0]["SoLuong"]);
                        giatri = dongia * toncuoi;
                        tong = tong + giatri;
                        str = str + "<tr><td>" + (k + 1) + "</td><td>" + tb_InfoOfProductInImportMonth.Rows[0]["id_mathang"] + "</td><td>" + tb_InfoOfProductInImportMonth.Rows[0]["Tenmathang"] + "</td><td>" + tb_InfoOfProductInImportMonth.Rows[0]["Donvitinh"] + "</td><td>" + tondau + "</td><td>" + tb_InfoOfProductInImportMonth.Rows[0]["SoLuong"] + "</td><td>0</td><td>" + toncuoi + "</td><td>" + string.Format("{0:N0}",dongia) + "</td><td>" + string.Format("{0:N0}",giatri) + "</td></tr>";
                    }
                    else if (ok4 == true) // chi co xuat
                    {
                        tb_InfoOfProductInExportMonth = Statistics.InfoOfProductInExportMonth(Convert.ToString(tb_idProductInCatalog.Rows[j]["Id_Mathang"]), ddl_ChonThang.Text);
                        tondau = Statistics.GetQuantityStartInventory(Convert.ToString(tb_InfoOfProductInExportMonth.Rows[0]["id_mathang"]), ddl_ChonThang.Text);
                        toncuoi = tondau - Convert.ToInt32(tb_InfoOfProductInExportMonth.Rows[0]["SoLuong"]);
                        giatri = dongia * toncuoi;
                        tong = tong + giatri;
                        str = str + "<tr><td>" + (k + 1) + "</td><td>" + tb_InfoOfProductInExportMonth.Rows[0]["id_mathang"] + "</td><td>" + tb_InfoOfProductInExportMonth.Rows[0]["Tenmathang"] + "</td><td>" + tb_InfoOfProductInExportMonth.Rows[0]["Donvitinh"] + "</td><td>" + tondau + "</td><td>0</td><td>" + tb_InfoOfProductInExportMonth.Rows[0]["SoLuong"] + "</td><td>" + toncuoi + "</td><td>" + string.Format("{0:N0}",dongia) + "</td><td>" + string.Format("{0:N0}",giatri) + "</td></tr>";
                    }
                    else k--;

                    k++;
                }
                str = str + "<tr><td colspan=9 align=right><b>Tổng nhóm: </b></td><td><b>" + string.Format("{0:N0}",tong) + " vnđ</b></td></tr>";
            }
        }

        str = str + "</table>";
        str = str + "<br></td></tr>"+
                    "<tr><td align=center valign=top>Quản lý<br /><i>(ký và ghi Họ Tên)</i></td><td align=center>Người lập phiếu<br /><i>(ký và ghi Họ Tên)</i><br><br><br><br>" + Session["FullName"] + "</td></tr>" +
                    "</table>";

        return str;   
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = lbl_ThongKeHangTon;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        Response.Redirect("~/Print.aspx");
    }
}
