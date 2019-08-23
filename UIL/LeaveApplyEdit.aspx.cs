using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace UIL
{
    public partial class LeaveApplyEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //页面第一次加载
            if (!IsPostBack)
            {
                //获取申请单ID
               // getIDInfo();

                //判断是否是查看,修改
                if (Request.QueryString["ApproveID"] != null)
                {
                    //表示查看,修改
                    Model.Approve a = new Model.Approve();
                    a.ApproveID = Convert.ToInt32(Request["ApproveID"]);//请假单ID
                    Model.UserInfo u = (Model.UserInfo)Session["User"];
                    a.UserID = u.UserID;//申请人ID
                    DataTable dt = BLL.LeaveApplyBll.GetSingleApproveInfo(a);
                    if (dt.Rows[0] != null)//存在请假单
                    {
                        txtLeaveReason.Text = dt.Rows[0]["Reason"].ToString();//请假原因
                        lblNo.Text = dt.Rows[0]["ApproveID"].ToString();//请假单ID
                        txtTitle.Text = dt.Rows[0]["Title"].ToString();//标题
                        //拆分时间  
                        DateTime begin = Convert.ToDateTime(dt.Rows[0]["BeginDate"]);//起始时间
                        txtBeginTime.Text = begin.ToString("yyyy-MM-dd");
                        DateTime end = Convert.ToDateTime(dt.Rows[0]["EndDate"]);//结束时间
                        ddlBegin.Text = begin.ToString("HH:nn");
                        txtEndTime.Text = end.ToString("yyyy-MM-dd");//结束时间      
                        ddlEnd.SelectedValue = end.ToString("HH:nn");
                        if (Convert.ToInt32(dt.Rows[0]["Status"]) == 0)//判断请假单状态 ===》  0：待办   1：归档
                        {     //待办
                            btnOK.Visible = false;
                            btnUpdate.Visible = true;


                        }
                    }
                                                
                }   
                else  //表示添加
                {
                    btnOK.Visible = true;
                    btnUpdate.Visible = false;
                }

            }
        }

        //添加
        protected void btnOK_Click(object sender, EventArgs e)
        {
            //@ApplyUser, @Title, @BeginDate, @EndDate, @Reason, @ApproveUser, @ApplyDate, @ApproveDate, @Status, @Result, @Remark      
            Model.Approve u = new Model.Approve();
            Model.UserInfo user = (Model.UserInfo)Session["User"];//将登陆时获取的用户信息Session重新调用
            u.ApplyUser = user.UserID;//申请人ID
            u.Title = txtTitle.Text;//标题
            u.BeginDate = Convert.ToDateTime(txtBeginTime.Text + " " + ddlBegin.Text);//开始时间
            u.EndDate = Convert.ToDateTime(txtEndTime.Text + " " + ddlEnd.Text);//结束时间
            u.Reason = txtLeaveReason.Text;//请假原因
            u.ApplyDate = DateTime.Now;//申请时间
            u.Status = 0;//请假单状态    
            DateTime beginTime=Convert.ToDateTime(u.BeginDate);
            DateTime endTime=Convert.ToDateTime(u.EndDate);
            if (beginTime > endTime)  //判断起始时间，最终时间  输入的时候合理
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('请假的起始时间不能大于结束时间！');", true);
            }
            else
            {
                //将以上数据赋值
                if (BLL.LeaveApplyBll.InsertAttendInfo(u))
                {
                    //添加成功
                    this.ClientScript.RegisterStartupScript(GetType(), "", "alert('恭喜您，添加成功！');window.parent.location='LeaveApply.aspx';", true);

                }
               //添加失败
                else
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "", "alert('很遗憾，添加失败！');", true);
                }
            }
           
        }
        //修改
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Model.Approve u = new Model.Approve();
            u.ApproveID = Convert.ToInt32(lblNo.Text);//申请单ID
            u.Title = txtTitle.Text;//标题
            string beginTime = txtBeginTime.Text + " " + ddlBegin.Text;
            u.BeginDate = Convert.ToDateTime(beginTime);//起始时间
            string endTimer = txtEndTime.Text + " " + ddlEnd.Text;
            u.EndDate = Convert.ToDateTime(endTimer);//结束时间
            u.Reason = txtLeaveReason.Text;//请假原因
            if (BLL.LeaveApplyBll.UpdateApproveInfo(u))
            {
                //表示修改成功
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('恭喜您，修改成功！');window.parent.location='/LeaveApply.aspx';", true);
            }
            else
            {
                //表示修改失败
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('对不起，修改失败！');", true);
            }
        }

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