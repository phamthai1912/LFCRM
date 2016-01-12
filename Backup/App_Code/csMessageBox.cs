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
/// Summary description for csMessageBox
/// </summary>
public class csMessageBox
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;

	public csMessageBox()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void OpenConnect()
    {
        sqlDS.ConnectionString = ConfigurationManager.ConnectionStrings["TSKN"].ConnectionString;
        ketnoi = new SqlConnection(sqlDS.ConnectionString);
        ketnoi.Open();
    }

    public void CloseConnect()
    {
        ketnoi.Close();
    }

    public string MessageBox(string m)
    {
        string str = "<script language='javascript'> alert('" + m + "'); </script>";
        return str;
    }

    public string MesageBoxAndBack(string m)
    {
        string str = "<script language='javascript'> alert('" + m + "'); window.history.back(-1); </script>";
        return str;
    }

    public void SendMessage(string idnguoigoi, string idnguoinhan, string tieude, string noidung, string ngay)
    {
        OpenConnect();

        sqlDS.InsertCommandType = SqlDataSourceCommandType.Text;
        sqlDS.InsertCommand = "INSERT INTO ThongBao(Id_nguoigoi, Id_nguoinhan, tieude, noidung, ngaygoi, trangthai) VALUES ('" + idnguoigoi + "','" + idnguoinhan + "','" + tieude + "','" + noidung + "','" + ngay + "', 'new.gif')";
        sqlDS.Insert();

        CloseConnect();
    }
    
}
