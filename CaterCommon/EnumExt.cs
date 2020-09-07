using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel.Enum;
namespace CaterCommon
{
 public static   class EnumExt
    {

     public static string ToDesc(this CaterMessage en)
     {
         return ReflectHelper.GetFieldDescriptionAttr<CaterMessage>(en.ToString());
     }

    }
}
