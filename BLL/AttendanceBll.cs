using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BLL
{
   public class AttendanceBll
    {

        //===========================考勤管理==========================================
        //考勤管理====查询考勤信息
       public static DataTable SearchAttendInfo(int deptID)
       {
           return DAL.AttendanceDal.SearchAttendInfo(deptID);
       }
       /// <summary>
       /// 实现批量添加考勤数据
       /// </summary>
       /// <param name="dt"></param>
       /// <returns></returns>
       public static bool ImportAttendData(DataTable dt)
       {
           return DAL.AttendanceDal.ImportAttendData(dt);
       }
        //===========================考勤设置==========================================
       /// <summary>
       /// 实现考勤设置的批量添加
       /// </summary>
       /// <param name="dt"></param>
       /// <returns></returns>
       public static bool InsertDataTable(DataTable dt)
       {
           return DAL.AttendanceDal.InsertDataTable(dt);
       }
       /// <summary>
       /// 删除年和月现有的设置
       /// </summary>
       /// <param name="year"></param>
       /// <param name="month"></param>
       /// <returns></returns>
       public static bool DeleteAttendanceSetting(int year, int month)
       {
           return DAL.AttendanceDal.DeleteAttendanceSetting(year, month);
       }
       /// <summary>
       /// 根据日期得到该日期对应的考勤设置情况
       /// </summary>
       /// <param name="date"></param>
       /// <returns></returns>
       public static Model.AttendanceSetting GetAttendanceSettingByDate(DateTime date)
       {
           return DAL.AttendanceDal.GetAttendanceSettingByDate(date);
       }

        //===========================我的考勤==========================================
        /// <summary>
       /// 根据UserID和日期得到考勤信息
       /// </summary>
       /// <param name="userID"></param>
       /// <param name="date"></param>
       /// <returns></returns>
       public static DataTable GetAttendanceInfoByCondition(string userID, DateTime date)
       {
           return DAL.AttendanceDal.GetAttendanceInfoByCondition(userID,date); 
       }
    }
}
