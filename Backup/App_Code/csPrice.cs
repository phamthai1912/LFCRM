using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for csPrice
/// </summary>
public class csPrice
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;

	public csPrice()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void OpenConnect()
    {
        sqlDS.ConnectionString = ConfigurationManager.ConnectionStrings["TSKN"].ConnectionString;
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        ketnoi = new SqlConnection(sqlDS.ConnectionString);
        ketnoi.Open();
    }

    public void CloseConnect()
    {
        ketnoi.Close();
    }

    public DataTable ShowPriceByCatalog(string idcatalog)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT * "+
                                "FROM Mathang a, Loaihang b, nhasanxuat c "+
                                "WHERE a.id_loaihang = b.id_loaihang "+
                                "AND a.id_nhasanxuat = c.id_nhasanxuat " +
                                "AND a.id_loaihang = '"+idcatalog+"' " +
                                "ORDER BY DonGia";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public DataTable ShowPriceByCatalogAndProduction(string idcatalog, string idproduction)
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT * " +
                                "FROM Mathang a, Loaihang b, nhasanxuat c " +
                                "WHERE a.id_loaihang = b.id_loaihang " +
                                "AND a.id_nhasanxuat = c.id_nhasanxuat " +
                                "AND a.id_loaihang = '" + idcatalog + "' " +
                                "AND a.id_nhasanxuat = '" + idproduction + "' " +
                                "ORDER BY DonGia";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public void UpdatePrice(string id_product, string price)
    {
        OpenConnect();
        sqlDS.UpdateCommand = "Update Mathang SET Dongia=@price WHERE ID_mathang=@id_product";
        sqlDS.UpdateParameters.Add("id_product", id_product);
        sqlDS.UpdateParameters.Add("price", price);
        sqlDS.Update();
        CloseConnect();
    }
}
