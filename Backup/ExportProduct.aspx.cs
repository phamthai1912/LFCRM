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

public partial class ExportProduct : System.Web.UI.Page
{
    csExport Export = new csExport();
    csProduct Product = new csProduct();
    csCatalogue Catalog = new csCatalogue();
    csProduction Production = new csProduction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbl_Code.Text = (Export.ShowIdPXMax() + 1).ToString();
            ViewState["ok1"] = false;
            ViewState["ok2"] = false;
        }
    }

    protected void txt_Product_TextChanged(object sender, EventArgs e)
    {
        if (txt_Product.Text != "") CheckProduct();
        lbl_ThongBao.Text = "";
        ShowBtnAdd();
    }

    protected void txt_Quantity_TextChanged(object sender, EventArgs e)
    {
        if (txt_Quantity.Text != "") CheckQuantity();
        ShowBtnAdd();
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txt_Quantity.Text) > Convert.ToInt32(lbl_TonTrongKho.Text)) lbl_ThongBao.Text = "Số lượng trong kho không đủ!";
        else
        {
            if (Export.ShowIdPXMax().ToString() != lbl_Code.Text)
            {
                Export.ExportProduct(lbl_Code.Text, Session["UserID"].ToString(), DateTime.Now.ToString(), lbl_MaSp.Text, ddl_DonViTinh.Text, txt_Quantity.Text, txt_Notes.Text);
                lbl_ThongBao.Text = "Thêm thành công!";
            }
            else
            {
                if (Export.CheckExportProductDulicate(lbl_Code.Text, lbl_MaSp.Text))
                {
                    int QuantityOld = Export.GetQuantityInExportDetail(lbl_Code.Text, lbl_MaSp.Text);
                    int QuantityNew = QuantityOld + Convert.ToInt32(txt_Quantity.Text);
                    Export.UpdateImportProductDetail(lbl_Code.Text, lbl_MaSp.Text,ddl_DonViTinh.Text, QuantityNew, txt_Notes.Text);
                    lbl_ThongBao.Text = "Cộng thêm số lượng cho mặt hàng " + txt_Product.Text + " thành công!";
                }
                else
                {
                    Export.ExportProductMore(lbl_Code.Text, lbl_MaSp.Text, ddl_DonViTinh.Text,txt_Quantity.Text, txt_Notes.Text);
                    lbl_ThongBao.Text = "Thêm thành công!";
                }
            }
        }
        ShowGrvExport();
        ShowInfoProduct();
        btn_Finish.Visible = true;
        btn_Cancel.Visible = true;
    }

    void CheckQuantity()
    {
        if (Convert.ToInt32(txt_Quantity.Text) > Convert.ToInt32(lbl_TonTrongKho.Text))
        {
            lbl_ThongBao.Text = "Số lượng trong kho không đủ!";
            ViewState["ok1"] = false;
        }
        else
        {
            lbl_ThongBao.Text = "";
            ViewState["ok1"] = true;
        }
    }

    void CheckProduct()
    {
        if (!Product.CheckProductName(txt_Product.Text))
        {
            lbl_Product.Text = "<br>Mặt hàng này hiện không có!";
            lbl_MaSp.Text = "";
            lbl_TonTrongKho.Text = "";
            lbl_LoaiHang.Text = "";
            lbl_NhaSanXuat.Text = "";
            ViewState["ok2"] = false;
        }
        else
        {
            lbl_Product.Text = "<span style='color: green;'>Ok!</span>";
            ViewState["ok2"] = true;
            ShowInfoProduct();
        }
    }

    void ShowBtnAdd()
    {
        if ((bool)ViewState["ok1"] && (bool)ViewState["ok2"]) btn_Add.Visible = true;
        else btn_Add.Visible = false;
    }

    void ShowGrvExport()
    {
        grv_Export.DataSource = Export.SelectExportDetail(lbl_Code.Text);
        grv_Export.DataBind();
    }

    protected void grv_Export_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Export.DeleteExportDetail(grv_Export.Rows[index].Cells[0].Text, lbl_Code.Text);
            lbl_ThongBao.Text = "Xóa thành công!!!";
            ShowGrvExport();
            ShowInfoProduct();
        }
    }

    void ShowInfoProduct()
    {
        lbl_MaSp.Text = Product.ProductNameToProductId(txt_Product.Text);
        lbl_TonTrongKho.Text = (Product.ShowQuantityOfProductNow(lbl_MaSp.Text) - Export.GetQuantityInExportDetail(lbl_Code.Text, lbl_MaSp.Text)).ToString();
        lbl_LoaiHang.Text = Catalog.GetCatalogueName(lbl_MaSp.Text);
        lbl_NhaSanXuat.Text = Production.GetProductionName(lbl_MaSp.Text);
    }

    protected void btn_Finish_Click(object sender, EventArgs e)
    {
        tb_Export.Visible = false;
        lbl_DeliveryNote.Text = PrintDeliveryNote();
        btn_Print.Visible = true;

        int row = grv_Export.Rows.Count;
        int QuantityNow;
        int QuantityNew;
        string idproduct;
        for (int i = 0; i < row; i++)
        {
            idproduct = grv_Export.Rows[i].Cells[0].Text;
            QuantityNow = Product.ShowQuantityOfProductNow(idproduct);
            QuantityNew = QuantityNow - Convert.ToInt32(grv_Export.Rows[i].Cells[3].Text);
            Product.UpdateQuantityOfProduct(idproduct, QuantityNew);
        }
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Export.DeleteAllExportDetail(lbl_Code.Text);
        ShowGrvExport();
        btn_Cancel.Visible = false;
        btn_Finish.Visible = false;
        txt_Product.Text = "";
        txt_Quantity.Text = "";
        txt_Notes.Text = "";
        lbl_ThongBao.Text = "";
    }

    public string PrintDeliveryNote()
    {
        DataTable tb_ExportInfo = Export.ShowExportInformation(lbl_Code.Text);
        string str = "";
        if (grv_Export.Rows.Count > 0)
        {
            str = " <table style='width: 750px; color: black; margin-left:30px; border-collapse:collapse; border-color: Black' border=1>" +
                            "<tr><td rowspan=2 align=center><img src='Anh/fpt.jpg' height=100px width=150px /></td><td colspan=2 align=center style='font-size: 40px'><span style='font-size: 30px'><b>PHIẾU XUẤT KHO</b></span></td></tr>" +
                            "<tr><td valign=top colspan=2>Số: XH000" + tb_ExportInfo.Rows[0]["Id_PhieuXuat"] + "<br />Ngày:" + tb_ExportInfo.Rows[0]["NgayXuat"] + "</td></tr>" +
                            "<tr><td colspan=3 align=center>" +
                                    "<table style='width: 730px; color: black; border-collapse:collapse' border='1' borderColor='#213943'>" +
                                        "<tr><td><b>No</b></td><td><b>Mã mặt hàng</b></td><td><b>Loại hàng</b></td><td><b>Nhà sản xuất</b></td><td><b>Tên mặt hàng</b></td><td><b>Đơn vị tính</b></td><td><b>Số lượng</b></td><td><b>Ghi chú</b></td></tr>";
            for (int i = 0; i < tb_ExportInfo.Rows.Count; i++)
                str = str + "<tr><td>" + (i + 1) + "</td><td>" + tb_ExportInfo.Rows[i]["Id_MatHang"] + "</td><td>" + tb_ExportInfo.Rows[i]["LoaiHang"] + "</td><td>" + tb_ExportInfo.Rows[i]["NhaSanXuat"] + "</td><td>" + tb_ExportInfo.Rows[i]["TenMatHang"] + "</td><td>" + tb_ExportInfo.Rows[i]["DonViTinh"] + "</td><td>" + tb_ExportInfo.Rows[i]["SoLuong"] + "</td><td>" + tb_ExportInfo.Rows[i]["GhiChu"] + "</td></tr>";

            str = str + "</table>" +
                                "</td>" +
                            "</tr>" +
                            "<tr><td align=center valign=top>Thủ kho<br /><i>(ký và ghi Họ Tên)</i></td><td align=center valign=top>Người nhận hàng<br /><i>(ký và ghi Họ Tên)</i></td><td align=center>Người lập phiếu<br /><i>(ký và ghi Họ Tên)</i><br><br><br><br>" + tb_ExportInfo.Rows[0]["HoTen"] + "</td></tr>" +
                        "</table>";
        }
        return str;
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = lbl_DeliveryNote;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        Response.Redirect("~/Print.aspx");
    }
}
