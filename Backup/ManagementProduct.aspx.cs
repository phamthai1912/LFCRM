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
using System.IO;
using System.Drawing;

public partial class ManagementProduct : System.Web.UI.Page
{
    csProduct product = new csProduct();
    csProduction production = new csProduction();
    csImport Import = new csImport();
    csCatalogue catalogue = new csCatalogue();
    csMessageBox messbox = new csMessageBox();

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(btn_Insert);
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(btn_Update);
        lbl_Mess.Text = "";
        SelectProduct();
    }

    private void SelectProduct()
    {
        grv_Product.DataSource = product.SelectProduct();
        grv_Product.DataBind();
    }

    protected void btn_Insert_Click(object sender, EventArgs e)
    {
        product.InsertProduct(txt_ProductName.Text, Fup_HinhAnh.FileName.ToString(), txt_Description.Text, int.Parse(ddl_Production.SelectedValue), int.Parse(ddl_Catalogue.SelectedValue));
        if(Fup_HinhAnh.HasFile)
            Fup_HinhAnh.PostedFile.SaveAs(Server.MapPath("~/AnhSP/" + Fup_HinhAnh.FileName));
        SelectProduct();
        lbl_Mess.Text = "Thêm thành công !!!";
        txt_ProductName.Text = "";
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        product.UpdateProduct(int.Parse(lbl_ProductID.Text), txt_ProductName.Text, Fup_HinhAnh.FileName.ToString(), txt_Description.Text, int.Parse(ddl_Production.SelectedValue), int.Parse(ddl_Catalogue.SelectedValue));
        if (Fup_HinhAnh.HasFile)
            Fup_HinhAnh.PostedFile.SaveAs(Server.MapPath("~/AnhSP/" + Fup_HinhAnh.FileName));
        SelectProduct();
        lbl_Mess.Text = "Cập nhập thành công !!!";
        txt_ProductName.Text = "";
    }

    protected void grv_Product_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lbl_ProductID.Text = grv_Product.Rows[e.NewEditIndex].Cells[0].Text;
        txt_ProductName.Text = HttpUtility.HtmlDecode(grv_Product.Rows[e.NewEditIndex].Cells[2].Text);
        ddl_Production.Text = production.ProductionNameToProductionId(HttpUtility.HtmlDecode(grv_Product.Rows[e.NewEditIndex].Cells[3].Text));
        ddl_Catalogue.Text = catalogue.CatalogueNameToCatalogueId(HttpUtility.HtmlDecode(grv_Product.Rows[e.NewEditIndex].Cells[4].Text));
        txt_Description.Text = product.ShowDescription(HttpUtility.HtmlDecode(grv_Product.Rows[e.NewEditIndex].Cells[0].Text));
        btn_Insert.Visible = false;
        btn_Update.Visible = true;
        btn_Cancel.Visible = true;
    }

    protected void grv_Product_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            string idproduct = grv_Product.Rows[index].Cells[0].Text;
            if (Import.CheckProductHasInImport(idproduct))
                lbl_Mess.Text = "Mặt hàng này hiện tại không thể xóa!";
            else
            {
                product.DeleteProduct(Convert.ToInt32(idproduct));
                lbl_Mess.Text = "Xóa thành công !!!";
                SelectProduct();
            }
        }
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        btn_Insert.Visible = true;
        btn_Update.Visible = false;
        btn_Cancel.Visible = false;
        txt_ProductName.Text = "";
        txt_Description.Text = "";
    }

    protected void grv_Product_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_Product.PageIndex = e.NewPageIndex;
        SelectProduct();
    }

    protected void txt_ProductName_TextChanged(object sender, EventArgs e)
    {
        if (!product.CheckProductName(txt_ProductName.Text))
        {
            lbl_Product.Text = "<span style='color: green;'>Ok!</span>";
            txt_ProductName.BackColor = Color.Green;
            txt_ProductName.ForeColor = Color.White;
            btn_Insert.Visible = true;
        }
        else
        {
            lbl_Product.Text = "<span style='color: red;'>Đã tồn tại!</span>";
            txt_ProductName.BackColor = Color.Red;
            txt_ProductName.ForeColor = Color.White;
            btn_Insert.Visible = false;
        }
    }
}
