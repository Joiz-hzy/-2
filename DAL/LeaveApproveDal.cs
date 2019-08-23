using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
   public class LeaveApproveDal
    {
        //查询=====>请假审批
        public static DataTable SearchLeaveApprove(Model.Approve u,string userName)
        {           
            string strSql = "select CASE WHEN  status = 0 THEN  '待审批' WHEN  status = 1 THEN  '归档' ELSE  '' END  AS   status, * from Approve inner join UserInfo on UserInfo.UserID=Approve.ApplyUser";
            if (u.Title != "")   //如果标题不为空  
            {
                strSql += " and title like '%" + u.Title + "%'";
            }
            if (u.BeginDate.ToString() != "" || u.EndDate.ToString() != "")    //判断申请时间是否为空
            {
                // strSql += "  and ApplyDate between '" + u.BeginDate + "' and '" + u.EndDate + "'";
                strSql += "  and ApplyDate >'" + u.BeginDate + "' and ApplyDate < '" + u.EndDate + "'";
            }
            if (u.Status != 2)   //请假状态  0待办  1归档  2全部  当前状态为全部
            {

                strSql += " and Status=" + u.Status ;
            }
            //如果申请人不为空
            if(u.ApplyUser!="")
            {
                strSql += " and UserInfo.UserName like '%" + userName+ "%'";
            
            }
            return DBHelper.ExecuteSelect(strSql);

        }
        /// <summary>
        /// 确定是否审批
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool IsApprove(Model.Approve u)
        {
            string strSql = "UPDATE [Approve] SET [ApproveUser] = @ApproveUser, [ApproveDate] = @ApproveDate, [Result] = @Result, [Status] = @Status, [Remark] = @Remark WHERE [ApproveID] = @ApproveID";
            SqlParameter[] para ={
                                new SqlParameter("Result",u.Result),//审批结果
                               new SqlParameter("Remark",u.Remark),//备注
                               new SqlParameter("ApproveDate",u.ApproveDate),//批复时间
                               new SqlParameter("ApproveUser",u.ApproveUser),//审批人ID
                               new SqlParameter("ApproveID",u.ApproveID),//请假单号
                               new SqlParameter("Status",u.Status),//请假单状态
                               };
            return DBHelper.ExecuteNonQuery(strSql, para);
        }
        /// <summary>
        /// 获取请假单信息
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static DataTable GetApproveInfo(Model.Approve u)
        {
            string strSql = "select * from Approve left join UserInfo on Approve.ApplyUser=UserInfo.UserID where ApproveID=@ApproveID";
            SqlParameter[] para ={
                                new SqlParameter("ApproveID",u.ApproveID),
                               };
            return DBHelper.ExecuteSelect(strSql, para);
        }
        /// <summary>
        /// 通过ID删除请假数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteApprove(int id)
        {
            //编写sql语句
            string strSql = "delete from Approve where ApproveID =" + id;
            int i = 0;
            try
            {
                bool b = DBHelper.ExecuteNonQuery(strSql);

                if (b)
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
            }
            catch
            {
                i = -1;
            }
            return i;
        }

         public static bool UpdateApproveInfo(Model.Approve u)
         {              
            //参数化sql语句
             string strSql = "UPDATE [Approve] SET [Title] = @Title where ApproveID =" + u.ApproveID;
            SqlParameter[] para ={
                                new SqlParameter("Title",u.Title),                                      
                               };
        
               return DBHelper.ExecuteNonQuery(strSql, para);
        }
          

          
        }
    }

