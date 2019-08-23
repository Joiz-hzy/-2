using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class MyAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取年月份
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                for (int i = year; i > year - 3; i--)
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

        protected void gvMyAttend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //判断当前行是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblFirstTime = (Label)e.Row.FindControl("lblFirstTime");
                Label lblLastTime = (Label)e.Row.FindControl("lblLastTime");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                DataRowView drv = (DataRowView)e.Row.DataItem;
                //1、得到当前行对应的日期
                DateTime date = (DateTime)drv["Date"];
                //2、得到首次打卡时间
                DateTime firstTime = new DateTime(1900, 1, 1);
                //3、得到最后打卡时间
                DateTime lastTime = new DateTime(1900, 1, 1);
                Model.UserInfo u = (Model.UserInfo)Session["User"];
                DataTable dt = BLL.AttendanceBll.GetAttendanceInfoByCondition(u.UserID, date);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["FirstTime"] != DBNull.Value)
                    {
                        firstTime = (DateTime)dr["FirstTime"];
                        lblFirstTime.Text = firstTime.ToString();
                    }
                    if (dr["LastTime"] != DBNull.Value)
                    {
                        lastTime = (DateTime)dr["LastTime"];
                        lblLastTime.Text = lastTime.ToString();
                    }
                }
                //4、得到当天的考勤状态的设置情况
                Model.AttendanceSetting a = BLL.AttendanceBll.GetAttendanceSettingByDate(date);
                //5、得到上班时间
                DateTime d1 = date.AddHours(8).AddMinutes(30);
                //6、得到下班时间
                DateTime d4 = date.AddHours(17).AddMinutes(30);
                if (a != null)
                {
                    //表示当前日期有考勤设置
                    if (a.Status == 2)
                    {
                        //a.Status == 2表示当天休假
                        lblStatus.Text = "<font color='#99ff00'>休假</font>";
                    }
                    else
                    {
                        if (lblFirstTime.Text == "")
                        {
                            lblStatus.Text = "<font color='#dd0000'>缺勤</font>";

                        }
                        else
                        {
                            //代表当天要上班
                            if (firstTime > d1)
                            {
                                lblStatus.Text = "<font color='#dd0033'>迟到</font>";
                            }
                            if (lastTime < d4)
                            {
                                lblStatus.Text = "<font color='#cc11ee'>早退</font>";
                            }
                            if ((firstTime > d1) && (lastTime < d4))
                            {
                                lblStatus.Text = "<font color='#dd0033'>迟到</font>&<font color='#cc11ee'>早退</font>";
                            }
                            if (DateTime.Parse(lblFirstTime.Text) < d1 && DateTime.Parse(lblLastTime.Text) > d4)
                            {
                                lblStatus.Text = "<font color='#000099'>正常</font>";
                            }
                        }
                    }

                }
                else
                {
                    //表示当前日期木有考勤设置
                    //判断当前日期是星期几
                    if ((date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday))
                    {
                        //表示当天为星期六或星期天
                        lblStatus.Text = "<font color='#99ff00'>休假</font>";
                    }
                    else
                    {
                        if (lblFirstTime.Text == "")
                        {
                            lblStatus.Text = "<font color='#dd0000'>缺勤</font>";
                        }
                        else
                        {
                            //表示当天是星期一到星期五
                            if (firstTime > d1)
                            {
                                lblStatus.Text = "<font color='#dd0033'>迟到</font>";
                            }
                            if (lastTime < d4)
                            {
                                lblStatus.Text = "<font color='#cc11ee'>早退</font>";
                            }
                            if ((firstTime > d1) && (lastTime < d4))
                            {
                                lblStatus.Text = "<font color='#dd0033'>迟到</font>&<font color='#cc11ee'>早退</font>";
                            }
                            if (DateTime.Parse(lblFirstTime.Text) < d1 && DateTime.Parse(lblLastTime.Text) > d4)
                            {
                                lblStatus.Text = "<font color='#000099'>正常</font>";
                            }
                        }

                    }
                }
            }
        }
        //查看
        protected void btnShow_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(DateTime));
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Date"] = new DateTime(year, month, i);
                dt.Rows.Add(dr);
            }
            gvMyAttend.DataSource = dt;
            gvMyAttend.DataBind();
        }
    }
}