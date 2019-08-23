using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BLL
{
    public class UserManageBll
    {
        //=======================UserManage=====================
       //获取所有部门名字
        public static DataTable GetAllDeptName()
        {
            return DAL.UserManageDal.GetAllDeptName();
        }
             //查询用户
        public static DataTable SearchUserInFo(string userID, string userName, int deptID)
        {
            return DAL.UserManageDal.SearchUserInFo(userID, userName, deptID);
        }
          //根据用户ID删除用户信息
        public static bool DelUserInfo(Model.UserInfo u)
        {
            return DAL.UserManageDal.DelUserInfo(u);
        }
        //=======================UserManageEdit=====================
        /// <summary>
        /// 实现对用户信息的添加
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool InsertUserInfo(Model.UserInfo u)
        {
            return DAL.UserManageDal.InsertUserInfo(u);

        }
        /// <summary>
        /// 根据userID获取单个用户对象
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public static Model.UserInfo GetSingleUserInfo(string userID)
        {
            return DAL.UserManageDal.GetSingleUserInfo(userID);
        }
        /// <summary>
        /// 实现对用户管理的修改
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(Model.UserInfo u)
        {
            return DAL.UserManageDal.UpdateUserInfo(u);
        }
    }
}
