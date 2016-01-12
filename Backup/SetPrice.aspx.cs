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

public partial class SetPrice : System.Web.UI.Page
{
    csPrice price = new csPrice();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ddl_Loaihang_SelectedIndexChanged(object sender, EventArgs e)
    {
        grv_Setprice.DataSource = price.ShowPriceByCatalog(ddl_Loaihang.Text);
        grv_Setprice.DataBind();
    }

    protected void ddl_NhaSanXuat_SelectedIndexChanged(object sender, EventArgs e)
    {
        grv_Setprice.DataSource = price.ShowPriceByCatalogAndProduction(ddl_Loaihang.Text, ddl_NhaSanXuat.Text);
        grv_Setprice.DataBind();
    }

    protected void btn_ThietLap_Click(object sender, EventArgs e)
    {
        price.UpdatePrice(lbl_Masp.Text, txt_GiaBanMoi.Text);
        lbl_ThongBao.Text = "Cập nhập giá thành công!";
        if (ddl_NhaSanXuat.Text == "")
        {
            grv_Setprice.DataSource = price.ShowPriceByCatalog(ddl_Loaihang.Text);
            grv_Setprice.DataBind();
        }
        else
        {
            grv_Setprice.DataSource = price.ShowPriceByCatalogAndProduction(ddl_Loaihang.Text, ddl_NhaSanXuat.Text);
            grv_Setprice.DataBind();
        }
    }

    protected void btn_Huy_Click(object sender, EventArgs e)
    {
        txt_GiaBanMoi.Text = "";
        lbl_Masp.Text = "";
        lbl_ThongBao.Text = "";
        btn_ThietLap.Visible = false;
    }

    protected void grv_Setprice_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lbl_Masp.Text = grv_Setprice.Rows[e.NewEditIndex].Cells[0].Text;
        txt_GiaBanMoi.Text = grv_Setprice.Rows[e.NewEditIndex].Cells[4].Text;
        btn_ThietLap.Visible = true;
    }
}
