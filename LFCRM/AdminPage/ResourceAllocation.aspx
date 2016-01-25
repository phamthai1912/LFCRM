<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResourceAllocation.aspx.cs" Inherits="LFCRM.AdminPage.ResourceAllocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="true"></asp:ScriptManager>

    <script type="text/javascript" src="/Scripts/jquery-latest.js"></script> 
    <script type="text/javascript" src="/Scripts/jquery.tablesorter.js"></script> 
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tb_titleTable").tablesorter({ sortList: [[0, 0], [1, 0]] });
        }
        );
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="Save" CssClass="btn btn-success"/> 
                        &nbsp;   
                        <asp:Button ID="Button3" runat="server" Text="Copy from last day" CssClass="btn btn-success"/>
                        <br /><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 380px;" valign="top">
                        <table id="tb_titleTable" class="table table-striped table-bordered table-responsive table-condensed table-hover">
                            <tr>
                                <td>No.</td>
                                <td>3LD</td>
                                <td style="width: 52px">Expected</td>
                                <td>Actual</td>
                                <td>Action</td>
                            </tr>
                            <asp:PlaceHolder ID="ph_DynamicTitleTableRow" runat="server"></asp:PlaceHolder>
                            <tr>
                                <td colspan="5"><asp:Button ID="btn_AddTitle" runat="server" Text="+" OnClick="btn_AddTitle_Click" CssClass="btn btn-success"/></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 20px"></td>
                    <td style="width: 800px;" valign="top">
                        <table id="tb_resourceTable" class="table table-striped table-bordered table-responsive table-condensed table-hover">
                            <tr>
                                <td>No.</td>
                                <td>Name</td>
                                <td>Role</td>
                                <td>Title</td>
                                <td>Working Hours</td>
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
           <%-- <asp:Label ID="Label1" runat="server" BackColor="White" Font-Bold="True" ForeColor="#FF9900" ></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <asp:DropDownList ID="DropDownList1" runat="server" ></asp:DropDownList>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextBox1" FilterType="Numbers"/>
          --%> </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
