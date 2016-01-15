<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TitleManager.aspx.cs" Inherits="LFCRM.AdminPage.TitleManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <strong>Title Manager
    <br />
    </strong>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        DeleteCommand="DELETE FROM [tbl_Title] WHERE [TitleID] = @TitleID" 
        InsertCommand="INSERT INTO [tbl_Title] ([TitleID], [TitleCategoryID], [ColorID], [TOCKCode], [TitleName]) VALUES (@TitleID, @TitleCategoryID, @ColorID, @TOCKCode, @TitleName)" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT TitleID,TitleName,TOCKCode,Category,ColorCode
FROM tbl_Title,tbl_TitleCategory,tbl_Color
WHERE tbl_Title.TitleCategoryID = tbl_TitleCategory.TitleCategoryID
AND tbl_Title.ColorID = tbl_Color.ColorID" 
        UpdateCommand="UPDATE [tbl_Title]
SET [TitleName] = @TitleName,[TOCKCode] = @TOCKCode 
WHERE [TitleID] = @TitleID">
        <UpdateParameters>
            <asp:Parameter Name="TitleName" Type="String"/>
            <asp:Parameter Name="TOCKCode" Type="String"/>
            <asp:Parameter Name="TitleID" Type="Int32"/>
        </UpdateParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        DeleteCommand="DELETE FROM [tbl_TitleCategory] WHERE [TitleCategoryID] = @TitleCategoryID" 
        InsertCommand="INSERT INTO [tbl_TitleCategory] ([Category], [Color]) VALUES (@Category, @Color)" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT * FROM [tbl_TitleCategory]">
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
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        DeleteCommand="DELETE FROM [tbl_Color] WHERE [ColorID] = @ColorID" 
        InsertCommand="INSERT INTO [tbl_Color] ([ColorID], [ColorCode]) VALUES (@ColorID, @ColorCode)" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT * FROM [tbl_Color]">
        <DeleteParameters>
            <asp:Parameter Name="ColorID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ColorID" Type="Int32" />
            <asp:Parameter Name="ColorCode" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="ColorCode" Type="String" />
            <asp:Parameter Name="ColorID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
    CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" >
    <Columns>
        <asp:TemplateField HeaderText="Title Name">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" class="form-control" Text='<%# Bind("TitleName") %>'></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("TitleName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Tock Code">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" class="form-control" Text='<%# Bind("TOCKCode") %>'></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("TOCKCode") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Category">
            <EditItemTemplate>
                <asp:DropDownList CssClass="form-control" ID="category" DataSourceID="SqlDataSource2" DataTextField="Category" DataValueField="Category" runat="server"></asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Color">
            <EditItemTemplate>
                <asp:DropDownList ID="color" CssClass="form-control" DataSourceID="SqlDataSource3" DataTextField="ColorCode" DataValueField="ColorCode" runat="server" Text='<%# Bind("ColorCode") %>'></asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ColorCode") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Modify" ShowHeader="False">
            <EditItemTemplate>
                <asp:LinkButton ID="btn_update" runat="server" CausesValidation="True" CommandName="Update" Text="Update" class="btn btn-success"></asp:LinkButton>
                &nbsp;<asp:LinkButton ID="btn_cancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" class="btn btn-warning"></asp:LinkButton>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="btn_edit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" class="btn btn-info"></asp:LinkButton>
                &nbsp;<asp:LinkButton ID="btn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" class="btn btn-info"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    <br />
    <center>
        <button class="btn btn-info">Create New Title</button>
    </center>
</asp:Content>
