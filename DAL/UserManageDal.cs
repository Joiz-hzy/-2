using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class UserManageDal
    {
        //=======================UserManage=====================
        //获取所有部门名字
        public static DataTable GetAllDeptName()
        {
            string strSql = "select * from Department";
            return DBHelper.ExecuteSelect(strSql);
        
        }
        //查询用户
        public static DataTable SearchUserInFo(string userID, string userName, int deptID)
       {
           string strSql = " select * from UserInfo left join Department on UserInfo.DeptID=Department.DeptID where usertype not in (2) and UserID like '%" + userID + "%' and  UserName like '%" + userName + "%'";
           if(deptID!=0)
           {
               //进行字符串的追加
           strSql+=" and UserInfo.DeptID="+deptID+"";
           }
           return DBHelper.ExecuteSelect(strSql);
       }
        //根据用户ID删除用户信息
        public static bool DelUserInfo(Model.UserInfo u)
        {
            string sql = "delete from UserInfo where UserID=@UserID";
            SqlParameter[] para ={
                            new SqlParameter("UserID",u.UserID)
                            };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        //=======================UserManageEdit=====================
        /// <summary>
        /// 实现对用户信息的添加
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool InsertUserInfo(Model.UserInfo u)
        {
            //参数化sql语句
            string strSql = "INSERT INTO [UserInfo] ([UserID], [UserName], [DeptID], [Password], [Cellphone], [UserType]) VALUES (@UserID, @UserName, @DeptID, @Password, @Cellphone, @UserType)";
            SqlParameter[] para ={
                                new SqlParameter("UserID",u.UserID),
                                 new SqlParameter("UserName",u.UserName),
                                  new SqlParameter("DeptID",u.DeptID),
                                   new SqlParameter("Password",u.Password),
                                    new SqlParameter("Cellphone",u.Cellphone),
                                  new SqlParameter("UserType",u.UserType),
                              
                                };
            return DBHelper.ExecuteNonQuery(strSql, para);

        }

        /// <summary>
        /// 根据userID获取单个用户对象
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public static Model.UserInfo GetSingleUserInfo(string userID)
        {
            string strSql = "select * from userInfo where UserID=@userID";
            SqlParameter[] para ={
                                 new SqlParameter("UserID",userID)
                                 };
            DataTable dt = DBHelper.ExecuteSelect(strSql, para);
            DataRow dr = dt.Rows[0];  //得到内存表的第一行数据
            Model.UserInfo u = new Model.UserInfo();
            u.Cellphone = (string)dr["Cellphone"];
            u.DeptID = (int)dr["DeptID"];
            u.UserID = (string)dr["UserID"]; ;
            u.UserName = (string)dr["UserName"]; ;
            u.UserType = (byte)dr["UserType"]; ;
            return u;

        }
        /// <summary>
        /// 实现对用户管理的修改
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(Model.UserInfo u)
        {
            //参数化sql语句
            string strSql = "UPDATE [UserInfo] SET [UserName] = @UserName, [DeptID] = @DeptID,[Cellphone] = @Cellphone, [UserType] = @UserType WHERE [UserID] = @UserID";
            SqlParameter[] para ={
                                new SqlParameter("UserID",u.UserID),
                                 new SqlParameter("UserName",u.UserName),
                                 new SqlParameter("DeptID",u.DeptID),
                                 new SqlParameter("Cellphone",u.Cellphone),
                                  new SqlParameter("UserType",u.UserType),
                              
                                };
            return DBHelper.ExecuteNonQuery(strSql, para);


        }
    }
}
