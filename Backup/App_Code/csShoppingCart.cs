using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public class csShoppingCart: DataTable
{
    public static String chuoiketnoi = ConfigurationManager.ConnectionStrings["TSKN"].ConnectionString;

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(chuoiketnoi);
    }

    private static SqlCommand CreateCommand(String sql, params Object[] parameters)
    {
        SqlCommand command = new SqlCommand(sql, GetConnection());
        for (int i = 0; i < parameters.Length; i += 2)
        {
            command.Parameters.AddWithValue(parameters[i].ToString(), parameters[i + 1]);
        }
        return command;
    }

    public static DataTable Fill(DataTable table, String sql, params Object[] parameters)
    {
        SqlCommand command = CreateCommand(sql, parameters);
        new SqlDataAdapter(command).Fill(table);
        command.Connection.Close();

        return table;
    }

    public csShoppingCart()
	{
		this.Columns.Add("ID_MatHang", typeof(int)).DefaultValue = -1;
        this.Columns.Add("TenMatHang", typeof(string)).DefaultValue = "";
        this.Columns.Add("DonGia", typeof(int)).DefaultValue = 0;
        this.Columns.Add("SoLuong", typeof(int)).DefaultValue = 1;
        this.Columns.Add("HinhAnh", typeof(string)).DefaultValue = "";
        this.Columns.Add("ThanhTien", typeof(double), "DonGia*SoLuong");

        this.PrimaryKey = new DataColumn[] { this.Columns["ID_MatHang"] };
	}

    private void AddProduct(int id_mathang, int soluong, bool Update)
    {
        try
        {
            DataRow Item = this.Rows.Find(id_mathang);
            Item["SoLuong"] = soluong + (Update ? 0 : (int)Item["SoLuong"]);
        }
        catch
        {
            String sql = "SELECT ID_MatHang,HinhAnh, TenMatHang, DonGia, " + soluong + " as SoLuong FROM MatHang WHERE ID_MatHang=@id_mathang";
            Fill(this, sql, "@id_mathang", id_mathang);
        }
    }

    public void Add(int id_mathang, int soluong)
    {
        this.AddProduct(id_mathang, soluong, false);
    }

    public void Update(int id_mathang, int soluong)
    {
        this.AddProduct(id_mathang, soluong, true);
    }

    public void Remove(int id_mathang)
    {
        try
        {
            DataRow MatHang = this.Rows.Find(id_mathang);
            this.Rows.Remove(MatHang);
        }
        catch
        {
            Console.WriteLine("Product not found !");
        }
    }

    public double Total
    {
        get
        {
            object value = this.Compute("SUM(ThanhTien)", "");
            return value == DBNull.Value ? 0 : (double)value;
        }
    }

    public int Count
    {
        get
        {
            return this.Rows.Count;
        }
    }
}
