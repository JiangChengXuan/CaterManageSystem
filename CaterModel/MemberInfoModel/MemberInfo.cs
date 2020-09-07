using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.MemberInfoModel
{
    /// <summary>
    /// 会员信息类
    /// </summary>
  public  partial class MemberInfo
    {


      public long MId { get; set; }
      public long MTypeId { get; set; }
      public string MName { get; set; }

      public string MPhone { get; set; }

      public decimal MMoney { get; set; }

      public bool MIsDelete;

    }
}
