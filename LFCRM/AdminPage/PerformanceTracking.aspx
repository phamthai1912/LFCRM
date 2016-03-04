<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PerformanceTracking.aspx.cs" Inherits="LFCRM.AdminPage.PerformanceTracking" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <script src="../Scripts/bootstrap.min.js"></script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="true"></asp:ScriptManager>
    <!--Tooltip-->
    <script type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle=tooltip]').tooltip();
            $('[rel=tooltip]').tooltip();
        });
    </script>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #F0F0F0; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;"></div>
                    <div style="position: fixed; top: 40%; left: 40%; height:15%; width:15%; z-index: 100001;  background-color: #FFFFFF; background-image: url('../Image/loading.gif'); background-repeat: no-repeat; background-position:center;"></div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <table>
                <tr>
                    <td style="width: 1180px; text-align: right" colspan="2">
                        <div style="text-align: right">
                            <asp:TextBox ID="txt_date" placeholder="Select a months" runat="server" AutoPostBack="true" class="form-control" Width="200px" OnTextChanged="txt_date_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txt_date_CalendarExtender" runat="server" BehaviorID="txt_date_CalendarExtender" TargetControlID="txt_date" Format="MM/yyyy" DefaultView="Months" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden" />
                            
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style='vertical-align:top'>
                        <br />
                        <asp:Label ID="lbl_PT" runat="server" Text="" AutoPostBack="true"></asp:Label>
                        
                        <asp:Label ID="Label1" runat="server" Text="" AutoPostBack="true"></asp:Label>
                        <br /><br />
                    </td>
                    <td style='width:10px;'></td>
                    <td style='vertical-align:top'>
                        <br />
                        <asp:Label ID="lbl_Title" runat="server" Text="" AutoPostBack="true"></asp:Label>
                    </td>
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
