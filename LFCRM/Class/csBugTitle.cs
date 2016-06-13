using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LFCRM.Class
{
    public class csBugTitle
    {
        csDBConnect dbconnect = new csDBConnect();
        public string Get3LDTitle(string employeeid)
        {
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            string sql = "SELECT [3LD] " +
                        "FROM tbl_ResourceAllocation,tbl_User,tbl_Title " +
                        "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID " +
                        "AND EmployeeID = '" + employeeid + "'  " +
                        "AND Date = '" + date + "'";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public DataSet GetListBugTitle(string _3ld)
        {
            string sql = "SELECT BugIDTemporary,BugStatusName,BugTitleID,Summary,BuildNumber,BugSeverity,UserEnterID,CONVERT(VARCHAR(10), DateEnter, 101) As DateEnter,UserUpdateID,Link " +
                    "FROM tbl_BugTitle,tbl_BugStatus,tbl_BugSeverity,tbl_BuildTitle,tbl_Title "+
                    "WHERE tbl_BugTitle.BugStatusID = tbl_BugStatus.BugStatusID "+
                    "AND tbl_BugTitle.BugSeverityID = tbl_BugSeverity.BugSeverityID " +
                    "AND tbl_BugTitle.TitleID = tbl_Title.TitleID "+
                    "AND tbl_BugTitle.BuildTitleID = tbl_BuildTitle.BuildTitleID "+
                    "AND [3LD] = '" + _3ld + "' " +
                    "ORDER BY BugTitleID DESC";
            DataSet ds = dbconnect.getDataSet(sql);
            if (ds != null)
                return ds;
            else return null;
        }

        public DataSet GetListBugTitleByDate(string _3ld, string date)
        {
            string sql = "SELECT BugIDTemporary,BugStatusName,BugTitleID,Summary,BuildNumber,BugSeverity,UserEnterID,CONVERT(VARCHAR(10), DateEnter, 101) As DateEnter,UserUpdateID,Link " +
                               "FROM tbl_BugTitle,tbl_BugStatus,tbl_BugSeverity,tbl_BuildTitle,tbl_Title " +
                               "WHERE tbl_BugTitle.BugStatusID = tbl_BugStatus.BugStatusID " +
                               "AND tbl_BugTitle.BugSeverityID = tbl_BugSeverity.BugSeverityID " +
                               "AND tbl_BugTitle.TitleID = tbl_Title.TitleID " +
                               "AND tbl_BugTitle.BuildTitleID = tbl_BuildTitle.BuildTitleID " +
                               "AND [3LD] = '" + _3ld + "' " +
                               "AND DateEnter = '" + date + "' " +
                               "ORDER BY BugTitleID DESC";
            DataSet ds = dbconnect.getDataSet(sql);
            if (ds != null)
                return ds;
            else return null;
        }

        public string GetTotalSeverityBugByDate(string _3ld, string date, string severity)
        {
            string sql = "";
            switch (severity)
            {
                case "S1":
                    sql = "SELECT COUNT(BugTitleID) FROM tbl_BugTitle,tbl_Title WHERE tbl_BugTitle.TitleID = tbl_Title.TitleID AND [3LD] = '" + _3ld + "' AND DateEnter = '" + date + "' AND BugSeverityID = '1'";
                    break;
                case "S2":
                    sql = "SELECT COUNT(BugTitleID) FROM tbl_BugTitle,tbl_Title WHERE tbl_BugTitle.TitleID = tbl_Title.TitleID AND [3LD] = '" + _3ld + "' AND DateEnter = '" + date + "' AND BugSeverityID = '2'";
                    break;
                case "S3":
                    sql = "SELECT COUNT(BugTitleID) FROM tbl_BugTitle,tbl_Title WHERE tbl_BugTitle.TitleID = tbl_Title.TitleID AND [3LD] = '" + _3ld + "' AND DateEnter = '" + date + "' AND BugSeverityID = '9'";
                    break;
                case "S4":
                    sql = "SELECT COUNT(BugTitleID) FROM tbl_BugTitle,tbl_Title WHERE tbl_BugTitle.TitleID = tbl_Title.TitleID AND [3LD] = '" + _3ld + "' AND DateEnter = '" + date + "' AND BugSeverityID = '10'";
                    break;
                case "S5":
                    sql = "SELECT COUNT(BugTitleID) FROM tbl_BugTitle,tbl_Title WHERE tbl_BugTitle.TitleID = tbl_Title.TitleID AND [3LD] = '" + _3ld + "' AND DateEnter = '" + date + "' AND BugSeverityID = '11'";
                    break;
            }

            DataTable dt = dbconnect.getDataTable(sql);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else return "0";

        }

        public DataSet GetListBugTitleBySeverity(string _3ld, string severity, string search, Boolean bugtoday)
        {
            string searchstring = GetSearchString(search);
            string _bugtoday = "";
            if (bugtoday)
                _bugtoday = "AND (DateEnter = '" + DateTime.Now.ToString("MM/dd/yyyy") + "' OR BugStatusName = 'Pending') ";
            string sql = "Select BugIDTemporary,BugStatusName,BugTitleID,Summary,BuildNumber,BugSeverity,UserEnterID,CONVERT(VARCHAR(10), DateEnter, 101) As DateEnter,UserUpdateID " +
                    "FROM tbl_BugTitle,tbl_BugStatus,tbl_BuildTitle,tbl_BugSeverity,tbl_BugReproduce,tbl_Title " +
                    "WHERE tbl_BugTitle.BugStatusID = tbl_BugStatus.BugStatusID " +
                    "AND tbl_BugTitle.BuildTitleID = tbl_BuildTitle.BuildTitleID " +
                    "AND tbl_BugTitle.BugSeverityID = tbl_BugSeverity.BugSeverityID " +
                    "AND tbl_BugTitle.BugReproduceID = tbl_BugReproduce.BugReproduceID " +
                    "AND tbl_BugTitle.TitleID = tbl_Title.TitleID " + _bugtoday +
                    "AND [3LD] = '" + _3ld + "' " +
                    "AND (Summary LIKE '" + searchstring + "%' " +
                    "OR Description LIKE '" + searchstring + "%' " +
                    "OR Steps LIKE '" + searchstring + "%') " +
                    "AND BugSeverity = '" + severity + "' " +
                    "ORDER BY BugTitleID DESC";
            DataSet ds = dbconnect.getDataSet(sql);
            if (ds != null)
                return ds;
            return null;
        }

        public DataSet GetListBugTitleByUserEnter(string _3ld, string userenterid, string search, Boolean bugtoday)
        {
            string searchstring = GetSearchString(search);
            string _bugtoday = "";
            if (bugtoday)
                _bugtoday = "AND (DateEnter = '" + DateTime.Now.ToString("MM/dd/yyyy") + "' OR BugStatusName = 'Pending') ";
            string sql = "Select BugIDTemporary,BugStatusName,BugTitleID,Summary,BuildNumber,BugSeverity,UserEnterID,CONVERT(VARCHAR(10), DateEnter, 101) As DateEnter,UserUpdateID " +
                    "FROM tbl_BugTitle,tbl_BugStatus,tbl_BuildTitle,tbl_BugSeverity,tbl_BugReproduce,tbl_Title,tbl_User " +
                    "WHERE tbl_BugTitle.BugStatusID = tbl_BugStatus.BugStatusID " +
                    "AND tbl_BugTitle.BuildTitleID = tbl_BuildTitle.BuildTitleID " +
                    "AND tbl_BugTitle.BugSeverityID = tbl_BugSeverity.BugSeverityID " +
                    "AND tbl_BugTitle.BugReproduceID = tbl_BugReproduce.BugReproduceID " +
                    "AND tbl_BugTitle.TitleID = tbl_Title.TitleID " +
                    "AND tbl_BugTitle.UserEnterID = tbl_User.UserID " + _bugtoday +
                    "AND [3LD] = '" + _3ld + "' " +
                    "AND (Summary LIKE '" + searchstring + "%' " +
                    "OR Description LIKE '" + searchstring + "%' " +
                    "OR Steps LIKE '" + searchstring + "%') " +
                    "AND UserID = '" + userenterid + "' " +
                    "ORDER BY BugTitleID DESC";
            DataSet ds = dbconnect.getDataSet(sql);
            if (ds != null)
                return ds;
            return null;
        }

        public void UpdateBug(string bugid, string _3ld, string summary, string description, string steps, string severity, string build, string reproduce, string dateenter, string employeeupdate)
        {
            string userupdateid = GetUserID(employeeupdate);
            string severityid = GetSeverityID(severity);
            string buildtitleid = GetTitleBuildID(build, _3ld);
            string reproduceid = GetReproduceID(reproduce);
            string dateupdate = DateTime.Now.ToString("MM/dd/yyyy");
            string sql = "UPDATE tbl_BugTitle "+
                        "SET Summary = N'" + summary.Replace("'", "''") + "', Description = N'" + description.Replace("'", "''") + "', Steps = N'" + steps.Replace("'", "''") + "', BuildTitleID = '" + buildtitleid + "', BugSeverityID = '" + severityid + "', " +
                        "BugReproduceID = '1', DateEnter = '" + dateenter + "', DateUpdate = '" + dateupdate + "', UserUsing = '', UserUpdateID = '" + userupdateid + "', BugStatusID = '6' " +
                        "WHERE BugTitleID = '" + bugid + "'";

            dbconnect.ExeCuteNonQuery(sql);
        }

        public string GetUserName(string userid)
        {
            string sql = "SELECT FullName FROM tbl_User WHERE UserID = '" + userid + "'";
            DataTable dt = dbconnect.getDataTable(sql);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else return "";
        }

        public string GetFullName(string employeeid)
        {
            string sql = "SELECT FullName FROM tbl_User WHERE EmployeeID = '" + employeeid + "'";
            DataTable dt = dbconnect.getDataTable(sql);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else return "";
        }

        public string GetUserRole(string _3ld, string employeeid)
        {
            string sql = "SELECT ProjectRoleName " +
                        "FROM tbl_ResourceAllocation,tbl_ProjectRole,tbl_Title,tbl_User " +
                        "WHERE tbl_ResourceAllocation.ProjectRoleID = tbl_ProjectRole.ProjectRoleID " +
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID " +
                        "AND tbl_ResourceAllocation.UserID = tbl_User.UserID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND EmployeeID = '" + employeeid + "' " +
                        "AND ProjectRoleName = 'Core' " +
                        "GROUP BY ProjectRoleName";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetUserApprovalRole(string _3ld, string employeeid)
        {
            string sql = "SELECT ProjectRoleName "+
                    "FROM tbl_BugApproval,tbl_ProjectRole,tbl_Title,tbl_User "+
                    "WHERE tbl_BugApproval.UserID = tbl_User.UserID "+
                    "AND tbl_BugApproval.ProjectRoleID = tbl_ProjectRole.ProjectRoleID "+
                    "AND tbl_BugApproval.TitleID = tbl_Title.TitleID "+
                    "AND EmployeeID = '" + employeeid + "' " +
                    "AND [3LD] = '" + _3ld + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetUserUpdating(string bugid)
        {
            string sql = "SELECT UserUsing FROM tbl_BugTitle WHERE BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            } else return "";
        }
        
        //Get Severity
        public string GetSeverity(string bugid)
        {
            string sql = "SELECT BugSeverity FROM tbl_BugTitle,tbl_BugSeverity WHERE tbl_BugTitle.BugSeverityID = tbl_BugSeverity.BugSeverityID AND BugTitleID = '" + bugid + "'";
            DataTable dt = dbconnect.getDataTable(sql);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else return "";
        }

        public string GetSeverityID(string severity)
        {
            string sql = "SELECT BugSeverityID FROM tbl_BugSeverity WHERE BugSeverity = '" + severity + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetTitleID(string _3ld)
        {
            string sql = "SELECT TitleID FROM tbl_Title WHERE [3LD] = '" + _3ld + "'";
            DataTable dt = dbconnect.getDataTable(sql);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else return "";
        }

        //Get Build
        public DataTable GetTitleBuild(string _3ld)
        {
            string titleid = GetTitleID(_3ld);
            string sql = "SELECT BuildTitleID,BuildNumber " +
                        "FROM tbl_BuildTitle,tbl_Title "+
                        "WHERE tbl_BuildTitle.TitleID = tbl_Title.TitleID "+
                        "AND [3LD] = '" + _3ld + "' " +
                        "ORDER BY BuildNumber DESC";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
                return tb;
            else return null;
        }

        public string GetBuildNumber(string bugid)
        {
            string sql = "SELECT BuildNumber FROM tbl_BugTitle, tbl_BuildTitle WHERE tbl_BugTitle.BuildTitleID = tbl_BuildTitle.BuildTitleID AND BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetTitleBuildID(string titlebuild, string _3ld)
        {
            string sql = "SELECT BuildTitleID FROM tbl_BuildTitle,tbl_Title WHERE tbl_BuildTitle.TitleID = tbl_Title.TitleID AND [3LD] = '" + _3ld + "' AND BuildNumber = '" + titlebuild + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetDescription(string bugid)
        {
            string sql = "SELECT Description FROM tbl_BugTitle WHERE BugTitleID = '" + bugid + "'";
            DataTable dt = dbconnect.getDataTable(sql);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else return "";
        }

        public string GetSteps(string bugid)
        {
            string sql = "SELECT Steps FROM tbl_BugTitle WHERE BugTitleID = '" + bugid + "'";
            DataTable dt = dbconnect.getDataTable(sql);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else return "";
        }

        public string GetUserID(string employeeid)
        {
            string sql = "SELECT UserID FROM tbl_User WHERE EmployeeID = '"+employeeid+"'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        //Get Reproduce
        public string GetReproduce(string bugid)
        {
            string sql = "SELECT BugReproduce FROM tbl_BugReproduce,tbl_BugTitle WHERE tbl_BugTitle.BugReproduceID = tbl_BugReproduce.BugReproduceID AND BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        public string GetReproduceID(string reproduce)
        {
            string sql = "SELECT BugReproduceID FROM tbl_BugReproduce WHERE BugReproduce = '" + reproduce + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        //Approve
        public void ApproveBug(string bugid)
        {
            string sql = "UPDATE tbl_BugTitle SET UserUsing = '', BugStatusID = '1' WHERE BugTitleID = '" + bugid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        //UnApprove
        public void UnApproveBug(string bugid)
        {
            string sql = "UPDATE tbl_BugTitle SET UserUsing = '', BugStatusID = '6' WHERE BugTitleID = '" + bugid + "'";
            if (CheckPendingBug(bugid))
                sql = "UPDATE tbl_BugTitle SET UserUsing = '', BugStatusID = '6', DateEnter = '" + DateTime.Now.ToString("MM/dd/yyyy") + "' WHERE BugTitleID = '" + bugid + "'";
            
            dbconnect.ExeCuteNonQuery(sql);
        }

        //Pending
        public void PendingBug(string bugid)
        {
            string sql = "UPDATE tbl_BugTitle SET BugStatusID = '2' WHERE BugTitleID = '" + bugid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        //Not A Bug
        public void NotABug(string bugid)
        {
            string sql = "UPDATE tbl_BugTitle SET BugStatusID = '3' WHERE BugTitleID = '" + bugid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        //Logged Bug
        public void LoggedBug(string bugid)
        {
            string sql = "UPDATE tbl_BugTitle SET BugStatusID = '7' WHERE BugTitleID = '" + bugid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        //Remove Bug
        public void RemoveBug(string bugid)
        {
            string sql = "DELETE FROM tbl_BugTitle WHERE BugTitleID = '" + bugid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        //Watching Bug
        public void Watching(string bugid, string fullname)
        {
            string sql = "UPDATE tbl_BugTitle SET UserUsing = '" + fullname + "' WHERE BugTitleID = '" + bugid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        //Get Status
        public string GetBugStatus(string bugid)
        {
            string sql = "SELECT BugStatusName FROM tbl_BugTitle,tbl_BugStatus WHERE tbl_BugTitle.BugStatusID = tbl_BugStatus.BugStatusID AND BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        //Add A New Bug
        public void AddANewBug(string title, string employeeid, string summary, string severity, string build, string reproduce, string enterdate, string description, string steps)
        {
            string titleid = GetTitleID(title);
            string userid = GetUserID(employeeid);
            string severityid = GetSeverityID(severity);
            string buildid = GetTitleBuildID(build, title);
            string reproduceid = GetReproduceID(reproduce);
            string bugidtemp = (Convert.ToInt32(CountTotalBug(title).ToString()) + 1).ToString();
            string sql = "INSERT INTO tbl_BugTitle (BugIDTemporary, TitleID, UserEnterID, Summary, Description, Steps, BuildTitleID, BugSeverityID, BugReproduceID, DateEnter, BugStatusID) " +
                        "VALUES ('" + bugidtemp + "', '" + titleid + "', '" + userid + "', N'" + summary.Replace("'", "''") + "', N'" + description.Replace("'", "''") + "', N'" + steps.Replace("'", "''") + "', '" + buildid + "', '" + severityid + "', '" + reproduceid + "', '" + enterdate + "', '6')";
            
            dbconnect.ExeCuteNonQuery(sql);
        }

        //Split Search String
        public string GetSearchString(string searchstring)
        {
            String[] temp;
            temp = searchstring.Split(' ');
            string searchstring1 = "";
            for (int i = 0; i < temp.Length; i++)
            {
                searchstring1 = searchstring1 + "%" + temp[i];
            }

            return searchstring1;
        }

        //Search Bug Title
        public DataSet SearchBugTitle(string searchstring, string _3ld, Boolean bugtoday)
        {
            string search = GetSearchString(searchstring);
            string _bugtoday = "";
            if (bugtoday)
                _bugtoday = "AND (DateEnter = '" + DateTime.Now.ToString("MM/dd/yyyy") + "' OR BugStatusName = 'Pending') ";
            string sql = "Select BugIDTemporary,BugStatusName,BugTitleID,Summary,BuildNumber,BugSeverity,UserEnterID,CONVERT(VARCHAR(10), DateEnter, 101) As DateEnter,UserUpdateID " +
                    "FROM tbl_BugTitle,tbl_BugStatus,tbl_BuildTitle,tbl_BugSeverity,tbl_BugReproduce,tbl_Title " +
                    "WHERE tbl_BugTitle.BugStatusID = tbl_BugStatus.BugStatusID "+
                    "AND tbl_BugTitle.BuildTitleID = tbl_BuildTitle.BuildTitleID "+
                    "AND tbl_BugTitle.BugSeverityID = tbl_BugSeverity.BugSeverityID "+
                    "AND tbl_BugTitle.BugReproduceID = tbl_BugReproduce.BugReproduceID "+
                    "AND tbl_BugTitle.TitleID = tbl_Title.TitleID "+ _bugtoday +
                    "AND [3LD] = '" + _3ld + "' " +
                    "AND (Summary LIKE '" + search.Replace("'","''") + "%' " +
                    "OR BugIDTemporary LIKE '" + search.Replace("'", "''") + "%' " +
                    "OR Description LIKE '" + search.Replace("'", "''") + "%' " +
                    "OR Link LIKE '" + search.Replace("'", "''") + "%' " +
                    "OR Steps LIKE '" + search.Replace("'", "''") + "%') " +
                    "ORDER BY BugTitleID DESC";
            DataSet ds = dbconnect.getDataSet(sql);
            if (ds != null)
                return ds;
            return null;
        }

        //Get Link Bug
        public string GetLinkBug(string bugid)
        {
            string sql = "SELECT Link FROM tbl_BugTitle WHERE BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        //Update Link Bug
        public void UpdateLinkBug(string bugid, string link)
        {
            if (link != "")
            {
                Boolean http = link.StartsWith("http");
                if (!http)
                    link = "https://jira.leapfrog.com/browse/" + link;
            }
            string sql = "UPDATE tbl_BugTitle SET Link = '" + link.Replace(" ","") + "' WHERE BugTitleID = '" + bugid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        //Get List Bug By Severtity
        public DataTable GetListBugBySeverity(string _3ld, string severity)
        {
            switch (severity)
            {
                case "S1":
                    severity = "S1. CRASHING - Non-Functionality";
                    break;
                case "S2":
                    severity = "S2. MAJOR - Incorrect Functionality";
                    break;
                case "S3":
                    severity = "S3. MEDIUM - Does not meet specification/usability problem";
                    break;
                case "S4":
                    severity = "S4. MINOR - No usability problem/may affect user experience";
                    break;
                case "S5":
                    severity = "S5. Feature request";
                    break;
            }
            string sql = "Select Link,Summary " +
                "FROM tbl_BugTitle,tbl_BugSeverity,tbl_Title " +
                "WHERE tbl_BugTitle.BugSeverityID = tbl_BugSeverity.BugSeverityID " +
                "AND tbl_BugTitle.TitleID = tbl_Title.TitleID " +
                "AND DateEnter = '" + DateTime.Now.ToString("MM/dd/yyyy") + "' " +
                "AND [3LD] = '" + _3ld + "' " +
                "AND BugSeverity = '" + severity + "' "+
                "ORDER BY Link";
            DataTable dt = dbconnect.getDataTable(sql);
            if (dt != null)
                return dt;
            return null;
        }

        //Add To My Favorite
        public void AddToMyFavorite(string employeeid, string bugid)
        {
            string userid = GetUserID(employeeid);
            string sql = "INSERT INTO tbl_BugFavourite (UserID, BugTitleID) VALUES ('" + userid + "', '" + bugid + "')";

            dbconnect.ExeCuteNonQuery(sql);
        }
        //Check My Favorite
        public Boolean CheckMyFavorite(string employeeid, string bugid)
        {
            string sql = "SELECT BugTitleID FROM tbl_BugFavourite,tbl_User WHERE tbl_BugFavourite.UserID = tbl_User.UserID AND EmployeeID = '" + employeeid + "' AND BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return true;
                else return false;
            }
            else return false;
        }

        //Remove From My Favorite
        public void RemoveFromMyFavorite(string employeeid, string bugid)
        {
            string userid = GetUserID(employeeid);
            string sql = "DELETE FROM tbl_BugFavourite WHERE UserID = '" + userid + "' AND BugTitleID = '" + bugid + "'";
            dbconnect.ExeCuteNonQuery(sql);
        }

        //Get UserID From Bug ID
        public string GetUserIDFromBugID(string bugid)
        {
            string sql = "SELECT UserEnterID FROM tbl_BugTitle WHERE BugTitleID = '" + bugid + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        //Send FeedBack
        public void SendFeedBack(string sendid, string receiveid, string titleid, string message, string point)
        {
            if(sendid=="")
                sendid = "NULL";
            else sendid = "'"+sendid+"'";
            string sql = "INSERT INTO tbl_FeedBack (Date,UserSendID,UserReceiveID,TitleID,Message,Point) "+
                   " VALUES ('" + DateTime.Now.ToString("MM/dd/yyyy") + "'," + sendid + ", '" + receiveid + "', '" + titleid + "', N'" + message.Replace("'","''") + "', '" + point + "')";
            dbconnect.ExeCuteNonQuery(sql);
        }

        //Check Owner Bug
        public Boolean CheckOwnerBug(string employeeid, string bugid)
        {
            string userid = GetUserID(employeeid);
            string sql = "SELECT Summary FROM tbl_BugTitle WHERE UserEnterID = '" + userid + "' AND BugTitleID = '" + bugid + "'";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return true;
                else return false;
            }
            else return false;
        }

        //Check Title Is Working
        public Boolean CheckTitleTodayWorking(string employeeid, string _3ld)
        {
            string sql = "SELECT * FROM tbl_ResourceAllocation,tbl_Title,tbl_User "+
                        "WHERE tbl_ResourceAllocation.UserID = tbl_User.UserID "+
                        "AND tbl_ResourceAllocation.TitleID = tbl_Title.TitleID "+
                        "AND EmployeeID = '" + employeeid + "' " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND Date = '"+DateTime.Now.ToString("MM/dd/yyyy")+"'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return true;
                else return false;
            }
            else return false;
        }

        //Check Watching Status
        public Boolean CheckWatchingStatus(string fullname, string userwatching)
        {
            if (fullname.Equals(userwatching))
                return true;
            else return false;
        }

        //Check Matching Title
        public Boolean CheckMatchingTitle(string _3ld)
        {
            string sql = "SELECT * FROM tbl_Title WHERE [3LD] = '" + _3ld + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return true;
                else return false;
            }
            else return false;
        }

        //Check Pending Bug
        public Boolean CheckPendingBug(string bugid)
        {
            string sql = "SELECT * FROM tbl_BugTitle,tbl_Title WHERE tbl_BugTitle.TitleID = tbl_Title.TitleID AND BugStatusID = '2' AND BugTitleID = '" + bugid + "'";

            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return true;
                else return false;
            }
            else return false;
        }

        //Count Total Bug
        public string CountTotalBug(string _3ld)
        {
            string sql = "SELECT MAX(BugIDTemporary) AS NUNMBER " +
                        "FROM tbl_BugTitle,tbl_Title "+
                        "WHERE tbl_BugTitle.TitleID = tbl_Title.TitleID "+
                        "AND [3LD] = '" + _3ld + "'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "0";
            }
            else return "0";
        }

        //Get New User Name
        public string getLastName(string _fullname)
        {
            String[] temp;
            temp = _fullname.Split(' ');

            return temp.First();
        }

        public string getFirstName(string _fullname)
        {
            String[] temp;
            temp = _fullname.Split(' ');

            return temp.Last();
        }

        public string getMidleName(string _fullname)
        {
            String[] temp;
            temp = _fullname.Split(' ');
            if (temp.Length > 2)
                return temp[2];
            return temp[1];
        }

        //Get Employeeid
        public string GetEmployeeId(string userid)
        {
            if (userid == "")
                userid = "NULL";
            string sql = "SELECT EmployeeID FROM tbl_User WHERE UserID = " + userid + "";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "";
            }
            else return "";
        }

        //Get Total Bug Approved Today
        public string GetTotalBugApprovedToday(string _3ld)
        {
            string sql = "SELECT COUNT(BugTitleID) AS NUMBER " +
                        "FROM tbl_BugTitle, tbl_Title, tbl_BugStatus " +
                        "WHERE tbl_BugTitle.TitleID = tbl_Title.TitleID " +
                        "AND tbl_BugTitle.BugStatusID = tbl_BugStatus.BugStatusID " +
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND DateEnter = '" + DateTime.Now.ToString("MM/dd/yyyy") + "' " +
                        "AND BugStatusName = 'Approved'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "0";
            }
            else return "0";
        }

        //Get Total Bug Logged Today
        public string GetTotalBugLoggedToday(string _3ld)
        {
            string sql = "SELECT COUNT(BugTitleID) AS NUMBER "+
                        "FROM tbl_BugTitle, tbl_Title, tbl_BugStatus "+
                        "WHERE tbl_BugTitle.TitleID = tbl_Title.TitleID "+
                        "AND tbl_BugTitle.BugStatusID = tbl_BugStatus.BugStatusID "+
                        "AND [3LD] = '" + _3ld + "' " +
                        "AND DateEnter = '" + DateTime.Now.ToString("MM/dd/yyyy") + "' " +
                        "AND Link != '' AND Link != 'NULL'";
            DataTable tb = dbconnect.getDataTable(sql);
            if (tb.Rows.Count != 0)
            {
                if (tb.Rows[0][0].ToString() != "")
                    return tb.Rows[0][0].ToString();
                else return "0";
            }
            else return "0";
        }

        //Get List Email Of Admin
        public DataTable GetListAdminEmail()
        {
            string sql = "SELECT Email "+
                        "FROM tbl_User,tbl_UserRole "+
                        "WHERE tbl_User.UserRoleID = tbl_UserRole.UserRoleID "+
                        "AND RoleName = 'Admin'";
            DataTable dt = dbconnect.getDataTable(sql);
            if (dt != null)
                return dt;
            return null;
        }
    }
}