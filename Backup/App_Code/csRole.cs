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
/// Summary description for csRole
/// </summary>
public class csRole
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


    public string RoleNameToRoleId(string rolename)
    {
        OpenConnect();

        string Idrole = "0";
        sqlDS.SelectCommand = "SELECT Id_Quyen FROM PhanQuyen WHERE QuyenHan LIKE N'"+rolename+"'";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) Idrole = Convert.ToString(dv.Table.Rows[0]["Id_Quyen"]);

        CloseConnect();
        return Idrole;
    }

    public bool CheckRoleName(string rolename)
    {
        OpenConnect();
        bool ok;

        sqlDS.SelectCommand = "SELECT * FROM PhanQuyen WHERE QuyenHan=@rolename ";
        sqlDS.SelectParameters.Add("rolename", rolename);
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0) ok = false;
        else ok = true;

        CloseConnect();
        return ok;
    }

    public void InsertRole(string role)
    {
        OpenConnect();
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "insert into PhanQuyen(QuyenHan) values(@role)";
        sqlDS.InsertParameters.Add("role", role);
        sqlDS.Insert();
        CloseConnect();
    }

    public void DeleteRole(int roleid)
    {
        OpenConnect();
        sqlDS.DeleteCommandType = SqlDataSourceCommandType.StoredProcedure;
        sqlDS.DeleteCommand = "DeleteRole";
        sqlDS.DeleteParameters.Add("roleid", Convert.ToString(roleid));
        sqlDS.Delete();
        CloseConnect();
    }
}
