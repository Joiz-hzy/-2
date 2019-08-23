using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace UIL
{
    public partial class LeaveApply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //第一次加载页面
            if (!IsPostBack)
            {
                BindApprove();
               // JudgeLeaveStatus();//刷新归档/待审批            
            }

        }
        //判断是否归档(根据条件隐藏显示控件【修改，删除，查询】)
        //public void JudgeLeaveStatus()
        //{           
        //    for (int i = 0; i < gvApproveInfo.Rows.Count;i++ )
        //    {
        //        if (gvApproveInfo.Rows[i].Cells[6].Text == "归档")    //归档   隐藏修改/删除
        //        {
        //            gvApproveInfo.Rows[i].Cells[7].FindControl("imgUpdate").Visible = false;
        //            gvApproveInfo.Rows[i].Cells[7].FindControl("delApprove").Visible = false;
        //        }
        //        else 
        //        {
        //            gvApproveInfo.Rows[i].Cells[7].FindControl("hlShow").Visible = false;
        //        }
        //    }

        //}
        //查询
        //第一种【DIY】
        //private void BindApprove()
        //{
        //    Model.Approve a = new Model.Approve();
        //    Model.UserInfo u = (Model.UserInfo)Session["User"];
        //    a.Title = txtTitle.Text;   //标题
        //    //判断初始时间/起始时间是否输入
        //    if (txtFromDate.Text != "")
        //    {
        //        a.BeginDate = Convert.ToDateTime(txtFromDate.Text);

        //    }
        //    else    //设置初始，起始时间
        //    {
        //        a.BeginDate = Convert.ToDateTime("2014-4-12");
        //    }
        //    if (txtToDate.Text != "")
        //    {
        //        a.EndDate = Convert.ToDateTime(txtToDate.Text);
        //    }
        //    else
        //    {
        //        a.EndDate = Convert.ToDateTime("2020-4-15");
        //    }
        //    a.Status = Convert.ToByte(ddlLeaveState.SelectedValue);
        //    //讲以上数据 进行赋值
        //    DataTable dt = BLL.LeaveApplyBll.SearchAskForLeave(a,u.UserID);
        //    if (dt != null)   //成功
        //    {
        //        gvApproveInfo.DataSource = dt;
        //        gvApproveInfo.DataBind();
        //    }
        //    else   //失败
        //    {
        //        this.ClientScript.RegisterStartupScript(GetType(), "", "alert('很遗憾，数据库出现异常!')", true);
        //    }     

        //}
        //第二种【Tescher】
        private void BindApprove()
        {
            Model.UserInfo u = (Model.UserInfo)Session["User"];
            DataTable dt = BLL.LeaveApplyBll.SearchAskForLeave(u.UserID, txtTitle.Text, txtFromDate.Text, txtToDate.Text, Convert.ToByte(ddlLeaveState.SelectedValue));
            gvApproveInfo.DataSource = dt;
            gvApproveInfo.DataBind();
            //分页功能     
            List<int> li = new List<int>();
            for (int i = 1; i <= gvApproveInfo.PageCount; i++)
            {
                li.Add(i);
            }
            ddlPageIndex.DataSource = li;
            ddlPageIndex.DataBind();
            lblPageCount.Text = gvApproveInfo.PageCount.ToString(); //求的总页码数 
            lblRowsCount.Text = BLL.LeaveApplyBll.SearchAskForLeave(u.UserID, txtTitle.Text, txtFromDate.Text, txtToDate.Text, Convert.ToByte(ddlLeaveState.SelectedValue)).Rows.Count.ToString();//总共数据量
            lblPageIndex.Text = Convert.ToString(gvApproveInfo.PageIndex + 1);  //当前第几页 
        }
            
        //在gridview将数据行绑定数据时发生
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
            //根据条件隐藏显示控件
            //第一种方法
            //1.判断当前行是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //根据控件ID查找控件
                HyperLink hlShow = (HyperLink)e.Row.FindControl("hlShow");
                Image imgUpdate = (Image)e.Row.FindControl("imgUpdate");
                ImageButton delApprove = (ImageButton)e.Row.FindControl("delApprove");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                //得到绑定gridview上面每一行对应的基础数据源
                DataRowView drv = (DataRowView)e.Row.DataItem;
                byte status = (byte)drv["Status"];//得到请假单状态
                if (status == 0)   //待审批
                {
                    lblStatus.Text = "待审批";
                    hlShow.Visible = false;
                    imgUpdate.Visible = true;
                    delApprove.Visible = true;
                }
                else
                {
                    lblStatus.Text = "归档";
                    hlShow.Visible = true;
                    imgUpdate.Visible = false;
                    delApprove.Visible = false;
                }
            }
            //第二种方法JudgeLeaveStatus()

        }
        //查询
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindApprove();
           // JudgeLeaveStatus();//刷新归档/待审批
        }
        //命令对象 ===>删除
        protected void gvApproveInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteApproveInfo")
            {
                //表示要执行删除操作
                Model.Approve a = new Model.Approve();
                a.ApproveID = Convert.ToInt32(e.CommandArgument.ToString());
                if (BLL.LeaveApplyBll.DelApproveInfo(a))
                {
                    //表示删除成功
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('恭喜您，删除成功！');", true);
                    //即时刷新列表里面的数据
                    BindApprove();
                }
                else
                {
                    //表示删除失败
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('对不起，删除失败！');", true);
                }
            }
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