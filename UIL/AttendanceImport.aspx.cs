using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;  //用来查找excel表文件
namespace UIL
{
    public partial class AttendanceImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            //先检查别人有没有上传文件
            if (fuFile.HasFile)
            {
                //表示别人上传了文件
                string extName = Path.GetExtension(fuFile.FileName);//得到文件的后缀名
                if (extName != ".xls" && extName != ".xlsx")
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请上传包含考勤数据的Excel文件');", true);
                }
                else
                {
                    //表示别人上传的是Excel
                    string path = "~/Uploads/" + fuFile.FileName;//相对路径
                    fuFile.SaveAs(Server.MapPath(path));//Server.MapPath(path):把相对路径转换成绝对路径
                    //通过ADO.NET来读取Excel表里面的考勤数据
                    string connString;
                    if (extName == ".xls")
                    {
                        connString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes'", Server.MapPath(path));
                    }
                    else
                    {
                        connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes'", Server.MapPath(path));
                    }
                    string sql = "select 0,登记号或卡号,时间 from ['Sheet 1$']";
                    //准备一个数据适配器
                    OleDbDataAdapter da = new OleDbDataAdapter(sql, connString);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (BLL.AttendanceBll.ImportAttendData(dt))
                    {
                        //表示考勤导入成功
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('恭喜您，考勤导入成功！');window.parent.$('#dlg').dialog('close');", true);
                    }
                    else
                    {
                        //表示考勤导入失败
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('对不起，考勤导入失败！');", true);
                    }
                }
            }
            else
            {
                //表示别人没有上传文件
                //this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请上传包含考勤数据的文件！');", true);
            }
        }
    }
}