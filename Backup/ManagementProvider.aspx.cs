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

public partial class ManagementProvider : System.Web.UI.Page
{
    csProvider provider = new csProvider();

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_Mess.Text = "";
        lbl_Mess0.Text = "";
        lbl_Mess1.Text = "";
        lbl_Mess2.Text = "";
        lbl_Mess3.Text = "";
        showData();
    }

    public void showData()
    {
        grv_Provider.DataSource = provider.ShowProvider();
        grv_Provider.DataBind();
    }

    protected void btn_Insert_Click(object sender, EventArgs e)
    {
        if(txt_ProviderName.Text == "")
        {
            lbl_Mess0.Text = "*";
            return;
        }

        if(txt_Email.Text == "")
        {
            lbl_Mess1.Text = "*";
            return;
        }

        if(txt_Address.Text == "")
        {
            lbl_Mess2.Text = "*";
            return;
        }

        if (txt_Phone.Text == "")
        {
            lbl_Mess3.Text = "*";
            return;
        }

        if (!provider.CheckEmail(txt_Email.Text))
        {
            lbl_Mess.Text = "Email đã tồn tại !!!";
            txt_Email.Text = "";
        }
        else if (provider.CheckProviderName(txt_ProviderName.Text))
        {
            lbl_Mess.Text = "Nhà cung cấp đã tồn tại !!!";
            txt_ProviderName.Text = "";
        }
        else
        {
            provider.InsertProvider(txt_ProviderName.Text, txt_Email.Text, txt_Address.Text, txt_Phone.Text, txt_Fax.Text);
            lbl_Mess.Text = "Thêm thành công !!!";
            grv_Provider.DataBind();
            txt_ProviderName.Text = "";
            txt_Email.Text = "";
            txt_Address.Text = "";
            txt_Phone.Text = "";
            txt_Fax.Text = "";
            showData();
        }
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        if (txt_ProviderName.Text == "")
        {
            lbl_Mess0.Text = "*";
            return;
        }

        if (txt_Email.Text == "")
        {
            lbl_Mess1.Text = "*";
            return;
        }

        if (txt_Address.Text == "")
        {
            lbl_Mess2.Text = "*";
            return;
        }

        if (txt_Phone.Text == "")
        {
            lbl_Mess3.Text = "*";
            return;
        }
        if (!provider.CheckProviderNameUpdate(lbl_Code.Text, txt_ProviderName.Text))
        {
            lbl_Mess.Text = "Nhà cung cấp này đã tồn tại !!!";
            return;
        }
        if (!provider.CheckEmailUpdate(lbl_Code.Text, txt_Email.Text))
        {
            lbl_Mess.Text = "Email này đã tồn tại !!!";
            return;
        }
        provider.Update(lbl_Code.Text, txt_ProviderName.Text, txt_Email.Text, txt_Address.Text, txt_Phone.Text, txt_Fax.Text);
        lbl_Mess.Text = "Cập nhật thành công !!!";
        showData();
        btn_Insert.Visible = true;
        btn_Update.Visible = false;
    }

    protected void grv_Provider_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            string idprovider = grv_Provider.Rows[index].Cells[0].Text;

            if (provider.CheckProviderHasInImport(idprovider))
                lbl_Mess.Text = "Nhà cung cấp này hiện tại không thể xóa!";
            else
            {
                provider.DeleteProvider(idprovider);
                showData();
                lbl_Mess.Text = "Xóa thành công !!!";
            }
        }
    }
    protected void grv_Provider_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grv_Provider.PageIndex = e.NewPageIndex;
        grv_Provider.DataBind();
    }
    protected void grv_Provider_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lbl_Code.Text = HttpUtility.HtmlDecode(grv_Provider.Rows[e.NewEditIndex].Cells[0].Text);
        txt_ProviderName.Text = HttpUtility.HtmlDecode(grv_Provider.Rows[e.NewEditIndex].Cells[1].Text);
        txt_Email.Text = HttpUtility.HtmlDecode(grv_Provider.Rows[e.NewEditIndex].Cells[2].Text);
        txt_Address.Text = HttpUtility.HtmlDecode(grv_Provider.Rows[e.NewEditIndex].Cells[3].Text);
        txt_Phone.Text = HttpUtility.HtmlDecode(grv_Provider.Rows[e.NewEditIndex].Cells[4].Text);
        txt_Fax.Text = HttpUtility.HtmlDecode(grv_Provider.Rows[e.NewEditIndex].Cells[5].Text);
        btn_Insert.Visible = false;
        btn_Update.Visible = true;
    }

}
