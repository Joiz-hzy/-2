<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AttendanceSetting.aspx.cs" Inherits="UIL.AttendanceSetting" %>
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
                年：
            </td>
            <td>
                <asp:DropDownList ID="ddlMonth" runat="server">
                </asp:DropDownList>
                月：
            </td>
            <td>
                <asp:Button ID="btnShow" runat="server" Text="显示" OnClick="btnShow_Click" CssClass="btn" />
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <hr />
    <asp:GridView ID="gvAttendSettings" runat="server" CssClass="grid" 
        AutoGenerateColumns="False" OnRowDataBound="gvAttendSettings_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="日期">
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server" Text='<%#((DateTime)Eval("Date")).ToString("D") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="星期">
                <ItemTemplate>
                    <%#((DateTime)Eval("Date")).ToString("dddd") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="0" style="color: Black;">默认</asp:ListItem>
                        <asp:ListItem Value="1" style="color: Green;">上班</asp:ListItem>
                        <asp:ListItem Value="2" style="color: Red;">休假</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerSettings Visible="False" />
    </asp:GridView>
    <p></p>
    <br />
</asp:Content>
