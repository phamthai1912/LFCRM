<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ManagementCatalogue.aspx.cs" Inherits="ManagerCatalogue" Title="Untitled Page" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="upCatalogue" runat="server">
   <ContentTemplate>
   
        <table style="width:770px" align="center">
        
        <tr style="font-weight: bold; height: 35px; font-size:x-large">
            <td colspan="2" style="text-align: center">
                Quản lý loại hàng
                </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: right" width=300px>
                <br />
                Tên loại hàng :&nbsp;&nbsp; </td>
            <td style="text-align: left">
                <asp:Label ID="lbl_Code" runat="server" ForeColor="#FF3300" 
                    style="color: #FF0000" Visible="False"></asp:Label>
                <br />
                <asp:TextBox ID="txt_CatalogueName" Width="150px" runat="server"></asp:TextBox>
                <asp:Label ID="lbl_LoaiHang" runat="server" ForeColor="#FF3300"></asp:Label>
                &nbsp;&nbsp;
                <asp:Button ID="btn_Insert" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Insert_Click" Text="Thêm" />
                <asp:Button ID="btn_Update" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Update_Click" Text="Sửa" Visible="False" />
            </td>
        </tr>
        
         <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
         
         <tr>
            <td colspan="2" style="text-align: center">
                <asp:GridView ID="grv_Catalogue" runat="server" style="color: #000000" 
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                    CellPadding="4" CellSpacing="2" DataKeyNames="ID_LoaiHang"  HorizontalAlign="Center" 
                    onrowcommand="grv_Catalogue_RowCommand" PageSize="20" 
                    onpageindexchanging="grv_Catalogue_PageIndexChanging" Width="700px" 
                    onrowediting="grv_Catalogue_RowEditing" 
                    onselectedindexchanged="grv_Catalogue_SelectedIndexChanged">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID_LoaiHang" HeaderText="Mã loại hàng" 
                            InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="LoaiHang" HeaderText="Loại hàng" />
                        <asp:CommandField DeleteText="" HeaderText="Sửa" ShowEditButton="True" 
                            UpdateText="" EditText="Chọn" CancelText="" InsertText="" NewText="" 
                            SelectText="">
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

