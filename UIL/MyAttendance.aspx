<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyAttendance.aspx.cs" Inherits="UIL.MyAttendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<table align="center">
        <tr>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>
                年：</td>
            <td>
                <asp:DropDownList ID="ddlMonth" runat="server">
                </asp:DropDownList>月：</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem>列表展示</asp:ListItem>
                    <asp:ListItem>日历展示</asp:ListItem>
                </asp:DropDownList> 
                </td>
            <td>
                <asp:Button ID="btnShow" runat="server" Text="查看" CssClass="btn" 
                    onclick="btnShow_Click" /></td>
        </tr>
    </table>
    <hr />
    <asp:GridView ID="gvMyAttend" runat="server" AutoGenerateColumns="False" 
        CssClass="grid" onrowdatabound="gvMyAttend_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="日期">
                <ItemTemplate>
                    <%#((DateTime)Eval("Date")).ToShortDateString() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="星期">
                <ItemTemplate>
                    <%#((DateTime)Eval("Date")).ToString("ddd") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="首次打卡时间">
                <ItemTemplate>
                    <asp:Label ID="lblFirstTime" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="最后打卡时间">
                <ItemTemplate>
                    <asp:Label ID="lblLastTime" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="考勤状态">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>
