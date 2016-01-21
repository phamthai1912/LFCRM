using LFCRM.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using AjaxControlToolkit;

namespace LFCRM.AdminPage
{
    public partial class ResourceAllocation : System.Web.UI.Page
    {
        csResourceAllocation RA = new csResourceAllocation();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ViewState["TitleCounter"] = 0;
                ViewState["ResourceCounter"] = 0;
            }
            else
            {
                //------------------------Title--------------------------------//
                if ((int)ViewState["TitleCounter"] != 0)
                    for (int i = 1; i <= (int)ViewState["TitleCounter"]; i++)
                        HandleAddTitle(i.ToString());

                //------------------------Resource------------------------------//
                if ((int)ViewState["ResourceCounter"] != 0)
                    for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
                        HandleAddResource(i.ToString());
            }
        }

        protected void btn_AddTitle_Click(object sender, EventArgs e)
        {
            ViewState["TitleCounter"] = (int)ViewState["TitleCounter"] + 1;

            HandleAddTitle(ViewState["TitleCounter"].ToString());
        }

        protected void btn_AddResource_Click(object sender, EventArgs e)
        {
            //--------------------------Add Resource-----------------------------
            ViewState["ResourceCounter"] = (int)ViewState["ResourceCounter"] + 1;

            HandleAddResource(ViewState["ResourceCounter"].ToString());

            //--------------------------Add title to new resource----------------
            AddTitletoResource(ViewState["ResourceCounter"].ToString());
            
        }

        protected void btn_MinusTitleClick_event(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            TextBox tb= new TextBox();
            string buttonId = btn.ID;
            tb = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + Regex.Match(buttonId, @"\d+").Value);

            tb.Text = "";
            ph_DynamicTitleTableRow.FindControl("tbr_ContentTitle" + Regex.Match(buttonId, @"\d+").Value).Visible = false;

            //---------------------Re-Add Title to Resource---------------
            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
                AddTitletoResource(i.ToString());
        }

        protected void btn_MinusResourceClick_event(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string buttonId = btn.ID;

            ph_DynamicResourceTableRow.FindControl("tbr_ContentResource" + Regex.Match(buttonId, @"\d+").Value).Visible = false;
        }

        protected void tb_TitleTextChanged_event(object sender, EventArgs e)
        {
            for (int i=1; i<=(int)ViewState["ResourceCounter"]; i++)
                AddTitletoResource(i.ToString());
        }

        protected void HandleAddTitle(string id)
        {
            Button btn = new Button();
            TextBox tb = new TextBox();
            var tuple = RA.AddTitle(id);

            btn = tuple.Item2;
            tb = tuple.Item3;
            btn.Click += new EventHandler(btn_MinusTitleClick_event);
            tb.TextChanged += new EventHandler(tb_TitleTextChanged_event);
            ph_DynamicTitleTableRow.Controls.Add(tuple.Item1);
        }

        protected void HandleAddResource(string id)
        {
            Button btn = new Button();
            var tuple = RA.AddResource(id);

            btn = tuple.Item2;
            btn.Click += new EventHandler(btn_MinusResourceClick_event);
            ph_DynamicResourceTableRow.Controls.Add(tuple.Item1);
        }

        protected void AddTitletoResource(string idResource)
        {
            DropDownList ddl = new DropDownList();
            ddl = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + idResource);
            Session["ddl_" + idResource] = ddl.SelectedValue;
            ddl.Items.Clear();

            for (int j = 1; j <= (int)ViewState["TitleCounter"]; j++)
            {
                ListItem l = new ListItem();
                TextBox tb = new TextBox();
                tb = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + j.ToString());
                if (tb.Text != "")
                {
                    l.Value = j.ToString();
                    l.Text = tb.Text;
                    ddl.Items.Add(l);
                }
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(Convert.ToString(Session["ddl_" + idResource])));
        }
    }
}