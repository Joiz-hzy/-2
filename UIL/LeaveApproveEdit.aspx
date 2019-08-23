<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveApproveEdit.aspx.cs"
    Inherits="UIL.LeaveApproveEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>请假审批</title>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;&nbsp;
        <fieldset>
            <legend dir="rtl">请假信息 </legend>
            <table align="center" cellpadding="3" cellspacing="3">
                <%Model.UserInfo u = (Model.UserInfo)Session["User"]; %>
                <tr>
                    <td align="right">
                        请假单号：
                    </td>
                    <td>
                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        申请人：
                    </td>
                    <td>
                        <asp:Label ID="lblApply" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        *标题：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtTitle" runat="server" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        *起始时间：
                    </td>
                    <td>
                        <asp:TextBox ID="txtBeginTime" runat="server" class="easyui-datebox"></asp:TextBox>
                        <asp:DropDownList ID="ddlBegin" runat="server" AppendDataBoundItems="True">
                            <asp:ListItem>8:30</asp:ListItem>
                            <asp:ListItem>11:50</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        *结束时间：
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndTime" runat="server" class="easyui-datebox"></asp:TextBox>
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
                        <asp:TextBox ID="txtLeaveReason" runat="server" Width="500px" TextMode="MultiLine"
                            ViewStateMode="Enabled"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>审批信息 </legend>
            <table align="center" cellpadding="3" cellspacing="3">
                <tr>
                    <td>
                        申请时间：<asp:Label ID="lblApplyTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        *审批结果：<asp:DropDownList ID="ddlApproveResult" 
                            runat="server" AppendDataBoundItems="True">
                            <asp:ListItem Selected="True" Value="2">请选择</asp:ListItem>
                            <asp:ListItem Value="1">同意</asp:ListItem>
                        <asp:ListItem Value="0">不同意</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="请选择审批结果" 
                            Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        审批人：
                        <%=u.UserName %>
                    </td>
                </tr>
                <tr>
                    <td>
                        审批时间：<asp:Label ID="lblApproveTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        备注：<asp:TextBox 
                            ID="txtBackup" runat="server" TextMode="MultiLine" CausesValidation="True"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <center>
            <asp:Button ID="btnOK" runat="server" Text="确定" CssClass="btn" OnClick="btnOK_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="btn" OnClick="btnCancel_Click" />
        </center>
    </div>
    </form>
</body>
</html>
