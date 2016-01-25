﻿using LFCRM.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using AjaxControlToolkit;
using System.Drawing;

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
            DropDownList ddl = new DropDownList();
            string buttonId = btn.ID;
            ddl = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + Regex.Match(buttonId, @"\d+").Value);

            ddl.Items.Clear();
            ddl.Items.Add("-Select Item-");
            UpdateActualResource();
            ph_DynamicResourceTableRow.FindControl("tbr_ContentResource" + Regex.Match(buttonId, @"\d+").Value).Visible = false;
        }

        protected void tb_TitleTextChanged_event(object sender, EventArgs e)
        {
            for (int i=1; i<=(int)ViewState["ResourceCounter"]; i++)
                AddTitletoResource(i.ToString());
        }

        protected void ddl_TitleSelectedIndexChannged_event(object sender, EventArgs e)
        {
            UpdateActualResource();
        }

        protected void tb_ExpectedResoucesTextChanged_event(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string texboxId = tb.ID;
            Label lbl = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ActualResources" + Regex.Match(texboxId, @"\d+").Value);

            if (tb.Text != "")
            {
                if (Convert.ToDouble(tb.Text) < Convert.ToDouble(lbl.Text)) lbl.ForeColor = Color.Red;
                else if (Convert.ToDouble(tb.Text) == Convert.ToDouble(lbl.Text)) lbl.ForeColor = Color.LightGreen;
                else lbl.ForeColor = Color.Black;
            }
        }

        protected void ddl_WorkingHoursSelectedIndexChannged_event(object sender, EventArgs e)
        {
            UpdateActualResource();
        }

        protected void HandleAddTitle(string id)
        {
            Button btn = new Button();
            TextBox tb = new TextBox();
            TextBox tb2 = new TextBox();
            var tuple = RA.AddTitle(id);

            btn = tuple.Item2;
            tb = tuple.Item3;
            tb2 = tuple.Item4;
            btn.Click += new EventHandler(btn_MinusTitleClick_event);
            tb.TextChanged += new EventHandler(tb_TitleTextChanged_event);
            tb2.TextChanged += new EventHandler(tb_ExpectedResoucesTextChanged_event);
            ph_DynamicTitleTableRow.Controls.Add(tuple.Item1);
        }

        protected void HandleAddResource(string id)
        {
            Button btn = new Button();
            DropDownList ddl = new DropDownList();
            DropDownList ddl2 = new DropDownList();
            var tuple = RA.AddResource(id);

            btn = tuple.Item2;
            ddl = tuple.Item3;
            ddl2 = tuple.Item4;
            btn.Click += new EventHandler(btn_MinusResourceClick_event);
            ddl.SelectedIndexChanged += new EventHandler(ddl_TitleSelectedIndexChannged_event);
            ddl2.SelectedIndexChanged += new EventHandler(ddl_WorkingHoursSelectedIndexChannged_event);
            ph_DynamicResourceTableRow.Controls.Add(tuple.Item1);
        }

        protected void AddTitletoResource(string idResource)
        {
            DropDownList ddl = new DropDownList();
            ddl = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + idResource);
            Session["ddl_" + idResource] = ddl.SelectedValue;
            ddl.Items.Clear();
            ddl.Items.Add("-Select Item-");

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

        protected void UpdateActualResource()
        {
            for (int i = 1; i <= (int)ViewState["TitleCounter"]; i++)
            {
                double count = 0;
                TextBox tb = new TextBox();
                TextBox tb2 = new TextBox();
                Label lbl = new Label();
                tb = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + i.ToString());
                tb2 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_ExpectedResouces" + i.ToString());
                lbl = (Label)ph_DynamicTitleTableRow.FindControl("lbl_ActualResources" + i.ToString());

                for (int j = 1; j <= (int)ViewState["ResourceCounter"]; j++)
                {
                    DropDownList ddl = new DropDownList();
                    DropDownList ddl2 = new DropDownList();
                    ddl = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + j.ToString());
                    ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + j.ToString());
                    if (ddl2.SelectedItem.Text !="OFF")
                        if (ddl.SelectedItem.Text == tb.Text) count = count + Convert.ToDouble(ddl2.SelectedItem.Text)/8;
                }

                lbl.Text = count.ToString();

                if (tb2.Text != "")
                {
                    if (Convert.ToDouble(tb2.Text) < count) lbl.ForeColor = Color.Red;
                    else if (Convert.ToDouble(tb2.Text) == count) lbl.ForeColor = Color.LightGreen;
                    else lbl.ForeColor = Color.Black;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}