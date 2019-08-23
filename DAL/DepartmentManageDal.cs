using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
   public class DepartmentManageDal
    {
        //============================DepartmentManage==========================================
       //获取部门主管名字
       public static DataTable GetDeptChargeName()
       {
           string strSql = "select * from UserInfo inner join Department on UserInfo.DeptID=Department.DeptID and UserType=1";
           return DBHelper.ExecuteSelect(strSql);
       }
       //查询部门
       public static DataTable SearchDeptInfo(string deptName, int deptID)
       {
           string strSql = "select * from Department left join UserInfo on userinfo.userid=department.ManagerID  where DeptName like '%" + deptName + "%'";
           if (deptID != 0)
           {
               strSql += " and UserInfo.DeptID= " + deptID + "";
           }
           return DBHelper.ExecuteSelect(strSql);
       }
       //根据用户ID删除用户信息
       public static bool DelDeptInfo(Model.DepartmentInfo u)
       {
           string sql = "delete from Department where DeptID=@DeptID";
           SqlParameter[] para ={
                            new SqlParameter("DeptID",u.DeptID)
                            };
           return DBHelper.ExecuteNonQuery(sql, para);
       }
       /// <summary>
       /// 判断部门员工===》是否显示删除按钮
       /// </summary>
       /// <param name="u"></param>
       /// <returns></returns>
       public static DataTable JudgeDeptEmp (Model.DepartmentInfo u)
       {
           string sql = "select * from UserInfo inner join Department on UserInfo.DeptID=Department.DeptID where UserInfo.UserType=0 and Department.DeptName=@DeptName";
           SqlParameter[] para ={
                            new SqlParameter("DeptName",u.DeptName)
                            };
           return DBHelper.ExecuteSelect(sql, para);
       }
       //===============================DepartmentEdit=====================================
       /// <summary>
       /// 实现部门信息的添加
       /// </summary>
       /// <param name="u"></param>
       /// <returns></returns>
       public static bool InsertDeptInfo(Model.DepartmentInfo u)
       {
           //参数化sql语句
           string strSql = "INSERT INTO [Department] ([DeptName], [ManagerID], [DeptInfo]) VALUES (@DeptName, @ManagerID, @DeptInfo)";
           SqlParameter[] para ={
                                 new SqlParameter("DeptName",u.DeptName),
                                 new SqlParameter("ManagerID",u.ManagerID),
                                 new SqlParameter("DeptInfo",u.DeptInfo),
                                };
           return DBHelper.ExecuteNonQuery(strSql, para);

       }
       /// <summary>
       /// 根据部门id获取单个对象
       /// </summary>
       /// <param name="managerID">主管ID</param>
       /// <returns></returns>
       public static Model.DepartmentInfo GetSingleDeptInfo(int deptID)
       {
           string strSql = "select * from Department where DeptID=@DeptID";
           SqlParameter[] para ={
                            new SqlParameter("DeptID",deptID)
                            };
           DataTable dt = DBHelper.ExecuteSelect(strSql, para);
           DataRow dr = dt.Rows[0];
           Model.DepartmentInfo u = new Model.DepartmentInfo();
           u.DeptName = (string)dr["DeptName"];
           u.DeptInfo = (string)dr["DeptInfo"];
           u.DeptID = (int)dr["DeptID"];
           u.ManagerID = (string)dr["ManagerID"];
           return u;
       }
       /// <summary>
       /// 实现对部门管理数据的修改
       /// </summary>
       /// <param name="u"></param>
       /// <returns></returns>
       public static bool UpdateDeptInfo(Model.DepartmentInfo u)
       {
           //参数化sql语句
           string strSql = "UPDATE [Department] SET [DeptName] = @DeptName, [ManagerID] = @ManagerID, [DeptInfo] = @DeptInfo WHERE [DeptID] = @DeptID";
           SqlParameter[] para ={
                                 new SqlParameter("DeptName",u.DeptName),
                                 new SqlParameter("ManagerID",u.ManagerID),
                                 new SqlParameter("DeptInfo",u.DeptInfo),
                                 new SqlParameter("DeptID",u.DeptID),
                                };
           return DBHelper.ExecuteNonQuery(strSql, para);

       }
    }
}
