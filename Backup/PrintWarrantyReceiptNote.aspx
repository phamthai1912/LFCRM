<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="PrintWarrantyReceiptNote.aspx.cs" Inherits="PrintWarrantyReceiptNote" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="Pn_Form" runat="server" >
            <table style="width:800px" align="center" > 
                <tr style="font-weight: bold; height: 35px; font-size:x-large">
                     <td style="text-align: center" >
                    Lập phiếu nhận bảo hành
                    </td>
                </tr>           
                <tr>
                    <td align=center style="color:Black">
                        Chọn phiếu nhận số : 
                        <asp:DropDownList ID="ddl_MaPhieuNhan" runat="server" 
                            DataSourceID="SqlDataSource1" DataTextField="ID_PhieuBaoHanh" 
                            DataValueField="ID_PhieuBaoHanh" Width="100px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:Button ID="btn_XuatPhieu" runat="server" BackColor="#213943" 
                            BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                            onclick="btn_XuatPhieu_Click" Text="Xuất phiếu" />
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                            SelectCommand="SELECT [ID_PhieuBaoHanh] FROM [PhieuNhanBaoHanh] ORDER BY [ID_PhieuBaoHanh] DESC">
                        </asp:SqlDataSource>
                    </td>
                </tr> 
                <tr>
                    <td>
                        <asp:Panel ID="pn_lblprint" runat="server">
                            <table style="width:800px" align="center" >
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lbl_Form" runat="server"></asp:Label>                              
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
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

