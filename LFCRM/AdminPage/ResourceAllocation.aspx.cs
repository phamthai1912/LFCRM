using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LFCRM.AdminPage
{
    public partial class ResourceAllocation : System.Web.UI.Page
    {
        List<string> controlIDList = new List<string>();
        int counter = 1;

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            controlIDList = (List<string>)ViewState["controlIDList"];
            foreach (string Id in controlIDList)
            {
                counter++;

                TextBox tb1 = new TextBox();
                TextBox tb2 = new TextBox();
                Label lbl = new Label();
                Button btn = new Button();

                tb1.ID = "txt_title" + Id;
                tb2.ID = "txt_expectedresouces" + Id;
                lbl.ID = "lbl_actualresources" + Id;
                btn.ID = "btn_remove" + Id;

                ph_title.Controls.Add(tb1);
                ph_expectedresouces.Controls.Add(tb2);
                ph_actualresouces.Controls.Add(lbl);
                ph_remove.Controls.Add(btn);
                btn.Text = " - ";

                btn.Click += new EventHandler(btnclick_event);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            counter++;

            TextBox tb1 = new TextBox();
            TextBox tb2 = new TextBox();
            Label lbl = new Label();
            Button btn = new Button();

            tb1.ID = "txt_title" + counter;
            tb2.ID = "txt_expectedresouces" + counter;
            lbl.ID = "lbl_actualresources" + counter;
            btn.ID = "btn_remove" + counter;

            //LiteralControl linebreak = new LiteralControl("<br />");
            ph_title.Controls.Add(tb1);
            ph_expectedresouces.Controls.Add(tb2);
            ph_actualresouces.Controls.Add(lbl);
            ph_remove.Controls.Add(btn);
            btn.Text = " - ";

            //btn.Click += new EventHandler(btnclick_event);
            controlIDList.Add(counter.ToString());
            ViewState["controlIDList"] = controlIDList;
        }

        protected void btnclick_event(object sender, EventArgs e)
        {
            TextBox tb1 = new TextBox();
            TextBox tb2 = new TextBox();
            Label lbl = new Label();
            Button btn = (Button)sender;

            string buttonId = btn.ID;

            tb1.ID = "txt_title" + buttonId.Substring(buttonId.Length - 1);
            tb2.ID = "txt_expectedresouces" + buttonId.Substring(buttonId.Length - 1);
            lbl.ID = "lbl_actualresources1" + buttonId.Substring(buttonId.Length - 1);

            Page.Controls.Remove(Page.FindControl(tb1.ID));
            Page.Controls.Remove(tb2);
            Page.Controls.Remove(lbl);
            Page.Controls.Remove(btn);
            lbl_actualresources1.Text = tb1.ID;

            tb1.Visible = false;

            tb1.Controls.Clear();
        }
    }
}