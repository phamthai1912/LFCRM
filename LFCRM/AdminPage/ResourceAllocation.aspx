<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResourceAllocation.aspx.cs" Inherits="LFCRM.AdminPage.ResourceAllocation" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
<%--    <script src="../Scripts/jquery.base64.js"></script>
    <script src="../Scripts/html2canvas.js"></script>
    <script src="../Scripts/tableExport.js"></script>--%>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="true"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="TableResourceAllocation" runat="server" visible="true">
                <table>
                <tr>
                    <td style="width: 380px;"></td>
                    <td></td>
                    <td id="td_ResourceAllocationButtonBar" style="width: 800px;">
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
                        <asp:Button ID="btn_Export" runat="server" Text="Export" CssClass="btn btn-success" OnClick="btn_Export_Click" /> 
                        &nbsp; 
                        <asp:Button ID="btn_Clear" runat="server" Text="Clear" CssClass="btn" OnClick="btn_Clear_Click" /> 
                    </td>
                </tr>
                <tr id="ra123">
                    <td valign="top">
                        <br />
                        <table>
                            <tr>
                                <td style="width: 380px;" valign="top">
                                    <table id="tb_titleTable" class="table table-striped table-bordered table-responsive table-condensed table-hover">
                                        <tr>
                                            <td style="width: 180px;">3LD</td>
                                            <td>Bill</td>
                                            <td>Actual</td>
                                            <td>Train</td>
                                            <td>Action</td>
                                        </tr> 
                                        <asp:PlaceHolder ID="ph_DynamicTitleTableRow" runat="server"></asp:PlaceHolder>
                                        <tr>
                                            <td colspan="5"><asp:Button ID="btn_AddTitle" runat="server" Text="+" OnClick="btn_AddTitle_Click" CssClass="btn btn-success"/></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tb_headCountTable" class="table table-striped table-bordered table-responsive table-condensed table-hover">
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
                        <br />
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
            </div>
            
            <div id="TableReport" runat="server" visible="false">
                <table id="tbreport">
                    <tr>
                        <td style="width: 1120px;"><asp:Label ID="lbl_titlexport" runat="server" Text="Label" Font-Bold="True" Font-Size="Large"></asp:Label></td>
                        <td><asp:Button ID="btn_closeReport" runat="server" Text="X" OnClick="btn_closeReport_Click" CssClass="btn" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 1150px;">
                            <hr />
                            Hi All,<br /><br />

                            &nbsp;&nbsp; • Total billable headcount: <asp:Label ID="lbl_noRsReport" runat="server"></asp:Label> resource(s).<br />
                            &nbsp;&nbsp; • <strong>Below is the resource allocation for today: </strong>
                            <br />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="lbl_contentexport" runat="server" Text="Label" ></asp:Label>
            </div>


            <br /><br />

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
