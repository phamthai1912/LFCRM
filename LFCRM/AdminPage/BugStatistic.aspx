<%@ Page Title="" MaintainScrollPositionOnPostback="true" SmartNavigation="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BugStatistic.aspx.cs" Inherits="LFCRM.AdminPage.BugStatistic" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True"></asp:ScriptManager>
    <script src="../Scripts/bootstrap.min.js"></script>
    <style type="text/css">
         .hiddenTitle
         {
             display:none;
         }
    </style>
    <script type="text/javascript">
        // It is important to place this JavaScript code after ScriptManager1
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=Panel1.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos = $get('<%=Panel1.ClientID%>').scrollLeft;
                yPos = $get('<%=Panel1.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=Panel1.ClientID%>') != null) {
               // Set X and Y positions back to the scrollbar
               // after partial postback
               $get('<%=Panel1.ClientID%>').scrollLeft = xPos;
               $get('<%=Panel1.ClientID%>').scrollTop = yPos;
           }
       }

       prm.add_beginRequest(BeginRequestHandler);
       prm.add_endRequest(EndRequestHandler);
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" Visible="True" onkeydown = "return (event.keyCode!=13)">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #F0F0F0; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;"></div>
                    <div style="position: fixed; top: 40%; left: 40%; height:15%; width:15%; z-index: 100001;  background-color: #FFFFFF; background-image: url('../Image/loading.gif'); background-repeat: no-repeat; background-position:center;"></div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <!--Searching-->
            <script type="text/javascript">
                function Search_Gridview(strKey, strGV) {
                    var strData = strKey.value.toLowerCase().split(" ");
                    var tblData = document.getElementById("<%= GridView1.ClientID %>");
                    var rowData;
                    for (var i = 1; i < tblData.rows.length; i++) {
                        rowData = tblData.rows[i].innerHTML;
                        var styleDisplay = 'none';
                        for (var j = 0; j < strData.length; j++) {
                            if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                                styleDisplay = '';
                            else {
                                styleDisplay = 'none';
                                break;
                            }
                        }
                        tblData.rows[i].style.display = styleDisplay;
                    }
                }
            </script>
            
            <!--Top Gridview-->
            <h3><asp:Label ID="lb_datestatus" runat="server"></asp:Label></h3>
            <hr />
            <div class="topright-grid">
                <ul>                    
                    <%--<li>
                        <asp:TextBox runat="server"
                            type="text"  AutoPostBack="true"
                            placeholder="Quick Search" id="txtSearch" 
                            onkeyup="Search_Gridview(this, 'GridView1')"
                            class="form-control" style="width:200px;"></asp:TextBox>
                    </li>--%>
                    <li>
                        <asp:TextBox ID="txt_date" placeholder="Filter resouce by Months" runat="server" AutoPostBack="true" 
                            OnTextChanged="txt_date_TextChanged" class="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                            BehaviorID="txt_date_CalendarExtender" TargetControlID="txt_date" 
                            ClientIDMode="Static" OnClientShown="onCalendarShown"
                            OnClientHidden="onCalendarHidden"
                            PopupButtonID="imgStart"
                            Format="MM/yyyy" DefaultView="Months" />
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
                    <li>
                        <asp:TextBox ID="txt_newsearch" runat="server"
                            placeholder="Search User" class="form-control" style="width:200px;"></asp:TextBox>
                    </li>
                    <li>
                        <asp:TextBox ID="txt_title" placeholder="Search title" class="form-control" runat="server" style="width:200px;"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Button ID="btn_search" runat="server" Text="Search" class="btn btn-success" OnClick="btn_search_Click"/>
                    </li>
                    <li>
                        <asp:Button ID="btn_updatebugs" runat="server" 
                                validationgroup="updatevalidationgroup"
                                data-toggle="tooltip" data-placement="top" title="Updates number of bugs"
                                class="btn btn-success"
                                Visible="false"
                                Text="Save" OnClick="btn_updatebugs_Click" />
                    </li>
                </ul>
            </div><br /><br />
            <!--<div class="grid-style" style="height:470px; "> -->
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="470px">
                <asp:GridView ID="GridView1" GridLines="Horizontal" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                    CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Date">
                            <HeaderTemplate>
                                Date <asp:LinkButton ID="lnk_date" runat="server" CommandName="sorting_date"><span class="glyphicon glyphicon-sort"></span></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lb_date" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID">
                            <HeaderTemplate>
                                ID <asp:LinkButton ID="lnk_id" runat="server" CommandName="sorting_id"><span class="glyphicon glyphicon-sort"></span></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lb_employeeid" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <HeaderTemplate>
                                Name <asp:LinkButton ID="lnk_fullname" runat="server" CommandName="sorting_fullname"><span class="glyphicon glyphicon-sort"></span></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lb_fullname" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title ID">                            
                            <ItemTemplate>
                                <asp:Label ID="lb_titleid" runat="server" Text='<%# Bind("TitleID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="hiddenTitle" />
                            <ItemStyle CssClass="hiddenTitle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <HeaderTemplate>
                                Title <asp:LinkButton ID="lnk_title" runat="server" CommandName="sorting_title"><span class="glyphicon glyphicon-sort"></span></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lb_title" runat="server" Text='<%# Bind("3LD") %>' CssClass="btn btn-default btn-sm">
                                    <span class='glyphicon glyphicon-tag'></span>
                                </asp:LinkButton>
                                                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Billing">
                            <HeaderTemplate>
                                Billing <asp:LinkButton ID="lnk_billing" runat="server" CommandName="sorting_billing"><span class="glyphicon glyphicon-sort"></span></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lb_billing" runat="server" Text='<%# Bind("ProjectRoleName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Working Hours">
                            <HeaderTemplate>
                                Working Hours <asp:LinkButton ID="lbk_working" runat="server" CommandName="sorting_working"><span class="glyphicon glyphicon-sort"></span></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lb_working" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bugs" ItemStyle-Width="170px">
                            <HeaderTemplate>
                                Bugs <asp:LinkButton ID="lnk_sort" runat="server" CommandName="sorting_bugs"><span class="glyphicon glyphicon-sort"></span></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lb_nobugs" runat="server"
                                    CausesValidation="False" 
                                    class="label label-info">
                                </asp:Label>
                                <asp:TextBox ID="txt_numberofbugs" runat="server" class="form-control"
                                    Visible="false"></asp:TextBox>
                                    <asp:CompareValidator ControlToValidate="txt_numberofbugs" 
                                        validationgroup="updatevalidationgroup"
                                        runat="server" ErrorMessage="Please enter number only" 
                                        Display="Dynamic"
                                        Operator="DataTypeCheck" Type="Integer" CssClass="label label-danger">
                                        </asp:CompareValidator>
                            </ItemTemplate>
                            <ItemStyle Width="170px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID = "chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID = "chkid" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </asp:Panel>
            <!--</div>                        -->
    </ContentTemplate>
 </asp:UpdatePanel>
    <!-----------------------------MODAL UPDATE BUG TITLE-------------------------------->     
    <div id="BugTitle" class="modal fade">
        <div class="modal-dialog" >
            <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h4 class="modal-title">Update Bugs Title</h4>
            </div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                        <asp:Label ID="lb_modal_titleid" runat="server" Visible="false"></asp:Label>
                        <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Height="400px">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" 
                            OnRowDataBound="GridView2_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_modal_date" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_modal_title" runat="server" Text='<%# Bind("3LD") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Bugs">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_modal_currentbugs" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Bugs">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_modal_totalbugs" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CompareValidator ControlToValidate="txt_modal_totalbugs" 
                                            validationgroup="modalupdatetitlebugs"
                                            runat="server" ErrorMessage="Please enter number only" 
                                            Display="Dynamic"
                                            Operator="DataTypeCheck" Type="Integer" CssClass="label label-danger">
                                        </asp:CompareValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Save">
                                    <ItemTemplate>
                                        <asp:Button ID="bt_modal_save" 
                                            CommandName="modal_savebugs"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            validationgroup="modalupdatetitlebugs" 
                                            runat="server" Text="Save" class="btn btn-success small"/>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                        </asp:Panel>
                        <center>
                            <asp:Label ID="lb_modal_status" runat="server" CssClass="label label-success" Visible="false"></asp:Label>
                            <asp:Label ID="lb_modal_status2" runat="server" CssClass="label label-danger" Visible="false"></asp:Label>
                        </center>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btn_save" runat="server" Text="Save" class="btn btn-success" OnClick="btn_save_Click"/>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </ContentTemplate>
                <Triggers> 
                    <asp:AsyncPostBackTrigger ControlID="btn_save" EventName="Click"/>
                </Triggers>
            </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
