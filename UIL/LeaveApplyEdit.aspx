<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveApplyEdit.aspx.cs"
    Inherits="UIL.LeaveApplyEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>请假申请</title>
      <!--第一步：引入Easy UI所需的样式表文件-->
    <link href="css/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <!--第二步：引入Easy UI所需的图标样式表文件-->
    <link href="css/themes/icon.css" rel="stylesheet" type="text/css" />
    <!--第三步：引入jQuery所需的js文件-->
    <script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
    <!--第四步：引入Easy UI所需的js文件-->
    <script src="Scripts/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <!--第五步：引入Easy UI所需的汉化包js文件-->
    <script src="Scripts/easyui/easyui-lang-zh_CN.js" type="text/javascript"></script>


  <link href="css/myCalendar.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/myCalendar.js" type="text/javascript"></script>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
      <link href="css/dialog.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/SVSE.UI.js" type="text/javascript"></script>
       <!--温馨提示-->
    <script type="text/javascript">
       //确定
        $(function () {
            $("#btnOK").click(function () {
                var bln = true;
                if ($("#txtTitle").val() == "") {
                    msgbox.show("txtTitle", "标题为必填项");
                    bln = false;
                }
                if ($("#txtBeginTime").val() == "") {
                    msgbox.show("txtBeginTime", "请输入开始时间");
                    bln = false;
                }
                if ($("#txtEndTime").val() == "") {
                    msgbox.show("txtEndTime", "请输入结束时间");
                    bln = false;
                }
                if ($("#txtLeaveReason").val() == "") {
                    msgbox.show("txtLeaveReason", "请输入请假原因");
                    bln = false;
                }
                return bln;
            });
        });
        //修改
        $(function () {
            $("#btnUpdate").click(function () {
                var bln = true;
                if ($("#txtTitle").val() == "") {
                    msgbox.show("txtTitle", "标题为必填项");
                    bln = false;
                }
                if ($("#txtBeginTime").val() == "") {
                    msgbox.show("txtBeginTime", "请输入开始时间");
                    bln = false;
                }
                if ($("#txtEndTime").val() == "") {
                    msgbox.show("txtEndTime", "请输入结束时间");
                    bln = false;
                }
                if ($("#txtLeaveReason").val() == "") {
                    msgbox.show("txtLeaveReason", "请输入请假原因");
                    bln = false;
                }
                return bln;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" cellpadding="3" cellspacing="3" style="table-layout: auto;
            display: table">
            <%
                Model.UserInfo u = (Model.UserInfo)Session["User"];
            %>
            <tr>
                <td align="right">
                    请假单号：
                </td>
                <td>
                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                </td>
                <td align="right">
                    申请人：<%=u.UserName %>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    *标题：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtTitle" runat="server" Width="550px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    *起始时间：
                </td>
                <td>
                    <asp:TextBox ID="txtBeginTime" runat="server" onclick="Calendar.show(this);"></asp:TextBox>
                    <asp:DropDownList ID="ddlBegin" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem>8:30</asp:ListItem>
                        <asp:ListItem>11:50</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    *结束时间：
                </td>
                <td>
                    <asp:TextBox ID="txtEndTime" runat="server" onclick="Calendar.show(this);"></asp:TextBox>
                    <asp:DropDownList ID="ddlEnd" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem>8:30</asp:ListItem>
                        <asp:ListItem>11:50</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    *请假原因：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtLeaveReason" runat="server" Width="550px" TextMode="MultiLine"
                        ViewStateMode="Enabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnOK" runat="server" Text="确定" CssClass="btn" OnClick="btnOK_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="修改" CssClass="btn" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="btn" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
