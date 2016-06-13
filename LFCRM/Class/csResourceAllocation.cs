using AjaxControlToolkit;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;

namespace LFCRM.Class
{
    public class csResourceAllocation : IHttpModule
    {
        Class.csDBConnect dbconnect = new Class.csDBConnect();
        Class.csCommonClass commonClass = new Class.csCommonClass();
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

        public Tuple<TableRow, Button, TextBox, TextBox> AddTitle(string Id)
        {
            TextBox tb1 = new TextBox();
            TextBox tb2 = new TextBox();
            Label lbl1 = new Label();
            Label lbl2 = new Label();
            Label lbl3 = new Label();
            Label lbl4 = new Label();
            Button btn = new Button();
            Button btn2 = new Button();
            
            TableRow tbr = new TableRow();
            TableCell tbc1 = new TableCell();
            TableCell tbc2 = new TableCell();
            TableCell tbc3 = new TableCell();
            TableCell tbc4 = new TableCell();
            TableCell tbc5 = new TableCell();
            AutoCompleteExtender autoCompleteExtender = new AjaxControlToolkit.AutoCompleteExtender();
            FilteredTextBoxExtender fteExpectedResouces = new FilteredTextBoxExtender();
            //RequiredFieldValidator rfvInputTitle = new RequiredFieldValidator();
            //RequiredFieldValidator rfvInputExpected = new RequiredFieldValidator();

            tb1.ID = "txt_Title" + Id;
            tb1.AutoPostBack = true;
            tb2.ID = "txt_ExpectedResouces" + Id;
            tb2.AutoPostBack = true;
            lbl1.ID = "lbl_TrainResources" + Id;
            lbl1.Text = "0";
            lbl2.ID = "lbl_ActualResources" + Id;
            lbl2.Font.Bold = true;
            lbl2.Text = "0";
            lbl3.ID = "lbl_TitleExist" + Id;
            lbl3.Text = "It does not exist";
            lbl3.Visible = false;
            lbl4.Text = " ";
            btn.ID = "btn_RemoveTitle" + Id;
            btn2.ID = "btn_AutoAllocation" + Id;
            tbr.ID = "tbr_ContentTitle" + Id;
            btn.Text = " - ";
            btn2.Text = "A";

            autoCompleteExtender.ID = "at_TitleExtender" + Id;
            autoCompleteExtender.TargetControlID = tb1.ID;
            autoCompleteExtender.ServiceMethod = "GetCompletionList3LD";
            autoCompleteExtender.ServicePath = "~/AutoComplete.asmx";
            autoCompleteExtender.CompletionInterval = 200;
            autoCompleteExtender.CompletionSetCount = 5;
            autoCompleteExtender.MinimumPrefixLength=1;

            fteExpectedResouces.ID = "fte_ExpectedResouces" + Id;
            fteExpectedResouces.TargetControlID = "txt_ExpectedResouces" + Id;
            fteExpectedResouces.FilterType = FilterTypes.Numbers | FilterTypes.Custom;
            fteExpectedResouces.ValidChars = ".";

            //rfvInputTitle.InitialValue = "";
            //rfvInputTitle.ID = "rfvInputTitle" + Id;
            //rfvInputTitle.Display = ValidatorDisplay.Dynamic;
            //rfvInputTitle.ValidationGroup = "RAValidation";
            //rfvInputTitle.ControlToValidate = "txt_Title" + Id;
            //rfvInputTitle.ErrorMessage = "Input a title";
            //rfvInputTitle.CssClass = "label label-danger";

            //rfvInputExpected.InitialValue = "";
            //rfvInputExpected.ID = "rfvInputExpected" + Id;
            //rfvInputExpected.Display = ValidatorDisplay.Dynamic;
            //rfvInputExpected.ValidationGroup = "RAValidation";
            //rfvInputExpected.ControlToValidate = "txt_ExpectedResouces" + Id;
            //rfvInputExpected.ErrorMessage = "Input a number";
            //rfvInputExpected.CssClass = "label label-danger";

            autoCompleteExtender.CompletionListCssClass = "form-control";
            tb1.ControlStyle.CssClass = "form-control";
            tb2.ControlStyle.CssClass = "form-control";
            lbl3.ControlStyle.CssClass = "label label-danger";
            btn.ControlStyle.CssClass = "btn btn-success btn-sm";
            btn2.ControlStyle.CssClass = "btn btn-info btn-sm";

            tbc1.Controls.Add(autoCompleteExtender);
            tbc1.Controls.Add(tb1);
            //tbc1.Controls.Add(rfvInputTitle);
            tbc1.Controls.Add(lbl3);
            tbc2.Controls.Add(tb2);
            //tbc2.Controls.Add(rfvInputExpected);
            tbc2.Controls.Add(fteExpectedResouces);
            tbc3.Controls.Add(lbl2);
            tbc4.Controls.Add(lbl1);
            //tbc5.Controls.Add(btn2);
            tbc5.Controls.Add(lbl4);
            tbc5.Controls.Add(btn);
            tbr.Cells.Add(tbc1);
            tbr.Cells.Add(tbc2);
            tbr.Cells.Add(tbc3);
            tbr.Cells.Add(tbc4);
            tbr.Cells.Add(tbc5);

            return new Tuple<TableRow, Button, TextBox, TextBox>(tbr, btn, tb1, tb2);
        }

