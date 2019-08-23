using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// AttendanceSetting（考勤设置表）
    /// </summary>
  public  class AttendanceSetting
  {
      /// <summary>
      /// 考勤设置ID
      /// </summary>
      public int SettingID { get; set; }
      /// <summary>
      /// 日期
      /// </summary>
      public DateTime Date { get; set; }
      /// <summary>
      /// 0，默认；1，上班；2，休假（如果该表中没有某一天的数据，则表示当天按照默认状态处理（默认的工作日、周末休假）
      /// </summary>
      public byte Status { get; set; }

    }
}
