<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ExportProduct.aspx.cs" Inherits="ExportProduct" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <table style="width:100%; font-weight: bold; height: 35px; font-size:x-large">
            <tr>
                <td align="center">
                    Xuất Hàng</td>
            </tr>
        </table>
        <br />
        <table style="width: 770px; color: black; margin-left:30px" runat=server id="tb_Export">
            <tr>
                <td>Mã phiếu xuất: &nbsp;<b>000
                    <asp:Label ID="lbl_Code" runat="server" Text=""></asp:Label>
                    </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Số lượng còn: &nbsp;<asp:Label ID="lbl_TonTrongKho" runat="server" 
                        Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Mã mặt hàng: &nbsp;<asp:Label ID="lbl_MaSp" runat="server" 
                        Font-Bold="True"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td ><hr /></td>
            </tr>
            <tr>
                <td >
                    <table style="width: 770px; color: black; text-align:center; border-collapse:collapse" borderColor="#213943" border="1" runat=server id="tb_ImportDetail">
                        <tr>
                            <td style="width: 320px">Mặt hàng:</td>
                            <td style="width: 50px">Đơn vị tính:</td>
                            <td style="width: 90px">Loại hàng:</td>
                            <td style="width: 100px">Nhà sản xuất:</td>
                            <td style="width: 90px">Số lượng:</td>
                            <td style="width: 150px">Ghi chú:</td>
                        </tr>
                        
                        <tr>
                            <td><asp:TextBox ID="txt_Product" runat="server"  AutoPostBack="True" 
                                    ontextchanged="txt_Product_TextChanged" ></asp:TextBox>
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
                            <td><asp:Label ID="lbl_LoaiHang" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_NhaSanXuat" runat="server" Text=""></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txt_Quantity" runat="server" Width="50px" AutoPostBack="True" 
                                    ontextchanged="txt_Quantity_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ErrorMessage="*" ControlToValidate="txt_Quantity" 
                                    ValidationGroup="ImportProduct"></asp:RequiredFieldValidator></td>
                            <td><asp:TextBox ID="txt_Notes" runat="server"></asp:TextBox></td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td align=center>
                    <asp:Label ID="lbl_ThongBao" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Button ID="btn_Add" runat="server" Text="Thêm" 
                    ValidationGroup="ImportProduct" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        onclick="btn_Add_Click"/>
                </td>
            </tr>
            <tr>
                <td align=center>
                    <asp:GridView ID="grv_Export" runat="server" AutoGenerateColumns="False" 
                        onrowcommand="grv_Export_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Id_mathang" HeaderText="Mã mặt hàng" />
                            <asp:BoundField DataField="TenMatHang" HeaderText="Tên mặt hàng" />
                            <asp:BoundField DataField="DonViTinh" HeaderText="Đơn vị tính" />
                            <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" />
                            <asp:BoundField DataField="GhiChu" HeaderText="Ghi Chú" />
                            <asp:ButtonField CommandName="Del" HeaderText="Xóa" Text="Xóa" />
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
        <asp:Label ID="lbl_DeliveryNote" runat="server" Text=""></asp:Label>
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

