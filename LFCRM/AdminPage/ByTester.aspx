<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ByTester.aspx.cs" Inherits="LFCRM.AdminPage.ByTester" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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
                    <asp:Label ID="lb_userstatus" runat="server" Visible="false" CssClass="label label-danger"></asp:Label>
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
                    <asp:RequiredFieldValidator ErrorMessage="Name should not be null" 
                                            validationgroup="searchuser"
                                            ControlToValidate="txt_username"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/>
                </li>
                <li>
                    <asp:TextBox ID="txt_startdate" runat="server" placeholder="Date Start" class="form-control" style="width:100px;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txt_startdate_CalendarExtender" runat="server" 
                        BehaviorID="txt_startdate_CalendarExtender" TargetControlID="txt_startdate" 
                        ClientIDMode="Static"
                        Format="MM/dd/yyyy"/>
                    <asp:RequiredFieldValidator ErrorMessage="Start date should not be null" 
                                            validationgroup="searchuser"
                                            ControlToValidate="txt_startdate"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        validationgroup="searchuser"
                        ControlToValidate="txt_startdate" ErrorMessage="Invalid date format"
                        ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                        CssClass="label label-danger">
                    </asp:RegularExpressionValidator>
                </li>
                <li>
                    <asp:TextBox ID="txt_enddate" runat="server" placeholder="Date End" class="form-control" style="width:100px;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txt_enddate_CalendarExtender" runat="server" 
                        BehaviorID="txt_enddate_CalendarExtender" TargetControlID="txt_enddate" 
                        ClientIDMode="Static"
                        Format="MM/dd/yyyy"/>
                    <asp:RequiredFieldValidator ErrorMessage="End date should not be null" 
                                            validationgroup="searchuser"
                                            ControlToValidate="txt_enddate"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                        validationgroup="searchuser"
                        ControlToValidate="txt_enddate" ErrorMessage="Invalid date format"
                        ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                        CssClass="label label-danger">
                    </asp:RegularExpressionValidator>
                </li>
                <li>
                    <asp:Button ID="btn_search" validationgroup="searchuser" runat="server" Text="Search" class="btn btn-success" OnClick="btn_search_Click"/>
                </li>
            </ul>
        </div><br /><br />

        <table class="table table-bordered table-responsive table-condensed table-hover">
            <tr>
                <td style="width:250px;background-color:white; ">
                    <div style="padding:25px;"><asp:Image ID="img_profile" runat="server" ImageUrl="~/Image/no-avatar.png" /><br /></div>                    
                    <img width="100px" height="25px" src="http://www.newbrandanalytics.com/blog/wp-content/uploads/2013/03/3-5-stars.png" />
                    
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
        <br />
        <h4>Top 10 Users Interaction</h4>
        <asp:GridView ID="GridViewPeople" AutoGenerateColumns="False" 
            CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" runat="server" OnRowCommand="GridViewPeople_RowCommand" OnRowDataBound="GridViewPeople_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Full Name" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <div style="text-align:center; vertical-align:middle;">
                            <asp:Label ID="lb_P_fullname" runat="server" Text='<%# Bind("FullName") %>' CssClass="label label-warning"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                            <asp:Label ID="lb_P_title" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Days">
                    <ItemTemplate>
                            <asp:Label ID="lb_P_days" runat="server" Text='<%# Bind("NUMBER") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details">
                    <ItemTemplate>
                            <asp:LinkButton ID="btn_detailpeople" CausesValidation="False" 
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                CommandName="DetailsPeople" 
                                class="btn btn-success"
                                runat="server"><span class="glyphicon glyphicon-info-sign"></span></asp:LinkButton>
                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <h4>Worked Titles</h4>        
        <asp:GridView ID="GridViewTitles" AutoGenerateColumns="False" 
            CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" 
            runat="server" OnRowDataBound="GridViewTitles_RowDataBound" OnRowCommand="GridViewTitles_RowCommand" OnDataBound="GridViewTitles_DataBound">
            <Columns>
                <asp:TemplateField HeaderText="Time Range">
                    <ItemTemplate>
                        <div style="text-align:center;">
                            <h4><asp:Label ID="lb_date" runat="server" Text='<%# Bind("Date") %>' CssClass="label label-warning"></asp:Label></h4>
                            
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Titles">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_3ld" runat="server" Text='<%# Bind("3LD") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Days">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_numberdays" runat="server" CausesValidation="False" CssClass="label label-primary"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cores">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_cores" runat="server" CausesValidation="False" class="label label-danger"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bills">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_bills" runat="server" CausesValidation="False" class="label label-default"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Back-up">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_backup" runat="server" CausesValidation="False" class="label label-default"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Bugs User/Team">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_totalbugs" runat="server" data-toggle="tooltip" CausesValidation="False" class="label label-info"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bug/Day of Team">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_bugperformance" runat="server" CausesValidation="False" class="label label-info"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:LinkButton ID="btn_detailtitle" CausesValidation="False" 
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                CommandName="DetailTitles" 
                                class="btn btn-success"
                                runat="server"><span class="glyphicon glyphicon-info-sign"></span></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <h4>FeedBack from others</h4>
        <asp:GridView ID="GridViewFeedback" AutoGenerateColumns="False" 
             GridLines="Horizontal"
            CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" runat="server">
            <Columns>
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
                        <div style="text-align:left;">
                            <asp:Label ID="lb_FB_message" runat="server" Text='<%# Bind("Message") %>'></asp:Label>                            
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Point" HeaderStyle-Width="70px">
                    <ItemTemplate>
                        <div class="modal-body" style="text-align:center;">
                            <asp:Label ID="lb_FB_point" runat="server" Text='<%# Bind("Point") %>'></asp:Label>                            
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
