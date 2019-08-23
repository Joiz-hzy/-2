﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UIL.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户登录</title>
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('.rem').click(function () {
                $(this).toggleClass('selected');
            })

            $('#signup_select').click(function () {
                $('.form_row ul').show();
                event.cancelBubble = true;
            })

            $('#d').click(function () {
                $('.form_row ul').toggle();
                event.cancelBubble = true;
            })

            $('body').click(function () {
                $('.form_row ul').hide();
            })

            $('.form_row li').click(function () {
                var v = $(this).text();
                $('#signup_select').val(v);
                $('.form_row ul').hide();
            })
            $(".login-btn").click(function () {
                $("form").submit();
            });
        })
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class='signup_container'>
        <h1 class='signup_title'>
            用户登陆</h1>
        <img src="image/login/people.png" id='admin' />
        <div id="signup_forms" class="signup_forms clearfix">
            <div class="form_row first_row">
                <label for="U_LoginName">
                    请输入用户名</label>
                    <asp:TextBox ID="txtID" runat="server" placeholder="请输入用户名" data-required="required" ></asp:TextBox>
            </div>
            <div class="form_row">
                <label for="U_Password">
                    请输入密码</label>
                    <asp:TextBox ID="txtPassword" runat="server" placeholder="请输入密码" 
                    data-required="required" TextMode="Password"></asp:TextBox>
            </div>
        </div>
        <div class="login-btn-set">
            <asp:ImageButton ID="ibLogin" runat="server"  
                ImageUrl="~/image/login/login_btn.png" onclick="ibLogin_Click" />
        </div>
    </div>
    </form>
</body>
</html>