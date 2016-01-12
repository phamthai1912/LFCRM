<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ManagementUser.aspx.cs" Inherits="ManagementUser" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
        function TestPass(e,args)
        {
            if(args.Value.length>=3)
                args.IsValid=true;
            else
                args.IsValid=false;
        }
</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <table style="width:800px" align="center" >
        
        <tr style="font-weight: bold; height: 35px; font-size:x-large">
            <td colspan="4" style="text-align: center">
                Quản lý người dùng</td>
        </tr>
        
        
        <tr style="color:Black">
            <td style="text-align: left; width:150px"  >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Họ và Tên:<br /></td>
            <td style="text-align: left; width:200px"  >
                <asp:TextBox ID="txt_Fullname" runat="server" width="180px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txt_Fullname" ErrorMessage="*" ValidationGroup="Them"></asp:RequiredFieldValidator>
            </td>
            <td style="text-align: left; width:120px"  >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Địa chỉ:</td>
            <td style="text-align: left; width:200px" >
                <asp:TextBox ID="txt_Address" runat="server" width="180px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="txt_Address" ErrorMessage="*" ValidationGroup="Them"></asp:RequiredFieldValidator>
                <br />
            </td>
        </tr>
        
                 
        <tr style="color:Black">
            <td style="text-align: left; "   >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email:</td>
            <td style="text-align: left; " valign="top"  >
                <asp:TextBox ID="txt_Email" width="180px"  runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="txt_Email" ErrorMessage="*" ValidationGroup="Them"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txt_Email" ErrorMessage="*" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    ValidationGroup="Them"></asp:RegularExpressionValidator>
                
            </td>
            <td style="text-align: left; "   >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thành phố:</td>
            <td style="text-align: left; "   >
                <asp:DropDownList ID="ddl_City" runat="server" Width="180px">
                    <asp:ListItem Value="Đà Nẵng">Đà Nẵng</asp:ListItem>
                    <asp:ListItem Value="Hồ Chí Minh">Hồ Chí Minh</asp:ListItem>
                    <asp:ListItem Value="Hà Nội">Hà Nội</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
                  
        <tr style="color:Black">
            <td style="text-align: left; width:150px"  >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tên đăng nhập</td>
            <td style="text-align: left; width:200px" >
                <asp:TextBox ID="txt_User" runat="server" width="180px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="txt_User" ErrorMessage="*" ValidationGroup="Them"></asp:RequiredFieldValidator>
                <td style="text-align: left; width:120px" >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Điện thoại</td>
            <td style="text-align: left; width:200px" >
                <asp:TextBox ID="txt_Phone" runat="server" Width="180px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txt_Phone" ErrorMessage="*" ValidationGroup="Them"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" 
                    ControlToValidate="txt_Phone" ErrorMessage="*" MaximumValue="999999999999" 
                    MinimumValue="1" Type="Double" ValidationGroup="Them"></asp:RangeValidator>
                </td>
        </tr>
             
         <tr style="color:Black">
            <td style="text-align: left; width:150px" >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Mật khẩu:</td>
            <td style="text-align: left; width:200px" >
                <asp:TextBox ID="txt_Pass" runat="server" width="180px" TextMode="Password"></asp:TextBox>
                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ControlToValidate="txt_Pass" ErrorMessage="*" 
                    ClientValidationFunction="TestPass" ValidationGroup="Them"></asp:CustomValidator>
                <td style="text-align: left; width:120px" >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Quyền</td>
            <td style="text-align: left; width:200px" >
                <asp:DropDownList ID="ddl_Role" runat="server" width="180px" 
                    DataSourceID="SqlDataSource1" DataTextField="QuyenHan" 
                    DataValueField="ID_Quyen">
                </asp:DropDownList>
                
                
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                    SelectCommand="SELECT * FROM [PhanQuyen]" 
                    ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
                </td>
        </tr>          
        
        
                   
        <tr style="color:Black">
            <td style="text-align: center" valign="middle" colspan="4"  >
                <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        
                   
        <tr style="color:Black">
            <td style="text-align: center" valign="middle" colspan="4" >
                <asp:Button ID="btn_Insert" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Insert_Click" Text="Thêm" Height="26px" 
                    ValidationGroup="Them" />
                <asp:Button ID="btn_Update" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Update_Click" Text="Cập nhập" Visible="False" 
                    ValidationGroup="Them" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_Cancel" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Cancel_Click" Text="Hủy" Visible="False" />
                <br />
                
                <asp:Label ID="lbl_UserID" runat="server" style="color: #FFFFFF" 
                    Visible="False"></asp:Label>
                
            </td>
        </tr>
        
    </table>
    
    <table  align="center">
        <tr style="color:Black">
            <td style="text-align: center" valign="middle">
                
                <asp:GridView ID="grv_User" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" DataKeyNames="ID_NguoiDung" 
                    HorizontalAlign="Center" BackColor="#CCCCCC" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                    Font-Size="Medium" ForeColor="Black" CellSpacing="2" AllowSorting="True" 
                    onrowediting="grv_User_RowEditing" 
                    onrowcommand="grv_User_RowCommand" 
                    onpageindexchanging="grv_User_PageIndexChanging">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID_NguoiDung" HeaderText="Mã" 
                            InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="HoTen" HeaderText="Họ tên" />
                        <asp:BoundField DataField="TenDangNhap" HeaderText="Tên đăng nhập" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="QuyenHan" HeaderText="Quyền" />
                        <asp:CommandField CancelText="" DeleteText="" HeaderText="Sửa" InsertText="" 
                            NewText="" SelectText="" ShowCancelButton="False" ShowEditButton="True" 
                            UpdateText="">
                            <ItemStyle ForeColor="#0000CC" />
                        </asp:CommandField>
                        <asp:ButtonField CommandName="Del" HeaderText="Xóa" Text="Delete">
                            <ItemStyle ForeColor="#0000CC" />
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
    
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

