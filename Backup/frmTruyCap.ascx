<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmTruyCap.ascx.cs" Inherits="frmTruyCap" %>
<br /><br />
<table style="color:Black ; margin-left:10px">
    <tr>
        <td width=120px>&nbsp;&nbsp;<b> Lượt truy cập</b> : </td>
        <td><% = Application["SoLuotTruyCap"]%></td>
    </tr>
    <tr>
        <td>&nbsp;&nbsp; <b>Trực tuyến : </b> </td>
        <td><% = Application["tructuyen"]%></td>
    </tr>
</table>
