﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TitleManager.aspx.cs" Inherits="LFCRM.AdminPage.TitleManager" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True" />
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

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT [3LD],TitleName,TOCKCode,Category,ColorCode
FROM tbl_Title,tbl_TitleCategory
WHERE tbl_Title.TitleCategoryID = tbl_TitleCategory.TitleCategoryID
ORDER BY [3LD] DESC" 
        DeleteCommand="DELETE FROM tbl_Title
WHERE [3LD] = @3LD" >
</asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [Category] FROM [tbl_TitleCategory] ORDER BY [Category]"></asp:SqlDataSource>
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="True" onkeydown = "return (event.keyCode!=13)">
        <ContentTemplate>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
        
        <div class="topright-grid">
            <ul>
                <li>
                    <asp:TextBox runat="server"
                            type="text"  AutoPostBack="true"
                            placeholder="Search " id="TextBox1" 
                            onkeyup="Search_Gridview(this, 'GridView1')"
                            class="form-control" style="width:300px;"></asp:TextBox>
                </li>
                <li>
                    <asp:Button ID="btn_addnew" 
                        data-toggle="tooltip" data-placement="top" title="Add new Title"
                        runat="server" Text="+" class="btn btn-success" OnClick="btn_addnew_Click"/>
                </li>
            </ul>
        </div><br /><br />
        <div class="grid-style">
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
            OnRowCommand="GridView1_RowCommand"
            CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" 
            DataKeyNames="3LD" OnRowDataBound="GridView1_RowDataBound" AllowSorting="True"
            >
            <Columns>
                <asp:BoundField DataField="3LD" HeaderText="3LD" ItemStyle-Width="50px"/>
                <asp:BoundField DataField="TitleName" HeaderText="Title Name" />
                <asp:BoundField DataField="TOCKCode" HeaderText="Tock Code" />
                <asp:BoundField DataField="Category" HeaderText="Category" />
                <asp:BoundField DataField="ColorCode" HeaderText="Color" />
                <asp:TemplateField HeaderText="Edit Title" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_edit" runat="server" 
                            data-toggle="tooltip" data-placement="top" title="Edit Title"
                            CommandName="edit_Title" class="btn btn-success" 
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                            <span class="glyphicon glyphicon-pencil"></span>
                        </asp:LinkButton>                
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete Title" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_delete" runat="server" CausesValidation="False" 
                            data-toggle="tooltip" data-placement="top" title="Delete Title"
                            CommandName="Delete" class="btn btn-success" 
                            OnClientClick="return confirm('Are you sure you want to delete this Title?');" 
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
                        <h4 class="modal-title">Edit Title</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                        <table class="table table-striped table-bordered table-responsive table-condensed"> 
                            <tr>
                                <td class="modal-body">3LD: </td>
                                <td class="modal-body">
                                    <asp:Label ID="lb_3ld" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">Title Name:</td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_titlename" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txt_titlename_TextChanged"></asp:TextBox>
                                    <asp:Label ID="lb_titlename" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">Tock Code</td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_tockcode" runat="server" ValidateRequestMode="Disabled" class="form-control"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">Category</td>
                                <td class="modal-body">
                                    <asp:DropDownList ID="drop_category" runat="server" 
                                        DataSourceID="SqlDataSource2" 
                                        DataTextField="Category" 
                                        DataValueField="Category"
                                        class="form-control">

                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">Color</td>
                                <td class="modal-body">
                                    <div style="text-align:left;">
                                    <asp:Label ID="lb_oricolor" runat="server" Width="70px" Height="35px" class="label"></asp:Label><br />
                                    <asp:Label ID="lb_color1" runat="server" Width="70px" Height="35px" class="label" Text="Change To"></asp:Label><br />
                                    <asp:Button ID="btn_changecolor" runat="server" Text="Change Color" class="btn btn-default" OnClick="btn_changecolor_Click"/>
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
                        <asp:Button ID="btn_save" runat="server" Text="Save" class="btn btn-success" OnClick="btn_save_Click"/>
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
    <!-----------------------------Add New MODAL--------------------------------> 
    <div id="AddModal" class="modal fade">
            <div class="modal-dialog" >
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Add New Title</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                    <div class="modal-body" style=" margin-top: 10px; margin-bottom: 50px;">
                        <table class="table table-striped table-bordered table-responsive table-condensed">
                            <tr>
                                <td class="modal-body">3LD: </td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_new3ld" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txt_new3ld_TextChanged"
                                        placeholder="3LD"></asp:TextBox>
                                    <asp:Label ID="lb_new3ld" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">Title Name:</td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_newtitlename" AutoPostBack="true" runat="server" 
                                        class="form-control" OnTextChanged="txt_newtitlename_TextChanged"
                                        placeholder="Title Name"></asp:TextBox>
                                    <asp:Label ID="lb_newtitlename" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">Tock Code</td>
                                <td class="modal-body">
                                    <asp:TextBox ID="txt_newtockcode" runat="server" class="form-control"
                                        placeholder="Tock Code"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">Category</td>
                                <td class="modal-body">
                                    <asp:DropDownList ID="drop_newcate" runat="server" 
                                        DataSourceID="SqlDataSource2" 
                                        DataTextField="Category" 
                                        DataValueField="Category"
                                        class="form-control">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="modal-body">Color</td>
                                <td class="modal-body">
                                    <div style="text-align:left;">
                                    <asp:Label ID="lb_newcolor" runat="server" Width="70px" Height="25px" class="label label-default"></asp:Label>
                                    </div>
                                </td>
                            </tr>
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
</asp:Content>