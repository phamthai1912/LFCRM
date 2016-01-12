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
using System.IO;

public partial class ManagementOrder : System.Web.UI.Page
{
    csLogin login = new csLogin();
    csOrder order = new csOrder();
    csWarranty warranty = new csWarranty();
    csDoiSoThanhChu NumberToString = new csDoiSoThanhChu();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_MaBH.Text = (warranty.ShowIdBHMax() + 1).ToString();
            txt_StartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_EndDate.Text = txt_StartDate.Text;
        }
        txt_Begin.Text = DateTime.Now.ToString("MM/dd/yyyy");

        lbl_Mess.Text = "";
        lbl_Mess1.Text = "";
        showGridviewWarranty();
    }

    protected void btn_ComeBack_Click(object sender, EventArgs e)
    {
        if (pn_Warranty.Visible == true)
        {
            grv_Warranty.DataSource = null;
            grv_Warranty.DataBind();
            
            txt_End.Text = "";
            txt_Serial.Text = "";
            txt_Begin.Text = DateTime.Now.ToString("MM/dd/yyyy");

            txt_MaBH.Text = (warranty.ShowIdBHMax() + 1).ToString();
            lbl_Title.Text = "Chi tiết đơn hàng";
            lbl_Serial.Text = "";

            btn_RequireExportStorage.Visible = true;
            btn_SaleBill.Visible = true;
            btn_Add.Visible = true;
            pn_OrderDetail.Visible = true;

            btn_Update.Visible = false;
            pn_ManagementOrder.Visible = false;
            pn_Warranty.Visible = false;

            return;
        }
        if (pn_OrderDetail.Visible == true)
        {
            lbl_Title.Text = "Quản lý đơn hàng";
            pn_ManagementOrder.Visible = true;
            pn_OrderDetail.Visible = false;
            pn_Warranty.Visible = false;
            btn_RequireExportStorage.Visible = false;
            btn_SaleBill.Visible = false;
            btn_ComeBack.Visible = false;
            return;
        }

        if (Pn_Form.Visible == true)
        {
            lbl_Form.Text = "";
            lbl_Title.Text = "Chi tiết đơn hàng";
            lbl_Mess2.Text = "";
            btn_SaleBill.Visible = true;
            btn_RequireExportStorage.Visible = true;
            Pn_btnPrint.Visible = false;
            Pn_Form.Visible = false;
            pn_OrderDetail.Visible = true;
            return;
        }
    }

    protected void txt_StartDate_TextChanged(object sender, EventArgs e)
    {
        txt_EndDate.Text = txt_StartDate.Text;
    }

    public void showData()
    {
        grv_Orders.DataSource = order.SelectAllOrders(txt_StartDate.Text, txt_EndDate.Text);
        grv_Orders.DataBind();
        if (grv_Orders.Rows.Count == 0)
            lbl_Mess.Text = "Không có đơn đặt hàng nào từ ngày " + txt_StartDate.Text + " đến " + txt_EndDate.Text + " ";
    }

    //Tìm kiếm đơn hàng
    protected void btn_View_Click(object sender, EventArgs e)
    {
        showData();
    }

    //Template chi tiết trong gridview order
    protected void lbn_Detail_Click(object sender, EventArgs e)
    {
        pn_ManagementOrder.Visible = false;
        pn_OrderDetail.Visible = true;
        lbl_Title.Text = "Chi tiết đơn hàng";
        btn_RequireExportStorage.Visible = true;
        btn_SaleBill.Visible = true;
        btn_ComeBack.Visible = true;
    }

    //Gửi yêu cầu xuất kho
    protected void btn_RequireExportStorage_Click(object sender, EventArgs e)
    {
        lbl_Form.Text = RequireExportStorage();

        btn_Send.Visible = true;
        lbl_Title.Text = "Phiếu yêu cầu xuất kho";
        btn_RequireExportStorage.Visible = false;
        btn_SaleBill.Visible = false;
        Pn_Form.Visible = true;
        Pn_btnPrint.Visible = true;
        pn_ManagementOrder.Visible = false;
        pn_OrderDetail.Visible = false;
    }

    //Hàm xuất hóa đơn bán hàng
    public string SaleBill()
    {
        int amount = 0;
        int total = 0;
        TextBox txt_OrderID = (TextBox)fv_OrderDetail.FindControl("txt_OrderID");
        DataTable tb_OrderInfo = order.ShowOrderInformation(txt_OrderID.Text);
        string str = "";
        Label lbl_Now = new Label();
        Label lbl_UserID = new Label();
        Label lbl_FullName = new Label();
        lbl_UserID.Text = Session["UserID"].ToString();
        lbl_FullName.Text = Session["FullName"].ToString();
        lbl_Now.Text = DateTime.Now.ToString("dd/MM/yyyy");
        if (tb_OrderInfo.Rows.Count > 0)
        {
            str = "<table style='width: 770px; color: black; border-collapse:collapse; border-color: Black' border=1>" +
                        "<tr><td align=center><img src='Anh/fpt.jpg' height=100px width=150px /></td><td align=center><span style='font-size: 30px'><b>HÓA ĐƠN BÁN HÀNG</b></span><br /><i><b>Trung tâm bán lẻ : </b></span>FPT SHOP_10 NGUYỄN VĂN LINH_ĐÀ NẴNG</br><span style='font-size: 18px'><b>TEL : </b></span>05113.552666</i></td></tr>" +
                        "<tr><td colspan=2 align=center>";
            str = str + "<br><table style='width: 750px; color: black; border-collapse:collapse; border-color: Black' border='0'> "
                + "<tr align=left><td width=230px height=25px> - Họ tên khách hàng:</td>"
                    + "<td><b>" + tb_OrderInfo.Rows[0]["HoTen"] + "</b></tr>"
                + "<tr align=left><td width=230px height=25px> - Địa chỉ giao hàng:</td>"
                    + "<td><b>" + tb_OrderInfo.Rows[0]["DiaChiGiaoHang"] + "&nbsp;&nbsp;" + tb_OrderInfo.Rows[0]["ThanhPho"] + "</b></tr>"
                + "<tr align=left><td width=230px height=25px> - Số điện thoại:</td>"
                    + "<td><b>0" + tb_OrderInfo.Rows[0]["Phone"] + "</b></tr>"
                + "<tr align=left><td width=230px height=25px> - Ngày đặt hàng:</td>"
                    + "<td><b>" + tb_OrderInfo.Rows[0]["NgayDatHang"] + "</b></td></tr></table></br></td></tr>"
                + "<tr><td colspan=2 align=center>";

            str = str + "<br><table style='width: 770px; color: black; border-collapse:collapse; border-color: Black' border='1'> "
                + "<tr style='font-weight:bold'><td width=50px>STT</td><td width=250px>Tên sản phẩm</td><td width=100px>Số lượng</td><td width=170px>Đơn giá (vnđ)</td><td width=200px>Thành tiền</td></tr>";

            for (int i = 0; i < tb_OrderInfo.Rows.Count; i++)
            {
                amount = Convert.ToInt32(tb_OrderInfo.Rows[i]["SoLuong"]) * Convert.ToInt32(tb_OrderInfo.Rows[i]["DonGia"]);
                str = str + "<tr><td>" + (i + 1) + "</td><td>" + tb_OrderInfo.Rows[i]["TenMatHang"] + "</td><td>" + tb_OrderInfo.Rows[i]["SoLuong"] + "</td><td>" + string.Format("{0:N0}", tb_OrderInfo.Rows[i]["DonGia"]) + "</td><td>" + string.Format("{0:N0}", amount) + "</td></tr>";
                total = total + amount;
            }
            str = str + "<tr><td colspan=4 align=right><b>Tổng tiền : </b></td><td><b>" + string.Format("{0:N0}", total) + " vnđ</b></td></tr>"
                + "<tr><td colspan=5 align=left height=25px>&nbsp;- Tổng tiền bằng chữ : <b>" + NumberToString.converNumToString(NumberToString.slipArray(total.ToString())) + " đồng.</b></td></tr>"
                + "</table></br>"
            + "</td></tr>"
            + "<tr><td align=center valign=top></br>Nhân viên bán hàng<br /><i>(ký và ghi Họ Tên)</i><br><br><br><br>" + lbl_FullName.Text + "</td><td align=center><i>Đà Nẵng, " + lbl_Now.Text + "</i></br>Khách hàng<br /><i>(ký và ghi Họ Tên)</i><br><br><br><br>" + tb_OrderInfo.Rows[0]["HoTen"] + "</td></tr>"
                + "</table>";
        }
        return str;
    }

    // Tạo hóa đơn bán hàng
    protected void btn_SaleBill_Click(object sender, EventArgs e)
    {
        lbl_Form.Text = SaleBill();

        btn_Send.Visible = false;
        lbl_Title.Text = "Hóa đơn bán hàng";
        btn_RequireExportStorage.Visible = false;
        btn_SaleBill.Visible = false;
        Pn_Form.Visible = true;
        Pn_btnPrint.Visible = true;
        pn_ManagementOrder.Visible = false;
        pn_OrderDetail.Visible = false;
    }

    //Xóa đơn hàng gridview order
    protected void grv_Orders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            pn_OrderDetail.Visible = false;
            fv_OrderDetail.DataSource = null;
            grv_OrderDetail.DataSource = null;

            if (order.CheckDeleteOrder(grv_Orders.Rows[index].Cells[0].Text))
                lbl_Mess.Text = "Đơn hàng này đã được duyệt bạn không thể xóa !!!";
            else
            {
                order.DeleteOrder(grv_Orders.Rows[index].Cells[0].Text);
                lbl_Mess.Text = "Xóa đơn hàng thành công !!!";
            }
            showData();
        }
    }

    protected void grv_Orders_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_Orders.PageIndex = e.NewPageIndex;
        showData();
    }

    //Xóa mặt hàng trong gridview orderdetail
    protected void grv_OrderDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = int.Parse(e.CommandArgument.ToString());
        
        if (e.CommandName == "Del")
        {
            TextBox txt_OrderID = (TextBox)fv_OrderDetail.FindControl("txt_OrderID");
            if (order.CheckDeleteOrder(txt_OrderID.Text))
                lbl_Mess1.Text = "Đơn hàng này đã được duyệt bạn không thể xóa !!!";
            else
            {
                order.DeleteOrderDetail(int.Parse(txt_OrderID.Text), int.Parse(grv_OrderDetail.Rows[index].Cells[0].Text));
                lbl_Mess1.Text = "Xóa mặt hàng thành công !!!";
            }
            grv_OrderDetail.DataBind();
        }

        if (e.CommandName == "Warranty")
        {            
            lbl_Title.Text = "Bảo hành";
            lbl_ThongBao.Text = "";
            btn_RequireExportStorage.Visible = false;
            btn_SaleBill.Visible = false;
            pn_Warranty.Visible = true;
            pn_OrderDetail.Visible = false;
            pn_ManagementOrder.Visible = false;

            TextBox txt_UserID = (TextBox)fv_OrderDetail.FindControl("txt_UserID");
            txt_MaKH.Text = txt_UserID.Text;
            txt_Product.Text = grv_OrderDetail.Rows[index].Cells[1].Text;
        }
    }

    // Duyệt đơn hàng
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        TextBox txt_OrderID = (TextBox)fv_OrderDetail.FindControl("txt_OrderID");
        TextBox txt_RequireDate = (TextBox)fv_OrderDetail.FindControl("txt_RequireDate");
        CheckBox ckb_Status = (CheckBox)fv_OrderDetail.FindControl("ckb_Status");
        Label lbl_RequireDate = (Label)fv_OrderDetail.FindControl("lbl_rq");
        TextBox txt_OrderDate = (TextBox)fv_OrderDetail.FindControl("txt_OrderDate");
        
        if (txt_RequireDate.Text=="")
        {
            lbl_RequireDate.Text = "*";
            return;
        }
        DateTime dt_OrderDate = DateTime.Parse(txt_OrderDate.Text);
        DateTime dt_RequireDate = DateTime.Parse(txt_RequireDate.Text);

        TimeSpan ts_Day = dt_RequireDate - dt_OrderDate;

        if (ts_Day.Days < 0)
        {
            lbl_RequireDate.Text = "Nhập sai";
            return;
        }
        else
        {
            order.ApproveOrder(int.Parse(txt_OrderID.Text), txt_RequireDate.Text, true);
            txt_RequireDate.Enabled = false;
            fv_OrderDetail.DataBind();
            showData();
            lbl_Mess1.Text = "Đơn hàng này đã giao hàng !!!";
        } 
    }

    //Hủy duyệt đơn hàng
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        TextBox txt_OrderID = (TextBox)fv_OrderDetail.FindControl("txt_OrderID");
        TextBox txt_RequireDate = (TextBox)fv_OrderDetail.FindControl("txt_RequireDate");
        order.ApproveOrder(int.Parse(txt_OrderID.Text), "", false);
        txt_RequireDate.Enabled = true;
        fv_OrderDetail.DataBind();
        showData();
        lbl_Mess1.Text = "Đơn hàng này chưa giao hàng !!!";

    }

    //Gửi yêu cầu xuất kho
    protected void btn_Send_Click(object sender, EventArgs e)
    {
        order.SendExport(Session["UserID"].ToString(), RequireExportStorage(), DateTime.Now.ToString());
        lbl_Mess2.Text = "Phiếu yêu cầu xuất kho đã được gửi đi !!!";
    }

    //Hàm gửi yêu cầu xuất kho
    public string RequireExportStorage()
    {
        TextBox txt_OrderID = (TextBox)fv_OrderDetail.FindControl("txt_OrderID");
        DataTable tb_OrderInfo = order.ShowOrderInformation(txt_OrderID.Text);

        string str = "";
        Label lbl_RequireDate = new Label();
        Label lbl_UserID = new Label();
        Label lbl_FullName = new Label();
        lbl_UserID.Text = Session["UserID"].ToString();
        lbl_FullName.Text = Session["FullName"].ToString();
        lbl_RequireDate.Text = DateTime.Now.ToString();
        if (tb_OrderInfo.Rows.Count > 0)
        {
            str = " <table align='center' style='width: 750px; color: black; margin-left:30px; border-collapse:collapse; border-color: Black' border=1>" +
        "<tr><td rowspan=2 align=center><img src='Anh/fpt.jpg' height=100px width=150px /></td><td colspan=2 align=center style='font-size: 40px'><span style='font-size: 30px'><b>Phiếu yêu cầu xuất kho</b></span></td></tr>" +
                            "<tr><td align=left>Đơn hàng số: 000" + tb_OrderInfo.Rows[0]["Id_DonHang"] + "<br />Ngày lập phiếu: " + lbl_RequireDate.Text + "</td><td align=left>Mã nhân viên: " + lbl_UserID.Text + " <br />Họ tên: " + lbl_FullName.Text + "<br /></td></tr>" +
                            "<tr><td colspan=3 align=center>" +
                                    "</br><table style='width: 745px; color: black; border-collapse:collapse' border='1' borderColor='#213943'>" +
                                        "<tr style='font-weight:bold'><td width=62px>STT</td><td width=221px>Mã sản phẩm</td><td width=294px>Tên sản phẩm</td><td width=168px>Số lượng</td></tr>";
            for (int i = 0; i < tb_OrderInfo.Rows.Count; i++)
            {
                str = str + "<tr><td>" + (i + 1) + "</td><td>" + tb_OrderInfo.Rows[i]["Id_MatHang"] + "</td><td>" + tb_OrderInfo.Rows[i]["TenMatHang"] + "</td><td>" + tb_OrderInfo.Rows[i]["SoLuong"] + "</td></tr>";
            }
            str = str + 
                                    "</table></br>" +
                                "</td>" +
                            "</tr>" +
                            
                            "<tr><td align=center valign=top>Thủ kho<br /><i>(ký và ghi Họ Tên)</i></td><td align=center valign=top>Người giao hàng<br /><i>(ký và ghi Họ Tên)</i></td><td align=center>Người lập phiếu<br /><i>(ký và ghi Họ Tên)</i><br><br><br><br>" + lbl_FullName.Text + "</td></tr>" +
                        "</table>";
        }
        return str;
    }





    //Print
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Pn_Form;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        Response.Redirect("~/Print.aspx");
    }







    //phần bảo hành
    public void showGridviewWarranty()
    {
        grv_Warranty.DataSource = warranty.SelectWarranty();
        grv_Warranty.DataBind();
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (txt_Serial.Text == "")
        {
            lbl_Serial.Text = "*";
            lbl_EndDate.Text = "";
            return;
        }
        if (txt_End.Text == "")
        {
            lbl_EndDate.Text = "*";
            lbl_Serial.Text = "";
            return;
        }
        if (IsPostBack)
        {
            if (!warranty.CheckSerial(txt_Serial.Text))
            {
                lbl_Serial.Text = "Số serial này đã tồn tại !!!";
                lbl_EndDate.Text = "";
                lbl_ThongBao.Text = "";
                return;
            }
            DateTime dt_Begin = DateTime.Parse(txt_Begin.Text);
            DateTime dt_End = DateTime.Parse(txt_End.Text);

            TimeSpan ts_Day = dt_End - dt_Begin;

            if (ts_Day.Days < 0)
            {
                lbl_EndDate.Text = "Nhập sai";
                lbl_Serial.Text = "";
                return;
            }
            else
            {
                string IdProduct = warranty.ProductNameToProductId(txt_Product.Text);
                warranty.InsertWarranty(Convert.ToInt16(txt_MaBH.Text), txt_Serial.Text, Convert.ToDateTime(txt_Begin.Text), Convert.ToDateTime(txt_End.Text), Convert.ToInt16(IdProduct), Convert.ToInt16(txt_MaKH.Text));
                lbl_ThongBao.Text = "Thêm thành công";
                lbl_Serial.Text = "";
                txt_Serial.Text = "";
                txt_End.Text = "";
                lbl_EndDate.Text = "";
            }
            showGridviewWarranty();   
        }
        txt_MaBH.Text = (warranty.ShowIdBHMax() + 1).ToString();
    }

    public void grv_Warranty_RowEditing(object sender, GridViewEditEventArgs e)
    {
        txt_MaBH.Text = HttpUtility.HtmlDecode(grv_Warranty.Rows[e.NewEditIndex].Cells[0].Text);
        txt_Product.Text = HttpUtility.HtmlDecode(grv_Warranty.Rows[e.NewEditIndex].Cells[3].Text);
        txt_Serial.Text = HttpUtility.HtmlDecode(grv_Warranty.Rows[e.NewEditIndex].Cells[4].Text);
        txt_Begin.Text = HttpUtility.HtmlDecode(grv_Warranty.Rows[e.NewEditIndex].Cells[5].Text);
        txt_End.Text = HttpUtility.HtmlDecode(grv_Warranty.Rows[e.NewEditIndex].Cells[6].Text);
        btn_Add.Visible = false;
        btn_Update.Visible = true;
        lbl_ThongBao.Text = "";
        lbl_Serial.Text = "";
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        if (txt_Serial.Text == "")
        {
            lbl_Serial.Text = "*";
            return;
        }
        if (!warranty.CheckSerialUpdate(txt_MaBH.Text,txt_Serial.Text))
        {
            lbl_Serial.Text = "Số serial này đã tồn tại !!!";
            lbl_ThongBao.Text = "";
            return;
        }
        warranty.UpdateWarranty(txt_MaBH.Text, txt_Serial.Text, txt_End.Text);
        lbl_ThongBao.Text = "Cập nhật mã bảo hành số " + txt_MaBH.Text + " thành công !!!";
        txt_MaBH.Text = (warranty.ShowIdBHMax() + 1).ToString();
        btn_Add.Visible = true;
        btn_Update.Visible = false;
        showGridviewWarranty();
        txt_End.Text = "";
        txt_Serial.Text = "";
        txt_Begin.Text = DateTime.Now.ToString("MM/dd/yyyy");
        lbl_Serial.Text = "";
    }

    protected void grv_Warranty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            if (warranty.CheckWarrantyHasInWarrantyReceiptNote(grv_Warranty.Rows[index].Cells[0].Text))
                lbl_ThongBao.Text = "Mã bảo hành này hiện tại không thể xóa!";
            else
            {
                warranty.DeleteWarranty(grv_Warranty.Rows[index].Cells[0].Text);
                lbl_ThongBao.Text = "Đã xóa mã bảo hành số " + grv_Warranty.Rows[index].Cells[0].Text + " !!!";
                showGridviewWarranty();
            }
        }
        txt_MaBH.Text = (warranty.ShowIdBHMax() + 1).ToString();
    }

    protected void grv_Warranty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_Warranty.PageIndex = e.NewPageIndex;
        grv_Warranty.DataBind();
    }
}
