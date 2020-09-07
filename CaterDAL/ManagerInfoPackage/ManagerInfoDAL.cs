using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CaterModel;
using CaterDAL.DBAccess;
using CaterCommon;
using System.Reflection;
using System.Data.SQLite;
using System.Data.Common;
namespace CaterDAL.ManagerInfoPackage
{
  public  class ManagerInfoDAL:BaseDAL
    {

    

      public List<ManagerInfo> GetManagerInfoList()
      {
          List<ManagerInfo> list = new List<ManagerInfo>();

          string sql = "select *  from ManagerInfo";
          DataTable dt = dbHelper.GetDataTable(sql);

          DataTableToListColByModel<ManagerInfo>(dt, list);
          return list;
      }  //END GetManagerInfoList（）


    

      /// <summary>
      /// 添加管理员
      /// </summary>
      public int AddManagerInfo(ManagerInfo managerInfo, Func<string, string> Encryption)
      {
          SQLiteParameter[] paras = new SQLiteParameter[] {
          new SQLiteParameter("MName",managerInfo.MName),
           new SQLiteParameter("MPwd",Encryption(managerInfo.MPwd)),
           new SQLiteParameter("MType",managerInfo.MType)
          };

          string sql = "insert into ManagerInfo(MName,MPwd,MType) values(@MName,@MPwd,@MType)";

         return dbHelper.ExecuteNoQuery(sql, paras);

      }  // END AddManagerInfo（）

      /// <summary>
      /// 修改管理员
      /// </summary>

      public int EditManagerInfo(ManagerInfo managerInfo, Func<string, string> Encryption)
      {
          string sql = "update ManagerInfo set MName=@MName,MType=@MType,MPwd=@MPwd";
          sql += "  where MId=@MId";         

          if (managerInfo.MPwd==null) //没有修改密码
          {
              sql=sql.Replace(",MPwd=@MPwd", "");
          }
          else
          {
              managerInfo.MPwd = Encryption(managerInfo.MPwd);
          }

          List<DbParameter> paras = dbHelper.CreateParameter(sql, managerInfo);

          return dbHelper.ExecuteNoQuery(sql, paras.ToArray());


      } //EditManagerInfo

      public int DeleteManagerInfo(int MId)
      {
          string sql = "delete from ManagerInfo where MId=@MId";

          return dbHelper.ExecuteNoQuery(sql, new SQLiteParameter("MId",MId));
           
      }


      /// <summary>
      /// 根据用户名查询管理员对象
      /// </summary>
      /// <returns></returns>
      public ManagerInfo GetManagerInfoByMName(string mName)
      {
          ManagerInfo managerInfo = null;

          string sql = "select * from ManagerInfo where MName=@MName";

          DataTable dt = dbHelper.GetDataTable(sql, new SQLiteParameter("MName", mName));

          if (dt.Rows.Count>0) //存在对应的用户
          {
              List<ManagerInfo> list = new List<ManagerInfo>();
               DataTableToListColByModel<ManagerInfo>(dt, list);
               managerInfo = list[0];
          }

          return managerInfo;
      } // END GetManagerInfoByMName（）


    }
}
