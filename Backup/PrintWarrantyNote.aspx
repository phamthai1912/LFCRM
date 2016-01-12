<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="PrintWarrantyNote.aspx.cs" Inherits="PrintWarrantyNote" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <asp:Panel ID="pn_PhieuBaoHanh" runat="server"  >
            <table style="width:800px" align="center" >    
                <tr style="font-weight: bold; height: 35px; font-size:x-large ">
            <td style="text-align: center">
                <asp:Label ID="lbl_Title" runat="server" Text="Xuất phiếu bảo hành"></asp:Label>
                </td>
        </tr>        
                <tr style=" color:Black">
                    <td align=center >
                        Chọn mã bảo hành số :
                        <asp:DropDownList ID="ddl_MaBH" runat="server" DataSourceID="SqlDataSource3" 
                            DataTextField="ID_BaoHanh" DataValueField="ID_BaoHanh" Width="100px">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button ID="btn_XuatPhieu" runat="server" BackColor="#213943" 
                            BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                            onclick="btn_XuatPhieu_Click" Text="Xuất phiếu" />
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                            SelectCommand="SELECT [ID_BaoHanh] FROM [SoBaoHanh] ORDER BY [ID_BaoHanh] DESC">
                        </asp:SqlDataSource>
                    </td>
                </tr> 
               
            </table>
        </asp:Panel>
    
    
        <asp:Panel ID="Pn_Form" runat="server" Visible=false >
            <table style="width:800px" align="center" >            
                <tr>
                    <td align=center >
                        <asp:Label ID="lbl_Form" runat="server" ></asp:Label>
                    </td>
                </tr> 
            </table>
        </asp:Panel>
        
        
        <asp:Panel ID="Pn_btnPrint" runat="server" Visible=false>   
            <table style="width:800px" align="center" >
                <tr>
            <td style="text-align: center">
                <asp:Button  runat="server" BackColor="#213943" BorderStyle="Ridge" 
                Font-Bold="True" ForeColor="White" Text="In phiếu" ID="btn_Print" 
                        onclick="btn_Print_Click" />
            </td>
          </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

