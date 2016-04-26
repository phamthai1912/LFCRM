<%@ Page Title="" MaintainScrollPositionOnPostback="true" SmartNavigation="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BugStatistic.aspx.cs" Inherits="LFCRM.AdminPage.BugStatistic" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True"></asp:ScriptManager>
    <script src="../Scripts/bootstrap.min.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" Visible="True" onkeydown = "return (event.keyCode!=13)">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #F0F0F0; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;"></div>
                    <div style="position: fixed; top: 40%; left: 40%; height:15%; width:15%; z-index: 100001; background-image: url('../Image/loading.gif'); background-repeat: no-repeat; background-position:center;"></div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="topright-grid">
                <ul>
                    <li>
                        <asp:TextBox ID="txt_month" AutoPostBack="true" runat="server" placeholder="Select Month" CssClass="form-control" Width="150px" OnTextChanged="txt_month_TextChanged"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender runat="server" 
                            BehaviorID="txt_month_CalendarExtender" 
                            ClientIDMode="Static" OnClientShown="onCalendarShown"
                            OnClientHidden="onCalendarHidden"
                            PopupButtonID="imgStart"
                            TargetControlID="txt_month" ID="txt_month_CalendarExtender" 
                            Format="MM/yyyy" DefaultView="Months"></ajaxToolkit:CalendarExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            validationgroup="month"
                            Display="Dynamic"
                            ControlToValidate="txt_month" ErrorMessage="Invalid month format"
                            ValidationExpression="^((0[1-9])|(1[0-2]))\/((2009)|(20[1-2][0-9]))$"
                            CssClass="label label-danger">
                        </asp:RegularExpressionValidator>
                        <script type="text/javascript">

                            function onCalendarHidden() {
                                var cal = $find("txt_month_CalendarExtender");

                                if (cal._monthsBody) {
                                    for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                                        var row = cal._monthsBody.rows[i];
                                        for (var j = 0; j < row.cells.length; j++) {
                                            Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                                        }
                                    }
                                }
                            }

                            function onCalendarShown() {

                                var cal = $find("txt_month_CalendarExtender");

                                cal._switchMode("months", true);

                                if (cal._monthsBody) {
                                    for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                                        var row = cal._monthsBody.rows[i];
                                        for (var j = 0; j < row.cells.length; j++) {
                                            Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                                        }
                                    }
                                }
                            }

                            function call(eventElement) {
                                var target = eventElement.target;
                                switch (target.mode) {
                                    case "month":
                                        var cal = $find("txt_month_CalendarExtender");
                                        cal._visibleDate = target.date;
                                        cal.set_selectedDate(target.date);
                                        //cal._switchMonth(target.date);
                                        cal._blur.post(true);
                                        cal.raiseDateSelectionChanged();
                                        break;
                                }
                            }
                    </script>
                    </li>
                </ul>                
            </div>
            <br />
            <h3><asp:Label ID="lb_time" runat="server"></asp:Label></h3>
            <hr />
            <asp:Chart ID="Chart1" Width="1100px" Height="600px" runat="server">
                <Series>
                    <asp:Series Name="Series1" ChartType="Column" LabelBackColor="#f5b57f" Color="#abd46e"  XValueMember="FullName" YValueMembers="NUMBER"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisY Title="Bug Number">
                            <MajorGrid LineColor="YellowGreen" />
                        </AxisY>
                        <AxisX IsLabelAutoFit="False" LabelAutoFitStyle="None" Title="User Name">
                            <MajorGrid LineColor="YellowGreen" />
                            <LabelStyle Angle="-90" Interval="Auto"
                                IsEndLabelVisible="False" />
                            <ScaleBreakStyle BreakLineStyle="None" />
                        </AxisX>
                        <AxisX2 LineColor="YellowGreen" TextOrientation="Rotated90">
                        </AxisX2>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </ContentTemplate>        
 </asp:UpdatePanel>
</asp:Content>
