using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.AdminPage
{
    public partial class BugStatistic : System.Web.UI.Page
    {
        Class.csBugStatistic statistic = new Class.csBugStatistic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load_grid("");
            }
                
        }

        protected void txt_date_TextChanged(object sender, EventArgs e)
        {
            String dd = txt_date.Text;
            load_grid(dd);

        }

        public void load_grid(String date)
        {
            String dd = txt_date.Text;
            GridView1.DataSource = statistic.getResourceAlocation(date);
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                String date = e.Row.Cells[0].Text.ToString();
                String name = e.Row.Cells[1].Text.ToString();
                //String title = e.Row.Cells[2].Text.ToString();
                Label lbtitle = ((Label)e.Row.FindControl("lb_title"));
                Label lb = e.Row.Cells[5].FindControl("lb_nobugs") as Label;

                lb.Text = statistic.getNoBugs(date, name, lbtitle.Text);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void btn_updatebugs_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[6].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked)
                    {
                        String date = HttpUtility.HtmlDecode(row.Cells[0].Text).ToString();
                        String name = HttpUtility.HtmlDecode(row.Cells[1].Text).ToString();
                        //String title = HttpUtility.HtmlDecode(row.Cells[2].Text).ToString();
                        Label lbtitle = ((Label)row.FindControl("lb_title"));
                        String numberbugs = row.Cells[5].Controls.OfType<TextBox>().FirstOrDefault().Text;

                        String currentbug = row.Cells[5].Controls.OfType<Label>().FirstOrDefault().Text;

                        if (numberbugs != "")
                        {
                            if (currentbug == "N/A")
                                statistic.addBugs(date, name, lbtitle.Text, numberbugs);
                            statistic.updateBugs(date, name, lbtitle.Text, numberbugs);
                        }
                    }
                }
            }
            btn_updatebugs.Visible = false;

            String dd = txt_date.Text;
            load_grid(dd);
        }

        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            bool isUpdateVisible = false;
            CheckBox chk = (sender as CheckBox);
            if (chk.ID == "chkAll")
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        row.Cells[6].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                    }
                }
            }
            CheckBox chkAll = (GridView1.HeaderRow.FindControl("chkAll") as CheckBox);
            chkAll.Checked = true;
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[6].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    
                    row.Cells[5].Controls.OfType<Label>().FirstOrDefault().Visible = !isChecked;
                    Label lbtitle = ((Label)row.FindControl("lb_title"));
                    if (lbtitle.Text != "")
                    { 
                        if (row.Cells[5].Controls.OfType<TextBox>().ToList().Count > 0)
                        {
                             row.Cells[5].Controls.OfType<TextBox>().FirstOrDefault().Visible = isChecked;
                             String bug = statistic.getNumberBugs(row.Cells[5].Controls.OfType<Label>().FirstOrDefault().Text);
                             row.Cells[5].Controls.OfType<TextBox>().FirstOrDefault().Text = bug;
                        }
                        if (isChecked && !isUpdateVisible)
                        {
                            isUpdateVisible = true;
                        }
                        if (!isChecked)
                        {
                            chkAll.Checked = false;
                        }
                    }
                }
            }
            btn_updatebugs.Visible = isUpdateVisible;
        }
    }
}