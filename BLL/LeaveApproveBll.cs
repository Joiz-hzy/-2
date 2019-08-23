using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BLL
{
   public class LeaveApproveBll
    {
        /// <summary>
        /// 查询=====>请假申请
        /// </summary>
        /// <param name="u"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
       public static DataTable SearchLeaveApprove(Model.Approve u, string userName)
       {
           return DAL.LeaveApproveDal.SearchLeaveApprove(u, userName);
        }
        /// <summary>
        /// 确定是否审批
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
       public static bool IsApprove(Model.Approve u)
       {
           return DAL.LeaveApproveDal.IsApprove(u);
       }
         /// <summary>
        /// 获取请假单信息
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
       public static DataTable GetApproveInfo(Model.Approve u)
       {
           return DAL.LeaveApproveDal.GetApproveInfo(u);
       }
          /// <summary>
        /// 通过ID删除请假数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public static int DeleteApprove(int id)
       {
           return DAL.LeaveApproveDal.DeleteApprove(id);
       }
         /// <summary>
        /// 实现对考勤的修改
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
       public static bool UpdateApproveInfo(Model.Approve u)
       {
           return DAL.LeaveApproveDal.UpdateApproveInfo(u);
       }
    }
}
