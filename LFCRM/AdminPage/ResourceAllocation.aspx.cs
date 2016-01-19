using LFCRM.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace LFCRM.AdminPage
{
    public partial class ResourceAllocation : System.Web.UI.Page
    {
        List<string> ControlTitleIDList = new List<string>();
        List<string> ControlResourceIDList = new List<string>();
        int TitleCounter = 0;
        int ResourceCounter = 0;
        csResourceAllocation RA = new csResourceAllocation();
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            //------------------------Title----------------------------------//
            //if (TitleCounter != 0)
            //{
            //    ControlTitleIDList = (List<string>)ViewState["ControlTitleIDList"];
            //    foreach (string Id in ControlTitleIDList)
            //    {
            //        TitleCounter++;
            //        Button btn = new Button();
            //        var tuple = RA.AddTitle(TitleCounter.ToString());

            //        btn = tuple.Item2;
            //        btn.Click += new EventHandler(btn_MinusTitleClick_event);
            //        ph_DynamicTitleTableRow.Controls.Add(tuple.Item1);
            //    }
            //}

            //------------------------Resource--------------------------------//
            //if (ResourceCounter != 0)
            //{
                ControlResourceIDList = (List<string>)ViewState["ControlResourceIDList"];
                foreach (string Id in ControlResourceIDList)
                {
                    ResourceCounter++;
                    Button btn = new Button();
                    var tuple = RA.AddResource(ResourceCounter.ToString());

                    btn = tuple.Item2;
                    btn.Click += new EventHandler(btn_MinusResourceClick_event);
                    ph_DynamicResourceTableRow.Controls.Add(tuple.Item1);
                }
            //}
        }

        protected void btn_AddTitle_Click(object sender, EventArgs e)
        {
            TitleCounter++;
            Button btn = new Button();
            var tuple = RA.AddTitle(TitleCounter.ToString());

            btn = tuple.Item2;
            btn.Click += new EventHandler(btn_MinusTitleClick_event);
            ph_DynamicTitleTableRow.Controls.Add(tuple.Item1);

            ControlTitleIDList.Add(TitleCounter.ToString());
            ViewState["ControlTitleIDList"] = ControlTitleIDList;
        }

        protected void btn_MinusTitleClick_event(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string buttonId = btn.ID;

            ph_DynamicTitleTableRow.FindControl("tbr_Content" + Regex.Match(buttonId, @"\d+").Value).Visible = false;
        }

        protected void btn_AddResource_Click(object sender, EventArgs e)
        {
            ResourceCounter++;
            Button btn = new Button();
            var tuple = RA.AddResource(ResourceCounter.ToString());

            btn = tuple.Item2;
            btn.Click += new EventHandler(btn_MinusResourceClick_event);
            ph_DynamicResourceTableRow.Controls.Add(tuple.Item1);

            ControlResourceIDList.Add(ResourceCounter.ToString());
            ViewState["ControlResourceIDList"] = ControlResourceIDList;
        }

        protected void btn_MinusResourceClick_event(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string buttonId = btn.ID;

            ph_DynamicResourceTableRow.FindControl("tbr_Content" + Regex.Match(buttonId, @"\d+").Value).Visible = false;
        }
    }
}