<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="UIL.PersonalInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>个人信息</title>
    <link href="css/dialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/SVSE.UI.js" type="text/javascript"></script>
    <!--温馨提示-->
    <script type="text/javascript">
        $(function () {
            $("#btnSave").click(function () {
                var bln = true;
                var pwd1 = $("#txtNewPwd").val(), pwd2 = $("#txtAgainPwd").val();
                if (pwd1 == "" && pwd2 == "") {
                    return true;
                }
                if (pwd1 == "") {
                    msgbox.show("txtNewPwd", "请填写新密码");
                    return false;
                } else if (pwd2 == "") {
                    msgbox.show("txtAgainPwd", "请确认新密码");
                    return false;
                }
                if (pwd1 != pwd2) {
                    msgbox.show("txtNewPwd", "两次密码填写不相同");
                    msgbox.show("txtAgainPwd", "两次密码填写不相同");
                    return false;
                }
                return true;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" cellpadding="5" cellspacing="5">
            <tr>
                <td align="right">
                    新密码：
                </td>
                <td>
                    <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    确认密码：
                </td>
                <td>
                    <asp:TextBox ID="txtAgainPwd" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    手机：
                </td>
                <td>
                    <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn" OnClick="btnSave_Click" />
                     <input id="btnCancel" type="button" value="取消" onclick="parent.dialog.close();" 
                    class="btn" />  
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>