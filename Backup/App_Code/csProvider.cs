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
/// Summary description for csProvider
/// </summary>
public class csProvider
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

    public DataTable ShowProvider()
    {
        OpenConnect();

        DataTable dt = new DataTable();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "select * FROM NhaCungCap ORDER BY ID_NhaCungCap DESC";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        dt = dv.ToTable();

        CloseConnect();
        return dt;
    }

    public bool CheckEmail(string email)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM NhaCungCap WHERE Email=@email ";
        sqlDS.SelectParameters.Add("email", email);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = false;
        else ok = true;

        CloseConnect();
        return ok;
    }

    public bool CheckProviderHasInImport(string idprovider)
    {
        OpenConnect();

        bool ok = false;
        sqlDS.SelectCommand = "SELECT * FROM PhieuNhap WHERE Id_NhaCungCap=" + idprovider;
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;

        CloseConnect();
        return ok;
    }

    public void InsertProvider(string provider, string email, string address, string phone, string fax)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "insert into NhaCungCap values(@provider,@email,@address,@phone,@fax)";
        sqlDS.InsertParameters.Add("provider", provider);
        sqlDS.InsertParameters.Add("email", email);
        sqlDS.InsertParameters.Add("address", address);
        sqlDS.InsertParameters.Add("phone", phone);
        sqlDS.InsertParameters.Add("fax", fax);
        sqlDS.Insert();
        CloseConnect();
    }

    public bool CheckProviderNameUpdate(string providerid, string provider)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM NhaCungCap WHERE TenNhaCungCap=@provider AND ID_NhaCungCap != @providerid";
        sqlDS.SelectParameters.Add("providerid", providerid);
        sqlDS.SelectParameters.Add("provider", provider);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = false;
        else ok = true;

        CloseConnect();
        return ok;
    }

    public bool CheckEmailUpdate(string provider_id, string email)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM NhaCungCap WHERE Email=@email AND ID_NhaCungCap != @provider_id";
        sqlDS.SelectParameters.Add("provider_id", provider_id);
        sqlDS.SelectParameters.Add("email", email);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = false;
        else ok = true;

        CloseConnect();
        return ok;
    }

    public void Update(string providerid, string provider, string email, string diachi, string phone, string fax)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.UpdateCommand = "Update NhaCungCap SET TenNhaCungCap=@provider,Email=@email,DiaChi=@diachi,Phone=@phone,Fax=@fax WHERE ID_NhaCungCap=@providerid";
        sqlDS.UpdateParameters.Add("providerid", providerid);
        sqlDS.UpdateParameters.Add("provider", provider);
        sqlDS.UpdateParameters.Add("email", email);
        sqlDS.UpdateParameters.Add("diachi", diachi);
        sqlDS.UpdateParameters.Add("phone", phone);
        sqlDS.UpdateParameters.Add("fax", fax);

        sqlDS.Update();
        CloseConnect();
    }

    public void DeleteProvider(string providerid)
    {
        OpenConnect();
        sqlDS.DeleteCommandType = SqlDataSourceCommandType.StoredProcedure;
        sqlDS.DeleteCommand = "DeleteProvider";
        sqlDS.DeleteParameters.Add("providerid", Convert.ToString(providerid));
        sqlDS.Delete();
        CloseConnect();
    }

    public string ProviderNameToProviderId(string providername)
    {
        OpenConnect();

        string IdProvider = "0";
        sqlDS.SelectCommand = "SELECT Id_NhaCungCap FROM NhaCungCap WHERE TenNhaCungCap=@providername";
        sqlDS.SelectParameters.Add("providername", providername);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) IdProvider = Convert.ToString(dv.Table.Rows[0]["Id_NhaCungCap"]);

        CloseConnect();
        return IdProvider;
    }

    public bool CheckProviderName(string provider)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.SelectCommand = "SELECT * FROM NhaCungCap WHERE TenNhaCungCap=@provider";
        sqlDS.SelectParameters.Add("provider", provider);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = true;
        else ok = false;

        CloseConnect();
        return ok;
    }
}
