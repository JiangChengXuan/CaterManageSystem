using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel.ViewModel;
using CaterModel.Enum;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
namespace CaterDAL
{
  public  class TableInfoDAL:BaseDAL
    {

      /// <summary>
      /// 获取所有未删除的餐桌信息
      /// </summary>
      /// <param name="where">查询条件</param>
      /// <returns></returns>
      public List<TableInfoView> GetList(Dictionary<string,string> where=null)
      {
          List<TableInfoView> list = new List<TableInfoView>();

          StringBuilder sql = new StringBuilder("select t1.TId,t1.TTitle,t2.HTitle,");
          sql.Append("t1.TIsFree,t1.OrderId from TableInfo t1 ");
          sql.Append("inner join HallInfo t2 on t1.THallId=t2.HId ");
          sql.AppendFormat(" where t1.TIsDelete={0} and t2.HIsDelete={0}  ",(int)DeleteState.NotDelete);

          List<SQLiteParameter> paras = new List<SQLiteParameter>();
          if (where!=null&&where.Count>0)  // 指定了查询条件
          {
              foreach (KeyValuePair<string,string> item in where)
              {
                  sql.AppendFormat(" and {0} like @{0}",item.Key);
                  paras.Add(new SQLiteParameter (item.Key,"%"+item.Value+"%"));
              }
          }

          DataTable dt = dbHelper.GetDataTable(sql.ToString(),paras.ToArray());

          DataTableToListColByDataTable(dt, list);

          return list;
      } // END GetList（）

    }
}
