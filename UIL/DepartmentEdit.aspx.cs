using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UIL
{
    public partial class DepartmentEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断页面是否是第一次加载
            if (!IsPostBack)
            {
                ddlCharge.DataSource = BLL.DepartmentManageBll.GetDeptChargeName();
                ddlCharge.DataTextField = "UserName";
                ddlCharge.DataValueField = "UserId";
                ddlCharge.DataBind();
                //判断是否为空
                if (Request["DeptID"] != null)
                {
                    //表示修改
                    btnSave.Visible = false;
                    btnModify.Visible = true;
                    Model.DepartmentInfo u = BLL.DepartmentManageBll.GetSingleDeptInfo(Convert.ToInt32(Request["DeptID"]));
                    txtDeptName.Text = u.DeptName;
                    txtDeptInfo.Text = u.DeptInfo;                   
                    ddlCharge.SelectedValue = u.ManagerID; ;
             
                }
                else
                { 
                //表示添加
                    btnSave.Visible = true;
                    btnModify.Visible = false;
                }
            }
        }
        //保存信息
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.DepartmentInfo u = new Model.DepartmentInfo();
            u.DeptName = txtDeptName.Text;
            u.ManagerID =ddlCharge.SelectedValue;
            u.DeptInfo = txtDeptInfo.Text;
            //赋值
            if (BLL.DepartmentManageBll.InsertDeptInfo(u))
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('恭喜您，添加成功！');window.parent.location='DepartmentManage.aspx'", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('很遗憾，添加失败！');", true);
            }
        }
        //修改
        protected void btnModify_Click(object sender, EventArgs e)
        {
            Model.DepartmentInfo u = new Model.DepartmentInfo();
            u.DeptName = txtDeptName.Text;
            u.DeptID = Convert.ToInt32(Request["DeptID"]);
            u.ManagerID = ddlCharge.SelectedValue;
            u.DeptInfo = txtDeptInfo.Text;
            //赋值
            if (BLL.DepartmentManageBll.UpdateDeptInfo(u))
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('恭喜您，修改成功！');window.parent.location='DepartmentManage.aspx'", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('很遗憾，修改失败！');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "", "window.parent.$('#dlg').dialog('close');", true);
        }
    }
}