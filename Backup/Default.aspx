﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 810px; color: black; margin-left:10px" runat=server id="tb_Product">
        <tr>
            <td colspan=2 align="center" style="font-weight: bold; height: 35px; font-size:x-large; color:White"> Khuyến mãi</td>
        </tr>
        <tr>
            <td>
                <embed src="Anh/quangcao1.swf" quality="high" 
                pluginspage=" http://www.macromedia.com/go/getflashplayer" 
                type="application/x-shockwave-flash" width="480" height="230" >
            </td>
            <td>
                <embed src="Anh/quangcao2.swf" quality="high" 
                pluginspage=" http://www.macromedia.com/go/getflashplayer" 
                type="application/x-shockwave-flash" width="310" height="230" >
            </td>
        </tr>
        <tr>
            <td colspan=2 background="Anh/Dock2.png">
                <fieldset>
                    <legend><img src="Anh/sanphamoi.png" /></legend>
                    <asp:DataList ID="dtlProduct" runat="server" DataKeyField="ID_MatHang" 
                        DataSourceID="SqlDataSource1" ForeColor="#213943" RepeatColumns="5">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="imgProduct" runat="server" 
                                            ImageUrl='<%# Eval("HinhAnh", "~/AnhSP/{0}") %>' 
                                            style="width: 105px; height:125px"
                                            onclientclick='<%# Eval("ID_MatHang") %>' 
                                            PostBackUrl='<%# Eval("ID_MatHang", "default.aspx?ID_MatHang={0}") %>' 
                                            onclick="imgProduct_Click"  />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="TenMatHangLabel" runat="server" Text='<%# Eval("TenMatHang") %>' style="font-weight: 700" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center"style="text-align: center; font-weight: 700; color: #FF0000">
                                        <asp:Label ID="DonGiaLabel" runat="server" Text='<%# Eval("DonGia", "{0:N0}") %>'/> vnđ
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                        ProviderName="System.Data.SqlClient" 
                        SelectCommand="SELECT TOP 5 * FROM [MatHang] ORDER BY NEWID()">
                    </asp:SqlDataSource>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan=2 background="Anh/Dock2.png">
                <fieldset>
                    <legend><img src="Anh/sanphamhot.png" /></legend>
                    <asp:DataList ID="DataList1" runat="server" DataKeyField="ID_MatHang" 
                        DataSourceID="SqlDataSource1" ForeColor="#213943" RepeatColumns="5">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="imgProduct" runat="server" 
                                            ImageUrl='<%# Eval("HinhAnh", "~/AnhSP/{0}") %>' 
                                            style="width: 105px; height:125px"
                                            onclientclick='<%# Eval("ID_MatHang") %>' 
                                            PostBackUrl='<%# Eval("ID_MatHang", "default.aspx?ID_MatHang={0}") %>' 
                                            onclick="imgProduct_Click"  />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="TenMatHangLabel" runat="server" Text='<%# Eval("TenMatHang") %>' style="font-weight: 700" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center"style="text-align: center; font-weight: 700; color: #FF0000">
                                        <asp:Label ID="DonGiaLabel" runat="server" Text='<%# Eval("DonGia", "{0:N0}") %>'/> vnđ
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan=2 background="Anh/Dock2.png">
                <fieldset>
                    <legend><img src="Anh/sanphambanchay.png" /></legend>
                    <asp:DataList ID="DataList2" runat="server" DataKeyField="ID_MatHang" 
                        DataSourceID="SqlDataSource1" ForeColor="#213943" RepeatColumns="5">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="imgProduct" runat="server" 
                                            ImageUrl='<%# Eval("HinhAnh", "~/AnhSP/{0}") %>' 
                                            style="width: 105px; height:125px"
                                            onclientclick='<%# Eval("ID_MatHang") %>' 
                                            PostBackUrl='<%# Eval("ID_MatHang", "default.aspx?ID_MatHang={0}") %>' 
                                            onclick="imgProduct_Click"  />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="TenMatHangLabel" runat="server" Text='<%# Eval("TenMatHang") %>' style="font-weight: 700" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center"style="text-align: center; font-weight: 700; color: #FF0000">
                                        <asp:Label ID="DonGiaLabel" runat="server" Text='<%# Eval("DonGia", "{0:N0}") %>'/> vnđ
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </fieldset>
            </td>
        </tr>
    </table>
    
    <asp:Panel ID="pn_ProductDetail" runat="server" Visible=false>
    <table style="width:770px" align="center" id="tbProductDetail" runat=server>
        <tr style="font-weight: bold; height: 35px; font-size:x-large ">
            <td  style="text-align: center" valign=middle>
                Chi tiết sản phẩm
                <br />
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:FormView ID="fv_ProductDetail" runat="server" DataKeyNames="ID_MatHang" 
                    DataSourceID="SqlDataSource2" style="color: #000000" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical">
                   
                    <ItemTemplate>
                        <table style="width:770px" align="center" id="tbProduct">
                            <tr>
                                <td rowspan="6" valign="top" width="150px" align="right" >
                                    <asp:Image ID="Image1" runat="server" Height="150px" 
                                        ImageUrl='<%# Eval("HinhAnh", "~/AnhSP/{0}") %>' Width="120px" 
                                        BorderColor="#FF6699" BorderStyle="Inset" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td valign="top" width=150px>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sản phẩm</td>
                                <td valign="top">
                                
                                    <asp:Label ID="TenMatHangLabel" runat="server" 
                                        Text='<%# Bind("TenMatHang") %>' Font-Size="Large" 
                                        style="font-weight: 700" />
                                
                                </td>
                            </tr>
                            
                            <tr>
                                <td valign="top">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Nhà sản xuất:</td>
                                <td valign="top">
                                    <asp:Label ID="ID_NhaSanXuatLabel" runat="server" 
                                        Text='<%# Bind("NhaSanXuat") %>' style="font-weight: 700" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Giá:</td>
                                <td valign="top">
                                    <asp:Label ID="DonGiaLabel" runat="server" 
                                        Text='<%# Bind("DonGia", " {0:N0}") %>' ForeColor="Red" 
                                        style="font-weight: 700" />
                                    &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Red" style="font-weight: 700" 
                                        Text="vnđ"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td valign="top">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Color</td>
                                <td valign="middle">
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="15px" Width="15px" 
                                        ImageUrl="~/Anh/icon_black.gif" />
                                    &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" Height="15px" 
                                        ImageUrl="~/Anh/white.gif" Width="15px" />
                                    &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" Height="15px" 
                                        ImageUrl="~/Anh/white_blue.gif" Width="15px" />
                                    &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" Height="15px" 
                                        ImageUrl="~/Anh/grey.gif" Width="15px" />
                                    &nbsp;<asp:ImageButton ID="ImageButton5" runat="server" Height="15px" 
                                        ImageUrl="~/Anh/black_purple.gif" Width="15px" />
                                    &nbsp;<asp:ImageButton ID="ImageButton6" runat="server" Height="15px" 
                                        ImageUrl="~/Anh/black_red.gif" Width="15px" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="text-align: left">
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ID="btn_BuyNow" runat="server" ImageUrl="~/Anh/buynow_button.png" 
                                        onclick="btn_BuyNow_Click" Height="30px" />
                                </td>
                                <td valign="top" style="text-align: left">
                                    <asp:TextBox ID="txt_Quantity" runat="server" Width="60px">1</asp:TextBox>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                        ControlToValidate="txt_Quantity" ErrorMessage="*" MaximumValue="1000" 
                                        MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                    <asp:Label ID="lbl_Mess" runat="server" ForeColor="#FF3300" Visible="False">Nhập 
                                    số lượng cần mua !!!</asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Panel ID="Panel1" runat="server" Height="665px" ScrollBars="Auto">
                                        <asp:Label ID="MoTaLabel" runat="server" Text='<%# Bind("MoTa") %>' />
                                    </asp:Panel>
                                </td>
                            </tr>
                            
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:TSKN %>" 
                    SelectCommand="SELECT * FROM [MatHang],[NhaSanXuat] WHERE ([ID_MatHang] = @ID_MatHang) AND MatHang.ID_NhaSanXuat=NhaSanXuat.ID_NhaSanXuat">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="ID_MatHang" QueryStringField="ID_MatHang" 
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                
            </td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>
