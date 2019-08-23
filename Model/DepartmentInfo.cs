using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// Department（部门表）
    /// </summary>
   public class DepartmentInfo
    {
       /// <summary>
        /// 部门ID
       /// </summary>
        public int DeptID { get; set; }
       /// <summary>
        /// 部门名称
       /// </summary>
        public string DeptName { get; set; }
       /// <summary>
        /// 部门负责人主管ID
       /// </summary>
        public string ManagerID { get; set; }
       /// <summary>
        /// 备注信息
       /// </summary>
        public string DeptInfo { get; set; }
        public string UserName { get; set; }
    }
}
