<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ManagementRole.aspx.cs" Inherits="ManagementRole" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   
        <table style="width:770px" align="center">
        
        <tr style="font-weight: bold; height: 35px; font-size:x-large; font-family:Agency FB ">
            <td colspan="2" style="text-align: center">
                Role Management
                </td>
        </tr>
        
        <tr style="color:Black">
            <td style="text-align: right">
                <br />
                Role Name :&nbsp;&nbsp; </td>
            <td style="text-align: left">
                <br />
                <asp:TextBox ID="txt_RoleName" Width="150px" runat="server"></asp:TextBox>
            </td>
        </tr>
        
         <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="btn_Insert" runat="server" BackColor="#213943" 
                BorderStyle="Ridge" Font-Bold="True" ForeColor="White" Text="Insert" onclick="btn_Insert_Click" 
                     />
            </td>
        </tr>
         
         <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300"></asp:Label>
             </td>
        </tr>
         
         <tr>
            <td colspan="2" style="text-align: center">
                <asp:GridView ID="grv_Role" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" BackColor="#CCCCCC" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                    CellSpacing="2" DataKeyNames="ID_Quyen" DataSourceID="SqlDataSource1" 
                    ForeColor="Black" HorizontalAlign="Center" onrowcommand="grv_Role_RowCommand" 
                    style="color: #000000" Width="700px">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID_Quyen" HeaderText="ID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="ID_Quyen" />
                        <asp:BoundField DataField="QuyenHan" HeaderText="Role Name" 
                            SortExpression="QuyenHan" />
                        <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" SelectText="" 
                            ShowEditButton="True" UpdateText="ok">
                            <ItemStyle ForeColor="#0000CC" />
                        </asp:CommandField>
                        <asp:ButtonField CommandName="del" HeaderText="Delete" Text="Delele">
                            <ItemStyle ForeColor="#0000CC" />
                        </asp:ButtonField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                    SelectCommand="SELECT * FROM [PhanQuyen] ORDER BY ID_Quyen DESC" 
                    UpdateCommand="UPDATE [PhanQuyen] SET [QuyenHan] = @QuyenHan WHERE [ID_Quyen] = @ID_Quyen">
                    <UpdateParameters>
                        <asp:Parameter Name="QuyenHan" Type="String" />
                        <asp:Parameter Name="ID_Quyen" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
             </td>
        </tr>
        
    </table>
    
   </ContentTemplate>
   </asp:UpdatePanel>  
</asp:Content>

