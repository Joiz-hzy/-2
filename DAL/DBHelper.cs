using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DAL
{
    public class DBHelper
    {
        //1、获取数据库连接字符串
        public static string connString = ConfigurationManager.ConnectionStrings["AttendanceConnectionString"].ConnectionString;
        //2、专门用来执行增、删、改的方法
        /// <summary>
        /// 此方法专门用来执行增、删、改的sql语句;如果执行成功，则返回true;如果执行失败，则返回false;
        /// </summary>
        /// <param name="sql">参数化的sql语句</param>
        /// <param name="para">SqlParameter数组型的参数:如果此sql语句没有参数则para为null;否则在调用方传一个SqlParameter[]数组</param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string sql, params SqlParameter[] para)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (para != null)
                    {
                        cmd.Parameters.AddRange(para);
                    }
                    //打开连接
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    int i = cmd.ExecuteNonQuery();
                    return i > 0 ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //3、专门用来执行查询的方法
        /// <summary>
        /// 此方法专门用来执行sql语句，并且返回一个DataTable对象
        /// </summary>
        /// <param name="sql">参数化的sql语句(一般为含有select关键字的sql语句)</param>
        /// <param name="para">SqlParameter数组型的参数:如果此sql语句没有参数则para为null;否则在调用方传一个SqlParameter[]数组</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteSelect(string sql, params SqlParameter[] para)
        {
            //SQL注入式攻击
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, connString);
                if (para != null)
                {
                    da.SelectCommand.Parameters.AddRange(para);
                }
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
