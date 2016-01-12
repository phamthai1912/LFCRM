<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ManagementOrder.aspx.cs" Inherits="ManagementOrder" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    
        <asp:Panel ID="pn_Title" runat="server">
        <table style="width:800px" align="center">
        
        <tr style="font-weight: bold; height: 35px; font-size:x-large ">
            <td style="text-align: center">
                <asp:Label ID="lbl_Title" runat="server" Text="Quản lý đơn hàng"></asp:Label>
                </td>
        </tr>
        
        <tr style="color:Black">
             <td style="text-align: left" >
                <asp:LinkButton ID="btn_ComeBack" runat="server" onclick="btn_ComeBack_Click" Visible="False">Trở lại trang trước
                </asp:LinkButton>
            </td>
            </tr>
        </table>
        </asp:Panel>
        
        
        
        
        
        
        <asp:Panel ID="pn_ManagementOrder" runat="server">
        <table style="width:800px" align="center">
  
            <tr style="color:Black">
                <td style="text-align: center; ">
                    Xem đặt hàng từ ngày:
                    <asp:TextBox ID="txt_StartDate" runat="server" AutoPostBack="True" 
                        ontextchanged="txt_StartDate_TextChanged"></asp:TextBox>
                    <cc1:CalendarExtender ID="cld_StartDate" runat="server" TargetControlID="txt_StartDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    &nbsp;&nbsp;&nbsp;
                    Đến ngày:
                    <asp:TextBox ID="txt_EndDate" runat="server" AutoPostBack="True"></asp:TextBox>
                    <cc1:CalendarExtender ID="cld_EndDate" runat="server" TargetControlID="txt_EndDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_View" runat="server" Text="Xem"  BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        onclick="btn_View_Click" />
                    </td>
            </tr>     
            <tr style="color:Black">
                <td style="text-align: center; ">
                
                    <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300"></asp:Label>
                
                </td>
            </tr>    
            <tr style="color:Black">
                <td style="text-align: center; ">
                    <asp:GridView ID="grv_Orders" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" 
                        BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" 
                        ForeColor="Black" HorizontalAlign="Center" 
                        onpageindexchanging="grv_Orders_PageIndexChanging" 
                        onrowcommand="grv_Orders_RowCommand">
                        <RowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ID_DonHang" HeaderText="Mã đơn hàng" />
                            <asp:BoundField DataField="HoTen" HeaderText="Khách hàng" />
                            <asp:BoundField DataField="NgayDatHang" HeaderText="Ngày đặt hàng" />
                            <asp:BoundField DataField="NgayNhan" HeaderText="Ngày nhận" />
                            <asp:BoundField DataField="TinhTrang" HeaderText="Tình trạng" />
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbn_Detail" runat="server" onclick="lbn_Detail_Click" 
                                        PostBackUrl='<%# Eval("ID_DonHang", "ManagementOrder.aspx?ID={0}") %> '>Chi 
                                    tiết</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="#0000CC" />
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="Del" HeaderText="Xóa" Text="Xóa">
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
        
        
        
        
        
        
        
        
             
        <asp:Panel ID="pn_OrderDetail" runat="server" Visible="False">               
        <table style="width:800px" align="center" id=tb_OrderDetail>
        
            <tr>
            <td style="text-align: center">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                    SelectCommand="SELECT * FROM [DonHang],[NguoiDung] WHERE ([ID_DonHang] = @ID_DonHang) AND NguoiDung.ID_NguoiDung=DonHang.ID_NguoiDung">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="ID_DonHang" QueryStringField="ID" 
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:FormView ID="fv_OrderDetail" runat="server" BackColor="White" 
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    DataKeyNames="ID_DonHang" DataSourceID="SqlDataSource1" ForeColor="Black" 
                    GridLines="Horizontal" HorizontalAlign="Center">
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <ItemTemplate>
                        <table align="center" style="width:400px">
                            <tr>
                                <td style="text-align: left; width:160px">
                                    Mã đơn hàng</td>
                                <td style="text-align: left; width:240px">
                                    <asp:TextBox ID="txt_OrderID" runat="server" Enabled="False" ReadOnly="True" 
                                        Text='<%# Eval("ID_DonHang") %>' Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Mã khách hàng</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txt_UserID" runat="server" Enabled="False" ReadOnly="True" 
                                        Text='<%# Bind("ID_NguoiDung") %>' Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Họ tên</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txt_FullName" runat="server" Enabled="False" ReadOnly="True" 
                                        Text='<%# Eval("HoTen") %>' Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Ngày đặt hàng</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txt_OrderDate" runat="server" Enabled="False" ReadOnly="True" 
                                        Text='<%# Bind("NgayDatHang") %>' Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Ngày nhận</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txt_RequireDate" runat="server" Text='<%# Bind("NgayNhan") %>'></asp:TextBox>
                                    <cc1:CalendarExtender ID="cldRequireDate" runat="server" Format="MM/dd/yyyy" 
                                        TargetControlID="txt_RequireDate">
                                    </cc1:CalendarExtender>
                                    <asp:Label ID="lbl_rq" runat="server" style="color: #FF0000"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Địa chỉ vận chuyển</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txt_ShipAddree" runat="server" Enabled="False" ReadOnly="True" 
                                        Text='<%# Bind("DiaChiGiaoHang") %>' TextMode="MultiLine" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Thành phố
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txt_ShipCity" runat="server" Enabled="False" ReadOnly="True" 
                                        Text='<%# Bind("ThanhPho") %>' Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Tình trạng</td>
                                <td style="text-align: left">
                                    <asp:Label ID="lbl_Status" runat="server" style="font-weight: 700" 
                                        Text='<%# Bind("TinhTrang") %>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <EditRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                </asp:FormView>
                </td>
        </tr>
        
            
            
            
            <tr style="color:Black">
                <td style="text-align: center; " valign="top">
                    <asp:GridView ID="grv_OrderDetail" runat="server" AutoGenerateColumns="False" 
                        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                        CellPadding="4" CellSpacing="2" DataKeyNames="ID_DonHang,ID_MatHang" 
                        DataSourceID="SqlDataSource2" ForeColor="Black" HorizontalAlign="Center" 
                        onrowcommand="grv_OrderDetail_RowCommand">
                        <RowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ID_MatHang" HeaderText="Mã sản phẩm" 
                                ReadOnly="True" />
                            <asp:BoundField DataField="TenMatHang" HeaderText="Sản phẩm" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Số lượng">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_SoLuong" runat="server" 
                                        Text='<%# Bind("SoLuong", "{0:N0}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_SoLuong" runat="server" Text='<%# Bind("SoLuong") %>' 
                                        Width="80px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txt_SoLuong" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" 
                                        ControlToValidate="txt_SoLuong" ErrorMessage="Nhập sai !!!" 
                                        MaximumValue="1000000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Đơn giá (VNĐ)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_DonGia" runat="server" 
                                        Text='<%# Eval("DonGia", "{0:N0}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="Warranty" HeaderText="Kích hoạt bảo hành" 
                                Text="Kích hoạt">
                                <ItemStyle ForeColor="#0000CC" />
                            </asp:ButtonField>
                            <asp:CommandField CancelText="Hủy" DeleteText="" EditText="Sửa" 
                                HeaderText="Sửa" InsertText="" NewText="" SelectText="" ShowEditButton="True" 
                                UpdateText="ok">
                                <ItemStyle ForeColor="#0000CC" />
                            </asp:CommandField>
                            <asp:ButtonField CommandName="Del" HeaderText="Xóa" Text="Xóa">
                                <ItemStyle ForeColor="Red" />
                            </asp:ButtonField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                        
                        SelectCommand="SELECT * FROM [ChiTietDonHang],[MatHang] WHERE ([ID_DonHang] = @ID_DonHang) AND MatHang.ID_MatHang = ChiTietDonHang.ID_MatHang" 
                        UpdateCommand="UPDATE [ChiTietDonHang] SET [SoLuong] = @SoLuong WHERE [ID_DonHang] = @ID_DonHang AND [ID_MatHang] = @ID_MatHang">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="ID_DonHang" QueryStringField="ID" 
                                Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="SoLuong" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr style="color:Black">
                <td style="text-align: center; ">
                    <asp:Label ID="lbl_Mess1" runat="server" ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
            <tr style="color:Black">
                <td style="text-align: center; ">
                    <asp:Button ID="btn_RequireExportStorage" runat="server" BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        onclick="btn_RequireExportStorage_Click" Text="Lập phiếu y/c xuất kho" />
                    &nbsp;
                    <asp:Button ID="btn_SaleBill" runat="server" BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        onclick="btn_SaleBill_Click" Text="Hóa đơn bán hàng" />
