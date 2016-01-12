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

public partial class StatisticWarranty : System.Web.UI.Page
{
    csStatisticWarranty warr = new csStatisticWarranty();
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
        lbl_ThongKe.Text = Statistic();
        Pn_btnPrint.Visible = true;
    }

    public string Statistic()
    {
        DataTable tb_StatisticWarranty = warr.StatisticWarranty(txt_StartDate.Text, txt_EndDate.Text);
        Label lbl_FullName = new Label();
        lbl_FullName.Text = Session["FullName"].ToString();
        string str = "<table style='width: 770px; color: black; border-collapse:collapse; border-color: Black' border=1>" 
                        + "<tr><td align=center><img src='Anh/fpt.jpg' height=100px width=150px /></td><td align=center><span style='font-size: 25px'><b>THỐNG KÊ BẢO HÀNH</b></span><br /><i>Từ ngày: " + txt_StartDate.Text + " đến ngày: " + txt_EndDate.Text + "</i></td></tr>" 
                        +"<tr><td colspan=2 align=center>";

        str = str + "<br><table style='width: 750px; color: black; border-collapse:collapse; border-color: Black' border='1'> "
                           + "<tr><td><b>STT</b></td><td><b>Mã bảo hành</b></td><td><b>Tên máy</b></td><td><b>Serial</b></td><td><b>Ngày nhận</b></td><td><b>Ngày trả</b></td><td><b>Ghi chú</b></td></tr>";
        for (int i = 0; i < tb_StatisticWarranty.Rows.Count; i++)
        {
            str = str + "<tr><td>" + (i + 1) + "</td><td>"
                + tb_StatisticWarranty.Rows[i]["ID_PhieuBaoHanh"] + "</td><td>"
                + tb_StatisticWarranty.Rows[i]["TenMatHang"] + "</td><td>"
                + tb_StatisticWarranty.Rows[i]["Serial"] + "</td><td>"
                + tb_StatisticWarranty.Rows[i]["NgayNhan"] + "</td><td>"
                + tb_StatisticWarranty.Rows[i]["NgayTra"] + "</td><td>"
                + tb_StatisticWarranty.Rows[i]["GhiChu"] + "</td></tr>";
        }
        str = str + "</table></br></td></tr>";


        str = str + "<tr><td align=center valign=top>Quản lý<br/><i>(ký và ghi Họ Tên)</i></td><td align=center>Người lập phiếu<br /><i>(ký và ghi Họ Tên)</i><br><br><br><br>" + lbl_FullName.Text + "</td></tr>"
                + "</table>";
        return str;
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pn_lbl_ThongKe;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        Response.Redirect("~/Print.aspx");
    }
}
