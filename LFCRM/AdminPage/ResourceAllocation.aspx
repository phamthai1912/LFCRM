<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResourceAllocation.aspx.cs" Inherits="LFCRM.AdminPage.ResourceAllocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td style="width: 300px" valign="top">
                        <table class="table table-striped table-bordered table-responsive table-condensed table-hover">
                            <tr>
                                <td>No.</td>
                                <td>Title</td>
                                <td>Expected Resources</td>
                                <td>Actual Resources</td>
                                <td>Action</td>
                            </tr>
                            <asp:PlaceHolder ID="ph_DynamicTitleTableRow" runat="server"></asp:PlaceHolder>
                            <tr>
                                <td colspan="5"><asp:Button ID="btn_AddTitle" runat="server" Text="+" OnClick="btn_AddTitle_Click" CssClass="btn btn-success"/></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 20px"></td>
                    <td style="width: 880px" valign="top">
                        <table class="table table-striped table-bordered table-responsive table-condensed table-hover">
                            <tr>
                                <td>No.</td>
                                <td>Name</td>
                                <td>Role</td>
                                <td>Titles</td>
                                <td>Working Hours</td>
                                <td>Action</td>
                            </tr>
                            <asp:PlaceHolder ID="ph_DynamicResourceTableRow" runat="server"></asp:PlaceHolder>
                            <tr>
                                <td colspan="6"><asp:Button ID="btn_AddResource" runat="server" Text="+" CssClass="btn btn-success" OnClick="btn_AddResource_Click"/></td>
                            </tr>
                        </table>
                    </td>
            </table>
            <asp:Label ID="Label1" runat="server" ></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_AddResource" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
