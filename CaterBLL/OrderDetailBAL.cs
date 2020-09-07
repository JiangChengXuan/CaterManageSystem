using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDAL;
using CaterModel.DataModel;
using CaterModel.Enum;
namespace CaterBLL
{
  public  class OrderDetailBAL
    {

      private OrderDetailDAL dal = new OrderDetailDAL();


      /// <summary>
      /// 根据订单编号查询订单详情(菜品信息)
      /// </summary>
      /// <param name="orderId">订单编号</param>
      public List<OrderDetail> GetListBy(long orderId)
      {
          return dal.Select(new OrderDetail { OrderId = orderId }, "OrderId");

      } // END GetList（）


      /// <summary>
      /// 提交已点菜品进行下单
      /// </summary>
      /// <returns></returns>
      public bool SubmitDishes(List<ShoppingCart> ShoppingCart, decimal totalMoeny, out CaterMessage msg)
      {
          msg = CaterMessage.Success;
          int rowCount = 0;

        
          //使用订单编号查询是否存在对应的详情信息，如果不存在则新增，如果存在则进行数量的修改
          long orderId = ShoppingCart[0].OrderId;
          List<OrderDetail> OrderDetailList = GetListBy(orderId);

          #region  Step1-购物车里的菜品信息  录入到到订单详情信息中
 
          //遍历购物车的菜品信息
          foreach (ShoppingCart dish in ShoppingCart) //此处的操作需要使用到事务
          {
              #region Step1-1-数据封装
              //封装录入的数据对象
              OrderDetail data = new OrderDetail();
              data.DishId = dish.DishId;
              data.OrderId = dish.OrderId;
              data.PayQuantity = dish.Count; 
              #endregion


              #region Step1-2-数据操作

              if (DishIsConfirm(dish.OrderId, dish.DishId) == "是")//存在则进行数量的修改：客户加菜的场景
              {
                  rowCount += dal.Update(data, new Dictionary<List<string>, List<string>>() 
                          {
                            {new List<string>{"OrderId","DishId"},new List<string>{"PayQuantity"}}
                          });
              }
              else//不存在则新增，首次点菜下单
              {
                  rowCount += dal.InsertOrUpdate(data, SQLType.Insert);
              } 

              #endregion
 
          }

          #endregion


          #region Step2-将总金额录入到订单信息表中

          //将总金额录入到订单信息表中
          rowCount += new OrderInfoBAL().UpdateOrderTotalMoeny(orderId, totalMoeny);

          if (rowCount <= 1) //执行失败
          {
              msg = CaterMessage.Fail;
              return false;
          } 

          #endregion

          return true;
      } // END SubmitDishes（）


      /// <summary>
      /// 查询购物车某个菜品是否已经下单
      /// </summary>
      /// <param name="orderId">订单编号</param>
      /// <param name="dishId">菜品ID</param>
      /// <returns></returns>
      public string DishIsConfirm(long orderId,long dishId)
      {
          string result = string.Empty;

        List<OrderDetail>data=  dal.Select(new OrderDetail { OrderId = orderId, DishId = dishId }
              , "OrderId", "DishId");

        if (data!=null&&data.Count>0)
        {
            result = "是";
        }
        else
        {
            result = "否";
        }

          return result;
      }  // END DishIsConfirm（）

    }
}
