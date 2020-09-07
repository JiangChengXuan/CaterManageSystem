using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using CaterDAL;
using CaterCommon;
using CaterModel.Enum;
namespace CaterBLL
{
  public  class HallInfoBAL
    {

      private HallInfoDAL dal = new HallInfoDAL();

      /// <summary>
      /// 获取所有未删除的包厢信息
      /// </summary>
      public List<HallInfo> GetList()
      {
          return dal.Select<HallInfo>(new HallInfo {
              HIsDelete = Convert.ToBoolean((int)DeleteState.NotDelete)
          }, "HIsDelete");
      }  // END GetList（）



      /// <summary>
      /// 添加包厢信息或修改包厢信息
      /// </summary>
      public bool AddOrUpdate(HallInfo data,out CaterMessage msgEnum)
      {
          msgEnum = CaterMessage.Success;

          //判断添加数据是否存在
          if (dal.Count<HallInfo>(new string[] { "HId", "HTitle", data.HTitle }, data)
              > 0)
          {
              msgEnum = CaterMessage.IsExist;
              return false;
          }

          int rowCount = -1;
          if (data.HId>0)  // 修改
          {
              rowCount = dal.InsertOrUpdate<HallInfo>(data, SQLType.Update);
          }
          else // 新增
          {
              rowCount = dal.InsertOrUpdate<HallInfo>(data, SQLType.Insert);
          }

          if (rowCount<=0)
          {
               msgEnum = CaterMessage.Fail;
              return false;
          }

          return true;
      }  // END AddOrUpdate（）

      /// <summary>
      /// 删除包厢信息
      /// </summary>
      public bool Delete(int hId,out CaterMessage msgEnum)
      {
          msgEnum = CaterMessage.Success;

          int rowCount = dal.InsertOrUpdate<HallInfo>
              (new HallInfo { HId = hId, HIsDelete = Convert.ToBoolean((int)DeleteState.Deleted) }
              , SQLType.Update, "HId", "HIsDelete");

          if (rowCount <= 0)
          {
              msgEnum = CaterMessage.Fail;
              return false;
          }

          return true;
      } // END Delete（）


    }
}
