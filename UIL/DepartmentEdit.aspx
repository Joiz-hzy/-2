<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentEdit.aspx.cs" Inherits="UIL.DepartmentEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部门管理</title>
    <script src="Scripts/SVSE.UI.js" type="text/javascript"></script>
    <link href="css/dialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script type="text/javascript">
          $(function () {
              $("#btnSave").click(function () {
                        var bln = true;
                        if ($("#txtDeptName").val() == "") {
                            msgbox.show("txtDeptName", "请填写部门名称");
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
    
        <table align="center" cellpadding="5" cellspacing="5">
            <tr>
                <td align="right">
                   *部门名称：</td>
                <td>
                    <asp:TextBox ID="txtDeptName" runat="server"></asp:TextBox>
                   </td>
            </tr>
            <tr>
                 <td align="right">
                    主管：</td>
                <td>
                    <asp:DropDownList ID="ddlCharge" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem>请选择</asp:ListItem>
                    </asp:DropDownList>
                    
                    </td>
            </tr>
            <tr>
                 <td align="right">
                   部门说明：</td>
                <td>
                  <asp:TextBox ID="txtDeptInfo" runat="server" Height="31px" 
                        TextMode="MultiLine" EnableTheming="True" Columns="30" Enabled="False" 
                        Rows="2"></asp:TextBox>                   
                   </td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:AttendanceConnectionString %>" 
                        DeleteCommand="DELETE FROM [Department] WHERE [DeptID] = @DeptID" 
                        InsertCommand="INSERT INTO [Department] ([DeptName], [ManagerID], [DeptInfo]) VALUES (@DeptName, @ManagerID, @DeptInfo)" 
                        SelectCommand="SELECT * FROM [Department]" 
                        UpdateCommand="UPDATE [Department] SET [DeptName] = @DeptName, [ManagerID] = @ManagerID, [DeptInfo] = @DeptInfo WHERE [DeptID] = @DeptID">
                        <DeleteParameters>
                            <asp:Parameter Name="DeptID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="DeptName" Type="String" />
                            <asp:Parameter Name="ManagerID" Type="String" />
                            <asp:Parameter Name="DeptInfo" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="DeptName" Type="String" />
                            <asp:Parameter Name="ManagerID" Type="String" />
                            <asp:Parameter Name="DeptInfo" Type="String" />
                            <asp:Parameter Name="DeptID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" 
                        CssClass="btn" />&nbsp;<asp:Button ID="btnModify" runat="server" 
                        CssClass="btn" onclick="btnModify_Click" Text="修改" />
&nbsp;<asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="btn" 
                        onclick="btnCancel_Click" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
