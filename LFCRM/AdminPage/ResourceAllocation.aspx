<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResourceAllocation.aspx.cs" Inherits="LFCRM.AdminPage.ResourceAllocation" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="true"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="ra123">
                <tr>
                    <td></td>
                    <td></td>
                    <td id="td_ResourceAllocationButtonBar" style="height: 68px" >
                        <asp:TextBox ID="txt_Date" runat="server" Height="33px" Width="100px" BorderStyle="Ridge" BorderWidth="1px" TextMode="DateTime"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txt_Date_CalendarExtender" runat="server" FirstDayOfWeek="Monday" PopupButtonID="txt_Date" TargetControlID="txt_Date" Format="MM/dd/yyyy" />
                        &nbsp; 
                        <asp:Button ID="btn_CopyFromThisDay" runat="server" Text="Copy from this day" CssClass="btn btn-success" OnClick="btn_CopyFromThisDay_Click"/>
                        &nbsp;
                        &nbsp; 
                        &nbsp; 
                        &nbsp;
                        &nbsp;
                        &nbsp; 
                        <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btn_Save_Click" ValidationGroup="RAValidation"/> 
                        &nbsp; 
                        <asp:Button ID="btn_Export" runat="server" Text="Export" CssClass="btn btn-success" OnClick="btn_Export_Click"/> 
                        &nbsp; 
                        <asp:Button ID="btn_Clear" runat="server" Text="Clear" CssClass="btn" OnClick="btn_Clear_Click" /> 
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <table>
                            <tr>
                                <td style="width: 380px;" valign="top">
                                    <table id="tb_titleTable" class="table table-striped table-bordered table-responsive table-condensed table-hover">
                                        <tr>
                                            <td style="width: 180px;">3LD</td>
                                            <td>Bill</td>
                                            <td>Actual</td>
                                            <td>Action</td>
                                        </tr> 
                                        <asp:PlaceHolder ID="ph_DynamicTitleTableRow" runat="server"></asp:PlaceHolder>
                                        <tr>
                                            <td colspan="4"><asp:Button ID="btn_AddTitle" runat="server" Text="+" OnClick="btn_AddTitle_Click" CssClass="btn btn-success"/></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="test" runat="server" class="table table-striped table-bordered table-responsive table-condensed table-hover">
                                        <tr>
                                            <td><strong>Headcount</strong></td>
                                            <td><asp:Label ID="lbl_headCount" runat="server" Text="0" ForeColor="Red" style="font-weight: 700"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Total Billing</td>
                                            <td><asp:Label ID="lbl_totalBilling" runat="server" Text="0" style="font-weight: 700"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Total Assigned</td>
                                            <td><asp:Label ID="lbl_totalAssigned" runat="server" Text="0" ForeColor="#33CC33" style="font-weight: 700"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Total Trainee & Trainer</td>
                                            <td><asp:Label ID="lbl_totalTrainee" runat="server" Text="0" style="font-weight: 700"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Off</td>
                                            <td><asp:Label ID="lbl_Off" runat="server" Text="0" style="font-weight: 700"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 20px"></td>
                    <td style="width: 800px" valign="top">
                        <table id="tb_resourceTable" class="table table-striped table-bordered table-responsive table-condensed table-hover">
                            <tr>
                                <td>ID&nbsp;&nbsp;<asp:Button ID="btn_SortbyID" runat="server" Text="Sort" CssClass="btn btn-xs" OnClick="btn_SortbyID_Click"/></td>
                                <td>Name&nbsp;&nbsp;<asp:Button ID="btn_SortbyName" runat="server" Text="Sort" CssClass="btn btn-xs" OnClick="btn_SortbyName_Click"/></td>
                                <td>Role&nbsp;&nbsp;<asp:Button ID="btn_SortbyRole" runat="server" Text="Sort" CssClass="btn btn-xs" OnClick="btn_SortbyRole_Click"/></td>
                                <td>Title&nbsp;&nbsp;<asp:Button ID="btn_SortbyTitle" runat="server" Text="Sort" CssClass="btn btn-xs" OnClick="btn_SortbyTitle_Click"/> </td>
                                <td>Working Hours&nbsp;&nbsp;<asp:Button ID="btn_SortbyHours" runat="server" Text="Sort" CssClass="btn btn-xs" OnClick="btn_SortbyHours_Click"/></td>
                                <td>Action</td>
                            </tr>
                            <asp:PlaceHolder ID="ph_DynamicResourceTableRow" runat="server"></asp:PlaceHolder>
                            <tr>
                                <td colspan="6"><asp:Button ID="btn_AddResource" runat="server" Text="+" CssClass="btn btn-success" OnClick="btn_AddResource_Click"/></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

           <%--<asp:Label ID="Label1" runat="server" BackColor="White" Font-Bold="True" ForeColor="#FF9900" ></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" />
            <asp:DropDownList ID="DropDownList1" runat="server" style="border-color:Green;border-width:2px" >
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
            </asp:DropDownList>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextBox1" FilterType="Numbers"/>
         --%>
            <br /><br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
