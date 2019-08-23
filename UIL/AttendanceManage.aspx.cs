using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace UIL
{
    public partial class AttendanceManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindAttend();  
           }
          
        }
        //查询考勤信息
        private void BindAttend()
        {
            Model.UserInfo u = (Model.UserInfo)Session["User"];
            gvAttendanceManage.DataSource = BLL.AttendanceBll.SearchAttendInfo(u.DeptID);
            gvAttendanceManage.DataBind();
            //分页功能     
            List<int> a = new List<int>();
            for (int i = 1; i <= gvAttendanceManage.PageCount; i++)
            {
                a.Add(i);
            }
            ddlPageIndex.DataSource = a;
            ddlPageIndex.DataBind();
            lblPageCount.Text = gvAttendanceManage.PageCount.ToString(); //求的总页码数 
            lblRowsCount.Text = BLL.AttendanceBll.SearchAttendInfo(u.DeptID).Rows.Count.ToString();//总共数据量
            lblPageIndex.Text = Convert.ToString(gvAttendanceManage.PageIndex + 1);  //当前第几页 
        }
        int i=1;
        //序号
        protected void gvAttendanceManage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //光棒变色效果
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标放上去的时候 先保存当前行的背景颜色并给附一颜色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;w=this.style.fontWeight;this.style.backgroundColor='#E6F5FA';this.style.fontWeight='500';this.style.color='red'");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;this.style.fontWeight=w;this.style.color=''");
                //不能同时测试:单击/双击 事件, 要分别测试它们
                e.Row.Attributes.Add("OnDblClick", "DbClickEvent('" + e.Row.Cells[1].Text + "')");
                //e.Row.Attributes.Add("OnClick", "ClickEvent('" + e.Row.Cells[1].Text + "')");
                //e.Row.Attributes.Add("OnKeyDown", "GridViewItemKeyDownEvent('" + e.Row.Cells[1].Text + "')"); 
                e.Row.Attributes["style"] = "Cursor:hand";
            }
            //判断当前行是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //找控件lblNo
                Label lbl = (Label)e.Row.FindControl("lblNo");
                lbl.Text = i.ToString();
                i++;
            }
        }
        //必须要重写VerifyRenderingInServerForm方法
        public override void VerifyRenderingInServerForm(Control control)
        {
            //这里不需要编写任何代码
        }
        //导出考勤数据
        protected void btnExportAttend_Click(object sender, EventArgs e)
        {
            Export("application/ms-excel", "Attend.xls");

        }
        //导出Excel函数
        private void Export(string fileType, string fileName)
        {
            Response.Clear();
            Response.Charset = "gb2312";
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment;filename="+fileName);
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312"); ;
            Response.ContentType = fileType;
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.gvAttendanceManage.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void ImportExcel_Click(object sender, EventArgs e)
        {
            gvAttendanceManage.DataSource = createDataSource();
            gvAttendanceManage.DataBind();
        }
        private DataSet createDataSource()
        {
            string strCon;
            strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Files/Attend.xls") + ";Extended Properties=Excel 8.0;";
            OleDbConnection con = new OleDbConnection(strCon);
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [Attend$]", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        //跳转
        protected void ddlPageIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvAttendanceManage.PageIndex = ddlPageIndex.SelectedIndex;
            lblPageIndex.Text = Convert.ToString(gvAttendanceManage.PageIndex + 1);  //当前第几页          
        }
        //首页
        protected void btnFirstPage_Click1(object sender, ImageClickEventArgs e)
        {
            gvAttendanceManage.PageIndex = 0;
            //同步
            lblPageIndex.Text = Convert.ToString(gvAttendanceManage.PageIndex + 1);  //当前第几页
            ddlPageIndex.Text = lblPageIndex.Text;
            BindAttend();
        }
        
        //上一页                                                                                                     
        protected void btnPrePage_Click(object sender, ImageClickEventArgs e)
        {
            if (this.gvAttendanceManage.PageIndex > 0)
            {
                this.gvAttendanceManage.PageIndex = this.gvAttendanceManage.PageIndex - 1;
                //同步
                lblPageIndex.Text = Convert.ToString(gvAttendanceManage.PageIndex + 1);  //当前第几页
                ddlPageIndex.Text = lblPageIndex.Text;
                BindAttend();
            }
            else
            {
                //  gvAttendanceManage.PageIndex = 0;
                BindAttend();
            }
        }
        //下一页
        protected void btnNextPage_Click(object sender, ImageClickEventArgs e)
        {
            if (this.gvAttendanceManage.PageIndex < this.gvAttendanceManage.PageCount)
            {
                this.gvAttendanceManage.PageIndex = this.gvAttendanceManage.PageIndex + 1;
                //同步
                lblPageIndex.Text = Convert.ToString(gvAttendanceManage.PageIndex);  //当前第几页
                ddlPageIndex.Text = Convert.ToString(gvAttendanceManage.PageIndex);
                BindAttend();
            }
        }
        //尾页
        protected void btnEndPage_Click(object sender, ImageClickEventArgs e)
        {
            gvAttendanceManage.PageIndex = gvAttendanceManage.PageCount - 1;
            //同步
            lblPageIndex.Text = Convert.ToString(gvAttendanceManage.PageIndex + 1);  //当前第几页
            ddlPageIndex.Text = lblPageIndex.Text;
            BindAttend();
        }
    }
}