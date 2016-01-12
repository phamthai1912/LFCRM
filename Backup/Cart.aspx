<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" Title="Untitled Page" %>

<%@ Register src="Flash3.ascx" tagname="Flash3" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
        <table style="width:770px" align="center">
        <tr style="font-weight: bold; font-size:x-large">
            <td style="text-align: center" valign=middle>
                <asp:Label ID="lbl_Title" runat="server" Text="Giỏ hàng của bạn"></asp:Label></td>
        </tr>
        </table>

        <asp:Panel ID="Pn_Cart" runat="server" >
        <table style="width:770px" align="center">
             <tr>
                <td style="text-align: center">
                    <br />
                <asp:GridView ID="grv_Cart" runat="server" AutoGenerateColumns="False" AllowPaging=True
                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                    CellPadding="4" ShowFooter="True" CellSpacing="2" ForeColor="Black" 
                    HorizontalAlign="Center" onrowdeleting="grv_Cart_RowDeleting">
                    <FooterStyle BackColor="#CCCCCC" />
                    <EmptyDataRowStyle BackColor="Blue" Font-Bold="True" Font-Size="18pt" 
                        ForeColor="White" Height="50px" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="ID_MatHang" HeaderText="Mã Sản Phẩm" />
                        <asp:BoundField DataField="TenMatHang" HeaderText="Sản phẩm" >

                        </asp:BoundField>

                        <asp:BoundField DataField="DonGia" HeaderText="Đơn Giá (VNĐ)" 
                            DataFormatString="{0:N0}" />
                        
                        <asp:TemplateField FooterText="Tổng tiền : " HeaderText="Số lượng">
                            
                            <ItemTemplate >
                                <asp:TextBox ID="txtQuantity"  runat="server" Text='<%# Eval("SoLuong") %>' 
                                    Width="42px" Height="18px" ></asp:TextBox>
                                
                                <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                    ControlToValidate="txtQuantity" ErrorMessage="*" MaximumValue="1000" 
                                    MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300" Visible="False">Bạn chưa nhập số lượng</asp:Label>
                                
                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="ThanhTien" HeaderText="Thành tiền (VNĐ)" 
                            DataFormatString="{0:N0}">
                            <FooterStyle Font-Bold="True" ForeColor="Blue"  />
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="Sửa">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtn_Edit" runat="server" onclick="lbtn_Edit_Click">Sửa</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle ForeColor="#0000CC" />
                        </asp:TemplateField>
                        
                        <asp:CommandField ShowDeleteButton="True" DeleteText="Xóa" 
                            HeaderText="Xóa" >
                            <ItemStyle ForeColor="#0000CC" />
                        </asp:CommandField>
                    </Columns>
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                 </td>
            </tr>
            
        <tr>
            <td style=" text-align: center;" valign="middle">
                    <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300"></asp:Label>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Register.aspx" 
                        style="color: #0000CC" Visible="False">đây</asp:HyperLink>
            </td>
        </tr>
        
        
        <tr>
            <td style="font-size: 2pt; text-align: center;" valign="middle">
                <asp:Button ID="btn_DeleleAll" runat="server"  Text="Hủy tất cả" 
                     BackColor="#213943" 
                BorderStyle="Ridge" Font-Bold="True" ForeColor="White"
                    onclientclick="return confirm(&quot;Bạn có chắc không ?&quot;)" 
                    onclick="btn_DeleleAll_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_ContinueShopping" runat="server"  BackColor="#213943" 
                BorderStyle="Ridge" Font-Bold="True" ForeColor="White"
                    Text="Tiếp tục chọn hàng" Width="143px" PostBackUrl="~/Default.aspx" 
                    onclick="btn_ContinueShopping_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_Order" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                     Text="Đặt hàng" Width="83px" onclick="btn_Order_Click" />
            </td>
            </tr>

             <tr>
                 <td style="text-align: center" >
                    
                 </td>
             </tr>



        

        
    </table>
        </asp:Panel>
        
        <asp:Panel ID="Pn_Order" runat="server" Visible=false>
        
        <table style="width:600px; color:Black;" align="center" >
        
            <tr style="color:Black">
                <td style="text-align: center">
                    <br />
                    <asp:GridView ID="grv_Order" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" 
                        BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" 
                        ForeColor="Black" HorizontalAlign="Center" onrowdeleting="grv_Cart_RowDeleting" 
                        ShowFooter="True">
                        <FooterStyle BackColor="#CCCCCC" />
                        <EmptyDataRowStyle BackColor="Blue" Font-Bold="True" Font-Size="18pt" 
                            ForeColor="White" Height="50px" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="ID_MatHang" HeaderText="Mã Sản Phẩm" />
                            <asp:BoundField DataField="TenMatHang" HeaderText="Sản phẩm" />
                            <asp:BoundField DataField="DonGia" DataFormatString="{0:N0}" 
                                HeaderText="Đơn Giá (VNĐ)" />
                            <asp:TemplateField FooterText="Tổng tiền : " HeaderText="Số lượng">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Quantity" runat="server" Text='<%# Eval("SoLuong") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ThanhTien" DataFormatString="{0:N0}" 
                                HeaderText="Thành tiền (VNĐ)">
                                <FooterStyle Font-Bold="true" ForeColor="blue" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            
        </table>
        
        <table style="width:600px; color:Black;" align="center" 
             bordercolor="#0099CC" >

            <tr style="color:Black">
                <td colspan="2" 
                    
                    style="font-weight: bold; text-align: center; font-size:x-large; height:40px" 
                    valign="middle"> 
                    Vui lòng điền đầy đủ thông tin.</td>
            </tr>
            </table>
            
            <table style="width:700px; color:Black;" align="center" 
            bordercolor="#003300">
            
            <tr style="color:Black">
                <td style="width:300px" >

                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;User ID
            </td>
                <td style="width:400px" >
                    <asp:Label ID="lbl_UserID" runat="server" ></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td  >
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Họ tên người nhận
            </td>
                <td  >
                    <asp:Label ID="lbl_FullName" runat="server" ></asp:Label>
                </td>
            </tr>
            
            
            <tr>
                <td  >
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Địa chỉ người nhận
            <asp:Label ID="Label1" runat="server" Text="*" style="color: #FF0000"></asp:Label></td>
                <td  >
                    <asp:TextBox ID="txt_ShipAddress" runat="server" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txt_ShipAddress" ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            
            <tr>
                <td  >
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ngày đặt hàng
            </td>
                <td  >
                    <asp:TextBox ID="txt_OrderDate" runat="server" Width="150px" ReadOnly="True" 
                        style="background-color: #FFFFFF"></asp:TextBox>
         
                </td>
            </tr>
            
            
            
            <tr>
                <td >
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thành phố
                </td>
                <td >
                    <asp:DropDownList ID="ddl_City" runat="server" Width="150px">
                        <asp:ListItem Value="Đà Nẵng">Đà Nẵng</asp:ListItem>
                        <asp:ListItem Value="Hồ Chí Minh">Hồ Chí Minh</asp:ListItem>
                        <asp:ListItem Value="Hà Nội">Hà Nội</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
            
            
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Label ID="lbl_MessOrder" runat="server" ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
            
            
            
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="btn_SendOrder" runat="server" BackColor="#213943" 
                            BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                            onclick="btn_SendOrder_Click" Text="Đặt hàng" Width="90px" />
                    </td>
                </tr>
            
            
            
        </table>  
        
        </asp:Panel>
        
        <asp:Panel ID="pn_ThongBao" runat="server" Visible=false>
        <table style="width:770px" align="center">
             <tr>
                <td style="text-align: center; color:Black; font-size:large" height=60px>
                    <asp:Label ID="Label2" runat="server" Text="Đơn đặt hàng đã được gởi đi. " 
                        style="font-weight: 700" Font-Size=Large></asp:Label>
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        style="color: #0000CC; font-weight: 700;" NavigateUrl="~/Default.aspx"> Trở 
                    lại trang chủ.</asp:HyperLink>
                </td>
             </tr>
        </table>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

