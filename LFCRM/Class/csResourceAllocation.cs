using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace LFCRM.Class
{
    public class csResourceAllocation : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            context.LogRequest += new EventHandler(OnLogRequest);
        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public Tuple<TableRow, Button> AddTitle(string Id)
        {
            TextBox tb1 = new TextBox();
            TextBox tb2 = new TextBox();
            Label lbl1 = new Label();
            Label lbl2 = new Label();
            Button btn = new Button();
            TableRow tbr = new TableRow();
            TableCell tbc1 = new TableCell();
            TableCell tbc2 = new TableCell();
            TableCell tbc3 = new TableCell();
            TableCell tbc4 = new TableCell();
            TableCell tbc5 = new TableCell();

            tb1.ID = "txt_Title" + Id;
            tb2.ID = "txt_ExpectedResouces" + Id;
            lbl1.Text = Id;
            lbl2.ID = "lbl_ActualResources" + Id;
            btn.ID = "btn_Remove" + Id;
            tbr.ID = "tbr_Content" + Id;
            btn.Text = " - ";

            tb1.ControlStyle.CssClass = "form-control";
            tb2.ControlStyle.CssClass = "form-control";
            btn.ControlStyle.CssClass = "btn btn-success";

            tbc1.Controls.Add(lbl1);
            tbc2.Controls.Add(tb1);
            tbc3.Controls.Add(tb2);
            tbc4.Controls.Add(lbl2);
            tbc5.Controls.Add(btn);
            tbr.Cells.Add(tbc1);
            tbr.Cells.Add(tbc2);
            tbr.Cells.Add(tbc3);
            tbr.Cells.Add(tbc4);
            tbr.Cells.Add(tbc5);           

            return new Tuple<TableRow, Button>(tbr, btn);
        }

        public Tuple<TableRow, Button> AddResource(string Id)
        {
            Label lbl = new Label();
            TextBox tb = new TextBox();
            DropDownList ddl1 = new DropDownList();
            DropDownList ddl2 = new DropDownList();
            DropDownList ddl3 = new DropDownList();
            Button btn = new Button();
            TableRow tbr = new TableRow();
            TableCell tbc1 = new TableCell();
            TableCell tbc2 = new TableCell();
            TableCell tbc3 = new TableCell();
            TableCell tbc4 = new TableCell();
            TableCell tbc5 = new TableCell();
            TableCell tbc6 = new TableCell();

            lbl.Text = Id;
            tb.ID = "txt_Resource" + Id;
            ddl1.ID = "ddl_Role" + Id;
            ddl2.ID = "ddl_Title" + Id;
            ddl3.ID = "ddl_WorkingHours" + Id;
            btn.ID = "btn_Remove" + Id;
            tbr.ID = "tbr_Content" + Id;
            btn.Text = " - ";

            tb.ControlStyle.CssClass = "form-control";
            ddl1.ControlStyle.CssClass = "dropdown";
            ddl2.ControlStyle.CssClass = "dropdown";
            ddl3.ControlStyle.CssClass = "dropdown";
            btn.ControlStyle.CssClass = "btn btn-success";

            tbc1.Controls.Add(lbl);
            tbc2.Controls.Add(tb);
            tbc3.Controls.Add(ddl1);
            tbc4.Controls.Add(ddl2);
            tbc5.Controls.Add(ddl3);
            tbc6.Controls.Add(btn);
            tbr.Cells.Add(tbc1);
            tbr.Cells.Add(tbc2);
            tbr.Cells.Add(tbc3);
            tbr.Cells.Add(tbc4);
            tbr.Cells.Add(tbc5);
            tbr.Cells.Add(tbc6);

            return new Tuple<TableRow, Button>(tbr, btn);
        }
    }
}