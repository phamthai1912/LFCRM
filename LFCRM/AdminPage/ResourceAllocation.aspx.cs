using LFCRM.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using AjaxControlToolkit;

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
                {
                    for (int i = 1; i <= (int)ViewState["TitleCounter"]; i++)
                    {
                        Button btn = new Button();
                        var tuple = RA.AddTitle(i.ToString());

                        btn = tuple.Item2;
                        btn.Click += new EventHandler(btn_MinusTitleClick_event);
                        ph_DynamicTitleTableRow.Controls.Add(tuple.Item1);
                    }
                }

                //------------------------Resource------------------------------//
                if ((int)ViewState["ResourceCounter"] != 0)
                {
                    for (int i = 1; i <= (int)ViewState["ResourceCounter"]; i++)
                    {
                        Button btn = new Button();
                        var tuple = RA.AddResource(i.ToString());

                        btn = tuple.Item2;
                        btn.Click += new EventHandler(btn_MinusResourceClick_event);
                        ph_DynamicResourceTableRow.Controls.Add(tuple.Item1);
                    }
                }
            }
        }

        //protected override void LoadViewState(object savedState)
        //{
        //    base.LoadViewState(savedState);

        //    //------------------------Title----------------------------------//
        //    if ((int)ViewState["l"] !=0)
        //    {
        //        ControlTitleIDList = (List<string>)ViewState["ControlTitleIDList"];
        //        foreach (string Id in ControlTitleIDList)
        //        {
        //            TitleCounter++;
        //            Button btn = new Button();
        //            var tuple = RA.AddTitle(TitleCounter.ToString());

        //            btn = tuple.Item2;
        //            btn.Click += new EventHandler(btn_MinusTitleClick_event);
        //            ph_DynamicTitleTableRow.Controls.Add(tuple.Item1);
        //        }
        //    }

        //    //------------------------Resource--------------------------------//
        //    if ((int)ViewState["m"] != 0)
        //    {
        //        ControlResourceIDList = (List<string>)ViewState["ControlResourceIDList"];
        //        foreach (string Id in ControlResourceIDList)
        //        {
        //            ResourceCounter++;
        //            Button btn = new Button();
        //            var tuple = RA.AddResource(ResourceCounter.ToString());

        //            btn = tuple.Item2;
        //            btn.Click += new EventHandler(btn_MinusResourceClick_event);
        //            ph_DynamicResourceTableRow.Controls.Add(tuple.Item1);
        //        }
        //    }
        //}

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void btn_AddTitle_Click(object sender, EventArgs e)
        {
            ViewState["TitleCounter"] = (int)ViewState["TitleCounter"] + 1;
            ViewState["Switcher"] = 1;

            Button btn = new Button();
            var tuple = RA.AddTitle(ViewState["TitleCounter"].ToString());

            btn = tuple.Item2;
            btn.Click += new EventHandler(btn_MinusTitleClick_event);
            ph_DynamicTitleTableRow.Controls.Add(tuple.Item1);
        }

        protected void btn_AddResource_Click(object sender, EventArgs e)
        {
            ViewState["ResourceCounter"] = (int)ViewState["ResourceCounter"] + 1;
            ViewState["Switcher"] = 2;

            Button btn = new Button();
            var tuple = RA.AddResource(ViewState["ResourceCounter"].ToString());

            btn = tuple.Item2;
            btn.Click += new EventHandler(btn_MinusResourceClick_event);
            ph_DynamicResourceTableRow.Controls.Add(tuple.Item1);
        }

        protected void btn_MinusTitleClick_event(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string buttonId = btn.ID;

            ph_DynamicTitleTableRow.FindControl("tbr_ContentTitle" + Regex.Match(buttonId, @"\d+").Value).Visible = false;
        }

        protected void btn_MinusResourceClick_event(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string buttonId = btn.ID;

            ph_DynamicResourceTableRow.FindControl("tbr_ContentResource" + Regex.Match(buttonId, @"\d+").Value).Visible = false;
        }
    }
}