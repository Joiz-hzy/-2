using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UIL
{
    public partial class PersonalInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Model.UserInfo u = (Model.UserInfo)Session["user"];
                txtCellPhone.Text = u.Cellphone;  //获取手机号

            }

        }
        //修改个人信息
        private void UpdatePersonInfo()
        {
            //判断两次密码是否相同==>1.当两次密码不相同的情况下
            if (txtAgainPwd.Text != txtNewPwd.Text)
            {

                // this.ClientScript.RegisterStartupScript(GetType(), "", "alert('两次填写密码不相同');", true);
            }
            else
            { //2.当两次密码相同的情况下==》判断密码是否为空进行保存，为空密码还是原来的密码，否则修改了原密码==》
                Model.UserInfo u = (Model.UserInfo)Session["user"];
                //（1）密码不为空的情况下 
                if (!string.IsNullOrEmpty(txtAgainPwd.Text))
                {
                    u.Password = txtNewPwd.Text;
                    u.Cellphone = txtCellPhone.Text;
                }
                else
                { //（2）密码为空的情况下 
                    u.Cellphone = txtCellPhone.Text;
                }
                //修改成功
                if (BLL.LoginBll.UpdateUserPwd(u))
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "", "alert('恭喜您，修改成功！');window.parent.dialog.close();", true);
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "", "alert('很遗憾，修改失败！');", true);
                }
            }
        }
        //修改
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdatePersonInfo();

        }

    }
}
