<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WarrantyIntroduction.aspx.cs" Inherits="WarrantyIntroduction" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   
        <table style="width:800px" align="center">
        
        <tr style="font-weight: bold; height: 35px; font-size:x-large">
            <td style="text-align: center">
                Bảo Hành
                </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" Text="FPT Shop cam kết:" 
                    style="text-decoration: underline; font-weight: 700; color: #FF0000;"></asp:Label>
                &nbsp;<asp:Label ID="Label35" runat="server" Text="Tất cả hàng hóa được bán tại Hệ thống Bán lẻ FPT - FPT Shop trên toàn quốc đều là hàng chính hãng, có tem bảo hành."></asp:Label>
                <br />
            </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label36" runat="server" 
                    Text="Các sản phẩm do FPT cung cấp đều được bảo hành theo chỉ định của hãng sản xuất. Mọi thông tin về bảo hành (nếu có) Quý khách vui lòng liên hệ với Bộ phận Chăm sóc khách hàng của FPT Shop theo địa chỉ email: HaNTT17@fpt.com.vn"></asp:Label>
            </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label37" runat="server" 
                    Text="Địa chỉ các Trung tâm Bảo hành:" 
                    
                    style="font-weight: 700; text-decoration: underline; color: #FF0000; text-align: center;"></asp:Label></td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: center">
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/Anh/banner bao hanh moi.JPG" />
            </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left">
                <asp:Label ID="Label5" runat="server" 
                    Text="Thông báo về địa điểm bảo hành mới của FPT Shop:" 
                    style="font-weight: 700; "></asp:Label>&nbsp;<asp:Label ID="Label38" 
                    runat="server" Text="Tất cả các sản phẩm ( Máy tính xách tay, Điện thoại di động) do FPT phân phối sẽ được chính thức bảo hành tại FPT Shop."></asp:Label>
                <br /></td>
        </tr>
        
    </table><br />
    
    <table style="width:770px; border-collapse:collapse" align="center" border="1" bordercolor="Black">
       
        
        <tr style="color:Black">
            <td style="text-align: center" width="100px">
                <asp:Label ID="Label8" runat="server" style="font-weight: 700" Text="Tên hãng"></asp:Label>
            </td>
            <td style="text-align: center" width="550px">
                <asp:Label ID="Label9" runat="server" style="font-weight: 700" Text="Địa chỉ bảo hành"></asp:Label>
            </td>
            <td style="text-align: center" width="150px">
                <asp:Label ID="Label10" runat="server" style="font-weight: 700" Text="Mobile"></asp:Label>
            </td>
        </tr>
        

        <tr style="color:Black">
            <td style="text-align: center" width="100px">
                <asp:Image ID="Image2" runat="server" Height="50px" 
                    ImageUrl="~/Anh/logo motorola.jpg" Width="100px" />
            </td>
            <td style="text-align: left" width="550px">
                <asp:Label ID="Label60" runat="server" Text="118 Phan Chu Trinh, Q. Hải Châu"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label61" runat="server" Text="0511.356.5616"></asp:Label>
            </td>
        </tr>
        

        <tr style="color:Black">
            <td style="text-align: center" width="100px">
                <asp:Image ID="Image3" runat="server" Height="50px" 
                    ImageUrl="~/Anh/lenovo-logo.JPG" Width="100px" />
            </td>
            <td style="text-align: left" width="550px">
                <asp:Label ID="Label59" runat="server" Text="178 Trần Phú, Q. Hải Châu"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label62" runat="server" Text="0511.3562.6626"></asp:Label>
            </td>
        </tr>
        <tr style="color:Black">
            <td style="text-align: center" width="100px">
                <asp:Image ID="Image4" runat="server" Height="50px" 
                    ImageUrl="~/Anh/logo HTC Mobile_0.jpg" Width="100px" />
            </td>
            <td style="text-align: left" width="550px">
                <asp:Label ID="Label58" runat="server" Text="323 Lê Duẩn, Phường Tân Chính, Q. Thanh Khê"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                &nbsp;</td>
        </tr>
        <tr style="color:Black">
            <td style="text-align: center" width="100px" rowspan="2">
                <asp:Image ID="Image5" runat="server" Height="50px" 
                    ImageUrl="~/Anh/nokia_0.jpg" Width="100px" />
            </td>
            <td style="text-align: left" width="550px">
                <asp:Label ID="Label67" runat="server" 
                    Text="58-60 Nguyễn Văn Linh, Hải Châu"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label68" runat="server" 
                    Text="0511.3812.848"></asp:Label>
            </td>
        </tr>
        <tr style="color:Black">
            <td style="text-align: left" width="550px">
                <asp:Label ID="Label57" runat="server" 
                    Text="108 Điện Biên Phủ, P.Chính Gián, Q.Thanh Khê"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label64" runat="server" Text="0511.3771.222"></asp:Label>
            </td>
        </tr>
        <tr style="color:Black">
            <td style="text-align: center" width="100px">
                <asp:Image ID="Image6" runat="server" Height="50px" 
                    ImageUrl="~/Anh/logo samsung.JPG" Width="100px" />
            </td>
            <td style="text-align: left" width="550px">
                <asp:Label ID="Label56" runat="server" Text="53-55 Nguyễn Văn Linh, Q.Hải Châu"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label65" runat="server" Text="0511-3652359"></asp:Label>
            </td>
        </tr>
        <tr style="color:Black">
            <td  style="text-align: center" width="100px" rowspan="2">
                <asp:Image ID="Image7" runat="server" Height="50px" 
                    ImageUrl="~/Anh/logo dell_3.jpg" Width="100px" />
            </td>
            <td  style="text-align: left" width="550px">
                <asp:Label ID="Label52" runat="server" Text="336 Lê Duẩn"></asp:Label>
            </td>
            <td  style="text-align: right" width="150px">
                <asp:Label ID="Label53" runat="server" Text="0511.3562.666"></asp:Label>
            </td>
        </tr>
        <tr style="color:Black">
            <td  style="text-align: left" width="550px">
                <asp:Label ID="Label44" runat="server" Text="296 Nguyễn Hoàng, Q. Thanh Khê"></asp:Label>
            </td>
            <td  style="text-align: right" width="150px">
                <asp:Label ID="Label54" runat="server" Text="0511.3584.488"></asp:Label>
            </td>
        </tr>
        <tr style="color:Black">
            <td style="text-align: center" width="100px">
                <asp:Image ID="Image8" runat="server" Height="50px" 
                    ImageUrl="~/Anh/logo sony Ericsson_0.jpg" Width="100px" />
            </td>
            <td style="text-align: left" width="550px">
                <asp:Label ID="Label55" runat="server" Text="Số 5 Đặng Thai Mai, Q, Thanh Khê"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label66" runat="server" Text="0511.3888.177"></asp:Label>
            </td>
        </tr>
        

        </table>
     
   </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

