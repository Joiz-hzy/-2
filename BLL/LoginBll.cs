using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BLL
{
   public class LoginBll
    {
         /// <summary>
        /// 验证用户名和密码
       /// </summary>
       /// <param name="userID">用户名</param>
       /// <param name="password">密码</param>
       /// <returns></returns>
       public static Model.UserInfo ValidateUserAndPwd(string userID, string password)
       {
           return DAL.LoginDal.ValidateUserAndPwd(userID, password);
       }
   /// <summary>
        /// 实现对用户密码的修改
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
       public static bool UpdateUserPwd(Model.UserInfo u)
       {
           return DAL.LoginDal.UpdateUserPwd(u);
       }
    }
}
