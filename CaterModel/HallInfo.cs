using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    /// <summary>
    /// 餐厅包厢信息
    /// </summary>
   public class HallInfo
    {
        [Description("AUTOINCREMENT")]
        public long HId { get; set; }
        public string HTitle { get; set; }

        public bool HIsDelete { get; set; }
    }
}
