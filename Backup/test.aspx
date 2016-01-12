<%@ Page Language="C#" MasterPageFile="~/MPAdmin.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            background-color: #FF0000;
        }
        .style2
        {
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br /><br /><br />
    <table style='width: 770px; color: black; border-collapse:collapse; border-color: Black; margin-left:30px' border=1>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="style1"></td>
            <td style="background-color: #FF0000; text-decoration: none;"><A HREF="javascript:window.print()" 
                    class="style2">Click to Print This Page</A></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="style1"></td>
            <td style="background-color: #FF0000"></td>
            <td style="background-color: #FF0000"></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>
</asp:Content>

