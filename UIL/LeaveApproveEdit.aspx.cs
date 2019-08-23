using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
namespace UIL
{
    public partial class LeaveApproveEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //页面第一次加载
            if (!IsPostBack)
            {
                //禁用
                txtBeginTime.Enabled = false;
                txtEndTime.Enabled = false;
                txtLeaveReason.Enabled = false;
                txtTitle.Enabled = false;
                //判断是否是查看
                if (Request.QueryString["ApproveID"] != null)
                {
                    //表示查看
                    Model.Approve a = new Model.Approve();
                    a.ApproveID = Convert.ToInt32(Request["ApproveID"]);
                    Model.UserInfo u = (Model.UserInfo)Session["User"];
                    DataTable dt = BLL.LeaveApproveBll.GetApproveInfo(a);
                    if (dt.Rows[0] != null)//存在请假单
                    {
                        lblNo.Text = dt.Rows[0]["ApproveID"].ToString();//请假单ID
                        lblApply.Text = dt.Rows[0]["UserName"].ToString();//申请人        
                        txtTitle.Text = dt.Rows[0]["Title"].ToString();//标题
                        //拆分时间  
                        DateTime begin = Convert.ToDateTime(dt.Rows[0]["BeginDate"]);//起始时间
                        txtBeginTime.Text = begin.ToString("yyyy-MM-dd");
                        DateTime end = Convert.ToDateTime(dt.Rows[0]["EndDate"]);//结束时间
                        ddlBegin.Text = begin.ToString("HH:nn");
                        txtEndTime.Text = end.ToString("yyyy-MM-dd");
                        ddlEnd.SelectedValue = end.ToString("HH:nn");
                        txtLeaveReason.Text = dt.Rows[0]["Reason"].ToString();//请假原因
                        lblApplyTime.Text = dt.Rows[0]["ApplyDate"].ToString();//申请时间
                        txtBackup.Text = dt.Rows[0]["Remark"].ToString();//备注
                        lblApproveTime.Text = dt.Rows[0]["ApproveDate"].ToString();//申请时间
                        ddlBegin.Enabled = false;
                        ddlEnd.Enabled = false;
                        if (Convert.ToInt32(dt.Rows[0]["Status"]) == 1)//判断请假单状态 ===》  0：待办   1：归档
                        {   //归档
                            ddlApproveResult.SelectedValue = dt.Rows[0]["Result"].ToString();//审批结果
                            //禁用按钮  隐藏
                            ddlApproveResult.Enabled = false;
                            txtBackup.Enabled = false;
                            txtBeginTime.Enabled = false;
                            txtEndTime.Enabled = false;
                            txtLeaveReason.Enabled = false;
                            txtTitle.Enabled = false;
                            btnOK.Visible = false;

                            lblApplyTime.ForeColor = Color.Blue;
                            ddlApproveResult.ForeColor = Color.Blue;
                            lblApproveTime.ForeColor = Color.Blue;
                            txtBackup.ForeColor = Color.Blue;

                        }

                    }

                }
            }
        }
        //修改
        protected void btnOK_Click(object sender, EventArgs e)
        {
            // @ApplyUser, @Title, @BeginDate, @EndDate, @Reason,  @ApplyDate,  @ApproveDate, @Status, @Result,  @Remark  @ApproveID
            Model.Approve a = new Model.Approve();
            Model.UserInfo u = (Model.UserInfo)Session["User"];//将登陆时获取的用户信息Session重新调用
            a.ApproveID = Convert.ToInt32(lblNo.Text);//请假ID
            a.ApproveDate = DateTime.Now;//审批时间
            a.Result = Convert.ToByte(ddlApproveResult.SelectedValue);//审批结果
            a.Remark = txtBackup.Text;//备注
            a.ApproveUser = u.UserID;// 申请人ID
            a.Title = txtTitle.Text;//标题    
            a.Reason = txtLeaveReason.Text;//请假原因
            //请假单状态 
            if (ddlApproveResult.SelectedValue == "2")
            {
                Label1.Visible = true;
                return;
            }
            else
            {
                Label1.Visible = false;
            }
            a.Status = 1;
            //将以上数据赋值
            if (BLL.LeaveApproveBll.IsApprove(a))
            {

                //审批成功
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('恭喜您，审批已通过！！');window.parent.location='LeaveApprove.aspx';", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('很遗憾，审批未通过！');window.parent.location='LeaveApprove.aspx';", true);
            }



        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Model.UserInfo u = (Model.UserInfo)Session["User"];//将Session中的存储的用户信息提取出来
            if (u.UserType == 0)//员工
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "window.parent.location='LeaveApply.aspx';", true);
            }
            if (u.UserType == 1)//主管
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "window.parent.location='LeaveApprove.aspx';", true);
            }
        }
    }
}