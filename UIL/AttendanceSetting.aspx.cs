using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace UIL
{
    public partial class AttendanceSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //隐藏保存按钮
                btnSave.Visible = false;
                //获取年月份
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;             
                for (int i = year; i >year-3; i--)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }    
                for (int i = 1; i <= 12; i++)
                {
                    ddlMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddlYear.SelectedValue = year.ToString();
                ddlMonth.SelectedValue = month.ToString();
            }
        }
        //显示
        protected void btnShow_Click(object sender, EventArgs e)
        {
            //显示保存按钮
            btnSave.Visible = true;
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);
            //整合一个DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(DateTime));
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Date"] = new DateTime(year, month, i);
                dt.Rows.Add(dr);
            }
            gvAttendSettings.DataSource = dt;
            gvAttendSettings.DataBind();
            
        }
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //整合一个DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("SettingID", typeof(int));
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("Status", typeof(byte));
            foreach (GridViewRow gvr in gvAttendSettings.Rows)
            {
                DropDownList ddlStatus = (DropDownList)gvr.FindControl("ddlStatus");
                Label lblDate = (Label)gvr.FindControl("lblDate");
                if (ddlStatus.SelectedValue != "0")
                {
                    //表示状态改变了
                    DataRow dr = dt.NewRow();
                    dr["Date"] = Convert.ToDateTime(lblDate.Text);
                    dr["Status"] = Convert.ToByte(ddlStatus.SelectedValue);
                    dt.Rows.Add(dr);
                }
            }
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);
            //先删掉现有的设置
            BLL.AttendanceBll.DeleteAttendanceSetting(year, month);
            //后进行添加
            if (BLL.AttendanceBll.InsertDataTable(dt))
            {
                //表示设置成功
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('恭喜您，考勤设置成功！');", true);
            }
            else
            {
                //表示设置失败
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('对不起，考勤设置失败！');", true);
            }
        }

        protected void gvAttendSettings_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                DataRowView drv = (DataRowView)e.Row.DataItem;
                //1、得到当前行对应的日期
                DateTime date = (DateTime)drv["Date"];
                Model.AttendanceSetting a = BLL.AttendanceBll.GetAttendanceSettingByDate(date);
                if (a != null)
                {
                    //表示有考勤设置
                    ddlStatus.SelectedValue = a.Status.ToString();
                }
            }
        }
    }
}