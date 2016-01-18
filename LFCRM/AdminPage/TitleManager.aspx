<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TitleManager.aspx.cs" Inherits="LFCRM.AdminPage.TitleManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <strong>Title Manager</strong>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="False">
        <ContentTemplate>
            <table class="table table-striped table-bordered table-responsive table-condensed">
                <tr>
                    <td>Tittle ID:</td>
                    <td>
                        <asp:Label ID="lb_titleid" runat="server"></asp:Label>
                    </td>            
                </tr>
                <tr>
                    <td>Title Name:</td>
                    <td>
                        <asp:TextBox ID="txt_titlename" runat="server" ValidateRequestMode="Enabled"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Tock Code</td>
                    <td>
                        <asp:TextBox ID="txt_tockcode" runat="server" ValidateRequestMode="Enabled"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Category</td>
                    <td>
                        <asp:DropDownList ID="drop_category" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Color</td>
                    <td>
                        <asp:Label ID="lb_color" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btn_save" runat="server" Text="Save" class="btn btn-success" OnClick="btn_save_Click"/>&nbsp;
                        <asp:Button ID="btn_close" runat="server" Text="Close" class="btn btn-success" OnClick="btn_close_Click1"/>
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=LGDN14091\SQLEXPRESS;Initial Catalog=LFCRM;User ID=sa;Password=qwe123" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT TitleID,TitleName,TOCKCode,Category,ColorCode
FROM tbl_Title,tbl_TitleCategory,tbl_Color
WHERE tbl_Title.TitleCategoryID = tbl_TitleCategory.TitleCategoryID
AND tbl_Title.ColorID = tbl_Color.ColorID 
ORDER BY TitleID DESC">
</asp:SqlDataSource>
    <center><asp:Label ID="lb_success" runat="server" class="label label-success"></asp:Label></center>
<asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
    OnRowCommand="GridView1_RowCommand"
    CssClass="table table-striped table-bordered table-responsive table-condensed table-hover" 
    AllowPaging="True" 
    AllowSorting="True"
    DataKeyNames="TitleID">
    <Columns>
        <asp:BoundField DataField="TitleID" HeaderText="Title ID" />
        <asp:BoundField DataField="TitleName" HeaderText="Title Name" />
        <asp:BoundField DataField="TOCKCode" HeaderText="Tock Code" />
        <asp:BoundField DataField="Category" HeaderText="Category" />
        <asp:BoundField DataField="ColorCode" HeaderText="Color" />
        <asp:TemplateField HeaderText="Edit Title" ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="btn_edit" runat="server" CommandName="edit_Title" Text="Edit" class="btn btn-success" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Delete Title" ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="btn_delete" runat="server" CausesValidation="False" CommandName="delete_Title" Text="Delete" class="btn btn-success"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    <br />
    <center>
        <button class="btn btn-info" ID="btn_creatnew">Create New Title</button>
    </center>

</asp:Content>
