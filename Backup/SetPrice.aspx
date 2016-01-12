<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="SetPrice.aspx.cs" Inherits="SetPrice" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width:100%; font-weight: bold; height: 35px; font-size:x-large">
            <tr>
                <td align="center">Thiết lập giá bán</td>
            </tr>
        </table>
        <br />
        <table style="width: 750px; color: black; margin-left:30px">
            <tr>
                <td width=200px>
                    Chọn loại hàng:
                    <asp:DropDownList ID="ddl_Loaihang" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource1" DataTextField="LoaiHang" 
                        DataValueField="ID_LoaiHang" 
                        onselectedindexchanged="ddl_Loaihang_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                        SelectCommand="SELECT * FROM [LoaiHang]"></asp:SqlDataSource></td>
                <td align=left>
                    Chọn nhà sản xuất:
                    <asp:DropDownList ID="ddl_NhaSanXuat" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource2" DataTextField="NhaSanXuat" 
                        DataValueField="ID_NhaSanXuat" 
                        onselectedindexchanged="ddl_NhaSanXuat_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                        SelectCommand="SELECT DISTINCT [NhaSanxuat], [nhasanxuat].[id_nhasanxuat] FROM [Nhasanxuat], [Loaihang], [Mathang] WHERE [nhasanxuat].[id_nhasanxuat] = [mathang].[id_nhasanxuat] AND [Loaihang].[id_Loaihang] = [mathang].[id_loaihang] AND ([Loaihang].[id_Loaihang]=@ID_LoaiHang)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddl_Loaihang" Name="ID_LoaiHang" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan=2><hr /></td>
            </tr>
            <tr>
                <td>Mã sản phẩm: 
                    <asp:Label ID="lbl_Masp" runat="server" Text=""></asp:Label></td>
                <td>Giá bán mới:
                    <asp:TextBox ID="txt_GiaBanMoi" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="*" ControlToValidate="txt_GiaBanMoi" 
                        ValidationGroup="Setprice"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_ThietLap" runat="server" Text="Thiết lập" 
                        ValidationGroup="Setprice" BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        onclick="btn_ThietLap_Click" Visible="False"/>
                    <asp:Button ID="btn_Huy" runat="server" Text="Hủy" BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        onclick="btn_Huy_Click"/>
                    &nbsp;&nbsp;
                    <asp:RangeValidator ID="RangeValidator3" runat="server" 
                    ControlToValidate="txt_GiaBanMoi" ErrorMessage="Chỉ nhập số" 
                    MaximumValue="100000000000" MinimumValue="1" Type="Double" 
                        ValidationGroup="Setprice"></asp:RangeValidator>
                    </td>
            </tr>
            <tr>
                <td colspan=2 align=center><asp:Label ID="lbl_ThongBao" runat="server" Text="" ForeColor=Red></asp:Label></td>
            </tr>
        </table>
        
        <table style="width: 750px; color: black; margin-left:30px">
            <tr>
                <td align="center">
                    <asp:GridView ID="grv_Setprice" runat="server" AutoGenerateColumns="False" 
                        Width="700px" onrowediting="grv_Setprice_RowEditing" >
                        <Columns>
                            <asp:BoundField DataField="id_mathang" HeaderText="Mã mặt hàng" />
                            <asp:BoundField DataField="LoaiHang" HeaderText="Loại hàng" />
                            <asp:BoundField DataField="Nhasanxuat" HeaderText="Nhà sản xuất" />
                            <asp:BoundField DataField="tenmathang" HeaderText="Mặt hàng" />
                            <asp:BoundField DataField="DonGia" HeaderText="Đơn giá" />
                            <asp:CommandField CancelText="" DeleteText="" HeaderText="" InsertText="" 
                                NewText="" SelectText="" ShowCancelButton="False" ShowEditButton="True" 
                                UpdateText="" EditText="Chọn">
                                <ItemStyle ForeColor="#0000CC" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

