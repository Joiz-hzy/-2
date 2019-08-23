<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AttendanceManage.aspx.cs" Inherits="UIL.AttendanceManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  <!--第一步：引入Easy UI所需的样式表文件-->
    <link href="css/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <!--第二步：引入Easy UI所需的图标样式表文件-->
    <link href="css/themes/icon.css" rel="stylesheet" type="text/css" />
    <!--第三步：引入jQuery所需的js文件-->
    <script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
    <!--第四步：引入Easy UI所需的js文件-->
    <script src="Scripts/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <!--第五步：引入Easy UI所需的汉化包js文件-->
    <script src="Scripts/easyui/easyui-lang-zh_CN.js" type="text/javascript"></script>--%>

    <script src="Scripts/SVSE.UI.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //文档就绪函数
        $(function () {
            //通过jQuery找按钮“考勤导入”
            $("#btnImportAttend").click(function () {
//                $("#dlg").dialog("setTitle", "考勤导入").dialog("open");
                //                $("#frm").attr("src", "AttendanceImport.aspx"); //通过attr函数  将iframe标签的属性值src赋值
                dialog.show("AttendanceImport.aspx", "考勤导入", 360, 140);
            });
        });
        //查看
        function ShowAttendInfo(deptID) {
//            $("#dlg").dialog("setTitle", "查看考勤").dialog("open");
//            $("#frm").attr("src", "AttendanceShow.aspx?UserInfo.DeptID=" + deptID);  //通过attr函数给iframe标签的属性src赋值
            dialog.show("AttendanceShow.aspx?UserInfo.DeptID=" + escape(deptID), "查看考勤", 640, 400);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table align="center" width="80%" style="border-collapse: collapse;">
        <!--该表格显示的宽度-->
        <tr>
            <td>
                <input id="btnImportAttend" type="button" value="导入考勤数据" class="btn" />
                <asp:Button ID="btnExportAttend" runat="server" Text="导出考勤数据" class="btn" OnClick="btnExportAttend_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvAttendanceManage" runat="server" CssClass="grid" AutoGenerateColumns="False"
                    OnRowDataBound="gvAttendanceManage_RowDataBound" Width="100%" 
                    AllowPaging="True">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UserID" HeaderText="用户ID" />
                        <asp:BoundField DataField="UserName" HeaderText="用户名" />
                        <asp:BoundField DataField="DeptName" HeaderText="部门" />
                        <asp:TemplateField HeaderText="考勤信息">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" onclick='<%# "ShowAttendInfo("+Eval("DeptID")+")"  %>'>查看</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        没有数据！
                    </EmptyDataTemplate>
                    <PagerSettings Visible="False" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <!--分页-->
    <table align="center">
        <tr>
            <td>
                <!--首页-->
                <asp:ImageButton ID="btnFirstPage" runat="server" ImageUrl="image/DataPager/begin.gif"
                    OnClick="btnFirstPage_Click1" CssClass="style1" />
            </td>
            <td>
                <!--上一页-->
                <asp:ImageButton ID="btnPrePage" runat="server" ImageUrl="image/DataPager/last.gif"
                    OnClick="btnPrePage_Click" />
            </td>
            <td>
                <!--跳转-->
                <asp:DropDownList ID="ddlPageIndex" runat="server" OnSelectedIndexChanged="ddlPageIndex_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <!--下一页-->
                <asp:ImageButton ID="btnNextPage" runat="server" ImageUrl="image/DataPager/next.gif"
                    OnClick="btnNextPage_Click" />
            </td>
            <td>
                <!--尾页-->
                <asp:ImageButton ID="btnEndPage" runat="server" ImageUrl="image/DataPager/end.gif"
                    OnClick="btnEndPage_Click" />
            </td>
            <td style="font-size: 12px;">
                页数：<asp:Label ID="lblPageIndex" runat="server"></asp:Label>/<asp:Label ID="lblPageCount"
                    runat="server"></asp:Label>
            </td>
            <td style="font-size: 12px;">
                总记录数：<asp:Label ID="lblRowsCount" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="dlg" class="easyui-dialog" title="My Dialog" style="width: 400px; height: 640px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <iframe id="frm" width="99%" height="99%" frameborder="0"></iframe>
    </div>
</asp:Content>
