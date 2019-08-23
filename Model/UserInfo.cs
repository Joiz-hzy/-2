using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Model
{
    /// <summary>
    /// UserInfo(用户信息表）
    /// </summary>
  public  class UserInfo
    {
      /// <summary>
        /// 用户ID
      /// </summary>
        public string UserID { get; set; }//3次按tab键
      /// <summary>
        /// 用户名称
      /// </summary>
        public string UserName { get; set; }
      /// <summary>
        /// 部门ID
      /// </summary>
        public int DeptID { get; set; }
      /// <summary>
        /// 密码
      /// </summary>
        public string Password { get; set; }
      /// <summary>
        /// 手机
      /// </summary>
        public string Cellphone { get; set; }
      /// <summary>
        /// 用户类型   0，员工；1，主管；2，系统管理员
      /// </summary>
        public byte UserType { get; set; }
                             
    }
}
