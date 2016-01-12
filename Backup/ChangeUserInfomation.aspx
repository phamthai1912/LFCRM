<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangeUserInfomation.aspx.cs" Inherits="ChangeUserInfomation" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   
        <table style="width:770px" align="center">
        
        <tr style="font-weight: bold; height: 35px; font-size:x-large">
            <td style="text-align: center">
                Thông tin người dùng
                </td>
        </tr>
        <tr >
                <td style="text-align: left">
                    <asp:LinkButton ID="btn_ChangeInformation" runat="server" 
                        onclick="btn_ChangeInformation_Click">Thay đổi thông tin cá nhân</asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btn_ChangePass" runat="server" 
                        onclick="btn_ChangePass_Click">Thay đổi mật khẩu</asp:LinkButton>
                    
                </td>
            </tr>
        </table>
        
     <asp:Panel ID="pn_Thaydoithongtin" runat="server" Visible=true>
         <table style="width:440px" align="center" >            
                <tr>
                    <td align=left style="color:Black" width=200px>
                        
                        &nbsp;</td>
                    <td align=left style="color:Black">
                        
                        &nbsp;</td>
                </tr>
                
                <tr>
                    <td align="left" style="color:Black" width="200px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Họ tên người dùng</td>
                    <td align="left" style="color:Black">
                        <asp:TextBox ID="txt_HoTen" runat="server" Width="200px"></asp:TextBox>
                        <asp:Label ID="lbl_MessHoTen" runat="server" style="color: #FF0000"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td align=left style="color:Black" width=200px>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Địa chỉ</td>
                    <td align=left style="color:Black">
                        
                        <asp:TextBox ID="txt_DiaChi" runat="server" Width="200px"></asp:TextBox>
                        
                        <asp:Label ID="lbl_MessDiaChi" runat="server" style="color: #FF0000"></asp:Label>
                        
                    </td>
                </tr>  
                <tr>
                    <td align="left" style="color:Black" width="200px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thành phố</td>
                    <td align="left" style="color:Black">
                        <asp:DropDownList ID="ddl_City" runat="server" Width="200px">
                            <asp:ListItem Value="Đà Nẵng">Đà Nẵng</asp:ListItem>
                            <asp:ListItem Value="Hồ Chí Minh">Hồ Chí Minh</asp:ListItem>
                            <asp:ListItem Value="Hà Nội">Hà Nội</asp:ListItem>
                            <asp:ListItem Value="Huế">Huế</asp:ListItem>
                            <asp:ListItem Value="Quảng Trị">Quảng Trị</asp:ListItem>
                            <asp:ListItem Value="Quảng Bình">Quảng Bình</asp:ListItem>
                            <asp:ListItem Value="Quãng Ngãi">Quãng Ngãi</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color:Black" width="200px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Phone</td>
                    <td align="left" style="color:Black">
                        <asp:TextBox ID="txt_Phone" runat="server" Width="200px"></asp:TextBox>
                        <asp:Label ID="lbl_MessPhone" runat="server" style="color: #FF0000"></asp:Label>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" 
                            ControlToValidate="txt_Phone" ErrorMessage="*" MaximumValue="999999999999" 
                            MinimumValue="111" Type="Double"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color:Black" width="200px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tên đăng nhập</td>
                    <td align="left" style="color:Black">
                        <asp:TextBox ID="txt_TenDangNhap" runat="server" Width="200px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color:Black" width="200px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;E-mail</td>
                    <td align="left" style="color:Black">
                        <asp:TextBox ID="txt_Email" runat="server" Width="200px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="color:Black; text-align: center;" 
                         >
                        <asp:Button ID="btn_Update" runat="server" BackColor="#213943" 
                            BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                            onclick="btn_Update_Click" Text="Cập nhật" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="color:Black; text-align: center;" >
                        <asp:Label ID="lbl_Mess" runat="server" style="color: #FF0000"></asp:Label>
                    </td>
                </tr>
          </table>
     </asp:Panel>
     
     
     <asp:Panel ID="pn_ThaydoiPass" runat="server" Visible=false>
         <table style="width:500px" align="center" >            
                <tr>
                    <td align=left style="color:Black" width=200px>
                        
                        &nbsp;</td>
                    <td align="left" style="color:Black">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" style="color:Black" width="200px">
                        &nbsp;&nbsp;&nbsp;Nhập mật khẩu cũ</td>
                    <td align="left" style="color:Black; " 
                         >
                        <asp:TextBox ID="txt_OldPass" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lbl_MessOldPass" runat="server" style="color: #FF0000"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td align=left style="color:Black" width=200px>
                        &nbsp;&nbsp;&nbsp;Nhập mật khẩu mới</td>
                    <td align=left style="color:Black">
                        
                        <asp:TextBox ID="txt_NewPass" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                        
                        <asp:Label ID="lbl_MessNewPass" runat="server" style="color: #FF0000"></asp:Label>
                        
                    </td>
                </tr>  
                <tr>
                    <td align="left" style="color:Black" width="200px">
                        &nbsp;&nbsp;&nbsp;Xác nhận mật khẩu mới</td>
                    <td align="left" style="color:Black">
                        <asp:TextBox ID="txt_ConfirmNewPass" runat="server" Width="200px" 
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="color:Black; text-align: center;">
                        <asp:Label ID="lbl_Mess1" runat="server" style="color: #FF0000"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td align="left" colspan="2" style="color:Black; text-align: center;">
                        <asp:Button ID="btn_UpdatePass" runat="server" BackColor="#213943" 
                            BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                            onclick="btn_UpdatePass_Click" Text="Cập nhật mật khẩu" />
                    </td>
                </tr>
                
          </table>
     </asp:Panel>
  </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