        public Tuple<TableRow, Button, DropDownList, DropDownList, TextBox, DropDownList> AddResource(string Id)
        {
            Label lbl = new Label();
            Label lbl2 = new Label();
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
            AutoCompleteExtender autoCompleteExtender = new AjaxControlToolkit.AutoCompleteExtender();
            //RequiredFieldValidator rfvSelectTitle = new RequiredFieldValidator();
            //RequiredFieldValidator rfvResourceName = new RequiredFieldValidator();

            lbl.ID = "lbl_ResourceID" + Id;
            lbl.Text="N/A";
            tb.ID = "txt_Resource" + Id;
            tb.AutoPostBack = true;
            lbl2.ID = "lbl_ResourceExist" + Id;
            lbl2.Text = "It does not exist";
            lbl2.Visible = false;
            ddl1.ID = "ddl_Role" + Id;
            ddl1.AutoPostBack = true;
            ddl2.ID = "ddl_Title" + Id;
            ddl2.AutoPostBack = true;
            ddl3.ID = "ddl_WorkingHours" + Id;
            ddl3.AutoPostBack = true;
            btn.ID = "btn_RemoveResource" + Id;
            tbc4.ID = "tbc_TitleResource" + Id;
            tbr.ID = "tbr_ContentResource" + Id;
            btn.Text = " - ";

            autoCompleteExtender.ID = "at_ResourceExtender" + Id;
            autoCompleteExtender.TargetControlID = tb.ID;
            autoCompleteExtender.ServiceMethod = "GetCompletionListResource";
            autoCompleteExtender.ServicePath = "~/AutoComplete.asmx";
            autoCompleteExtender.CompletionInterval = 200;
            autoCompleteExtender.CompletionSetCount = 5;
            autoCompleteExtender.MinimumPrefixLength = 1;

            //rfvSelectTitle.InitialValue = "- Select Item -";
            //rfvSelectTitle.ID = "rfvSelectTitle"+Id;
            //rfvSelectTitle.Display = ValidatorDisplay.Dynamic;
            //rfvSelectTitle.ValidationGroup = "RAValidation";
            //rfvSelectTitle.ControlToValidate = "ddl_Title" + Id;
            //rfvSelectTitle.ErrorMessage = "Select a title";
            //rfvSelectTitle.CssClass = "label label-danger";

            //rfvResourceName.InitialValue = "";
            //rfvResourceName.ID = "rfvInputName" + Id;
            //rfvResourceName.Display = ValidatorDisplay.Dynamic;
            //rfvResourceName.ValidationGroup = "RAValidation";
            //rfvResourceName.ControlToValidate = "txt_Resource" + Id;
            //rfvResourceName.ErrorMessage = "Input a name";
            //rfvResourceName.CssClass = "label label-danger";

            ddl1 = commonClass.AddDBToDDL(ddl1, "SELECT ProjectRoleID, ProjectRoleName FROM tbl_ProjectRole");
            ddl3 = commonClass.AddDBToDDL(ddl3, "SELECT WorkingHoursID, Value FROM tbl_WorkingHours");

            autoCompleteExtender.CompletionListCssClass = "form-control";
            tb.ControlStyle.CssClass = "form-control";
            lbl2.ControlStyle.CssClass = "label label-danger";
            ddl1.ControlStyle.CssClass = "form-control";
            ddl2.ControlStyle.CssClass = "form-control";
            ddl3.ControlStyle.CssClass = "form-control";
            btn.ControlStyle.CssClass = "btn btn-success btn-sm";

            tbc1.Controls.Add(lbl);
            tbc1.Controls.Add(autoCompleteExtender);
            tbc2.Controls.Add(tb);
            //tbc2.Controls.Add(rfvResourceName);
            tbc2.Controls.Add(lbl2);
            tbc3.Controls.Add(ddl1);
            tbc4.Controls.Add(ddl2);
            //tbc4.Controls.Add(rfvSelectTitle);
            tbc5.Controls.Add(ddl3);
            tbc6.Controls.Add(btn);
            tbr.Cells.Add(tbc1);
            tbr.Cells.Add(tbc2);
            tbr.Cells.Add(tbc3);
            tbr.Cells.Add(tbc4);
            tbr.Cells.Add(tbc5);
            tbr.Cells.Add(tbc6);

            return new Tuple<TableRow, Button, DropDownList, DropDownList, TextBox, DropDownList>(tbr, btn, ddl2, ddl3, tb, ddl1);
        }

