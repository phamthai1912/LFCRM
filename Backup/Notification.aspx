<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="Notification.aspx.cs" Inherits="Notification" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <table style="width:100%; font-weight: bold; height: 35px; font-size:x-large">
            <tr>
                <td align="center">Thông báo</td>
            </tr>
        </table>
        <br />
        <table style="width:770px; color: Black; margin-left:30px" runat=server id=tb_ThongBaoDen>
            <tr>
                <td colspan=2>
                    <fieldset>
                        <legend><b><span style="color: #0066FF;">Thông báo đến</span> (Hiện có <asp:Label ID="lbl_NumMess" runat=server ForeColor=Red></asp:Label> thông báo chưa đọc)</b></legend>
                        <asp:GridView ID="grv_ThongBaoDen" runat="server" AutoGenerateColumns="False" 
                            onrowediting="grv_ThongBaoDen_RowEditing" BorderStyle="None" 
                            AllowPaging="True" Width="750px" 
                            onpageindexchanging="grv_ThongBaoDen_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="id_thongbao">
                                    <ControlStyle BackColor="White" ForeColor="White" />
                                    <ItemStyle BackColor="White" ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ngaygoi" HeaderText="Ngày nhận"/>
                                <asp:ImageField DataImageUrlField="TrangThai" 
                                    DataImageUrlFormatString="~/Anh/{0}">
                                    <ControlStyle Height="25px" Width="40px" />
                                </asp:ImageField>
                                <asp:BoundField DataField="Tieude" HeaderText="Tiêu đề">
                                    <ControlStyle Width="400px" />
                                    <HeaderStyle Width="400px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Tennguoigoi" HeaderText="Người gởi"/>
                                <asp:CommandField CancelText="" DeleteText="" HeaderText="" InsertText="" 
                                    NewText="" SelectText="" ShowCancelButton="False" ShowEditButton="True" 
                                    UpdateText="" EditText="Chi tiết">
                                <ItemStyle ForeColor="#0000CC" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </td>
            </tr>
        </table>
        
        <br />
        
        <table style="width:770px; color: Black; margin-left:30px" runat=server id=tb_ThongBaoDi>
            <tr>
                <td colspan=2>
                    <fieldset>
                        <legend><b><span style="color: #0066FF;">Thông báo đi</span></b></legend>
                            <asp:GridView ID="grv_ThongBaoDi" runat="server" AutoGenerateColumns="False" 
                            onrowediting="grv_ThongBaoDi_RowEditing" BorderStyle="None" 
                            AllowPaging="True" Width="750px" 
                            onpageindexchanging="grv_ThongBaoDi_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="id_thongbao">
                                    <ControlStyle BackColor="White" ForeColor="White" />
                                    <ItemStyle BackColor="White" ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ngaygoi" HeaderText="Ngày gởi"/>
                                <asp:BoundField DataField="Tieude" HeaderText="Tiêu đề">
                                    <ControlStyle Width="460px" />
                                    <HeaderStyle Width="460px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Tennguoinhan" HeaderText="Người nhận"/>
                                <asp:CommandField CancelText="" DeleteText="" HeaderText="" InsertText="" 
                                    NewText="" SelectText="" ShowCancelButton="False" ShowEditButton="True" 
                                    UpdateText="" EditText="Chi tiết">
                                <ItemStyle ForeColor="#0000CC" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </td>
            </tr>
        </table>
        
        <table style="width:770px; color: Black; margin-left:30px" runat=server id=tb_noidung visible=false>
            <tr>
                <td>
                    <asp:Label ID="lbl_noidung" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align=center>
                    <asp:Button ID="btn_Back" runat="server" Text="Quay lại"  BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White"
                        onclick="btn_Back_Click"/>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

