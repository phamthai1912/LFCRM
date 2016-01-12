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

public partial class ManagerCatalogue : System.Web.UI.Page
{
    csCatalogue catalog = new csCatalogue();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_Mess.Text = "";
        showData();
    }
    public void showData()
    {
        grv_Catalogue.DataSource = catalog.ShowCatalogue();
        grv_Catalogue.DataBind();
    }
    protected void btn_Insert_Click(object sender, EventArgs e)
    {
        if (txt_CatalogueName.Text == "")
        {
            lbl_Mess.Text = "Vui lòng nhập tên Loại hàng !!!";
            return;
        }
        if (catalog.CheckCatalogueName(txt_CatalogueName.Text))
        {
            catalog.InsertCatalogue(txt_CatalogueName.Text);
            lbl_Mess.Text = "Thêm thành công !!!";
            showData();
            txt_CatalogueName.Text = "";
        }
        else
        {
            lbl_Mess.Text = "Tên loại hàng đã tồn tại !!!";
            txt_CatalogueName.Text = "";
        }
    }
    protected void grv_Catalogue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            string id_loaihang = grv_Catalogue.Rows[index].Cells[0].Text;

            if (catalog.CheckCatalogueHasInProduct(id_loaihang))
                lbl_Mess.Text = "Loại hàng này hiện tại không thể xóa!";
            else
            {
                catalog.DeleteCatalogue(id_loaihang);
                showData();
                lbl_Mess.Text = "Xóa thành công !!!";
            }
        }
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        if (txt_CatalogueName.Text == "")
        {
            lbl_LoaiHang.Text = "*";
            return;
        }
        if (!catalog.CheckCatalogueUpdate(lbl_Code.Text,txt_CatalogueName.Text))
        {
            lbl_Mess.Text = "Loại hàng này đã tồn tại !!!";
            return;
        }
        catalog.Update(lbl_Code.Text, txt_CatalogueName.Text);
        lbl_Mess.Text = "Cập nhật thành công !!!";
        showData();
        btn_Insert.Visible = true;
        btn_Update.Visible = false;
    }

    protected void grv_Catalogue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_Catalogue.PageIndex = e.NewPageIndex;
        grv_Catalogue.DataBind();
    }

    protected void grv_Catalogue_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lbl_Code.Text = HttpUtility.HtmlDecode(grv_Catalogue.Rows[e.NewEditIndex].Cells[0].Text);
        txt_CatalogueName.Text = HttpUtility.HtmlDecode(grv_Catalogue.Rows[e.NewEditIndex].Cells[1].Text);
        btn_Insert.Visible = false;
        btn_Update.Visible = true;
    }
    protected void grv_Catalogue_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
