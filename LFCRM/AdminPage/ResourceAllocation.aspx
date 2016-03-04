<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResourceAllocation.aspx.cs" Inherits="LFCRM.AdminPage.ResourceAllocation" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <script src="../Scripts/bootstrap.min.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="true"></asp:ScriptManager>

    <script type="text/javascript">
        // It is important to place this JavaScript code after ScriptManager1
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args)
        {
            if ($get('<%=Panel1.ClientID%>') != null)
            {
               // Get X and Y positions of scrollbar before the partial postback
               xPos = $get('<%=Panel1.ClientID%>').scrollLeft;
               yPos = $get('<%=Panel1.ClientID%>').scrollTop;
            }
            else if ($get('<%=Panel2.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos = $get('<%=Panel2.ClientID%>').scrollLeft;
                yPos = $get('<%=Panel2.ClientID%>').scrollTop;
            }
       }

        function EndRequestHandler(sender, args)
        {
            if ($get('<%=Panel1.ClientID%>') != null)
            {
               // Set X and Y positions back to the scrollbar
               // after partial postback
               $get('<%=Panel1.ClientID%>').scrollLeft = xPos;
               $get('<%=Panel1.ClientID%>').scrollTop = yPos;
            }
            else if ($get('<%=Panel2.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=Panel2.ClientID%>').scrollLeft = xPos;
                $get('<%=Panel2.ClientID%>').scrollTop = yPos;
            }
       }

       prm.add_beginRequest(BeginRequestHandler);
       prm.add_endRequest(EndRequestHandler);
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #F0F0F0; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;"></div>
                    <div style="position: fixed; top: 40%; left: 40%; height:15%; width:15%; z-index: 100001;  background-color: #FFFFFF; background-image: url('../Image/loading.gif'); background-repeat: no-repeat; background-position:center;"></div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <div id="TableResourceAllocation" runat="server" visible="true">
                <table>
                    <tr>
                        <td style="width: 770px;">
                            <table>
                                <tr>
                                    <td style="width: 270px;">                            
                                        <div style='text-align: left;'>
                                            <asp:ImageButton ID="btn_MultAdd" runat="server" ImageUrl="../Image/mulitplus.ico" Height="37px" Width="37px" OnClick="btn_MultAdd_Click" ToolTip="2222  &#013; aaa" />
                                        </div></td>
                                    <td style="width: 500px;">
                                        <div style='text-align: right;'>
                                            <asp:TextBox ID="txt_Date" runat="server" Height="33px" Width="100px" BorderStyle="Ridge" BorderWidth="1px" TextMode="DateTime"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txt_Date_CalendarExtender" runat="server" FirstDayOfWeek="Monday" PopupButtonID="txt_Date" TargetControlID="txt_Date" Format="MM/dd/yyyy" />
                                                &nbsp; 
                                            <asp:Button ID="btn_CopyFromThisDay" runat="server" Text="Copy from this day" CssClass="btn btn-success" OnClick="btn_CopyFromThisDay_Click"/>                          
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                        <td style="width: 410px;">
                            <table>
                                <tr>
                                    <td style="width: 180px;">
                                        <div style='text-align: left;'>
                                            <asp:Label ID="lbl_StarofSaving" runat="server" Text="Not saved jet" Font-Bold="False" ForeColor="Red" Font-Italic="True"></asp:Label>
                                        </div>
                                    </td>
                                    <td style="width: 230px;">
                                        <div style='text-align: right;'>
                                            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btn_Save_Click" ValidationGroup="RAValidation"/> 
                                            &nbsp; 
                                            <asp:Button ID="btn_Export" runat="server" Text="Export" CssClass="btn btn-success" OnClick="btn_Export_Click" /> 
                                            &nbsp; 
                                            <asp:Button ID="btn_Clear" runat="server" Text="Clear" CssClass="btn" OnClick="btn_Clear_Click" />
                                        </div> 
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="ra123">
                        <td style="width: 770px" valign="top">
                            <br />
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="575">
                            <table id="tb_resourceTable" class="table table-striped table-bordered table-responsive table-condensed table-hover">
                                <tr>
                                    <td><strong>ID</strong>&nbsp;&nbsp;<span style="color: black"><asp:Button ID="btn_SortbyID" runat="server" Text="Sort" CssClass="btn btn-xs" OnClick="btn_SortbyID_Click"/></span></td>
                                    <td><strong>Name</strong>&nbsp;&nbsp;<span style="color: black"><asp:Button ID="btn_SortbyName" runat="server" Text="Sort" CssClass="btn btn-xs" OnClick="btn_SortbyName_Click"/></span></td>
                                    <td><strong>Role</strong>&nbsp;&nbsp;<span style="color: black"><asp:Button ID="btn_SortbyRole" runat="server" Text="Sort" CssClass="btn btn-xs" OnClick="btn_SortbyRole_Click"/>&nbsp;<asp:Button ID="btn_ClearRole" runat="server" Text="Clear" CssClass="btn btn-xs" OnClick="btn_ClearRole_Click"/></span></td>
                                    <td style="width: 150px;"><strong>Title</strong>&nbsp;&nbsp;<span style="color: black"><asp:Button ID="btn_SortbyTitle" runat="server" Text="Sort" CssClass="btn btn-xs" OnClick="btn_SortbyTitle_Click"/>&nbsp;<asp:Button ID="btn_ClearTitle" runat="server" Text="Clear" CssClass="btn btn-xs" OnClick="btn_ClearTitle_Click"/></span></td>
                                    <td><strong>Hours</strong>&nbsp;&nbsp;<span style="color: black"><asp:Button ID="btn_SortbyHours" runat="server" Text="Sort" CssClass="btn btn-xs" OnClick="btn_SortbyHours_Click"/></span></td>
                                    <td><strong>Action</strong></td>
                                </tr>
                                <asp:PlaceHolder ID="ph_DynamicResourceTableRow" runat="server"></asp:PlaceHolder>
                                <tr>
                                    <td colspan="6"><asp:Button ID="btn_AddResource" runat="server" Text="+" CssClass="btn btn-success" OnClick="btn_AddResource_Click"/></td>
                                </tr>
                            </table>
                            </asp:Panel>
                        </td>
                        <td style="width: 20px"></td>
                        <td valign="top">
                            <br />
                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="575">
                            <table>
                                <tr>
                                    <td style="width: 410px;" valign="top">
                                        <table id="tb_titleTable"  class="table table-striped table-bordered table-responsive table-condensed table-hover">
                                            <tr>
                                                <td style="width: 160px;"><strong>3LD</strong></td>
                                                <td><strong>Bill</strong> &nbsp;<span style="color: black"><asp:Button ID="btn_ClearBill" runat="server" Text="Clear" CssClass="btn btn-xs" OnClick="btn_ClearBill_Click"/></span</td>
                                                <td><strong>Actual</strong></td>
                                                <td><strong>Train</strong></td>
                                                <td><strong>Action</strong></td>
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
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
            
            <div id="TableReport" runat="server" visible="false">
                <table id="tbreport">
                    <tr>
                        <td style="width: 1120px;"><asp:Label ID="lbl_titlexport" runat="server" Text="Label" Font-Bold="True" Font-Size="Large" Font-Names="Times New Roman"></asp:Label></td>
                        <td><asp:Button ID="btn_closeReport" runat="server" Text="X" OnClick="btn_closeReport_Click" CssClass="btn" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 1150px; font-family: Times New Roman; font-size: 12pt">
                            <hr />
                            Hi All,<br /><br />

                            Below is the resource allocation for today:
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="lbl_contentexport" runat="server" Text="Label" ></asp:Label>
            </div>
            <asp:Label ID="Label1" runat="server" Font-Names="Times New Roman" Font-Size="12pt" ></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    
            <div id="editModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Add Multiple Resources</h4>
                    </div>
                    
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
						<ContentTemplate>
							<div class="modal-body" style=" margin-top: 10px; margin-bottom: 10px;">
                                <asp:TextBox id="txt_importMulRS" TextMode="multiline" Columns="75" Rows="25" runat="server" />
							</div>
							<div class="modal-footer"><asp:Button ID="btn_importMulRS" runat="server" Text="Import" class="btn btn-success" OnClick="btn_importMulRs_Click"/></div>
						</ContentTemplate>
                        <Triggers> 
                            <asp:AsyncPostBackTrigger ControlID="btn_importMulRs" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
</asp:Content>
