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

public partial class StatisticsSale : System.Web.UI.Page
{
    csStatisticSale csale = new csStatisticSale();
    csDoiSoThanhChu NumberToString = new csDoiSoThanhChu();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_StartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_EndDate.Text = txt_StartDate.Text;
        }
    }

    protected void txt_StartDate_TextChanged(object sender, EventArgs e)
    {
        txt_EndDate.Text = txt_StartDate.Text;
    }

    protected void btn_View_Click(object sender, EventArgs e)
    {
        lbl_ThongKe.Text = StatisticSale();
        Pn_btnPrint.Visible = true;
    }

    public string StatisticSale()
    {
        DataTable tb_CatalogHasInOrderDetail;
        DataTable tb_ProductHasInOrdertDetailByDateAndByCatalog;
        DataTable tb_StatisticsSaleByDateCatalogProduct;
        int tongtien = 0;
        string catalogname = "";

        string str = "<table style='width: 770px; color: black; border-collapse:collapse; border-color: Black' border=1>" +
                        "<tr><td align=center><img src='Anh/fpt.jpg' height=100px width=150px /></td><td align=center><span style='font-size: 25px'><b>THỐNG KÊ SỐ LƯỢNG HÀNG BÁN</b></span><br /><i>Từ ngày: " + txt_StartDate.Text + " đến ngày: " + txt_EndDate.Text + "</i></td></tr>" +
                        "<tr><td colspan=2 align=center>";

        tb_CatalogHasInOrderDetail = csale.CatalogHasInOrderDetailByDate(txt_StartDate.Text, txt_EndDate.Text);
        for (int i = 0; i < tb_CatalogHasInOrderDetail.Rows.Count; i++)
        {
            catalogname = Convert.ToString(tb_CatalogHasInOrderDetail.Rows[i]["Loaihang"]);
            str = str + "<br><table style='width: 750px; color: black; border-collapse:collapse; border-color: Black' border='1'> " +
                            "<tr><td colspan=5><b>Loại hàng: " + catalogname + "</b></td></tr> " +
                            "<tr><td><b>Mã hàng</b></td><td><b>Tên hàng</b></td><td><b>Số lượng</b></td><td><b>Đơn giá (vnđ)</b></td><td><b>Ngày bán</b></td></tr>";

            tb_ProductHasInOrdertDetailByDateAndByCatalog = csale.ProductHasInOrderDetailByDateAndByCatalog(catalogname, txt_StartDate.Text, txt_EndDate.Text);

            for (int j = 0; j < tb_ProductHasInOrdertDetailByDateAndByCatalog.Rows.Count; j++)
            {
                tb_StatisticsSaleByDateCatalogProduct = csale.SelectStatisticsSalesByDateCatalogProduct(catalogname, Convert.ToString(tb_ProductHasInOrdertDetailByDateAndByCatalog.Rows[j]["TenMatHang"]), txt_StartDate.Text, txt_EndDate.Text);

                int row = tb_StatisticsSaleByDateCatalogProduct.Rows.Count;

                str = str + "<tr><td rowspan='" + row + "' valign=top>" + tb_StatisticsSaleByDateCatalogProduct.Rows[0]["Id_Mathang"] + "</td><td rowspan='" + row + "' valign=top>" + tb_StatisticsSaleByDateCatalogProduct.Rows[0]["TenMatHang"] + "</td><td>" + tb_StatisticsSaleByDateCatalogProduct.Rows[0]["SoLuong"] + "</td><td>" + tb_StatisticsSaleByDateCatalogProduct.Rows[0]["DonGia"]+ "</td><td>" + tb_StatisticsSaleByDateCatalogProduct.Rows[0]["NgayNhan"] + "</td></tr>";

                int tongsoluong = Convert.ToInt32(tb_StatisticsSaleByDateCatalogProduct.Rows[0]["Soluong"]);
                int tonggiatrimathang = Convert.ToInt32(tb_StatisticsSaleByDateCatalogProduct.Rows[0]["Soluong"]) * Convert.ToInt32(tb_StatisticsSaleByDateCatalogProduct.Rows[0]["DonGia"]);

                for (int k = 1; k < row; k++)
                {

                    str = str + "<tr><td>" + tb_StatisticsSaleByDateCatalogProduct.Rows[k]["SoLuong"] + "</td><td>" +tb_StatisticsSaleByDateCatalogProduct.Rows[k]["Dongia"] + "</td><td>" + tb_StatisticsSaleByDateCatalogProduct.Rows[k]["Ngaynhan"] + "</td></tr>";           
                    tongsoluong = tongsoluong + Convert.ToInt32(tb_StatisticsSaleByDateCatalogProduct.Rows[k]["Soluong"]);
                    tonggiatrimathang = tonggiatrimathang + Convert.ToInt32(tb_StatisticsSaleByDateCatalogProduct.Rows[k]["Soluong"]) * Convert.ToInt32(tb_StatisticsSaleByDateCatalogProduct.Rows[k]["DonGia"]);
                }
                str = str + "<tr><td colspan=2 align=right> <b>Tổng số lượng: </b></td><td colspan=3 align=left>" + tongsoluong + "</td></tr> " +
                            "<tr><td colspan=2 align=right> <b>Tổng giá trị: </b></td><td colspan=3 align=left>" + string.Format("{0:N0}",tonggiatrimathang) + " vnđ</td></tr>";
                tongtien = tongtien + tonggiatrimathang;
            }
            str = str + "</table>";
        }


        str = str + "<br></td></tr>" +
                    "<tr><td colspan=2>Tổng tiền: <b>" + string.Format("{0:N0}",tongtien) + " vnđ</b></tr>" +
                    "<tr><td colspan=2>Tổng tiền bằng chữ: <b>" + NumberToString.converNumToString(NumberToString.slipArray(tongtien.ToString())) + " đồng.</b></tr>" +
                    "<tr><td align=center valign=top>Quản lý<br /><i>(ký và ghi Họ Tên)</i></td><td align=center>Người lập phiếu<br /><i>(ký và ghi Họ Tên)</i><br><br><br><br>" + Session["FullName"].ToString() + "</td></tr>" +
                "</table>";
        return str;
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pn_lbl_ThongKe;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        Response.Redirect("~/Print.aspx");
    }
}
