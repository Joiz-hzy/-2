<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceImport.aspx.cs"
    Inherits="UIL.AttendanceImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考勤导入</title>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/dialog.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/SVSE.UI.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <style type="text/css">
        .file
        {
            border: 1px solid #72a1bd;
            margin-left:5px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#btnImport").click(function () {
                var bln = true;
                if ($("#fuFile").val() == "") {
                    msgbox.show("fuFile", "请选择要导入的Excel文件");
                    bln = false;
                }
                return bln;
            });
        });
    </script>
</head>
<body>
    <center>
        <form id="form1" runat="server">
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:FileUpload ID="fuFile" runat="server" CssClass="file" />
                        <asp:Button ID="btnImport" runat="server" Text="导入" CssClass="btn" OnClick="btnImport_Click" />
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </center>
</body>
</html>
