<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ManagementProduction.aspx.cs" Inherits="Admin_Productor" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="upProduction" runat="server">
<ContentTemplate>
    <table style="width:770px" align="center">
        <tr style="font-weight: bold; height: 35px; font-size:x-large">
            <td style="text-align: center">
                Nhà sản xuất</td>
        </tr>
        <tr>
            <td>
             
                <asp:Label ID="lbl_Code" runat="server" ForeColor="#FF3300" 
                    style="color: #FF0000" Visible="False"></asp:Label>
             
            </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: center">
                Tên nhà sản xuất :&nbsp;&nbsp; 
                <asp:TextBox ID="txt_ProductionName" runat="server" Width="150px"></asp:TextBox>
                <asp:Label ID="lbl_nsx" runat="server" ForeColor="#FF3300"></asp:Label>
                <asp:Button ID="btn_Insert" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Insert_Click" Text="Thêm" style="height: 26px" />
                <asp:Button ID="btn_Update" runat="server" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btn_Update_Click" Text="Sửa" Visible="False" Width="60px" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center" >
                <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:GridView ID="grv_Production" runat="server" AutoGenerateColumns="False" 
                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                    CellPadding="4" DataKeyNames="ID_NhaSanXuat" 
                    ForeColor="Black" AllowPaging="True" 
                    HorizontalAlign="Center" PageSize="20" AllowSorting="True" 
                    onrowcommand="grv_Production_RowCommand" CellSpacing="2" 
                    onpageindexchanging="grv_Production_PageIndexChanging" Width="700px" 
                    onrowediting="grv_Production_RowEditing">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID_NhaSanXuat" HeaderText="Mã nhà sản xuất" 
                            InsertVisible="False" ReadOnly="True" >
                            <HeaderStyle Width="180px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NhaSanXuat" HeaderText="Nhà sản xuất" />
                        <asp:CommandField DeleteText="" HeaderText="Sửa" 
                            ShowEditButton="True" UpdateText="" EditText="Chọn" CancelText="">
                            <ItemStyle ForeColor="#0000CC" />
                        </asp:CommandField>
                        <asp:ButtonField CommandName="del" HeaderText="Xóa" Text="Xóa" >
                            <ItemStyle ForeColor="Red" />
                        </asp:ButtonField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr style="color:Black; text-align: center">
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
    </table>
</ContentTemplate>
</asp:UpdatePanel>

    
    
    </asp:Content>

