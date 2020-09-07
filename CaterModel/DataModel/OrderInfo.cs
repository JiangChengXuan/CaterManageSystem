using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.DataModel
{
    /// <summary>
    /// 订单信息
    /// </summary>
  public  class OrderInfo
    {
     [Description("AUTOINCREMENT")]
      public long OId { get; set; }
      public long MemberId { get; set; }


      public DateTime ODate { get; set; }

      public decimal OMoney { get; set; }
      public bool IsPay { get; set; }
      public long TableId { get; set; }
      public decimal Discount { get; set; }

    }
}
