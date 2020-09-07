using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel.DishTypeInfoModel;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;
using CaterModel.Enum;
namespace CaterDAL.DishTypeInfoPackage
{

    /// <summary>
    /// 菜品数据访问类
    /// </summary>
 public   class DishTypeInfoDAL:BaseDAL
    {

     /// <summary>
     /// 获取所有菜品分类
     /// </summary>
     public List<DishTypeInfo> GetList()
     {
         List<DishTypeInfo>list=new List<DishTypeInfo>();
         string sql = "select * from DishTypeInfo where DIsDelete=0";
         DataTable dt = dbHelper.GetDataTable(sql);
             DataTableToListColByDataTable<DishTypeInfo>(dt, list);
             return list;
     }  // END GetList（）


     /// <summary>
     /// 根据分类名称查询统计
     /// </summary>
     /// <param name="name"></param>
     /// <returns></returns>
     public int GetDishTypeInfoByName(string typeName)
     {
         string sql = "select count(DId)  from DishTypeInfo where  DTitle=@DTitle";
         int count = Convert.ToInt32(dbHelper.ExecuteScalar(sql, new SQLiteParameter("DTitle", typeName)));
         return count;
     }  //END GetDishTypeInfoByName（）

     /// <summary>
     /// 新增分类
     /// </summary>
     public int Add(DishTypeInfo data)
     {
         string sql = "insert into DishTypeInfo(DTitle,DIsDelete) values(@DTitle,0)";
         return dbHelper.ExecuteNoQuery(sql, new SQLiteParameter("DTitle", data.DTitle));

     } // END Add()

     /// <summary>
     /// 修改菜品分类
     /// </summary>
     public int Edit(DishTypeInfo data)
     {
         string sql = "update DishTypeInfo set DTitle=@DTitle where DId=@DId";

         List<DbParameter> paras = dbHelper.CreateParameter(sql, data);

         return dbHelper.ExecuteNoQuery(sql,paras.ToArray());
     }  // END Edit（）


     /// <summary>
     /// 修改删除状态
     /// </summary>
     public int EditFieldDIsDelete(DeleteState deleteState,int id)
     {
         string sql = string.Format("update DishTypeInfo set DIsDelete={0} where DId=@DId",Convert.ToInt32(deleteState));

         return dbHelper.ExecuteNoQuery(sql, new SQLiteParameter("DId", id));
     } // END EditFieldDIsDelete（）


    }
}
