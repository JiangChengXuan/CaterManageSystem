using CaterDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using CaterModel.Enum;
using CaterModel.DataModel;
namespace CaterBLL
{
  public  class ShoppingCartBAL
    {

      private ShoppingCartDAL dal = new ShoppingCartDAL();


     /// <summary>
      /// 根据订单点菜
     /// </summary>
     /// <param name="orderId">订单ID</param>
     /// <param name="dishId">菜品ID</param>
     /// <returns>是否处理成功</returns>
      public bool OrderDishes(long orderId, long dishId, out CaterMessage msgEnum)
      {
           msgEnum = CaterMessage.Success;

          //封装数据
          ShoppingCart inputData = new ShoppingCart();        
          inputData.OrderId = orderId;
          inputData.DishId = dishId;

          //点菜之前进行判断，如果订单没有点过则新增，如果点过则进行数量的累加
        var ShoppingCartList = dal.Select(inputData, "OrderId", "DishId");
        int rowCount = -1;

          //执行数据操作
        if (ShoppingCartList.Count>0) //数量累加
        {
            var data = ShoppingCartList[0];
            inputData.Count = data.Count+1;
            rowCount = dal.Update(inputData, new Dictionary<List<string>, List<string>>()
            { 
                {new List<string>{"OrderId","DishId"},new List<string>{"Count"}}
            });
        }
        else // 新增一行
        {
            inputData.Count = 1;
            rowCount = dal.InsertOrUpdate(inputData, SQLType.Insert);
        }

 
        if (rowCount<=0) //处理失败
        {
            msgEnum = CaterMessage.Fail;
            return false;
        }

          return true;
      } // END order dishes（）


      /// <summary>
      /// 根据订单ID获取对应的详情(菜品)信息
      /// </summary>
      /// <param name="orderId"></param>
      /// <returns></returns>
      public List<ShoppingCart> GetListBy(long orderId)
      {
          List<ShoppingCart> list = new List<ShoppingCart>();

          list = dal.Select(new ShoppingCart { OrderId = orderId }, "OrderId");

          return list;
      }// END GetList () 

 

    /// <summary>
      /// 删除购物车中指定的菜品信息
    /// </summary>
    /// <param name="scId">购物车主键</param>
    /// <param name="delNum">要删除的数量</param>
    /// <param name="nowNum">已选购数量</param>
    /// <param name="msg"></param>
    /// <returns></returns>
      public bool Delete(long scId, long delNum, long nowNum, out CaterMessage msg)
      {
          msg = CaterMessage.Success;
          int rowCount = -1;
 
       
          //要删除的数量等于或超过已选数量，则进行数据删除
          if (delNum>=nowNum)
          {
              rowCount=dal.Delete(new ShoppingCart { SCId = scId }, "SCId");
          }
          else //小于已选购数量，则进行数量上的修改
          {
              rowCount=dal.Update(new ShoppingCart
              {
                  Count = nowNum - delNum,
                   SCId=scId
              }, new Dictionary<List<string>, List<string>>() 
              {
              {new List<string>{"SCId"},new List<string>{"Count"}}
              });
          }

          if (rowCount <= 0)
          {
              msg = CaterMessage.Fail;
              return false;
          }

          return true;
      } // END ReturnDish（）

    }
}
