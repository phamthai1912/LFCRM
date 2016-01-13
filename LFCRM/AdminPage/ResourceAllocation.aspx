<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResourceAllocation.aspx.cs" Inherits="LFCRM.AdminPage.ResourceAllocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">



        <br />
    <br />
    <br />

    <br />
    <br />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
</asp:GridView>
    <br />

    <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;Persist Security Info=True;User ID=sa;Password=qwe123" ProviderName="System.Data.SqlClient" SelectCommand="SELECT TitleName, Category, ColorCode AS TitleCategory
        FROM tbl_Title, tbl_Color, tbl_TitleCategory
        WHERE tbl_Title.TitleCategoryID = tbl_TitleCategory.TitleCategoryID
        AND tbl_Title.ColorID = tbl_Color.ColorID"></asp:SqlDataSource>
    <br />
    <br />




</asp:Content>
