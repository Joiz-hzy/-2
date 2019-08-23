<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DepartmentManage.aspx.cs" Inherits="UIL.DepartmentManage" %>

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
    <%-- <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>--%>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //文档就绪函数
        $(function () {
            //通过jQuery找按钮“添加”  ==》通过空间ID
            $("#btnAdd").click(function () {
                $("#dlg").dialog("setTitle", "新建部门").dialog("open");
                $("#frm").attr("src", "DepartmentEdit.aspx"); //通过attr函数  将iframe标签的属性值src赋值
            });
        });
        function UpdateDeptInfo(deptID) {    // ==》通过按钮
            $("#dlg").dialog("setTitle", "修改部门信息").dialog("open");
            $("#frm").attr("src", "DepartmentEdit.aspx?DeptID=" + deptID); //通过attr函数  将iframe标签的属性值src赋值
        }
        function btnAdd_onclick() {

        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table align="center">
        <tr>
            <td>
                部门名称：
                <asp:TextBox ID="txtDeptName" runat="server"></asp:TextBox>
            </td>
            <td>
                主管：<asp:DropDownList ID="ddlCharge" runat="server" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/image/common/btnSearch.gif"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <hr />
    <div class="gridTopDiv">
        <input id="btnAdd" type="button" value="添加" class="btn" />
    </div>
    <div>
        <table align="center" width="80%"  style="border-collapse: collapse;">
            <!--该表格显示的宽度-->
            <tr>
                <td>
                    <asp:GridView ID="gvDeptInfo" runat="server" CssClass="grid" AutoGenerateColumns="False"
                        OnRowDataBound="gvDeptInfo_RowDataBound" OnRowCommand="gvDeptInfo_RowCommand"
                        Width="100%" AllowPaging="True">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DeptName" HeaderText="部门名称" />
                            <asp:BoundField DataField="UserName" HeaderText="主管" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="imgUpdate" runat="server" ImageUrl="~/image/common/edit.png" ToolTip="修改"
                                        Width="20" Height="20" onclick='<%# "UpdateDeptInfo("+Eval("DeptID")+")" %>' />
                                    <asp:ImageButton ID="imgBtnDel" runat="server" ImageUrl="~/image/common/delete.png"
                                        ToolTip="删除" Width="20" Height="20" CommandName="DeleteDeptInfo" CommandArgument='<%# Eval("DeptID") %>'
                                        OnClientClick="return confirm('你确定要删除吗？');" />
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
    </div>
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
    <div id="dlg" class="easyui-dialog" title="My Dialog" style="width: 400px; height: 300px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <iframe id="frm" width="99%" height="99%" frameborder="0"></iframe>
    </div>
</asp:Content>
