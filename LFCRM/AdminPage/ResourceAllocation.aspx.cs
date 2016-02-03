using LFCRM.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using AjaxControlToolkit;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;

namespace LFCRM.AdminPage
{
    public partial class ResourceAllocation : System.Web.UI.Page
    {
        csResourceAllocation RA = new csResourceAllocation();
        csTitleManager titleManager = new csTitleManager();
        csCommonClass commonClass = new csCommonClass();
        Class.csDBConnect dbconnect = new Class.csDBConnect();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["TitleCounter"] = 0;
                ViewState["ResourceCounter"] = 0;

                ViewState["TitleSort"] = 0;
                ViewState["RoleSort"] = 0;
                ViewState["IDSort"] = 0;
                ViewState["NameSort"] = 0;
                ViewState["HoursSort"] = 0;
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
            TextBox tb = new TextBox();
            string buttonId = btn.ID;
            tb = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + Regex.Match(buttonId, @"\d+").Value);

            tb.Text = "";
            ph_DynamicTitleTableRow.FindControl("tbr_ContentTitle" + Regex.Match(buttonId, @"\d+").Value).Visible = false;

            //---------------------Re-Add Title to Resource---------------
            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
                AddTitletoResource(i.ToString());

            UpdateTotalBillingLabel();
            UpdateTotalAssignedLabel();
        }

        protected void btn_MinusResourceClick_event(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string buttonId = btn.ID;
            DropDownList ddl = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + Regex.Match(buttonId, @"\d+").Value);
            TextBox tb = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + Regex.Match(buttonId, @"\d+").Value);
            Label lb = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + Regex.Match(buttonId, @"\d+").Value);

            ddl.Items.Clear();
            ddl.Items.Add("- Select Item -");
            tb.Text = "";
            lb.Text = "N/A";
            UpdateActualResource();
            ph_DynamicResourceTableRow.FindControl("tbr_ContentResource" + Regex.Match(buttonId, @"\d+").Value).Visible = false;
            UpdateHeadcountLabel();
            UpdateTotalAssignedLabel();
            UpdateTotalTrainee();
            UpdateTotalOff();
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string date = dateTime.ToString("MM/dd/yyyy");
            bool ok_ra = false;
            bool ok_ta = false;

            //---------------------------------- Save Resource Allocation------------------------------
            if (RA.CheckRADateExist(date))
            {
                RA.deleteResourceAllocationByDate(date);
            }

            if ((int)ViewState["ResourceCounter"] > 0)
                for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
                {
                    Label lb = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + i);

                    if (lb.Text != "N/A")
                    {
                        DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + i);
                        DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + i);
                        DropDownList ddl3 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + i);

                        RA.addResourceAllocation(date, lb.Text, ddl1.SelectedValue, ddl2.SelectedItem.ToString(), ddl3.SelectedValue);
                        ok_ra = true;
                    }
                    else
                    {
                        Label lb2 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceExist" + i);
                        lb2.Text = "This field cannot save since not existed";
                    }
                }

            //---------------------------------- Save Title Allocation------------------------------
            if (RA.CheckTADateExist(date))
            {
                RA.deleteTitleAllocationByDate(date);
            }

            if ((int)ViewState["TitleCounter"] > 0)
                for (int i = 1; i <= (int)ViewState["TitleCounter"]; i++)
                {
                    TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Title" + i);

                    if (tb1.Text != "")
                    {
                        if (titleManager.LDExist(tb1.Text))
                        {
                            TextBox tb2 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_ExpectedResouces" + i);
                            Label lb = (Label)ph_DynamicTitleTableRow.FindControl("lbl_ActualResources" + i);

                            RA.addTitleAllocation(date, tb1.Text, tb2.Text, lb.Text);
                            ok_ta = true;
                        }
                        else
                        {
                            Label lb3 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_TitleExist" + i);
                            lb3.Text = "This field cannot save since not existed";
                        }
                    }
                }

            ////----------------------------- Message Box ----------------------------------//
            if ((ok_ta == true) && (ok_ra == true))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("alert('Save successfully!');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("alert('Cannot save!');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
            }
        }

        protected void btn_SortbyTitle_Click(object sender, EventArgs e)
        {
            ViewState["TitleSort"] = (int)ViewState["TitleSort"] + 1;

            if ((int)ViewState["TitleSort"] % 2 == 0)
                ResourceTableSort("Title ASC");
            else
                ResourceTableSort("Title DESC");
        }

        protected void btn_SortbyID_Click(object sender, EventArgs e)
        {
            ViewState["IDSort"] = (int)ViewState["IDSort"] + 1;

            if ((int)ViewState["IDSort"] % 2 == 0)
                ResourceTableSort("ID ASC");
            else
                ResourceTableSort("ID DESC");
        }

        protected void btn_SortbyName_Click(object sender, EventArgs e)
        {
            ViewState["NameSort"] = (int)ViewState["NameSort"] + 1;

            if ((int)ViewState["NameSort"] % 2 == 0)
                ResourceTableSort("Name ASC");
            else
                ResourceTableSort("Name DESC");
        }

        protected void btn_SortbyRole_Click(object sender, EventArgs e)
        {
            ViewState["RoleSort"] = (int)ViewState["RoleSort"] + 1;

            if ((int)ViewState["RoleSort"] % 2 == 0)
                ResourceTableSort("Role ASC");
            else
                ResourceTableSort("Role DESC");
        }

        protected void btn_SortbyHours_Click(object sender, EventArgs e)
        {
            ViewState["HoursSort"] = (int)ViewState["HoursSort"] + 1;

            if ((int)ViewState["HoursSort"] % 2 == 0)
                ResourceTableSort("WorkingHours ASC");
            else
                ResourceTableSort("WorkingHours DESC");
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        protected void btn_CopyFromThisDay_Click(object sender, EventArgs e)
        {
            ClearAll();
            if(txt_Date.Text != "")
            {
                AddTitleByDay(txt_Date.Text);
                AddResourceByDay(txt_Date.Text);

                UpdateHeadcountLabel();
                UpdateTotalBillingLabel();
                UpdateTotalOff();
                UpdateTotalAssignedLabel();
            }
        }

        protected void btn_Export_Click(object sender, EventArgs e)
        {

        }

        protected void tb_TitleTextChanged_event(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Label lb = new Label();

            string textboxId = tb.ID;
            lb = (Label)ph_DynamicTitleTableRow.FindControl("lbl_TitleExist" + Regex.Match(textboxId, @"\d+").Value);

            if (!titleManager.LDExist(tb.Text)) lb.Visible = true;
            else
            {
                lb.Visible = false;
                for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
                    AddTitletoResource(i.ToString());
            }
        }

        protected void tb_ResouceTextChanged_event(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Label lb = new Label();
            Label lb2 = new Label();

            string textboxId = tb.ID;
            lb = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceExist" + Regex.Match(textboxId, @"\d+").Value);
            lb2 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + Regex.Match(textboxId, @"\d+").Value);

            if (tb.Text != "")
            {
                if (!RA.FullNameExist(tb.Text)) lb.Visible = true;
                else
                {
                    lb.Visible = false;
                    lb2.Text = RA.getEmployeeID(tb.Text);
                }
            }

            UpdateHeadcountLabel();
            UpdateTotalTrainee();
            UpdateTotalOff();
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

            UpdateTotalBillingLabel();
        }

        protected void ddl_TitleSelectedIndexChannged_event(object sender, EventArgs e)
        {
            DropDownList ddl1 = (DropDownList)sender;
            string ddlId = ddl1.ID;
            TableRow tbr = (TableRow)ph_DynamicResourceTableRow.FindControl("tbr_ContentResource" + Regex.Match(ddlId, @"\d+").Value);
            tbr.BorderColor = Color.Red;

            UpdateActualResource();
            UpdateTotalAssignedLabel();
        }

        protected void ddl_WorkingHoursSelectedIndexChannged_event(object sender, EventArgs e)
        {
            UpdateActualResource();
            UpdateTotalAssignedLabel();
            UpdateTotalTrainee();
            UpdateTotalOff();
        }

        protected void ddl_RoleSelectedIndexChannged_event(object sender, EventArgs e)
        {
            DropDownList ddl1 = (DropDownList)sender;
            string ddlId = ddl1.ID;

            ChangeRole(Regex.Match(ddlId, @"\d+").Value);
            UpdateTotalTrainee();
            UpdateTotalOff();
        }

        public void HandleAddTitle(string id)
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

        public void HandleAddResource(string id)
        {
            Button btn = new Button();
            DropDownList ddl = new DropDownList();
            DropDownList ddl2 = new DropDownList();
            DropDownList ddl3 = new DropDownList();
            TextBox tb = new TextBox();
            var tuple = RA.AddResource(id);

            btn = tuple.Item2;
            ddl = tuple.Item3;
            ddl2 = tuple.Item4;
            tb = tuple.Item5;
            ddl3 = tuple.Item6;
            btn.Click += new EventHandler(btn_MinusResourceClick_event);
            ddl.SelectedIndexChanged += new EventHandler(ddl_TitleSelectedIndexChannged_event);
            ddl2.SelectedIndexChanged += new EventHandler(ddl_WorkingHoursSelectedIndexChannged_event);
            ddl3.SelectedIndexChanged += new EventHandler(ddl_RoleSelectedIndexChannged_event);
            tb.TextChanged += new EventHandler(tb_ResouceTextChanged_event);
            ph_DynamicResourceTableRow.Controls.Add(tuple.Item1);
        }

        public void AddTitletoResource(string idResource)
        {
            DropDownList ddl = new DropDownList();
            ddl = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + idResource);
            Session["ddl_" + idResource] = ddl.SelectedValue;
            ddl.Items.Clear();
            ddl.Items.Add("- Select Item -");

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

        public void UpdateActualResource()
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
                    if (ddl2.SelectedItem.Text != "OFF")
                        if (ddl.SelectedItem.Text == tb.Text) count = count + Convert.ToDouble(ddl2.SelectedItem.Text) / 8;
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

        public void UpdateHeadcountLabel()
        {
            List<string> namelist = new List<string>();

            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
            {
                bool ok = false;
                TextBox tb1 = new TextBox();
                tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + i.ToString());

                if (tb1.Text != "")
                {
                    foreach (string item in namelist)
                    {
                        if (tb1.Text == item) ok = true;
                    }
                    if (!ok) namelist.Add(tb1.Text);
                }
            }

            lbl_headCount.Text = namelist.Count.ToString();
        }

        public void UpdateTotalBillingLabel()
        {
            double count = 0;
            for (int i = 1; i <= (int)ViewState["TitleCounter"]; i++)
            {
                TextBox tb1 = new TextBox();
                TextBox tb2 = new TextBox();
                tb1 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + i.ToString());
                tb2 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_ExpectedResouces" + i.ToString());

                if ((tb1.Text != "") && (tb2.Text != ""))
                {
                    count = count + Convert.ToDouble(tb2.Text);
                }
            }

            lbl_totalBilling.Text = count.ToString();
        }

        public void UpdateTotalAssignedLabel()
        {
            double count = 0;
            for (int i = 1; i <= (int)ViewState["TitleCounter"]; i++)
            {
                TextBox tb1 = new TextBox();
                Label lbl1 = new Label();
                tb1 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + i.ToString());
                lbl1 = (Label)ph_DynamicTitleTableRow.FindControl("lbl_ActualResources" + i.ToString());

                if (tb1.Text != "")
                {
                    count = count + Convert.ToDouble(lbl1.Text);
                }
            }

            lbl_totalAssigned.Text = count.ToString();
        }

        public void UpdateTotalTrainee()
        {
            double count = 0;

            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
            {
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + i.ToString());
                DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + i.ToString());
                TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + i.ToString());

                if (tb1.Text != "")
                {
                    if ((ddl1.SelectedItem.Text == "Trainee") || (ddl1.SelectedItem.Text == "Trainer"))
                    {
                        count = count + Convert.ToDouble(ddl2.SelectedItem.Text);
                    }
                }
            }

            lbl_totalTrainee.Text = (count / 8).ToString();
        }

        public void UpdateTotalOff()
        {
            double count = 0;

            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
            {
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + i.ToString());
                DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + i.ToString());
                TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + i.ToString());

                if (tb1.Text != "")
                {
                    if (ddl1.SelectedItem.Text == "Off")
                    {
                        count = count + Convert.ToDouble(ddl2.SelectedItem.Text);
                    }
                }
            }

            lbl_Off.Text = (count / 8).ToString();
        }

        public void ResourceTableSort(string sortType)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Role");
            dt.Columns.Add("Title");
            dt.Columns.Add("StateOfTitle");
            dt.Columns.Add("WorkingHours");
            dt.Columns.Add("ColorOfRole");

            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
            {
                Label lbl1 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + i.ToString());
                TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + i.ToString());
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + i.ToString());
                DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + i.ToString());
                DropDownList ddl3 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + i.ToString());

                dt.Rows.Add(lbl1.Text, tb1.Text, ddl1.SelectedItem.Text, ddl2.SelectedItem.Text, ddl2.Enabled.ToString(), ddl3.SelectedItem.Text, ddl1.BorderColor.ToString());
                dt.DefaultView.Sort = sortType;
                dt = dt.DefaultView.ToTable();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int j = i + 1;
                Label lbl1 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + j.ToString());
                TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + j.ToString());
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + j.ToString());
                DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + j.ToString());
                DropDownList ddl3 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + j.ToString());
                RequiredFieldValidator rfvSelectTitle = (RequiredFieldValidator)ph_DynamicResourceTableRow.FindControl("rfvSelectTitle" + j.ToString());
                bool state = Convert.ToBoolean(dt.Rows[i].ItemArray[4].ToString());
                string color = dt.Rows[i].ItemArray[6].ToString().Split('[', ']')[1];

                if (color != "Empty")
                {
                    Color myColor = ColorTranslator.FromHtml(color);
                    ddl1.BorderColor = myColor;
                }
                else ddl1.BorderColor = Color.Empty;

                lbl1.Text = dt.Rows[i].ItemArray[0].ToString();
                tb1.Text = dt.Rows[i].ItemArray[1].ToString();
                ddl1.ClearSelection();
                ddl2.ClearSelection();
                ddl3.ClearSelection();
                ddl1.Items.FindByText(dt.Rows[i].ItemArray[2].ToString()).Selected = true;
                ddl2.Items.FindByText(dt.Rows[i].ItemArray[3].ToString()).Selected = true;
                ddl2.Enabled = state;
                rfvSelectTitle.Enabled = state;
                ddl3.Items.FindByText(dt.Rows[i].ItemArray[5].ToString()).Selected = true;
            }
        }

        public void ClearAll()
        {
            ph_DynamicTitleTableRow.Controls.Clear();
            ph_DynamicResourceTableRow.Controls.Clear();
            ViewState["TitleCounter"] = 0;
            ViewState["ResourceCounter"] = 0;
            UpdateHeadcountLabel();
            UpdateTotalBillingLabel();
            UpdateTotalOff();
            UpdateTotalAssignedLabel();
        }

        public void ChangeRole(string idRow)
        {
            DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + idRow);
            DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + idRow);
            RequiredFieldValidator rfvSelectTitle = (RequiredFieldValidator)ph_DynamicResourceTableRow.FindControl("rfvSelectTitle" + idRow);

            if (ddl1.SelectedItem.Text == "Off")
            {
                ddl2.ClearSelection();
                ddl2.Items.FindByText("- Select Item -").Selected = true;
                ddl2.Enabled = false;
                rfvSelectTitle.Enabled = false;
                ddl1.BorderColor = Color.Gray;
            }
            else if (ddl1.SelectedItem.Text == "Project Leader")
            {
                ddl2.ClearSelection();
                ddl2.Items.FindByText("- Select Item -").Selected = true;
                ddl2.Enabled = false;
                rfvSelectTitle.Enabled = false;
                ddl1.BorderColor = Color.Red;
            }
            else if (ddl1.SelectedItem.Text == "Core")
            {
                ddl2.Enabled = true;
                rfvSelectTitle.Enabled = true;
                ddl1.BorderColor = Color.Green;
            }
            else
            {
                ddl2.Enabled = true;
                rfvSelectTitle.Enabled = true;
                ddl1.BorderColor = Color.Empty;
            }
        }

        public void AddResourceByDay(string date)
        {
            string sql = "SELECT * FROM tbl_ResourceAllocation WHERE Date = '" + date + "'";
            DataTable dt = dbconnect.getDataTable(sql);
            ViewState["ResourceCounter"] = dt.Rows.Count;

            for (int i=0; i< dt.Rows.Count; i++)
            {
                int j = i + 1;
                HandleAddResource(j.ToString());
                AddTitletoResource(j.ToString());

                Label lbl1 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + j.ToString());
                TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + j.ToString());
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + j.ToString());
                DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + j.ToString());
                DropDownList ddl3 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + j.ToString());
                //RequiredFieldValidator rfvSelectTitle = (RequiredFieldValidator)ph_DynamicResourceTableRow.FindControl("rfvSelectTitle" + j.ToString());
                string title = dt.Rows[i].ItemArray[3].ToString();
                if (title == "") title = "- Select Item -";

                lbl1.Text = dt.Rows[i].ItemArray[1].ToString();
                tb1.Text = RA.getFullName(dt.Rows[i].ItemArray[1].ToString());
                ddl1.ClearSelection();
                ddl2.ClearSelection();
                ddl3.ClearSelection();
                ddl1.Items.FindByText(RA.getProjectRoleName(dt.Rows[i].ItemArray[2].ToString())).Selected = true;
                ChangeRole(j.ToString());
                ddl2.Items.FindByText(title).Selected = true;
                ddl3.Items.FindByText(RA.getValueHours(dt.Rows[i].ItemArray[4].ToString())).Selected = true;
            }
        }

        public void AddTitleByDay(string date)
        {
            string sql = "SELECT * FROM tbl_TitleAllocation WHERE Date = '" + date + "'";
            DataTable dt = dbconnect.getDataTable(sql);
            ViewState["TitleCounter"] = dt.Rows.Count;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int j = i + 1;
                HandleAddTitle(j.ToString());

                TextBox tb1 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + j.ToString());
                TextBox tb2 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_ExpectedResouces" + j.ToString());
                Label lbl1 = (Label)ph_DynamicTitleTableRow.FindControl("lbl_ActualResources" + j.ToString());

                tb1.Text = dt.Rows[i].ItemArray[1].ToString();
                tb2.Text = dt.Rows[i].ItemArray[2].ToString();
                lbl1.Text = dt.Rows[i].ItemArray[3].ToString();

                if (tb2.Text != "")
                {
                    if (Convert.ToDouble(tb2.Text) < Convert.ToDouble(lbl1.Text)) lbl1.ForeColor = Color.Red;
                    else if (Convert.ToDouble(tb2.Text) == Convert.ToDouble(lbl1.Text)) lbl1.ForeColor = Color.LightGreen;
                    else lbl1.ForeColor = Color.Black;
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
            return;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.DesignMode == true)
            {
                this.EnsureChildControls();
            }
            this.Page.RegisterRequiresControlState(this);
        } 

    }
}