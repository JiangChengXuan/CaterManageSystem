using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDAL;
using CaterModel;
using CaterModel.Enum;
using CaterModel.DataModel;
using CaterModel.MemberInfoModel;

namespace CaterBLL
{

    /// <summary>
    /// 订单信息业务逻辑类
    /// </summary>
 public   class OrderInfoBAL
    {

     private OrderInfoDAL dal = new OrderInfoDAL();

     /// <summary>
     /// 开单
     /// </summary>
     /// <returns>订单号</returns>
     public long PlaceOrder(OrderInfo data)
     {
         

         //新增订单信息
         long orderId = dal.InsertReturnID(data);

         //占桌：改变餐桌空闲状态，并关联订单号
         if (orderId > 0)
         {
             TableInfoBAL tableBAL = new TableInfoBAL();
             bool result = tableBAL.Occupy(data.TableId, orderId);
             if (!result) 
             {            
                  return -1;
             }
         }
         else
         {          
             return -1;
         }

         return orderId;
     }  // END PlaceOrder（）


     /// <summary>
     /// 修改订单消费总金额
     /// </summary>
     /// <param name="orderId"></param>
     /// <param name="totalMoeny"></param>
     /// <returns></returns>
     public int UpdateOrderTotalMoeny(long orderId,decimal totalMoeny)
     {
         OrderInfo data = new OrderInfo() { OMoney = totalMoeny, OId = orderId };

     return dal.Update(data, new Dictionary<List<string>, List<string>>() 
              {
              {new List<string>{"OId" },new List<string>{"OMoney"}}
              });
  
     } // END UpdateOrderTotalMoeny（）


     /// <summary>
     /// 根据订单编号获取订单信息
     /// </summary>
     /// <param name="orderId">订单编号</param>
     /// <returns></returns>
     public OrderInfo GetOrderInfoById(long orderId)
     {
 
       List<OrderInfo>list = dal.Select(new OrderInfo { OId = orderId }, "OId");

       if (list.Count>0)
       {
           return list[0];
       }

         return null;
     } // END GetOrderInfoById（）


     /// <summary>
     /// 订单进行结账
     /// </summary>
     /// <param name="orderInfo">订单对象</param>
     /// <param name="member">会员对象</param>
     /// <returns>处理结果</returns>
     public bool OrderCheckOut(OrderInfo orderInfo, MemberInfoTableShow member, bool IsBalancePay, out CaterMessage msg)
     {

         msg = CaterMessage.Success;

         int rowCount = dal.OrderCheckOut(orderInfo, member, IsBalancePay);

         if (rowCount<=0)
         {
             msg = CaterMessage.Fail;
             return false;
         }

         return true;
     } // END OrderCheckOut（）


    }
}
