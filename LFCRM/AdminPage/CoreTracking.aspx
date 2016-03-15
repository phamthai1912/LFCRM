<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CoreTracking.aspx.cs" Inherits="LFCRM.AdminPage.CoreTracking" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="True" onkeydown = "return (event.keyCode!=13)">
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #F0F0F0; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;"></div>
                <div style="position: fixed; top: 40%; left: 40%; height:15%; width:15%; z-index: 100001;  background-color: #FFFFFF; background-image: url('../Image/loading.gif'); background-repeat: no-repeat; background-position:center;"></div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="topright-grid">
            <ul>
                <li>
                    <asp:TextBox ID="txt_date" AutoPostBack="true" placeholder="Select Month" runat="server" class="form-control" OnTextChanged="txt_date_TextChanged"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                        BehaviorID="txt_date_CalendarExtender" 
                        ClientIDMode="Static" OnClientShown="onCalendarShown"
                        OnClientHidden="onCalendarHidden"
                        PopupButtonID="imgStart"
                        TargetControlID="txt_date" 
                        Format="MM/yyyy" DefaultView="Months"/>
                    <script type="text/javascript">

                        function onCalendarHidden() {
                            var cal = $find("txt_date_CalendarExtender");

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

                            var cal = $find("txt_date_CalendarExtender");

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
                                    var cal = $find("txt_date_CalendarExtender");
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
        <h3><asp:Label ID="lb_status" runat="server"></asp:Label></h3>
        <hr />
        <asp:GridView ID="GridView1"
            CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" 
            runat="server" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
