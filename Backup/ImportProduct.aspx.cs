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

public partial class ImportProduct : System.Web.UI.Page
{
    csDoiSoThanhChu NumberToString = new csDoiSoThanhChu();
    csImport Import = new csImport();
    csProvider Provider = new csProvider();
    csProduct Product = new csProduct();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbl_Code.Text = (Import.ShowIdPNMax() + 1).ToString();
            ViewState["ok1"] = false;
            ViewState["ok2"] = false;
        }
    }

    protected void txt_Provider_TextChanged(object sender, EventArgs e)
    {
        if(txt_Provider.Text != "") CheckProvider();
        ShowBtnAdd();
    }

    protected void txt_Product_TextChanged(object sender, EventArgs e)
    {
        if (txt_Product.Text != "") CheckProduct();
        lbl_ThongBao.Text = "";
        ShowBtnAdd();
    }

    protected void btn_RefreshProvider_Click(object sender, ImageClickEventArgs e)
    {
        CheckProvider();
    }

    protected void btn_RefreshProduct_Click(object sender, ImageClickEventArgs e)
    {
        CheckProduct();
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        string IdProvider = Provider.ProviderNameToProviderId(txt_Provider.Text);
        string IdProduct = Product.ProductNameToProductId(txt_Product.Text);

        if (Import.ShowIdPNMax().ToString() != lbl_Code.Text)
        {
            Import.ImportProduct(lbl_Code.Text, IdProvider, Session["UserID"].ToString(), DateTime.Now.ToString(), IdProduct, ddl_DonViTinh.Text, txt_Quantity.Text, txt_UnitPrice.Text, txt_Notes.Text);
            lbl_ThongBao.Text = "Thêm thành công";
        }
        else
        {
            if (Import.CheckImportProductDulicate(lbl_Code.Text, IdProduct))
            {
                int QuantityOld = Import.GetQuantityInImportDetail(lbl_Code.Text, IdProduct);
                int QuantityNew = QuantityOld + Convert.ToInt32(txt_Quantity.Text);
                Import.UpdateImportProductDetail(lbl_Code.Text, IdProduct, ddl_DonViTinh.Text, Convert.ToString(QuantityNew), txt_UnitPrice.Text, txt_Notes.Text);
                lbl_ThongBao.Text = "Cộng thêm số lượng cho mặt hàng "+txt_Product.Text+" thành công!";
            }
            else
            {
                Import.ImportProductMore(lbl_Code.Text, IdProduct, ddl_DonViTinh.Text, txt_Quantity.Text, txt_UnitPrice.Text, txt_Notes.Text);
                lbl_ThongBao.Text = "Thêm thành công !";
            }
        }
        ShowGrvImport();
        btn_Finish.Visible = true;
        btn_Cancel.Visible = true;
        txt_Provider.ReadOnly = true;
    }

    void CheckProduct()
    {
        if (!Product.CheckProductName(txt_Product.Text))
        {
            lbl_Product.Text = "<br><a href='ManagementProduct.aspx' target='_blank'>Click</a> để cung cấp thông tin cho sản phầm này?";
            btn_RefreshProduct.Visible = true;
            ViewState["ok1"] = false;
        }
        else
        {
            lbl_Product.Text = "<span style='color: green;'>Ok!</span>";
            btn_RefreshProduct.Visible = false;
            ViewState["ok1"]= true;
        }
    }

    void CheckProvider()
    {
        if (!Provider.CheckProviderName(txt_Provider.Text))
        {
            lbl_Provider.Text = "Chưa có thông tin về Nhà cung cấp này. Bấm vào <a href='ManagementProvider.aspx' target='_blank'>đây</a> để bổ sung?";
            btn_RefreshProvider.Visible = true;
            ViewState["ok2"] = false;
        }
        else
        {
            lbl_Provider.Text = "<span style='color: green;'>Ok!</span>";
            btn_RefreshProvider.Visible = false;
            ViewState["ok2"] = true;
        }
    }

    void ShowBtnAdd()
    {
        if ((bool)ViewState["ok1"] && (bool)ViewState["ok2"]) btn_Add.Visible = true;
        else btn_Add.Visible = false;
    }

    void ShowGrvImport()
    {
        grv_Import.DataSource = Import.SelectImportDetail(lbl_Code.Text);
        grv_Import.DataBind();
    }

    protected void grv_Import_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Import.DeleteImportDetail(Product.ProductNameToProductId(grv_Import.Rows[index].Cells[0].Text), lbl_Code.Text);
            lbl_ThongBao.Text = "Xóa thành công!!!";
            ShowGrvImport();
        }
    }

    protected void btn_Finish_Click(object sender, EventArgs e)
    {
        tb_Code.Visible = false;
        tb_grvImport.Visible = false;
        tb_ImportDetail.Visible = false;
        btn_Print.Visible = true;
        
        int row = grv_Import.Rows.Count;
        int QuantityNow;
        int QuantityNew;
        string idproduct;
        for (int i = 0; i < row; i++)
        {
            idproduct = Product.ProductNameToProductId(grv_Import.Rows[i].Cells[0].Text);
            QuantityNow = Product.ShowQuantityOfProductNow(idproduct);
            QuantityNew = QuantityNow + Convert.ToInt32(grv_Import.Rows[i].Cells[3].Text);
            Product.UpdateQuantityOfProduct(idproduct, QuantityNew);
        }

        lbl_ReceiptNote.Text = PrintReceiptNote();
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Import.DeleteAllImportDetail(lbl_Code.Text);
        ShowGrvImport();
        btn_Cancel.Visible = false;
        btn_Finish.Visible = false;
        txt_Provider.ReadOnly = false;
        txt_Provider.Text = "";
        txt_Product.Text = "";
        txt_Quantity.Text = "";
        txt_UnitPrice.Text = "";
        txt_Notes.Text = "";
        lbl_ThongBao.Text = "";
    }

    public string PrintReceiptNote()
    {
        DataTable tb_ImportInfo = Import.ShowImportInformation(lbl_Code.Text);
        int amount = 0;
        int total = 0;
        string str = "";
        if (grv_Import.Rows.Count > 0)
        {
            str = " <table style='width: 750px; color: black; margin-left:30px; border-collapse:collapse; border-color: Black' border=1>" +
                            "<tr><td rowspan=2 align=center><img src='Anh/fpt.jpg' height=100px width=150px /></td><td colspan=2 align=center style='font-size: 40px'><span style='font-size: 30px'><b>PHIẾU NHẬP KHO</b></span></td></tr>" +
                            "<tr><td valign=top>Số: NH000" + tb_ImportInfo.Rows[0]["Id_PhieuNhap"] + "<br />Ngày:" + tb_ImportInfo.Rows[0]["NgayNhap"] + "</td><td>Nhà cung cấp: " + tb_ImportInfo.Rows[0]["TenNhaCungCap"] + "<br />Địa chỉ: " + tb_ImportInfo.Rows[0]["DiaChi"] + "<br />Điện thoại: " + tb_ImportInfo.Rows[0]["Phone"] + "</td></tr>" +
                            "<tr><td colspan=3 align=center>" +
                                    "<table style='width: 730px; color: black; border-collapse:collapse' border='1' borderColor='#213943'>" +
                                        "<tr><td><b>No</b></td><td><b>Mã mặt hàng</b></td><td><b>Tên mặt hàng</b></td><td><b>Đơn vị tính</b></td><td><b>Số lượng</b></td><td><b>Đơn giá</b></td><td><b>Thành tiền</b></td><td><b>Ghi chú</b></td></tr>";
            for (int i = 0; i < tb_ImportInfo.Rows.Count; i++)
            {
                amount = Convert.ToInt32(tb_ImportInfo.Rows[i]["SoLuong"]) * Convert.ToInt32(tb_ImportInfo.Rows[i]["DonGia"]);
                str = str + "<tr><td>" + (i + 1) + "</td><td>" + tb_ImportInfo.Rows[i]["Id_MatHang"] + "</td><td>" + tb_ImportInfo.Rows[i]["TenMatHang"] + "</td><td>" + tb_ImportInfo.Rows[i]["DonViTinh"] + "</td><td>" + tb_ImportInfo.Rows[i]["SoLuong"] + "</td><td>" + string.Format("{0:N0}",tb_ImportInfo.Rows[i]["DonGia"]) + " vnđ</td><td>" + string.Format("{0:N0}",amount) + "</td><td>" + tb_ImportInfo.Rows[i]["GhiChu"] + "</td></tr>";
                total = total + amount;
            }

            str = str + "<tr><td colspan=6 align=right>Tổng: </td><td colspan=2><b>" + string.Format("{0:N0}",total) + " vnđ</b></td></tr>" +
                                    "</table>" +
                                "</td>" +
                            "</tr>" +
                            "<tr><td colspan=3>Tổng tiền bằng chữ: <b>" + NumberToString.converNumToString(NumberToString.slipArray(total.ToString())) + " đồng.</b></td></tr>" +
                            "<tr><td align=center valign=top>Thủ kho<br /><i>(ký và ghi Họ Tên)</i></td><td align=center valign=top>Người giao hàng<br /><i>(ký và ghi Họ Tên)</i></td><td align=center>Người lập phiếu<br /><i>(ký và ghi Họ Tên)</i><br><br><br><br>" + tb_ImportInfo.Rows[0]["HoTen"] + "</td></tr>" +
                        "</table>";
        }
        return str;
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = lbl_ReceiptNote;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        Response.Redirect("~/Print.aspx");
    }
}
