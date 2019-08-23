using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class DepartmentManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断页面是否是第一次加载
            if (!IsPostBack)
            {
                ddlCharge.DataSource = BLL.DepartmentManageBll.GetDeptChargeName();
                ddlCharge.DataTextField = "UserName";
                ddlCharge.DataValueField = "DeptID";
                ddlCharge.DataBind();
                //绑定部门信息数据==刷新
                BindDeptInfo();                         
            }
        }
        // 判断部门员工===》是否显示删除按钮 
        private void IsShowDelBtnImg()
        {          
            //判断部门的人数是否为0，然后为删除的依据
            Model.DepartmentInfo d = new Model.DepartmentInfo();
            for (int i = 0; i < gvDeptInfo.Rows.Count; i++)
            {
                d.DeptName = gvDeptInfo.Rows[i].Cells[1].Text;//循环获取部门名称
                DataTable data = BLL.DepartmentManageBll.JudgeDeptEmp(d);
                if (data.Rows.Count > 0)//部门下有员工  ==>隐藏删除按钮  显示修改按钮
                {
                    gvDeptInfo.Rows[i].Cells[3].FindControl("imgUpdate").Visible = true;
                    gvDeptInfo.Rows[i].Cells[3].FindControl("imgBtnDel").Visible = false;
                }
                else//部门下没有员工  ==>显示删除按钮  显示修改按钮
                {
                    gvDeptInfo.Rows[i].Cells[3].FindControl("imgUpdate").Visible = true;
                    gvDeptInfo.Rows[i].Cells[3].FindControl("imgBtnDel").Visible = true;
                }
            }
        }
        //绑定部门信息数据
        private void BindDeptInfo()
        {           
            gvDeptInfo.DataSource = BLL.DepartmentManageBll.SearchDeptInfo(this.txtDeptName.Text, Convert.ToInt32(ddlCharge.SelectedValue));
            gvDeptInfo.DataBind();
            // 判断部门员工===》是否显示删除按钮
            IsShowDelBtnImg();
            //分页功能     
            List<int> a = new List<int>();
            for (int i = 1; i <= gvDeptInfo.PageCount; i++)
            {
                a.Add(i);
            }
            ddlPageIndex.DataSource = a;
            ddlPageIndex.DataBind();
            lblPageCount.Text = gvDeptInfo.PageCount.ToString(); //求的总页码数 
            lblRowsCount.Text = BLL.DepartmentManageBll.SearchDeptInfo(this.txtDeptName.Text, Convert.ToInt32(ddlCharge.SelectedValue)).Rows.Count.ToString();//总共数据量
            lblPageIndex.Text = Convert.ToString(gvDeptInfo.PageIndex + 1);  //当前第几页 
        }
        int i = 1;
        //在 GridView 控件中将数据行绑定到数据时发生
        protected void gvDeptInfo_RowDataBound(object sender, GridViewRowEventArgs e)
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

            //判断当前行是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //找控件lblNo
                Label lbl = (Label)e.Row.FindControl("lblNo");
                lbl.Text = i.ToString();
                i++;
            }

        }
        //查询
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {          
            BindDeptInfo();        
        }
        //命令对象
        protected void gvDeptInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDeptInfo")
            {
                //表示要执行删除操作
                Model.DepartmentInfo d = new Model.DepartmentInfo();
                d.DeptID = Convert.ToInt32(e.CommandArgument.ToString());
                if (BLL.DepartmentManageBll.DelDeptInfo(d))
                {
                    //表示删除成功
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('恭喜您，删除成功！');", true);
                    //即时刷新列表里面的数据
                    BindDeptInfo();
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
            gvDeptInfo.PageIndex = ddlPageIndex.SelectedIndex;
            lblPageIndex.Text = Convert.ToString(gvDeptInfo.PageIndex + 1);  //当前第几页          
           
        }
        //首页
        protected void btnFirstPage_Click1(object sender, ImageClickEventArgs e)
        {
            gvDeptInfo.PageIndex = 0;
            //同步
            lblPageIndex.Text = Convert.ToString(gvDeptInfo.PageIndex + 1);  //当前第几页
            ddlPageIndex.Text = lblPageIndex.Text;
            BindDeptInfo();
        }
        //上一页
        protected void btnPrePage_Click(object sender, ImageClickEventArgs e)
        {
            if (this.gvDeptInfo.PageIndex > 0)
            {
                this.gvDeptInfo.PageIndex = this.gvDeptInfo.PageIndex - 1;
                //同步
                lblPageIndex.Text = Convert.ToString(gvDeptInfo.PageIndex + 1);  //当前第几页
                ddlPageIndex.Text = lblPageIndex.Text;
                BindDeptInfo();
            }
            else
            {
                //  gvDeptInfo.PageIndex = 0;
                BindDeptInfo();
            }
        }
        //下一页
        protected void btnNextPage_Click(object sender, ImageClickEventArgs e)
        {
            if (this.gvDeptInfo.PageIndex < this.gvDeptInfo.PageCount)
            {
                this.gvDeptInfo.PageIndex = this.gvDeptInfo.PageIndex + 1;
                //同步
                lblPageIndex.Text = Convert.ToString(gvDeptInfo.PageIndex);  //当前第几页
                ddlPageIndex.Text = Convert.ToString(gvDeptInfo.PageIndex);
                BindDeptInfo();
            }
        }
        //尾页
        protected void btnEndPage_Click(object sender, ImageClickEventArgs e)
        {
            gvDeptInfo.PageIndex = gvDeptInfo.PageCount - 1;
            //同步
            lblPageIndex.Text = Convert.ToString(gvDeptInfo.PageIndex + 1);  //当前第几页
            ddlPageIndex.Text = lblPageIndex.Text;
            BindDeptInfo();
        }
    }
}