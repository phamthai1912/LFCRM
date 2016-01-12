<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
        function TestPass(e,args)
        {
            if(args.Value.length>=1)
                args.IsValid=true;
            else
                args.IsValid=false;
        }
</script>



<asp:UpdatePanel ID="upRegister" runat="server">
<ContentTemplate>

<table style="color: black; width: 100%">    
    <tr style="font-weight: bold; height: 35px; font-size:x-large">
        <td style="color:White;" align="center" colspan="2">
            Đăng ký tài khoản 
        </td>
    </tr>
    <tr>
        <td style="color: black; font-weight: bold; height: 35px; font-size:large" align="center">
            <br>
                Thông tin tài khoản
            </td>
        <td>
        
        </td>
    </tr>
    <tr style=" height: 35px">
        <td>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tên đăng nhập:
            <asp:Label ID="Label1" runat="server" Text="*" style="color: #FF0000"></asp:Label>
        </td>
        
        <td>
            <asp:TextBox ID="txt_User" Width="250px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txt_User" ErrorMessage="Vui lòng nhập tên đăng nhập" 
                ValidationGroup="Register"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr style=" height: 35px">
        <td>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Mật khẩu: 
            <asp:Label ID="Label2" runat="server" Text="*" style="color: #FF0000"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txt_Pass" Width="250px" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="txt_Pass" ErrorMessage="Vui lòng nhập mật khẩu" 
                ValidationGroup="Register"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr style=" height: 35px">
        <td >
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Confirm Password  
            <asp:Label ID="Label3" runat="server" Text="*" style="color: #FF0000"></asp:Label>
        </td>
        <td >
            <asp:TextBox ID="txt_ConPass" Width="250px" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="txt_Pass" ErrorMessage="Xin xác nhận lại mật khẩu" 
                ValidationGroup="Register"></asp:RequiredFieldValidator>
                <br />
            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                ControlToCompare="txt_Pass" ControlToValidate="txt_ConPass" 
                ErrorMessage="Xác nhận mật khẩu không đúng!" ValidationGroup="Register"></asp:CompareValidator>
        </td>
    </tr>
    <tr style=" height: 35px">
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;E-mail  
            <asp:Label ID="Label4" runat="server" Text="*" style="color: #FF0000"></asp:Label>
        </td>
        <td >
            <asp:TextBox ID="txt_Email" Width="250px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                 ControlToValidate="txt_Email" 
                ErrorMessage="Vui lòng nhập Email" ValidationGroup="Register"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td >
            </td>
         <td >
            </td>   
    </tr>
    <tr style="color: #000000; font-weight: bold; height: 35px; font-size:large">
        <td align="center">
            Thông tin người dùng
        </td>
        <td >
            </td>
    </tr>
    <tr style=" height: 35px">
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Họ và Tên:
            <asp:Label ID="Label5" runat="server" Text="*" style="color: #FF0000"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="txt_FullName" Width="250px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="txt_FullName" ErrorMessage="Vui lòng nhập họ tên" 
                ValidationGroup="Register"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr style=" height: 35px">
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Địa chỉ: 
            <asp:Label ID="Label6" runat="server" Text="*" style="color: #FF0000"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="txt_Address" Width="250px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txt_Address" ErrorMessage="Vui lòng nhập địa chỉ" 
                ValidationGroup="Register"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr style=" height: 35px">
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thành phố:
            <asp:Label ID="Label7" runat="server" Text="*" style="color: #FF0000"></asp:Label>
            </td>
        <td>
            <asp:DropDownList ID="ddl_City" Width="250px" runat="server">
                <asp:ListItem Value="0">Chọn</asp:ListItem>
                <asp:ListItem Value="1">Đà Nẵng</asp:ListItem>
                <asp:ListItem Value="2">Hồ Chí Minh</asp:ListItem>
                <asp:ListItem Value="3">Hà Nội</asp:ListItem>
                <asp:ListItem Value="4">Huế</asp:ListItem>
                <asp:ListItem Value="5">Quảng Trị</asp:ListItem>
                <asp:ListItem Value="6">Quảng Bình</asp:ListItem>
                <asp:ListItem Value="7">Quãng Ngãi</asp:ListItem>
            </asp:DropDownList>
        &nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" 
                ControlToValidate="ddl_City" ErrorMessage="Bạn chưa chọn thành phố" 
                Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
        </td>
    </tr>
    <tr style=" height: 35px">
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Điện thoại:
            <asp:Label ID="Label8" runat="server" Text="*" style="color: #FF0000"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="txt_Phone" Width="250px" runat="server"></asp:TextBox>
            
            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                ControlToValidate="txt_Phone" ErrorMessage="Vui lòng nhập địa chỉ" 
                MaximumValue="10000000000" MinimumValue="1" Type="Double"></asp:RangeValidator>
            
            </td>
    </tr>
    <tr >
        <td >
        </td>
        <td>
            <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align: right">
            &nbsp;</td>
        <td>
            <asp:Button ID="btnRegister" runat="server" BackColor="#213943" 
                BorderStyle="Ridge" Font-Bold="True" ForeColor="White" Text="Đăng ký" 
                onclick="btnRegister_Click" ValidationGroup="Register"/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
</table>
 
</ContentTemplate>

</asp:UpdatePanel>
 
</asp:Content>

