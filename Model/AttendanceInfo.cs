using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// AttendanceInfo（考勤记录表）
    /// </summary>
  public  class AttendanceInfo
    {
      /// <summary>
        /// 考勤记录ID
      /// </summary>
        public int AttendanceID { get; set; }
      /// <summary>
        /// 用户ID
      /// </summary>
        public string UserID { get; set; }
      /// <summary>
        /// 打卡时间
      /// </summary>
        public DateTime FaceTime { get; set; }
    }
}