&nbsp;
                    <asp:Button ID="btn_Approve" runat="server" BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        onclick="btn_Approve_Click" Text="Duyệt" Width="54px" />
                    &nbsp; &nbsp;<asp:Button ID="btn_Cancel" runat="server" BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        onclick="btn_Cancel_Click" Text="Hủy" />
                &nbsp;
                </td>
            </tr>
        </table>
        
    </asp:Panel>
        
    
    
    
    
    
        <asp:Panel ID="pn_Warranty" runat="server" Visible=false>
           
        <table style="width:700px" align="center" ID="tb_Warranty">
        <tr style="color:Black">
                <td style="text-align: left" width=150px>
                    &nbsp;&nbsp;Mã bảo hành</td>
                <td style="text-align: left; " width=200px>
                    <asp:TextBox ID="txt_MaBH" runat="server" ReadOnly="True" Width="150px"></asp:TextBox>
                </td>
                <td style="text-align: left" width=150px>
                    Số Serial</td>
                <td style="text-align: left" width=200px>
                    <asp:TextBox ID="txt_Serial" runat="server" Width="180px"></asp:TextBox>
                    <asp:Label ID="lbl_Serial" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>

       <tr style="color:Black">
                <td style="text-align: left" width=150px>
                    &nbsp;&nbsp;Mã khách hàng</td>
                <td style="text-align: left; font-weight: 700;" width=200px>                
                    
                    <asp:TextBox ID="txt_MaKH" runat="server" ReadOnly="True" Width="150px"></asp:TextBox>
                    
                </td>
                <td style="text-align: left" width=150px>
                    Tên sản phẩm</td>
                <td style="text-align: left" width=200px>
                    <asp:TextBox ID="txt_Product" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>

                    </td>
            </tr>
        
        <tr style="color:Black">
                <td style="text-align: left" width=150px>
                    &nbsp;&nbsp;Ngày kích hoạt</td>
                <td style="text-align: left" width=200px>               
                      <asp:TextBox ID="txt_Begin" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>
                      <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_Begin" Format="MM/dd/yyyy">
                        </cc1:CalendarExtender>                
                </td>
                <td style="text-align: left" width=150px>
                    Bảo hành đến ngày</td>
                <td style="text-align: left" width=200px>
                    <asp:TextBox ID="txt_End" runat="server" Width="150px"></asp:TextBox>
                    <asp:Label ID="lbl_EndDate" runat="server" ForeColor="Red"></asp:Label>
                </td>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_End" Format="MM/dd/yyyy">
                        </cc1:CalendarExtender>
            </tr>
              
        <tr style="color:Black">
            <td style="text-align: center" colspan="4">
                <asp:Button ID="btn_Add" runat="server" BackColor="#213943" BorderStyle="Ridge" 
                    Font-Bold="True" ForeColor="White" onclick="btn_Add_Click" 
                    Text="Thêm mới" Height="26px" />
                &nbsp;<asp:Button ID="btn_Update" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Update_Click" Text="Cập nhật" Visible="False" 
                    style="height: 26px" />
            </td>
        </tr>
            
            <tr style="color:Black">
                <td colspan="4" style="text-align: center">
                    <asp:Label ID="lbl_ThongBao" runat="server" ForeColor="Red" Text=""></asp:Label>
                </td>
            </tr>
            
        </table>
        <table style="width:800px" align="center" >
             <tr >
            <td style="text-align: center" >
                
                <asp:GridView ID="grv_Warranty" runat="server" AutoGenerateColumns="False" 
                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                    CellPadding="4" CellSpacing="2" ForeColor="Black" HorizontalAlign="Center" 
                    style="color: #000000" onrowediting="grv_Warranty_RowEditing" 
                    AllowPaging="True" onpageindexchanging="grv_Warranty_PageIndexChanging" 
                    PageSize="15" Width="770px" onrowcommand="grv_Warranty_RowCommand">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID_BaoHanh" HeaderText="Mã bảo hành" />
                        <asp:BoundField DataField="ID_NguoiDung" HeaderText="Mã khách hàng" 
                            Visible="False" />
                        <asp:BoundField DataField="HoTen" HeaderText="Họ tên" Visible="False" />
                        <asp:BoundField DataField="TenMatHang" HeaderText="Sản phẩm" />
                        <asp:BoundField DataField="Serial" HeaderText="Số serial" />
                        <asp:BoundField DataField="NgayKichHoat" HeaderText="Ngày kích hoạt" />
                        <asp:BoundField DataField="NgayHetHan" HeaderText="Ngày hết hạn" />
                        <asp:CommandField EditText="Chọn" HeaderText="Sửa" ShowEditButton="True">
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
    
    
        <asp:Panel ID="Pn_Form" runat="server" Visible=false >
            <table style="width:800px" align="center" >            
                <tr>
                    <td align=center >
                        <asp:Label ID="lbl_Form" runat="server" ></asp:Label>
                    </td>
                </tr> 
            </table>
        </asp:Panel>
        
        
        <asp:Panel ID="Pn_btnPrint" runat="server" Visible=false>   
            <table style="width:800px" align="center" >
                <tr>
            <td style="text-align: center">
                <asp:Label ID="lbl_Mess2" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
          </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btn_Print" runat="server" BackColor="#213943" 
                            BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                            onclick="btn_Print_Click" Text="In phiếu" />
                        &nbsp;
                        <asp:Button ID="btn_Send" runat="server" BackColor="#213943" 
                            BorderStyle="Ridge" Font-Bold="True" ForeColor="White" onclick="btn_Send_Click" 
                            Text="Gửi phiếu" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