        public Boolean FullNameExist(string fullname)
        {
            string sql = "SELECT * FROM tbl_User WHERE FullName = N'" + fullname + "'";
            DataTable tbcolor = dbconnect.getDataTable(sql);

            if (tbcolor.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public Boolean CheckRADateExist(string date)
        {
            string sql = "SELECT * FROM tbl_ResourceAllocation WHERE Date = '" + date + "'";
            DataTable tbcolor = dbconnect.getDataTable(sql);

            if (tbcolor.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public string getEmployeeIDbyName(string fullname)
        {
            string UserID = "";
            string sql = "SELECT EmployeeID FROM tbl_User WHERE FullName = N'" + fullname + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) UserID = tb.Rows[0][0].ToString();
            return UserID;
        }

        public string getIDbyEmployeeID(string eID)
        {
            string str = "";
            string sql = "SELECT UserID FROM tbl_User WHERE EmployeeID = N'" + eID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }

        public string getEmployeeIDbyID(string userID)
        {
            string str = "";
            string sql = "SELECT EmployeeID FROM tbl_User WHERE UserID = N'" + userID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }

        public string get3LDbyID(string titleID)
        {
            string str = "";
            string sql = "SELECT [3LD] FROM tbl_title WHERE titleID = N'" + titleID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }
      
        public string getTitleIDby3LD(string _LD)
        {
            string str = "";
            string sql = "SELECT TitleID FROM tbl_title WHERE [3LD] = N'" + _LD + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }

        public string getFullName(string eID)
        {
            string fullname = "";
            string sql = "SELECT FullName FROM tbl_User WHERE EmployeeID = '" + eID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) fullname = tb.Rows[0][0].ToString();
            return fullname;
        }

        public string getProjectRoleName(string ID)
        {
            string str = "";
            string sql = "SELECT ProjectRoleName FROM tbl_ProjectRole WHERE ProjectRoleID = '" + ID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }

        public string getProjectAbbreviation(string Projectrole)
        {
            string str = "";
            string sql = "SELECT Abbreviation FROM tbl_ProjectRole WHERE ProjectRoleName = '" + Projectrole + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }

        public string getValueHours(string ID)
        {
            string str = "";
            string sql = "SELECT Value FROM tbl_WorkingHours WHERE WorkingHoursID = '" + ID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }

        public string getColorCode(string _LD)
        {
            string str = "";
            string sql = "SELECT ColorCode FROM tbl_Title WHERE [3LD] = '" + _LD + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) str = tb.Rows[0][0].ToString();
            return str;
        }

        public string getBirthDay(string eID)
        {
            string date = "";
            string sql = "SELECT Birthday FROM tbl_User WHERE EmployeeID = '" + eID + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0) date = tb.Rows[0][0].ToString();
            return date;
        }

