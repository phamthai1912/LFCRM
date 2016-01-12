<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="StatisticsSale.aspx.cs" Inherits="StatisticsSale" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <table style="width:100%; font-weight: bold; height: 35px; font-size:x-large">
            <tr>
                <td align="center">
                    Thống kê bán hàng</td>
            </tr>
        </table>
        <br />
    
        <table style="width: 750px; color: black; margin-left:30px" runat=server id="tb_View">
            <tr>
                <td style="text-align: center">
                    Xem từ ngày:
                    <asp:TextBox ID="txt_StartDate" runat="server" AutoPostBack="True" 
                        ontextchanged="txt_StartDate_TextChanged"></asp:TextBox>
                    <asp:CalendarExtender ID="cld_StartDate" runat="server" TargetControlID="txt_StartDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Đến ngày:
                    <asp:TextBox ID="txt_EndDate" runat="server" AutoPostBack="True"></asp:TextBox>
                    <asp:CalendarExtender ID="cld_EndDate" runat="server" TargetControlID="txt_EndDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_View" runat="server" Text="Xem"  BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        onclick="btn_View_Click" />
                    <asp:Label ID="lbl_ThongBao" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
        </table>
        
        <asp:Panel ID="pn_ThongKe"  runat="server" Height="700px" ScrollBars="Auto">
        <asp:Panel ID="pn_lbl_ThongKe" runat="server" >
        <table  align=center style="width: 770px; color: black" runat=server>
            <tr>
                <td><asp:Label ID="lbl_ThongKe" runat="server" ></asp:Label></td>
            </tr>
        </table>
        </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="Pn_btnPrint" runat="server" Visible=false>   
            <table style="width:800px" align="center" >
                <tr>
            <td style="text-align: center">
                <asp:Button  runat="server" BackColor="#213943" BorderStyle="Ridge" 
                Font-Bold="True" ForeColor="White" Text="In phiếu" ID="btn_Print" 
                        onclick="btn_Print_Click" />
            </td>
          </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>       
        
        
        
        
</asp:Content>

