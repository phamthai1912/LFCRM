<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="LFCRM.AdminPage.Category" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True">
    </asp:ScriptManager>
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
    <!--Change color-->
    <script>
        function Color_Changed(sender) {
            sender.get_element().value = "#" + sender.get_selectedColor();
        }
    </script>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT [TitleCategoryID],[Category], [Color] FROM [tbl_TitleCategory]" 
        ></asp:SqlDataSource>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="True" onkeydown = "return (event.keyCode!=13)">
        <ContentTemplate>
        <script src="../Scripts/bootstrap.min.js"></script>
        <div class="topright-grid">
                <ul>
                    <li>
                        <asp:TextBox runat="server"
                            type="text"  AutoPostBack="true"
                            placeholder="Search Category" id="txtSearch" 
                            onkeyup="Search_Gridview(this, 'GridView1')"
                            class="form-control" style="width:300px;"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Button ID="btn_addnew" 
                            data-toggle="tooltip" data-placement="top" title="Add new Category"
                            runat="server" Text="+" class="btn btn-success" OnClick="btn_addnew_Click"/>
                    </li>
                </ul>
        </div><br /><br />
        <div class="grid-style">
        <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames="Category"
            AutoGenerateColumns="False" 
            CssClass="table table-striped table-bordered table-responsive table-condensed table-hover"
            DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                <asp:TemplateField ShowHeader="False" HeaderText="Edit Category" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" 
                            data-toggle="tooltip" data-placement="top" title="Edit Category"
                            CausesValidation="False" 
                            CommandName="edit_category" 
                            Text="Edit"
                            class="btn btn-success"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                            <span class="glyphicon glyphicon-pencil"></span>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="200px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete Category" ShowHeader="False" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            data-toggle="tooltip" data-placement="top" title="Delete Category"
                            CommandName="delete_category" 
                            Text="Delete"
                            class="btn btn-success"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                            <span class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="200px" />
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
                        <h4 class="modal-title">Edit Category</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                        <table class="table table-striped table-bordered table-responsive table-condensed"> 
                            <tr>
                                <asp:Label ID="lb_cateid" runat="server" Visible="false"></asp:Label>
                            </tr>
                            <tr>
                                <td class="modal-body">Category: </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_cate" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txt_cate_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="Category should not be empty. Please enter category." 
                                            validationgroup="editvalidationgroup"
                                            ControlToValidate="txt_cate"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/><br />
                                    <asp:Label ID="lb_cate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">Color: </td>
                                <td class="modal-body">
                                    <div style="text-align:left;">
                                        <asp:Label ID="lb_oricolor" runat="server" Width="70px" Height="35px" class="label"></asp:Label><br />
                                        <asp:Label ID="lb_color1" runat="server" Width="70px" Height="35px" class="label" Text="Change To"></asp:Label><br />
                                        <asp:Button ID="btn_changecolor" runat="server" Text="Change Color" class="btn btn-default"/>
                                        <asp:TextBox ID="txt_color" runat="server"></asp:TextBox> 
                                        <ajaxToolkit:ColorPickerExtender 
                                            ID="txt_color_ColorPickerExtender" 
                                            runat="server" 
                                            BehaviorID="txt_color_ColorPickerExtender" 
                                            TargetControlID="txt_color" 
                                            PopupButtonID="btn_changecolor"
                                            SampleControlID="lb_color1"
                                            OnClientColorSelectionChanged="Color_Changed"/>                                   

                                        <asp:Label ID="lb_color" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btn_save" runat="server" 
                            validationgroup="editvalidationgroup"
                            Text="Save" class="btn btn-success" OnClick="btn_save_Click"/>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                    </ContentTemplate>
                        <Triggers> 
                            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand"/>
                            <asp:AsyncPostBackTrigger ControlID="btn_save" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    <!-----------------------------ADD MODAL-------------------------------->     
    <div id="AddModal" class="modal fade">
            <div class="modal-dialog" >
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Add New Category</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                    <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                        <table class="table table-striped table-bordered table-responsive table-condensed">
                            <tr>
                                <td class="modal-body">Category:</td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_newcategory" AutoPostBack="true" runat="server" class="form-control"
                                        placeholder="Category name"
                                         OnTextChanged="txt_newcategory_TextChanged"></asp:TextBox>
                                    <asp:Label ID="lb_newcategory" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">Color: </td>
                                <td class="modal-body">
                                    <div style="text-align:left;">
                                        <asp:Label ID="lb_newcolor" runat="server" class="label label-default" Height="25px" Width="70px"></asp:Label>
                                    </div>
                                </td>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btn_add" runat="server" Text="Add" class="btn btn-success" OnClick="btn_add_Click"/>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                    </ContentTemplate>
                        <Triggers> 
                            <asp:AsyncPostBackTrigger ControlID="btn_add" EventName="Click"/>
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
                        <h4 class="modal-title">Delete Category</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                    <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                        <center><span><h4>Are you sure you want to delete?</h4></span><br />
                            <h3><strong><asp:Label ID="lb_deletecategory" runat="server" ></asp:Label></strong></h3>
                            <h4>
                                <asp:Label ID="lb_deletestatus" runat="server" CssClass="label label-danger"></asp:Label>
                            </h4>
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
</asp:Content>
