<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="ComposeMessage.aspx.cs" Inherits="ComposeMessage" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width:100%; font-weight: bold; height: 35px; font-size:x-large">
            <tr>
                <td align="center">Soạn tin nhắn</td>
            </tr>
        </table>
        <br />
        <table style='width: 770px; color: black; border-collapse:collapse; border-color: Black; margin-left:30px' border=1>
            <tr>
                <td style='width: 100px'>Người nhận: </td>
                <td><asp:TextBox ID="txt_NguoiNhan" runat="server" Width="200px" 
                        BorderStyle="Solid" AutoPostBack="True" 
                        ontextchanged="txt_NguoiNhan_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender runat="server" 
                    ID="autoComplete1" 
                    TargetControlID="txt_NguoiNhan"
                    ServicePath="AutoComplete.asmx" 
                    ServiceMethod="GetCompletionListUserName"
                    MinimumPrefixLength="1" 
                    CompletionInterval="500"
                    EnableCaching="true"
                    CompletionSetCount="5">
                </asp:AutoCompleteExtender>
                    <asp:Label ID="lbl_ThongBao" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style='width: 100px'>Tiêu đề: </td>
                <td><asp:TextBox ID="txt_TieuDe" runat="server" Width="650px" BorderStyle="Solid"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan=2><br />
                    <CKEditor:CKEditorControl ID="txt_NoiDung" runat="server" ToolbarFull="Source|-|Save|NewPage|Preview|-|Templates
                    Cut|Copy|Paste|PasteText|PasteFromWord|-|Print|SpellChecker|Scayt
                    Undo|Redo|-|Find|Replace|-|SelectAll|RemoveFormat
                    /
                    Bold|Italic|Underline|Strike|NumberedList|BulletedList|-|Outdent|Indent|Blockquote|CreateDiv
                    JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock
                    BidiLtr|BidiRtl
                    Link|Unlink|Anchor
                    Image|Table|HorizontalRule|Styles|Format|Font|FontSize
                    TextColor|BGColor
                    Maximize|ShowBlocks|" Height="300px" BorderStyle="Solid"></CKEditor:CKEditorControl>
                </td>
            </tr>
        </table>
        
        <table style='width: 770px;'>
            <tr>
                <td colspan=2 align=center>                    
                    <asp:Button ID="btn_Send" runat="server" Text="Gởi"  BackColor="#213943" 
                        BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                        onclick="btn_Send_Click" Visible=false/></td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

