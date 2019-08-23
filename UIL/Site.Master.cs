using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
namespace UIL
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //表示第一次访问
                Model.UserInfo u = (Model.UserInfo)Session["User"];
                lblUserName.Text = u.UserName;
                if (u.UserType == 0)
                {
                    lblUserType.Text = "普通员工";
                }
                else if (u.UserType == 1)
                {
                    lblUserType.Text = "主管";
                }
                else
                {
                    lblUserType.Text = "系统管理员";
                }

                //=================地址栏================
                if (Request["name"] == "用户管理")
                {
                    urlName.Text = "用户管理";
                }
                if (Request["name"] == "部门管理")
                {
                    urlName.Text = "部门管理";
                }
                if (Request["name"] == "考勤设置")
                {
                    urlName.Text = "考勤设置";
                }
                if (Request["name"] == "考勤管理")
                {
                    urlName.Text = "考勤管理";
                }
                if (Request["name"] == "请假审批")
                {
                    urlName.Text = "请假审批";
                }
                if (Request["name"] == "我的考勤")
                {
                    urlName.Text = "我的考勤";
                }
                if (Request["name"] == "请假申请")
                {
                    urlName.Text = "请假申请";
                }
            }
        }
        /// <summary>
        /// 实现退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbExit_Click(object sender, EventArgs e)
        {
            //清空Session里面的数据
            Session.Clear();
            //清空cookie里面的数据
            FormsAuthentication.SignOut();
            //跳转到登录的页面
            Response.Redirect("~/Login.aspx");
        }
    }
}