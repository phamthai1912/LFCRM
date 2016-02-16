<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BugStatistic.aspx.cs" Inherits="LFCRM.AdminPage.BugStatistic" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True"></asp:ScriptManager>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="True" onkeydown = "return (event.keyCode!=13)">
        <ContentTemplate>
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
            <div class="topright-grid">
                <ul>
                    <li>
                        <asp:TextBox ID="txt_date" placeholder="Filter resouce by Date" runat="server" AutoPostBack="true" 
                            OnTextChanged="txt_date_TextChanged" class="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txt_date_CalendarExtender" runat="server" BehaviorID="txt_date_CalendarExtender" TargetControlID="txt_date" Format="MM/dd/yyyy" />
                    </li>
                    <li>
                        <asp:TextBox runat="server"
                            type="text"  AutoPostBack="true"
                            placeholder="Search" id="txtSearch" 
                            onkeyup="Search_Gridview(this, 'GridView1')"
                            class="form-control" style="width:300px;"></asp:TextBox>
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
            <div class="grid-style">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                    <Columns>

                        <asp:BoundField DataField="Date" HeaderText="Date"/>
                        <asp:BoundField DataField="FullName" HeaderText="Name" />
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="lb_title" runat="server" Text='<%# Bind("3LD") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectRoleName" HeaderText="Billing" />
                        <asp:BoundField DataField="Value" HeaderText="Working Hours" />                    
                        <asp:TemplateField HeaderText="No. Bugs" ItemStyle-Width="170px">
                            <ItemTemplate>
                                <asp:Label ID="lb_nobugs" runat="server"
                                    CausesValidation="False" 
                                    class="label label-info">
                                </asp:Label>
                                <asp:TextBox ID="txt_numberofbugs" runat="server" class="form-control"
                                    Visible="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="ID should not be null" 
                                            validationgroup="updatevalidationgroup"
                                            ControlToValidate="txt_numberofbugs"
                                            Display="Dynamic" runat="server" 
                                            CssClass="label label-danger"/>
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
            </div>
                <div class="bottomright-grid">
                    <ul>
                        <li>
                            
                        </li>
                    </ul>
                </div>
            
    </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
