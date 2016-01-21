<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test1.aspx.cs" Inherits="LFCRM.AdminPage.Test1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True" />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="True">
        <ContentTemplate>
    <div>
        
        <asp:TextBox AutoPostBack="True" ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
