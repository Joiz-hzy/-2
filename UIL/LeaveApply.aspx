<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LeaveApply.aspx.cs" Inherits="UIL.LeaveApply" %>

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

    <link href="css/myCalendar.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/myCalendar.js" type="text/javascript"></script>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
   <%-- <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>--%>
    <script src="Scripts/SVSE.UI.js" type="text/javascript"></script>
    <script type="text/javascript">
        //请假
        $(function () {
            //通过jQuery找按钮“添加”===>请假
            $("#btnLeaveApply").click(function () {
//                $("#dlg1").dialog({ width: 700, height: 250 }).dialog("setTitle", "请假申请").dialog("open");
//                $("#frm1").attr("src", "LeaveApplyEdit.aspx"); //通过attr函数  将iframe标签的属性值src赋值
                dialog.show("LeaveApplyEdit.aspx?type=add", "请假申请", 700, 320);
            });
          
        });
        //修改
        function UpdateApproveInfo(approveID) {
            $("#dlg2").dialog("setTitle", "请假单详情").dialog("open");
            $("#frm2").attr("src", "LeaveApplyEdit.aspx?ApproveID=" + approveID);  //通过attr函数给iframe标签的属性src赋值
        }
        //查看
        function ShowApproveInfo(approveID) {
            $("#dlg2").dialog("setTitle", "请假单详情").dialog("open");
            $("#frm2").attr("src", "LeaveApproveEdit.aspx?ApproveID=" + approveID);  //通过attr函数给iframe标签的属性src赋值
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table align="center">
        <tr>
            <td>
                标题：<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </td>
            <td>
                申请时间：
            </td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" onclick="Calendar.show(this);"></asp:TextBox>&nbsp;到：<asp:TextBox
                    ID="txtToDate" runat="server" onclick="Calendar.show(this);"></asp:TextBox>
            </td>
            <td>
                请假单状态：<asp:DropDownList ID="ddlLeaveState" runat="server" AppendDataBoundItems="True">
                    <asp:ListItem Value="2">全部</asp:ListItem>
                    <asp:ListItem Value="0">待办</asp:ListItem>
                    <asp:ListItem Value="1">归档</asp:ListItem>
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
                <input id="btnLeaveApply" type="button" value="请假" class="btn" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvApproveInfo" runat="server" CssClass="grid" AutoGenerateColumns="False"
                    OnRowDataBound="gvApproveInfo_RowDataBound" OnRowCommand="gvApproveInfo_RowCommand"
                    Width="100%" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="ApproveID" HeaderText="请假单ID" />
                        <asp:HyperLinkField DataTextField="UserName" HeaderText="申请人" />
                        <asp:BoundField DataField="Title" HeaderText="标题" />
                        <asp:HyperLinkField DataTextField="BeginDate" HeaderText="起始时间" />
                        <asp:HyperLinkField DataTextField="EndDate" HeaderText="结束时间" />
                        <asp:HyperLinkField DataTextField="ApplyDate" HeaderText="申请时间" />
                        <asp:TemplateField HeaderText="请假单状态">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" ShowHeader="False">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlShow" runat="server" ForeColor="Blue" NavigateUrl="#" 
                                    onclick='<%# "ShowApproveInfo("+Eval("ApproveID")+")"  %>' ToolTip="查看">查看</asp:HyperLink>
                                <asp:Image ID="imgUpdate" runat="server" Height="20px" 
                                    ImageUrl="~/image/common/edit.png" 
                                    onclick='<%# "UpdateApproveInfo("+Eval("ApproveID")+")"  %>' ToolTip="修改" 
                                    Width="20px" />
                                <asp:ImageButton ID="delApprove" runat="server" 
                                    CommandArgument='<%# Eval("ApproveID") %>' CommandName="DeleteApproveInfo" 
                                    Height="20" ImageUrl="~/image/common/delete.png" 
                                    OnClientClick="return confirm('你确定要删除吗？');" ToolTip="删除" Width="20" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        没有出现数据！
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
                <asp:ImageButton ID="btnFirstPage" runat="server" 
                    ImageUrl="image/DataPager/begin.gif" onclick="btnFirstPage_Click1" 
                    CssClass="style1" />
            </td>
            <td>
                <!--上一页-->
                <asp:ImageButton ID="btnPrePage" runat="server" 
                    ImageUrl="image/DataPager/last.gif" onclick="btnPrePage_Click"/>
            </td>
            <td>
                <!--跳转-->
                <asp:DropDownList ID="ddlPageIndex" runat="server" 
                    onselectedindexchanged="ddlPageIndex_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <!--下一页-->
                <asp:ImageButton ID="btnNextPage" runat="server" 
                    ImageUrl="image/DataPager/next.gif" onclick="btnNextPage_Click" />
            </td>
            <td>
                <!--尾页-->
                <asp:ImageButton ID="btnEndPage" runat="server" 
                    ImageUrl="image/DataPager/end.gif" onclick="btnEndPage_Click" />
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
    <div id="dlg1" class="easyui-dialog" title="My Dialog"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <iframe id="frm1" width="99%" height="99%" frameborder="0"></iframe>
    </div>
    <div id="dlg2" class="easyui-dialog" title="My Dialog" style="width: 700px; height: 500px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <iframe id="frm2" width="99%" height="99%" frameborder="0"></iframe>
    </div>
</asp:Content>
