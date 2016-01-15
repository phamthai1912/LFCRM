<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResourceAllocation.aspx.cs" Inherits="LFCRM.AdminPage.ResourceAllocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">

        <table class="table-resourceallocation">
            <tr>
                <td style="width: 300px">Titles</td>
                <td style="width: 850px">Resources</td>
            </tr>
            <tr>
                <td class="tr-resourceallocation"">
                    <table>
                        <tr>
                            <td>Title</td>
                            <td>Expected Resources</td>
                            <td>Actual Resources</td>
                            <td>Action</td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="txt_title1" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="txt_expectedresouces1" runat="server"></asp:TextBox></td>
                            <td><asp:Label ID="lbl_actualresources1" runat="server"></asp:Label></td>
                            <td><asp:Button ID="btn_remove" runat="server" Text=" - "></asp:Button></td>
                        </tr>
                        <tr>
                            <td><asp:PlaceHolder ID="ph_title" runat="server"></asp:PlaceHolder></td>
                            <td><asp:PlaceHolder ID="ph_expectedresouces" runat="server"></asp:PlaceHolder></td>
                            <td><asp:PlaceHolder ID="ph_actualresouces" runat="server"></asp:PlaceHolder></td>
                            <td><asp:PlaceHolder ID="ph_remove" runat="server"></asp:PlaceHolder></td>
                        </tr>
                        <tr>
                            <td colspan="4"><br /><asp:Button ID="btn_add" runat="server" Text="+" OnClick="btn_add_Click" /></td>
                        </tr>
                    </table>
                </td>
                <td class="tr-resourceallocation">3333333</td>
            </tr>
        </table>
    <br />
    <br />

    <br />
    <br />
    <br />
    <br />
    <br />

    <br />
    <br />
    <br />




</asp:Content>
