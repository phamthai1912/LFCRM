<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmLogin.ascx.cs" Inherits="frmLogin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>

<asp:UpdatePanel ID="upLogin" runat="server">
<ContentTemplate> 

    <table id="tbLogin" visible="true" runat="server" style="height: 135px">
        <tr style="color: #000000; font-weight: bold; height:22px">
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;User:
            </td>
            <td>
                <asp:TextBox ID="txtUser" runat="server" Width="90px" BorderStyle="Outset"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" 
                    ControlToValidate="txtUser" Font-Bold="True" Font-Size="Larger" 
                    ValidationGroup="Login"></asp:RequiredFieldValidator>  
            </td>
        </tr>
        
        <tr style="color: #000000; font-weight: bold; height:22px">
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pass:
            </td>
            <td>
                <asp:TextBox ID="txtPass" runat="server" Width="90px" TextMode="Password" BorderStyle="Outset"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" 
                    ControlToValidate="txtPass" Font-Bold="True" Font-Size="Larger" 
                    ValidationGroup="Login"></asp:RequiredFieldValidator> 
            </td>
        </tr>

        <tr style="height:22px">
            <td colspan="3" align="center">
                <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    onclick="btnLogin_Click" ValidationGroup="Login" />
            </td>

        </tr>
        <tr>
            <td colspan="3" align="center" valign="top">
                <asp:Label ID="lblThongBao" runat="server" ForeColor="Red"></asp:Label> 
            </td>
        </tr>
    </table>
    <table id="tbThongTin" visible="false" runat="server" style="height:135px">
        <tr>
            <td valign="top">
                <asp:Label ID="lblThongTin" runat="server" ForeColor="Black"></asp:Label>
                <br /><br />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_Admin" runat="server" Text="Admin" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White" 
                    CausesValidation="False" PostBackUrl="Notification.aspx" Visible="False" />
                &nbsp;&nbsp;
                <asp:Button ID="btnLogout" runat="server" Text="Thoát" BackColor="#213943" 
                    BorderStyle="Ridge" Font-Bold="True" ForeColor="White"
                    onclick="btnLogout_Click" CausesValidation="False" />
            </td>
        </tr>
    </table>
    
</ContentTemplate>
</asp:UpdatePanel> 
