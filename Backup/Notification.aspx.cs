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

public partial class Notification : System.Web.UI.Page
{
    csNotification Notify = new csNotification();

    protected void Page_Load(object sender, EventArgs e)
    {
        ShowGrvNotifyGoIn();
        ShowGrvNotifyGoOut();
    }

    void ShowGrvNotifyGoIn()
    {
        grv_ThongBaoDen.DataSource = Notify.ShowNotifGoIn(Session["UserID"].ToString());
        grv_ThongBaoDen.DataBind();

        lbl_NumMess.Text = Convert.ToString(NumNotifi(Notify.ShowNotifGoIn(Session["UserID"].ToString())));
    }

    void ShowGrvNotifyGoOut()
    {
        grv_ThongBaoDi.DataSource = Notify.ShowNotifGoOut(Session["UserID"].ToString());
        grv_ThongBaoDi.DataBind();
    }

    protected void grv_ThongBaoDen_RowEditing(object sender, GridViewEditEventArgs e)
    {
        tb_ThongBaoDen.Visible = false;
        tb_ThongBaoDi.Visible = false;
        tb_noidung.Visible = true;

        string idthongbao = grv_ThongBaoDen.Rows[e.NewEditIndex].Cells[0].Text;
        Notify.UpdateStatus(idthongbao);
        lbl_noidung.Text = LoadContentGoIn(idthongbao);
    }

    protected void grv_ThongBaoDi_RowEditing(object sender, GridViewEditEventArgs e)
    {
        tb_ThongBaoDen.Visible = false;
        tb_ThongBaoDi.Visible = false;
        tb_noidung.Visible = true;

        string idthongbao = grv_ThongBaoDi.Rows[e.NewEditIndex].Cells[0].Text;
        lbl_noidung.Text = LoadContentGoOut(idthongbao);
    }

    public string LoadContentGoIn(string idthongbao)
    {
        DataTable tb_ContentGoIn = Notify.ShowInfoGoIn(idthongbao);
        string str = "<table style='width: 770px; color: black; border-collapse:collapse; border-color: Black' border=1> "+
                        "<tr><td style='width:100px'><b>Người gởi: </b>" + tb_ContentGoIn.Rows[0]["HoTen"] + "</td><td style='width:100px'><b>Ngày gởi: </b>" + tb_ContentGoIn.Rows[0]["NgayGoi"] + "</td></tr> " +
                        "<tr><td colspan=2><b>Tiêu đề: </b>" + tb_ContentGoIn.Rows[0]["TieuDe"] + "</td></tr> " +
                        "<tr><td colspan=2>" + tb_ContentGoIn.Rows[0]["Noidung"] + "</td></tr> " +
                    "</table>";

        return str;
    }

    public string LoadContentGoOut(string idthongbao)
    {
        DataTable tb_ContentGoOut = Notify.ShowInfoGoOut(idthongbao);
        string str = "<table style='width: 770px; color: black; border-collapse:collapse; border-color: Black' border=1> " +
                        "<tr><td style='width:100px'><b>Người gởi: </b>" + tb_ContentGoOut.Rows[0]["HoTen"] + "</td><td style='width:100px'><b>Ngày gởi: </b>" + tb_ContentGoOut.Rows[0]["NgayGoi"] + "</td></tr> " +
                        "<tr><td colspan=2><b>Tiêu đề: </b>" + tb_ContentGoOut.Rows[0]["TieuDe"] + "</td></tr> " +
                        "<tr><td colspan=2>" + tb_ContentGoOut.Rows[0]["Noidung"] + "</td></tr> " +
                    "</table>";

        return str;
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        tb_ThongBaoDen.Visible = true;
        tb_ThongBaoDi.Visible = true;
        tb_noidung.Visible = false;
    }

    public int NumNotifi(DataTable Notifi)
    {
        int k=0;

        for (int i = 0; i < Notifi.Rows.Count; i++)
            if (Convert.ToString(Notifi.Rows[i]["TrangThai"]) == "new.gif") k++;
        return k;

    }

    protected void grv_ThongBaoDen_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_ThongBaoDen.PageIndex = e.NewPageIndex;
        ShowGrvNotifyGoIn();
    }

    protected void grv_ThongBaoDi_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_ThongBaoDi.PageIndex = e.NewPageIndex;
        ShowGrvNotifyGoOut();
    }
}
