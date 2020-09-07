using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.MemberInfoModel
{
    /// <summary>
    /// 用于在窗体展示表格的实体
    /// </summary>
   public class MemberInfoTableShow : MemberInfo
    {
       /// <summary>
       /// 会员类型名称
       /// </summary>
       public string MTitle { get; set; }

       public decimal MDiscount { get; set; }

    }
}
