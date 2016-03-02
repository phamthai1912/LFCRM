using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Outlook;
using System.Net.Mail;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace LFCRM.AdminPage
{
    public partial class BillingList : System.Web.UI.Page
    {
        Class.csBillingList billinglist = new Class.csBillingList();
        Class.csCommonClass style = new Class.csCommonClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //check admin permission
                if ((bool)Session["LoggedIn"] == false) Response.Redirect("../UserPage/Login.aspx");
                if (((bool)Session["LoggedIn"] == true) && ((string)Session["UserRole"] != "Admin")) Response.Redirect("../UserPage/Default.aspx");

                String subject = DateTime.Now.ToString("dd MMM, yyyy");
                String day = DateTime.Now.ToString("ddd");
                lb_status.Text = "Billing list for Content on " + subject + " (" + day + ") ";

                loadBillingProject(txt_date.Text);
                loadBillingList(txt_date.Text);
            }
        }

        protected void txt_date_TextChanged(object sender, EventArgs e)
        {
            String subject = "";
            String day = "";

            if (txt_date.Text.ToString() == "")
            {
                subject = DateTime.Now.ToString("dd MMM, yyyy");
                day = DateTime.Now.ToString("ddd");
            }
            else
            {
                subject = Convert.ToDateTime(txt_date.Text).ToString("dd MMM, yyyy");
                DateTime date = Convert.ToDateTime(txt_date.Text);
                day = date.ToString("ddd");                
            }
            lb_status.Text = "Billing list for Content on " + subject + " (" + day + ") ";
            loadBillingProject(txt_date.Text);
            loadBillingList(txt_date.Text);
        }

        public void loadBillingList(String _date)
        {
            DataTable tb = billinglist.getBillingList(_date);

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            int totaltester = 0;
            float totalhours = 0;
            float hoursFW = 0;
            float hoursGaming = 0;            

            //Building the Data rows.
            String gaming = "";
            String fw = "";
            String section = "";
            ArrayList listname = billinglist.getNewListName(tb);

            foreach (DataRow row in tb.Rows)
            {
                int i = 0;
                int j = 0;
                section = row[1].ToString();
                
                String fullname = row[0].ToString();

                totalhours = totalhours + float.Parse(row[2].ToString());
                //Convert.ToInt16(row[2].ToString());
                if (billinglist.checkFwTitle(section) == true)
                {
                    hoursFW = hoursFW + float.Parse(row[2].ToString());
                    //Convert.ToInt16(row[2].ToString());

                    fw = fw + "<tr>";
                    foreach (DataColumn column in tb.Columns)
                    {
                        if (i == 0)
                            fw = fw + "<td style='text-align:left;border:1px solid #000000;padding:5px;'>" + listname[totaltester] + "</td>";
                        else
                            fw = fw + "<td style='text-align:center;border:1px solid #000000;'>" + row[column.ColumnName] + "</td>";
                        i++;
                    }
                    fw = fw + "</tr>";
                    totaltester++;
                }
                else
                {
                    hoursGaming = hoursGaming + float.Parse(row[2].ToString());
                    //Convert.ToInt16(row[2].ToString());

                    gaming = gaming + "<tr>";
                    foreach (DataColumn column in tb.Columns)
                    {
                        if (j == 0)
                            gaming = gaming + "<td style='text-align:left;border:1px solid #000000;padding:5px;'>" + listname[totaltester] + "</td>";
                        else
                            gaming = gaming + "<td style='text-align:center;border:1px solid #000000;'>" + row[column.ColumnName] + "</td>";
                        j++;
                    }
                    gaming = gaming + "</tr>";
                    totaltester++;
                }
            }
            totaltester = Convert.ToInt32(totalhours) / 8;

            String body = "";
            if(fw == "")
                body = "<tr style='border:1px solid #000000;background-color:#b7dee8;font-weight:600;text-align:center;'>" +
                        "<td colspan='2' style='border-right:1px solid #000000;border-bottom:1px solid #000000;padding:5px;'>GAMING QA</td>" +
                        "<td style='border-bottom:1px solid #000000;'>" + hoursGaming + "</td>" +
                        "</tr>" +
                        gaming;
            else
                body = "<tr style='border:1px solid #000000;background-color:#fabf8f;font-weight:600;text-align:center;'>" +
                        "<td colspan='2' style='border-right:1px solid #000000;border-bottom:1px solid #000000;padding:5px;'>FIRMWARE QA</td>" +
                        "<td style='border-bottom:1px solid #000000;'>" + hoursFW + "</td>" +
                        "</tr>" +
                        fw +
                        "<tr style='border:1px solid #000000;background-color:#b7dee8;font-weight:600;text-align:center;'>" +
                        "<td colspan='2' style='border-right:1px solid #000000;border-bottom:1px solid #000000;padding:5px;'>GAMING QA</td>" +
                        "<td style='border-bottom:1px solid #000000;'>" + hoursGaming + "</td>" +
                        "</tr>" +
                        gaming;

            String header = "" +
            "<table id='billinglist' runat='server' style='border:1px solid #000000; width:440px; border-collapse: collapse;font-family:Tahoma;font-size:13px;'>" +
                "<tr>" +
                    "<td style='background-color:#1f497d; color:white; text-align:center; vertical-align:middle; font-size:14px; font-weight:800;'>Billing List</td>" +
                    "<td style='width:170px;background-color:#4f81bd;border:1px solid #000000;margin-right:0;color:white;font-weight:800;'>" +
                        "<table style='width:100%;margin:0;color:white;font-weight:800;text-align:right;'>" +
                            "<tr style='border-bottom:1px solid #000000;'>" +
                                "<td style='padding:5px;text-align:right;border-bottom:1px solid #000000;'>Tester(s):</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='padding:5px;text-align:right;'>Total Hour(s):</td>" +
                            "</tr>" +
                        "</table>" +
                    "</td>" +
                    "<td style='width:100px;background-color:#4f81bd;border:1px solid #000000; margin-right:0;color:white;font-weight:800;'>" +
                        "<table style='width:100%;color:white;font-weight:800;text-align:center;'>" +
                            "<tr>" +
                                "<td style='border-bottom:1px solid #000000;padding:5px;'>" + totaltester + "</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='padding:5px;'>" + totalhours + "</td>" +
                            "</tr>" +
                        "</table>" +
                    "</td>" +
                "</tr>" +
                "<tr style='background-color:#00b050;border-bottom:1px solid #000000;font-weight:800;text-align:center;color:white;'>" +
                    "<td style='border:1px solid #000000;padding:5px;'>Tester</td>" +
                    "<td style='border:1px solid #000000;'>Title</td>" +
                    "<td style='border:1px solid #000000;'>Hours</td>" +
                "</tr>";
            String footer = "</table>";
            
            html.Append(header);
            html.Append(body);            
            html.Append(footer);

            //Append the HTML string to Placeholder.
            PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
        }

        public void loadBillingProject(String _date)
        {
            DataTable tb = billinglist.getBillingProject(_date);

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            float totalhours = 0;
            float hoursFW = 0;
            float hoursGaming = 0;

            //Building the Data rows.
            String gaming = "";
            String fw = "";
            String section = "";
            foreach (DataRow row in tb.Rows)
            {
                section = row[0].ToString();
                totalhours = totalhours + float.Parse(row[2].ToString());
                //Convert.ToInt16(row[2].ToString());
                if (billinglist.checkFwTitle(section) == true)
                {
                    hoursFW = hoursFW + Convert.ToInt16(row[2].ToString());

                    fw = fw + "<tr>";
                    foreach (DataColumn column in tb.Columns)
                    {
                        fw = fw + "<td style='text-align:center;border:1px solid #000000;padding:5px;'>" + row[column.ColumnName] + "</td>";
                    }
                    fw = fw + "</tr>";
                }
                else
                {
                    hoursGaming = hoursGaming + float.Parse(row[2].ToString());
                    //Convert.ToInt16(row[2].ToString());

                    gaming = gaming + "<tr>";
                    foreach (DataColumn column in tb.Columns)
                    {
                        gaming = gaming + "<td style='text-align:center;border:1px solid #000000;padding:5px;'>" + row[column.ColumnName] + "</td>";
                        
                    }
                    gaming = gaming + "</tr>";
                }
            }

            String body = "";
            if (fw == "")
                body = "<tr style='border:1px solid #000000;background-color:#b7dee8;font-weight:600;text-align:center;'>" +
                        "<td colspan='2' style='border-right:1px solid #000000;border-bottom:1px solid #000000;padding:5px;'>GAMING QA</td>" +
                        "<td style='border-bottom:1px solid #000000;'>" + hoursGaming + "</td>" +
                        "</tr>" +
                        gaming + "";
            else
                body = "<tr style='border:1px solid #000000;background-color:#fabf8f;font-weight:600;text-align:center;'>" +
                        "<td colspan='2' style='border-right:1px solid #000000;border-bottom:1px solid #000000;padding:5px;'>FIRMWARE QA</td>" +
                        "<td style='border-bottom:1px solid #000000;'>" + hoursFW + "</td>" +
                        "</tr>" +
                        fw +
                        "" +
                        "<tr style='border:1px solid #000000;background-color:#b7dee8;font-weight:600;text-align:center;'>" +
                        "<td colspan='2' style='border-right:1px solid #000000;border-bottom:1px solid #000000;padding:5px;'>GAMING QA</td>" +
                        "<td style='border-bottom:1px solid #000000;'>" + hoursGaming + "</td>" +
                        "</tr>" +
                        gaming + "";

            String header = "" +
            "<table id='billingproject' runat='server' style='border:1px solid #000000; width:340px; border-collapse: collapse;font-family:Tahoma;font-size:13px;'>" +
                "<tr style='background-color:#4f81bd;'>"+
                    "<td colspan='3' style='border-bottom:1px solid #000000;color:white; text-align:center; vertical-align:middle; font-weight:800;padding:5px;'>Hours by Project</td></tr>" +
                "<tr style='background-color:#31869b;'>" +
                    "<td colspan='2' style='background-color:#31869b; color:white;border-bottom:1px solid #000000; text-align:center; vertical-align:middle; font-weight:800;padding:5px;'>LF CONTENT</td>" +
                    "<td style='background-color:#4f81bd;border:1px solid #000000;margin-right:0;color:white;font-weight:800;padding:5px;width:100px;text-align:center;'>" +
                        totalhours +
                    "</td>"+
                "</tr>";

            String footer = "</table>";

            html.Append(header);
            html.Append(body);
            html.Append(footer);

            //Append the HTML string to Placeholder.
            PlaceHolder2.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void SendEmail(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    PlaceHolder1.RenderControl(hw);
                    //GridView1.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    MailMessage mm = new MailMessage("tan.thanh.vo@logigear.com", "tan.thanh.vo@logigear.com");
                    mm.Subject = "GridView Email";
                    mm.Body = "<b>Tan Vo</b>Test:<hr />" + sw.ToString(); ;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.logigear.com";
                    smtp.EnableSsl = true;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = "tan.thanh.vo@logigear.com";
                    NetworkCred.Password = "qwert098!@#$";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }
        }

    }
}