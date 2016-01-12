<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ManagementProduct.aspx.cs" Inherits="ManagementProduct" Title="Untitled Page" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <table>
        
        <tr style="font-weight: bold; height: 35px; font-size:x-large">
            <td colspan="4" style="text-align: center">
                Quản lý mặt hàng
            </td>
        </tr>
        
        
        <tr style="color:Black">
            <td style="text-align: left; width:150px; height: 30px" valign="top" >
                <br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Nhà sản xuất<br />
                <br /></td>
            <td style="text-align: left; width:200px" valign="top" >
                <br /><asp:DropDownList ID="ddl_Production" runat="server" width="180px"
                    DataSourceID="SqlDataSource2" DataTextField="NhaSanXuat" 
                    DataValueField="ID_NhaSanXuat">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                    SelectCommand="SELECT * FROM [NhaSanXuat]"></asp:SqlDataSource>
                <br />
                
            </td>
            <td style="text-align: left; width:120px" valign="top" >
                <br />&nbsp;Loại hàng<br /></td>
            <td style="text-align: left; width:200px" valign="top" >
                <br />
                <asp:DropDownList ID="ddl_Catalogue" runat="server" width="180px"
                    DataSourceID="SqlDataSource3" DataTextField="LoaiHang" 
                    DataValueField="ID_LoaiHang">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                    SelectCommand="SELECT * FROM [LoaiHang]"></asp:SqlDataSource>
            </td>
        </tr>
        
                 
        <tr style="color:Black">
            <td style="text-align: left; width:150px; height:30px" valign="top" >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ảnh</td>
            <td style="text-align: left; width:200px" valign="top" >
                <asp:FileUpload ID="Fup_HinhAnh" runat="server" />
            </td>
            <td style="text-align: left; width:120px" valign="top" >
                &nbsp;Tên mặt hàng:</td>
            <td style="text-align: left; width:200px" valign="top" >
                <asp:TextBox ID="txt_ProductName" width="150px"  runat="server" 
                    ontextchanged="txt_ProductName_TextChanged" AutoPostBack="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txt_ProductName" ErrorMessage="*" 
                    ValidationGroup="ProductManage"></asp:RequiredFieldValidator>
                <asp:Label ID="lbl_Product" runat="server" Text=""></asp:Label>
            </td>
        </tr>
                   
        <tr style="color:Black">
            <td style="text-align: left; width:150px" valign="top" >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Mô tả</td>
            <td style="text-align: left" colspan="3" valign="top" >
                <CKEditor:CKEditorControl ID="txt_Description" runat="server" ToolbarFull="Source|-|Save|NewPage|Preview|-|Templates
                    Cut|Copy|Paste|PasteText|PasteFromWord|-|Print|SpellChecker|Scayt
                    Undo|Redo|-|Find|Replace|-|SelectAll|RemoveFormat
                    /
                    Bold|Italic|Underline|Strike|NumberedList|BulletedList|-|Outdent|Indent|Blockquote|CreateDiv
                    JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock
                    BidiLtr|BidiRtl
                    Link|Unlink|Anchor
                    Image|Table|HorizontalRule|Styles|Format|Font|FontSize
                    TextColor|BGColor
                    Maximize|ShowBlocks|" Height="200px" Width="670px">Thêm thông tin mô tả tại đây!</CKEditor:CKEditorControl></td>
        </tr>
              
        <tr style="color:Black">
            <td style="text-align: center" valign="middle" colspan="4" class="style2" >
                <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        
            
        <tr style="color:Black">
            <td style="text-align: center" valign="middle" colspan="4" >
                <asp:Button ID="btn_Insert" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Insert_Click" Text="Thêm" ValidationGroup="ProductManage" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_Update" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Update_Click" Text="Cập nhập" Visible="false" 
                    ValidationGroup="ProductManage" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_Cancel" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" Text="Hủy" 
                    Visible="false" onclick="btn_Cancel_Click" />
                <br />
                
                <asp:Label ID="lbl_ProductID" runat="server" style="color: #FFFFFF" 
                    Visible="False"></asp:Label>
                
                <br />
            </td>
        </tr>
    </table>
    
    <table style="width:800px">
        <tr style="color:Black">
            <td style="text-align: center" valign="middle">
                <asp:GridView ID="grv_Product" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" BackColor="#CCCCCC" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="2" 
                    CellSpacing="1" DataKeyNames="ID_MatHang" Font-Size="Medium" ForeColor="Black" 
                    HorizontalAlign="Center" onrowcommand="grv_Product_RowCommand" 
                    onrowediting="grv_Product_RowEditing" 
                    onpageindexchanging="grv_Product_PageIndexChanging" PageSize="9" 
                    UseAccessibleHeader="False" Width="700px" >
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID_MatHang" HeaderText="Mã" InsertVisible="False" 
                            ReadOnly="True" SortExpression="ID_MatHang" />
                        <asp:ImageField DataImageUrlField="HinhAnh" 
                            DataImageUrlFormatString="~/AnhSP/{0}" HeaderText="Ảnh">
                            <ControlStyle Height="30px" Width="40px" />
                        </asp:ImageField>
                        <asp:BoundField DataField="TenMatHang" HeaderText="Tên" />
                        <asp:BoundField DataField="NhaSanXuat" HeaderText="Nhà sản xuất" 
                            SortExpression="NhaSanXuat" />
                        <asp:BoundField DataField="LoaiHang" HeaderText="Loại hàng" 
                            SortExpression="LoaiHang" />
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
                </asp:GridView></td>
        </tr>
    </table>
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

