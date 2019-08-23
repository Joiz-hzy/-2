using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UIL
{
    public partial class UserEdit : System.Web.UI.Page
    {

        //显示部门信息
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //判断页面是否是第一次加载
            {
                ddlBelongsDept.DataSource = BLL.UserManageBll.GetAllDeptName();
                ddlBelongsDept.DataTextField = "DeptName";
                ddlBelongsDept.DataValueField = "DeptID";
                ddlBelongsDept.DataBind();
                //Response.Write(Request.QueryString["UserID"]);
                if (Request.QueryString["UserID"] != null)
                {
                    //表示用户修改
                    btnModify.Visible = true;
                    btnSave.Visible = false;
                    txtUserID.Enabled = false;
                    Model.UserInfo u = BLL.UserManageBll.GetSingleUserInfo(Request.QueryString["UserID"]);
                    txtUserID.Text = u.UserID;
                    txtUserName.Text= u.UserName;
                    ddlUserType.SelectedValue = u.UserType.ToString();
                    ddlBelongsDept.SelectedValue = u.DeptID.ToString();
                    txtMobile.Text = u.Cellphone;
                }
                else
                {
                    //表示用户添加
                    btnModify.Visible = false;
                    btnSave.Visible = true;
                }

            }
        }
       //添加
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Model.UserInfo u = new Model.UserInfo();
                u.UserID = txtUserID.Text;
                u.UserName = txtUserName.Text;
                u.UserType = Convert.ToByte(ddlUserType.SelectedValue);
                //判断是否选中所属部门
                if (ddlBelongsDept.SelectedValue == "--请选择--")  //未选中时为空
                {
                    u.DeptID = -1;
                }
                else
                {
                    u.DeptID = Convert.ToInt32(ddlBelongsDept.SelectedValue);
                }

                u.Cellphone = txtMobile.Text;
                u.Password = "1";
                //将以上数据赋值
                if (BLL.UserManageBll.InsertUserInfo(u))
                {
                    //添加成功
                    this.ClientScript.RegisterStartupScript(GetType(), "", "alert('恭喜您，添加成功！');window.parent.location='UserManage.aspx';", true);

                }//添加失败
                else
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "", "alert('很遗憾，添加失败！');", true);
                }
            }
            catch
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('带*号的为必填项，请重新填写！');", true);
            }
           
      
        }
        //修改
        protected void btnModify_Click(object sender, EventArgs e)
        {
            Model.UserInfo u = new Model.UserInfo();
            u.UserID = txtUserID.Text;
            u.UserName = txtUserName.Text;
            u.UserType = Convert.ToByte(ddlUserType.SelectedValue);
            //判断是否选中所属部门
            if (ddlBelongsDept.SelectedValue == "--请选择--")  //未选中时为空
            {
                u.DeptID = -1;
            }
            else
            {
                u.DeptID = Convert.ToInt32(ddlBelongsDept.SelectedValue);
            }
            u.Cellphone = txtMobile.Text;
            //将以上数据赋值
            if (BLL.UserManageBll.UpdateUserInfo(u))
            {
                //修改成功
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('恭喜您，修改成功！');window.parent.location='UserManage.aspx';", true);

            }//修改失败
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('很遗憾，修改失败！');", true);
            }
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //this.ClientScript.RegisterStartupScript(this.GetType(), "","window.parent.$('#dlg').dialog('close');", true);
          
        }

      
    }
}