using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
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
                //check admin permission
                if ((bool)Session["LoggedIn"] == false) Response.Redirect("../UserPage/Login.aspx");
                if (((bool)Session["LoggedIn"] == true))
                {
                    txt_month.Text = DateTime.Now.ToString("MM/yyyy");
                    lb_time.Text = "Bug Statistics on " + DateTime.Now.ToString("MMM, yyyy");
                    LoadChart();
                }
            }     
        }
        public void LoadChart()
        {
            Chart1.DataSource = statistic.GetListUser(txt_month.Text);
            Chart1.DataBind();
            Chart1.Series["Series1"].IsValueShownAsLabel = true;

            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            Chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);

            DataTable tb = statistic.GetListUser(txt_month.Text);
            for (int x = 0; x < tb.Rows.Count; x++)
            {
                // Add each point and set its Label
                DataPoint pt = Chart1.Series["Series1"].Points[x];

                if (pt.AxisLabel == Session["FullName"].ToString())
                    pt.Color = Color.Red;
            }

        }

        protected void txt_month_TextChanged(object sender, EventArgs e)
        {
            //check admin permission
            if ((bool)Session["LoggedIn"] == false) Response.Redirect("../UserPage/Login.aspx");
            if (((bool)Session["LoggedIn"] == true))
            {
                Page.Validate("month");
                if (Page.IsValid)
                {
                    if (txt_month.Text == "")
                        txt_month.Text = DateTime.Now.ToString("MM/yyyy");

                    lb_time.Text = "Bug Statistics on " + (Convert.ToDateTime(txt_month.Text)).ToString("MMM, yyyy");
                    LoadChart();
                }
            }
        }
    }
}