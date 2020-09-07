using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.Enum
{
   /// <summary>
   /// 用户返回客户端处理的消息
   /// </summary>
   public enum CaterMessage
    {
       [Description("")]
       Default,

       [Description("处理成功")]
       Success,

       [Description("处理失败")]
       Fail,

       [Description("数据已经存在不能重复添加")]
       IsExist,

    }
}
