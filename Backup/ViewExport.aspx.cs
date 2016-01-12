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

public partial class ViewExport : System.Web.UI.Page
{
    csExport Export = new csExport();

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
        ShowListExport(ViewState["startdate"].ToString(), ViewState["enddate"].ToString());
    }

    void ShowListExport(string startdate, string enddate)
    {
        grv_ListExport.DataSource = Export.ShowExportByDate(startdate, enddate);
        grv_ListExport.DataBind();
    }

    public string ShowExportDetail(string idphieuxuat)
    {

        DataTable tb_ExportInfo = Export.ShowExportInformation(idphieuxuat);
        string str = "";
        if (tb_ExportInfo.Rows.Count > 0)
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
        else str = " <table style='width: 750px; color: black; margin-left:30px; border-collapse:collapse; border-color: Black' border=1>" +
                            "<tr><tdalign=center> Phiếu xuất này không có thông tin! </td><tr></table>";
        return str;
    }

    protected void grv_ListExport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_ListExport.PageIndex = e.NewPageIndex;
        ShowListExport(ViewState["startdate"].ToString(), ViewState["enddate"].ToString());
    }

    protected void grv_ListExport_RowEditing(object sender, GridViewEditEventArgs e)
    {
        tb_View.Visible = false;
        btn_Back.Visible = true;
        btn_Print.Visible = true;
        string idphieuxuat = grv_ListExport.Rows[e.NewEditIndex].Cells[0].Text;

        lbl_ViewDetail.Text = ShowExportDetail(idphieuxuat);
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        lbl_ViewDetail.Text = "";
        btn_Back.Visible = false;
        btn_Print.Visible = false;
        tb_View.Visible = true;
        txt_StartDate.Text = ViewState["startdate"].ToString();
        txt_EndDate.Text = ViewState["enddate"].ToString();
        ShowListExport(ViewState["startdate"].ToString(), ViewState["enddate"].ToString());
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = lbl_ViewDetail;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        Response.Redirect("~/Print.aspx");
    }
}
