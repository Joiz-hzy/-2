using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
  public  class LeaveApplyDal
    {

        //查询=====>请假申请
      //第一种  [DIY]
      //public static DataTable SearchAskForLeave(Model.Approve a, string applyUser)
      //  {
        //string strSql = string.Format("select CASE WHEN  status = 0 THEN  '待审批' WHEN  status = 1 THEN  '归档' ELSE  '' END  AS   status, * from Approve inner join UserInfo on UserInfo.UserID=Approve.ApplyUser and ApplyUser='{0}'",applyUser);        
        //if (a.Title != "")   //如果标题不为空  
        //{
        //    strSql += " and title like '%" + a.Title + "%'";
        //   // sb.AppendFormat(" and title like '%" + a.Title + "%'");
        //}
        //if (a.BeginDate.ToString() != "" || a.EndDate.ToString() != "")    //判断申请时间是否为空
        //{
        //    strSql += "  and ApplyDate >'" + a.BeginDate + "' and ApplyDate < '" + a.EndDate + "'";
        //}       
        //if (a.Status != 2)   //请假状态  0待办  1归档  2全部  当前状态为全部
        //{

        //    strSql += " and Status=" + a.Status;
        //}
        //if (a.ApplyUser != null)//申请人ID
        //{
        //    strSql += " and ApplyUser='" + a.ApplyUser + "'";
        //}
        //return DBHelper.ExecuteSelect(strSql);

      //  }
        //第二种【Teacher】
      public static DataTable SearchAskForLeave(string userID, string title, string beginDate, string endDate, byte status)
      {
          StringBuilder sb = new StringBuilder();
          sb.AppendFormat("select * from Approve inner join UserInfo on Approve.ApplyUser=UserInfo.UserID where UserID='{0}' and Title like '%{1}%'", userID, title);
          if (!string.IsNullOrEmpty(beginDate))
          {
              //表示beginDate不为空值
              sb.AppendFormat(" and ApplyDate>='{0}'", beginDate);
          }
          if (!string.IsNullOrEmpty(endDate))
          {
              //表示endDate不为空值
              sb.AppendFormat(" and ApplyDate<='{0}'", endDate);
          }
          if (status != 2)
          {
              sb.AppendFormat(" and Status={0}", status);
          }
          return DBHelper.ExecuteSelect(sb.ToString());
      }
         
        /// <summary>
        /// 添加请假条
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool InsertAttendInfo(Model.Approve u)
        {
            //参数化sql语句
            string strSql = "INSERT INTO [Approve] ([ApplyUser], [Title], [BeginDate], [EndDate], [Reason], [ApproveUser], [ApplyDate], [ApproveDate], [Status], [Result], [Remark]) VALUES (@ApplyUser, @Title, @BeginDate, @EndDate, @Reason, @ApproveUser, @ApplyDate, @ApproveDate, @Status, @Result, @Remark)";
            SqlParameter[] para ={
                               new SqlParameter("ApplyUser",u.ApplyUser),//申请人ID
                               new SqlParameter("Title",u.Title),//标题
                               new SqlParameter("BeginDate",u.BeginDate),//请假起始时间
                               new SqlParameter("EndDate",u.EndDate),//请假结束时间
                               new SqlParameter("Reason",u.Reason),//请假原因
                               new SqlParameter("ApproveUser",""),//审批人ID
                               new SqlParameter("ApplyDate",u.ApplyDate),//申请时间
                               new SqlParameter("ApproveDate",DBNull.Value),//批复时间
                               new SqlParameter("Status",u.Status),//请假单状态
                               new SqlParameter("Result",""),//审批结果
                               new SqlParameter("Remark",""),//备注
                               };
            return DBHelper.ExecuteNonQuery(strSql, para);

        }
        public static DataTable GetSingleApproveInfo(Model.Approve u)
        {
            string strSql = "select * from Approve left join UserInfo on Approve.ApplyUser=UserInfo.UserID where UserID=@UserID and ApproveID=@ApproveID";
            SqlParameter[] para ={
                                new SqlParameter("UserID",u.UserID),
                                new SqlParameter("ApproveID",u.ApproveID),
                               };
            return DBHelper.ExecuteSelect(strSql, para);
        }
        /// <summary>
        /// 实现对考勤的修改
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool UpdateApproveInfo(Model.Approve u)
        {
            //参数化sql语句
            string strSql = "UPDATE [Approve] SET [Title] = @Title, [BeginDate] = @BeginDate, [EndDate] = @EndDate, [Reason] = @Reason WHERE [ApproveID] = @ApproveID";
            SqlParameter[] para ={
                                new SqlParameter("Title",u.Title),
                                new SqlParameter("BeginDate",u.BeginDate),
                                new SqlParameter("EndDate",u.EndDate),
                                new SqlParameter("Reason",u.Reason),
                                new SqlParameter("ApproveID",u.ApproveID),
                               };
            return DBHelper.ExecuteNonQuery(strSql, para);


        }
        //根据用户ID删除用户信息
        public static bool DelApproveInfo(Model.Approve u)
        {
            string sql = "delete from Approve where ApproveID=@ApproveID";
            SqlParameter[] para ={
                            new SqlParameter("ApproveID",u.ApproveID)
                            };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
    }
}
