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

public partial class MenuDoc_Admin : System.Web.UI.UserControl
{
    csMessageBox Mess = new csMessageBox();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckPermission();
        if (Session["Level"].ToString() == "Quản lý")
        {
            InvisibleAll();
            tb_QuanLy.Visible = true;
        }
        else if (Session["Level"].ToString() == "Thủ kho")
        {
            InvisibleAll();
            tb_ThuKho.Visible = true;
        }
        else if (Session["Level"].ToString() == "Nhân viên bán hàng")
        {
            InvisibleAll();
            tb_NhanVienBanHang.Visible = true;
        }
        else if (Session["Level"].ToString() == "Nhân viên bảo hành")
        {
            InvisibleAll();
            tb_NhanVienBaoHanh.Visible = true;
        }
    }

    void InvisibleAll()
    {
        tb_QuanLy.Visible = false;
        tb_ThuKho.Visible = false;
        tb_NhanVienBaoHanh.Visible = false;
        tb_NhanVienBanHang.Visible = false;
    }

    void CheckPermission()
    {
        if ((bool)Session["DaDangNhap"] == true)
        {
            if (Session["Level"].ToString() == "Khách hàng") Response.Redirect("default.aspx");
        }
        else Response.Redirect("default.aspx");
    }
}
