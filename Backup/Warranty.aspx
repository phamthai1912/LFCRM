<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="Warranty.aspx.cs" Inherits="Warranty" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     
        <table style="width:770px" align="center" >
        
        <tr style="font-weight: bold; height: 35px; font-size:x-large">
            <td style="text-align: center" >
                Quản lý bảo hành
                </td>
        </tr>
            <tr >
                <td style="text-align: left">
                    <asp:LinkButton ID="btn_Back" runat="server" onclick="btn_Back_Click" 
                        Visible="False">Trở về trước</asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="btn_XuLyPhieuNhan" runat="server" 
                        onclick="btn_XuLyPhieuNhan_Click">Xử lý phiếu nhận</asp:LinkButton>
                    
                </td>
            </tr>
           </table>
           
           <asp:Panel ID="pn_Xulyphieunhan" runat="server" Visible=false>
                <table style="width:800px" align="center" >            
                <tr>
                    <td align=center style="color:Black">
                        Chọn phiếu nhận số : 
                        <asp:DropDownList ID="ddl_MaBH" runat="server" Width="100px" 
                            AutoPostBack="True" DataSourceID="SqlDataSource1" 
                            DataTextField="ID_PhieuBaoHanh" DataValueField="ID_PhieuBaoHanh">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                            SelectCommand="SELECT [ID_PhieuBaoHanh] FROM [PhieuNhanBaoHanh] ORDER BY [ID_PhieuBaoHanh] DESC">
                        </asp:SqlDataSource>
                        &nbsp;&nbsp;
                        <asp:Button ID="btn_Xem" runat="server" BackColor="#213943" 
                            BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                            Text="Xem" onclick="btn_Xem_Click" />                       
                        &nbsp;
                        <asp:Button ID="btn_Duyet" runat="server" BackColor="#213943" 
                            BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                            onclick="btn_Duyet_Click" Text="Duyệt" Visible="False" />
                        <asp:Button ID="btn_Huy" runat="server" BackColor="#213943" BorderStyle="Ridge" 
                            Font-Bold="True" ForeColor="White" onclick="btn_Huy_Click" Text="Hủy" 
                            Visible="False" />
                    </td>
                </tr> 
                    <tr>
                        <td align="center"  style="color:Black">
                            <asp:Label ID="lbl_Mess2" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="color:Black">
                            <asp:GridView ID="grv_XuLyPhieuNhan" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" 
                                BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" 
                                ForeColor="Black" HorizontalAlign="Center" PageSize="15" style="color: #000000" 
                                Width="790px">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="ID_PhieuBaoHanh" HeaderText="Mã phiếu nhận" />
                                    <asp:BoundField DataField="TenMatHang" HeaderText="Sản phẩm" />
                                    <asp:BoundField DataField="Serial" HeaderText="Số serial" />
                                    <asp:BoundField DataField="NgayNhan" HeaderText="Ngày nhận" />
                                    <asp:BoundField DataField="NgayTra" HeaderText="Ngày trả" />
                                    <asp:BoundField DataField="GhiChu" HeaderText="Tình trạng" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
           </asp:Panel>
           
           
           
           
           
         <asp:Panel ID="pn_FindForm" runat="server" >
           
        <table style="width:770px" align="center" >
            <tr style="color:Black">
                <td style="text-align: center">Nhập số Serial cần tìm :
                    <asp:TextBox ID="txt_FindSerial" runat="server" Width="180px" 
                        AutoPostBack="True"></asp:TextBox>
                    <asp:AutoCompleteExtender runat="server" 
                    ID="AutoCompleteExtender2" 
                    TargetControlID="txt_FindSerial"
                    ServicePath="AutoComplete.asmx" 
                    ServiceMethod="GetCompletionListSerial"
                    MinimumPrefixLength="1" 
                    CompletionInterval="500"
                    EnableCaching="true"
                    CompletionSetCount="5">
                </asp:AutoCompleteExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="txt_FindSerial" ErrorMessage="*" ValidationGroup="BH"></asp:RequiredFieldValidator>
                    &nbsp;<asp:Label ID="lbl_MessSerial" runat="server" ForeColor="Red"></asp:Label>
                <asp:Button ID="btn_View" runat="server" BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" onclick="btn_View_Click" 
                        Text="Xem" ValidationGroup="BH" />
                    &nbsp;
                    <br />
                </td>
            </tr>
            <tr style="color:Black">
                <td style="text-align: center">
                    <asp:Label ID="lbl_Mess" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr style="color:Black">
                <td style="text-align: center">
                    <asp:GridView ID="grv_FindWarranty" runat="server" AutoGenerateColumns="False" 
                        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                        CellPadding="4" CellSpacing="2" ForeColor="Black" HorizontalAlign="Center" 
                        style="color: #000000">
                        <RowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ID_BaoHanh" HeaderText="Mã bảo hành" />
                            <asp:BoundField DataField="ID_NguoiDung" HeaderText="Mã khách hàng" />
                            <asp:BoundField DataField="HoTen" HeaderText="Họ tên" />
                            <asp:BoundField DataField="TenMatHang" HeaderText="Sản phẩm" />
                            <asp:BoundField DataField="Serial" HeaderText="Số serial" />
                            <asp:BoundField DataField="NgayKichHoat" HeaderText="Ngày kích hoạt" />
                            <asp:BoundField DataField="NgayHetHan" HeaderText="Ngày hết hạn" />
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr style="color:Black">
                <td style="text-align: center">
                    <asp:Button ID="btn_LapHDBH" runat="server" BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        Text="Lập phiếu nhận bảo hành" Visible="False" 
                        onclick="btn_LapHDBH_Click" />
                </td>
            </tr>
        </table>
        
        </asp:Panel>
        
        
        
        
         <asp:Panel ID="pn_LapPhieuNhap" runat="server" Visible=false>
            
            <table style="width:600px" align="center" >
        
               <tr>
                 <td style="text-align: center; color:Black"  colspan="4" >
                        <hr /></td>
             </tr>
             
                <tr>
                    <td style="text-align: left; color:Black" width="100px">
                        - Ngày nhận
                    </td>
                    <td style="text-align: left; color:Black">
                        <asp:TextBox ID="txt_NgayNhan" runat="server" ReadOnly="True"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                            TargetControlID="txt_NgayNhan">
                        </asp:CalendarExtender>
                    </td>
                    <td style="text-align: left; color:Black" width="100px">
                        - Ngày trả
                    </td>
                    <td style="text-align: left; color:Black">
                        <asp:TextBox ID="txt_NgayTra" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                            TargetControlID="txt_NgayTra">
                        </asp:CalendarExtender>
                        <asp:Label ID="lbl_Mess0" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
             
                <tr>
                    <td colspan="4" style="text-align: center; color:Black" >
                        <asp:Label ID="lbl_Mess1" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="lbl_MaPhieuNhan" runat="server" ForeColor="Red" 
                            style="color: #FF0000" Visible="False"></asp:Label>
                        </td>
                </tr>
             
                <tr>
                    <td colspan="4" style="text-align: center; color:Black">
                        <asp:Button ID="btn_Them" runat="server" BackColor="#213943" 
                            BorderStyle="Ridge" Font-Bold="True" ForeColor="White" onclick="btn_Them_Click" 
                            Text="Thêm" Width="70px" />
                        &nbsp;
                        <asp:Button ID="btn_Sua" runat="server" BackColor="#213943" BorderStyle="Ridge" 
                            Font-Bold="True" ForeColor="White" onclick="btn_Sua_Click" Text="Sửa" 
                            Visible="False" Width="70px" />
                    </td>
                </tr>
             
           </table>
           
           <table style="width:800px" align="center" >
               <tr>
                 <td style="text-align: center">
                        
                     <asp:GridView ID="grv_PhieuNhanBH" runat="server" style="color: #000000" 
                         AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" 
                         BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" 
                         ForeColor="Black" HorizontalAlign="Center" Width="790px" 
                         AllowPaging="True" onpageindexchanging="grv_PhieuNhanBH_PageIndexChanging" 
                         onrowcommand="grv_PhieuNhanBH_RowCommand" 
                         onrowediting="grv_PhieuNhanBH_RowEditing" PageSize="15">
                         <RowStyle BackColor="White" />
                         <Columns>
                             <asp:BoundField DataField="ID_PhieuBaoHanh" HeaderText="Mã phiếu nhận" />
                             <asp:BoundField DataField="TenMatHang" HeaderText="Sản phẩm" />
                             <asp:BoundField DataField="Serial" HeaderText="Số serial" />
                             <asp:BoundField DataField="NgayNhan" HeaderText="Ngày nhận" />
                             <asp:BoundField DataField="NgayTra" HeaderText="Ngày trả" />
                             <asp:BoundField DataField="GhiChu" HeaderText="Tình trạng" />
                             <asp:CommandField CancelText="" DeleteText="" EditText="Chọn" HeaderText="Sửa" 
                                 InsertText="" NewText="" SelectText="" ShowEditButton="True" UpdateText="">
                                 <ItemStyle ForeColor="#0000CC" />
                             </asp:CommandField>
                             <asp:ButtonField CommandName="del" HeaderText="Xóa" Text="Xóa">
                                 <ItemStyle ForeColor="Red" />
                             </asp:ButtonField>
                         </Columns>
                         <FooterStyle BackColor="#CCCCCC" />
                         <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                         <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                         <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                     </asp:GridView>
                        
                </td>
               </tr>
           </table>
         </asp:Panel>

    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

