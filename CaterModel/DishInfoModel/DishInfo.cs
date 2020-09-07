using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.DishInfoModel
{
    /// <summary>
    /// 菜品信息
    /// </summary>
   public partial class DishInfo
    {       

       public long DId { get; set; }
       public string DTitle { get; set; }
       public long DTypeId { get; set; }
       public decimal DPrice { get; set; }
       public string DChar { get; set; }
       public bool DIsDelete { get; set; }

    }
}
