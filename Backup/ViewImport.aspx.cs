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

public partial class ViewImport : System.Web.UI.Page
{
    csImport Import = new csImport();
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
        ViewState["startdate"] = txt_StartDate.Text;
        ViewState["enddate"] = txt_EndDate.Text;
        ShowListImport(ViewState["startdate"].ToString(), ViewState["enddate"].ToString());
    }

    void ShowListImport(string startdate, string enddate)
    {
        grv_ListImport.DataSource = Import.ShowImportByDate(startdate, enddate);
        grv_ListImport.DataBind();
    }

    public string ShowImportDetail( string idphieunhap)
    {

        DataTable tb_ImportInfo = Import.ShowImportInformation(idphieunhap);
        int amount = 0;
        int total = 0;
        string str = "";
        if (tb_ImportInfo.Rows.Count > 0)
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
                str = str + "<tr><td>" + (i + 1) + "</td><td>" + tb_ImportInfo.Rows[i]["Id_MatHang"] + "</td><td>" + tb_ImportInfo.Rows[i]["TenMatHang"] + "</td><td>" + tb_ImportInfo.Rows[i]["DonViTinh"] + "</td><td>" + tb_ImportInfo.Rows[i]["SoLuong"] + "</td><td>" + string.Format("{0:N0}",tb_ImportInfo.Rows[i]["DonGia"]) + "</td><td>" + string.Format("{0:N0}",amount) + "</td><td>" + tb_ImportInfo.Rows[i]["GhiChu"] + "</td></tr>";
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
        else str = " <table style='width: 750px; color: black; margin-left:30px; border-collapse:collapse; border-color: Black' border=1>" +
                            "<tr><tdalign=center> Phiếu nhập này không có thông tin! </td><tr></table>";
        return str;
    }

    protected void grv_ListImport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_ListImport.PageIndex = e.NewPageIndex;
        ShowListImport(ViewState["startdate"].ToString(), ViewState["enddate"].ToString());
    }

    protected void grv_ListImport_RowEditing(object sender, GridViewEditEventArgs e)
    {
        tb_View.Visible = false;
        btn_Back.Visible = true;
        btn_Print.Visible = true;
        string idphieunhap = grv_ListImport.Rows[e.NewEditIndex].Cells[0].Text;

        lbl_ViewDetail.Text = ShowImportDetail(idphieunhap);
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        lbl_ViewDetail.Text = "";
        btn_Back.Visible = false;
        btn_Print.Visible = false;
        tb_View.Visible = true;
        txt_StartDate.Text = ViewState["startdate"].ToString();
        txt_EndDate.Text = ViewState["enddate"].ToString();
        ShowListImport(ViewState["startdate"].ToString(), ViewState["enddate"].ToString());
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = lbl_ViewDetail;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        Response.Redirect("~/Print.aspx");
    }
}
