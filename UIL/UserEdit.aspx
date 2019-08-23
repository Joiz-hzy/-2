<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="UIL.UserEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户管理</title>
     <script src="Scripts/SVSE.UI.js" type="text/javascript"></script>
    <link href="css/dialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
     <script type="text/javascript">
         $(function () {
             $("#btnSave").click(function () {
                 var bln = true;
                 if ($("#txtUserID").val() == "") {
                     msgbox.show("txtUserID", "请填写用户ID");
                     bln = false;
                 }
                 if ($("#txtUserName").val() == "") {
                     msgbox.show("txtUserName", "请填写用户姓名");
                     bln = false;
                 }
                 if ($("#ddlUserType").val() == "") {
                     msgbox.show("ddlUserType", "请选择用户类型");
                     bln = false;
                 }
                 return bln;
             });
         });
         $(function () {
             $("#btnModify").click(function () {
                 var bln = true;
                 if ($("#txtUserID").val() == "") {
                     msgbox.show("txtUserID", "请填写用户ID");
                     bln = false;
                 }
                 if ($("#txtUserName").val() == "") {
                     msgbox.show("txtUserName", "请填写用户姓名");
                     bln = false;
                 }
                 if ($("#ddlUserType").val() == "") {
                     msgbox.show("ddlUserType", "请选择用户类型");
                     bln = false;
                 }
                 return bln;
             });
         });
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" cellpadding="5" cellspacing="5">
        <tr>
            <td align="right">
                *用户ID:
            </td>
            <td>
                <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                *用户姓名：
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            </td>
            <td>
               <%--
                   
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="请输入中文的用户姓名"
                    ControlToValidate="txtUserName" ValidationExpression="^[\u4e00-\u9fa5],{0,}$"></asp:RegularExpressionValidator>
                     --%>
           
            </td>
        </tr>
        <tr>
            <td align="right">
                *用户类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlUserType" runat="server" AppendDataBoundItems="True">
                    <asp:ListItem Value="">--请选择--</asp:ListItem>
                    <asp:ListItem Value="0">员工</asp:ListItem>
                    <asp:ListItem Value="1">主管</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                所属部门：
            </td>
            <td>
                <asp:DropDownList ID="ddlBelongsDept" runat="server" AppendDataBoundItems="True">
                    <asp:ListItem>--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="right">
                手机：
            </td>
            <td>
                <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AttendanceConnectionString %>"
                    DeleteCommand="DELETE FROM [UserInfo] WHERE [UserID] = @UserID" InsertCommand="INSERT INTO [UserInfo] ([UserID], [UserName], [DeptID], [Password], [Cellphone], [UserType]) VALUES (@UserID, @UserName, @DeptID, @Password, @Cellphone, @UserType)"
                    SelectCommand="SELECT * FROM [UserInfo]" UpdateCommand="UPDATE [UserInfo] SET [UserName] = @UserName, [DeptID] = @DeptID, [Password] = @Password, [Cellphone] = @Cellphone, [UserType] = @UserType WHERE [UserID] = @UserID">
                    <DeleteParameters>
                        <asp:Parameter Name="UserID" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="UserID" Type="String" />
                        <asp:Parameter Name="UserName" Type="String" />
                        <asp:Parameter Name="DeptID" Type="Int32" />
                        <asp:Parameter Name="Password" Type="String" />
                        <asp:Parameter Name="Cellphone" Type="String" />
                        <asp:Parameter Name="UserType" Type="Byte" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="UserName" Type="String" />
                        <asp:Parameter Name="DeptID" Type="Int32" />
                        <asp:Parameter Name="Password" Type="String" />
                        <asp:Parameter Name="Cellphone" Type="String" />
                        <asp:Parameter Name="UserType" Type="Byte" />
                        <asp:Parameter Name="UserID" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="btn" />
                &nbsp;
                <asp:Button ID="btnModify" runat="server" CssClass="btn" OnClick="btnModify_Click"
                    Text="修改" />
                <input id="btnCancel" type="button" value="取消" onclick="parent.dialog.close();" 
                    class="btn" />           
            </td>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
