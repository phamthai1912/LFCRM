<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LFCRM.Login1" %>

<!DOCTYPE html>
<!-- saved from url=(0041)http://claimdn.logigear.com/Account/Login -->
<html lang="en"><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link rel="shortcut icon" id="favicon" href="http://claimdn.logigear.com/Content/Themes/Default/images/favicon.ico">
    <title>LFCRM - Login</title>
    <style>
        * {
            font-family: Arial;
            
        }

        body {
            background-color: #3177aa;
        }

        a, a:link {
            color: #033b75;
        }

        a:visited {
            color: #3173b4;
        }

        input {
            padding: 5px;
            color: black;
        }

        hr {
            margin: 3px 0;
        }
        #wrapper {
            margin: 0px auto;
            width: 100%;
        }

        #container {
            margin: 0px auto;
            width: 500px;
            position: relative;
            margin-top: 100px;
        }

        #logo {
            text-align: center;
            margin-bottom: 10px;
        }

        #clogin-form {
            width: 500px;
            height: auto;
            background: url(../images/frame/bg-login-form.png) repeat left top;
            box-shadow: 0 0px 20px #1d538a;
            border-radius: 6px;
            -moz-border-radius: 6px;
            -webkit-border-radius: 6px;
            background-color: #1e5d89;
        }

            #clogin-form .content {
                padding: 35px;
                color: #79b9f7;
                font-weight: bold;
            }

        .content .header {
            font-size: 11pt;
            margin-bottom: 30px;
            color: #d7edfc;
        }

        .content .form {
            margin-bottom: 40px;
        }

        .content .form .box {
            float: left;
            margin-right: 25px;
            width: 200px;
            margin-bottom: 20px;
        }

        .content .form .box.remember {
            color: #d7edfc;
            clear: left;
            display: block;
            width: 100%;
        }

        .content .form .box + .box {
            margin-right: 0px;
        }

        .content .form .tbox {
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border: 2px solid #79b9f7;
            background: #fff;
            width: 190px;
        }

        .content .form .tbox.error {
            border-color: #cc0000;
        }

        .content .form .box-button {
            clear: both;
            display: block;
            margin-bottom: 20px;
        }

        .content .form .errorMessage {
            display: block;
            padding-left: 25px;
            color: #fff;
            height: 25px;
            line-height: 25px;
            background: #cc0000 url(../images/icons-16/error.png) no-repeat 5px center;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
        }

        .button-login {
            border-radius: 6px;
            cursor: pointer;
            display: inline-block;
            font: 11px sans-serif;
            margin: 0;
            font-size: 1em;
            outline: none;
            overflow: visible;
            padding: 0.7em 4em;
            position: relative;
            white-space: nowrap;
            background-color: #3C8DDE;
            background-image: -moz-linear-gradient(#599BDC, #3072B3);
            border: 1px solid #d4d4d4;
            border-color: #3072B3 #3072B3 #2A65A0;
            color: #FFFFFF;
            text-decoration: none;
            text-shadow: -1px -1px 0 rgba(0, 0, 0, 0.3);
        }

        .button-login span {
            background: url(../images/frame/login-icon.png) no-repeat left center;
            padding-left: 17px;
        }

        .button-login:hover {
            background-color: #3072B3;
            background-image: -moz-linear-gradient(#599BDC, #2a65a0);
            border: 1px solid #2a65a0;
        }

        .field-validation-error {
            color: #ff6d6d;
            margin-top: 15px;
        }
        #summaryMessage{
            color: #ff6d6d !important;
        }
    </style>
</head>
<body>



    <div id="wrapper">
        <div id="container">
            <div id="clogin-form">
                <div class="content">
                    <h4 style="color:white">Welcome to LeapFrog Content Resource Management</h4>
                    <div class="form">
                        <form id="login_form" runat="server">
                            <input name="__RequestVerificationToken" type="hidden" value="LmKs9_12IvJ1zouLBMWS15VM4yj0-tUCDSYT552yHVEqC0WC6b7O27K5k5_pAXZRdpmWRDO9QEAc3ktGjCwctjyf-RaRoa7GlsRNUgPsvZM1">                            <div class="box">
                                Employee ID<label for="LoginForm_username" class="required"> <span class="required">*</span></label>
                                <asp:TextBox ID="txt_EmployeeID" runat="server" class="tbox" placeholder="Employee ID"></asp:TextBox>
                            </div>
                            <div class="box">
                                <label for="LoginForm_password" class="required">Password <span class="required">*</span></label>
                                <asp:TextBox ID="txt_Password" runat="server" class="tbox" placeholder="Password" type="password"></asp:TextBox>
                            </div>
                            <div class="box remember">
                                <input data-val="true" data-val-required="The Remember me field is required." id="IsRememberMe" name="IsRememberMe" type="checkbox" value="true"><input name="IsRememberMe" type="hidden" value="false">
                                <label for="IsRememberMe" style="font-weight:normal">Remember me</label>
                            </div>
                            <div class="box-button">
                                <asp:Button ID="btn_login" runat="server" Text="Login" class="button-login" OnClick="btn_login_Click"/>
                            </div>
                            <div class="box-error">
                                <div class="errorMessage" id="LoginForm_errorMessage_em_" style="display:none"></div>
                                <asp:Label ID="lbl_error" runat="server" ForeColor="Red" Text="Invalid Employee ID or Password"></asp:Label>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>




</body></html>
