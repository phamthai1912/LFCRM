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
/// Summary description for csMenu
/// </summary>
public class csMenu
{
    SqlDataSource sqlDS = new SqlDataSource();
    SqlConnection ketnoi;

	public csMenu()
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

    public string CreateMenuVertical()
    {
        OpenConnect();

        string Loaihang;
        string Nhasanxuat;
        string MaNSX;
        string str = "<div id='menudoc' style='line-height: 1px;'> ";
        int n=0;
        int[] a = new int[20];

        //----Lay id_loaihang co nhasanxuat, bo id_loaihang chua co nhasanxuat nao-----
        sqlDS.SelectCommand = "SELECT DISTINCT loaihang, mathang.id_loaihang FROM mathang,loaihang WHERE mathang.id_loaihang=loaihang.id_loaihang";
        DataView dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
        for (int i = 0; i < dv.Count; i++)
        {
            a[i] = Convert.ToInt32(dv.Table.Rows[i]["id_loaihang"]);
            n++;
        }
        //------------------------------------------------------------------------------

        //---Tao Menu-------------------------------------------------------------------
        for (int i = 0; i <n ; i++)
        {
            sqlDS.SelectCommand = "SELECT DISTINCT loaihang, nhasanxuat, mathang.id_nhasanxuat FROM mathang,loaihang, nhasanxuat WHERE mathang.id_loaihang=loaihang.id_loaihang AND mathang.id_nhasanxuat = nhasanxuat.id_nhasanxuat AND mathang.id_loaihang =" + a[i];

            dv = (DataView)sqlDS.Select(DataSourceSelectArguments.Empty);
            Loaihang = Convert.ToString(dv.Table.Rows[0]["loaihang"]);
            str = str + "<h3 ><a href='#'>" + Loaihang +
                    "</a></h3><div style='line-height: 25px; color: #3C1B0C;'>";

            for (int j = 0; j < dv.Count; j++)
            {
                Nhasanxuat = Convert.ToString(dv.Table.Rows[j]["nhasanxuat"]);
                MaNSX = Convert.ToString(dv.Table.Rows[j]["id_nhasanxuat"]);
                str = str + " - <a href='Product.aspx?LoaiHang=" + a[i] + "&MaNSX=" + MaNSX + "' style='color: #3C1B0C; text-decoration: none;'>" + Nhasanxuat + "</a><br>";
            }
            str = str + "</div>";
        }
        str = str + "</div>";
        //------------------------------------------------------------------------------*/

        CloseConnect();
        return str;
    }
}
