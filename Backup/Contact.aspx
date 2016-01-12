<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel runat="server">
   <ContentTemplate>
   
        <table style="width:800px" align="center">
        
        <tr style="font-weight: bold; height: 35px; font-size:x-large">
            <td style="text-align: center">
                Liên Hệ
                </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" Text="Bạn cần:" 
                    style="text-decoration: underline; font-weight: 700; color: #FF0000;"></asp:Label>
            </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label2" runat="server" 
                    Text="Hỏi thông tin cụ thể về sản phẩm: Vui lòng liên lạc với Phụ trách sản phẩm." 
                    style="font-weight: 700"></asp:Label></td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label3" runat="server" 
                    Text="Đặt hàng nhanh chóng: Vui lòng hiên lạc với Trưởng Trung tâm Bán lẻ gần nơi bạn ở nhất." 
                    style="font-weight: 700"></asp:Label></td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label4" runat="server" 
                    Text="Liên hệ với Bộ phận Chăm sóc khách hàng của Công ty TNHH Bán lẻ FPT– Email:  HaNTT17@fpt.com.vn" 
                    style="font-weight: 700"></asp:Label></td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label5" runat="server" 
                    Text="Hãy liên lạc với chúng tôi ngay để được phục vụ tốt nhất!!!" 
                    style="font-weight: 700; font-style: italic;"></asp:Label><br /></td>
        </tr>
        
    </table><br />
    
    <table style="width:770px; border-collapse:collapse" align="center" border="1" bordercolor="Black">
        
        <tr style="color:Black">
            <td style="text-align: center" colspan="4">
                <asp:Label ID="Label6" runat="server" 
                    Text="DANH SÁCH CÁN BỘ QUẢN LÝ KHỐI KINH DOANH" 
                    style="font-weight: 700; color: #FF0000; font-size: x-large;"></asp:Label>
            </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: center" width="130px">
                <asp:Label ID="Label8" runat="server" style="font-weight: 700" Text="Họ tên"></asp:Label>
            </td>
            <td style="text-align: center" width="470px">
                <asp:Label ID="Label9" runat="server" style="font-weight: 700" Text="Chức danh"></asp:Label>
            </td>
            <td style="text-align: center" width="100px">
                <asp:Label ID="Label10" runat="server" style="font-weight: 700" Text="Mobile"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label11" runat="server" style="font-weight: 700" Text="Email"></asp:Label>
            </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left" colspan="2">
                <asp:Label ID="Label7" runat="server" style="font-weight: 700; color: #FF0000;" 
                    Text="FPT Shop - Số 10 Nguyễn Văn Linh, Quận Hải Châu, Đà Nẵng"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label12" runat="server" 
                    style="text-decoration: underline; color: #0000CC;" Text="0511.355.2666"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label13" runat="server" 
                    style="color: #0000CC; text-decoration: underline;" Text="Fax:  0511.358.2982"></asp:Label>
            </td>
        </tr>
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label20" runat="server" Text="Nguyễn Thị Thái Hòa"></asp:Label>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label21" runat="server" Text="Trưởng Trung tâm Bán lẻ"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label14" runat="server" Text="0905.072.299"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label15" runat="server" Text="HoaNTT3@fpt.com.vn"></asp:Label>
            </td>
        </tr>
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label19" runat="server" Text="Nguyễn Thị Diệu"></asp:Label>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label18" runat="server" Text="Trưởng ngành hàng Mobile (Nokia, HTC, Sony) và Phụ kiện"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label17" runat="server" Text="0936.979.555"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label16" runat="server" Text="DieuNT@fpt.com.vn"></asp:Label>
            </td>
        </tr>
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label22" runat="server" Text="Hoàng Bảo Châu"></asp:Label>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label24" runat="server" Text="Trưởng Ngành hàng Mobile"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label27" runat="server" Text="0905.111.373"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label28" runat="server" Text="ChauHB@fpt.com.vn"></asp:Label>
            </td>
        </tr>
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label23" runat="server" Text="Trần Văn Quý"></asp:Label>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label25" runat="server" Text="Trưởng Ngành hàng Máy tính"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label26" runat="server" Text="0905.644.442"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label29" runat="server" Text="QuyTV@fpt.com.vn"></asp:Label>
            </td>
        </tr>
        </table>
     
       <table style="width:800px" align="center">
        
        <tr style="color:Black">
            <td style="text-align: left" height=50px>
                <asp:Label ID="Label34" runat="server" 
                    Text="FPT Shop – Luôn nỗ lực vì sự hài lòng của khách hàng!" 
                    style="font-weight: 700; font-style: italic; font-size: x-large;"></asp:Label><br /></td>
        </tr>
        
    </table>
   </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

