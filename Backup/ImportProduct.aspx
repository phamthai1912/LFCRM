<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ImportProduct.aspx.cs" Inherits="ImportProduct" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <table style="width:100%; font-weight: bold; height: 35px; font-size:x-large">
        <tr>
            <td align="center">
                Nhập kho
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 770px; color: black; margin-left:30px" runat=server id="tb_Code">
        <tr>
            <td width="120">Mã phiếu nhập: &nbsp;<b>000
                <asp:Label ID="lbl_Code" runat="server" Text=""></asp:Label></b></td>
            <td style="width:80px">Nhà cung cấp: </td>
            <td width="150">
                <asp:TextBox runat="server" ID="txt_Provider" 
                    ontextchanged="txt_Provider_TextChanged" AutoPostBack="True"/>
                <asp:AutoCompleteExtender runat="server" 
                    ID="autoComplete1" 
                    TargetControlID="txt_Provider"
                    ServicePath="AutoComplete.asmx" 
                    ServiceMethod="GetCompletionListProvider"
                    MinimumPrefixLength="1" 
                    CompletionInterval="500"
                    EnableCaching="true"
                    CompletionSetCount="5">
                </asp:AutoCompleteExtender>
                </td>
            <td >
                &nbsp;&nbsp;
                <asp:ImageButton ID="btn_RefreshProvider" runat="server" Visible="false" 
                    Height="30px" ImageUrl="~/Anh/Refresh.png" onclick="btn_RefreshProvider_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbl_Provider" runat="server" Text="" ForeColor="Red"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="*" ControlToValidate="txt_Provider" 
                    ValidationGroup="ImportProduct"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="4"><hr /></td>
        </tr>
    </table>
    
    <table style="width: 770px; color: black; margin-left:30px; text-align:center; border-collapse:collapse" borderColor="#213943" border="1" runat=server id="tb_ImportDetail">
        <tr>
            <td style="width: 320px">Mặt hàng:</td>
            <td style="width: 50px">Đơn vị tính:</td>
            <td style="width: 175px">Đơn giá:</td>
            <td style="width: 70px">Số lượng:</td>
            <td style="width: 155px">Ghi chú:</td>
        </tr>
        
        <tr>
            <td><asp:TextBox ID="txt_Product" runat="server"  AutoPostBack="True" 
                    ontextchanged="txt_Product_TextChanged" ></asp:TextBox>
                <asp:ImageButton ID="btn_RefreshProduct" runat="server" Visible="false" 
                    Height="25px" ImageUrl="~/Anh/Refresh.png" 
                    onclick="btn_RefreshProduct_Click"/>
                <asp:AutoCompleteExtender runat="server" 
                    ID="AutoCompleteExtender1" 
                    TargetControlID="txt_Product"
                    ServicePath="AutoComplete.asmx" 
                    ServiceMethod="GetCompletionListProduct"
                    MinimumPrefixLength="1" 
                    CompletionInterval="500"
                    EnableCaching="true"
                    CompletionSetCount="5">
                </asp:AutoCompleteExtender>
                <asp:Label ID="lbl_Product" runat="server" Text="" ForeColor="Red"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="*" ControlToValidate="txt_Product" 
                    ValidationGroup="ImportProduct"></asp:RequiredFieldValidator></td>
            <td>
                <asp:DropDownList ID="ddl_DonViTinh" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="DonViTinh" 
                    DataValueField="id_DonViTinh">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                    ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [DonViTinh]">
                </asp:SqlDataSource>
                </td>
            <td>
                <asp:TextBox ID="txt_UnitPrice" runat="server" Width="120px"></asp:TextBox>
                vnđ
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="*" ControlToValidate="txt_UnitPrice" 
                    ValidationGroup="ImportProduct"></asp:RequiredFieldValidator></td>
            <td>
                <asp:TextBox ID="txt_Quantity" runat="server" Width="50px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="*" ControlToValidate="txt_Quantity" 
                    ValidationGroup="ImportProduct"></asp:RequiredFieldValidator></td>
            <td><asp:TextBox ID="txt_Notes" runat="server"></asp:TextBox></td>
        </tr>
        
    </table>
    
    <table style="width: 750px; color: black; margin-left:30px;" runat=server id="tb_grvImport">
        <tr>
            <td align=center>
                <asp:Button ID="btn_Add" runat="server" Text="Thêm" 
                    ValidationGroup="ImportProduct" onclick="btn_Add_Click" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White"/></td>
        </tr>
        <tr>
            <td align=center>
                <asp:Label ID="lbl_ThongBao" runat="server" Text="" ForeColor="Red"></asp:Label>
                &nbsp;</td>
        </tr>

        <tr>
            <td align=center>
                <asp:GridView ID="grv_Import" runat="server" AutoGenerateColumns="False" 
                    onrowcommand="grv_Import_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="TenMatHang" HeaderText="Tên mặt hàng" />
                        <asp:BoundField DataField="DonViTinh" HeaderText="Đơn vị tính" />
                        <asp:BoundField DataField="DonGia" HeaderText="Đơn giá" />
                        <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" />
                        <asp:BoundField DataField="GhiChu" HeaderText="Ghi chú" />
                        <asp:ButtonField CommandName="Del" HeaderText="Delete" Text="Xóa" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align=center>
                <asp:Button ID="btn_Finish" runat="server" Text="Xong" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Finish_Click" Visible="False"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_Cancel" runat="server" Text="Hủy" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Cancel_Click" Visible="False"/>
            </td>
        </tr>
    </table>
    <asp:Label ID="lbl_ReceiptNote" runat="server" Text=""></asp:Label>
    <table style="width: 770px; margin-left:30px" runat=server id="Table1">
            <tr>
                <td align=center><asp:Button ID="btn_Print" runat="server" Text="In phiếu"  BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" Visible=false 
                        onclick="btn_Print_Click"/></td>
            </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

