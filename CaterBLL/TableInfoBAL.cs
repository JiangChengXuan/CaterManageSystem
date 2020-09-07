using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel.ViewModel;
using CaterDAL;
using CaterModel;
using CaterModel.Enum;
using CaterModel.DataModel;
namespace CaterBLL
{

    /// <summary>
    /// 餐桌业务逻辑类
    /// </summary>
  public  class TableInfoBAL
    {

      private TableInfoDAL dal = new TableInfoDAL();

      public List<TableInfoView> GetList(Dictionary<string,string> where)
      {
          return dal.GetList(where);
      }

      public List<KeyValue> GetTableState()
      {
          return dal.GetEnumData<TableState>(true);
      }


      public bool AddOrUpdate(TableInfo data, out CaterMessage msgEnum)
      {
          msgEnum = CaterMessage.Success;

          //使用餐桌名称判断是否已经存在
          int IsExist = dal.Count<TableInfo>(new string[] { "TId", "TTitle", data .TTitle}, data);
          if (IsExist>0)
          {
              msgEnum = CaterMessage.IsExist;
              return false;
          }

          int rowCount = -1;
          if (data.TId<=0) // 新增
          {
              rowCount = dal.InsertOrUpdate<TableInfo>(data, SQLType.Insert);
          }
          else // 修改
          {
              rowCount = dal.InsertOrUpdate<TableInfo>(data, SQLType.Update);
          }

          if (rowCount<=0)
          {
              msgEnum = CaterMessage.Fail;
              return false;
          }

          return true;
      }  // END AddOrUpdate（）


      public bool Delete(int id,out CaterMessage msgEnum)
      {
          msgEnum = CaterMessage.Success;

          int rowCount = dal.InsertOrUpdate<TableInfo>
              (new TableInfo { TIsDelete = Convert.ToBoolean((int)DeleteState.Deleted), TId=id }
              , SQLType.Update, "TIsDelete");

          if (rowCount<=0)
          {
              msgEnum = CaterMessage.Fail;
              return false;
          }

          return true;
      } // END Delete（）


      /// <summary>
      /// 占用餐桌
      /// </summary>
      /// <returns></returns>
      public bool Occupy(long id,long orderId)
      {
          TableInfo data=new TableInfo(){ TId=id, TIsFree=false, OrderId=orderId};
          return dal.InsertOrUpdate<TableInfo>(data, SQLType.Update, "TIsFree", "OrderId") > 0;

      } // END Occupy（）

    }
}
