using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace UIL
{
    public partial class LeaveApprove : System.Web.UI.Page
    {      
        protected void Page_Load(object sender, EventArgs e)
        {
            //正常换行
            gvApproveInfo.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
            //下面这行是自动换行
            gvApproveInfo.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            //第一次加载页面
            if (!IsPostBack)
            {
                //一开始隐藏
                gvApproveInfo.Columns[10].Visible = false;
                gvApproveInfo.Columns[9].Visible = false;
                gvApproveInfo.Columns[8].Visible = false;
                ckShow.Checked = false;//如果不这样后面的代码会把他True
                BindApprove();         
            }
        }
        //查询
        private void BindApprove()
        {
            Model.Approve a = new Model.Approve();
            a.Title = txtTitle.Text;   //标题
           a.ApplyUser = txtApplyUser.Text; //申请人ID
            //判断初始时间/起始时间是否输入
            if (txtFromDate.Text != "")
            {
                a.BeginDate = Convert.ToDateTime(txtFromDate.Text);

            }
            else    //设置初始，起始时间
            {       
                a.BeginDate = Convert.ToDateTime("2014-4-12");
            }
            if (txtToDate.Text != "")
            {
                a.EndDate = Convert.ToDateTime(txtToDate.Text);
            }
            else
            {
                a.EndDate = Convert.ToDateTime("2020-4-15");
            }
            a.Status = Convert.ToByte(rblLeaveState.SelectedValue);
            //讲以上数据 进行赋值
            DataTable dt = BLL.LeaveApproveBll.SearchLeaveApprove(a,txtApplyUser.Text);
            if (dt != null)   //成功
            {
                gvApproveInfo.DataSource = dt;
                gvApproveInfo.DataBind();
                //总共数据量
                lblRowsCount.Text = dt.Rows.Count.ToString(); ;//总共数据量
            }
            else   //失败
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('很遗憾，数据库出现异常!')", true);
            }
            //分页功能     
            List<int> li = new List<int>();
            for (int i = 1; i <= gvApproveInfo.PageCount; i++)
            {
                li.Add(i);
            }
            ddlPageIndex.DataSource = li;
            ddlPageIndex.DataBind();
            lblPageCount.Text = gvApproveInfo.PageCount.ToString(); //求的总页码数 
            lblRowsCount.Text = BLL.LeaveApproveBll.SearchLeaveApprove(a, txtApplyUser.Text).Rows.Count.ToString();//总共数据量
            lblPageIndex.Text = Convert.ToString(gvApproveInfo.PageIndex + 1);  //当前第几页 
         

        }
        //查询
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindApprove();
        }

        //显示/隐藏
        protected void ckShow_CheckedChanged(object sender, EventArgs e)
        {
            gvApproveInfo.Columns[8].Visible = !gvApproveInfo.Columns[8].Visible;
            gvApproveInfo.Columns[9].Visible = !gvApproveInfo.Columns[9].Visible;
            gvApproveInfo.Columns[10].Visible = !gvApproveInfo.Columns[10].Visible;
            //Response.Write("GridView1的第8列现在的显示隐藏状态是：" + gvApproveInfo.Columns[8].Visible.ToString());
            BindApprove();
        }
       // GridView实现删除时弹出确认对话框：
        protected void gvApproveInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //光棒变色效果
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标放上去的时候 先保存当前行的背景颜色并给附一颜色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;w=this.style.fontWeight;this.style.backgroundColor='#BBD3EB';this.style.fontWeight='500';this.style.color='red'");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;this.style.fontWeight=w;this.style.color=''");
                //不能同时测试:单击/双击 事件, 要分别测试它们
                e.Row.Attributes.Add("OnDblClick", "DbClickEvent('" + e.Row.Cells[1].Text + "')");
                //e.Row.Attributes.Add("OnClick", "ClickEvent('" + e.Row.Cells[1].Text + "')");
                //e.Row.Attributes.Add("OnKeyDown", "GridViewItemKeyDownEvent('" + e.Row.Cells[1].Text + "')"); 
                e.Row.Attributes["style"] = "Cursor:hand";
            }
            //如果是绑定数据行===========>(删除提示对话框)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[10].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除请假单：\"" + e.Row.Cells[0].Text + "\"吗?')");
                }
            }
        }
        //删除
        protected void gvApproveInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = BLL.LeaveApproveBll.DeleteApprove(Convert.ToInt32(gvApproveInfo.DataKeys[e.RowIndex].Value.ToString()));
            if (i > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "删除成功", "alert('删除成功');", true);
            }
            else if (i == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "数据不存在", "alert('数据不存在');", true);
            }

            //重新绑定===========>刷新gridView中的数据
            BindApprove();
        }
        //更新
        protected void gvApproveInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Model.Approve a = new Model.Approve();
            a.ApproveID= Convert.ToInt32(gvApproveInfo.DataKeys[e.RowIndex].Value.ToString());
            a.Title = ((TextBox)(gvApproveInfo.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();           
             if (BLL.LeaveApproveBll.UpdateApproveInfo(a))
             {

                 ClientScript.RegisterStartupScript(this.GetType(), "成功提示", "alert('更新成功！')", true);
                 BindApprove();
             }
             else
             {
                 ClientScript.RegisterStartupScript(this.GetType(), "温馨提示", "alert('更新失败……')", true);
             }      

            //编辑更新后，自动回到不可编辑状态
            gvApproveInfo.EditIndex = -1;
            //调用绑定方法
            BindApprove();
        }
        //取消
        protected void gvApproveInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //变为不可编辑状态---从而实现取消功能
            gvApproveInfo.EditIndex = -1;
            //调用绑定方法
            BindApprove();
        }
        //编辑状态
        protected void gvApproveInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //变为编辑状态
            gvApproveInfo.EditIndex = e.NewEditIndex;
            //调用绑定方法
            BindApprove();

        }
        //选择
        protected void gvApproveInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int id = Convert.ToInt32(e.CommandArgument); //取行索引号
            //string txt = gvApproveInfo.DataKeys[0].Value.ToString();　//取主键的值
            //Response.Write(txt);
            //BindApprove();
        }
        //跳转
        protected void ddlPageIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvApproveInfo.PageIndex = ddlPageIndex.SelectedIndex;
            lblPageIndex.Text = Convert.ToString(gvApproveInfo.PageIndex + 1);  //当前第几页          
        }
        //首页
        protected void btnFirstPage_Click1(object sender, ImageClickEventArgs e)
        {
            gvApproveInfo.PageIndex = 0;
            //同步
            lblPageIndex.Text = Convert.ToString(gvApproveInfo.PageIndex + 1);  //当前第几页
            ddlPageIndex.Text = lblPageIndex.Text;
            BindApprove();
        }
        //上一页
        protected void btnPrePage_Click(object sender, ImageClickEventArgs e)
        {
            if (this.gvApproveInfo.PageIndex > 0)
            {
                this.gvApproveInfo.PageIndex = this.gvApproveInfo.PageIndex - 1;
                //同步
                lblPageIndex.Text = Convert.ToString(gvApproveInfo.PageIndex + 1);  //当前第几页
                ddlPageIndex.Text = lblPageIndex.Text;
                BindApprove();
            }
            else
            {
                //  gvApproveInfo.PageIndex = 0;
                BindApprove();
            }
        }
        //下一页
        protected void btnNextPage_Click(object sender, ImageClickEventArgs e)
        {
            if (this.gvApproveInfo.PageIndex < this.gvApproveInfo.PageCount)
            {
                this.gvApproveInfo.PageIndex = this.gvApproveInfo.PageIndex + 1;
                //同步
                lblPageIndex.Text = Convert.ToString(gvApproveInfo.PageIndex);  //当前第几页
                ddlPageIndex.Text = Convert.ToString(gvApproveInfo.PageIndex);
                BindApprove();
            }
        }
        //尾页
        protected void btnEndPage_Click(object sender, ImageClickEventArgs e)
        {
            gvApproveInfo.PageIndex = gvApproveInfo.PageCount - 1;
            //同步
            lblPageIndex.Text = Convert.ToString(gvApproveInfo.PageIndex + 1);  //当前第几页
            ddlPageIndex.Text = lblPageIndex.Text;
            BindApprove();
        }
    }
}