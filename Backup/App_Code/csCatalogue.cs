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
/// Summary description for csCatalogue
/// </summary>
public class csCatalogue
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;
    string chuoiketnoi = ConfigurationManager.ConnectionStrings["TSKN"].ConnectionString;

    public void OpenConnect()
    {
        sqlDS.ConnectionString = chuoiketnoi;
        ketnoi = new SqlConnection(sqlDS.ConnectionString);
        ketnoi.Open();
    }

    public void CloseConnect()
    {
        ketnoi.Close();
    }

    public DataTable ShowCatalogue()
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "select * FROM LoaiHang ORDER BY ID_LoaiHang DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public bool CheckCatalogueName(string catalogue)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM LoaiHang WHERE LoaiHang='"+catalogue+"'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = false;
        else ok = true;

        CloseConnect();
        return ok;
    }

    public bool CheckCatalogueHasInProduct(string id_loaihang)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM MatHang WHERE Id_LoaiHang=" + id_loaihang;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public bool CheckCatalogueUpdate(string idloaihang, string loaihang)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM Loaihang WHERE LoaiHang=@loaihang AND ID_LoaiHang != @idloaihang";
        sqlDS.SelectParameters.Add("idloaihang", idloaihang);
        sqlDS.SelectParameters.Add("loaihang", loaihang);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = false;
        else ok = true;

        CloseConnect();
        return ok;
    }

    public void InsertCatalogue(string catalogue)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "insert into LoaiHang values(@catalogue)";
        sqlDS.InsertParameters.Add("catalogue", catalogue);
        sqlDS.Insert();
        CloseConnect();
    }

    public void Update(string idloaihang, string loaihang)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.UpdateCommand = "Update LoaiHang SET LoaiHang=@loaihang WHERE ID_LoaiHang=@idloaihang";
        sqlDS.UpdateParameters.Add("idloaihang", idloaihang);
        sqlDS.UpdateParameters.Add("loaihang", loaihang);

        sqlDS.Update();
        CloseConnect();
    }

    public void DeleteCatalogue(string catalogueid)
    {
        OpenConnect();
        sqlDS.DeleteCommandType = SqlDataSourceCommandType.StoredProcedure;
        sqlDS.DeleteCommand = "DeleteCatalogue";
        sqlDS.DeleteParameters.Add("catalogueid", Convert.ToString(catalogueid));
        sqlDS.Delete();
        CloseConnect();
    }

    public string CatalogueNameToCatalogueId(string cataloguename)
    {
        OpenConnect();

        string IdCatalogue = "0";
        sqlDS.SelectCommand = "SELECT Id_LoaiHang FROM Loaihang WHERE Loaihang=@cataloguename";
        sqlDS.SelectParameters.Add("cataloguename", cataloguename);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) IdCatalogue = Convert.ToString(dv.Table.Rows[0]["Id_Loaihang"]);

        CloseConnect();
        return IdCatalogue;
    }

    public string GetCatalogueName(string idproduct)
    {
        OpenConnect();
        string catalogname = "";

        sqlDS.SelectCommand = "SELECT * FROM LoaiHang, MatHang WHERE MatHang.ID_Loaihang = LoaiHang.Id_LoaiHang AND Id_matHang="+idproduct;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) catalogname = Convert.ToString(dv.Table.Rows[0]["LoaiHang"]);

        CloseConnect();
        return catalogname;
    }
}
