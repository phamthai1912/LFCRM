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
using System.Drawing;

public partial class frmExchangeRate : System.Web.UI.UserControl
{
    string Url = "http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (!IsPostBack)
        //{
        //    DataSet ds = new DataSet();

        //    ds.ReadXml(Url);

        //    if (ds != null && ds.Tables.Count > 0)
        //    {

        //        DataTable dt = new DataTable();

        //        dt = ds.Tables["Exrate"];

        //        lbl_tygia.Text = "&nbsp;&nbsp;<b>" + Convert.ToString(dt.Rows[17]["CurrencyCode"]) + "</b> : &nbsp;&nbsp;" + Convert.ToString(dt.Rows[17]["Buy"]) + " vnđ<br>"
        //                        + "&nbsp;&nbsp;<b>" + Convert.ToString(dt.Rows[1]["CurrencyCode"]) + "</b> : &nbsp;&nbsp;" + Convert.ToString(dt.Rows[1]["Buy"]) + " vnđ<br>"
        //                        + "&nbsp;&nbsp;<b>" + Convert.ToString(dt.Rows[2]["CurrencyCode"]) + "</b> : &nbsp;&nbsp;" + Convert.ToString(dt.Rows[2]["Buy"]) + " vnđ<br>"
        //                        + "&nbsp;&nbsp;<b>" + Convert.ToString(dt.Rows[0]["CurrencyCode"]) + "</b> : &nbsp;&nbsp;" + Convert.ToString(dt.Rows[0]["Buy"]) + " vnđ<br>"
        //                        + "&nbsp;&nbsp;<b>" + Convert.ToString(dt.Rows[4]["CurrencyCode"]) + "</b> : &nbsp;&nbsp;" + Convert.ToString(dt.Rows[4]["Buy"]) + " vnđ<br>"
        //                        + "&nbsp;&nbsp;<b>" + Convert.ToString(dt.Rows[5]["CurrencyCode"]) + "</b> : &nbsp;&nbsp;" + Convert.ToString(dt.Rows[5]["Buy"]) + " vnđ<br>"
        //                        + "&nbsp;&nbsp;<b>" + Convert.ToString(dt.Rows[6]["CurrencyCode"]) + "</b> : &nbsp;&nbsp;" + Convert.ToString(dt.Rows[6]["Buy"]) + " vnđ<br>";
        //        lbl_tygia.ForeColor = Color.Black;
        //    }

        //}
    }
}
