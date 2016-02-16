<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LFCRM.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>



&nbsp;<table>
                <tr>
                    <td class="auto-style1">
                        <table id='title' style='width: 380px; border-collapse: collapse; text-align:center;' border='1' >
                                        <tr><td style='width: 200px;'><b>3LD</b></td><td><b>Bill</b></td><td><b>Actual</b></td></tr>
                                        <tr><td>1</td><td>2</td><td>3</td></tr>
                                    </table>
                    </td>
                    <td rowspan="2">
                        &nbsp;&nbsp;&nbsp;
                    </td>
                    <td rowspan="2" valign="top">
                        <table id='resource' style='width: 800px; border-collapse: collapse; text-align:center;' border='1'>
                            <tr><td><b>ID</b></td><td><b>Name</b></td><td><b>Role</b></td><td><b>Title</b></td><td style='width: 150px;'><b>Working Hours</b></td></tr>
                            <tr style='border: thin solid #FF0000;'><td>1</td><td>2</td><td>3</td><td>4</td><td>5</td></tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>
                        <table id='headcount' style='width: 380px; border-collapse: collapse; text-align:center;' border='1'>
                                        <tr><td><strong>Headcount</strong></td><td style="color: red"><b>1</b></td></tr>
                                        <tr><td>Total Billing</td><td><b>2</b></td></tr>
                                        <tr><td>Total Assigned</td><td style="color: #33CC33"><b>3</b></td></tr>
                                        <tr><td>Total Trainee & Trainer</td><td><b>4</b></td></tr>
                                        <tr><td>Off</td><td><b>5</b></td></tr>
                                    </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
