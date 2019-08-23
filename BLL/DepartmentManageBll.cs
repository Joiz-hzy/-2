using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BLL
{
   public class DepartmentManageBll
    {
        //============================DepartmentManage==========================================
        //获取部门主管名字
        public static DataTable GetDeptChargeName()
        {
            return DAL.DepartmentManageDal.GetDeptChargeName();
        }
       //查询部门
        public static DataTable SearchDeptInfo(string deptName, int deptID)
        {
            return DAL.DepartmentManageDal.SearchDeptInfo(deptName, deptID);
        }
         //根据用户ID删除用户信息
        public static bool DelDeptInfo(Model.DepartmentInfo u)
        {
            return DAL.DepartmentManageDal.DelDeptInfo(u);
        }
           /// <summary>
       /// 判断部门员工===》是否显示删除按钮
       /// </summary>
       /// <param name="u"></param>
       /// <returns></returns>
        public static DataTable JudgeDeptEmp(Model.DepartmentInfo u)
        {
            return DAL.DepartmentManageDal.JudgeDeptEmp(u);
        }
        //===============================DepartmentEdit=====================================
        /// <summary>
        /// 实现部门信息的添加
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool InsertDeptInfo(Model.DepartmentInfo u)
        {
            return DAL.DepartmentManageDal.InsertDeptInfo(u);
        }
        /// <summary>
        /// 根据部门id获取单个对象
        /// </summary>
        /// <param name="managerID">主管ID</param>
        /// <returns></returns>
        public static Model.DepartmentInfo GetSingleDeptInfo(int deptID)
        {
            return DAL.DepartmentManageDal.GetSingleDeptInfo(deptID);
        }
        /// <summary>
        /// 实现对部门管理数据的修改
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static bool UpdateDeptInfo(Model.DepartmentInfo u)
        {
            return DAL.DepartmentManageDal.UpdateDeptInfo(u);

        }
    }
}
