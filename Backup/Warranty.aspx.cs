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
using System.Drawing;
using System.IO;

public partial class Warranty : System.Web.UI.Page
{
    csLogin login = new csLogin();
    csOrder order = new csOrder();
    csProduct Product = new csProduct();
    csWarranty warranty = new csWarranty();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ddl_MaBH.AutoPostBack == true)
        {
            btn_Duyet.Visible = false;
            btn_Huy.Visible = false;
        }
        lbl_Mess.Text = "";
        lbl_MessSerial.Text = "";
        txt_NgayNhan.Text = DateTime.Now.ToString("MM/dd/yyyy");
        showWarrantyReceipt();
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {

        if (pn_LapPhieuNhap.Visible == true)
        {
            pn_LapPhieuNhap.Visible = false;
            btn_Back.Visible = false;
            pn_Xulyphieunhan.Visible = false;

            pn_FindForm.Visible = true;
            lbl_Mess0.Text = "";
            txt_NgayTra.Text = null;
            grv_PhieuNhanBH.DataSource = null;
            grv_PhieuNhanBH.DataBind();
            return;
        }
        if (pn_Xulyphieunhan.Visible == true)
        {
            btn_Back.Visible = false;
            pn_Xulyphieunhan.Visible = false;

            btn_XuLyPhieuNhan.Visible = true;
            pn_FindForm.Visible = true;
            grv_XuLyPhieuNhan.DataSource = null;
            grv_XuLyPhieuNhan.DataBind();
            lbl_Mess2.Text = "";
            return;
        }
    }

    public void showXuLyPhieuNhan()
    {
        grv_XuLyPhieuNhan.DataSource = warranty.ShowWarrantyReceiptInformation(ddl_MaBH.SelectedValue);
        grv_XuLyPhieuNhan.DataBind();
    }

    public void showFindSerial()
    {
        grv_FindWarranty.DataSource = warranty.FindSerialWarranty(txt_FindSerial.Text);
        grv_FindWarranty.DataBind();
    }

    public void check()
    {
        if (txt_FindSerial.Text == "")
        {
            lbl_MessSerial.Text = "*";
            return;
        }

        if (warranty.CheckSerial(txt_FindSerial.Text))
        {
            lbl_Mess.Text = "Số serial này không tồn tại !!!";
            btn_LapHDBH.Visible = false;
            return;
        }
    }

    protected void btn_View_Click(object sender, EventArgs e)
    {
        check();
        DataTable tb_FindReceiptDateFromSerial = warranty.FindWarrantyIDandReceiptDateFromSerial(txt_FindSerial.Text);

        Label lbl_ReceiptDate = new Label();
        lbl_ReceiptDate.Text = tb_FindReceiptDateFromSerial.Rows[0]["NgayHetHan"].ToString();

        Label lbl_Now = new Label();
        lbl_Now.Text = DateTime.Now.ToString();

        DateTime dt_ReceiptDate = DateTime.Parse(lbl_ReceiptDate.Text);
        DateTime dt_Now = DateTime.Parse(lbl_Now.Text);
        TimeSpan ts_Day = dt_Now - dt_ReceiptDate;

        if (ts_Day.Days > 0)
        {
            lbl_Mess.Text = "<b>Sản phẩm này đã hết hạn bảo hành !!!</b>";
            btn_LapHDBH.Visible = false;
            pn_LapPhieuNhap.Visible = false;
        }
        else
        {
            lbl_Mess.Text = "";
            btn_LapHDBH.Visible = true;
        }

        showFindSerial();
        lbl_MessSerial.Text = "";
    }

    protected void btn_LapHDBH_Click(object sender, EventArgs e)
    {
        showFindSerial();
        check();
        DataTable tb_FindReceiptDateFromSerial = warranty.FindWarrantyIDandReceiptDateFromSerial(txt_FindSerial.Text);

        Label lbl_ReceiptDate = new Label();
        lbl_ReceiptDate.Text = tb_FindReceiptDateFromSerial.Rows[0]["NgayHetHan"].ToString();

        Label lbl_Now = new Label();
        lbl_Now.Text = DateTime.Now.ToString();

        DateTime dt_ReceiptDate = DateTime.Parse(lbl_ReceiptDate.Text);
        DateTime dt_Now = DateTime.Parse(lbl_Now.Text);
        TimeSpan ts_Day = dt_Now - dt_ReceiptDate;

        if (ts_Day.Days > 0)
        {
            lbl_Mess.Text = "<b>Sản phẩm này đã hết hạn bảo hành !!!</b>";
            btn_LapHDBH.Visible = false;
            pn_LapPhieuNhap.Visible = false;
            return;
        }
        else
        {
            lbl_Mess.Text = "";
            pn_LapPhieuNhap.Visible = true;
            btn_Back.Visible = true;
            pn_FindForm.Visible = false;
        }
        
    }

    public void showWarrantyReceipt()
    {
        grv_PhieuNhanBH.DataSource = warranty.ShowWarrantyReceipt();
        grv_PhieuNhanBH.DataBind();
    }
    protected void btn_Them_Click(object sender, EventArgs e)
    {
        if (txt_NgayTra.Text == "")
        {
            lbl_Mess0.Text = "*";
            return;
        }
        DateTime dt_NgayNhan = DateTime.Parse(txt_NgayNhan.Text);
        DateTime dt_NgayTra = DateTime.Parse(txt_NgayTra.Text);

        TimeSpan ts_Day = dt_NgayTra - dt_NgayNhan;

        if (ts_Day.Days < 0)
        {
            lbl_Mess1.Text = "Nhập sai";
            return;
        }
        DataTable tb_FindWarrantyIdFromSerial = warranty.FindWarrantyIDandReceiptDateFromSerial(txt_FindSerial.Text);
        Label lbl_WarrantyId = new Label();
        lbl_WarrantyId.Text = tb_FindWarrantyIdFromSerial.Rows[0]["ID_BaoHanh"].ToString();
        if (warranty.CheckInsertReceipt(lbl_WarrantyId.Text))
            lbl_Mess1.Text = "Phiếu nhận có số serial này hiện tại đã lưu trong hệ thống !!!";
        else
        {
            warranty.InsertReceipt(txt_NgayNhan.Text, txt_NgayTra.Text, lbl_WarrantyId.Text);
            lbl_Mess1.Text = "Thêm thành công !!!";
        }
        showWarrantyReceipt();
    }

    protected void btn_Sua_Click(object sender, EventArgs e)
    {
        if (txt_NgayTra.Text == "")
        {
            lbl_Mess0.Text = "*";
            return;
        }
        else
        {
            warranty.UpdateReturnDateReceiptWarranty(lbl_MaPhieuNhan.Text, txt_NgayTra.Text);
            showWarrantyReceipt();
            lbl_Mess1.Text = "Sửa thành công !!!";
            btn_Sua.Visible = false;
            btn_Them.Visible = true;
        }
    }

    protected void grv_PhieuNhanBH_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            if (warranty.CheckDeleteReceiptWarranty(grv_PhieuNhanBH.Rows[index].Cells[0].Text))
                lbl_Mess1.Text = "Phiếu này đã được duyệt bạn không thể xóa !!!";
            else
            {
                warranty.DeleteReceiptWarranty(grv_PhieuNhanBH.Rows[index].Cells[0].Text);
                lbl_Mess1.Text = "Xóa phiếu nhận thành công !!!";
            }
            showWarrantyReceipt();
        }
    }

   

    protected void grv_PhieuNhanBH_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lbl_MaPhieuNhan.Text = HttpUtility.HtmlDecode(grv_PhieuNhanBH.Rows[e.NewEditIndex].Cells[0].Text);
        txt_NgayTra.Text = HttpUtility.HtmlDecode(grv_PhieuNhanBH.Rows[e.NewEditIndex].Cells[4].Text);
        btn_Sua.Visible = true;
        btn_Them.Visible = false;
    }

    protected void grv_PhieuNhanBH_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_PhieuNhanBH.PageIndex = e.NewPageIndex;
        showWarrantyReceipt();
    }




    protected void btn_XuLyPhieuNhan_Click(object sender, EventArgs e)
    {
        pn_Xulyphieunhan.Visible = true;
        btn_Back.Visible = true;
        pn_LapPhieuNhap.Visible = false;
        pn_FindForm.Visible = false;
        btn_XuLyPhieuNhan.Visible = false;
        
    }
    protected void btn_Xem_Click(object sender, EventArgs e)
    {
        if (warranty.CheckReceiptNote(ddl_MaBH.SelectedValue))
        {
            btn_Duyet.Visible = true;
            btn_Huy.Visible = false;
        }
        else
        {
            btn_Duyet.Visible = false;
            btn_Huy.Visible = true;
        }
        showXuLyPhieuNhan();
    }
    protected void btn_Duyet_Click(object sender, EventArgs e)
    {
        warranty.UpdateReceiptWarrantyNote(ddl_MaBH.SelectedValue, "OK");
        lbl_Mess2.Text = "Đã duyệt phiếu nhận !!!";
        btn_Duyet.Visible = false;
        btn_Huy.Visible = true;
        showXuLyPhieuNhan();
    }
    protected void btn_Huy_Click(object sender, EventArgs e)
    {
        warranty.UpdateReceiptWarrantyNote(ddl_MaBH.SelectedValue, "NO");
        lbl_Mess2.Text = "Phiếu nhận chưa được duyệt !!!";
        btn_Duyet.Visible = true;
        btn_Huy.Visible = false;
        showXuLyPhieuNhan();
    }
}
