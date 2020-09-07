using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.Enum
{
    
   public enum TableState
    {

         [Description("空闲中")]
        IsFree=1,

         [Description("使用中")]
         NotFree = 0

    }
}
