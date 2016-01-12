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

public partial class Admin_Productor : System.Web.UI.Page
{

    csProduction pro = new csProduction();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_Mess.Text = "";
        showData();
    }

    public void showData()
    {
        grv_Production.DataSource = pro.ShowProduction();
        grv_Production.DataBind();
    }

    protected void btn_Insert_Click(object sender, EventArgs e)
    {
        if (txt_ProductionName.Text == "")
        {
            lbl_Mess.Text = "Vui lòng nhập tên Nhà sản xuất !!!";
            return;
        }
        if (pro.CheckProductionName(txt_ProductionName.Text))
        {
            pro.InsertProduction(txt_ProductionName.Text);
            lbl_Mess.Text = "Thêm thành công !!!";
            showData(); 
            txt_ProductionName.Text = "";
        }
        else
        {
            lbl_Mess.Text = "Tên Nhà sản xuất đã tồn tại !!!";
            txt_ProductionName.Text = "";
        }
    }

    protected void grv_Production_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = int.Parse(e.CommandArgument.ToString());

            if (pro.CheckProductionHasInProduct(grv_Production.Rows[index].Cells[0].Text))
                lbl_Mess.Text = "Nhà sản xuất này hiện tại không thể xóa!";
            else
            {
                pro.DeleteProduction(grv_Production.Rows[index].Cells[0].Text);
                showData();
                lbl_Mess.Text = "Xóa thành công !!!";
            }
        }
    }
    protected void grv_Production_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_Production.PageIndex = e.NewPageIndex;
        grv_Production.DataBind();
    }
    protected void grv_Production_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lbl_Code.Text = HttpUtility.HtmlDecode(grv_Production.Rows[e.NewEditIndex].Cells[0].Text);
        txt_ProductionName.Text = HttpUtility.HtmlDecode(grv_Production.Rows[e.NewEditIndex].Cells[1].Text);
        btn_Insert.Visible = false;
        btn_Update.Visible = true;
    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        if (txt_ProductionName.Text == "")
        {
            lbl_nsx.Text = "*";
            return;
        }
        if (!pro.CheckProductionUpdate(lbl_Code.Text, txt_ProductionName.Text))
        {
            lbl_Mess.Text = "Nhà sản xuất này đã tồn tại !!!";
            return;
        }
        pro.Update(lbl_Code.Text, txt_ProductionName.Text);
        lbl_Mess.Text = "Cập nhật thành công !!!";
        showData();
        btn_Insert.Visible = true;
        btn_Update.Visible = false;
    }
}
