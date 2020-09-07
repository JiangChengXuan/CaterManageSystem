using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel.Enum;
using System.Reflection;
using System.ComponentModel;
namespace CaterCommon
{
  public  class ReflectHelper
    {

      /// <summary>
        /// 针对某个类型返回DescriptionAttribute特性的 Description属性值
        /// 主要用于将枚举解释成对应的中文含义
      /// </summary>
      public static string GetFieldDescriptionAttr<T>(string enumItem)
      {
          string result = string.Empty;

          Type type =typeof(T);

          FieldInfo field = type.GetField(enumItem.ToString());
        
        DescriptionAttribute attr = field.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

        if (attr!=null)
        {
            result = attr.Description;
        }

          return result;
      }  // END GetFieldDescriptionAttr（）


     


    }
}
