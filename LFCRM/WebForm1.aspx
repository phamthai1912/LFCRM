<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LFCRM.WebForm1" %>

<!DOCTYPE html>

    <script type="text/javascript" src="/Scripts/jquery-latest.js"></script> 
    <script type="text/javascript" src="/Scripts/jquery.tablesorter.js"></script> 
    <script type="text/javascript">
        $(document).ready(function () {
            $("#myTable").tablesorter({ sortList: [[0, 0], [1, 0]] });
        }
        );
    </script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <%--   
        <asp:GridView ID="dataGridView1" runat="server"></asp:GridView>--%>
        <asp:DropDownList runat="server" ID="ddl1" Enabled="False"></asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
