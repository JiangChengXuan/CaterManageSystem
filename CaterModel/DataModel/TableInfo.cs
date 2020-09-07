using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.DataModel
{

    /// <summary>
    /// 餐桌管理
    /// </summary>
 public  class TableInfo
    {
         

        [Description("AUTOINCREMENT")]
        public long TId { get; set; }

        public string TTitle { get; set; }

       [Description("REFERENCES")]
        public long THallId { get; set; }

       public bool TIsFree { get; set; }

       public bool TIsDelete { get; set; }

       public long OrderId { get; set; }


    }
}