        //public string getProjectRoleID(string ProjectRoleName)
        //{
        //    string ProjectRoleID = "";
        //    string sql = "SELECT ProjectRoleID FROM tbl_ProjectRole WHERE ProjectRoleName = '" + ProjectRoleName + "'";
        //    DataTable tb = dbconnect.getDataTable(sql);
        //    if (tb.Rows.Count != 0) ProjectRoleID = tb.Rows[0][0].ToString();
        //    return ProjectRoleID;
        //}

        public void addResourceAllocation(string date, string UserID, string ProjectRoleID, string TitleID, string WorkingHoursID)
        {
            string sql = "";
            if (get3LDbyID(TitleID) != "")
                sql = "INSERT INTO tbl_ResourceAllocation (Date,UserID,ProjectRoleID,TitleID,WorkingHoursID) " +
                        "VALUES ('" + date + "','" + UserID + "','" + ProjectRoleID + "','" + TitleID + "','" + WorkingHoursID + "')";
            else
                sql = "INSERT INTO tbl_ResourceAllocation (Date,UserID,ProjectRoleID,TitleID,WorkingHoursID) " +
                       "VALUES ('" + date + "','" + UserID + "','" + ProjectRoleID + "', null,'" + WorkingHoursID + "')";
            

            dbconnect.ExeCuteNonQuery(sql);
        }

        public void deleteResourceAllocationByDate(string date)
        {
            String sql = "DELETE FROM tbl_ResourceAllocation WHERE Date = '" + date + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public Boolean CheckTADateExist(string date)
        {
            string sql = "SELECT * FROM tbl_TitleAllocation WHERE Date = '" + date + "'";
            DataTable tbcolor = dbconnect.getDataTable(sql);

            if (tbcolor.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public void deleteTitleAllocationByDate(string date)
        {
            String sql = "DELETE FROM tbl_TitleAllocation WHERE Date = '" + date + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        public void addTitleAllocation(string date, string TitleID, string ExpectedResourceQuantity, string ActualResourceQuantity, string TrainResourceQuantity)
        {
            string sql = "INSERT INTO tbl_TitleAllocation (Date,TitleID,ExpectedResourceQuantity,ActualResourceQuantity,TrainResourceQuantity) " +
                        "VALUES ('" + date + "','" + TitleID + "','" + ExpectedResourceQuantity + "','" + ActualResourceQuantity + "','" + TrainResourceQuantity + "')";

            dbconnect.ExeCuteNonQuery(sql);
        }

        public DataTable getHintbyTitle(string _LD)
        {
            DataTable dt = dbconnect.getDataTable("Select COUNT(tbl_ResourceAllocation.UserID) AS Numbers, Fullname, ProjectRoleName "
                                                    + "From tbl_ResourceAllocation, tbl_user, tbl_ProjectRole, tbl_title "
                                                    + "Where [3LD] = '" + _LD + "' "
                                                    + "And tbl_user.UserID = tbl_ResourceAllocation.UserID "
                                                    + "AND tbl_ProjectRole.ProjectRoleID = tbl_ResourceAllocation.ProjectRoleID "
                                                    + "And tbl_title.TitleID = tbl_ResourceAllocation.TitleID "
                                                    + "group by projectrolename, Fullname "
                                                    + "Order by Numbers Desc");
            return dt;
        }
    }
}