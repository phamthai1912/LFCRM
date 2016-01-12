<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="StatisticalInventory.aspx.cs" Inherits="StatisticalInventory" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width:100%; font-weight: bold; height: 35px; font-size:x-large">
            <tr>
                <td align="center">
                    Thống kê hàng tồn</td>
            </tr>
        </table>
         <br />
        <table style="width: 770px; color: black; margin-left:30px" runat=server id="tb_View">
            <tr>
                <td>
                    Chọn tháng:
                    <asp:DropDownList ID="ddl_ChonThang" runat="server" AutoPostBack="True"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_View" runat="server" Text="Xem"  BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" onclick="btn_View_Click" />
                    <asp:Label ID="lbl_ThongBao" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><hr /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_ThongKeHangTon" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align=center><asp:Button ID="btn_Print" runat="server" Text="In phiếu"  BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" Visible=false 
                        onclick="btn_Print_Click"/></td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

