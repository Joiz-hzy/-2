using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
   public class AttendanceDal
    {
        //===========================考勤管理==========================================
       //考勤管理====查询考勤信息
       public static DataTable SearchAttendInfo(int deptID)
       {
           //sql语句
           string strSql = string.Format("select * from UserInfo inner join Department on UserInfo.DeptID=Department.DeptID where UserInfo.DeptID='{0}'", deptID);
           return DBHelper.ExecuteSelect(strSql);
       }
       /// <summary>
       /// 实现批量添加考勤数据==>导入考勤
       /// </summary>
       /// <param name="dt"></param>
       /// <returns></returns>
       public static bool ImportAttendData(DataTable dt)
       {
           try
           {
               SqlBulkCopy bc = new SqlBulkCopy(DBHelper.connString);  //SqlBulkCopy【批处理《批量》】
               bc.DestinationTableName = "AttendanceInfo";   //目标表==》与sql表明相匹配
               bc.WriteToServer(dt);
               bc.Close();
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }
        //===========================考勤设置==========================================
       /// <summary>
       /// 实现考勤设置的批量添加
       /// </summary>
       /// <param name="dt"></param>
       /// <returns></returns>
       public static bool InsertDataTable(DataTable dt)
       {
           try
           {
               SqlBulkCopy bc = new SqlBulkCopy(DBHelper.connString);
               bc.DestinationTableName = "AttendanceSetting";
               bc.WriteToServer(dt);
               bc.Close();
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }
       /// <summary>
       /// 删除年和月现有的设置
       /// </summary>
       /// <param name="year"></param>
       /// <param name="month"></param>
       /// <returns></returns>
       public static bool DeleteAttendanceSetting(int year, int month)
       {
           string sql = "delete from AttendanceSetting where year(Date)=@Year and month(Date)=@Month";
           SqlParameter[] para = { 
                                    new SqlParameter("Year",year),
                                    new SqlParameter("Month",month)
                                  };
           return DBHelper.ExecuteNonQuery(sql, para);
       }
       /// <summary>
       /// 根据日期得到该日期对应的考勤设置情况
       /// </summary>
       /// <param name="date"></param>
       /// <returns></returns>
       public static Model.AttendanceSetting GetAttendanceSettingByDate(DateTime date)
       {
           string sql = "select * from AttendanceSetting where Date=@Date";
           SqlParameter[] para = { 
                                    new SqlParameter("Date",date)
                                  };
           DataTable dt = DBHelper.ExecuteSelect(sql, para);
           Model.AttendanceSetting a;
           if (dt.Rows.Count > 0)
           {
               //表示有考勤设置
               DataRow dr = dt.Rows[0];//得到第一行数据
               a = new Model.AttendanceSetting();
               a.Date = (DateTime)dr["Date"];
               a.SettingID = (int)dr["SettingID"];
               a.Status = (byte)dr["Status"];
           }
           else
           {
               //表示没有考勤设置
               a = null;
           }
           return a;
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
           string sql = "select min(FaceTime) FirstTime,max(FaceTime) LastTime from AttendanceInfo where UserID=@UserID and convert(varchar(10),FaceTime,112)=@Date";
           SqlParameter[] para = { 
                                    new SqlParameter("UserID",userID),
                                    new SqlParameter("Date",date)
                                  };
           return DBHelper.ExecuteSelect(sql, para);
       }
    }
}
