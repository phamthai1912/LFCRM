<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ViewExport.aspx.cs" Inherits="ViewExport" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <table style="width:100%; font-weight: bold; height: 35px; font-size:x-large">
            <tr>
                <td align="center">
                    Xem Phiếu Xuất</td>
            </tr>
        </table>
        <br />
        <table style="width: 750px; color: black; margin-left:30px" runat=server id="tb_View">
            <tr>
                <td>
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
            
            <tr>
                <td align=center>
                    <asp:GridView ID="grv_ListExport" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" 
                        onpageindexchanging="grv_ListExport_PageIndexChanging" PageSize="33" 
                        Width="700px" onrowediting="grv_ListExport_RowEditing">
                        <Columns>
                            <asp:BoundField DataField="Id_PhieuXuat" HeaderText="Mã phiếu xuất" />
                            <asp:BoundField DataField="HoTen" HeaderText="Người lập phiếu" />
                            <asp:BoundField DataField="NgayXuat" HeaderText="Ngày xuất" />
                            <asp:CommandField CancelText="" DeleteText="" HeaderText="" InsertText="" 
                                NewText="" SelectText="" ShowCancelButton="False" ShowEditButton="True" 
                                UpdateText="" EditText="Chi tiết">
                            <ItemStyle ForeColor="#0000CC" />
                        </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbl_ViewDetail" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td align=center>
                    <asp:Button ID="btn_Back" runat="server" Text="Quay lại"  BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White"
                        onclick="btn_Back_Click" Visible=false/>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Print" runat="server" Text="In phiếu"  BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" Visible=false 
                        onclick="btn_Print_Click"/>
                </td>
            </tr>
        </table>
        
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

