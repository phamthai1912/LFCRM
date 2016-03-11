<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ByTester.aspx.cs" Inherits="LFCRM.AdminPage.ByTester" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True"></asp:ScriptManager>
 <script src="../Scripts/bootstrap.min.js"></script>
    <!--Tooltip-->
    <script type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle=tooltip]').tooltip();
            $('[rel=tooltip]').tooltip();
        });
    </script>
    <!-- Rating -->
    <style type="text/css">
        .blankstar
        {
            background-image: url(../Image/blank_star.png);
            width: 20px;
            height: 20px;
        }

        .waitingstar
        {
            background-image: url(../Image/half_star.png);
            width: 20px;
            height: 20px;
        }

        .shiningstar
        {
            background-image: url(../Image/shining_star.png);
            width: 20px;
            height: 20px;
        }
    </style>
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
                <li style="text-align:right;">
                    <asp:Label ID="lb_userstatus" runat="server" Visible="false" CssClass="label label-danger"></asp:Label>
                    <asp:RequiredFieldValidator ErrorMessage="Name should not be null" 
                                            validationgroup="searchuser"
                                            ControlToValidate="txt_username"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/><br />                    
                </li>
                <li>
                    <asp:TextBox ID="txt_username" placeholder="Full Name" AutoPostBack="true" runat="server" class="form-control" style="width:200px;"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="txt_username_AutoCompleteExtender" 
                        ServiceMethod="GetCompletionListResource" 
                        runat="server" 
                        BehaviorID="txt_username_AutoCompleteExtender" 
                        CompletionSetCount="1"
                        MinimumPrefixLength="1" 
                        CompletionInterval="10"
                        EnableCaching="false"
                        FirstRowSelected="false"
                        ServicePath="~/AutoComplete.asmx" TargetControlID="txt_username">
                    </ajaxToolkit:AutoCompleteExtender>
                    
                </li>
                <li>
                    <asp:TextBox ID="txt_startdate" runat="server" placeholder="Date Start" class="form-control" style="width:100px;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txt_startdate_CalendarExtender" runat="server" 
                        BehaviorID="txt_startdate_CalendarExtender" TargetControlID="txt_startdate" 
                        ClientIDMode="Static"
                        Format="MM/dd/yyyy"/>                 
                    
                </li>
                <li>
                    <asp:TextBox ID="txt_enddate" runat="server" placeholder="Date End" class="form-control" style="width:100px;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txt_enddate_CalendarExtender" runat="server" 
                        BehaviorID="txt_enddate_CalendarExtender" TargetControlID="txt_enddate" 
                        ClientIDMode="Static"
                        Format="MM/dd/yyyy"/>
                    
                </li>
                <li>
                    <asp:Button ID="btn_search" validationgroup="searchuser" runat="server" Text="Search" class="btn btn-success" OnClick="btn_search_Click"/>
                </li>
            </ul>
            <div style="margin-left:340px;">
                <div style="float:left; margin-right:10px;">
                <!--Start Date-->
                    <asp:RequiredFieldValidator ErrorMessage="Start date should not be null" 
                                            validationgroup="searchuser"
                                            ControlToValidate="txt_startdate"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        validationgroup="searchuser"
                        Display="Dynamic"
                        ControlToValidate="txt_startdate" ErrorMessage="Invalid start date format"
                        ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                        CssClass="label label-danger">
                    </asp:RegularExpressionValidator>
                </div>
                <div>
                <!--End Date-->
                    <asp:RequiredFieldValidator ErrorMessage="End date should not be null" 
                                            validationgroup="searchuser"
                                            ControlToValidate="txt_enddate"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                        validationgroup="searchuser"
                        Display="Dynamic"
                        ControlToValidate="txt_enddate" ErrorMessage="Invalid end date format"
                        ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                        CssClass="label label-danger">
                    </asp:RegularExpressionValidator>
                </div>
            </div>
        </div><br /><br />

        <table class="table table-bordered table-responsive table-condensed table-hover">
            <tr>
                <td style="width:250px;background-color:white; ">
                    <div style="padding:25px;"><asp:Image ID="img_profile" runat="server" ImageUrl="~/Image/no-avatar.png" /><br /></div>                    
                    <div style="align-content:center; margin-left:80px;">
                        <asp:Rating ID="Rating1" runat="server" ReadOnly="true" AutoPostBack="true" StarCssClass="blankstar"
                            WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar"
                            EmptyStarCssClass="blankstar">
                        </asp:Rating>
                    </div>
                </td>
                <td>
                    <h4><strong>Profile Details</strong></h4>
                    <table class="table">
                        <tr>
                            <td class="modal-body" style="width:150px;">Full Name:</td>
                            <td class="modal-body">
                                <asp:Label ID="lb_fullname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="modal-body">EmployeeID:</td>
                            <td class="modal-body">
                                <asp:Label ID="lb_id" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="modal-body">Email:</td>
                            <td class="modal-body">
                                <asp:Label ID="lb_email" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="modal-body">Phone Number:</td>
                            <td class="modal-body">
                                <asp:Label ID="lb_phone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="modal-body">Active Status:</td>
                            <td class="modal-body">
                                <asp:CheckBox ID="cb_active" runat="server" Enabled="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="modal-body">Bug Hunter:</td>
                            <td class="modal-body">
                                <asp:PlaceHolder ID="PlaceHolder_bughunter" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <h4>Top 10 Users Interaction</h4>
        <asp:GridView ID="GridViewPeople" AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" runat="server" OnRowCommand="GridViewPeople_RowCommand" OnRowDataBound="GridViewPeople_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Full Name" HeaderStyle-Width="250px">
                    <ItemTemplate>
                        <div style="text-align:left; vertical-align:middle; padding-top:5px;">
                            
                              <span class="glyphicon glyphicon-user"></span> <asp:Label ID="lb_P_fullname" runat="server" Text='<%# Bind("FullName") %>'></asp:Label> 
                                                      
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Titles">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Days" HeaderStyle-Width="140px">
                    <ItemTemplate>
                        <div style="text-align:center; vertical-align:middle; padding-top:5px;">
                            <asp:Label ID="lb_P_days" runat="server" Text='<%# Bind("NUMBER") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <div style="padding-top:3px">
                            <asp:LinkButton ID="btn_detailpeople" CausesValidation="False" 
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                CommandName="DetailsPeople" 
                                class="btn btn-success btn-sm"
                                runat="server"><span class="glyphicon glyphicon-info-sign"></span></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <h4>Worked Titles</h4>        
        <asp:GridView ID="GridViewTitles" AutoGenerateColumns="False" 
            CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" 
            runat="server" OnRowDataBound="GridViewTitles_RowDataBound" OnRowCommand="GridViewTitles_RowCommand" OnDataBound="GridViewTitles_DataBound">
            <Columns>
                <asp:TemplateField HeaderText="Time Range" HeaderStyle-Width="100px">
                    <ItemTemplate>                   
                        <h5><asp:Label ID="lb_date" runat="server" Text='<%# Bind("Date") %>' CssClass="label label-warning"></asp:Label></h5>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Titles" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <div style="padding-top:7px;">
                        <asp:Label ID="lb_3ld" runat="server" Text='<%# Bind("3LD") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Days">
                    <ItemTemplate>
                        <div style="padding-top:7px">
                        <asp:Label ID="lb_numberdays" runat="server" CausesValidation="False" CssClass="label label-primary"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cores">
                    <ItemTemplate>
                        <div style="padding-top:7px">
                        <asp:Label ID="lb_cores" runat="server" CausesValidation="False" class="label label-danger"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bills">
                    <ItemTemplate>
                        <div style="padding-top:7px">
                        <asp:Label ID="lb_bills" runat="server" CausesValidation="False" class="label label-default"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Back-up">
                    <ItemTemplate>
                        <div style="padding-top:7px">
                        <asp:Label ID="lb_backup" runat="server" CausesValidation="False" class="label label-default"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bugs of User">
                    <ItemTemplate>
                        <div style="padding-top:5px">

                        <asp:Label ID="lb_totalbugofuser" runat="server" data-toggle="tooltip" CausesValidation="False" CssClass="btn btn-default btn-sm"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bug of Team" HeaderStyle-Width="140px">
                    <ItemTemplate>
                        <div style="padding-top:3px">
                        <asp:Label ID="lb_tototalbugofteam" runat="server" CausesValidation="False" CssClass="btn btn-default btn-sm"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="140px">
                    <ItemTemplate>
                        <div style="padding-top:7px">
                        <asp:Label ID="lb_bugperformance" runat="server" CausesValidation="False" class="label label-info"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <div style="padding-top:3px">
                        <asp:LinkButton ID="btn_detailtitle" CausesValidation="False" 
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                CommandName="DetailTitles" 
                                class="btn btn-success btn-sm"
                                runat="server"><span class="glyphicon glyphicon-info-sign"></span></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <h4>FeedBack from others</h4>
        <asp:GridView ID="GridViewFeedback" AutoGenerateColumns="False" 
             GridLines="Horizontal"
            CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" runat="server" OnRowDataBound="GridViewFeedback_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="UserSendID" HeaderStyle-Width="50px" Visible="false">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_FB_sendid" runat="server" Text='<%# Bind("UserSendID") %>'></asp:Label>                            
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_FB_date" runat="server" Text='<%# Bind("Date") %>'></asp:Label>                            
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" HeaderStyle-Width="70px">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_FB_title" runat="server" Text='<%# Bind("3LD") %>'></asp:Label>                            
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Message">
                    <ItemTemplate>
                        <div style="text-align: left;">                            
                            <asp:Label ID="lb_FB_message" runat="server" Text='<%# Bind("Message") %>'></asp:Label>
                            <h6 style="text-align:right;font-style:italic; margin-top:30px;">From: <asp:Label ID="lb_FB_usersend" runat="server" Text="Label"></asp:Label></h6>                           
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Point" HeaderStyle-Width="145px">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center">
                            <asp:Rating ID="RatingPoint"
                                runat="server"
                                AutoPostBack="true"
                                ReadOnly="true"
                                StarCssClass="blankstar"
                                WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar"
                                EmptyStarCssClass="blankstar" RatingDirection="LeftToRightTopToBottom">
                            </asp:Rating>
                            <h6>(<asp:Label ID="lb_FB_point" runat="server" Text='<%# Bind("Point") %>'></asp:Label>)</h6>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br /><br />
    </ContentTemplate>
 </asp:UpdatePanel>
    <!-----------------------------Details Based On Titles--------------------------------> 
    <div id="detailTitle" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Details</h4>
                </div>
            
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                            <table class="table table-striped table-bordered table-responsive table-condensed">
                                <tr>
                                    <td class="modal-body">Cores:</td>
                                    <td class="modal-body">
                                        <asp:PlaceHolder ID="place_coreslist" runat="server"></asp:PlaceHolder>                                    
                                    </td>
                                </tr>                            
                            </table>
                            <br />
                            <table class="table table-striped table-bordered table-responsive table-condensed">
                                <tr>
                                    <td class="modal-body"><b>People worked with</b></td>
                                    <td class="modal-body"><b>Number Days</b></td>
                                </tr>
                                <asp:PlaceHolder ID="place_peopledetails" runat="server"></asp:PlaceHolder>
                            </table>
                        </div>
                        <div class="modal-footer">

                        </div>
                </ContentTemplate>
                    <Triggers> 
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-----------------------------Details Based On People--------------------------------> 
    <div id="detailPeople" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Details</h4>
                </div>
            
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                            <table class="table table-striped table-bordered table-responsive table-condensed">
                                <tr>
                                    <td class="modal-body"><b>Titles</b></td>
                                    <td class="modal-body"><b>Days</b></td>
                                </tr>
                                <asp:PlaceHolder ID="PlaceHolder_people" runat="server"></asp:PlaceHolder>
                            </table>
                        </div>
                        <div class="modal-footer">
                        </div>
                </ContentTemplate>
                    <Triggers> 
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
