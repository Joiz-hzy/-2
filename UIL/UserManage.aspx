<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserManage.aspx.cs" Inherits="UIL.UserManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <!--第一步：引入Easy UI所需的样式表文件-->
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
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <%--   <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>--%>
 
    <script type="text/javascript">
        //文档就绪函数
        //        $(function () {
        //            //通过jQuery找按钮“添加”
        //            $("#btn_Add").click(function () {
        //                //第一种  
        //                $("#dlg").dialog("setTitle", "新增用户").dialog("open");
        //                //第二种(不需要再div里面修改宽高)    $("#dlg").dialog({ width: 500, height: 300 }).dialog("setTitle", "新增用户").dialog("open");
        //                $("#frm").attr("src", "UserEdit.aspx");  //通过attr函数给iframe标签的属性src赋值
        //            });
        //        });
        //   function UpdateUserInfo(userID) {
        //            $("#dlg").dialog("setTitle", "编辑用户信息").dialog("open");
        //            $("#frm").attr("src", "UserEdit.aspx?UserID=" + userID);  //通过attr函数给iframe标签的属性src赋值
        //        }
        //  <!--新增更新渐变窗口-->
        function addUser() {
            dialog.show("UserEdit.aspx?type=add", "新增用户", 540, 320);
        }
        //修改
        function UpdateUserInfo(userID) {
            dialog.show("UserEdit.aspx?type=edit&userID=" + userID, "编辑用户信息", 540, 320);
        }
        //删除
        function deleteUsers() {
            var users = [];
            $("#ctl00_ContentPlaceHolder1_GridView1 :checkbox:not(#chkAll)").each(function () {
                if (this.checked)
                    users.push(this.value);
            });
            if (users.length == 0) {
                alert("请选择要删除的用户！");
                return false;
            }
            if (confirm("确定要删除选择的用户吗？")) {
                $("#ctl00_ContentPlaceHolder1_hidUserIDs").val(users.toString());
                return true;
            }
            return false;
        }      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table align="center">
        <tr>
            <td>
                用户ID：<asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
            </td>
            <td>
                用户名：<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            </td>
            <td>
                部门：    
                <asp:DropDownList ID="ddlDepart" runat="server" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/image/common/btnSearch.gif"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <hr />
    <table align="center" width="80%" style="border-collapse: collapse;">
        <!--该表格显示的宽度-->
        <tr>
            <td>
                <input id="btn_Add" type="button" value="添加" class="btn" onclick="addUser();" />
                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btn" OnClick="btnDel_Click"
                    OnClientClick="return confirm('你确定要删除吗？')" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvUserInfo" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvUserInfo_RowDataBound"
                    CssClass="grid" Width="100%" AllowSorting="True" OnSorting="gvUserInfo_Sorting"
                    AllowPaging="True" OnPageIndexChanging="gvUserInfo_PageIndexChanging" DataKeyNames="UserID"
                    BorderStyle="None">
                    <Columns>
                        <asp:BoundField HeaderText="序号" />
                        <asp:TemplateField HeaderText="全选">
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="CheckBox2_CheckedChanged"
                                    AutoPostBack="True" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="用户ID" DataField="UserID" SortExpression="UserID" />
                        <asp:BoundField DataField="UserName" HeaderText="用户姓名" SortExpression="UserName" />
                        <asp:BoundField DataField="DeptName" HeaderText="部门" SortExpression="DeptName" />
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" Height="20px" ImageUrl="~/image/common/edit.png"
                                    Width="20px" ToolTip="修改" onclick='<%# "UpdateUserInfo("+Eval("UserID")+")"  %>' />
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
                    OnClick="btnFirstPage_Click1" />
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
    <%-- <div id="dlg" class="easyui-dialog" title="My Dialog" style="width: 400px; height: 300px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <iframe id="frm" width="99%" height="99%" frameborder="0"></iframe>
    </div>--%>
</asp:Content>
