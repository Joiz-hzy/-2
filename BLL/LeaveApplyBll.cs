using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BLL
{
   public class LeaveApplyBll
    {
        //查询=====>请假申请
        //第一种  [DIY]
        //public static DataTable SearchAskForLeave(Model.Approve a, string applyUser)
        //{
        //    return DAL.LeaveApplyDal.SearchAskForLeave(a, applyUser);
        //}
        //第二种【Teacher】
        public static DataTable SearchAskForLeave(string userID, string title, string beginDate, string endDate, byte status)
        {
            return DAL.LeaveApplyDal.SearchAskForLeave(userID, title, beginDate, endDate, status);
        }
        /// <summary>
        /// 添加请假条
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool InsertAttendInfo(Model.Approve u)
        {
            return DAL.LeaveApplyDal.InsertAttendInfo(u);
        }
        /// <summary>
        /// 根据ApproveID获取单个用户对象
        /// </summary>
        /// <param name="userID">审批ID</param>
        /// <returns></returns>
        public static DataTable GetSingleApproveInfo(Model.Approve u)
        {
            return DAL.LeaveApplyDal.GetSingleApproveInfo(u);
        }
        /// <summary>
        /// 实现对考勤的修改
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool UpdateApproveInfo(Model.Approve u)
        {
            return DAL.LeaveApplyDal.UpdateApproveInfo(u);
        }
        //根据用户ID删除用户信息
        public static bool DelApproveInfo(Model.Approve u)
        {
            return DAL.LeaveApplyDal.DelApproveInfo(u);
        }
    }
}
