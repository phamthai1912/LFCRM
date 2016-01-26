﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LFCRM.WebForm1" %>

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
        
<table id="myTable" class="tablesorter"> 
<thead> 
<tr> 
    <th>Last Name</th> 
    <th>First Name</th> 
    <th>Email</th> 
    <th>Due</th> 
    <th>Web Site</th> 
</tr> 
</thead> 
<tbody> 
<tr> 
    <td>Smith</td> 
    <td><asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>3</asp:ListItem>
        </asp:DropDownList></td> 
    <td>jsmith@gmail.com</td> 
    <td>$50.00</td> 
    <td>http://www.jsmith.com</td> 
</tr> 
<tr> 
    <td>Bach</td> 
    <td><asp:DropDownList ID="DropDownList2" runat="server">
        <asp:ListItem>2</asp:ListItem>
        </asp:DropDownList></td> 
    <td>fbach@yahoo.com</td> 
    <td>$50.00</td> 
    <td>http://www.frank.com</td> 
</tr> 
<tr> 
    <td>Doe</td> 
    <td><asp:DropDownList ID="DropDownList3" runat="server">
        <asp:ListItem>4</asp:ListItem>
        </asp:DropDownList></td> 
    <td>jdoe@hotmail.com</td> 
    <td>$100.00</td> 
    <td>http://www.jdoe.com</td> 
</tr> 
<tr> 
    <td>Conway</td> 
    <td><asp:DropDownList ID="DropDownList4" runat="server">
        <asp:ListItem>1</asp:ListItem>
        <asp:ListItem>5</asp:ListItem>
        </asp:DropDownList></td> 
    <td>tconway@earthlink.net</td> 
    <td>$50.00</td> 
    <td>http://www.timconway.com</td> 
</tr> 
</tbody> 
</table>
    </form>
</body>
</html>
