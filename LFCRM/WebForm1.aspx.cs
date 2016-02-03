using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace LFCRM
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.dataGridView1.Rows.Add("Abc", 5);
            //this.dataGridView1.Rows.Add("Def", 8);
            //this.dataGridView1.Rows.Add("Ghi", 3);
            //this.dataGridView1.Sort(this.dataGridView1.Columns[1],
            //                        ListSortDirection.Ascending);

            Label1.Text = ddl1.Enabled.ToString();
        }


    }
}