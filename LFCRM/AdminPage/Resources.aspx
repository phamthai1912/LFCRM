<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Resources.aspx.cs" Inherits="LFCRM.AdminPage.Resources" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True"></asp:ScriptManager>
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
    <!--Tooltip-->
    <script type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle=tooltip]').tooltip();
            $('[rel=tooltip]').tooltip();
        });
    </script>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT EmployeeID,FullName,Email,PhoneNumber,RoleName,Active
FROM tbl_User,tbl_UserRole
WHERE tbl_User.UserRoleID = tbl_UserRole.UserRoleID"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        ProviderName="System.Data.SqlClient" SelectCommand="SELECT RoleName FROM tbl_UserRole ORDER BY RoleName DESC"></asp:SqlDataSource>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="True" onkeydown = "return (event.keyCode!=13)">
        <ContentTemplate>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
        <div class="topright-grid">
                <ul>
                    <li>
                        <asp:TextBox runat="server"
                            type="text"  AutoPostBack="true"
                            placeholder="Search Resources" id="txtSearch" 
                            onkeyup="Search_Gridview(this, 'GridView1')"
                            class="form-control" style="width:300px;"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Button ID="btn_addresource" 
                            data-toggle="tooltip" data-placement="top" title="Add new Resource"
                            runat="server" Text="+" class="btn btn-success" OnClick="btn_addresource_Click"/>
                    </li>
                </ul>
        </div><br /><br />
        <div class="grid-style">
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"
                CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="EmployeeID" HeaderText="Resource ID" />
                    <asp:BoundField DataField="FullName" HeaderText="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
                    <asp:BoundField DataField="RoleName" HeaderText="Role" />
                    <asp:TemplateField HeaderText="Active">
                        <ItemTemplate>
                            <asp:CheckBox ID="cb_activegrid" runat="server" Enabled="false" Checked='<%# Bind("Active") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="Change Password">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" 
                                data-toggle="tooltip" data-placement="top" title="Change password"
                                class="btn btn-success"
                                CausesValidation="False" 
                                CommandName="change_pass" 
                                Text="Change Pass"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                <span class="glyphicon glyphicon-lock"></span>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="Edit Resource">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" 
                                data-toggle="tooltip" data-placement="top" title="Edit Resource"
                                CausesValidation="False" 
                                class="btn btn-success"
                                CommandName="edit_resource" Text="Edit"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                <span class="glyphicon glyphicon-pencil"></span>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="Delete Resource">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" 
                                data-toggle="tooltip" data-placement="top" title="Delete Resource"
                                CausesValidation="False" 
                                class="btn btn-success"
                                CommandName="delete_resource" Text="Delete"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                <span class="glyphicon glyphicon-trash"></span>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-----------------------------EDIT MODAL-------------------------------->      
        <div id="editModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Edit Resource</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                    <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                        <table class="table table-striped table-bordered table-responsive table-condensed"> 
                            <tr>
                                <td class="modal-body">
                                    Resource ID:
                                    <asp:Label ID="lb_id" runat="server"></asp:Label>                                    
                                </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_id" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="ID should not be null" 
                                            validationgroup="editvalidationgroup"
                                            ControlToValidate="txt_id"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/>
                                    <asp:CompareValidator ControlToValidate="txt_id" 
                                        runat="server" ErrorMessage="Please enter number only" 
                                        Operator="DataTypeCheck" Type="Integer" CssClass="label label-danger">
                                    </asp:CompareValidator><br />
                                    <asp:Label ID="lb_id1" runat="server" CssClass="label label-danger"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Name:
                                </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_name" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="Name should not be null" 
                                            validationgroup="editvalidationgroup"
                                            ControlToValidate="txt_name"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Email:
                                </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_email" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="Email should not be null" 
                                            validationgroup="editvalidationgroup"
                                            Display="Dynamic"
                                            ControlToValidate="txt_email"
                                            runat="server" 
                                            class="label label-danger"/><br />
                                    <asp:RegularExpressionValidator ErrorMessage="Invalid Email Format"
                                        ID="regexEmailValid" 
                                        validationgroup="editvalidationgroup"
                                        Display="Dynamic"
                                        runat="server" 
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        ControlToValidate="txt_email"                                         
                                        CssClass="label label-danger"
                                        >
                                    </asp:RegularExpressionValidator>                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Phone Number:
                                </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_phone" runat="server" class="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Role:
                                </td>
                                <td class="modal-body">
                                    <asp:DropDownList ID="drop_role" runat="server"
                                        Display="Dynamic"
                                        DataSourceID="SqlDataSource2" 
                                        DataTextField="RoleName" 
                                        DataValueField="RoleName"
                                        class="form-control"
                                        ></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Active Status:
                                </td>
                                <td class="modal-body">
                                    <asp:CheckBox ID="cb_active" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btn_save" 
                            causesvalidation="true"
                            validationgroup="editvalidationgroup"
                            runat="server" Text="Save" class="btn btn-success" OnClick="btn_save_Click"/>
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
    <!-----------------------------Change Pass MODAL-------------------------------->      
        <div id="PassModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Change Password</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                    <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                        <table class="table table-striped table-bordered table-responsive table-condensed"> 
                            <tr>
                                <td class="modal-body">
                                    User ID:
                                </td>
                                <td class="modal-body">
                                    <asp:Label ID="lb_passid" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    New Password:
                                </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_newpass" TextMode="password" Placeholder="Enter new password" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="newpassword" runat=server 
                                            Display="Dynamic"
                                            validationgroup="passvalidationgroup"
                                            ControlToValidate=txt_newpass
                                            ErrorMessage="Please enter new password."
                                            CssClass="label label-danger">
                                        </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Confirm New Password:
                                </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_confirmnewpass" TextMode="password" Placeholder="Re-enter new password" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="confirmpassword" runat=server 
                                            Display="Dynamic"
                                            validationgroup="passvalidationgroup"
                                            ControlToValidate=txt_confirmnewpass
                                            ErrorMessage="Please re-enter new password."
                                            CssClass="label label-danger">
                                        </asp:RequiredFieldValidator><br />
                                    <asp:CompareValidator runat=server
                                        Display="Dynamic"
                                        validationgroup="passvalidationgroup"
                                        ControlToValidate=txt_newpass
                                        ControlToCompare=txt_confirmnewpass 
                                        ErrorMessage="Passwords do not match."
                                        CssClass="label label-danger" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btn_update" 
                            causesvalidation="true"
                            validationgroup="passvalidationgroup"
                            runat="server" Text="Update" class="btn btn-success" OnClick="btn_update_Click"/>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                    </ContentTemplate>
                        <Triggers> 
                            <asp:AsyncPostBackTrigger ControlID="btn_update" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    <!-----------------------------DELETE MODAL-------------------------------->     
    <div id="DeleteModal" class="modal fade">
            <div class="modal-dialog" >
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Delete Resource</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                    <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                        <center><span><h4>Are you sure you want to delete?</h4></span><br />
                            <h3><strong>ID:
                                <asp:Label ID="lb_deleteid" runat="server"></asp:Label><br />
                                Name:
                                <asp:Label ID="lb_deleteuser" runat="server" ></asp:Label>

                                </strong></h3>
                        </center>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btn_ok" runat="server" Text="OK" class="btn btn-success" OnClick="btn_ok_Click"/>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    </div>
                    </ContentTemplate>
                        <Triggers> 
                            <asp:AsyncPostBackTrigger ControlID="btn_ok" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    <!-----------------------------Add MODAL-------------------------------->     
    <div id="AddModal" class="modal fade">
            <div class="modal-dialog" >
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Add new Resource</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                    <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                        <table class="table table-striped table-bordered table-responsive table-condensed"> 
                            <tr>
                                <td class="modal-body">
                                    Resource ID:
                                </td>
                                <td class="modal-body">                                    
                                    <asp:TextBox ID="txt_newid" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="ID should not be null" 
                                            validationgroup="addvalidationgroup"
                                            ControlToValidate="txt_newid"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/>
                                    <asp:CompareValidator ControlToValidate="txt_newid" 
                                        validationgroup="addvalidationgroup"
                                        runat="server" ErrorMessage="Please enter number only" 
                                        Display="Dynamic"
                                        Operator="DataTypeCheck" Type="Integer" CssClass="label label-danger">
                                    </asp:CompareValidator><br />
                                    <asp:Label ID="lb_newid" runat="server" CssClass="label label-danger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Role:
                                </td>
                                <td class="modal-body">
                                    <asp:DropDownList ID="drop_newrole" 
                                        DataSourceID="SqlDataSource2" 
                                        DataTextField="RoleName" 
                                        DataValueField="RoleName"
                                        runat="server" class="form-control"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Full name:
                                </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_newname" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="Name should not be null" 
                                            validationgroup="addvalidationgroup"
                                            ControlToValidate="txt_newname"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Email:
                                </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_newmail" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="Email should not be null" 
                                            Display="Dynamic"
                                            validationgroup="addvalidationgroup"
                                            ControlToValidate="txt_newmail"
                                            runat="server" 
                                            class="label label-danger"/><br />
                                    <asp:RegularExpressionValidator ErrorMessage="Invalid Email Format"
                                        Display="Dynamic"
                                        ID="Emailvalidation2" 
                                        validationgroup="addvalidationgroup"
                                        runat="server" 
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        ControlToValidate="txt_newmail"                                         
                                        CssClass="label label-danger"
                                        >
                                    </asp:RegularExpressionValidator> 
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Phone Number:
                                </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_newphone" runat="server" class="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">
                                    Active:
                                </td>
                                <td class="modal-body">
                                    <asp:CheckBox ID="cb_newactive" runat="server" class="form-control"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btn_add" 
                            causesvalidation="true"
                            validationgroup="addvalidationgroup"
                            va
                            runat="server" Text="Add" class="btn btn-success" OnClick="btn_add_Click"/>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    </div>
                    </ContentTemplate>
                        <Triggers> 
                            <asp:AsyncPostBackTrigger ControlID="btn_add" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
</asp:Content>
