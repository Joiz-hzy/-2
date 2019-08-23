using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Model
{
    /// <summary>
    /// Approve（请假申请表)
    /// </summary>
   public class Approve
    {
       /// <summary>
        /// 请假单ID
       /// </summary>
        public int ApproveID { get; set; }
       /// <summary>
        /// 申请人ID
       /// </summary>
        public string ApplyUser { get; set; }
       /// <summary>
        /// 请假单标题
       /// </summary>
        public string Title { get; set; }
       /// <summary>
        /// 请假起始时间
       /// </summary>
        public DateTime BeginDate { get; set; }
       /// <summary>
        /// 请假结束时间
       /// </summary>
        public DateTime EndDate { get; set; }
       /// <summary>
        /// 请假原因
       /// </summary>
        public string Reason { get; set; }
       /// <summary>
        /// 审批人ID
       /// </summary>
        public string ApproveUser { get; set; }
       /// <summary>
        /// 申请时间
       /// </summary>
        public DateTime ApplyDate { get; set; }
       /// <summary>
        /// 批复时间
       /// </summary>
        public DateTime ApproveDate { get; set; }
       /// <summary>
        /// 请假单状态  0，待办；1，归档
       /// </summary>
        public byte Status { get; set; }
       /// <summary>
        /// 审批结果  0，不同意；1，同意
       /// </summary>
        public byte Result { get; set; } 
       /// <summary>
        /// 备注
       /// </summary>
        public string Remark { get; set; }


        /// <summary>
        /// 用户ID（用户表）
        /// </summary>
        public string UserID { get; set; }
      
    }
}
