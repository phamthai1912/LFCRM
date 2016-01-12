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

public partial class PrintWarrantyReceiptNote : System.Web.UI.Page
{
    csLogin login = new csLogin();
    csOrder order = new csOrder();
    csProduct Product = new csProduct();
    csWarranty warranty = new csWarranty();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string TaoPhieuNhan()
    {
        DataTable tb_WarrantyReceiptInfo = warranty.ShowWarrantyReceiptInformation(ddl_MaPhieuNhan.SelectedValue);
        Label lbl_Now = new Label();
        Label lbl_UserID = new Label();
        Label lbl_FullName = new Label();
        lbl_UserID.Text = Session["UserID"].ToString();
        lbl_FullName.Text = Session["FullName"].ToString();
        lbl_Now.Text = DateTime.Now.ToString("dd/MM/yyyy");
        string str = "";
        str = "<table style='width: 770px; color: black; border-collapse:collapse; border-color: Black' border=1>"
              + "<tr><td align=center><img src='Anh/fpt.jpg' height=100px width=150px /></td><td align=center><span style='font-size: 30px'><b>PHIẾU NHẬN BẢO HÀNH</b></span><br /><i><b>Trung tâm bán lẻ : </b></span>FPT SHOP_10 NGUYỄN VĂN LINH_ĐÀ NẴNG</br><span style='font-size: 18px'><b>TEL : </b></span>05113.552666</i></td></tr>"
              + "<tr><td colspan=2 align=right height=30px><span style='font-size: 16px'>Phiếu nhận số: <b>" + tb_WarrantyReceiptInfo.Rows[0]["ID_PhieuBaoHanh"] + "&nbsp;&nbsp;</b></span>"
              + "<tr><td colspan=2 align=center></br><span style='font-size: 20px'><b>THÔNG TIN VỀ KHÁCH HÀNG</b></span>";

        str = str + "<br><table style='width: 700px; color: black; border-collapse:collapse; border-color: Black' border='1'> "
               + "<tr align=left><td width=150px height=25px> - Họ tên khách hàng:</td>"
                   + "<td width=200px><b>&nbsp;&nbsp;" + tb_WarrantyReceiptInfo.Rows[0]["HoTen"] + "</b></td>"
                   + "<td width=150px height=25px> - Địa chỉ :</td>"
                   + "<td width=200px><b>&nbsp;&nbsp;" + tb_WarrantyReceiptInfo.Rows[0]["DiaChi"] + "</b></td></tr></table></br></td></tr>"
               + "<tr><td colspan=2 align=center></br><span style='font-size: 20px'><b>THÔNG TIN VỀ SẢN PHẨM</b></span>";

        str = str + "<br><table style='width: 750px; color: black; border-collapse:collapse; border-color: Black' border='1'> "
                + "<tr style='font-weight:bold'><td width=50px>STT</td><td width=200px>Tên sản phẩm</td><td width=200px>Số Serial</td><td width=150px>Ngày nhận</td><td width=150px>Ngày trả</td></tr>";
        for (int i = 0; i < tb_WarrantyReceiptInfo.Rows.Count; i++)
        {
            str = str + "<tr><td>" + (i + 1) + "</td><td>"
                + tb_WarrantyReceiptInfo.Rows[i]["TenMatHang"] + "</td><td>"
                + tb_WarrantyReceiptInfo.Rows[i]["Serial"] + "</td><td>"
                + tb_WarrantyReceiptInfo.Rows[i]["NgayNhan"] + "</td><td>"
                + tb_WarrantyReceiptInfo.Rows[i]["NgayTra"] + "</td></tr>";
        }
        str = str + "</table></br></td></tr>";


        str = str + "<tr><td align=center valign=top>Khách hàng<br/><i>(ký và ghi Họ Tên)</i><br><br><br><br><br>" + tb_WarrantyReceiptInfo.Rows[0]["HoTen"] + "</td><td align=center><i>Đà Nẵng, " + lbl_Now.Text + "</i></br>Người lập phiếu<br /><i>(ký và ghi Họ Tên)</i><br><br><br><br>" + lbl_FullName.Text + "</td></tr>"
                + "</table>";
        return str;
    }
    protected void btn_XuatPhieu_Click(object sender, EventArgs e)
    {
        lbl_Form.Text = TaoPhieuNhan();
        btn_Print.Visible = true;
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pn_lblprint;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        Response.Redirect("~/Print.aspx");
    }
}
