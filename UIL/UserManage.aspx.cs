using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace UIL
{
    public partial class UserManage : System.Web.UI.Page
    {
       //ctrl+k+s 外设代码
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnDel.Attributes.Add("onclick", "return confirm('你确定要删除吗？')");
            //下面这行是自动换行
            gvUserInfo.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            //点击复选框==》鼠标选中时，改变gridview行的颜色
            ChangeGridviewRowColor();
            if (!IsPostBack) //判断页面是否是第一次加载
            {              
                ddlDepart.DataSource = BLL.UserManageBll.GetAllDeptName();
                ddlDepart.DataTextField = "DeptName";
                ddlDepart.DataValueField = "DeptID";
                ddlDepart.DataBind();
                //绑定用户数据====刷新
                BindUserInfo();         
                //【排序字段
                ViewState["SortOrder"] = "UserID";
                ViewState["OrderDire"] = "ASC";
                bind(); //绑定GridView的方法====》排序
              
        
            }

        }
     // private int j = 1;
        //绑定用户数据 ===》查询  
        #region
        public void BindUserInfo()
        { 
            gvUserInfo.DataSource = BLL.UserManageBll.SearchUserInFo(txtUserID.Text, txtUserName.Text, Convert.ToInt32(ddlDepart.SelectedValue));
            gvUserInfo.DataKeyNames = new string[] { "UserID" }; //获取所绑定的主键值
            gvUserInfo.DataBind();
            //分页功能     
            List<int> a = new List<int>();
            for (int i = 1; i <= gvUserInfo.PageCount; i++)
            {
                a.Add(i);
            }
            ddlPageIndex.DataSource = a;
            ddlPageIndex.DataBind();
            lblPageCount.Text = gvUserInfo.PageCount.ToString(); //求的总页码数 
            lblRowsCount.Text = BLL.UserManageBll.SearchUserInFo(txtUserID.Text, txtUserName.Text, Convert.ToInt32(ddlDepart.SelectedValue)).Rows.Count.ToString();//总共数据量
            lblPageIndex.Text = Convert.ToString(gvUserInfo.PageIndex + 1);  //当前第几页 
        }
        int j = 0;
        #endregion
        /// <summary>
        /// RowDataBound事件
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。此事件通常用于在某个行被绑定到数据时修改该行的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        #region
        protected void gvUserInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
            //执行循环  保证每条数据都可以更新
            for (int i = 0; i < gvUserInfo.Rows.Count;i++ )
            {              
                //光棒变色效果
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;w=this.style.fontWeight;this.style.backgroundColor='#BBD3EB';this.style.fontWeight='500';this.style.color='red'");
                    //当鼠标离开的时候 将背景颜色还原的以前的颜色
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;this.style.fontWeight=w;this.style.color=''");
                    //不能同时测试:单击/双击 事件, 要分别测试它们
                    e.Row.Attributes.Add("OnDblClick", "DbClickEvent('" + e.Row.Cells[1].Text + "')");
                    //e.Row.Attributes.Add("OnClick", "ClickEvent('" + e.Row.Cells[1].Text + "')");
                    //e.Row.Attributes.Add("OnKeyDown", "GridViewItemKeyDownEvent('" + e.Row.Cells[1].Text + "')"); 
                    e.Row.Attributes["style"] = "Cursor:hand";
                }
            }
           
            ////遍历所有行设置边框样式
            //foreach (TableCell tc in e.Row.Cells)
            //{
            //    tc.Attributes["style"] = "border-color:Black";
            //    //单击鼠标可以将单元格中的内容复制到剪切板 
            //    tc.Attributes.Add("onclick", "window.clipboardData.setData('Text', this.innerText);");
            //    //鼠标移入可以设置单元格字体颜色 
            //    tc.Attributes.Add("onmouseover", "this.style.color='red'");
            //    //鼠标移开,取消单元格字体颜色设置 
            //    tc.Attributes.Add("onmouseout", "this.style.color=''");
            //}
            //GridView自动增加序号====>第一种方法  在RowDataBound事件里
            //if (e.Row.RowIndex != -1)
            //{
            //    int indexID = this.gvUserInfo.PageIndex * this.gvUserInfo.PageSize + e.Row.RowIndex + 1;
            //    e.Row.Cells[0].Text = indexID.ToString();
            //}

            //GridView自动增加序号====>第二种方法
            //判断当前行是否是数据行               
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //找控件lblNo
            //    Label lbl = (Label)e.Row.FindControl("lblNo");
            //    lbl.Text = j.ToString();
            //    j++;
            //}
            //序号  每一页都是从序号1开始进行
            if (e.Row.DataItemIndex % gvUserInfo.PageSize == 0)
            {
                j = e.Row.DataItemIndex;
            }
            if (e.Row.RowIndex >= 0)
            {
                e.Row.Cells[0].Text = Convert.ToString(e.Row.DataItemIndex + 1 - j);
            }
          
        }
        #endregion
        //全选  
        #region
        public void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            //第一种 采用内部表头的CheckBox进行全选或反选
            //CheckBox cbAll = (CheckBox)sender;
            //if (cbAll.Text == "")
            //{
            //    foreach (GridViewRow gvr in gvUserInfo.Rows)
            //    {
            //        CheckBox cbSel = (CheckBox)gvr.Cells[0].FindControl("CheckBox1");
            //        cbSel.Checked = cbAll.Checked;
            //        //gvUserInfo.BackColor = System.Drawing.Color.Blue;
            //    }
            //}

            //第二种 使用外在的CheckBox控件实现全选或反选
            CheckBox cbox2 = (CheckBox)gvUserInfo.HeaderRow.FindControl("CheckBox2");
            for (int i = 0; i <= gvUserInfo.Rows.Count - 1; i++)
            {
                CheckBox cbox1 = (CheckBox)(gvUserInfo.Rows[i].FindControl("CheckBox1"));
                if (cbox2.Checked == true)
                {
                    cbox1.Checked = true;
                    gvUserInfo.Rows[i].BackColor = System.Drawing.Color.OrangeRed;
                }
                else
                {
                    cbox1.Checked = false;
                    gvUserInfo.Rows[i].BackColor = System.Drawing.Color.Transparent;
                }
               
               
            }
        }
        //点击复选框==》鼠标选中时，改变gridview行的颜色
        public void ChangeGridviewRowColor()
        {
            for (int i = 0; i < gvUserInfo.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvUserInfo.Rows[i].FindControl("CheckBox1");
                if (chk.Checked == true)
                {
                    gvUserInfo.Rows[i].BackColor = System.Drawing.Color.OrangeRed;
                }
                else
                {
                    gvUserInfo.Rows[i].BackColor = System.Drawing.Color.Transparent;
                }
            }
        }
        #endregion
        //删除===>通过选取复选框
        #region
        protected void btnDel_Click(object sender, EventArgs e)
        {
            int s = 0;
           for (int i = 0; i <= gvUserInfo.Rows.Count - 1; i++)
           {
               
                   CheckBox cbox = (CheckBox)gvUserInfo.Rows[i].Cells[1].FindControl("CheckBox1");
                   if (cbox.Checked == true)
                   {
                       s++;
                       Model.UserInfo u = new Model.UserInfo();
                       u.UserID = Convert.ToString(gvUserInfo.DataKeys[i].Value);
                       BLL.UserManageBll.DelUserInfo(u);
                   }
             
           }
           if (s == 0)
           {
               ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请选择要删除的用户！')", true);
           }
           else
           {
               ClientScript.RegisterStartupScript(this.GetType(), "", "alert('已删除" + s + "条数据')", true);
               BindUserInfo();   //刷新    
           }  
                
        }
        #endregion
        //查询
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindUserInfo();       
        }
        //首页
        protected void btnFirstPage_Click1(object sender, ImageClickEventArgs e)
        {
            gvUserInfo.PageIndex = 0;
            //同步
            lblPageIndex.Text = Convert.ToString(gvUserInfo.PageIndex + 1);  //当前第几页
            ddlPageIndex.Text = lblPageIndex.Text;
            BindUserInfo();
            
        }
        //尾页
        protected void btnEndPage_Click(object sender, ImageClickEventArgs e)
        {
            gvUserInfo.PageIndex = gvUserInfo.PageCount-1;
            //同步
            lblPageIndex.Text = Convert.ToString(gvUserInfo.PageIndex + 1);  //当前第几页
            ddlPageIndex.Text = lblPageIndex.Text;
            BindUserInfo();
          
        }
        //上一页
        protected void btnPrePage_Click(object sender, ImageClickEventArgs e)
        {
            if (this.gvUserInfo.PageIndex > 0)
            {
                this.gvUserInfo.PageIndex = this.gvUserInfo.PageIndex - 1;
                //同步
                lblPageIndex.Text = Convert.ToString(gvUserInfo.PageIndex + 1);  //当前第几页
                ddlPageIndex.Text = lblPageIndex.Text;
                BindUserInfo();
            }
        }
        //下一页
        protected void btnNextPage_Click(object sender, ImageClickEventArgs e)
        {
            if (this.gvUserInfo.PageIndex < this.gvUserInfo.PageCount)
            {
                this.gvUserInfo.PageIndex = this.gvUserInfo.PageIndex + 1;
                //同步
                lblPageIndex.Text = Convert.ToString(gvUserInfo.PageIndex);  //当前第几页
                ddlPageIndex.Text = Convert.ToString(gvUserInfo.PageIndex);
                BindUserInfo();
            }
           
        }
        //跳转
        protected void ddlPageIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvUserInfo.PageIndex = ddlPageIndex.SelectedIndex;
            lblPageIndex.Text = Convert.ToString(gvUserInfo.PageIndex + 1);  //当前第几页          
         
        }
        /// <summary>
        /// 在单击某个用于对列进行排序的超链接时发生，但在 GridView 控件执行排序操作之前。此事件通常用于取消排序操作或执行自定义的排序例程。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        protected void gvUserInfo_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sPage = e.SortExpression;
            if (ViewState["SortOrder"].ToString() == sPage)
            {
                if (ViewState["OrderDire"].ToString() == "DESC")
                    ViewState["OrderDire"] = "ASC";
                else
                    ViewState["OrderDire"] = "DESC";
            }
            else
            {
                ViewState["SortOrder"] = e.SortExpression;
            }
            bind();
        }
        #endregion
        //绑定GridView的方法====》排序
        #region
        public void bind()
        {
            SqlConnection sqlCon = new SqlConnection("Data Source=DESKTOP-HNAGO0B;Initial Catalog=Attendance;User ID=sa;Password=123456");
            sqlCon.Open();
            string strSql = "select * from UserInfo inner join Department on UserInfo.DeptID=Department.DeptID";
            SqlDataAdapter myda = new SqlDataAdapter(strSql,sqlCon);
            DataTable tabData = new DataTable();
            myda.Fill(tabData);
            //通过自定义视图来实现排序  
            DataView view = tabData.DefaultView;
            string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
            view.Sort = sort;
            gvUserInfo.DataSource = view;
            gvUserInfo.DataBind();
            sqlCon.Close();
           
        }
        #endregion
        //假分页
        #region
        protected void gvUserInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserInfo.PageIndex = e.NewPageIndex;
            BindUserInfo();
        }
         #endregion
    }
}