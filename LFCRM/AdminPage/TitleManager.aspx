<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TitleManager.aspx.cs" Inherits="LFCRM.AdminPage.TitleManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <strong>Title Manager
    <br />
    </strong>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT TitleID,TitleName,TOCKCode,Category,ColorCode
FROM tbl_Title,tbl_TitleCategory,tbl_Color
WHERE tbl_Title.TitleCategoryID = tbl_TitleCategory.TitleCategoryID
AND tbl_Title.ColorID = tbl_Color.ColorID">
        <UpdateParameters>
            <asp:Parameter Name="TitleName" Type="String"/>
            <asp:Parameter Name="TOCKCode" Type="String"/>
            <asp:Parameter Name="TitleID" Type="Int32"/>
        </UpdateParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT * FROM [tbl_TitleCategory]">
        <UpdateParameters>
            <asp:Parameter Name="Category" Type="String" />
            <asp:Parameter Name="Color" Type="String" />
            <asp:Parameter Name="TitleCategoryID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT * FROM [tbl_Color]">
        <UpdateParameters>
            <asp:Parameter Name="ColorCode" Type="String" />
            <asp:Parameter Name="ColorID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
    CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" 
    AllowPaging="True" 
    AllowSorting="True" >
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
        <button class="btn btn-info" ID="btn_creatnew">Create New Title</button>
    </center>
</asp:Content>
