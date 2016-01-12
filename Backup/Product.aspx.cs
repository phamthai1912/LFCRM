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

public partial class Product : System.Web.UI.Page
{
    csShoppingCart cart;
    csLogin login = new csLogin();
    csOrder order = new csOrder();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.cart = (csShoppingCart)Session["ShoppingCart"];
    }

    protected void imgProduct_Click(object sender, ImageClickEventArgs e)
    {
        Pn_ProductDetail.Visible = true;
        Pn_Product.Visible = false;
    }

    protected void btn_BuyNow_Click(object sender, ImageClickEventArgs e)
    {
        csShoppingCart Cart = (csShoppingCart)Session["ShoppingCart"];

        int id_product = int.Parse(Request.QueryString["ID_MatHang"]);
        TextBox txt_Quantity = (TextBox)fv_ProductDetail.FindControl("txt_Quantity");
        if (txt_Quantity.Text == "")
        {
            Label lbl = (Label)fv_ProductDetail.FindControl("lbl_Mess");
            lbl.Visible = true;
            return;
        }
        int quantity = int.Parse(txt_Quantity.Text);
        Cart.Add(id_product, quantity);

        //ASP.masterpage_master mnr = (ASP.masterpage_master)this.Master;
        //mnr.RefreshShoppingCartInfo();
        Response.Redirect("Cart.aspx");
    }

}
