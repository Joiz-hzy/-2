<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LeaveApprove.aspx.cs" Inherits="UIL.LeaveApprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <!--第一步：引入Easy UI所需的样式表文件-->
    <link href="css/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <!--第二步：引入Easy UI所需的图标样式表文件-->
    <link href="css/themes/icon.css" rel="stylesheet" type="text/css" />
    <!--第三步：引入jQuery所需的js文件-->
    <script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
    <!--第四步：引入Easy UI所需的js文件-->
    <script src="Scripts/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <!--第五步：引入Easy UI所需的汉化包js文件-->
    <script src="Scripts/easyui/easyui-lang-zh_CN.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        //查看
        function SelectApproveInfo(approveID) {
            $("#dlg").dialog("setTitle", "请假单详情").dialog("open");
            $("#frm").attr("src", "LeaveApproveEdit.aspx?ApproveID=" + approveID);  //通过attr函数给iframe标签的属性src赋值
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
                <asp:TextBox ID="txtFromDate" runat="server" class="easyui-datebox"></asp:TextBox>&nbsp;到：<asp:TextBox
                    ID="txtToDate" runat="server" class="easyui-datebox"></asp:TextBox>
            </td>
            <td>
                请假单状态：
            </td>
            <td>
                <asp:RadioButtonList ID="rblLeaveState" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">待审批</asp:ListItem>
                    <asp:ListItem Value="1">归档</asp:ListItem>
                    <asp:ListItem Value="2">全部</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                申请人：<asp:TextBox ID="txtApplyUser" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/image/common/btnSearch.gif"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <hr />
    <table align="center" width="80%">
        <!--该表格显示的宽度-->
        <tr>
            <td>
                <asp:GridView ID="gvApproveInfo" runat="server" CssClass="grid" AutoGenerateColumns="False"
                    Width="100%" OnRowDeleting="gvApproveInfo_RowDeleting" 
                    onrowdatabound="gvApproveInfo_RowDataBound" DataKeyNames="ApproveID" 
                    onrowcancelingedit="gvApproveInfo_RowCancelingEdit" 
                    onrowediting="gvApproveInfo_RowEditing" 
                    onrowupdating="gvApproveInfo_RowUpdating" 
                    onrowcommand="gvApproveInfo_RowCommand" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="ApproveID" HeaderText="请假单ID" />
                        <asp:HyperLinkField DataTextField="UserName" HeaderText="申请人" />
                        <asp:BoundField DataField="Title" HeaderText="标题" />
                        <asp:BoundField DataField="BeginDate" DataFormatString="{0:F}" 
                            HeaderText="起始时间" HtmlEncode="False" />
                        <asp:BoundField DataField="EndDate" DataFormatString="{0:F}" HeaderText="结束时间" 
                            HtmlEncode="False" />
                        <asp:BoundField DataField="ApplyDate" DataFormatString="{0:F}" 
                            HeaderText="申请时间" HtmlEncode="False" />
                        <asp:BoundField DataField="Status" HeaderText="请假单状态" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="查看" NavigateUrl="#" ForeColor="Blue"
                                    onclick='<%# "SelectApproveInfo("+Eval("ApproveID")+")"  %>'>查看</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>
                        没有出现数据！
                    </EmptyDataTemplate>
                    <PagerSettings Visible="False" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="ckShow" runat="server" AutoPostBack="True" OnCheckedChanged="ckShow_CheckedChanged" />显示隐藏删除功能
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
    <div id="dlg" class="easyui-dialog" title="My Dialog" style="width: 700px; height: 530px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <iframe id="frm" width="99%" height="99%" frameborder="0">
        </iframe>
    </div>
</asp:Content>
