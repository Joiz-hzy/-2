using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace UIL
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //登录用户名和密码
        protected void ibLogin_Click(object sender, ImageClickEventArgs e)
        {
            Model.UserInfo u = BLL.LoginBll.ValidateUserAndPwd(txtID.Text,txtPassword.Text);
            if (u != null)
            {
                Session["User"] = u;//把登录成功的用户存储在里去         
                //判断用户角色  0==员工 1==主管   2===管理员
                if(u.UserType==0)  //员工登录
                {
                    Response.Redirect("~/MyAttendance.aspx?name=我的考勤"); 
                }
                else if (u.UserType == 1) //主管登录
                {
                    Response.Redirect("~/AttendanceManage.aspx?name=考勤管理");   
                }
                else if (u.UserType == 2) //管理员登录
                {                   
                    Response.Redirect("~/AttendanceSetting.aspx?name=考勤设置");  // ~/:表示网站的根目录  
                }    
            }
            else
            { 
            this.ClientScript.RegisterStartupScript(this.GetType(),"","alert('用户名或密码错误。请重新填入！');",true);
            }
        }
    }
}