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
using Microsoft.Office.Interop.Outlook;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Globalization;

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
                //check admin permission
                if ((bool)Session["LoggedIn"] == false) Response.Redirect("../UserPage/Login.aspx");
                if (((bool)Session["LoggedIn"] == true) && ((string)Session["UserRole"] != "Admin")) Response.Redirect("../UserPage/Default.aspx");

                ViewState["TitleCounter"] = 0;
                ViewState["ResourceCounter"] = 0;

                ViewState["TitleSort"] = 0;
                ViewState["RoleSort"] = 0;
                ViewState["IDSort"] = 0;
                ViewState["NameSort"] = 0;
                ViewState["HoursSort"] = 0;

                lbl_StarofSaving.Text = "Not saved yet";
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
            int k = 0;
            Button btn = (Button)sender;
            TextBox tb = new TextBox();
            TextBox tb2 = new TextBox();
            string buttonId = btn.ID;
            tb = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + Regex.Match(buttonId, @"\d+").Value);

            tb.Text = "";
            ph_DynamicTitleTableRow.FindControl("tbr_ContentTitle" + Regex.Match(buttonId, @"\d+").Value).Visible = false;

            //--------------------- Re-Add Title to Resource ---------------------------
            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
                AddTitletoResource(i.ToString());

            UpdateTotalBillingLabel();
            UpdateTotalAssignedLabel();

            //---------------------- Remove duplicate-----------------------------------
            HighlightTitleDuplicate();
        }

        protected void btn_MinusResourceClick_event(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string buttonId = btn.ID;
            //DropDownList ddl = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + Regex.Match(buttonId, @"\d+").Value);
            TextBox tb = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + Regex.Match(buttonId, @"\d+").Value);
            Label lb = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + Regex.Match(buttonId, @"\d+").Value);

            //ddl.Items.Clear();
            //ddl.Items.Add("- Select Item -");
            tb.Text = "";
            lb.Text = "N/A";
            UpdateActualResource();
            UpdateTrainResource();
            UpdateTrainResource();
            ph_DynamicResourceTableRow.FindControl("tbr_ContentResource" + Regex.Match(buttonId, @"\d+").Value).Visible = false;
            UpdateHeadcountLabel();
            UpdateTotalAssignedLabel();
            UpdateTotalTrainee();
            UpdateTotalOff();
            HighlightResourceDuplicate();
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

                        RA.addResourceAllocation(date, RA.getIDbyEmployeeID(lb.Text), ddl1.SelectedValue, RA.getTitleIDby3LD(ddl2.SelectedItem.ToString()), ddl3.SelectedValue);
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
                            Label lb2 = (Label)ph_DynamicTitleTableRow.FindControl("lbl_TrainResources" + i);

                            RA.addTitleAllocation(date, RA.getTitleIDby3LD(tb1.Text), tb2.Text, lb.Text, lb2.Text);
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

                lbl_StarofSaving.Text = "Last saving at " + DateTime.Now.ToString("HH:mm");
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

                UpdateActualResource();
                UpdateHeadcountLabel();
                UpdateTotalBillingLabel();
                UpdateTotalOff();
                UpdateTotalAssignedLabel();
            }
        }

        protected void btn_Export_Click(object sender, EventArgs e)
        {
            DateTime dateTime_now = DateTime.UtcNow.Date;
            ResourceTableSort("Role ASC");
            ResourceTableSort("Title DESC");

            string tb_title = "<table id='title' class='table-striped table-bordered table-condensed' >"
                                + "<tr style='background-color:#376091; color: #FFFFFF'><td style='width: 170px;'><b>3LD</b></td><td style='width: 50px;'><b>Bill</b></td><td style='width: 50px;'><b>Actual</b></td><td style='width: 50px;'><b>Train</b></td></tr>";
            string tb_resource = "<table id='resource' class='table-striped table-bordered table-condensed' >"
                            + "<tr style='background-color:#376091; color: #FFFFFF'><td><b>ID</b></td><td style='width: 220px;'><b>Name</b></td><td><b>Role</b></td><td><b>Title</b></td><td><b>Hours</b></td></tr>";
            string tb_headcount = "<table id='headcount' class='table-striped table-bordered table-condensed' >"
                                        + "<tr style='background-color:#376091; color: #FFFFFF'><td style='width: 170px;' ><strong>Headcount</strong></td><td style='color: white; width: 154px'><b>" + lbl_headCount.Text + "</b></td></tr>"
                                        +"<tr><td>Total Billing</td><td><b>"+lbl_totalBilling.Text+"</b></td></tr>"
                                        +"<tr><td>Total Assigned</td><td style='color: #33CC33'><b>"+lbl_totalAssigned.Text+"</b></td></tr>"
                                        +"<tr><td>Total Trainee & Trainer</td><td><b>"+lbl_totalTrainee.Text+"</b></td></tr>"
                                        +"<tr><td>Off</td><td><b>"+lbl_Off.Text+"</b></td></tr>"
                                    +"</table>";
            string temp_title = "";
            string color_tr="";

            for (int i = 1; i <= (int)ViewState["TitleCounter"]; i++)
            {
                TextBox tb1 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + i.ToString());
                TextBox tb2 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_ExpectedResouces" + i.ToString());
                Label lbl1 = (Label)ph_DynamicTitleTableRow.FindControl("lbl_ActualResources" + i.ToString());
                Label lbl2 = (Label)ph_DynamicTitleTableRow.FindControl("lbl_TrainResources" + i.ToString());
                Color myColor = lbl1.ForeColor;

                if (tb1.Text != "") tb_title = tb_title + "<tr><td>" + tb1.Text + "</td><td>" + tb2.Text + "</td><td style='color: " + myColor.ToString().Split('[', ']')[1] + "'><b>" + lbl1.Text + "</b></td><td>" + lbl2.Text + "</td></tr>";
            }

            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
            {
                Label lbl1 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + i.ToString());
                TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + i.ToString());
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + i.ToString());
                DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + i.ToString());
                DropDownList ddl3 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + i.ToString());
                string color_text="";
                string color_cell = "";
                string hour_color = "";
                string birthday = "";
                string birthday_icon = "";
                string title= ddl2.SelectedItem.Text;
                
                //---------------------------------------------------role-------------------------------
                if (ddl1.SelectedItem.Text == "Core") {color_text = "white"; color_cell="#C0504D";}
                else if (ddl1.SelectedItem.Text == "Billable") {color_text = "white"; color_cell="#00B050";}
                else if ((ddl1.SelectedItem.Text == "Trainee") || (ddl1.SelectedItem.Text == "Trainer")) {color_text = "black"; color_cell="#FFFF99";}
                else if (ddl1.SelectedItem.Text == "Backup") {color_text = "white"; color_cell="#31849B";}
                else if (ddl1.SelectedItem.Text == "Project Leader") {color_text = "white"; color_cell="#FF0000";}
                else if (ddl1.SelectedItem.Text == "Off") { color_text = "white"; color_cell = "#7F7F7F"; }

                //--------------------------------------------birthday----------------------------------
                if (RA.getBirthDay(lbl1.Text) != "")
                {
                    string[] s = RA.getBirthDay(lbl1.Text).Split('/');
                    birthday = s[0] + "/" + s[1];
                    if (birthday == dateTime_now.ToString("M/d")) birthday_icon = "<img src='../Image/cake.png' width='20' height='20' />";
                }

                //--------------------------------------------Export----------------------------------
                if (title == "- Select Item -") title = "N/A";
                if (Convert.ToDouble(ddl3.SelectedItem.Text) < 8) hour_color = "red";
                if (tb1.Text != "")
                {
                    if (temp_title != title)
                    {
                        temp_title = title;
                        if (color_tr == "#FFFFFF") color_tr = "#DBE5F1";
                        else color_tr = "#FFFFFF";
                    }
                    tb_resource = tb_resource + "<tr style='background-color:" + color_tr + " ;'><td>" + lbl1.Text + "</td><td style='text-align: left;'>" + tb1.Text + "&nbsp;&nbsp;&nbsp;" + birthday_icon + "</td><td style='color: " + color_text + "; background-color:" + color_cell + "'>" + RA.getProjectAbbreviation(ddl1.SelectedItem.Text) + "</td><td>" + title + "</td><td style='color: "+hour_color+"'>" + ddl3.SelectedItem.Text + "</td></tr>";
                }
            }

            string tb_final = "<table valign='top' style='font-family: Times New Roman; font-size: 12pt'"
                                + "<tr><td rowspan='2' style='vertical-align:top'>" + tb_resource + "</table></td><td rowspan='2'>&nbsp;&nbsp;&nbsp;</td><td style='vertical-align:top; height: 70px'>" + tb_title + "</table></td></tr>"
                                + "<tr><td style='vertical-align:top'> <br />" + tb_headcount + "</td></tr>"
                            +"</table><br /><br />";

            lbl_titlexport.Text = "[LFDN] - Resource allocation for Content (" + System.DateTime.Now.ToString("ddd") + ")";
            lbl_contentexport.Text = tb_final;

            TableResourceAllocation.Visible = false;
            TableReport.Visible = true;
        }

        protected void btn_closeReport_Click(object sender, EventArgs e)
        {
            TableResourceAllocation.Visible = true;
            TableReport.Visible = false;
        }

        protected void btn_ClearRole_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
            {
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + i.ToString());
                Label lbl1 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + i.ToString());
                TableRow tbr = (TableRow)ph_DynamicResourceTableRow.FindControl("tbr_ContentResource" + i.ToString());

                tbr.Attributes.Remove("style");
                ddl1.ClearSelection();
                ddl1.Items.FindByText("Billable").Selected = true;
                ddl1.Attributes.Remove("style");
            }

            UpdateTotalTrainee();
            UpdateTotalOff();
            UpdateActualResource();
            UpdateTrainResource();
        }

        protected void btn_ClearTitle_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
            {
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + i.ToString());
                Label lbl1 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + i.ToString());
                TableRow tbr = (TableRow)ph_DynamicResourceTableRow.FindControl("tbr_ContentResource" + i.ToString());

                tbr.Attributes.Remove("style");
                ddl1.ClearSelection();
                ddl1.Items.FindByText("- Select Item -").Selected = true;
                ddl1.Enabled = true;
            }

            UpdateActualResource();
            UpdateTrainResource();
            UpdateTotalAssignedLabel();
        }

        protected void btn_ClearBill_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= (int)ViewState["TitleCounter"]; i++)
            {
                TextBox tb1 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + i.ToString());
                TextBox tb2 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_ExpectedResouces" + i.ToString());

                if (tb1.Text != "")
                    tb2.Text = "0";
            }

            UpdateTotalBillingLabel();
        }

        protected void btn_MultAdd_Click(object sender, ImageClickEventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }

        protected void btn_importMulRs_Click(object sender, EventArgs e)
        {
            ClearAll();

            string[] allLines = txt_importMulRS.Text.Split('\n');
            int i = 1;

            foreach (string str in allLines)
            {
                if (str != "")
                {
                    HandleAddResource(i.ToString());
                    AddTitletoResource(i.ToString());

                    Label lbl1 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + i.ToString());
                    Label lbl2 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceExist" + i.ToString());
                    TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + i.ToString());

                    tb1.Text = str;

                    if (!RA.FullNameExist(tb1.Text))
                    {
                        lbl2.Visible = true;
                        lbl1.Text = "N/A";
                    }
                    else
                    {
                        lbl2.Visible = false;
                        lbl1.Text = RA.getEmployeeIDbyName(tb1.Text);
                    }

                    i++;
                }
            }

            ViewState["ResourceCounter"] = i - 1;
            UpdateHeadcountLabel();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }

        protected void tb_TitleTextChanged_event(object sender, EventArgs e)
        {
            int k=0;
            TextBox tb = (TextBox)sender;
            Label lb = new Label();

            string textboxId = tb.ID;
            string str_ToolTip = "";
            lb = (Label)ph_DynamicTitleTableRow.FindControl("lbl_TitleExist" + Regex.Match(textboxId, @"\d+").Value);

            if (!titleManager.LDExist(tb.Text)) lb.Visible = true;
            else
            {
                lb.Visible = false;
                for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
                    AddTitletoResource(i.ToString());

                //---------------------- Get Hint -----------------------------------------
                DataTable tb_Hint = RA.getHintbyTitle(tb.Text);
                if (tb_Hint.Rows.Count > 0)
                {
                    for (int i = 0; i < tb_Hint.Rows.Count; i++)
                    {
                        tb.ToolTip = tb.ToolTip + tb_Hint.Rows[i].ItemArray[1].ToString() + " - (" + tb_Hint.Rows[i].ItemArray[0].ToString() + ")(" + tb_Hint.Rows[i].ItemArray[2].ToString() + ")" + " \r\n";
                    }
                    //tb.ToolTip = str_ToolTip;
                    tb.Attributes.Add("style", "white-space:pre-wrap;");
                }
            }

            //---------------------- highlight duplicate-----------------------------------
            HighlightTitleDuplicate();
        }

        protected void tb_ResouceTextChanged_event(object sender, EventArgs e)
        {
            int k = 0;
            TextBox tb = (TextBox)sender;
            Label lb = new Label();
            Label lb2 = new Label();

            string textboxId = tb.ID;
            lb = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceExist" + Regex.Match(textboxId, @"\d+").Value);
            lb2 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + Regex.Match(textboxId, @"\d+").Value);
            DropDownList ddl = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + Regex.Match(textboxId, @"\d+").Value);

            if (tb.Text != "")
            {
                if (!RA.FullNameExist(tb.Text))
                {
                    lb.Visible = true;
                    lb2.Text = "N/A";
                }
                else
                {
                    lb.Visible = false;
                    lb2.Text = RA.getEmployeeIDbyName(tb.Text);
                }
            }
            else lb2.Text = "N/A";

            HighlightResourceDuplicate();
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
            //DropDownList ddl1 = (DropDownList)sender;
            //string ddlId = ddl1.ID;
            ////TableCell tbc = (TableCell)ph_DynamicResourceTableRow.FindControl("tbc_TitleResource" + Regex.Match(ddlId, @"\d+").Value);
            //TableRow tbr = (TableRow)ph_DynamicResourceTableRow.FindControl("tbr_ContentResource" + Regex.Match(ddlId, @"\d+").Value);
            //tbr.Attributes.Add("style", "background-color:" + RA.getColorCode(ddl1.SelectedItem.Text) + ";");

            UpdateActualResource();
            UpdateTrainResource();
            UpdateTotalAssignedLabel();
        }

        protected void ddl_WorkingHoursSelectedIndexChannged_event(object sender, EventArgs e)
        {
            DropDownList ddl1 = (DropDownList)sender;
            string ddlId = ddl1.ID;
            Label lbl = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + Regex.Match(ddlId, @"\d+").Value);
            TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + Regex.Match(ddlId, @"\d+").Value);
            TextBox tb2 = new TextBox();

            HighlightResourceDuplicate();
            UpdateActualResource();
            UpdateTrainResource();
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
            UpdateActualResource();
            UpdateTrainResource();
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
                double count2 = 0;
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
                    DropDownList ddl3 = new DropDownList();
                    Label lbl2 = new Label();
                    ddl = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + j.ToString());
                    ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + j.ToString());
                    ddl3 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + j.ToString());
                    lbl2 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + j.ToString());

                    if (lbl2.Text != "N/A")
                    {
                        if ((ddl3.SelectedItem.Text == "Core") || (ddl3.SelectedItem.Text == "Billable") || (ddl3.SelectedItem.Text == "Backup"))
                            if (ddl.SelectedItem.Text == tb.Text) count = count + Convert.ToDouble(ddl2.SelectedItem.Text) / 8;

                        if ((ddl3.SelectedItem.Text == "Core") || (ddl3.SelectedItem.Text == "Billable"))
                            if (ddl.SelectedItem.Text == tb.Text) count2 = count2 + Convert.ToDouble(ddl2.SelectedItem.Text) / 8;
                    
                    }
                }

                lbl.Text = count.ToString();

                if (tb2.Text != "")
                {
                    if (Convert.ToDouble(tb2.Text) < count2) lbl.ForeColor = Color.Red;
                    else if (Convert.ToDouble(tb2.Text) == count2)
                    {
                        if (Convert.ToDouble(tb2.Text) < count) lbl.ForeColor = Color.CornflowerBlue;
                        else lbl.ForeColor = Color.LimeGreen;
                    }
                    else lbl.ForeColor = Color.Black;
                }
            }
        }

        public void UpdateTrainResource()
        {
            for (int i = 1; i <= (int)ViewState["TitleCounter"]; i++)
            {
                double count = 0;
                TextBox tb = new TextBox();
                TextBox tb2 = new TextBox();
                Label lbl = new Label();
                tb = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + i.ToString());
                //tb2 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_ExpectedResouces" + i.ToString());
                lbl = (Label)ph_DynamicTitleTableRow.FindControl("lbl_TrainResources" + i.ToString());

                for (int j = 1; j <= (int)ViewState["ResourceCounter"]; j++)
                {
                    DropDownList ddl = new DropDownList();
                    DropDownList ddl2 = new DropDownList();
                    DropDownList ddl3 = new DropDownList();
                    Label lbl2 = new Label();
                    ddl = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + j.ToString());
                    ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + j.ToString());
                    ddl3 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + j.ToString());
                    lbl2 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + j.ToString());

                    if (lbl2.Text != "N/A")
                        if ((ddl3.SelectedItem.Text == "Trainee") || (ddl3.SelectedItem.Text == "Trainer"))
                            if (ddl.SelectedItem.Text == tb.Text) count = count + Convert.ToDouble(ddl2.SelectedItem.Text) / 8;
                }

                lbl.Text = count.ToString();

                //if (tb2.Text != "")
                //{
                //    if (Convert.ToDouble(tb2.Text) < count) lbl.ForeColor = Color.Red;
                //    else if (Convert.ToDouble(tb2.Text) == count) lbl.ForeColor = Color.LightGreen;
                //    else lbl.ForeColor = Color.Black;
                //}
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
                TextBox tb = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + i.ToString());
                Label lbl1 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + i.ToString());
                TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + i.ToString());
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + i.ToString());
                DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + i.ToString());
                DropDownList ddl3 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + i.ToString());

                if (lbl1.Text != "N/A")
                    dt.Rows.Add(lbl1.Text, tb1.Text, ddl1.SelectedItem.Text, ddl2.SelectedItem.Text, ddl2.Enabled.ToString(), ddl3.SelectedItem.Text);
            }

            dt.DefaultView.Sort = sortType;
            dt = dt.DefaultView.ToTable();

            ViewState["ResourceCounter"] = dt.Rows.Count;
            ph_DynamicResourceTableRow.Controls.Clear();

            string color_tr = "";
            string temp = "";
            string value="";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int j = i + 1;
                HandleAddResource(j.ToString());
                AddTitletoResource(j.ToString());

                Label lbl1 = (Label)ph_DynamicResourceTableRow.FindControl("lbl_ResourceID" + j.ToString());
                TextBox tb1 = (TextBox)ph_DynamicResourceTableRow.FindControl("txt_Resource" + j.ToString());
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Role" + j.ToString());
                DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_Title" + j.ToString());
                DropDownList ddl3 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + j.ToString());
                TableRow tbr = (TableRow)ph_DynamicResourceTableRow.FindControl("tbr_ContentResource" + j.ToString());
                RequiredFieldValidator rfvSelectTitle = (RequiredFieldValidator)ph_DynamicResourceTableRow.FindControl("rfvSelectTitle" + j.ToString());
                bool state = Convert.ToBoolean(dt.Rows[i].ItemArray[4].ToString());

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

                if (dt.Rows[i].ItemArray[2].ToString() == "Project Leader") ddl1.Attributes.Add("style", "border: 2px solid red;");
                else if (dt.Rows[i].ItemArray[2].ToString() == "Core") ddl1.Attributes.Add("style", "border: 2px solid red;");
                else if (dt.Rows[i].ItemArray[2].ToString() == "Backup") ddl1.Attributes.Add("style", "border: 2px solid #31849B;");

                //------------------- color tablerow -------------------------
                if ((sortType == "Title ASC") || (sortType == "Title DESC")) value = dt.Rows[i].ItemArray[3].ToString();
                else if ((sortType == "Role ASC") || (sortType == "Role DESC")) value = dt.Rows[i].ItemArray[2].ToString();
                else if ((sortType == "WorkingHours ASC") || (sortType == "WorkingHours DESC")) value = dt.Rows[i].ItemArray[5].ToString();

                if (temp != value)
                {
                    temp = value;
                    if (color_tr == "#FFFFFF") color_tr = "#DBE5F1";
                    else color_tr = "#FFFFFF";
                }
                tbr.Attributes.Add("style", "background-color:" + color_tr + ";");
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
            UpdateTotalTrainee();
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
                //ddl1.BorderColor = Color.Gray;
            }
            else if (ddl1.SelectedItem.Text == "Project Leader")
            {
                ddl2.ClearSelection();
                ddl2.Items.FindByText("- Select Item -").Selected = true;
                ddl2.Enabled = false;
                rfvSelectTitle.Enabled = false;
                ddl1.Attributes.Add("style", "border: 2px solid red;");
            }
            else if (ddl1.SelectedItem.Text == "Core")
            {
                ddl2.Enabled = true;
                rfvSelectTitle.Enabled = true;
                ddl1.Attributes.Add("style", "border: 2px solid red;");
            }
            else if (ddl1.SelectedItem.Text == "Backup")
            {
                ddl2.Enabled = true;
                rfvSelectTitle.Enabled = true;
                ddl1.Attributes.Remove("style");
                ddl1.Attributes.Add("style", "border: 2px solid #31849B;");
            }
            else if ((ddl1.SelectedItem.Text == "Trainer") || (ddl1.SelectedItem.Text == "Trainee"))
            {
                ddl2.Enabled = true;
                rfvSelectTitle.Enabled = true;
                ddl1.Attributes.Remove("style");
                //ddl1.BorderColor = Color.Gold;
            }
            else
            {
                ddl2.Enabled = true;
                rfvSelectTitle.Enabled = true;
                ddl1.Attributes.Remove("style");
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

                string title = RA.get3LDbyID(dt.Rows[i].ItemArray[4].ToString());
                if (title == "") title = "- Select Item -";

                lbl1.Text = RA.getEmployeeIDbyID(dt.Rows[i].ItemArray[2].ToString());
                tb1.Text = RA.getFullName(lbl1.Text);
                ddl1.ClearSelection();
                ddl2.ClearSelection();
                ddl3.ClearSelection();
                ddl1.Items.FindByText(RA.getProjectRoleName(dt.Rows[i].ItemArray[3].ToString())).Selected = true;
                ChangeRole(j.ToString());
                ddl2.Items.FindByText(title).Selected = true;
                ddl3.Items.FindByText(RA.getValueHours(dt.Rows[i].ItemArray[5].ToString())).Selected = true;
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
                Label lbl2 = (Label)ph_DynamicTitleTableRow.FindControl("lbl_TrainResources" + j.ToString());

                tb1.Text = RA.get3LDbyID(dt.Rows[i].ItemArray[2].ToString());
                tb2.Text = dt.Rows[i].ItemArray[3].ToString();
                lbl1.Text = dt.Rows[i].ItemArray[4].ToString();
                lbl2.Text = dt.Rows[i].ItemArray[5].ToString();

                //if (tb2.Text != "")
                //{
                //    if (Convert.ToDouble(tb2.Text) < Convert.ToDouble(lbl1.Text)) lbl1.ForeColor = Color.Red;
                //    else if (Convert.ToDouble(tb2.Text) == Convert.ToDouble(lbl1.Text)) lbl1.ForeColor = Color.LightGreen;
                //    else lbl1.ForeColor = Color.Black;
                //}
            }
        }

        public void HighlightTitleDuplicate()
        {
            for (int i = 1; i <= (int)ViewState["TitleCounter"]; i++)
            {
                TextBox tb1 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + i.ToString());
                tb1.Attributes.Remove("style");

                if (tb1.Text != "")
                    for (int j = i+1; j <= (int)ViewState["TitleCounter"]; j++)
                    {
                        TextBox tb2 = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Title" + j.ToString());
                        if (tb1.Text == tb2.Text) tb1.Attributes.Add("style", "border: 2px solid red;");
                    }
            }
        }

        public void HighlightResourceDuplicate()
        {
            for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
            {
                Label lbl1 = (Label)ph_DynamicTitleTableRow.FindControl("lbl_ResourceID" + i.ToString());
                DropDownList ddl1 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + i.ToString());
                TextBox tb = (TextBox)ph_DynamicTitleTableRow.FindControl("txt_Resource" + i.ToString());

                tb.Attributes.Remove("style");

                if (lbl1.Text != "N/A")
                    for (int j = i + 1; j <= (int)ViewState["ResourceCounter"]; j++)
                    {
                        Label lbl2 = (Label)ph_DynamicTitleTableRow.FindControl("lbl_ResourceID" + j.ToString());
                        DropDownList ddl2 = (DropDownList)ph_DynamicResourceTableRow.FindControl("ddl_WorkingHours" + j.ToString());
                        if (lbl1.Text == lbl2.Text)
                        {
                            double hour1 = Convert.ToDouble(ddl1.SelectedItem.Text);
                            double hour2 = Convert.ToDouble(ddl2.SelectedItem.Text);
                            if ((hour1 + hour2) > 8) tb.Attributes.Add("style", "border: 2px solid red;");
                        }
                                
                    }
            }
        }

        private string ReadSignature()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Signatures";
            string signature = string.Empty;
            DirectoryInfo diInfo = new DirectoryInfo(appDataDir);

            if (diInfo.Exists)
            {
                FileInfo[] fiSignature = diInfo.GetFiles("*.htm");

                if (fiSignature.Length > 0)
                {
                    StreamReader sr = new StreamReader(fiSignature[0].FullName, Encoding.Default);
                    signature = sr.ReadToEnd();

                    if (!string.IsNullOrEmpty(signature))
                    {
                        string fileName = fiSignature[0].Name.Replace(fiSignature[0].Extension, string.Empty);
                        signature = signature.Replace(fileName + "_files/", appDataDir + "/" + fileName + "_files/");
                    }
                }
            }
            return signature;
        }
    }
}