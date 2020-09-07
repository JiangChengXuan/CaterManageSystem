using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.DishTypeInfoModel
{
    /// <summary>
    /// 菜品分类
    /// </summary>
   public class DishTypeInfo
    {

        public long DId { get; set; }

        public string DTitle { get; set; }
        public bool DIsDelete { get; set; }

    }
}
