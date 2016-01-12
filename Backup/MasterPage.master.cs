using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.RefreshShoppingCartInfo();
    }

    public void RefreshShoppingCartInfo()
    {
        csShoppingCart Cart = (csShoppingCart)Session["ShoppingCart"];
        lblTongTien.Text = string.Format("{0:N0}", Cart.Total);
        lblSoLuong.Text = Cart.Count.ToString();
    }
}
