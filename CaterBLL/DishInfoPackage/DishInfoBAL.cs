using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel.DishInfoModel;
using CaterDAL.DishInfoPackage;
using CaterModel.Enum;
namespace CaterBLL.DishInfoPackage
{
 public   class DishInfoBAL
    {

     DishInfoDAL dal = new DishInfoDAL();

     public List<DishInfoShow> GetList(Dictionary<string, string> dicWhere = null)
     {
         return dal.GetList(dicWhere);
     } // END GetList（）


     /// <summary>
     /// 菜品添加或修改操作
     /// </summary>
     /// <returns></returns>
     public bool AddOrUpdate(DishInfo data,out CaterMessage msg)
     {
         msg = CaterMessage.Success;
         int rowCount = -1;

         if (IsAdd(data.DId)) //添加
         {
             #region 添加
             //判断菜品是否存在
             int count = dal.QueryCount(data.DTitle);
             if (count > 0)
             {
                 msg = CaterMessage.IsExist;
                 return false;
             }
             rowCount = dal.Add(data);       
             #endregion
         }
         else //修改
         {
             rowCount = dal.Edit(data);
         }

         if (rowCount <= 0)
         {
             msg = CaterMessage.Fail;
             return false;
         } 

         return true;
     } // END AddOrUpdate（）


     private bool IsAdd(long dId)
     {
         if (dId==null||dId<=0)
         {
             return true;
         }
         return false;
     }


     /// <summary>
     /// 删除菜品信息
     /// </summary>
     public bool Delete(int dId,out CaterMessage msg)
     {
         msg = CaterMessage.Success;

         int rowCount = dal.EditDIsDelete(dId, DeleteState.Deleted);

         if (rowCount<=0)
         {
             msg = CaterMessage.Fail;
             return false;
         }

         return true;
     } // END Delete（）


    }
}
