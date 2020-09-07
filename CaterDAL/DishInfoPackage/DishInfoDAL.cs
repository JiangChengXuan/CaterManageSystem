using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using CaterModel.DishInfoModel;
using CaterModel.Enum;
using System.Data.SQLite;
namespace CaterDAL.DishInfoPackage
{
    /// <summary>
    /// 菜品信息数据访问类
    /// </summary>
   public class DishInfoDAL:BaseDAL
    {

        /// <summary>
        /// 获取用于绑定表格的菜品信息集合
        /// </summary>
        /// <param name="dicParas">查询条件 key:属性名称、value:查询条件值</param>
       public List<DishInfoShow> GetList(Dictionary<string,string>dicWhere=null)
       {
           List<DishInfoShow> list = new List<DishInfoShow>();
           List<SQLiteParameter> paras = new List<SQLiteParameter>();

           StringBuilder sql = new StringBuilder("select  t1.DId,t1.DTitle,t1.DPrice,");
           sql.Append("t1.DTypeId||'@'||t2.DTitle as DTypeTitle,t1.DChar  from DishInfo t1 ");
           sql.Append("inner join DishTypeInfo t2 on t1.DTypeId=t2.DId ");
           sql.AppendFormat("where t1.DIsDelete={0} and t2.DIsDelete={0}", (int)DeleteState.NotDelete);

           //处理条件查询
           if (dicWhere!=null&&dicWhere.Count>0)
           {
               foreach (KeyValuePair<string,string> item in dicWhere)
               {
                   sql.AppendFormat(" and t1.{0} like @{0}", item.Key);
                   paras.Add(new SQLiteParameter(item.Key,'%'+item.Value+'%'));
               }
           }

           DataTable dt = dbHelper.GetDataTable(sql.ToString(),paras.ToArray());

           DataTableToListColByDataTable(dt, list);

           return list;
       } // END GetList（）


       /// <summary>
       /// 根据菜品名称查询统计
       /// </summary>
       /// <param name="dishInfoName">菜品名称</param>
       public int QueryCount(string dishInfoName)
       {
           string sql = "select Count(DId) from DishInfo where DTitle=@DTitle";

           var count = dbHelper.ExecuteScalar(sql, new SQLiteParameter("DTitle", dishInfoName));

           return Convert.ToInt32(count);
       } // END QueryCount（）


       /// <summary>
       /// 新增菜品信息
       /// </summary>
       public int Add(DishInfo data)
       {
           StringBuilder sql = new StringBuilder("insert into DishInfo");
           sql.Append("(DTitle,DTypeId,DPrice,DChar,DIsDelete)  ");
           sql.AppendFormat("values(@DTitle,@DTypeId,@DPrice,@DChar,{0})", (int)DeleteState.NotDelete);

           List<DbParameter> paras = dbHelper.CreateParameter(sql.ToString(), data);

           return dbHelper.ExecuteNoQuery(sql.ToString(),paras.ToArray());
       }  // END Add（）


       /// <summary>
       /// 修改菜品信息
       /// </summary>
       public int Edit(DishInfo data)
       {
           StringBuilder sql = new StringBuilder("update DishInfo set DTitle=@DTitle");
           sql.Append(",DTypeId=@DTypeId,DPrice=@DPrice,DChar=@DChar");
           sql.Append(" where DId=@DId");

           List<DbParameter> paras = dbHelper.CreateParameter(sql.ToString(), data);

           return dbHelper.ExecuteNoQuery(sql.ToString(), paras.ToArray());

       } // END Edit（）


       /// <summary>
       /// 修改删除状态
       /// </summary>
       public int EditDIsDelete(int dId,DeleteState state)
       {
           string sql = string.Format("update DishInfo set DIsDelete={0}  where DId=@DId ",(int)state);
           return dbHelper.ExecuteNoQuery(sql, new SQLiteParameter("DId", dId));
       } // END EditDIsDelete（）


    }
}
