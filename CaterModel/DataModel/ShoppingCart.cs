using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.DataModel
{
    /// <summary>
    /// 点菜购物车
    /// </summary>
  public  class ShoppingCart
    {
         [Description("AUTOINCREMENT")]
        public long SCId { get; set; }

        public long OrderId { get; set; }

        public long DishId { get; set; }

        public long Count { get; set; }

   

    }
}
