using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.DataModel
{

    /// <summary>
    /// 订单详情表-描述订单消费的菜品信息
    /// </summary>
  public  class OrderDetail
    {
       

          [Description("AUTOINCREMENT")]
        public long DetailId { get; set; }

          public long OrderId { get; set; }

          public long DishId { get; set; }

          public long PayQuantity { get; set; }

    }
}
