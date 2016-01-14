<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TitleManager.aspx.cs" Inherits="LFCRM.AdminPage.TitleManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    Title Manager
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" DeleteCommand="DELETE FROM [tbl_Title] WHERE [TitleID] = @TitleID" InsertCommand="INSERT INTO [tbl_Title] ([TitleID], [TitleCategoryID], [ColorID], [TOCKCode], [TitleName]) VALUES (@TitleID, @TitleCategoryID, @ColorID, @TOCKCode, @TitleName)" ProviderName="System.Data.SqlClient" SelectCommand="SELECT TitleName,TOCKCode,Category,ColorCode
FROM tbl_Title,tbl_TitleCategory,tbl_Color
WHERE tbl_Title.TitleCategoryID = tbl_TitleCategory.TitleCategoryID
AND tbl_Title.ColorID = tbl_Color.ColorID" UpdateCommand="UPDATE [tbl_Title] SET [TitleCategoryID] = @TitleCategoryID, [ColorID] = @ColorID, [TOCKCode] = @TOCKCode, [TitleName] = @TitleName WHERE [TitleID] = @TitleID">
        <DeleteParameters>
            <asp:Parameter Name="TitleID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="TitleID" Type="Int32" />
            <asp:Parameter Name="TitleCategoryID" Type="Int32" />
            <asp:Parameter Name="ColorID" Type="String" />
            <asp:Parameter Name="TOCKCode" Type="String" />
            <asp:Parameter Name="TitleName" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="TitleCategoryID" Type="Int32" />
            <asp:Parameter Name="ColorID" Type="String" />
            <asp:Parameter Name="TOCKCode" Type="String" />
            <asp:Parameter Name="TitleName" Type="String" />
            <asp:Parameter Name="TitleID" Type="Int32" />
        </UpdateParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" DeleteCommand="DELETE FROM [tbl_TitleCategory] WHERE [TitleCategoryID] = @TitleCategoryID" InsertCommand="INSERT INTO [tbl_TitleCategory] ([Category], [Color]) VALUES (@Category, @Color)" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [tbl_TitleCategory]" UpdateCommand="UPDATE [tbl_TitleCategory] SET [Category] = @Category, [Color] = @Color WHERE [TitleCategoryID] = @TitleCategoryID">
        <DeleteParameters>
            <asp:Parameter Name="TitleCategoryID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Category" Type="String" />
            <asp:Parameter Name="Color" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Category" Type="String" />
            <asp:Parameter Name="Color" Type="String" />
            <asp:Parameter Name="TitleCategoryID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
    CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" >
    <Columns>
        <asp:BoundField DataField="TitleName" HeaderText="Title Name" />
        <asp:BoundField DataField="TOCKCode" HeaderText="Tock Code" />
        <asp:BoundField DataField="Category" HeaderText="Category" />
        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" HeaderText="Modify" />
        <asp:TemplateField HeaderText="Category">
            <EditItemTemplate>
                <asp:DropDownList ID="category" runat="server"></asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<br />
    <button class="btn">Test</button>
</asp:Content>
