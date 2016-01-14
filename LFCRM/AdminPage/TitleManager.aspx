<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TitleManager.aspx.cs" Inherits="LFCRM.AdminPage.TitleManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    Title Manager
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" DeleteCommand="DELETE FROM [tbl_Title] WHERE [TitleID] = @TitleID" InsertCommand="INSERT INTO [tbl_Title] ([TitleID], [TitleCategoryID], [TOCKCode], [TitleName], [ColorID]) VALUES (@TitleID, @TitleCategoryID, @TOCKCode, @TitleName, @ColorID)" ProviderName="System.Data.SqlClient" SelectCommand="SELECT *,ColorCode,Category
FROM tbl_Title t
LEFT  JOIN tbl_Color c on t.ColorID = c.ColorID
LEFT  JOIN tbl_TitleCategory tc on t.TitleCategoryID = tc.TitleCategoryID" UpdateCommand="UPDATE [tbl_Title] SET [TitleCategoryID] = @TitleCategoryID, [TOCKCode] = @TOCKCode, [TitleName] = @TitleName, [ColorID] = @ColorID WHERE [TitleID] = @TitleID">
        <DeleteParameters>
            <asp:Parameter Name="TitleID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="TitleID" Type="Int32" />
            <asp:Parameter Name="TitleCategoryID" Type="Int32" />
            <asp:Parameter Name="TOCKCode" Type="String" />
            <asp:Parameter Name="TitleName" Type="String" />
            <asp:Parameter Name="ColorID" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="TitleCategoryID" Type="Int32" />
            <asp:Parameter Name="TOCKCode" Type="String" />
            <asp:Parameter Name="TitleName" Type="String" />
            <asp:Parameter Name="ColorID" Type="String" />
            <asp:Parameter Name="TitleID" Type="Int32" />
            <asp:Parameter Name="Category" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="TitleID" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="TitleName" HeaderText="TitleName" SortExpression="TitleName" />
            <asp:BoundField DataField="TOCKCode" HeaderText="Code" SortExpression="TOCKCode" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="ColorCode" HeaderText="Color" />
            <asp:TemplateField HeaderText="Modify" ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
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
</asp:Content>
