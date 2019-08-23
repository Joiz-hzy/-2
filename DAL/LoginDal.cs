using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class LoginDal
    {
        /// <summary>
        /// 验证用户名和密码
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static Model.UserInfo ValidateUserAndPwd(string userID, string password)
        {
            //string sql = string.Format("select * from UserInfo where UserID='{99}' and Password='{100}'",userID,password,,,,,);
            //参数化sql语句      防止sql注入式攻击
            string strSql = "select * from UserInfo where UserID=@UserID and Password=@Password";
            SqlParameter[] para ={
                                 new SqlParameter("UserID",userID),
                                 new SqlParameter("Password",password)
                                };
            DataTable dt = DBHelper.ExecuteSelect(strSql, para);
            Model.UserInfo u;
            if (dt.Rows.Count > 0)//用户名和密码正确
            {
                u = new Model.UserInfo();
                DataRow dr = dt.Rows[0];//得到DataTable里面的第一行
                u.Cellphone = (string)dr["Cellphone"];
                if (dr["DeptID"] != DBNull.Value)//表示在表里面不是null值
                {
                    u.DeptID = (int)dr["DeptID"];
                }
                u.Password = (string)dr["Password"];
                u.UserID = (string)dr["UserID"];
                u.UserName = (string)dr["UserName"];
                u.UserType = (byte)dr["UserType"];
            }
            else
            {
                u = null;
            }
            return u;

        }
        /// <summary>
        /// 实现对用户密码的修改
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool UpdateUserPwd(Model.UserInfo u)
        {
            //参数化sql语句
            string strSql = "UPDATE [UserInfo] SET [Password] = @Password,[Cellphone] = @Cellphone WHERE [UserID] = @UserID";
            SqlParameter[] para ={
                                new SqlParameter("UserID",u.UserID),
                                new SqlParameter("Password",u.Password),
                                  new SqlParameter("Cellphone",u.Cellphone)      
                                };
            return DBHelper.ExecuteNonQuery(strSql, para);


        }

    }
}
