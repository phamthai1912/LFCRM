<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ManagementProvider.aspx.cs" Inherits="ManagementProvider" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   
        <table style="width:600px" align="center">
        
        <tr style="font-weight: bold;  font-size:x-large">
            <td colspan="2" style="text-align: center">
                Nhà cung cấp
                </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left" width="250px">
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Tên nhà cung cấp :</td>
            <td style="text-align: left" >
                <br />
                <asp:TextBox ID="txt_ProviderName" Width="150px" runat="server"></asp:TextBox>
                <asp:Label ID="lbl_Mess0" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
         
        <tr style="color:Black">
            <td style="text-align: left" >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; E-Mail :</td>
            <td style="text-align: left"  >
                <asp:TextBox ID="txt_Email" Width="150px" runat="server"></asp:TextBox>
                <asp:Label ID="lbl_Mess1" runat="server" ForeColor="#FF3300"></asp:Label>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txt_Email" ErrorMessage="E-mail không đúng" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
         
        
        <tr style="color:Black">
            <td style="text-align: left" >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Địa chỉ :</td>
            <td style="text-align: left" >
                <asp:TextBox ID="txt_Address" Width="150px" runat="server"></asp:TextBox>
                <asp:Label ID="lbl_Mess2" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
         
        <tr style="color:Black">
            <td style="text-align: left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Điện thoại :</td>
            <td style="text-align: left" >
                <asp:TextBox ID="txt_Phone" Width="150px" runat="server"></asp:TextBox>
                <asp:Label ID="lbl_Mess3" runat="server" ForeColor="#FF3300"></asp:Label>
                <asp:RangeValidator ID="RangeValidator3" runat="server" 
                    ControlToValidate="txt_Phone" ErrorMessage="Chỉ nhập số" 
                    MaximumValue="100000000000" MinimumValue="1" Type="Double"></asp:RangeValidator>
            </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fax: </td>
            <td style="text-align: left" >
                <asp:TextBox ID="txt_Fax" Width="150px" runat="server"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator4" runat="server" 
                    ControlToValidate="txt_Fax" ErrorMessage="Chỉ nhập số" 
                    MaximumValue="100000000000" MinimumValue="1" Type="Double"></asp:RangeValidator>
            </td>
        </tr>
         
        
         <tr>
            <td colspan="2" style="text-align: center" class="style1">
                <asp:Button ID="btn_Insert" runat="server" BackColor="#213943" 
                BorderStyle="Ridge" Font-Bold="True" ForeColor="White" Text="Thêm" onclick="btn_Insert_Click" 
                     />
                <asp:Button ID="btn_Update" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Update_Click" Text="Sửa" Visible="False" />
            </td>
        </tr>
         
         <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300"></asp:Label>
                <asp:Label ID="lbl_Code" runat="server" ForeColor="#FF3300" Visible="False"></asp:Label>
             </td>
        </tr>
        
    </table>
    
    <table style="width:800px" align="center">
        
         <tr>
            <td  style="text-align: center">
                <asp:GridView ID="grv_Provider" runat="server" BackColor="#CCCCCC" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                    CellSpacing="2" ForeColor="Black" HorizontalAlign="Center" 
                    style="color: #000000" AllowPaging="True" AllowSorting="True" 
                    AutoGenerateColumns="False" DataKeyNames="ID_NhaCungCap" onrowcommand="grv_Provider_RowCommand" 
                    onpageindexchanging="grv_Provider_PageIndexChanging" PageSize="15" 
                    Width="770px" onrowediting="grv_Provider_RowEditing">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID_NhaCungCap" HeaderText="Mã" InsertVisible="False" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="TenNhaCungCap" HeaderText="Nhà cung cấp" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="Fax" HeaderText="Fax" />
                        <asp:CommandField DeleteText="" HeaderText="Sửa" InsertText="" NewText="" 
                            SelectText="" ShowEditButton="True" UpdateText="" EditText="Chọn">
                            <ItemStyle ForeColor="#0000CC" />
                        </asp:CommandField>
                        <asp:ButtonField CommandName="del" HeaderText="Xóa" Text="Xóa">
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

