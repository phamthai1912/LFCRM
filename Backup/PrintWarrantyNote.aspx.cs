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

public partial class PrintWarrantyNote : System.Web.UI.Page
{
    csWarranty warranty = new csWarranty();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string WarrantyExport()
    {
        DataTable tb_WarrantyInfo = warranty.SelectIdWarranty(ddl_MaBH.SelectedValue);
        Label lbl_FullName = new Label();
        lbl_FullName.Text = Session["FullName"].ToString();
        string str = "";
        str = "<table style='width: 770px; color: black; border-collapse:collapse; border-color: Black' border=1>"
               + "<tr><td align=center><img src='Anh/fpt.jpg' height=100px width=150px /></td><td  align=center style='font-size: 40px'><span style='font-size: 30px'><b>CÔNG TY CỔ PHẦN PHÁT TRIỂN ĐẦU TƯ CÔNG NGHỆ</b></span><br/><span style='font-size: 16px'><b>THE CORPORATION FOR FINANCING AND PROMOTING TECHNOLOGY</b></span><br/></td></tr>"
               + "<tr><td colspan=2 align=center height=50px><span style='font-size: 30px'><b>PHIẾU BẢO HÀNH</b></span></td></tr>"
               + "<tr><td colspan=2 align=right><b>No : " + ddl_MaBH.SelectedValue + "&nbsp;&nbsp;&nbsp;&nbsp;</b></td></tr>"
               + "<tr><td colspan=2 align=center>";

        str = str + "<table style='width: 500px; color: black; border-collapse:collapse; border-color: Black' border='0'> "
                + "<tr align=left><td width=250px height=25px> - Họ tên khách hàng:</td>"
                    + "<td><b>" + tb_WarrantyInfo.Rows[0]["HoTen"] + "</b></tr>"
                + "<tr align=left><td width=250px height=25px> - Địa chỉ:</td>"
                    + "<td><b>" + tb_WarrantyInfo.Rows[0]["DiaChi"] + "&nbsp;-&nbsp;" + tb_WarrantyInfo.Rows[0]["ThanhPho"] + "</b></tr>"
                + "<tr align=left><td width=250px height=25px> - Tên sản phẩm:</td>"
                    + "<td><b>" + tb_WarrantyInfo.Rows[0]["TenMatHang"] + "</b></tr>"
                + "<tr align=left><td width=250px height=25px> - Số Serial:</td>"
                    + "<td><b>" + tb_WarrantyInfo.Rows[0]["Serial"] + "</b></td></tr></table></td></tr>"
                        + "<tr><td colspan=2 align=center>";

        str = str + "<table style='width: 770px; color: black; border-collapse:collapse; border-color: Black' border='0'> "
                + "<tr><td valign=top align=center width=385px height=35px>NGÀY KÍCH HOẠT<br/><b>" + tb_WarrantyInfo.Rows[0]["NgayKichHoat"] + "</b><br/></td>" + "<td valign=top align=center>BẢO HÀNH CÓ GIỚI HẠN ĐẾN NGÀY<br/><b>" + tb_WarrantyInfo.Rows[0]["NgayHetHan"] + "</b><br/><br/><br/></td></tr>"

                + "<tr><td valign=top align=center width=375px height=35px><b>ĐẠI DIỆN FPT</b><br/><i>(Họ tên, chữ ký)</i><br/><br/><br/><br/><br/><br/>" + lbl_FullName.Text + "</td>"
                  + "<td valign=top align=center><b>ĐẠI DIỆN KHÁCH HÀNG</b><br/><i>(Họ tên, chữ ký)</b></i><br/><br/><br/><br/><br/><br/>" + tb_WarrantyInfo.Rows[0]["HoTen"] + "</td></tr></table></td></tr>"
                  + "<tr><td colspan=2 align=center height=50px><span style='font-size: 20px; color: RED'><b>XIN CHÂN THÀNH CẢM ƠN QUÝ KHÁCH ĐÃ MUA HÀNG CỦA FPT</b></span></td></tr>";


        str = str + "</table>";

        return str;
    }

    protected void btn_XuatPhieu_Click(object sender, EventArgs e)
    {
        lbl_Form.Text = WarrantyExport();
        Pn_Form.Visible = true;
        Pn_btnPrint.Visible = true; 
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Pn_Form;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        Response.Redirect("~/Print.aspx");
    }
}
